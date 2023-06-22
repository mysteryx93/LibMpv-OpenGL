using System.Diagnostics;
using System.Globalization;
using HanumanInstitute.LibMpv.Core;

namespace HanumanInstitute.LibMpv;

/// <summary>
/// Handles basic communication protocols with MPV via IPC named pipe.
/// </summary>
public partial class MpvContext
{
    private ulong _requestId = 1;
    private readonly List<MpvEventArgs> _responses = new();
    private bool _logEnabled;
    private readonly object _lockLogEnabled = new object();
    private const bool DefaultWaitForResponse = true;
    private const int DefaultResponseTimeout = 3000;
    private const bool DefaultThrowOnError = false;
    private readonly ManualResetEvent _waitResponse = new(true);

    /// <summary>
    /// Gets or sets the default options for all requests passing through this controller.
    /// </summary>
    public ApiCommandOptions DefaultOptions { get; } = new ApiCommandOptions()
    {
        WaitForResponse = DefaultWaitForResponse,
        ResponseTimeout = DefaultResponseTimeout,
        ThrowOnError = DefaultThrowOnError
    };

    /// <summary>
    /// Gets whether to wait for response, first taking the value in options, if null taking the value in DefaultOptions, and if null taking a default value.
    /// </summary>
    /// <param name="options">Optional command options, may be null.</param>
    public bool GetWaitForResponseOption(ApiAsyncOptions? options) => options?.WaitForResponse ?? DefaultOptions.WaitForResponse ?? DefaultWaitForResponse;

    /// <summary>
    /// Gets the response timeout, first taking the value in options, if null taking the value in DefaultOptions, and if null taking a default value.
    /// </summary>
    /// <param name="options">Optional command options, may be null.</param>
    public int GetResponseTimeoutOption(ApiAsyncOptions? options) => options?.ResponseTimeout ?? DefaultOptions.ResponseTimeout ?? DefaultResponseTimeout;

    /// <summary>
    /// Gets whether to throw an exception on error, first taking the value in options, if null taking the value in DefaultOptions, and if null taking a default value.
    /// </summary>
    /// <param name="options">Optional command options, may be null.</param>
    public bool GetThrowOnErrorOption(ApiAsyncOptions? options) => options?.ThrowOnError ?? DefaultOptions.ThrowOnError ?? DefaultThrowOnError;

    /// <summary>
    /// Gets a text log of communication data from both directions.
    /// </summary>
    public StringBuilder? Log { get; private set; }

    /// <summary>
    /// Gets or sets whether to keep a log of communication data.
    /// </summary>
    public bool LogEnabled
    {
        get => _logEnabled;
        set
        {
            lock (_lockLogEnabled)
            {
                if (value && !_logEnabled)
                {
                    Log = new StringBuilder();
                }
                else if (!value && _logEnabled)
                {
                    Log = null;
                }
                _logEnabled = value;
            }
        }
    }

    /// <summary>
    /// Occurs when a full message has been received.
    /// </summary>
    /// <param name="e">The message received from the player.</param>
    private void Command_Reply(MpvCommandReplyEventArgs e)
    {
        if (e.ErrorCode != 0) { return; }
        
        if (e.RequestId > 0)
        {
            LogAppend(e.Data);
            // Add to list of responses to be retrieved by QueryId.
            lock (_responses)
            {
                _responses.Add(e);
            }
            _waitResponse.Set();
        }
    }
    
    /// <summary>
    /// Occurs when a full message has been received.
    /// </summary>
    /// <param name="e">The message received from the player.</param>
    private void GetProperty_Reply(MpvPropertyEventArgs e)
    {
        if (e.ErrorCode != 0) { return; }
        
        if (e.RequestId > 0)
        {
            LogAppend($"{e.Name}: {e.Value}");
            // Add to list of responses to be retrieved by QueryId.
            lock (_responses)
            {
                _responses.Add(e);
            }
            _waitResponse.Set();
        }
    }
    
    /// <summary>
    /// Occurs when a full message has been received.
    /// </summary>
    /// <param name="e">The message received from the player.</param>
    private void SetProperty_Reply(MpvEventArgs e)
    {
        if (e.ErrorCode != 0) { return; }
        
        if (e.RequestId > 0)
        {
            // Add to list of responses to be retrieved by QueryId.
            lock (_responses)
            {
                _responses.Add(e);
            }
            _waitResponse.Set();
        }
    }

    /// <summary>
    /// Sends specified message to MPV and returns the response as string.
    /// </summary>
    /// <param name="options">Additional command options.</param>
    /// <param name="cmd">The command values to send.</param>
    /// <returns>The server's response to the command.</returns>
    /// <exception cref="InvalidOperationException">The response contained an error and ThrowOnError is True.</exception>
    /// <exception cref="TimeoutException">A response from MPV was not received before timeout.</exception>
    /// <exception cref="FormatException">The data returned by the server could not be parsed.</exception>
    /// <exception cref="ObjectDisposedException">The underlying connection was disposed.</exception>
    public async Task<MpvNode?> CommandAsync(ApiCommandOptions? options, params string[] cmd)
    {
        cmd.CheckNotNullOrEmpty(nameof(cmd));

        // Append prefixes and remove null values at the end.
        var cmdLength = cmd.Length;
        var prefixes = options != null ? options.GetPrefixes() : DefaultOptions.GetPrefixes();
        var prefixCount = prefixes.Count;
        for (var i = cmd.Length - 1; i >= 0; i--)
        {
            if (cmd[i] == null)
            {
                cmdLength--;
            }
            else
            {
                break;
            }
        }
        if (cmdLength != cmd.Length || prefixCount > 0)
        {
            var cmd2 = cmd;
            cmd = new string[cmdLength + prefixCount];
            for (var i = 0; i < prefixCount; i++)
            {
                cmd[i] = prefixes![i];
            }
            Array.Copy(cmd2, 0, cmd, prefixCount, cmdLength);
        }
        
        // Prepare the request.
        var requestId = GetWaitForResponseOption(options) ? _requestId++ : 0;

        // Send the request.
        CommandAsync(requestId, cmd);
        LogAppend(cmd);

        if (requestId > 0)
        {
            var result = await WaitForResponseAsync(requestId, cmd[0] ?? string.Empty, options).ConfigureAwait(false) as MpvCommandReplyEventArgs;
            return result?.Data;
        }
        return null;
    }

