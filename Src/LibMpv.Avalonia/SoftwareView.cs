using Avalonia;
using Avalonia.Controls;
using Avalonia.Data;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using Avalonia.Threading;

namespace HanumanInstitute.LibMpv.Avalonia;

public class SoftwareView : Control, IVideoView
{
    private sealed class SoftwareMpvContext : MpvContext
    {
        protected override void OnPreInitialize()
        {
            this.SetOptionString("vo", "libmpv");
            
#if ANDROID
            this.SetOptionString("gpu-debug", "yes");
            this.SetOptionString("gpu-context", "android");
#endif
        } 
    }

    private WriteableBitmap? _renderTarget;
    private volatile bool _isIdle;
    
    private double _renderScaling = 1.0;
    private int _renderQueued;
    
#if ANDROID
    private const string PixelFormatName = "rgba";
#else
    private const string PixelFormatName = "bgra";
#endif


    // MpvContext property
    public static readonly DirectProperty<SoftwareView, MpvContext> MpvContextProperty = AvaloniaProperty.RegisterDirect<SoftwareView, MpvContext>(
        nameof(MpvContext), o => o.MpvContext, defaultBindingMode: BindingMode.OneWayToSource);
    public MpvContext MpvContext { get; } = new SoftwareMpvContext();

    public SoftwareView()
    {
        ClipToBounds = true;
    }

    protected override void OnInitialized()
    {
        MpvContext.Idle += OnMpvIdle;
        MpvContext.StartFile += OnMpvStartFile;
        MpvContext.StartSoftwareRendering(this.UpdateVideoView);
        base.OnInitialized();
    }

    private void OnMpvIdle(object? sender, EventArgs e)
    {
        _isIdle = true;
        UpdateVideoView();
    }

    private void OnMpvStartFile(object? sender, MpvStartFileEventArgs e) => _isIdle = false;

    public override void Render(DrawingContext context)
    {
        if (_isIdle)
        {
            context.FillRectangle(Brushes.Black, new Rect(Bounds.Size));
            return;
        }
        
        if (TopLevel.GetTopLevel(this) == null) { return; }
        
        if (Bounds.Width < 1 || Bounds.Height < 1) { return; }

        var bitmapSize = GetPixelSize();

        if (_renderTarget == null || _renderTarget.PixelSize.Width != bitmapSize.Width || _renderTarget.PixelSize.Height != bitmapSize.Height)
        {
            _renderTarget?.Dispose();
            _renderTarget = new WriteableBitmap(bitmapSize, new Vector(96, 96), PixelFormat.Bgra8888, AlphaFormat.Premul);
        }
        
        using (var lockedBitmap = this._renderTarget.Lock())
        {
            MpvContext.InvokePreRender();
            MpvContext.SoftwareRender(
                lockedBitmap.Size.Width, 
                lockedBitmap.Size.Height, 
                lockedBitmap.Address, 
                PixelFormatName);
        }

        context.DrawImage(
            _renderTarget,
            new Rect(Bounds.Size));
    }

    private PixelSize GetPixelSize()
    {
        var scaling = TopLevel.GetTopLevel(this)?.RenderScaling ?? 1.0;
        return new PixelSize(Math.Max(1, (int)(Bounds.Width * scaling)), Math.Max(1, (int)(Bounds.Height * scaling)));
    }

    private void UpdateVideoView()
    {
        if(Interlocked.Exchange(ref _renderQueued, 1) == 1)
            return;
        
        
        this.Dispatcher.Post(() =>
        {
            _renderQueued = 0;

            InvalidateVisual();
        }, DispatcherPriority.Render);
    }

    protected virtual void Dispose(bool disposing)
    {
        // ReleaseUnmanagedResources();
        
        if(!disposing)
            return;
        
        MpvContext.Idle -= OnMpvIdle;
        MpvContext.StartFile -= OnMpvStartFile;
        MpvContext.Dispose();
        _renderTarget?.Dispose();
    }
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
    
    ~SoftwareView()
    {
        Dispose(false);
    }

}
