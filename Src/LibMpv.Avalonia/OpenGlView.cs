using System;
using System.Runtime.InteropServices;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Data;
using Avalonia.OpenGL;
using Avalonia.OpenGL.Controls;
using Avalonia.Threading;

namespace HanumanInstitute.LibMpv.Avalonia;

public class OpenGlView : OpenGlControlBase, IVideoView
{
    private sealed class OpenGlMpvContext : MpvContext
    {
        protected override void OnPreInitialize()
        {
            SetOptionString("vo", "libmpv");
            // Linux uses native OpenGL (GLX/EGL), so zero-copy VAAPI/VDPAU interop is
            // available. Windows and macOS use ANGLE/Metal which lack the GL<->GPU interop
            // extensions hwdec drivers need, so copy-back is the only option there.
            SetOptionString("hwdec", RuntimeInformation.IsOSPlatform(OSPlatform.Linux)
                ? "auto"
                : "auto-copy");
        }
    }

    // GLX: retrieve the X11 Display* from the current GL context (X11+GLX only).
    [DllImport("libGL", EntryPoint = "glXGetCurrentDisplay")]
    private static extern nint GlXGetCurrentDisplay();

    // Wayland: open/close a display connection for mpv's VAAPI device setup.
    [DllImport("libwayland-client", EntryPoint = "wl_display_connect")]
    private static extern nint WlDisplayConnect(string? name);

    [DllImport("libwayland-client", EntryPoint = "wl_display_disconnect")]
    private static extern void WlDisplayDisconnect(nint display);

    delegate IntPtr GetProcAddress(string proc);

    private GetProcAddress? _getProcAddress;
    private nint _waylandDisplay;
    private volatile bool _isIdle;

    // MpvContext property
    public static readonly DirectProperty<OpenGlView, MpvContext> MpvContextProperty = AvaloniaProperty.RegisterDirect<OpenGlView, MpvContext>(
        nameof(MpvContext), o => o.MpvContext, defaultBindingMode: BindingMode.OneWayToSource);
    public MpvContext MpvContext { get; } = new OpenGlMpvContext();

    protected override void OnAttachedToVisualTree(VisualTreeAttachmentEventArgs e)
    {
        base.OnAttachedToVisualTree(e);
        RequestNextFrameRendering();
    }

    protected override void OnOpenGlRender(GlInterface gl, int fbo)
    {
        if (_isIdle)
        {
            gl.ClearColor(0, 0, 0, 1);
            gl.Clear(0x4000); // GL_COLOR_BUFFER_BIT
            return;
        }
        if (MpvContext.IsCustomRendering())
        {
            var size = GetPixelSize();
            MpvContext.OpenGlRender(size.Width, size.Height, fbo, 1);
        }
    }

    protected override void OnOpenGlInit(GlInterface gl)
    {
        if (_getProcAddress != null) { return; }

        _getProcAddress = gl.GetProcAddress;
        MpvContext.Idle += OnMpvIdle;
        MpvContext.StartFile += OnMpvStartFile;
        MpvContext.StopRendering();

        nint x11Display = 0;
        nint waylandDisplay = 0;

        if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
        {
            if (string.IsNullOrEmpty(Environment.GetEnvironmentVariable("WAYLAND_DISPLAY")))
            {
                // X11+GLX: the Display* is retrievable directly from the current GL context.
                try { x11Display = GlXGetCurrentDisplay(); } catch { }
            }
            else
            {
                // Wayland+EGL: open a dedicated connection; mpv uses it to create the
                // VAAPI wl_display device. Kept alive until OnOpenGlDeinit.
                try
                {
                    _waylandDisplay = WlDisplayConnect(null);
                    waylandDisplay = _waylandDisplay;
                }
                catch { }
            }
        }

        MpvContext.StartOpenGlRendering((name) => _getProcAddress(name), UpdateVideoView,
            x11Display, waylandDisplay);
    }

    protected override void OnOpenGlDeinit(GlInterface gl)
    {
        MpvContext.Idle -= OnMpvIdle;
        MpvContext.StartFile -= OnMpvStartFile;
        MpvContext.StopRendering();
        _getProcAddress = null;

        if (_waylandDisplay != 0)
        {
            try { WlDisplayDisconnect(_waylandDisplay); } catch { }
            _waylandDisplay = 0;
        }
    }

    private void OnMpvIdle(object? sender, EventArgs e)
    {
        _isIdle = true;
        UpdateVideoView();
    }

    private void OnMpvStartFile(object? sender, MpvStartFileEventArgs e) => _isIdle = false;

    private PixelSize GetPixelSize()
    {
        var scaling = TopLevel.GetTopLevel(this)?.RenderScaling ?? 1.0;
        return new PixelSize(Math.Max(1, (int)(Bounds.Width * scaling)), Math.Max(1, (int)(Bounds.Height * scaling)));
    }

    private void UpdateVideoView()
    {
        this.Dispatcher.InvokeAsync(this.RequestNextFrameRendering, DispatcherPriority.Background);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            MpvContext?.Dispose();
        }
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}
