// ReSharper disable InvalidXmlDocComment

namespace LibMpv.Api;

public static unsafe partial class Mpv
{
    /// <summary>Signal to all async requests with the matching ID to abort. This affects the following API calls:</summary>
    /// <param name="replyUserData">ID of the request to be aborted (see above)</param>
    public static void AbortAsyncCommand(MpvHandle* ctx, ulong replyUserData) => MpvBindings.AbortAsyncCommand(ctx, replyUserData);
    
    /// <summary>Return the MPV_CLIENT_API_VERSION the mpv source has been compiled with.</summary>
    public static ulong ClientApiVersion() => MpvBindings.ClientApiVersion();
    
    /// <summary>Return the ID of this client handle. Every client has its own unique ID. This ID is never reused by the core, even if the mpv_handle at hand gets destroyed and new handles get allocated.</summary>
    /// <returns>The client ID.</returns>
    public static long ClientId(MpvHandle* ctx) => MpvBindings.ClientId(ctx);
    
    /// <summary>Return the name of this client handle. Every client has its own unique name, which is mostly used for user interface purposes.</summary>
    /// <returns>The client name. The string is read-only and is valid until the mpv_handle is destroyed.</returns>
    public static string ClientName(MpvHandle* ctx) => MpvBindings.ClientName(ctx);
    
    /// <summary>Send a command to the player. Commands are the same as those used in input.conf, except that this function takes parameters in a pre-split form.</summary>
    /// <param name="args">NULL-terminated list of strings. Usually, the first item is the command, and the following items are arguments.</param>
    /// <returns>error code</returns>
    public static int Command(MpvHandle* ctx, byte** args) => MpvBindings.Command(ctx, args);
    
    /// <summary>Same as mpv_command, but run the command asynchronously.</summary>
    /// <param name="replyUserData">the value mpv_event.replyUserData of the reply will be set to (see section about asynchronous calls)</param>
    /// <param name="args">NULL-terminated list of strings (see mpv_command())</param>
    /// <returns>error code (if parsing or queuing the command fails)</returns>
    public static int CommandAsync(MpvHandle* ctx, ulong replyUserData, byte** args) => MpvBindings.CommandAsync(ctx, replyUserData, args);
    
    /// <summary>Same as mpv_command(), but allows passing structured data in any format. In particular, calling mpv_command() is exactly like calling mpv_command_node() with the format set to MPV_FORMAT_NODE_ARRAY, and every arg passed in order as MPV_FORMAT_STRING.</summary>
    /// <param name="args">mpv_node with format set to one of the values documented above (see there for details)</param>
    /// <param name="result">Optional, pass NULL if unused. If not NULL, and if the function succeeds, this is set to command-specific return data. You must call mpv_free_node_contents() to free it (again, only if the command actually succeeds). Not many commands actually use this at all.</param>
    /// <returns>error code (the result parameter is not set on error)</returns>
    public static int CommandNode(MpvHandle* ctx, MpvNode* args, MpvNode* result) => MpvBindings.CommandNode(ctx, args, result);
    
    /// <summary>Same as mpv_command_node(), but run it asynchronously. Basically, this function is to mpv_command_node() what mpv_command_async() is to mpv_command().</summary>
    /// <param name="replyUserData">the value mpv_event.replyUserData of the reply will be set to (see section about asynchronous calls)</param>
    /// <param name="args">as in mpv_command_node()</param>
    /// <returns>error code (if parsing or queuing the command fails)</returns>
    public static int CommandNodeAsync(MpvHandle* ctx, ulong replyUserData, MpvNode* args) => MpvBindings.CommandNodeAsync(ctx, replyUserData, args);
    
