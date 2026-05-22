using HanumanInstitute.LibMpv;
using HanumanInstitute.LibMpv.Avalonia;
using ReactiveUI;
using ReactiveUI.SourceGenerators;

namespace Sample.LibMpv.Avalonia.ViewModels;

public partial class MainViewModel : ReactiveObject
{
    public MpvContext? Mpv { get; set; }

    [Reactive]
    public partial VideoRenderer Renderer { get; set; }

    public VideoRenderer[] RendererOptions { get; } = Enum.GetValues<VideoRenderer>();

    string _mediaUrl = "https://download.blender.org/peach/bigbuckbunny_movies/BigBuckBunny_320x180.mp4";
    public string MediaUrl
    {
        get => _mediaUrl;
        set { this.RaiseAndSetIfChanged(ref _mediaUrl, value); }
    }

    public async void Play()
    {
        try
        {
            Stop();

            if (Mpv != null)
            {
                await Mpv.LoadFile(MediaUrl).InvokeAsync();
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Play failed: + {ex.Message}");
        }
    }

    public void Pause() => Pause(null);

    public void Pause(bool? value)
    {
        if (Mpv != null)
        {
            value ??= !Mpv.Pause.Get()!;
            Mpv.Pause.Set(value.Value);
        }
    }

    public void Stop()
    {
        if (Mpv != null)
        {
            Mpv.Stop().Invoke();
            Mpv.Pause.Set(false);
        }
    }

    public void Software() => Renderer = VideoRenderer.Software;
    public void OpenGl() => Renderer = VideoRenderer.OpenGl;
    public void Native() => Renderer = VideoRenderer.Native;
}
