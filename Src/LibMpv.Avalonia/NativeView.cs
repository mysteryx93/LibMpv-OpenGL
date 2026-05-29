using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Runtime.InteropServices;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Data;
using Avalonia.Layout;
using Avalonia.LogicalTree;
using Avalonia.Media;
using Avalonia.Metadata;
using Avalonia.Platform;
using Avalonia.VisualTree;

namespace HanumanInstitute.LibMpv.Avalonia;

#if !ANDROID
public class NativeView : NativeControlHost, IVideoView
{
    public static readonly StyledProperty<object?> ContentProperty =
        ContentControl.ContentProperty.AddOwner<NativeView>();

    private IPlatformHandle? _platformHandle;

    private bool _attached;
    private Window? _floatingContent;
    private IDisposable? _disposables;
    private IDisposable? _isEffectivelyVisibleSub;

    [DllImport("user32.dll", SetLastError = false)]
    private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

    private sealed class NativeMpvContext : MpvContext
    {
        protected override void OnPreInitialize() => SetOptionString("hwdec", "auto");
    }

    // MpvContext property
    public static readonly DirectProperty<NativeView, MpvContext> MpvContextProperty = AvaloniaProperty.RegisterDirect<NativeView, MpvContext>(
        nameof(MpvContext), o => o.MpvContext, defaultBindingMode: BindingMode.OneWayToSource);
    public MpvContext MpvContext { get; } = new NativeMpvContext();

    static NativeView()
    {
        ContentProperty.Changed.AddClassHandler<NativeView>((s, e) => s.InitializeNativeOverlay());
        IsVisibleProperty.Changed.AddClassHandler<NativeView>((s, e) => s.ShowNativeOverlay(s.IsVisible));
    }


    [Content]
    public object? Content
    {
        get => GetValue(ContentProperty);
        set => SetValue(ContentProperty, value);
    }

    protected override IPlatformHandle CreateNativeControlCore(IPlatformHandle parent)
    {
        _platformHandle = base.CreateNativeControlCore(parent);
        MpvContext.StartNativeRendering(_platformHandle.Handle.ToInt64());
        MpvContext.Tick += OnMpvTick;
        return _platformHandle;
    }

    protected override void DestroyNativeControlCore(IPlatformHandle control)
    {
        MpvContext.Tick -= OnMpvTick;
        MpvContext.StopRendering();
        // Hide the HWND before Avalonia calls SetParent(NULL) inside base — prevents
        // it from briefly appearing as a top-level floating window before DestroyWindow.
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            try { ShowWindow(control.Handle, 0 /* SW_HIDE */); } catch { }
        base.DestroyNativeControlCore(control);
        _platformHandle = null;
    }

    private void OnMpvTick(object? sender, EventArgs e) => MpvContext.InvokePreRender();

    protected override void OnAttachedToVisualTree(VisualTreeAttachmentEventArgs e)
    {
        base.OnAttachedToVisualTree(e);

        _attached = true;

        // Fast re-attach: if the HWND is still alive (DestroyNativeControlCore hasn't run yet),
        // StopRendering() may have zeroed `wid` during the detach. Restore it so MPV renders to
        // this HWND instead of creating its own OS window.
        if (_platformHandle != null)
            MpvContext.StartNativeRendering(_platformHandle.Handle.ToInt64());

        InitializeNativeOverlay();

        _isEffectivelyVisibleSub = this.GetVisualAncestors().OfType<Control>()
            .Select(v => v.GetObservable(IsVisibleProperty))
            .CombineLatest(v => !v.Any(o => !o))
            .DistinctUntilChanged()
            .Subscribe(v => IsVisible = v);
    }

    protected override void OnDetachedFromVisualTree(VisualTreeAttachmentEventArgs e)
    {
        base.OnDetachedFromVisualTree(e);

        _isEffectivelyVisibleSub?.Dispose();

        ShowNativeOverlay(false);

        _attached = false;
    }