    /// <summary>
    /// Sends specified message to MPV and returns the response.
    /// </summary>
    /// <param name="options">Additional command options.</param>
    /// <param name="name">The property to get.</param>
    /// <returns>The server's response to the command.</returns>
    /// <exception cref="InvalidOperationException">The response contained an error and ThrowOnError is True.</exception>
    /// <exception cref="TimeoutException">A response from MPV was not received before timeout.</exception>
    /// <exception cref="FormatException">The data returned by the server could not be parsed.</exception>
    /// <exception cref="ObjectDisposedException">The underlying connection was disposed.</exception>
    public async Task<object?> GetPropertyAsync(ApiAsyncOptions? options, string name, MpvFormat format)
    {
        // Prepare the request.
        var requestId = GetWaitForResponseOption(options) ? _requestId++ : 0;

        // Send the request.
        GetPropertyAsync(requestId, name, format);
        LogAppend(name);

        if (requestId > 0)
        {
            var result = await WaitForResponseAsync(requestId, name, options).ConfigureAwait(false) as MpvPropertyEventArgs;
            return result?.Value;
        }
        return null;
    }
    
    // /// <summary>
    // /// Sends specified message to MPV and returns the response.
    // /// </summary>
    // /// <param name="options">Additional command options.</param>
    // /// <param name="name">The property to get.</param>
    // /// <returns>The server's response to the command.</returns>
    // /// <exception cref="InvalidOperationException">The response contained an error and ThrowOnError is True.</exception>
    // /// <exception cref="TimeoutException">A response from MPV was not received before timeout.</exception>
    // /// <exception cref="FormatException">The data returned by the server could not be parsed.</exception>
    // /// <exception cref="ObjectDisposedException">The underlying connection was disposed.</exception>
    // public async Task SetPropertyAsync(ApiAsyncOptions? options, string name, MpvFormat format, void* value)
    // {
    //     // Prepare the request.
    //     var requestId = GetWaitForResponseOption(options) ? _requestId++ : 0;
    //
    //     // Send the request.
    //     SetPropertyAsync(requestId, name, format, value);
    //     LogAppend(name);
    //
    //     if (requestId > 0)
    //     {
    //         await WaitForResponseAsync(requestId, name, options).ConfigureAwait(false);
    //     }
    // }

    /// <summary>
    /// Waits for a response with specified request ID.
    /// </summary>
    /// <param name="requestId">The request ID to wait for.</param>
    /// <param name="commandName">The name of the command being executed.</param>
    /// <param name="options">Additional command options.</param>
    private async Task<MpvEventArgs?> WaitForResponseAsync(ulong requestId, string commandName, ApiAsyncOptions? options = null, CancellationToken? cancelToken = null)
    {
        // Wait for response with matching RequestId.
        var watch = new Stopwatch();
        watch.Start();
        var response = FindResponse(requestId);
        var maxTimeout = GetResponseTimeoutOption(options);
        while (response == null && (maxTimeout < 0 || watch.ElapsedMilliseconds < maxTimeout) && cancelToken?.IsCancellationRequested != true)
        {
            // Calculate wait timeout.
            var timeout = -1;
            if (maxTimeout > -1)
            {
                timeout = (int)(maxTimeout - watch.ElapsedMilliseconds);
                timeout = timeout < 0 ? 0 : timeout > 1000 ? 1000 : timeout;
            }

            // Wait until any message is received.
            _waitResponse.Reset();
            await _waitResponse.WaitOneAsync(timeout, cancelToken).ConfigureAwait(false);
            response = FindResponse(requestId);
        }

        // Timeout.
        if (response == null)
        {
            throw new TimeoutException($"A response from MPV to request_id={requestId} was not received before timeout.");
        }
        else
        {
            // Remove response from list.
            lock (_responses)
            {
                _responses.Remove(response);
            }
        }

        if (GetThrowOnErrorOption(options) && response.ErrorCode < 0)
        {
            throw MpvException.FromCode(response.ErrorCode); 
        }
        return response.ErrorCode >= 0 ? response : null;
    }

    /// <summary>
    /// Returns the response with specified ID from the list of received responses.
    /// </summary>
    /// <param name="requestId">The request id to look for.</param>
    /// <returns>The response with matching id.</returns>
    private MpvEventArgs? FindResponse(ulong requestId)
    {
        lock (_responses)
        {
            return _responses.FirstOrDefault(x => x.RequestId == requestId);
        }
    }

    private void LogAppend(MpvNode args)
    {
        if (Log != null)
        {
            LogAppend(args.ToString());
        }
    }
    
    private void LogAppend(string[] args)
    {
        if (Log != null)
        {
            LogAppend(string.Join(" ", args));
        }
    }
    
    /// <summary>
    /// Adds a message to the log if enabled.
    /// </summary>
    /// <param name="message">The message to append to the log.</param>
    private void LogAppend(string message)
    {
        if (Log != null)
        {
            lock (Log)
            {
                Log?.Append(message);
            }
        }
    }
}
