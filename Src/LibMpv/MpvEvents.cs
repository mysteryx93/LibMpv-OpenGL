// ReSharper disable UnusedAutoPropertyAccessor.Global

using HanumanInstitute.LibMpv.Core;

namespace HanumanInstitute.LibMpv;

public class MpvEventArgs : EventArgs
{
    public MpvEventArgs(MpvEvent e)
    {
        RequestId = e.ReplyUserData;
        ErrorCode = e.Error;
    }

    public ulong RequestId { get; }
    public int ErrorCode { get; }
}

public class MpvPropertyEventArgs : MpvEventArgs
{
    public MpvPropertyEventArgs(MpvFormat format, string name, object? value, MpvEvent e) : base(e)
    {
        Format = format;
        Name = name;
        Value = value;
    }

    public MpvFormat Format { get; }
    public string Name { get; }
    public object? Value { get; }
}

public class MpvCommandReplyEventArgs : MpvEventArgs
{
    public MpvCommandReplyEventArgs(MpvNode data, MpvEvent e) : base(e) 
    {
        Data = data;
    }

    public MpvNode Data { get; }
}

public class MpvLogMessageEventArgs : EventArgs
{
    public MpvLogMessageEventArgs(string prefix, string level, string text, MpvLogLevel logLevel)
    {
        Prefix = prefix;
        Level = level;
        Text = text;
        LogLevel = logLevel;
    }

    public string Prefix { get; }
    public string Level { get; }
    public string Text { get; }
    public MpvLogLevel LogLevel { get; }
}

public class MpvEndFileEventArgs : EventArgs
{
    public MpvEndFileEventArgs(MpvEndFileReason reason, int error, long playListEntryId)
    {
        Reason = reason;
        Error = error;
        PlayListEntryId = playListEntryId;
    }

    public MpvEndFileReason Reason { get; }
    public int Error { get; }
    public long PlayListEntryId { get; }
}

public class MpvStartFileEventArgs : EventArgs
{
    public MpvStartFileEventArgs(long playListEntryId)
    {
        PlayListEntryId = playListEntryId;
    }
    public long PlayListEntryId { get; }
}
