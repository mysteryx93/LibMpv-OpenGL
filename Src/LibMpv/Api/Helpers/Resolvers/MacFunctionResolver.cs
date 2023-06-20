// ReSharper disable IdentifierTypo
// ReSharper disable InconsistentNaming
namespace LibMpv.Api;

public class MacFunctionResolver : FunctionResolverBase
{
    private const string Libdl = "libdl";
    private const int RTLD_NOW = 0x002;

    protected override string GetNativeLibraryName(string libraryName, int version) => $"{libraryName}.{version}.dylib";
    protected override string[] GetSearchPaths() => new string[] { Mpv.RootPath };
    protected override IntPtr LoadNativeLibrary(string libraryName) => dlopen(libraryName, RTLD_NOW);
    protected override IntPtr FindFunctionPointer(IntPtr nativeLibraryHandle, string functionName) => dlsym(nativeLibraryHandle, functionName);

    [DllImport(Libdl)]
    public static extern IntPtr dlsym(IntPtr handle, string symbol);

    [DllImport(Libdl)]
    public static extern IntPtr dlopen(string fileName, int flag);
}
