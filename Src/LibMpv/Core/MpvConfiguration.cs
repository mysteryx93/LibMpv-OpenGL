namespace HanumanInstitute.LibMpv.Core;

public class MpvConfiguration
{
    private string? _rootPath;

    public string RootPath
    {
        get
        {
#if ANDROID
            return _rootPath ??=
                Application.Context?.ApplicationInfo?.NativeLibraryDir ?? 
                throw new InvalidOperationException("Android application context is not initialized");
#else
            return _rootPath ??=
                AppContext.BaseDirectory;
#endif
        }
        set => _rootPath = value;
    }
    
    
    public string DllName { get; set; } = "libmpv";
    
    public static Dictionary<string, int> LibraryVersionMap = new()
    {
        {"libmpv", 2}
    };
}
