namespace HanumanInstitute.LibMpv;

public class MpvOptionWithNoAlways<T> : MpvOptionWithNo<T>
    where T : struct
{
    public MpvOptionWithNoAlways(MpvContext mpv, string name) :
        base(mpv, name)
    {
    }

    /// <summary>
    /// Sets the option to 'always'.
    /// </summary>
    public void SetAlways() => SetValue("always");

    /// <summary>
    /// Sets the option to 'always'.
    /// </summary>
    public Task SetAlwaysAsync(ApiCommandOptions? options = null) => SetValueAsync("always", options);

    /// <summary>
    /// Gets whether the option is 'always'.
    /// </summary>
    public bool GetAlways() => GetValue(new[] { "always", "true" });

    /// <summary>
    /// Gets whether the option is 'always'.
    /// </summary>
    public Task<bool> GetAlwaysAsync(ApiCommandOptions? options = null) => GetValueAsync(new[] { "always", "true" }, options);
}
