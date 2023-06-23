namespace HanumanInstitute.LibMpv;

/// <summary>
/// Represents a comma-delimited option list of string values.
/// </summary>
public class MpvOptionList : MpvOptionRef<IEnumerable<string>>
{
    // Note: API doesn't support escaping, so there's no way of interpreting values containing a separator.
    // As a reliable work-around, all APIs interpreting separators are discarded. The implemented methods work reliably with any values.

    public MpvOptionList(MpvContext mpv, string name) : base(mpv, name)
    {
        // _separator = isPath ? System.IO.Path.PathSeparator : ',';
    }

    /// <summary>
    /// Get the list of items.
    /// </summary>
    public new IEnumerable<string> Get() =>
        Mpv.GetProperty<IEnumerable<string>?>(PropertyName) ?? Array.Empty<string>();

    /// <summary>
    /// Get the list of items.
    /// </summary>
    public new async Task<IEnumerable<string>> GetAsync(ApiCommandOptions? options = null) =>
        await Mpv.GetPropertyAsync<IEnumerable<string>?>(PropertyName, options) ?? Array.Empty<string>();

    /// <summary>
    /// Set a list of items (using the list separator, interprets escapes).
    /// </summary>
    public void Set(string value) => Set(new[] { value });

    /// <summary>
    /// Set a list of items (using the list separator, interprets escapes).
    /// </summary>
    public Task SetAsync(string value, ApiCommandOptions? options = null) => SetAsync(new[] { value }, options);

    /// <summary>
    /// Set a list of items.
    /// </summary>
    public override void Set(IEnumerable<string> values)
    {
        // For some properties, SetProperty calls Append instead of Set, so we clear first for consistency.
        Clear();
        Add(values);
    }
    
    /// <summary>
    /// Set a list of items.
    /// </summary>
    public override async Task SetAsync(IEnumerable<string> values, ApiCommandOptions? options = null)
    {
        // For some properties, SetProperty calls Append instead of Set, so we clear first for consistency.
        await ClearAsync(options);
        await AddAsync(values, options);
    }

    /// <summary>
    /// Append single item.
    /// </summary>
    public void Add(string value, ApiCommandOptions? options = null) =>
        Mpv.ChangeList(PropertyName, 
            ListOptionOperation.Append, value.CheckNotNullOrEmpty(nameof(value))).Invoke(options);

    /// <summary>
    /// Append single item.
    /// </summary>
    public Task AddAsync(string value, ApiCommandOptions? options = null) =>
        Mpv.ChangeList(PropertyName, 
            ListOptionOperation.Append, value.CheckNotNullOrEmpty(nameof(value))).InvokeAsync(options);

    /// <summary>
    /// Adds a list of items to the list.
    /// </summary>
    /// <param name="values">The list of items to add.</param>
    /// <returns></returns>
    public override void Add(IEnumerable<string> values, ApiCommandOptions? options = null)
    {
        foreach (var item in values)
        {
            if (item.HasValue())
            {
                Add(item, options);
            }
        }
    }
    
    /// <summary>
    /// Adds a list of items to the list.
    /// </summary>
    /// <param name="values">The list of items to add.</param>
    /// <returns></returns>
    public override async Task AddAsync(IEnumerable<string> values, ApiCommandOptions? options = null)
    {
        foreach (var item in values)
        {
            if (item.HasValue())
            {
                await AddAsync(item, options);
            }
        }
    }

    /// <summary>
    /// Clear the option (remove all items).
    /// </summary>
    public void Clear(ApiCommandOptions? options = null) => Mpv.ChangeList(PropertyName, ListOptionOperation.Clr, string.Empty).Invoke(options);

    /// <summary>
    /// Clear the option (remove all items).
    /// </summary>
    public Task ClearAsync(ApiCommandOptions? options = null) => Mpv.ChangeList(PropertyName, ListOptionOperation.Clr, string.Empty).InvokeAsync(options);

    /// <summary>
    /// Delete item if present (does not interpret escapes).
    /// </summary>
    public void Remove(string value, ApiCommandOptions? options = null) =>
        Mpv.ChangeList(PropertyName, 
            ListOptionOperation.Remove, value.CheckNotNullOrEmpty(nameof(value))).Invoke(options);

    /// <summary>
    /// Delete item if present (does not interpret escapes).
    /// </summary>
    public Task RemoveAsync(string value, ApiCommandOptions? options = null) =>
        Mpv.ChangeList(PropertyName, 
            ListOptionOperation.Remove, value.CheckNotNullOrEmpty(nameof(value))).InvokeAsync(options);

    /// <summary>
    /// Append an item, or remove if if it already exists (no escapes).
    /// </summary>
    public void Toggle(string value, ApiCommandOptions? options = null) => 
        Mpv.ChangeList(PropertyName, ListOptionOperation.Toggle, value).Invoke(options);

    /// <summary>
    /// Append an item, or remove if if it already exists (no escapes).
    /// </summary>
    public Task ToggleAsync(string value, ApiCommandOptions? options = null) => 
        Mpv.ChangeList(PropertyName, ListOptionOperation.Toggle, value).InvokeAsync(options);
}
