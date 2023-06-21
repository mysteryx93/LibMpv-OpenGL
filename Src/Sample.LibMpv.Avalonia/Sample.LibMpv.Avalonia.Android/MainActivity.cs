using System;
using Android.App;
using Android.Content.PM;
using Avalonia.Android;
using HanumanInstitute.LibMpv.Api;

namespace Sample.LibMpv.Avalonia.Android;

[Activity(Label = "AndroidSample", Theme = "@style/MyTheme.NoActionBar", Icon = "@drawable/icon", LaunchMode = LaunchMode.SingleTop, ConfigurationChanges = ConfigChanges.Orientation | ConfigChanges.ScreenSize)]
public class MainActivity : AvaloniaMainActivity
{
}
