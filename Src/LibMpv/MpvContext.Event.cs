using System.Diagnostics;
using HanumanInstitute.LibMpv.Api;

namespace HanumanInstitute.LibMpv;

public unsafe partial class MpvContext
{
    private Dictionary<MpvEventId, MpvEventHandler> _eventHandlers;
    private delegate void MpvEventHandler(MpvEvent @event);

    public event EventHandler? Shutdown;
    public event EventHandler<MpvStartFileEventArgs>? StartFile;
    public event EventHandler<MpvEndFileEventArgs>? EndFile;
    public event EventHandler? FileLoaded;
    public event EventHandler? Idle;
    public event EventHandler? Tick;
    public event EventHandler? VideoReconfig;
    public event EventHandler? AudioReconfig;
    public event EventHandler? Seek;
    public event EventHandler? PlaybackRestart;
    public event EventHandler? QueueOverflow;
    public event EventHandler<MpvPropertyEventArgs>? PropertyChanged;
    public event EventHandler<MpvReplyEventArgs>? AsyncCommandReply;
    public event EventHandler<MpvPropertyEventArgs>? AsyncGetPropertyReply;
    public event EventHandler<MpvReplyEventArgs>? AsyncSetPropertyReply;
    public event EventHandler<MpvLogMessageEventArgs>? LogMessage;

    private void InitEventHandlers()
    {
        _eventHandlers = new Dictionary<MpvEventId, MpvEventHandler>()
        {
            { MpvEventId.None, TraceHandler },
            { MpvEventId.Shutdown, ShutdownHandler },
            { MpvEventId.LogMessage, LogMessageHandler },
            { MpvEventId.GetPropertyReply, AsyncGetPropertyHandler },
            { MpvEventId.SetPropertyReply, AsyncSetPropertyHandler },
            { MpvEventId.CommandReply, AsyncCommandReplyHandler },
            { MpvEventId.StartFile, StartFileHandler },
            { MpvEventId.EndFile, EndFileHandler },
            { MpvEventId.FileLoaded, FileLoadedHandler },
            { MpvEventId.EventIdle, IdleHandler },
            { MpvEventId.EventTick, TickHandler },
            { MpvEventId.EventClientMessage, TraceHandler },
            { MpvEventId.EventVideoReconfig, VideoReconfigHandler },
            { MpvEventId.EventAudioReconfig, AudioReconfigHandler },
            { MpvEventId.Seek, SeekHandler },
            { MpvEventId.PlaybackRestart, PlaybackRestartHandler },
            { MpvEventId.PropertyChange, PropertyChangedHandler },
            { MpvEventId.QueueOverflow, QueueOverflowHandler },
            { MpvEventId.Hook, TraceHandler },
        };
    }

    private void LogMessageHandler(MpvEvent @event)
    {
        if (@event.Data != null && LogMessage != null)
        {
            LogMessage?.Invoke(this, ToLogMessageEventArgs(@event));
        }
    }

    private void AsyncSetPropertyHandler(MpvEvent @event)
    {
        if (@event.Data != null && AsyncSetPropertyReply != null)
        {
            AsyncSetPropertyReply?.Invoke(this, new MpvReplyEventArgs(@event.ReplyUserData, @event.Error));
        }
    }

    private void AsyncGetPropertyHandler(MpvEvent @event)
    {
        if (@event.Data != null && AsyncGetPropertyReply != null)
        {
            AsyncGetPropertyReply?.Invoke(this, ToPropertyChangedEventArgs(@event));
        }
    }

    private void AsyncCommandReplyHandler(MpvEvent @event)
    {
        if (@event.Data != null)
        {
            AsyncCommandReply?.Invoke(this, new MpvReplyEventArgs(@event.ReplyUserData, @event.Error));
        }
    }

    private void PropertyChangedHandler(MpvEvent @event)
    {
        if (@event.Data != null && PropertyChanged != null)
        {
            PropertyChanged?.Invoke(this, ToPropertyChangedEventArgs(@event));
        }
    }

    private void QueueOverflowHandler(MpvEvent @event)
    {
        QueueOverflow?.Invoke(this, EventArgs.Empty);
    }