    protected override void OnDetachedFromLogicalTree(LogicalTreeAttachmentEventArgs e)
    {
        base.OnDetachedFromLogicalTree(e);

        _disposables?.Dispose();
        _disposables = null;
        _floatingContent?.Close();
        _floatingContent = null;
    }

    private void InitializeNativeOverlay()
    {
        if (!this.IsAttachedToVisualTree()) return;

        if (_floatingContent == null && Content != null)
        {
            _floatingContent = new Window
            {
                WindowDecorations = WindowDecorations.None,
                TransparencyLevelHint = new[] { WindowTransparencyLevel.Transparent },
                Background = Brushes.Transparent,
                SizeToContent = SizeToContent.WidthAndHeight,
                ShowInTaskbar = false
            };

            var parentWindow = TopLevel.GetTopLevel(this) as Window;
            var disposables = new CompositeDisposable()
            {
                _floatingContent.Bind(Window.ContentProperty, this.GetObservable(ContentProperty)),
                this.GetObservable(ContentProperty).Skip(1).Subscribe(_ => UpdateOverlayPosition()),
                this.GetObservable(BoundsProperty).Skip(1).Subscribe(_ => UpdateOverlayPosition()),
            };
            if (parentWindow != null)
            {
                disposables.Add(Observable.FromEventPattern(parentWindow, nameof(Window.PositionChanged))
                    .Subscribe(_ => UpdateOverlayPosition()));
            }
            _disposables = disposables;
        }

        ShowNativeOverlay(IsEffectivelyVisible);
    }

    private void ShowNativeOverlay(bool show)
    {
        if (_floatingContent == null || _floatingContent.IsVisible == show)
            return;

        if (show && _attached && TopLevel.GetTopLevel(this) is Window owner)
            _floatingContent.Show(owner);
        else
            _floatingContent.Hide();
    }

    private void UpdateOverlayPosition()
    {
        if (_floatingContent == null) { return; }

        bool forceSetWidth = false, forceSetHeight = false;

        var topLeft = new Point();

        var child = _floatingContent.Presenter?.Child;

        if (child?.IsArrangeValid == true)
        {
            switch (child.HorizontalAlignment)
            {
                case HorizontalAlignment.Right:
                    topLeft = topLeft.WithX(Bounds.Width - _floatingContent.Bounds.Width);
                    break;

                case HorizontalAlignment.Center:
                    topLeft = topLeft.WithX((Bounds.Width - _floatingContent.Bounds.Width) / 2);
                    break;

                case HorizontalAlignment.Stretch:
                    forceSetWidth = true;
                    break;
            }

            switch (child.VerticalAlignment)
            {
                case VerticalAlignment.Bottom:
                    topLeft = topLeft.WithY(Bounds.Height - _floatingContent.Bounds.Height);
                    break;

                case VerticalAlignment.Center:
                    topLeft = topLeft.WithY((Bounds.Height - _floatingContent.Bounds.Height) / 2);
                    break;

                case VerticalAlignment.Stretch:
                    forceSetHeight = true;
                    break;
            }
        }

        if (forceSetWidth && forceSetHeight)
            _floatingContent.SizeToContent = SizeToContent.Manual;
        else if (forceSetHeight)
            _floatingContent.SizeToContent = SizeToContent.Width;
        else if (forceSetWidth)
            _floatingContent.SizeToContent = SizeToContent.Height;
        else
            _floatingContent.SizeToContent = SizeToContent.Manual;

        _floatingContent.Width = forceSetWidth ? Bounds.Width : double.NaN;
        _floatingContent.Height = forceSetHeight ? Bounds.Height : double.NaN;

        _floatingContent.MaxWidth = Bounds.Width;
        _floatingContent.MaxHeight = Bounds.Height;

        var newPosition = this.PointToScreen(topLeft);

        if (newPosition != _floatingContent.Position)
        {
            _floatingContent.Position = newPosition;
        }
    }

    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            _disposables?.Dispose();
            _isEffectivelyVisibleSub?.Dispose();
            MpvContext.Dispose();
        }
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}
#endif