    /// <summary>This is essentially identical to mpv_command() but it also returns a result.</summary>
    /// <param name="args">NULL-terminated list of strings. Usually, the first item is the command, and the following items are arguments.</param>
    /// <param name="result">Optional, pass NULL if unused. If not NULL, and if the function succeeds, this is set to command-specific return data. You must call mpv_free_node_contents() to free it (again, only if the command actually succeeds). Not many commands actually use this at all.</param>
    /// <returns>error code (the result parameter is not set on error)</returns>
    public static int CommandRet(MpvHandle* ctx, byte** args, MpvNode* result) => MpvBindings.CommandRet(ctx, args, result);
    
    /// <summary>Same as mpv_command, but use input.conf parsing for splitting arguments. This is slightly simpler, but also more error prone, since arguments may need quoting/escaping.</summary>
    public static int CommandString(MpvHandle* ctx, string args) => MpvBindings.CommandString(ctx, args);
    
    /// <summary>Create a new mpv instance and an associated client API handle to control the mpv instance. This instance is in a pre-initialized state, and needs to be initialized to be actually used with most other API functions.</summary>
    /// <returns>a new mpv client API handle. Returns NULL on error. Currently, this can happen in the following situations: - out of memory - LC_NUMERIC is not set to &quot;C&quot; (see general remarks)</returns>
    public static MpvHandle* Create() => MpvBindings.Create();
    
    /// <summary>Create a new client handle connected to the same player core as ctx. This context has its own event queue, its own mpv_request_event() state, its own mpv_request_log_messages() state, its own set of observed properties, and its own state for asynchronous operations. Otherwise, everything is shared.</summary>
    /// <param name="ctx">Used to get the reference to the mpv core; handle-specific settings and parameters are not used. If NULL, this function behaves like mpv_create() (ignores name).</param>
    /// <param name="name">The client name. This will be returned by mpv_client_name(). If the name is already in use, or contains non-alphanumeric characters (other than '_'), the name is modified to fit. If NULL, an arbitrary name is automatically chosen.</param>
    /// <returns>a new handle, or NULL on error</returns>
    public static MpvHandle* CreateClient(MpvHandle* ctx, string name) => MpvBindings.CreateClient(ctx, name);
    
    /// <summary>This is the same as mpv_create_client(), but the created mpv_handle is treated as a weak reference. If all mpv_handles referencing a core are weak references, the core is automatically destroyed. (This still goes through normal uninit of course. Effectively, if the last non-weak mpv_handle is destroyed, then the weak mpv_handles receive MPV_EVENT_SHUTDOWN and are asked to terminate as well.)</summary>
    public static MpvHandle* CreateWeakClient(MpvHandle* ctx, string name) => MpvBindings.CreateWeakClient(ctx, name);
    
    /// <summary>Convenience function to delete a property.</summary>
    /// <param name="name">The property name. See input.rst for a list of properties.</param>
    /// <returns>error code</returns>
    public static int DelProperty(MpvHandle* ctx, string name) => MpvBindings.DelProperty(ctx, name);
    
    /// <summary>Disconnect and destroy the mpv_handle. ctx will be deallocated with this API call.</summary>
    public static void Destroy(MpvHandle* ctx) => MpvBindings.Destroy(ctx);
    
    /// <summary>Return a string describing the error. For unknown errors, the string &quot;unknown error&quot; is returned.</summary>
    /// <param name="error">error number, see enum mpv_error</param>
    /// <returns>A static string describing the error. The string is completely static, i.e. doesn't need to be deallocated, and is valid forever.</returns>
    public static string ErrorString(int error) => MpvBindings.ErrorString(error);
    
    /// <summary>Return a string describing the event. For unknown events, NULL is returned.</summary>
    /// <param name="event">event ID, see see enum mpv_event_id</param>
    /// <returns>A static string giving a short symbolic name of the event. It consists of lower-case alphanumeric characters and can include &quot;-&quot; characters. This string is suitable for use in e.g. scripting interfaces. The string is completely static, i.e. doesn't need to be deallocated, and is valid forever.</returns>
    public static string EventName(MpvEventId @event) => MpvBindings.EventName(@event);
    
