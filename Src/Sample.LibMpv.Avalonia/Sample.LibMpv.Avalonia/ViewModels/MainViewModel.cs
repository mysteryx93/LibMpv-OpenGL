using HanumanInstitute.LibMpv;
using HanumanInstitute.LibMpv.Avalonia;
using HanumanInstitute.LibMpv.Core;
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

    public async void Play()
    {
        Stop();
        await Mpv.LoadFile(MediaUrl).InvokeAsync();
        // await Mpv.CommandAsync(null, "loadfile", MediaUrl, "replace");
    }

    public void Pause() => Pause(null);

    public void Pause(bool? value)
    {
        value ??= !Mpv.Pause.Get()!;
        Mpv.Pause.Set(value.Value);
    }

    public void Stop() => Mpv.Stop().Invoke();

    public void Software() => Renderer = VideoRenderer.Software;
    public void OpenGl() => Renderer = VideoRenderer.OpenGl;
    public void Native() => Renderer = VideoRenderer.Native;
}
