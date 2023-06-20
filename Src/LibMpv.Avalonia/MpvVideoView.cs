using Avalonia;
using Avalonia.Controls;
using Avalonia.Data;

namespace LibMpv.Avalonia;

public class MpvVideoView : Control
{
    // RendererInstance property
    public static readonly DirectProperty<MpvVideoView, IVideoView?> RendererInstanceProperty = AvaloniaProperty.RegisterDirect<MpvVideoView, IVideoView?>(
        nameof(MpvContext), o => o.RendererInstance, defaultBindingMode: BindingMode.OneWayToSource);
    public IVideoView? RendererInstance
    {
        get => _renderInstance;
        private set => this.SetAndRaise(RendererInstanceProperty, ref _renderInstance, value);
    }
    private IVideoView? _renderInstance;

    // MpvContext property
    public static readonly DirectProperty<MpvVideoView, MpvContext?> MpvContextProperty = AvaloniaProperty.RegisterDirect<MpvVideoView, MpvContext?>(
        nameof(MpvContext), o => o.MpvContext, defaultBindingMode: BindingMode.OneWayToSource);
    public MpvContext? MpvContext => RendererInstance?.MpvContext;
    
    /// <summary>
    /// Defines the Renderer property.
    /// </summary>
    public static readonly DirectProperty<MpvVideoView, VideoRenderer> RendererProperty = AvaloniaProperty.RegisterDirect<MpvVideoView, VideoRenderer>(
        nameof(Renderer), o => o.Renderer, (o, v) => o.Renderer = v);
    private VideoRenderer _renderer = Avalonia.VideoRenderer.Software;
    /// <summary>
    /// Gets or sets the video renderer.
    /// </summary>
    public VideoRenderer Renderer
    {
        get => _renderer;
        set
        {
            if (SetAndRaise(RendererProperty, ref _renderer, value))
            {
                InitRenderer();
            }
        }
    }

    protected override void OnInitialized()
    {
        InitRenderer();
    }

    private void InitRenderer()
    {
        StopRenderer();

        var oldContext = MpvContext;
        RendererInstance = Renderer switch
        {
            VideoRenderer.Software => new SoftwareVideoView(),
            VideoRenderer.OpenGl => new OpenGlVideoView(),
            VideoRenderer.Native => new NativeVideoView(),
            _ => null
        };
        
        if (RendererInstance != null)
        {
            this.VisualChildren.Add((Visual)RendererInstance);
            RaisePropertyChanged(MpvContextProperty, oldContext, MpvContext);
        }
    }

    private void StopRenderer()
    {
        if (RendererInstance != null)
        {
            var oldContext = MpvContext;
            this.VisualChildren.Remove((Visual)RendererInstance);
            RendererInstance?.Dispose();
            RendererInstance = null;
            this.RaisePropertyChanged(MpvContextProperty, oldContext, null);
        }
    }
}
