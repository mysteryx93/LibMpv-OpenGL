namespace HanumanInstitute.LibMpv;

/// <summary>
/// Represents a key in the script-opts option dictionary.
/// </summary>
public class MpvScriptOption
{
    private readonly MpvOptionDictionary _options;
    private readonly string _key;

    public MpvScriptOption(MpvContext mpv, string key)
    {
        mpv.CheckNotNull(nameof(mpv));
        _options = new MpvOptionDictionary(mpv, "script-opts");
        _key = key.CheckNotNull(nameof(key));
    }

    /// <summary>
    /// Gets the value of the script option.
    /// </summary>
    public Task<string?> GetAsync(ApiCommandOptions? options = null) => _options.GetAsync(_key, options);
    /// <summary>
    /// Sets the value of the script option.
    /// </summary>
    public async Task SetAsync(string value, ApiCommandOptions? options = null)
    {
        if (value.HasValue())
        {
            await _options.AddAsync(_key, value, options);
        }
        else
        {
            await RemoveAsync(options);
        }
    }

    /// <summary>
    /// Removes the value from script options.
    /// </summary>
    public Task RemoveAsync(ApiCommandOptions? options = null) => _options.RemoveAsync(_key, options);
}
