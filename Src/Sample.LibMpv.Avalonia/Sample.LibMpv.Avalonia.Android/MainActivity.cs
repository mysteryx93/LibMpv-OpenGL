using System;
using Android.App;
using Android.Content.PM;
using Avalonia.Android;
using LibMpv.Api;

namespace Sample.LibMpv.Avalonia.Android;

[Activity(Label = "AndroidSample", Theme = "@style/MyTheme.NoActionBar", Icon = "@drawable/icon", LaunchMode = LaunchMode.SingleTop, ConfigurationChanges = ConfigChanges.Orientation | ConfigChanges.ScreenSize)]
public class MainActivity : AvaloniaMainActivity
{
    public MainActivity() => InitJvm();
    
    delegate int AvJniSetJavaVmDelegate(nint jvm, nint logCtx);
    private void InitJvm()
    {
        Mpv.LibraryVersionMap.Add("libavcodec", 0);
        FunctionResolverBase.LibraryDependenciesMap.Add("libavcodec", new string[]{ });
        
        var functionResolver = FunctionResolverFactory.Create();

        var avJniSetJavaVm = functionResolver.GetFunctionDelegate<AvJniSetJavaVmDelegate>("libavcodec", "av_jni_set_java_vm");
        Java.Interop.JniEnvironment.References.GetJavaVM(out nint jvmPointer);
        avJniSetJavaVm(jvmPointer, IntPtr.Zero);
    }
}
