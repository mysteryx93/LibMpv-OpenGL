using System.Security;

namespace HanumanInstitute.LibMpv.Api;

internal static unsafe class MpvBindings
{
    private static IFunctionResolver FunctionResolver => _functionResolver ??= FunctionResolverFactory.Create();
    private static IFunctionResolver? _functionResolver;

    private static T GetFunction<T>(string functionName)
        where T : Delegate =>
        FunctionResolver.GetFunctionDelegate<T>(Mpv.DllName, functionName, true) ?? throw new NotSupportedException();
    
    [SuppressUnmanagedCodeSecurity]
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void AbortAsyncCommandDelegate(MpvHandle* ctx, ulong replyUserData);
    public static AbortAsyncCommandDelegate AbortAsyncCommand => _abortAsyncCommandDelegate ??= GetFunction<AbortAsyncCommandDelegate>("mpv_abort_async_command");
    private static AbortAsyncCommandDelegate? _abortAsyncCommandDelegate;
    
    [SuppressUnmanagedCodeSecurity]
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate ulong ClientApiVersionDelegate();
    public static ClientApiVersionDelegate ClientApiVersion => _clientApiVersion ??= GetFunction<ClientApiVersionDelegate>("mpv_client_api_version");
    private static ClientApiVersionDelegate? _clientApiVersion;
    
    [SuppressUnmanagedCodeSecurity]
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate long ClientIdDelegate(MpvHandle* ctx);
    public static ClientIdDelegate ClientId => _clientId ??= GetFunction<ClientIdDelegate>("mpv_client_id");
    private static ClientIdDelegate? _clientId;
    
