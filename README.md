# LibMpv for .NET (with OpenGL)

Cross-platform MPV video player for .NET and Avalonia with OpenGL implementation.

Supported renderers: Software, OpenGL, Native (Desktop & Android).

**Work in progress**

### LibMpv.Avalonia

Avalonia implementation. Place `MpvVideoView` in your view.

You can access the MpvContext in your ViewModel and set the Renderer method by binding it like this. 

    <mpv:MpvVideoView MpvContext="{Binding Mpv}" Renderer="{Binding Renderer}" />

MpvContext provides access to all MPV features.

### Sample.LibMpv.Avalonia

Sample project tested on Windows, Linux and Android.

Android MPV binaries are taken from the project https://github.com/mpv-android/mpv-android

### License

This project is under [MIT license](LICENSE).

By Etienne Charland (mysteryx93) and Vadim Beloborodov (homov).