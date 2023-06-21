using Avalonia;
using Avalonia.Controls;
using Avalonia.Data;

namespace HanumanInstitute.LibMpv.Avalonia;

public class MpvView : Control
{
    // RendererInstance property
    public static readonly DirectProperty<MpvView, IVideoView?> RendererInstanceProperty = AvaloniaProperty.RegisterDirect<MpvView, IVideoView?>(
        nameof(MpvContext), o => o.RendererInstance, defaultBindingMode: BindingMode.OneWayToSource);
    public IVideoView? RendererInstance
    {
        get => _renderInstance;
        private set => this.SetAndRaise(RendererInstanceProperty, ref _renderInstance, value);
    }
    private IVideoView? _renderInstance;

    // MpvContext property
    public static readonly DirectProperty<MpvView, MpvContext?> MpvContextProperty = AvaloniaProperty.RegisterDirect<MpvView, MpvContext?>(
        nameof(MpvContext), o => o.MpvContext, defaultBindingMode: BindingMode.OneWayToSource);
    public MpvContext? MpvContext => RendererInstance?.MpvContext;
    
    /// <summary>
    /// Defines the Renderer property.
    /// </summary>
    public static readonly DirectProperty<MpvView, VideoRenderer> RendererProperty = AvaloniaProperty.RegisterDirect<MpvView, VideoRenderer>(
        nameof(Renderer), o => o.Renderer, (o, v) => o.Renderer = v);
    private VideoRenderer _renderer = Avalonia.VideoRenderer.Auto;
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
            VideoRenderer.Software => new SoftwareView(),
            VideoRenderer.OpenGl => new OpenGlView(),
            VideoRenderer.Native => new NativeView(),
            _ => SelectAuto()
        };
        
        if (RendererInstance != null)
        {
            this.VisualChildren.Add((Visual)RendererInstance);
            RaisePropertyChanged(MpvContextProperty, oldContext, MpvContext);
        }
    }

    private IVideoView SelectAuto()
    {
#if ANDROID
        return new NativeView();
#endif
        return new OpenGlView();
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
