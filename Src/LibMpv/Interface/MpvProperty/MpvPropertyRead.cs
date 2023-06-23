namespace HanumanInstitute.LibMpv;

/// <summary>
/// Represents a read-only MPV property.
/// </summary>
/// <typeparam name="T">The return type of the property.</typeparam>
public class MpvPropertyRead<T> : MpvProperty<T?>
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
    public T? Get() => Mpv.GetProperty<T?>(PropertyName);
    
    /// <summary>
    /// Returns the value of the given property. The value will be sent in the data field of the replay message.
    /// </summary>
    public Task<T?> GetAsync(ApiCommandOptions? options = null) => Mpv.GetPropertyAsync<T?>(PropertyName, options);
}

/// <summary>
/// Represents a read-only MPV property.
/// </summary>
/// <typeparam name="T">The return type of the property.</typeparam>
public class MpvPropertyReadRef<T> : MpvProperty<T?>
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
    public virtual T? Get() => Mpv.GetProperty<T?>(PropertyName);

    /// <summary>
    /// Returns the value of the given property. The value will be sent in the data field of the replay message.
    /// </summary>
    public virtual Task<T?> GetAsync(ApiCommandOptions? options = null) => Mpv.GetPropertyAsync<T?>(PropertyName, options);
}

/// <summary>
/// Represents a read-only MPV property of type String.
/// </summary>
public class MpvPropertyReadString : MpvPropertyReadRef<string>
{
    public MpvPropertyReadString(MpvContext mpv, string name) : base(mpv, name)
    {
    }
}
