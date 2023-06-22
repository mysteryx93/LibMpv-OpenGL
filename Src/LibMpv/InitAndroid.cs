using HanumanInstitute.LibMpv.Core;

namespace HanumanInstitute.LibMpv;

#if ANDROID
internal static class InitAndroid
{
    delegate int AvJniSetJavaVmDelegate(nint jvm, nint logCtx);
    private static bool s_isJvmInitialized;
    private static object s_initLock = new();
    
    public static void InitJvm()
    {
        if (!s_isJvmInitialized)
        {
            lock (s_initLock)
            {
                if (!s_isJvmInitialized)
                {
                    MpvApi.LibraryVersionMap.Add("libavcodec", 0);
                    FunctionResolverBase.LibraryDependenciesMap.Add("libavcodec", new string[] { });
        
                    var functionResolver = FunctionResolverFactory.Create();
        
                    var avJniSetJavaVm = functionResolver.GetFunctionDelegate<AvJniSetJavaVmDelegate>("libavcodec", "av_jni_set_java_vm")!;
                    Java.Interop.JniEnvironment.References.GetJavaVM(out nint jvmPointer);
                    avJniSetJavaVm(jvmPointer, IntPtr.Zero);
                    s_isJvmInitialized = true;
                }
            }
        }
    }
}
#endif
