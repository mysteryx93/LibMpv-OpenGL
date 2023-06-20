using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using Avalonia.Threading;

namespace LibMpv.Avalonia;

public class SoftwareVideoView : Control, IVideoView
{
    private WriteableBitmap? _renderTarget;
    public MpvContext MpvContext { get; } = new();

    public SoftwareVideoView()
    {
        ClipToBounds = true;
    }

    protected override void OnInitialized()
    {
        MpvContext.StartSoftwareRendering(this.UpdateVideoView);
        base.OnInitialized();
    }

    public override void Render(DrawingContext context)
    {
        if (VisualRoot == null) { return; }

        var bitmapSize = GetPixelSize();
            
        if (_renderTarget == null || _renderTarget.PixelSize.Width != bitmapSize.Width || _renderTarget.PixelSize.Height != bitmapSize.Height)
            this._renderTarget = new WriteableBitmap(bitmapSize, new Vector(96.0, 96.0), PixelFormat.Bgra8888, AlphaFormat.Premul);

        using (var lockedBitmap = this._renderTarget.Lock())
        {
            MpvContext.SoftwareRender(lockedBitmap.Size.Width, lockedBitmap.Size.Height, lockedBitmap.Address, "bgra");
        }
        context.DrawImage(this._renderTarget, new Rect(0, 0, _renderTarget.PixelSize.Width, _renderTarget.PixelSize.Height));
    }

    private PixelSize GetPixelSize()
    {
        var scaling = VisualRoot!.RenderScaling;
        //return new PixelSize(Math.Max(1, (int)(Bounds.Width * scaling)),Math.Max(1, (int)(Bounds.Height * scaling)));
        return new PixelSize((int)Bounds.Width, (int)Bounds.Height);
    }

    private void UpdateVideoView()
    {
        Dispatcher.UIThread.Post(this.InvalidateVisual, DispatcherPriority.Background);
    }

    protected virtual void Dispose(bool disposing)
    {
        // ReleaseUnmanagedResources();
        if (disposing)
        {
            MpvContext.Dispose();
            _renderTarget?.Dispose();
        }
    }
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
    
    ~SoftwareVideoView()
    {
        Dispose(false);
    }
}
