using System.Runtime.CompilerServices;
using LibMpv.Api;

namespace LibMpv;

public unsafe partial class MpvContext : IDisposable
{
    private bool _disposed;
    private IEventLoop _eventLoop;
    protected MpvHandle* Ctx => _ctx != null ? _ctx : InitCtx();
    private MpvHandle* _ctx;
    private MpvHandle* InitCtx()
    {
        _ctx = Mpv.Create();
        if (_ctx == null)
        {
            throw new MpvException("Unable to create mpv_context. Currently, this can happen in the following situations - out of memory or LC_NUMERIC is not set to \"C\"");
        }
        return _ctx;
    }

    public MpvContext() : this(MpvEventLoop.Default)
    {
    }

    public MpvContext(MpvEventLoop mpvEventLoop)
    {
        var code = Mpv.Initialize(Ctx);
        CheckCode(code);

        InitEventHandlers();

        _eventLoop = mpvEventLoop switch
        {
            MpvEventLoop.Default => new MpvSimpleEventLoop(Ctx, this.HandleEvent),
            MpvEventLoop.Thread => new MpvThreadEventLoop(Ctx, this.HandleEvent),
            _ => new MpvWeakEventLoop(Ctx, this.HandleEvent)
        };

        _eventLoop.Start();
    }

    ~MpvContext()
    {
        Dispose(false);
    }

    public void RequestLogMessages(string level)
    {
        CheckDisposed();
        Mpv.RequestLogMessages(Ctx, level);
    }
    
    public string? GetPropertyString(string name)
    {
        CheckDisposed();
        var value = Mpv.GetPropertyString(Ctx, name);
        return Utf8Marshaler.FromNative(Encoding.UTF8, value);
    }

    public void SetPropertyString(string name, string newValue)
    {
        CheckDisposed();
        int code = Mpv.SetPropertyString(Ctx, name, newValue);
        CheckCode(code);
    }

    public bool GetPropertyFlag(string name)
    {
        CheckDisposed();
        int code;
        var value= new int[1] { 0 };
        fixed(int* valuePtr = value)
        {
            code = Mpv.GetProperty(Ctx, name, MpvFormat.Flag, valuePtr );
        }
        CheckCode(code);
        return value[0] == 1 ? true : false;
    }

    public void SetPropertyFlag(string name, bool newValue)
    {
        CheckDisposed();
        int code;
        var value = new int[1] { newValue ? 1:0 };
        fixed (int* valuePtr = value)
        {
            code = Mpv.SetProperty(Ctx, name, MpvFormat.Flag, valuePtr);
        }
        CheckCode(code);
    }

    public long GetPropertyLong(string name)
    {
        CheckDisposed();
        int code;
        var value = new long[1] { 0 };
        fixed (long* valuePtr = value)
        {
            code = Mpv.GetProperty(Ctx, name, MpvFormat.Int64, valuePtr);
        }
        CheckCode(code);
        return value[0];
    }

    public void SetPropertyLong(string name, long newValue)
    {
        CheckDisposed();
        int code;
        var value = new long[1] { newValue };
        fixed (long* valuePtr = value)
        {
            code = Mpv.SetProperty(Ctx, name, MpvFormat.Int64, valuePtr);
        }
        CheckCode(code);
    }

    public double GetPropertyDouble(string name)
    {
        CheckDisposed();
        int code;
        var value = new double[1] { 0 };
        fixed (double* valuePtr = value)
        {
            code = Mpv.GetProperty(Ctx, name, MpvFormat.Double, valuePtr);
        }
        CheckCode(code);
        return value[0];
    }

    public void SetPropertyDouble(string name, double newValue)
    {
        CheckDisposed();
        int code;
        var value = new double[1] { 0 };
        fixed (double* valuePtr = value)
        {
            code = Mpv.SetProperty(Ctx, name, MpvFormat.Double, valuePtr);
        }
        CheckCode(code);
    }

    public void ObserveProperty(string name, MpvFormat format, ulong userData)
    {
        CheckDisposed();
        int code = Mpv.ObserveProperty(Ctx, userData, name, format);
        CheckCode(code);
    }

    public void UnobserveProperty(ulong userData)
    {
        CheckDisposed();
        int code = Mpv.UnobserveProperty(Ctx, userData);
        CheckCode(code);
    }

    public void Command(params string[] args)
    {
        if (args.Length == 0)
        {
            throw new ArgumentException("Missing arguments.", nameof(args));
        }

        CheckDisposed();

        using var helper = new MarshalHelper();
        var code = Mpv.Command(Ctx, (byte**) helper.CStringArrayForManagedUtf8StringArray(args));

        CheckCode(code);
    }

    public void CommandAsync(ulong userData, params string[] args)
    {
        if (args.Length == 0)
        {
            throw new ArgumentException("Missing arguments.", nameof(args));
        }

        CheckDisposed();

        using var helper = new MarshalHelper();
        var code = Mpv.CommandAsync(Ctx, userData, (byte**) helper.CStringArrayForManagedUtf8StringArray(args));

        CheckCode(code);
    }

    public void SetOptionString(string name, string data)
    {
        CheckDisposed();
        var code = Mpv.SetOptionString(Ctx, name, data);
        CheckCode(code);
    }

    public void RequestEvent(MpvEventId @event, bool enabled)
    {
        CheckDisposed();
        var code = Mpv.RequestEvent(Ctx, @event, enabled ? 1 : 0);
        CheckCode(code);
    }

    public string EventName(MpvEventId @event) => Mpv.EventName(@event);

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    public virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            StopRendering();
            _eventLoop.Stop();
            if (_eventLoop is IDisposable disposable)
            {
                disposable.Dispose();
            }
            Mpv.TerminateDestroy(Ctx);
            _disposed = true;
        }
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected int CheckCode(int code)
    {
        if (code >= 0)
        {
            return code;
        }
        throw MpvException.FromCode(code);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected void CheckDisposed()
    {
        if (_disposed)
        {
            throw new ObjectDisposedException(nameof(MpvContext));
        }
    }
}
