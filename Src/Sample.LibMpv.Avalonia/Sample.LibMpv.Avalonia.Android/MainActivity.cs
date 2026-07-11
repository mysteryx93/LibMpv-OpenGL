using Android.App;
using Android.Content.PM;
using Avalonia;
using Avalonia.Android;
using HanumanInstitute.LibMpv.Core;

namespace Sample.LibMpv.Avalonia.Android;

[Activity(
    Label = "Sample.LibMpv.Avalonia.Android",
    Theme = "@style/MyTheme.NoActionBar",
    Icon = "@drawable/icon",
    MainLauncher = true,
    ConfigurationChanges = ConfigChanges.Orientation | ConfigChanges.ScreenSize | ConfigChanges.UiMode)]
public class MainActivity : AvaloniaMainActivity
{
    public MainActivity()
    {
        
    }
}
