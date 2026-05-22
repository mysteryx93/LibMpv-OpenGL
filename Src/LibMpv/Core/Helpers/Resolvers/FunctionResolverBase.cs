using System.Reflection;

namespace HanumanInstitute.LibMpv.Core;

public abstract class FunctionResolverBase : IFunctionResolver
{
    public static readonly Dictionary<string, string[]> LibraryDependenciesMap =
        new()
        {
            { "libmpv", new string[] { } }
        };

    private readonly Dictionary<string, IntPtr> _loadedLibraries = new();

    private readonly object _syncRoot = new();

    public T? GetFunctionDelegate<T>(string libraryName, string functionName, bool throwOnError = true)
    {
        //var nativeLibraryHandle = GetOrLoadLibrary(libraryName, throwOnError);

        try
        {
            var nativeLibraryHandle = NativeLibrary.Load(GetNativeLibraryName(MpvApi.DllName, MpvApi.LibraryVersionMap.First().Value), Assembly.GetExecutingAssembly(), DllImportSearchPath.AssemblyDirectory);
            return GetFunctionDelegate<T>(nativeLibraryHandle, functionName, throwOnError);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        
    }

    public T? GetFunctionDelegate<T>(IntPtr nativeLibraryHandle, string functionName, bool throwOnError)
    {
        var functionPointer = FindFunctionPointer(nativeLibraryHandle, functionName);

        if (functionPointer == IntPtr.Zero)
        {
            if (throwOnError)
            {
                throw new EntryPointNotFoundException($"Could not find the entrypoint for {functionName}.");
            }
            return default;
        }

#if NETSTANDARD2_0_OR_GREATER || NET10_0
        try
        {
            return Marshal.GetDelegateForFunctionPointer<T>(functionPointer);
        }
        catch (MarshalDirectiveException)
        {
            if (throwOnError)
            {
                throw;
            }
            return default;
        }
#else
        return (T)(object)Marshal.GetDelegateForFunctionPointer(functionPointer, typeof(T));
#endif
    }

    public IntPtr GetOrLoadLibrary(string libraryName, bool throwOnError)
    {
        if (_loadedLibraries.TryGetValue(libraryName, out var ptr)) return ptr;

        lock (_syncRoot)
        {
            if (_loadedLibraries.TryGetValue(libraryName, out ptr)) return ptr;

            if (LibraryDependenciesMap.TryGetValue(libraryName, out var dependencies))
            {
                dependencies.Where(n => !_loadedLibraries.ContainsKey(n) && !n.Equals(libraryName))
                    .ToList()
                    .ForEach(n => GetOrLoadLibrary(n, false));
            }

            var version = MpvApi.LibraryVersionMap[libraryName];
            var nativeLibraryName = GetNativeLibraryName(libraryName, version);
            foreach (var path in GetSearchPaths())
            {
                var libraryPath = Path.Combine(path, nativeLibraryName);
                ptr = LoadNativeLibrary(libraryPath);
                if (ptr != IntPtr.Zero)
                {
                    break;
                }
            }

            if (ptr != IntPtr.Zero)
            {
                _loadedLibraries.Add(libraryName, ptr);
            }
            else if (throwOnError)
            {
                throw new DllNotFoundException(
                    $"Unable to load DLL '{libraryName}.{version} [{nativeLibraryName}] under {MpvApi.RootPath}': The specified module could not be found.");
            }

            return ptr;
        }
    }

    protected abstract string GetNativeLibraryName(string libraryName, int version);
    protected abstract string[] GetSearchPaths();
    protected abstract IntPtr LoadNativeLibrary(string libraryName);
    protected abstract IntPtr FindFunctionPointer(IntPtr nativeLibraryHandle, string functionName);
}
