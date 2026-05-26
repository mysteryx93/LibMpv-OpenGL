using Avalonia.Media;
using HanumanInstitute.LibMpv;
using HanumanInstitute.LibMpv.Avalonia;
using ReactiveUI;
using ReactiveUI.SourceGenerators;

namespace Sample.LibMpv.Avalonia.ViewModels;

public partial class MainViewModel : ReactiveObject
{
    public MpvContext? Mpv { get; set; }

    [Reactive]
    public partial VideoRenderer Renderer { get; set; }

    public VideoRenderer[] RendererOptions { get; } = Enum.GetValues<VideoRenderer>();

    string _mediaUrl = "https://github.com/vidanov/video/raw/master/test_files/1080p50audio.mp4";
    public string MediaUrl
    {
        get => _mediaUrl;
        set { this.RaiseAndSetIfChanged(ref _mediaUrl, value); }
    }

    private MpvOverlay? _overlay;
    private bool _overlayEnabled;

    public string OverlayButtonText => _overlayEnabled ? "Overlay: ON" : "Overlay: OFF";

    public async void Play()
    {
        try
        {
            Stop();

            if (Mpv != null)
            {
                await Mpv.LoadFile(MediaUrl).InvokeAsync();
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Play failed: + {ex.Message}");
        }
    }

    public void Pause() => Pause(null);

    public void Pause(bool? value)
    {
        if (Mpv != null)
        {
            value ??= !Mpv.Pause.Get()!;
            Mpv.Pause.Set(value.Value);
        }
    }

    public void Stop()
    {
        DisableOverlay();
        if (Mpv != null)
        {
            Mpv.Stop().Invoke();
            Mpv.Pause.Set(false);
        }
    }

    public void ToggleOverlay()
    {
        if (Mpv == null) return;

        if (_overlayEnabled)
            DisableOverlay();
        else
            EnableOverlay();
    }

    private void EnableOverlay()
    {
        if (Mpv == null || _overlayEnabled) return;
        _overlayEnabled = true;
        _overlay = new MpvOverlay(Mpv);
        Mpv.PreRender += OnTick;
        this.RaisePropertyChanged(nameof(OverlayButtonText));
    }

    private void DisableOverlay()
    {
        if (!_overlayEnabled) return;
        _overlayEnabled = false;
        if (Mpv != null) Mpv.PreRender -= OnTick;
        _overlay?.Dispose();
        _overlay = null;
        this.RaisePropertyChanged(nameof(OverlayButtonText));
    }

    private static readonly Typeface _overlayTypeface = new("Arial");

    private void OnTick(object? sender, EventArgs e)
    {
        try
        {
            if (_overlay == null || Mpv == null) return;

            var width = Mpv.Width.Get() ?? 0;
            var height = Mpv.Height.Get() ?? 0;
            if (width <= 0 || height <= 0) return;

            var seconds = Mpv.PlaybackTime.Get() ?? 0;
            var ts = TimeSpan.FromSeconds(seconds);
            var label = $"{(int)ts.TotalHours:D2}:{ts.Minutes:D2}:{ts.Seconds:D2}.{ts.Milliseconds:D1}";

            const int overlayW = 160;
            const int overlayH = 32;

            _overlay.Show(width / 2 - overlayW / 2, height / 2 - overlayH / 2, overlayW, overlayH, ctx =>
            {
                ctx.FillRectangle(new SolidColorBrush(Color.FromArgb(180, 0, 0, 0)), new Rect(0, 0, overlayW, overlayH));
                var text = new FormattedText(label, System.Globalization.CultureInfo.InvariantCulture,
                    FlowDirection.LeftToRight, _overlayTypeface, 20, Brushes.White);
                ctx.DrawText(text, new Point(overlayW / 2.0 - text.Width / 2, overlayH / 2.0 - text.Height / 2));
            });
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"OnTick overlay error: {ex}");
        }
    }

    public void FrameStepForward() => Mpv?.FrameStep().Invoke();
    public void FrameStepBack() => Mpv?.FrameBackStep().Invoke();

    public void Software() => Renderer = VideoRenderer.Software;
    public void OpenGl() => Renderer = VideoRenderer.OpenGl;
    public void Native() => Renderer = VideoRenderer.Native;
}
