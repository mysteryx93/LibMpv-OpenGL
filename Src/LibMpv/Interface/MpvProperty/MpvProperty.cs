using System.Globalization;
using System.Text.Json;
using HanumanInstitute.LibMpv.Core;

namespace HanumanInstitute.LibMpv;

/// <summary>
/// Represents a read-only MPV property.
/// </summary>
/// <typeparam name="TNull">The nullable return type of the property.</typeparam>
public abstract class MpvProperty<TNull>
{
    protected MpvContext Mpv { get; private set; }
    protected MpvFormat Format { get; private set; }

    public MpvProperty(MpvContext mpv, string name)
    {
        Mpv = mpv;
        PropertyName = name.CheckNotNullOrEmpty(nameof(name));
        var type = typeof(TNull);
        Format = 1 switch
        {
             _ when type == typeof(long?) || type == typeof(int?) => MpvFormat.Int64,
             _ when type == typeof(double?) || type == typeof(float?) => MpvFormat.Double,
             _ when type == typeof(bool?) => MpvFormat.Flag,
             _ when type == typeof(string) => MpvFormat.String,
             _ => MpvFormat.None
        };
    }

    /// <summary>
    /// Gets the API name of the property.
    /// </summary>
    public string PropertyName { get; private set; }

    /// <summary>
    /// Parse value as specified type.
    /// </summary>
    /// <param name="value">The raw value to parse.</param>
    /// <returns>The typed parsed value.</returns>
    /// <exception cref="FormatException">Value is not in a valid format.</exception>
    /// <exception cref="OverflowException">Value represents a number that is out of the range.</exception>
    protected virtual TNull ParseValue(string? value) => ParseDefault(value);


    public static TNull ParseDefault(string? value)
    {
        if (value == null) { return default!; }

        if (typeof(TNull) == typeof(string))
        {
            var str = value.ToString();
            if (str.Length >= 2 && str[0] == '"' && str[str.Length - 1] == '"')
            {
                str = str.Substring(1, str.Length - 2);
            }
            return (TNull)(object)str;
        } 
        if (typeof(TNull).IsValueType)
        {
            var type = Nullable.GetUnderlyingType(typeof(TNull)) ?? typeof(TNull);
            return (TNull)Convert.ChangeType(value, type, CultureInfo.InvariantCulture);
        }
        else
        {
            var jsonOptions = new JsonSerializerOptions()
            {
                PropertyNamingPolicy = new MpvJsonNamingPolicy()
            };
            return (TNull)JsonSerializer.Deserialize<TNull>(value, jsonOptions)! ?? default!;
        }
    }

    /// <summary>
    /// Formats specified value to send into a MPV request.
    /// </summary>
    /// <param name="value">The value to format.</param>
    /// <returns>The formatted value.</returns>
    protected virtual string? FormatValue(TNull value) => value?.ToStringInvariant();
}
