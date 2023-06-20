namespace LibMpv.Api;

public partial class Mpv
{
    /// <summary>
    ///     Gets or sets the root path for loading libraries.
    /// </summary>
    /// <value>The root path.</value>
    public static string RootPath { get; set; } = AppDomain.CurrentDomain.BaseDirectory;
    
    /// <summary>
    /// Gets or sets the name of the DLL to load.
    /// </summary>
    public static string DllName { get; set; } = "libmpv";
}
