namespace HanumanInstitute.LibMpv;

/// <summary>
/// Represents a read-only MPV indexed property.
/// </summary>
/// <typeparam name="TIndex">The indexer data type.</typeparam>
/// <typeparam name="T">The return type of the property.</typeparam>
public class MpvPropertyIndexRead<TIndex, T>
    where T : struct
{
    protected readonly MpvContext Mpv;
    protected readonly string PropertyName;

    public MpvPropertyIndexRead(MpvContext mpv, string name)
    {
        Mpv = mpv;
        PropertyName = name.CheckNotNullOrEmpty(nameof(name));
    }

    /// <summary>
    /// Gives access to specified indexed property.
    /// </summary>
    /// <param name="index">The property index to access.</param>
    /// <returns>A property.</returns>
    public MpvPropertyRead<T> this[TIndex index] => new(Mpv, GetPropertyIndexName(index));

    /// <summary>
    /// Returns the property name after replacing {0} with specified index.
    /// </summary>
    /// <param name="index">The index to insert into the property name.</param>
    /// <returns>The indexed property name.</returns>
    public string GetPropertyIndexName(TIndex index) => PropertyName.FormatInvariant(index);
}

/// <summary>
/// Represents a read-only MPV indexed property.
/// </summary>
/// <typeparam name="TIndex">The indexer data type.</typeparam>
/// <typeparam name="T">The return type of the property.</typeparam>
public class MpvPropertyIndexReadRef<TIndex, T>
    where T : class
{
    protected readonly MpvContext Mpv;
    protected readonly string PropertyName;

    public MpvPropertyIndexReadRef(MpvContext mpv, string name)
    {
        Mpv = mpv;
        PropertyName = name.CheckNotNullOrEmpty(nameof(name));
    }

    /// <summary>
    /// Gives access to specified indexed property.
    /// </summary>
    /// <param name="index">The property index to access.</param>
    /// <returns>A property.</returns>
    public MpvPropertyReadRef<T> this[TIndex index] => new(Mpv, GetPropertyIndexName(index));

    /// <summary>
    /// Returns the property name after replacing {0} with specified index.
    /// </summary>
    /// <param name="index">The index to insert into the property name.</param>
    /// <returns>The indexed property name.</returns>
    public string GetPropertyIndexName(TIndex index) => PropertyName.FormatInvariant(index);
}
