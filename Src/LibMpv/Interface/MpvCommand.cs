namespace HanumanInstitute.LibMpv;

public class MpvCommand<T>
{
    private readonly MpvContextBase _context;
    private readonly object?[] _cmd;

    public MpvCommand(MpvContextBase context, params object?[] cmd)
    {
        _context = context;
        _cmd = cmd;
    }

    public T Invoke(ApiCommandOptions? options = null) => (T)_context.RunCommandRet(options, _cmd);

    public async Task<T> InvokeAsync(ApiCommandOptions? options = null) => (T)(await _context.CommandAsync(options, _cmd))!;
}

public class MpvCommand
{
    private readonly MpvContextBase _context;
    private readonly object?[] _cmd;

    public MpvCommand(MpvContextBase context, params object?[] cmd)
    {
        _context = context;
        _cmd = cmd;
    }

    public void Invoke(ApiCommandOptions? options = null) => _context.RunCommandRet(options, _cmd);

    public async Task InvokeAsync(ApiCommandOptions? options = null) => await _context.CommandAsync(options, _cmd);
}