    /// <summary>Convert the given src event to a mpv_node, and set *dst to the result. *dst is set to a MPV_FORMAT_NODE_MAP, with fields for corresponding mpv_event and mpv_event.data/mpv_event_* fields.</summary>
    /// <param name="dst">Target. This is not read and fully overwritten. Must be released with mpv_free_node_contents(). Do not write to pointers returned by it. (On error, this may be left as an empty node.)</param>
    /// <param name="src">The source event. Not modified (it's not const due to the author's prejudice of the C version of const).</param>
    /// <returns>error code (MPV_ERROR_NOMEM only, if at all)</returns>
    public static int EventToNode(MpvNode* dst, MpvEvent* src) => MpvBindings.EventToNode(dst, src);
    
    /// <summary>General function to deallocate memory returned by some of the API functions. Call this only if it's explicitly documented as allowed. Calling this on mpv memory not owned by the caller will lead to undefined behavior.</summary>
    /// <param name="data">A valid pointer returned by the API, or NULL.</param>
    public static void Free(void* data) => MpvBindings.Free(data);
    
    /// <summary>Frees any data referenced by the node. It doesn't free the node itself. Call this only if the mpv client API set the node. If you constructed the node yourself (manually), you have to free it yourself.</summary>
    public static void FreeNodeContents(MpvNode* node) => MpvBindings.FreeNodeContents(node);
    
    /// <summary>Read the value of the given property.</summary>
    /// <param name="name">The property name.</param>
    /// <param name="format">see enum mpv_format.</param>
    /// <param name="data">Pointer to the variable holding the option value. On success, the variable will be set to a copy of the option value. For formats that require dynamic memory allocation, you can free the value with mpv_free() (strings) or mpv_free_node_contents() (MPV_FORMAT_NODE).</param>
    /// <returns>error code</returns>
    public static int GetProperty(MpvHandle* ctx, string name, MpvFormat format, void* data) => MpvBindings.GetProperty(ctx, name, format, data);
    
    /// <summary>Get a property asynchronously. You will receive the result of the operation as well as the property data with the MPV_EVENT_GET_PROPERTY_REPLY event. You should check the mpv_event.error field on the reply event.</summary>
    /// <param name="replyUserData">see section about asynchronous calls</param>
    /// <param name="name">The property name.</param>
    /// <param name="format">see enum mpv_format.</param>
    /// <returns>error code if sending the request failed</returns>
    public static int GetPropertyAsync(MpvHandle* ctx, ulong replyUserData, string name, MpvFormat format) => MpvBindings.GetPropertyAsync(ctx, replyUserData, name, format);
    
    /// <summary>Return the property as &quot;OSD&quot; formatted string. This is the same as mpv_get_property_string, but using MPV_FORMAT_OSD_STRING.</summary>
    /// <returns>Property value, or NULL if the property can't be retrieved. Free the string with mpv_free().</returns>
    public static byte* GetPropertyOsdString(MpvHandle* ctx, string name) => MpvBindings.GetPropertyOsdString(ctx, name);
    
    /// <summary>Return the value of the property with the given name as string. This is equivalent to mpv_get_property() with MPV_FORMAT_STRING.</summary>
    /// <param name="name">The property name.</param>
    /// <returns>Property value, or NULL if the property can't be retrieved. Free the string with mpv_free().</returns>
    public static byte* GetPropertyString(MpvHandle* ctx, string name) => MpvBindings.GetPropertyString(ctx, name);
    
    /// <summary>Return the internal time in microseconds. This has an arbitrary start offset, but will never wrap or go backwards.</summary>
    public static long GetTimeUs(MpvHandle* ctx) => MpvBindings.GetTimeUs(ctx);
    
