using System;
using Android.Content;
using Android.Graphics;
using Android.Runtime;
using Android.Views;
using Avalonia;
using Avalonia.Android;
using Avalonia.Controls;
using Avalonia.Data;
using Avalonia.Platform;
using HanumanInstitute.LibMpv;

namespace Sample.LibMpv.Avalonia.Android.Views;

public class NativeVideoView : NativeControlHost
{
    public class MpvSurfaceView : SurfaceView, ISurfaceHolderCallback
    {
        MpvContextBase? _mpvContext = null;

        private IntPtr _nativeHandle = IntPtr.Zero;

        public IntPtr NativeHandle { get { return _nativeHandle; } }

        public MpvSurfaceView(Context context):base(context)
        {
            this.SetZOrderOnTop(true);
            Holder.AddCallback(this);
        }

        public void SurfaceChanged(ISurfaceHolder holder, [GeneratedEnum] Format format, int width, int height)
        {
            if (_mpvContext!=null)
            {
                _mpvContext.SetPropertyString("android-surface-size",$"{width}x{height}");
            }
        }

        public void SurfaceCreated(ISurfaceHolder holder)
        {
            _nativeHandle = holder.Surface.Handle;
            if (_mpvContext != null)
                Attach(_mpvContext);
        }

        public void SurfaceDestroyed(ISurfaceHolder holder)
        {
            Detach();
            if (_nativeHandle != IntPtr.Zero)
            {
                _nativeHandle = IntPtr.Zero;
            }
        }

        public void Attach(MpvContextBase mpvContext)
        {
            _mpvContext = mpvContext;
            if (NativeHandle != IntPtr.Zero )
                _mpvContext.StartNativeRendering(NativeHandle);
        }

        public void Detach()
        {
            if (_mpvContext != null)
                _mpvContext.StopRendering();
            _mpvContext = null;
        }
    }

    private MpvSurfaceView? _mpvSurfaceView = null;
    
    private MpvContextBase? _mpvContext = null;

    public static readonly DirectProperty<NativeVideoView, MpvContextBase?> MpvContextProperty =
           AvaloniaProperty.RegisterDirect<NativeVideoView, MpvContextBase?>(
               nameof(MpvContext),
               o => o.MpvContext,
               (o, v) => o.MpvContext = v,
               defaultBindingMode: BindingMode.TwoWay);


    public MpvContextBase? MpvContext
    {
        get { return _mpvContext; }
        set
        {
            if (ReferenceEquals(value, _mpvContext)) return;
            _mpvContext?.StopRendering();
            _mpvContext = value;
            if (_mpvSurfaceView != null)
                _mpvSurfaceView?.Attach(_mpvContext);
        }
    }

    protected override IPlatformHandle CreateNativeControlCore(IPlatformHandle parent)
    {
        var parentContext = (parent as AndroidViewControlHandle)?.View.Context
            ?? global::Android.App.Application.Context;

        _mpvSurfaceView = new MpvSurfaceView(parentContext);
        

        if (_mpvContext != null) _mpvSurfaceView.Attach(_mpvContext);

        return new AndroidViewControlHandle(_mpvSurfaceView);
    }

    protected override void DestroyNativeControlCore(IPlatformHandle control)
    {
        _mpvSurfaceView?.Detach();
        _mpvSurfaceView = null;
        base.DestroyNativeControlCore(control);
    }
}
