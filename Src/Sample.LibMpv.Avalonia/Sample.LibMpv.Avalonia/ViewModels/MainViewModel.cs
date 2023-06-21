using HanumanInstitute.LibMpv;
using HanumanInstitute.LibMpv.Avalonia;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace Sample.LibMpv.Avalonia.ViewModels;

public class MainViewModel : ReactiveObject
{
    public MpvContext Mpv { get; set; } = default!;
    
    [Reactive]
    public VideoRenderer Renderer { get; set; }

    string _mediaUrl = "http://commondatastorage.googleapis.com/gtv-videos-bucket/sample/BigBuckBunny.mp4";
    public string MediaUrl
    {
        get => _mediaUrl;
        set { this.RaiseAndSetIfChanged(ref _mediaUrl, value); }
    }

    public void Play()
    {
        // Stop();
        Mpv.Command("loadfile", MediaUrl, "replace");
        // Mpv.SetPropertyFlag("pause", false);
    }

    public void Pause() => Pause(null);

    public void Pause(bool? value)
    {
        value ??= !Mpv.GetPropertyFlag("pause");
        Mpv.SetPropertyFlag("pause", value.Value);
    }

    public void Stop() => Mpv.Command("stop");

    public void Software() => Renderer = VideoRenderer.Software;
    public void OpenGl() => Renderer = VideoRenderer.OpenGl;
    public void Native() => Renderer = VideoRenderer.Native;
}
