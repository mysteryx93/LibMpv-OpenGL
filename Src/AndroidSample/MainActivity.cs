using Android.App;
using Android.Content.PM;
using Avalonia.Android;

namespace Sample.LibMpv.Avalonia.Android;

[Activity(Label = "_Android", Theme = "@style/MyTheme.NoActionBar", Icon = "@drawable/icon", LaunchMode = LaunchMode.SingleTop, ConfigurationChanges = ConfigChanges.Orientation | ConfigChanges.ScreenSize)]
public class MainActivity : AvaloniaMainActivity
{
    public MainActivity()
    {
    }
}
