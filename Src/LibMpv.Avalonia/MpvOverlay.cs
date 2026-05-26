using Avalonia;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using Avalonia.Threading;
using System.Runtime.InteropServices;

namespace HanumanInstitute.LibMpv.Avalonia;

/// <summary>
/// Composites a bitmap overlay onto mpv's OSD pipeline using overlay-add.
/// Works identically across NativeView, OpenGlView, and SoftwareView.
/// </summary>
public sealed class MpvOverlay : IDisposable
{
    private readonly MpvContext _context;
    private readonly int _id;

    private byte[]? _buffer;
    private GCHandle _gcHandle;
    private int _bufferWidth;
    private int _bufferHeight;

    private bool _active;
    private bool _disposed;

    /// <param name="context">The MpvContext to attach the overlay to.</param>
    /// <param name="id">Overlay slot 0-63. Each active overlay needs a unique id.</param>
    public MpvOverlay(MpvContext context, int id = 0)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _id = id;
    }

    /// <summary>
    /// Fills the overlay with a solid color. Thread-safe — can be called from any thread.
    /// </summary>
    public void Show(int x, int y, int width, int height, Color color)
    {
        ObjectDisposedException.ThrowIf(_disposed, this);
        if (width < 1 || height < 1) throw new ArgumentOutOfRangeException(nameof(width), "Width and height must be at least 1.");

        EnsureBuffer(width, height);

        // Premultiplied BGRA
        var a = color.A;
        var r = (byte)(color.R * a / 255);
        var g = (byte)(color.G * a / 255);
        var b = (byte)(color.B * a / 255);
        var buf = _buffer!;
        for (var i = 0; i < buf.Length; i += 4)
        {
            buf[i]     = b;
            buf[i + 1] = g;
            buf[i + 2] = r;
            buf[i + 3] = a;
        }

        SendOverlay(x, y, width, height);
    }

    /// <summary>
    /// Renders the overlay using an Avalonia DrawingContext.
    /// Must be called from the UI thread, or will be dispatched automatically.
    /// </summary>
    public void Show(int x, int y, int width, int height, Action<DrawingContext> draw)
    {
        ObjectDisposedException.ThrowIf(_disposed, this);
        if (width < 1 || height < 1) throw new ArgumentOutOfRangeException(nameof(width), "Width and height must be at least 1.");

        if (Dispatcher.UIThread.CheckAccess())
        {
            ShowOnUIThread(x, y, width, height, draw);
        }
        else
        {
            Dispatcher.UIThread.Post(() =>
            {
                if (!_disposed) ShowOnUIThread(x, y, width, height, draw);
            }, DispatcherPriority.Background);
        }
    }

    private void ShowOnUIThread(int x, int y, int width, int height, Action<DrawingContext> draw)
    {
        EnsureBuffer(width, height);

        using var rtb = new RenderTargetBitmap(new PixelSize(width, height), new Vector(96, 96));
        using (var ctx = rtb.CreateDrawingContext())
            draw(ctx);

        var ptr = _gcHandle.AddrOfPinnedObject();
        rtb.CopyPixels(new PixelRect(0, 0, width, height), ptr, _buffer!.Length, width * 4);

        SendOverlay(x, y, width, height);
    }

    /// <summary>Removes the overlay from mpv's OSD pipeline.</summary>
    public void Hide()
    {
        if (_active && !_disposed)
        {
            _context.ImageOverlayRemove(_id).Invoke();
            _active = false;
        }
    }

    private void SendOverlay(int x, int y, int width, int height)
    {
        var ptr = _gcHandle.AddrOfPinnedObject();
        _context.ImageOverlayAdd(_id, x, y, ptr, width, height, width * 4).Invoke();
        _active = true;
    }

    private void EnsureBuffer(int width, int height)
    {
        if (_buffer != null && _bufferWidth == width && _bufferHeight == height)
            return;

        FreeBuffer();

        _buffer = new byte[width * height * 4];
        _gcHandle = GCHandle.Alloc(_buffer, GCHandleType.Pinned);
        _bufferWidth = width;
        _bufferHeight = height;
    }

    private void FreeBuffer()
    {
        if (_gcHandle.IsAllocated)
            _gcHandle.Free();
        _buffer = null;
        _bufferWidth = 0;
        _bufferHeight = 0;
    }

    public void Dispose()
    {
        if (_disposed) return;
        Hide();
        _disposed = true;
        FreeBuffer();
    }
}
