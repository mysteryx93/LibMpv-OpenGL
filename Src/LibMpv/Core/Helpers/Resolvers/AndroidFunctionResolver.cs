// ReSharper disable IdentifierTypo
// ReSharper disable InconsistentNaming

namespace HanumanInstitute.LibMpv.Core;

public class AndroidFunctionResolver : FunctionResolverBase
{
    private const string Libdl = "libdl.so";
    private const int RTLD_NOW = 0x002;

    protected override string GetNativeLibraryName(string libraryName, int version) =>
        version > 0 ? $"{libraryName}.so.{version}" : $"{libraryName}.so";
    protected override string[] GetSearchPaths() => new string[] { MpvApi.RootPath };
    protected override IntPtr LoadNativeLibrary(string libraryName)
    {
        var handle = dlopen(libraryName, RTLD_NOW);

        if (handle == IntPtr.Zero)
        {
            var err = Marshal.PtrToStringAnsi(dlerror());

#if ANDROID
            Android.Util.Log.Error("HanumanInstitute.LibMpv", $"dlopen({libraryName}) failed");
            Android.Util.Log.Error("HanumanInstitute.LibMpv", err ?? "");
#endif
        }

        return handle;
    }
    protected override IntPtr FindFunctionPointer(IntPtr nativeLibraryHandle, string functionName) => dlsym(nativeLibraryHandle, functionName);

    [DllImport(Libdl)]
    public static extern IntPtr dlsym(IntPtr handle, string symbol);

    [DllImport(Libdl)]
    public static extern IntPtr dlopen(string fileName, int flag);
    
    [DllImport(Libdl)]
    public static extern IntPtr dlerror();
}