    /// <summary>Return a UNIX file descriptor referring to the read end of a pipe. This pipe can be used to wake up a poll() based processing loop. The purpose of this function is very similar to mpv_set_wakeup_callback(), and provides a primitive mechanism to handle coordinating a foreign event loop and the libmpv event loop. The pipe is non-blocking. It's closed when the mpv_handle is destroyed. This function always returns the same value (on success).</summary>
    /// <returns>A UNIX FD of the read end of the wakeup pipe, or -1 on error. On MS Windows/MinGW, this will always return -1.</returns>
    [Obsolete("this function will be removed in the future. If you need this functionality, use mpv_set_wakeup_callback(), create a pipe manually, and call write() on your pipe in the callback.")]
    public static int GetWakeupPipe(MpvHandle* ctx) => MpvBindings.GetWakeupPipe(ctx);
    
    /// <summary>A hook is like a synchronous event that blocks the player. You register a hook handler with this function. You will get an event, which you need to handle, and once things are ready, you can let the player continue with mpv_hook_continue().</summary>
    /// <param name="replyUserData">This will be used for the mpv_event.replyUserData field for the received MPV_EVENT_HOOK events. If you have no use for this, pass 0.</param>
    /// <param name="name">The hook name. This should be one of the documented names. But if the name is unknown, the hook event will simply be never raised.</param>
    /// <param name="priority">See remarks above. Use 0 as a neutral default.</param>
    /// <returns>error code (usually fails only on OOM)</returns>
    public static int HookAdd(MpvHandle* ctx, ulong replyUserData, string name, int priority) => MpvBindings.HookAdd(ctx, replyUserData, name, priority);
    
    /// <summary>Respond to a MPV_EVENT_HOOK event. You must call this after you have handled the event. There is no way to &quot;cancel&quot; or &quot;stop&quot; the hook.</summary>
    /// <param name="id">This must be the value of the mpv_event_hook.id field for the corresponding MPV_EVENT_HOOK.</param>
    /// <returns>error code</returns>
    public static int HookContinue(MpvHandle* ctx, ulong id) => MpvBindings.HookContinue(ctx, id);
    
    /// <summary>Initialize an uninitialized mpv instance. If the mpv instance is already running, an error is returned.</summary>
    /// <returns>error code</returns>
    public static int Initialize(MpvHandle* ctx) => MpvBindings.Initialize(ctx);
    
    /// <summary>Load a config file. This loads and parses the file, and sets every entry in the config file's default section as if mpv_set_option_string() is called.</summary>
    /// <param name="filename">absolute path to the config file on the local filesystem</param>
    /// <returns>error code</returns>
    public static int LoadConfigFile(MpvHandle* ctx, string filename) => MpvBindings.LoadConfigFile(ctx, filename);
    
    /// <summary>Get a notification whenever the given property changes. You will receive updates as MPV_EVENT_PROPERTY_CHANGE. Note that this is not very precise: for some properties, it may not send updates even if the property changed. This depends on the property, and it's a valid feature request to ask for better update handling of a specific property. (For some properties, like ``clock``, which shows the wall clock, this mechanism doesn't make too much sense anyway.)</summary>
    /// <param name="replyUserData">This will be used for the mpv_event.replyUserData field for the received MPV_EVENT_PROPERTY_CHANGE events. (Also see section about asynchronous calls, although this function is somewhat different from actual asynchronous calls.) If you have no use for this, pass 0. Also see mpv_unobserve_property().</param>
    /// <param name="name">The property name.</param>
    /// <param name="format">see enum mpv_format. Can be MPV_FORMAT_NONE to omit values from the change events.</param>
    /// <returns>error code (usually fails only on OOM or unsupported format)</returns>
    public static int ObserveProperty(MpvHandle* mpv, ulong replyUserData, string name, MpvFormat format) => MpvBindings.ObserveProperty(mpv, replyUserData, name, format);
    
