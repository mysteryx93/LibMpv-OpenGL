// ReSharper disable IdentifierTypo
// ReSharper disable InconsistentNaming
namespace HanumanInstitute.LibMpv.Core;

public class MacFunctionResolver : FunctionResolverBase
{
    private const string Libdl = "libSystem";
    private const int RTLD_NOW = 0x002;

    protected override string GetNativeLibraryName(string libraryName, int version) => $"{libraryName}.{version}.dylib";
    protected override string[] GetSearchPaths() => new[] { "/usr/local/lib", "/opt/homebrew/lib", "/usr/lib", MpvApi.RootPath };
    protected override IntPtr LoadNativeLibrary(string libraryName) => dlopen(libraryName, RTLD_NOW);
    protected override IntPtr FindFunctionPointer(IntPtr nativeLibraryHandle, string functionName) => dlsym(nativeLibraryHandle, functionName);

    [DllImport("libSystem")]
    public static extern IntPtr dlsym(IntPtr handle, string symbol);

    [DllImport("libSystem")]
    public static extern IntPtr dlopen(string fileName, int flag);
}
