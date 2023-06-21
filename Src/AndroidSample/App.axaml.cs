using System;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using HanumanInstitute.LibMpv.Api;
using Sample.LibMpv.Avalonia.Android.ViewModels;
using Sample.LibMpv.Avalonia.Android.Views;

namespace Sample.LibMpv.Avalonia.Android;

public partial class App : Application
{
    public override void Initialize()
    {
        //let the system determine where libmpv is
        // Mpv.RootPath = "";

        //Step on Android - setup JVM for MPV
        // InitJVM();
        
        AvaloniaXamlLoader.Load(this);
    }

    // delegate int av_jni_set_java_vm_delegate(nint jvm, nint logCtx);
    // private void InitJVM()
    // {
    //     Mpv.LibraryVersionMap.Add("libavcodec", 0);
    //     FunctionResolverBase.LibraryDependenciesMap.Add("libavcodec", new string[]{ });
    //     
    //     var functionResolver = FunctionResolverFactory.Create();
    //
    //     var av_jni_set_java_vm = functionResolver.GetFunctionDelegate<av_jni_set_java_vm_delegate>("libavcodec", "av_jni_set_java_vm");
    //     Java.Interop.JniEnvironment.References.GetJavaVM(out nint jvmPointer);
    //     av_jni_set_java_vm(jvmPointer, IntPtr.Zero);
    // }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is ISingleViewApplicationLifetime singleViewPlatform)
        {
            singleViewPlatform.MainView = new MainView
            {
                DataContext = new MainViewModel()
            };
        }
        base.OnFrameworkInitializationCompleted();
    }
}
