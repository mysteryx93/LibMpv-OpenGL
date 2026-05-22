using HanumanInstitute.LibMpv.Core;

namespace HanumanInstitute.LibMpv;

public static unsafe class MpvFormatter
{
    /// <summary>Read the value of the given property.</summary>
    /// <param name="ctx">Mpv Handle</param>
    /// <param name="name">The property name.</param>
    /// <typeparam name="T">The data type of the property to read.</typeparam>
    public static T GetProperty<T>(MpvHandle* ctx, string name)
    {
        var format = GetMpvFormat<T>();
        switch (format)
        {
            case MpvFormat.Int64:
                var vLong = 0L;
                MpvApi.GetProperty(ctx, name, MpvFormat.Int64, &vLong).CheckCode();
                return (T)(object)vLong;
            case MpvFormat.Double:
                var vDouble = 0.0;
                MpvApi.GetProperty(ctx, name, MpvFormat.Double, &vDouble).CheckCode();
                return (T)(object)vDouble;
            case MpvFormat.Flag:
                var vBool = 0;
                MpvApi.GetProperty(ctx, name, MpvFormat.Flag, &vBool).CheckCode();
                return (T)(object)(vBool == 1);
            case MpvFormat.String:
            case MpvFormat.OsdString:
                var value = MpvApi.GetPropertyString(ctx, name);
                return (T)(object)(value != null ? Utf8Marshaler.FromNative(value, Encoding.UTF8) : null)!;
            case MpvFormat.None:
                break;
            case MpvFormat.Node:
                break;
            case MpvFormat.NodeArray:
                break;
            case MpvFormat.NodeMap:
                break;
            case MpvFormat.ByteArray:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
        return default!;
    }

    public static MpvFormat GetMpvFormat<T>()
    {
        var type = Nullable.GetUnderlyingType(typeof(T)) ?? typeof(T);
        return type switch
        {
            _ when type == typeof(long) || type == typeof(int) => MpvFormat.Int64,
            _ when type == typeof(double) || type == typeof(float) => MpvFormat.Double,
            _ when type == typeof(bool) => MpvFormat.Flag,
            _ when type == typeof(string) => MpvFormat.String,
            _ => MpvFormat.None
        };
    }
    
    public static void SetProperty<T>(MpvHandle* ctx, string name, T value)
    {
        var format = GetMpvFormat<T>();
        switch (format)
        {
            case MpvFormat.Int64:
                var vLong = Convert.ToInt64(value);
                MpvApi.SetProperty(ctx, name, MpvFormat.Int64, &vLong).CheckCode();
                break;
            case MpvFormat.Double:
                var vDouble = Convert.ToDouble(value);
                MpvApi.SetProperty(ctx, name, MpvFormat.Double, &vDouble).CheckCode();
                break;
            case MpvFormat.Flag:
                var vBool = value as bool? == true ? 1 : 0;
                MpvApi.SetProperty(ctx, name, MpvFormat.Flag, &vBool).CheckCode();
                break;
            case MpvFormat.String:
            case MpvFormat.OsdString:
                MpvApi.SetPropertyString(ctx, name, value.ToStringInvariant()).CheckCode();
                break;
            case MpvFormat.None:
                break;
            case MpvFormat.Node:
                break;
            case MpvFormat.NodeArray:
                break;
            case MpvFormat.NodeMap:
                break;
            case MpvFormat.ByteArray:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    public static void SetPropertyAsync<T>(MpvHandle* ctx, string name, T value, ulong requestId)
    {
        var type = Nullable.GetUnderlyingType(typeof(T)) ?? typeof(T);
        switch (type)
        {
            case not null when type == typeof(long) || type == typeof(int):
                var vLong = Convert.ToInt64(value);
                MpvApi.SetPropertyAsync(ctx, requestId, name, MpvFormat.Int64, &vLong).CheckCode();
                break;
            case not null when type == typeof(double) || type == typeof(float):
                var vDouble = Convert.ToDouble(value);
                MpvApi.SetPropertyAsync(ctx, requestId, name, MpvFormat.Double, &vDouble).CheckCode();
                break;
            case not null when type == typeof(bool):
                var vBool = value as bool? == true ? 1 : 0;
                MpvApi.SetPropertyAsync(ctx, requestId, name, MpvFormat.Flag, &vBool).CheckCode();
                break;
            case not null when type == typeof(string):
                var vString = Utf8Marshaler.FromManaged(Encoding.UTF8, value.ToStringInvariant());
                MpvApi.SetPropertyAsync(ctx, requestId, name, MpvFormat.String, &vString).CheckCode();
                break;
        }
    }
    
    // ReSharper disable once ReturnTypeCanBeNotNullable
    public static T? Parse<T>(this MpvNode node)
    {
        var result = node.Format switch
        {
            MpvFormat.Double => (object)node.U.Double,
            MpvFormat.Flag => node.U.Flag,
            MpvFormat.Int64 => node.U.Int64,
            MpvFormat.String => Utf8Marshaler.FromNative(node.U.String, Encoding.UTF8),
            MpvFormat.OsdString => Utf8Marshaler.FromNative(node.U.String, Encoding.UTF8),
            _ => null
        };
        return (T)result!;
    }

    /// <summary>
    /// Copies and parses native event property data immediately while the mpv event buffer is still valid.
    /// Must be called within the event handler before the next mpv_wait_event call.
    /// </summary>
    public static object? ParseDataEagerly(MpvFormat format, IntPtr data)
    {
        if (data == IntPtr.Zero) return null;
        return format switch
        {
            MpvFormat.Int64 => *(long*)data,
            MpvFormat.Flag => *(int*)data == 1,
            MpvFormat.Double => *(double*)data,
            // data is char** — dereference once to get the char*, then marshal to managed string
            MpvFormat.String => Utf8Marshaler.FromNative(Marshal.ReadIntPtr(data)),
            _ => null
        };
    }

    public static T? ParseData<T>(object? data)
    {
        if (data is null) return default;
        if (data is T direct) return direct;
        var type = Nullable.GetUnderlyingType(typeof(T)) ?? typeof(T);
        return (T?)Convert.ChangeType(data, type);
    }
}