    private void PlaybackRestartHandler(MpvEvent @event)
    {
        PlaybackRestart?.Invoke(this, EventArgs.Empty);
    }

    private void SeekHandler(MpvEvent @event)
    {
        Seek?.Invoke(this, EventArgs.Empty);
    }

    private void AudioReconfigHandler(MpvEvent @event)
    {
        AudioReconfig?.Invoke(this, EventArgs.Empty);
    }

    private void VideoReconfigHandler(MpvEvent @event)
    {
        VideoReconfig?.Invoke(this, EventArgs.Empty);
    }

    private void TickHandler(MpvEvent @event)
    {
        Tick?.Invoke(this, EventArgs.Empty);
    }

    private void IdleHandler(MpvEvent @event)
    {
        Idle?.Invoke(this, EventArgs.Empty);
    }

    private void FileLoadedHandler(MpvEvent mpvEvent)
    {
        FileLoaded?.Invoke(this, EventArgs.Empty);
    }

    private void EndFileHandler(MpvEvent @event)
    {
        if (@event.Data != null && EndFile != null)
        {
            MpvEventEndFile endFile = MarshalHelper.PtrToStructure<MpvEventEndFile>((nint) @event.Data);
            EndFile?.Invoke(this, new MpvEndFileEventArgs(endFile.Reason, endFile.Error, endFile.PlaylistEntryId));
        }
    }

    private void StartFileHandler(MpvEvent @event)
    {
        if (@event.Data != null && StartFile != null)
        {
            MpvEventStartFile startFile = MarshalHelper.PtrToStructure<MpvEventStartFile>((nint) @event.Data);
            StartFile?.Invoke(this, new MpvStartFileEventArgs(startFile.PlaylistEntryId));
        }
    }

    private void ShutdownHandler(MpvEvent @event)
    {
        Shutdown?.Invoke(this, EventArgs.Empty);
    }

    private void TraceHandler(MpvEvent @event)
    {
        Debug.WriteLine($"Unhandled MPV Event: {Enum.GetName(typeof(MpvEventId), @event.EventId)}");
    }

    private void HandleEvent(MpvEvent @event)
    {
        if (_eventHandlers.TryGetValue(@event.EventId, out var eventHandler))
        {
            eventHandler.Invoke(@event);
        }
    }

    private MpvLogMessageEventArgs ToLogMessageEventArgs(MpvEvent @event)
    {
        MpvEventLogMessage logMessage = MarshalHelper.PtrToStructure<MpvEventLogMessage>((nint) @event.Data);
        return new MpvLogMessageEventArgs(
            MarshalHelper.PtrToStringUtf8OrEmpty((nint) logMessage.Prefix),
            MarshalHelper.PtrToStringUtf8OrEmpty((nint) logMessage.Level),
            MarshalHelper.PtrToStringUtf8OrEmpty((nint) logMessage.Text),
            logMessage.LogLevel
        );
    }


    private MpvPropertyEventArgs ToPropertyChangedEventArgs(MpvEvent @event)
    {
        MpvEventProperty property = MarshalHelper.PtrToStructure<MpvEventProperty>((nint) @event.Data);

        object? value = null;

        if (property.Format == MpvFormat.String)
        {
            value = MarshalHelper.PtrToStringUtf8OrNull((nint) property.Data);
        }
        else if (property.Format == MpvFormat.Int64)
        {
            value = Marshal.ReadInt64((nint) property.Data);
        }
        else if (property.Format == MpvFormat.Flag)
        {
            var flag = Marshal.ReadInt32((nint) property.Data);
            value = flag == 1;
        }
        else if (property.Format == MpvFormat.Double)
        {
            var doubleBytes = new byte[sizeof(double)];
            Marshal.Copy((nint) property.Data, doubleBytes, 0, sizeof(double));
            value = BitConverter.ToDouble(doubleBytes, 0);
        }
        var name = MarshalHelper.PtrToStringUtf8OrEmpty((nint) property.Name);
        return new MpvPropertyEventArgs(property.Format, name, value, @event.ReplyUserData, @event.Error);
    }
}