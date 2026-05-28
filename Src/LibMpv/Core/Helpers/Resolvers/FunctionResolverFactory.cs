using System.Reflection;

namespace HanumanInstitute.LibMpv.Core;

public static class FunctionResolverFactory
{
    public static PlatformID GetPlatformId()
    {
        return Environment.OSVersion.Platform;
    }

    public static IFunctionResolver Create()
    {
        switch (GetPlatformId())
        {
            //case PlatformID.MacOSX:
            //    return new MacFunctionResolver();
            case PlatformID.Unix:
                {
                    //OSX Is returned as "Unix" now
                    if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                    {
                        
                        return new MacFunctionResolver();
                    }
                    else
                    {
                        var isAndroid = RuntimeInformation.IsOSPlatform(OSPlatform.Create("ANDROID"));
                        return isAndroid ? new AndroidFunctionResolver() : new LinuxFunctionResolver();
                    }
                }
            case PlatformID.Win32NT:
                return new WindowsFunctionResolver();
            default:
                throw new PlatformNotSupportedException();
        }
    }
}