    /// <summary>Initialize the renderer state. Depending on the backend used, this will access the underlying GPU API and initialize its own objects.</summary>
    /// <param name="res">set to the context (on success) or NULL (on failure). The value is never read and always overwritten.</param>
    /// <param name="mpv">handle used to get the core (the mpv_render_context won't depend on this specific handle, only the core referenced by it)</param>
    /// <param name="params">an array of parameters, terminated by type==0. It's left unspecified what happens with unknown parameters. At least MPV_RENDER_PARAM_API_TYPE is required, and most backends will require another backend-specific parameter.</param>
    /// <returns>error code, including but not limited to: MPV_ERROR_UNSUPPORTED: the OpenGL version is not supported (or required extensions are missing) MPV_ERROR_NOT_IMPLEMENTED: an unknown API type was provided, or support for the requested API was not built in the used libmpv binary. MPV_ERROR_INVALID_PARAMETER: at least one of the provided parameters was not valid.</returns>
    public static int RenderContextCreate(MpvRenderContext** res, MpvHandle* mpv, MpvRenderParam* @params) => MpvBindings.RenderContextCreate(res, mpv, @params);
    
    /// <summary>Destroy the mpv renderer state.</summary>
    /// <param name="ctx">a valid render context. After this function returns, this is not a valid pointer anymore. NULL is also allowed and does nothing.</param>
    public static void RenderContextFree(MpvRenderContext* ctx) => MpvBindings.RenderContextFree(ctx);
    
    /// <summary>Retrieve information from the render context. This is NOT a counterpart to mpv_render_context_set_parameter(), because you generally can't read parameters set with it, and this function is not meant for this purpose. Instead, this is for communicating information from the renderer back to the user. See mpv_render_param_type; entries which support this function explicitly mention it, and for other entries you can assume it will fail.</summary>
    /// <param name="ctx">a valid render context</param>
    /// <param name="param">the parameter type and data that should be retrieved</param>
    /// <returns>error code. If a parameter could actually be retrieved, this returns success, otherwise an error code depending on the parameter type and situation. MPV_ERROR_NOT_IMPLEMENTED is used for unknown param.type, or if retrieving it is not supported.</returns>
    public static int RenderContextGetInfo(MpvRenderContext* ctx, MpvRenderParam param) => MpvBindings.RenderContextGetInfo(ctx, param);
    
    /// <summary>Render video.</summary>
    /// <param name="ctx">a valid render context</param>
    /// <param name="params">an array of parameters, terminated by type==0. Which parameters are required depends on the backend. It's left unspecified what happens with unknown parameters.</param>
    /// <returns>error code</returns>
    public static int RenderContextRender(MpvRenderContext* ctx, MpvRenderParam* @params) => MpvBindings.RenderContextRender(ctx, @params);
    
    /// <summary>Tell the renderer that a frame was flipped at the given time. This is optional, but can help the player to achieve better timing.</summary>
    /// <param name="ctx">a valid render context</param>
    public static void RenderContextReportSwap(MpvRenderContext* ctx) => MpvBindings.RenderContextReportSwap(ctx);
    
    /// <summary>Attempt to change a single parameter. Not all backends and parameter types support all kinds of changes.</summary>
    /// <param name="ctx">a valid render context</param>
    /// <param name="param">the parameter type and data that should be set</param>
    /// <returns>error code. If a parameter could actually be changed, this returns success, otherwise an error code depending on the parameter type and situation.</returns>
    public static int RenderContextSetParameter(MpvRenderContext* ctx, MpvRenderParam param) => MpvBindings.RenderContextSetParameter(ctx, param);
    
    /// <summary>Set the callback that notifies you when a new video frame is available, or if the video display configuration somehow changed and requires a redraw. Similar to mpv_set_wakeup_callback(), you must not call any mpv API from the callback, and all the other listed restrictions apply (such as not exiting the callback by throwing exceptions).</summary>
    /// <param name="callback">callback(callbackCtx) is called if the frame should be redrawn</param>
    /// <param name="callbackCtx">opaque argument to the callback</param>
    public static void RenderContextSetUpdateCallback(MpvRenderContext* ctx, MpvRenderContextSetUpdateCallbackCallbackFunc callback, void* callbackCtx) => MpvBindings.RenderContextSetUpdateCallback(ctx, callback, callbackCtx);
    
