namespace LibMpv.Avalonia;

public interface IVideoView : IDisposable
{
    MpvContext? MpvContext { get; }
}
