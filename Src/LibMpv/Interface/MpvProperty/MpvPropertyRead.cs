namespace HanumanInstitute.LibMpv;

/// <summary>
/// Represents a read-only MPV property.
/// </summary>
/// <typeparam name="T">The return type of the property.</typeparam>
public class MpvPropertyRead<T> : MpvPropertyRead<T, T>
    where T : struct
{
    public MpvPropertyRead(MpvContext mpv, string name) : base(mpv, name)
    {
    }
}

/// <summary>
/// Represents a read-only MPV property.
/// </summary>
/// <typeparam name="T">The return type of the property.</typeparam>
/// <typeparam name="TRaw">The raw data type to be parsed from MPV. Usually the same.</typeparam>
public class MpvPropertyRead<T, TRaw> : MpvProperty<T?, TRaw>
    where T : struct
{
    public MpvPropertyRead(MpvContext mpv, string name) : base(mpv, name)
    {
    }

    /// <summary>
    /// Watches a property for changes. If the given property is changed, then an event 'property-change' will be generated.
    /// </summary>
    public void Observe(ulong observeId) =>
        Mpv.ObserveProperty(observeId, PropertyName, Format);

    /// <summary>
    /// Undo ObserveProperty or ObservePropertyString. This requires the numeric id passed to the observed command as argument.
    /// </summary>
    /// <param name="observeId">The ID of the observer.</param>
    public void UnobservePropertyAsync(ulong observeId) =>
        Mpv.UnobserveProperty(observeId);

    /// <summary>
    /// Returns the value of the given property. The value will be sent in the data field of the replay message.
    /// </summary>
    public T? Get() => ParseValue(Mpv.GetProperty<TRaw>(PropertyName));
    
    /// <summary>
    /// Returns the value of the given property. The value will be sent in the data field of the replay message.
    /// </summary>
    public async Task<T?> GetAsync(MpvAsyncOptions? options = null)
    {
        var result = await Mpv.GetPropertyAsync<TRaw>(PropertyName, options);
        return ParseValue(result);
    }
}

/// <summary>
/// Represents a read-only MPV property.
/// </summary>
/// <typeparam name="T">The return type of the property.</typeparam>
public class MpvPropertyReadRef<T> : MpvPropertyReadRef<T, T>
    where T : class
{
    public MpvPropertyReadRef(MpvContext mpv, string name) : base(mpv, name)
    {
    }
}

/// <summary>
/// Represents a read-only MPV property.
/// </summary>
/// <typeparam name="T">The return type of the property.</typeparam>
/// <typeparam name="TRaw">The raw data type to be parsed from MPV. Usually the same.</typeparam>
public class MpvPropertyReadRef<T, TRaw> : MpvProperty<T?, TRaw>
    where T : class
{
    public MpvPropertyReadRef(MpvContext mpv, string name) : base(mpv, name)
    {
    }

    /// <summary>
    /// Watches a property for changes. If the given property is changed, then an event 'property-change' will be generated.
    /// </summary>
    /// <param name="observeId">An ID that will be passed to the generated events as parameter 'id'.</param>
    public void Observe(ulong observeId) =>
        Mpv.ObserveProperty(observeId, PropertyName, Format);

    /// <summary>
    /// Undo ObserveProperty or ObservePropertyString. This requires the numeric id passed to the observed command as argument.
    /// </summary>
    /// <param name="observeId">The ID of the observer.</param>
    public void UnobservePropertyAsync(ulong observeId) =>
        Mpv.UnobserveProperty(observeId);

    /// <summary>
    /// Returns the value of the given property. The value will be sent in the data field of the replay message.
    /// </summary>
    public virtual T? Get() => ParseValue(Mpv.GetProperty<TRaw>(PropertyName));

    /// <summary>
    /// Returns the value of the given property. The value will be sent in the data field of the replay message.
    /// </summary>
    public virtual async Task<T?> GetAsync(MpvAsyncOptions? options = null)
    {
        var result = await Mpv.GetPropertyAsync<TRaw>(PropertyName, options);
        return ParseValue(result);
    }
}

/// <summary>
/// Represents a read-only MPV property of type String.
/// </summary>
public class MpvPropertyReadString : MpvPropertyReadRef<string, string>
{
    public MpvPropertyReadString(MpvContext mpv, string name) : base(mpv, name)
    {
    }
}