    [SuppressUnmanagedCodeSecurity]
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(ConstCharPtrMarshaler))]
    public delegate string ClientNameDelegate(MpvHandle* ctx);
    public static ClientNameDelegate ClientName => _clientName ??= GetFunction<ClientNameDelegate>("mpv_client_name");
    private static ClientNameDelegate? _clientName;
    
    [SuppressUnmanagedCodeSecurity]
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int CommandDelegate(MpvHandle* ctx, byte** args);
    public static CommandDelegate Command => _command ??= GetFunction<CommandDelegate>("mpv_command");
    private static CommandDelegate? _command;
    
    [SuppressUnmanagedCodeSecurity]
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int CommandAsyncDelegate(MpvHandle* ctx, ulong replyUserData, byte** args);
    public static CommandAsyncDelegate CommandAsync => _commandAsync ??= GetFunction<CommandAsyncDelegate>("mpv_command_async");
    private static CommandAsyncDelegate? _commandAsync;
    
    [SuppressUnmanagedCodeSecurity]
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int CommandNodeDelegate(MpvHandle* ctx, MpvNode* args, MpvNode* result);
    public static CommandNodeDelegate CommandNode => _commandNode ??= GetFunction<CommandNodeDelegate>("mpv_command_node");
    private static CommandNodeDelegate? _commandNode;
    
    [SuppressUnmanagedCodeSecurity]
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int CommandNodeAsyncDelegate(MpvHandle* ctx, ulong replyUserData, MpvNode* args);
    public static CommandNodeAsyncDelegate CommandNodeAsync => _commandNodeAsync ??= GetFunction<CommandNodeAsyncDelegate>("mpv_command_node_async");
    private static CommandNodeAsyncDelegate? _commandNodeAsync;
    
    [SuppressUnmanagedCodeSecurity]
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int CommandRetDelegate(MpvHandle* ctx, byte** args, MpvNode* result);
    public static CommandRetDelegate CommandRet => _commandRet ??= GetFunction<CommandRetDelegate>("mpv_command_ret");
    private static CommandRetDelegate? _commandRet;
    
    [SuppressUnmanagedCodeSecurity]
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int CommandStringDelegate(MpvHandle* ctx,     
    #if NETSTANDARD2_1_OR_GREATER
    [MarshalAs(UnmanagedType.LPUTF8Str)]
    #else
    [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(Utf8Marshaler))]
    #endif
    string args);
    public static CommandStringDelegate CommandString => _commandString ??= GetFunction<CommandStringDelegate>("mpv_command_string");
    private static CommandStringDelegate? _commandString;
    
    [SuppressUnmanagedCodeSecurity]
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate MpvHandle* CreateDelegate();
    public static CreateDelegate Create => _create ??= GetFunction<CreateDelegate>("mpv_create");
    private static CreateDelegate? _create;
    
    [SuppressUnmanagedCodeSecurity]
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate MpvHandle* CreateClientDelegate(MpvHandle* ctx,     
    #if NETSTANDARD2_1_OR_GREATER
    [MarshalAs(UnmanagedType.LPUTF8Str)]
    #else
    [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(Utf8Marshaler))]
    #endif
    string name);
    public static CreateClientDelegate CreateClient => _createClient ??= GetFunction<CreateClientDelegate>("mpv_create_client");
    private static CreateClientDelegate? _createClient;
    
    [SuppressUnmanagedCodeSecurity]
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate MpvHandle* CreateWeakClientDelegate(MpvHandle* ctx,     
    #if NETSTANDARD2_1_OR_GREATER
    [MarshalAs(UnmanagedType.LPUTF8Str)]
    #else
    [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(Utf8Marshaler))]
    #endif
    string name);
    public static CreateWeakClientDelegate CreateWeakClient => _createWeakClient ??= GetFunction<CreateWeakClientDelegate>("mpv_create_weak_client");
    private static CreateWeakClientDelegate? _createWeakClient;
    
    [SuppressUnmanagedCodeSecurity]
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int DelPropertyDelegate(MpvHandle* ctx,     
    #if NETSTANDARD2_1_OR_GREATER
    [MarshalAs(UnmanagedType.LPUTF8Str)]
    #else
    [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(Utf8Marshaler))]
    #endif
    string name);
    public static DelPropertyDelegate DelProperty => _delProperty ??= GetFunction<DelPropertyDelegate>("mpv_del_property");
    private static DelPropertyDelegate? _delProperty;
    
    [SuppressUnmanagedCodeSecurity]
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void DestroyDelegate(MpvHandle* ctx);
    public static DestroyDelegate Destroy => _destroy ??= GetFunction<DestroyDelegate>("mpv_destroy");
    private static DestroyDelegate? _destroy;
    
    [SuppressUnmanagedCodeSecurity]
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(ConstCharPtrMarshaler))]
    public delegate string ErrorStringDelegate(int error);
    public static ErrorStringDelegate ErrorString => _errorString ??= GetFunction<ErrorStringDelegate>("mpv_error_string");
    private static ErrorStringDelegate? _errorString;
    
    [SuppressUnmanagedCodeSecurity]
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(ConstCharPtrMarshaler))]
    public delegate string EventNameDelegate(MpvEventId @event);
    public static EventNameDelegate EventName => _eventName ??= GetFunction<EventNameDelegate>("mpv_event_name");
    private static EventNameDelegate? _eventName;
    
    [SuppressUnmanagedCodeSecurity]
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int EventToNodeDelegate(MpvNode* dst, MpvEvent* src);
    public static EventToNodeDelegate EventToNode => _eventToNode ??= GetFunction<EventToNodeDelegate>("mpv_event_to_node");
    private static EventToNodeDelegate? _eventToNode;
    
    [SuppressUnmanagedCodeSecurity]
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void FreeDelegate(void* data);
    public static FreeDelegate Free => _free ??= GetFunction<FreeDelegate>("mpv_free");
    private static FreeDelegate? _free;
    
    [SuppressUnmanagedCodeSecurity]
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void FreeNodeContentsDelegate(MpvNode* node);
    public static FreeNodeContentsDelegate FreeNodeContents => _freeNodeContents ??= GetFunction<FreeNodeContentsDelegate>("mpv_free_node_contents");
    private static FreeNodeContentsDelegate? _freeNodeContents;
    
    [SuppressUnmanagedCodeSecurity]
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int GetPropertyDelegate(MpvHandle* ctx,     
    #if NETSTANDARD2_1_OR_GREATER
    [MarshalAs(UnmanagedType.LPUTF8Str)]
    #else
    [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(Utf8Marshaler))]
    #endif
    string name, MpvFormat format, void* data);
    public static GetPropertyDelegate GetProperty => _getProperty ??= GetFunction<GetPropertyDelegate>("mpv_get_property");
    private static GetPropertyDelegate? _getProperty;
    
    [SuppressUnmanagedCodeSecurity]
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int GetPropertyAsyncDelegate(MpvHandle* ctx, ulong replyUserData,     
    #if NETSTANDARD2_1_OR_GREATER
    [MarshalAs(UnmanagedType.LPUTF8Str)]
    #else
    [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(Utf8Marshaler))]
    #endif
    string name, MpvFormat format);
    public static GetPropertyAsyncDelegate GetPropertyAsync => _getPropertyAsync ??= GetFunction<GetPropertyAsyncDelegate>("mpv_get_property_async");
    private static GetPropertyAsyncDelegate? _getPropertyAsync;
    
    [SuppressUnmanagedCodeSecurity]
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate byte* GetPropertyOsdStringDelegate(MpvHandle* ctx,     
    #if NETSTANDARD2_1_OR_GREATER
    [MarshalAs(UnmanagedType.LPUTF8Str)]
    #else
    [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(Utf8Marshaler))]
    #endif
    string name);
    public static GetPropertyOsdStringDelegate GetPropertyOsdString => _getPropertyOsdString ??= GetFunction<GetPropertyOsdStringDelegate>("mpv_get_property_osd_string");
    private static GetPropertyOsdStringDelegate? _getPropertyOsdString;
    
    [SuppressUnmanagedCodeSecurity]
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate byte* GetPropertyStringDelegate(MpvHandle* ctx,     
    #if NETSTANDARD2_1_OR_GREATER
    [MarshalAs(UnmanagedType.LPUTF8Str)]
    #else
    [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(Utf8Marshaler))]
    #endif
    string name);
    public static GetPropertyStringDelegate GetPropertyString => _getPropertyString ??= GetFunction<GetPropertyStringDelegate>("mpv_get_property_string");
    private static GetPropertyStringDelegate? _getPropertyString;
    
    [SuppressUnmanagedCodeSecurity]
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate long GetTimeUsDelegate(MpvHandle* ctx);
    public static GetTimeUsDelegate GetTimeUs => _getTimeUs ??= GetFunction<GetTimeUsDelegate>("mpv_get_time_us");
    private static GetTimeUsDelegate? _getTimeUs;
    
    [SuppressUnmanagedCodeSecurity]
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int GetWakeupPipeDelegate(MpvHandle* ctx);
    public static GetWakeupPipeDelegate GetWakeupPipe => _getWakeupPipe ??= GetFunction<GetWakeupPipeDelegate>("mpv_get_wakeup_pipe");
    private static GetWakeupPipeDelegate? _getWakeupPipe;
    
    [SuppressUnmanagedCodeSecurity]
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int HookAddDelegate(MpvHandle* ctx, ulong replyUserData,     
    #if NETSTANDARD2_1_OR_GREATER
    [MarshalAs(UnmanagedType.LPUTF8Str)]
    #else
    [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(Utf8Marshaler))]
    #endif
    string name, int priority);
    public static HookAddDelegate HookAdd => _hookAdd ??= GetFunction<HookAddDelegate>("mpv_hook_add");
    private static HookAddDelegate? _hookAdd;
    
    [SuppressUnmanagedCodeSecurity]
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int HookContinueDelegate(MpvHandle* ctx, ulong id);
    public static HookContinueDelegate HookContinue => _hookContinue ??= GetFunction<HookContinueDelegate>("mpv_hook_continue");
    private static HookContinueDelegate? _hookContinue;
    
    [SuppressUnmanagedCodeSecurity]
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int InitializeDelegate(MpvHandle* ctx);
    public static InitializeDelegate Initialize => _initialize ??= GetFunction<InitializeDelegate>("mpv_initialize");
    private static InitializeDelegate? _initialize;
    
    [SuppressUnmanagedCodeSecurity]
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int LoadConfigFileDelegate(MpvHandle* ctx,     
    #if NETSTANDARD2_1_OR_GREATER
    [MarshalAs(UnmanagedType.LPUTF8Str)]
    #else
    [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(Utf8Marshaler))]
    #endif
    string filename);
    public static LoadConfigFileDelegate LoadConfigFile => _loadConfigFile ??= GetFunction<LoadConfigFileDelegate>("mpv_load_config_file");
    private static LoadConfigFileDelegate? _loadConfigFile;
    
    [SuppressUnmanagedCodeSecurity]
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int ObservePropertyDelegate(MpvHandle* mpv, ulong replyUserData,     
    #if NETSTANDARD2_1_OR_GREATER
    [MarshalAs(UnmanagedType.LPUTF8Str)]
    #else
    [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(Utf8Marshaler))]
    #endif
    string name, MpvFormat format);
    public static ObservePropertyDelegate ObserveProperty => _observeProperty ??= GetFunction<ObservePropertyDelegate>("mpv_observe_property");
    private static ObservePropertyDelegate? _observeProperty;
    
    [SuppressUnmanagedCodeSecurity]
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int RenderContextCreateDelegate(MpvRenderContext** res, MpvHandle* mpv, MpvRenderParam* @params);
    public static RenderContextCreateDelegate RenderContextCreate => _renderContextCreate ??= GetFunction<RenderContextCreateDelegate>("mpv_render_context_create");
    private static RenderContextCreateDelegate? _renderContextCreate;
    
    [SuppressUnmanagedCodeSecurity]
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void RenderContextFreeDelegate(MpvRenderContext* ctx);
    public static RenderContextFreeDelegate RenderContextFree => _renderContextFree ??= GetFunction<RenderContextFreeDelegate>("mpv_render_context_free");
    private static RenderContextFreeDelegate? _renderContextFree;
    
    [SuppressUnmanagedCodeSecurity]
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int RenderContextGetInfoDelegate(MpvRenderContext* ctx, MpvRenderParam param);
    public static RenderContextGetInfoDelegate RenderContextGetInfo => _renderContextGetInfo ??= GetFunction<RenderContextGetInfoDelegate>("mpv_render_context_get_info");
    private static RenderContextGetInfoDelegate? _renderContextGetInfo;
    
    [SuppressUnmanagedCodeSecurity]
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int RenderContextRenderDelegate(MpvRenderContext* ctx, MpvRenderParam* @params);
    public static RenderContextRenderDelegate RenderContextRender => _renderContextRender ??= GetFunction<RenderContextRenderDelegate>("mpv_render_context_render");
    private static RenderContextRenderDelegate? _renderContextRender;
    
    [SuppressUnmanagedCodeSecurity]
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void RenderContextReportSwapDelegate(MpvRenderContext* ctx);
    public static RenderContextReportSwapDelegate RenderContextReportSwap => _renderContextReportSwap ??= GetFunction<RenderContextReportSwapDelegate>("mpv_render_context_report_swap");
    private static RenderContextReportSwapDelegate? _renderContextReportSwap;
    
    [SuppressUnmanagedCodeSecurity]
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int RenderContextSetParameterDelegate(MpvRenderContext* ctx, MpvRenderParam param);
    public static RenderContextSetParameterDelegate RenderContextSetParameter => _renderContextSetParameter ??= GetFunction<RenderContextSetParameterDelegate>("mpv_render_context_set_parameter");
    private static RenderContextSetParameterDelegate? _renderContextSetParameter;
    
    [SuppressUnmanagedCodeSecurity]
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void RenderContextSetUpdateCallbackDelegate(MpvRenderContext* ctx, MpvRenderContextSetUpdateCallbackCallbackFunc callback, void* callbackCtx);
    public static RenderContextSetUpdateCallbackDelegate RenderContextSetUpdateCallback => _renderContextSetUpdateCallback ??= GetFunction<RenderContextSetUpdateCallbackDelegate>("mpv_render_context_set_update_callback");
    private static RenderContextSetUpdateCallbackDelegate? _renderContextSetUpdateCallback;
    
    [SuppressUnmanagedCodeSecurity]
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate ulong RenderContextUpdateDelegate(MpvRenderContext* ctx);
    public static RenderContextUpdateDelegate RenderContextUpdate => _renderContextUpdate ??= GetFunction<RenderContextUpdateDelegate>("mpv_render_context_update");
    private static RenderContextUpdateDelegate? _renderContextUpdate;
    
    [SuppressUnmanagedCodeSecurity]
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int RequestEventDelegate(MpvHandle* ctx, MpvEventId @event, int enable);
    public static RequestEventDelegate RequestEvent => _requestEvent ??= GetFunction<RequestEventDelegate>("mpv_request_event");
    private static RequestEventDelegate? _requestEvent;
    
    [SuppressUnmanagedCodeSecurity]
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int RequestLogMessagesDelegate(MpvHandle* ctx,     
    #if NETSTANDARD2_1_OR_GREATER
    [MarshalAs(UnmanagedType.LPUTF8Str)]
    #else
    [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(Utf8Marshaler))]
    #endif
    string minLevel);
    public static RequestLogMessagesDelegate RequestLogMessages => _requestLogMessages ??= GetFunction<RequestLogMessagesDelegate>("mpv_request_log_messages");
    private static RequestLogMessagesDelegate? _requestLogMessages;
    
    [SuppressUnmanagedCodeSecurity]
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int SetOptionDelegate(MpvHandle* ctx,     
    #if NETSTANDARD2_1_OR_GREATER
    [MarshalAs(UnmanagedType.LPUTF8Str)]
    #else
    [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(Utf8Marshaler))]
    #endif
    string name, MpvFormat format, void* data);
    public static SetOptionDelegate SetOption => _setOption ??= GetFunction<SetOptionDelegate>("mpv_set_option");
    private static SetOptionDelegate? _setOption;
    
    [SuppressUnmanagedCodeSecurity]
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int SetOptionStringDelegate(MpvHandle* ctx,     
    #if NETSTANDARD2_1_OR_GREATER
    [MarshalAs(UnmanagedType.LPUTF8Str)]
    #else
    [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(Utf8Marshaler))]
    #endif
    string name,     
    #if NETSTANDARD2_1_OR_GREATER
    [MarshalAs(UnmanagedType.LPUTF8Str)]
    #else
    [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(Utf8Marshaler))]
    #endif
    string data);
    public static SetOptionStringDelegate SetOptionString => _setOptionString ??= GetFunction<SetOptionStringDelegate>("mpv_set_option_string");
    private static SetOptionStringDelegate? _setOptionString;
    
    [SuppressUnmanagedCodeSecurity]
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int SetPropertyDelegate(MpvHandle* ctx,     
    #if NETSTANDARD2_1_OR_GREATER
    [MarshalAs(UnmanagedType.LPUTF8Str)]
    #else
    [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(Utf8Marshaler))]
    #endif
    string name, MpvFormat format, void* data);
    public static SetPropertyDelegate SetProperty => _setProperty ??= GetFunction<SetPropertyDelegate>("mpv_set_property");
    private static SetPropertyDelegate? _setProperty;
    
    [SuppressUnmanagedCodeSecurity]
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int SetPropertyAsyncDelegate(MpvHandle* ctx, ulong replyUserData,     
    #if NETSTANDARD2_1_OR_GREATER
    [MarshalAs(UnmanagedType.LPUTF8Str)]
    #else
    [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(Utf8Marshaler))]
    #endif
    string name, MpvFormat format, void* data);
    public static SetPropertyAsyncDelegate SetPropertyAsync => _setPropertyAsync ??= GetFunction<SetPropertyAsyncDelegate>("mpv_set_property_async");
    private static SetPropertyAsyncDelegate? _setPropertyAsync;
    
    [SuppressUnmanagedCodeSecurity]
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int SetPropertyStringDelegate(MpvHandle* ctx,     
    #if NETSTANDARD2_1_OR_GREATER
    [MarshalAs(UnmanagedType.LPUTF8Str)]
    #else
    [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(Utf8Marshaler))]
    #endif
    string name,     
    #if NETSTANDARD2_1_OR_GREATER
    [MarshalAs(UnmanagedType.LPUTF8Str)]
    #else
    [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(Utf8Marshaler))]
    #endif
    string data);
    public static SetPropertyStringDelegate SetPropertyString => _setPropertyString ??= GetFunction<SetPropertyStringDelegate>("mpv_set_property_string");
    private static SetPropertyStringDelegate? _setPropertyString;
    
    [SuppressUnmanagedCodeSecurity]
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void SetWakeupCallbackDelegate(MpvHandle* ctx, MpvSetWakeupCallbackCbFunc cb, void* d);
    public static SetWakeupCallbackDelegate SetWakeupCallback => _setWakeupCallback ??= GetFunction<SetWakeupCallbackDelegate>("mpv_set_wakeup_callback");
    private static SetWakeupCallbackDelegate? _setWakeupCallback;
    
    [SuppressUnmanagedCodeSecurity]
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int StreamCbAddRoDelegate(MpvHandle* ctx,     
    #if NETSTANDARD2_1_OR_GREATER
    [MarshalAs(UnmanagedType.LPUTF8Str)]
    #else
    [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(Utf8Marshaler))]
    #endif
    string protocol, void* userData, MpvStreamCbAddRoOpenFnFunc openFn);
    public static StreamCbAddRoDelegate StreamCbAddRo => _streamCbAddRo ??= GetFunction<StreamCbAddRoDelegate>("mpv_stream_cb_add_ro");
    private static StreamCbAddRoDelegate? _streamCbAddRo;
    
    [SuppressUnmanagedCodeSecurity]
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void TerminateDestroyDelegate(MpvHandle* ctx);
    public static TerminateDestroyDelegate TerminateDestroy => _terminateDestroy ??= GetFunction<TerminateDestroyDelegate>("mpv_terminate_destroy");
    private static TerminateDestroyDelegate? _terminateDestroy;
    
    [SuppressUnmanagedCodeSecurity]
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int UnobservePropertyDelegate(MpvHandle* mpv, ulong registeredReplyUserData);
    public static UnobservePropertyDelegate UnobserveProperty => _unobserveProperty ??= GetFunction<UnobservePropertyDelegate>("mpv_unobserve_property");
    private static UnobservePropertyDelegate? _unobserveProperty;
    
    [SuppressUnmanagedCodeSecurity]
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void WaitAsyncRequestsDelegate(MpvHandle* ctx);
    public static WaitAsyncRequestsDelegate WaitAsyncRequests => _waitAsyncRequests ??= GetFunction<WaitAsyncRequestsDelegate>("mpv_wait_async_requests");
    private static WaitAsyncRequestsDelegate? _waitAsyncRequests;
    
    [SuppressUnmanagedCodeSecurity]
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate MpvEvent* WaitEventDelegate(MpvHandle* ctx, double timeout);
    public static WaitEventDelegate WaitEvent => _waitEvent ??= GetFunction<WaitEventDelegate>("mpv_wait_event");
    private static WaitEventDelegate? _waitEvent;
    
    [SuppressUnmanagedCodeSecurity]
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void WakeupDelegate(MpvHandle* ctx);
    public static WakeupDelegate Wakeup => _wakeup ??= GetFunction<WakeupDelegate>("mpv_wakeup");
    private static WakeupDelegate? _wakeup;
}
