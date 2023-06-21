using System.Diagnostics;
using HanumanInstitute.LibMpv;

namespace Sample.LibMpv.Avalonia.Android.ViewModels;

public class MainViewModel 
{
    public MpvContext Context { get; } = new MpvContext();

    public MainViewModel()
    {
        Context.SetOptionString("vo", "gpu");
        Context.SetOptionString("gpu-debug", "yes");
        Context.SetOptionString("gpu-context", "android");
        Context.SetOptionString("opengl-es", "yes");

        Context.RequestLogMessages("debug");
        Context.LogMessage += Context_LogMessage;
    }

    private void Context_LogMessage(object? sender, MpvLogMessageEventArgs e)
    {
        Debug.WriteLine($"MpvContext: {e.Level} {e.Text}");
    }

    public void Play()
    {
        Context.Command("loadfile", "http://commondatastorage.googleapis.com/gtv-videos-bucket/sample/BigBuckBunny.mp4", "replace");
        Context.SetPropertyFlag("pause", false);
    }

    public void Stop()
    {
        Context.SetPropertyFlag("pause", true);
    }
}