    /// <summary>The API user is supposed to call this when the update callback was invoked (like all mpv_render_* functions, this has to happen on the render thread, and _not_ from the update callback itself).</summary>
    /// <returns>a bitset of mpv_render_update_flag values (i.e. multiple flags are combined with bitwise or). Typically, this will tell the API user what should happen next. E.g. if the MPV_RENDER_UPDATE_FRAME flag is set, mpv_render_context_render() should be called. If flags unknown to the API user are set, or if the return value is 0, nothing needs to be done.</returns>
    public static ulong RenderContextUpdate(MpvRenderContext* ctx) => MpvBindings.RenderContextUpdate(ctx);
    
    /// <summary>Enable or disable the given event.</summary>
    /// <param name="event">See enum mpv_event_id.</param>
    /// <param name="enable">1 to enable receiving this event, 0 to disable it.</param>
    /// <returns>error code</returns>
    public static int RequestEvent(MpvHandle* ctx, MpvEventId @event, int enable) => MpvBindings.RequestEvent(ctx, @event, enable);
    
    /// <summary>Enable or disable receiving of log messages. These are the messages the command line player prints to the terminal. This call sets the minimum required log level for a message to be received with MPV_EVENT_LOG_MESSAGE.</summary>
    /// <param name="minLevel">Minimal log level as string. Valid log levels: no fatal error warn info v debug trace The value &quot;no&quot; disables all messages. This is the default. An exception is the value &quot;terminal-default&quot;, which uses the log level as set by the &quot;--msg-level&quot; option. This works even if the terminal is disabled. (Since API version 1.19.) Also see mpv_log_level.</param>
    /// <returns>error code</returns>
    public static int RequestLogMessages(MpvHandle* ctx, string minLevel) => MpvBindings.RequestLogMessages(ctx, minLevel);
    
    /// <summary>Set an option. Note that you can't normally set options during runtime. It works in uninitialized state (see mpv_create()), and in some cases in at runtime.</summary>
    /// <param name="name">Option name. This is the same as on the mpv command line, but without the leading &quot;--&quot;.</param>
    /// <param name="format">see enum mpv_format.</param>
    /// <param name="data">Option value (according to the format).</param>
    /// <returns>error code</returns>
    public static int SetOption(MpvHandle* ctx, string name, MpvFormat format, void* data) => MpvBindings.SetOption(ctx, name, format, data);
    
    /// <summary>Convenience function to set an option to a string value. This is like calling mpv_set_option() with MPV_FORMAT_STRING.</summary>
    /// <returns>error code</returns>
    public static int SetOptionString(MpvHandle* ctx, string name, string data) => MpvBindings.SetOptionString(ctx, name, data);
    
    /// <summary>Set a property to a given value. Properties are essentially variables which can be queried or set at runtime. For example, writing to the pause property will actually pause or unpause playback.</summary>
    /// <param name="name">The property name. See input.rst for a list of properties.</param>
    /// <param name="format">see enum mpv_format.</param>
    /// <param name="data">Option value.</param>
    /// <returns>error code</returns>
    public static int SetProperty(MpvHandle* ctx, string name, MpvFormat format, void* data) => MpvBindings.SetProperty(ctx, name, format, data);
    
    /// <summary>Set a property asynchronously. You will receive the result of the operation as MPV_EVENT_SET_PROPERTY_REPLY event. The mpv_event.error field will contain the result status of the operation. Otherwise, this function is similar to mpv_set_property().</summary>
    /// <param name="replyUserData">see section about asynchronous calls</param>
    /// <param name="name">The property name.</param>
    /// <param name="format">see enum mpv_format.</param>
    /// <param name="data">Option value. The value will be copied by the function. It will never be modified by the client API.</param>
    /// <returns>error code if sending the request failed</returns>
    public static int SetPropertyAsync(MpvHandle* ctx, ulong replyUserData, string name, MpvFormat format, void* data) => MpvBindings.SetPropertyAsync(ctx, replyUserData, name, format, data);
    
