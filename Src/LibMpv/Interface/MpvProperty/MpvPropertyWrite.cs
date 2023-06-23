namespace HanumanInstitute.LibMpv;

/// <summary>
/// Represents a read/write MPV property.
/// </summary>
/// <typeparam name="T">The return type of the property.</typeparam>
public class MpvPropertyWrite<T> : MpvPropertyRead<T>
    where T : struct
{
    public MpvPropertyWrite(MpvContext mpv, string name) : base(mpv, name)
    {
    }

    /// <summary>
    /// Set the given property or option to the given value.
    /// </summary>
    /// <param name="value">The value to set.</param>
    public virtual void Set(T value, ApiCommandOptions? options = null) =>
        Mpv.SetProperty(PropertyName, value);

    /// <summary>
    /// Set the given property or option to the given value.
    /// </summary>
    /// <param name="value">The value to set.</param>
    public virtual Task SetAsync(T value, ApiCommandOptions? options = null) =>
        Mpv.SetPropertyAsync<T>(PropertyName, value, options);

    /// <summary>
    /// Add the given value to the property or option. On overflow or underflow, clamp the property to the maximum. If value is omitted, assume 1.
    /// </summary>
    /// <param name="value">The value to add.</param>
    public virtual void Add(T value, ApiCommandOptions? options = null) =>
        Mpv.Add(PropertyName, value).Invoke(options);

    /// <summary>
    /// Add the given value to the property or option. On overflow or underflow, clamp the property to the maximum. If value is omitted, assume 1.
    /// </summary>
    /// <param name="value">The value to add.</param>
    public virtual Task AddAsync(T value, ApiCommandOptions? options = null) =>
        Mpv.Add(PropertyName, value).InvokeAsync(options);

    /// <summary>
    /// Similar to add, but multiplies the property or option with the numeric value.
    /// </summary>
    /// <param name="value">The multiplication factor.</param>
    public virtual void Multiply(double value, ApiCommandOptions? options = null) =>
        Mpv.Multiply(PropertyName, value).Invoke(options);

    /// <summary>
    /// Similar to add, but multiplies the property or option with the numeric value.
    /// </summary>
    /// <param name="value">The multiplication factor.</param>
    public virtual Task MultiplyAsync(double value, ApiCommandOptions? options = null) =>
        Mpv.Multiply(PropertyName, value).InvokeAsync(options);

    /// <summary>
    /// Cycles the given property or option. The second argument can be up or down to set the cycle direction. On overflow, set the property back to the minimum, on underflow set it to the maximum.
    /// </summary>
    /// <param name="direction">The cycling direction. By default, Up.</param>
    public virtual void Cycle(CycleDirection direction = CycleDirection.Up, ApiCommandOptions? options = null) =>
        Mpv.Cycle(PropertyName, direction).Invoke(options);

    /// <summary>
    /// Cycles the given property or option. The second argument can be up or down to set the cycle direction. On overflow, set the property back to the minimum, on underflow set it to the maximum.
    /// </summary>
    /// <param name="direction">The cycling direction. By default, Up.</param>
    public virtual Task CycleAsync(CycleDirection direction = CycleDirection.Up, ApiCommandOptions? options = null) =>
        Mpv.Cycle(PropertyName, direction).InvokeAsync(options);
}

/// <summary>
/// Represents a read/write MPV property.
/// </summary>
/// <typeparam name="T">The return type of the property.</typeparam>
public class MpvPropertyWriteRef<T> : MpvPropertyReadRef<T>
    where T : class
{
    public MpvPropertyWriteRef(MpvContext mpv, string name) : base(mpv, name)
    {
    }

    /// <summary>
    /// Set the given property or option to the given value.
    /// </summary>
    /// <param name="value">The value to set.</param>
    public virtual void Set(T value) =>
        Mpv.SetProperty(PropertyName, value);

    /// <summary>
    /// Set the given property or option to the given value.
    /// </summary>
    /// <param name="value">The value to set.</param>
    public virtual Task SetAsync(T value, ApiCommandOptions? options = null) =>
        Mpv.SetPropertyAsync(PropertyName, value, options);

    /// <summary>
    /// Add the given value to the property or option. On overflow or underflow, clamp the property to the maximum. If value is omitted, assume 1.
    /// </summary>
    /// <param name="value">The value to add.</param>
    public virtual void Add(T value, ApiCommandOptions? options = null) =>
        Mpv.Add(PropertyName, value).Invoke(options);

    /// <summary>
    /// Add the given value to the property or option. On overflow or underflow, clamp the property to the maximum. If value is omitted, assume 1.
    /// </summary>
    /// <param name="value">The value to add.</param>
    public virtual Task AddAsync(T value, ApiCommandOptions? options = null) =>
        Mpv.Add(PropertyName, value).InvokeAsync(options);

    /// <summary>
    /// Similar to add, but multiplies the property or option with the numeric value.
    /// </summary>
    /// <param name="value">The multiplication factor.</param>
    public virtual void Multiply(double value, ApiCommandOptions? options = null) =>
        Mpv.Multiply(PropertyName, value).Invoke(options);

    /// <summary>
    /// Similar to add, but multiplies the property or option with the numeric value.
    /// </summary>
    /// <param name="value">The multiplication factor.</param>
    public virtual Task MultiplyAsync(double value, ApiCommandOptions? options = null) =>
        Mpv.Multiply(PropertyName, value).InvokeAsync(options);

    /// <summary>
    /// Cycles the given property or option. The second argument can be up or down to set the cycle direction. On overflow, set the property back to the minimum, on underflow set it to the maximum.
    /// </summary>
    /// <param name="direction">The cycling direction. By default, Up.</param>
    public virtual void Cycle(CycleDirection direction = CycleDirection.Up, ApiCommandOptions? options = null) =>
        Mpv.Cycle(PropertyName, direction).Invoke(options);

    /// <summary>
    /// Cycles the given property or option. The second argument can be up or down to set the cycle direction. On overflow, set the property back to the minimum, on underflow set it to the maximum.
    /// </summary>
    /// <param name="direction">The cycling direction. By default, Up.</param>
    public virtual Task CycleAsync(CycleDirection direction = CycleDirection.Up, ApiCommandOptions? options = null) =>
        Mpv.Cycle(PropertyName, direction).InvokeAsync(options);
}

/// <summary>
/// Represents a read/write MPV property of type String.
/// </summary>
public class MpvPropertyWriteString : MpvPropertyWriteRef<string>
{
    public MpvPropertyWriteString(MpvContext mpv, string name) : base(mpv, name)
    {
    }
}
