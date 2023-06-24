using System;

namespace HanumanInstitute.LibMpv.Tests;

public enum LogLevel { None, Debug, Info }

public interface IFormatter<T>
{
    T Parse(string? value);
    string? Format(T value);
}

public class PropertyBase<TNull> : IFormatter<TNull>
{
    public virtual TNull Parse(string? value) => default!;
    public virtual string? Format(TNull value) => null;
}

public class PropertyRead<T> : PropertyBase<T?>
    where T : struct
{ }

public class PropertyReadRef<T> : PropertyBase<T?>
    where T : class
{ }

public class PropertyReadWrite<T> : PropertyRead<T>
    where T : struct
{ }

public class PropertyReadWriteRef<T> : PropertyReadRef<T>
    where T : class
{ }

public class PropertyEnum<T> : PropertyRead<T>
    where T : struct, Enum
{ }

public class MyClass
{
    public void DoIt()
    {
        var f = new PropertyEnum<LogLevel>();
        var value = f.Parse(null); // non-nullable!
    }
}
