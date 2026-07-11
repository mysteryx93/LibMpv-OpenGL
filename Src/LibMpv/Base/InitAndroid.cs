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
                    MpvApi.RootPath = Application.Context?.ApplicationInfo?.NativeLibraryDir
                        ?? throw new InvalidOperationException("Android application context is not available");
                    
                    // Pre-load all native libs in dependency order with RTLD_GLOBAL so the
                    // dynamic linker can resolve transitive deps between app-private .so files.
                    const int RtldNowGlobal = 0x102; // RTLD_NOW | RTLD_GLOBAL
                    foreach (var lib in new[] { "libc++_shared", "libavutil", "libswresample", "libswscale", "libavcodec", "libavformat", "libavfilter", "libmpv" })
                    {
                        var path = Path.Combine(MpvApi.RootPath, lib + ".so");
                        
                        if (!File.Exists(path)) 
                            throw new FileNotFoundException(
                                $"Required native library '{lib}.so' was not found.", path);
                        
                        AndroidFunctionResolver.dlopen(path, RtldNowGlobal);
                    }

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