    /// <summary>Convenience function to set a property to a string value.</summary>
    public static int SetPropertyString(MpvHandle* ctx, string name, string data) => MpvBindings.SetPropertyString(ctx, name, data);
    
    /// <summary>Set a custom function that should be called when there are new events. Use this if blocking in mpv_wait_event() to wait for new events is not feasible.</summary>
    /// <param name="cb">function that should be called if a wakeup is required</param>
    /// <param name="d">arbitrary userdata passed to cb</param>
    public static void SetWakeupCallback(MpvHandle* ctx, MpvSetWakeupCallbackCbFunc cb, void* d) => MpvBindings.SetWakeupCallback(ctx, cb, d);
    
    /// <summary>Add a custom stream protocol. This will register a protocol handler under the given protocol prefix, and invoke the given callbacks if an URI with the matching protocol prefix is opened.</summary>
    /// <param name="protocol">protocol prefix, for example &quot;foo&quot; for &quot;foo://&quot; URIs</param>
    /// <param name="userData">opaque pointer passed into the mpv_stream_cb_open_fn callback.</param>
    /// <returns>error code</returns>
    public static int StreamCbAddRo(MpvHandle* ctx, string protocol, void* userData, MpvStreamCbAddRoOpenFnFunc openFn) => MpvBindings.StreamCbAddRo(ctx, protocol, userData, openFn);
    
    /// <summary>Similar to mpv_destroy(), but brings the player and all clients down as well, and waits until all of them are destroyed. This function blocks. The advantage over mpv_destroy() is that while mpv_destroy() merely detaches the client handle from the player, this function quits the player, waits until all other clients are destroyed (i.e. all mpv_handles are detached), and also waits for the final termination of the player.</summary>
    public static void TerminateDestroy(MpvHandle* ctx) => MpvBindings.TerminateDestroy(ctx);
    
    /// <summary>Undo mpv_observe_property(). This will remove all observed properties for which the given number was passed as replyUserData to mpv_observe_property.</summary>
    /// <param name="registeredReplyUserData">ID that was passed to mpv_observe_property</param>
    /// <returns>negative value is an error code, &gt;=0 is number of removed properties on success (includes the case when 0 were removed)</returns>
    public static int UnobserveProperty(MpvHandle* mpv, ulong registeredReplyUserData) => MpvBindings.UnobserveProperty(mpv, registeredReplyUserData);
    
    /// <summary>Block until all asynchronous requests are done. This affects functions like mpv_command_async(), which return immediately and return their result as events.</summary>
    public static void WaitAsyncRequests(MpvHandle* ctx) => MpvBindings.WaitAsyncRequests(ctx);
    
    /// <summary>Wait for the next event, or until the timeout expires, or if another thread makes a call to mpv_wakeup(). Passing 0 as timeout will never wait, and is suitable for polling.</summary>
    /// <param name="timeout">Timeout in seconds, after which the function returns even if no event was received. A MPV_EVENT_NONE is returned on timeout. A value of 0 will disable waiting. Negative values will wait with an infinite timeout.</param>
    /// <returns>A struct containing the event ID and other data. The pointer (and fields in the struct) stay valid until the next mpv_wait_event() call, or until the mpv_handle is destroyed. You must not write to the struct, and all memory referenced by it will be automatically released by the API on the next mpv_wait_event() call, or when the context is destroyed. The return value is never NULL.</returns>
    public static MpvEvent* WaitEvent(MpvHandle* ctx, double timeout) => MpvBindings.WaitEvent(ctx, timeout);
    
    /// <summary>Interrupt the current mpv_wait_event() call. This will wake up the thread currently waiting in mpv_wait_event(). If no thread is waiting, the next mpv_wait_event() call will return immediately (this is to avoid lost wake-ups).</summary>
    public static void Wakeup(MpvHandle* ctx) => MpvBindings.Wakeup(ctx);
}
