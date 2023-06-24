# LibMpv for .NET (with OpenGL)

Cross-platform MPV video player for .NET and Avalonia with OpenGL implementation.

Supported renderers: Software, OpenGL, Native (Desktop & Android).

## LibMpv

MPV API implemented in 3 layers:

- MpvApi: static API invokes
- MpvContextBase: .NET-friendly functions
- MpvContext: all commands, properties and options exposed in a strongly-typed way

LibMpv targets `netstandard2.0` and can be used with any UI.

## LibMpv.Avalonia

Avalonia implementation. Place `MpvVideoView` in your view.

You can access the MpvContext in your ViewModel by binding it like this.

    <mpv:MpvVideoView MpvContext="{Binding Mpv}" />

MpvContext provides access to all MPV features.

Default renderer is OpenGL for Linux and MacOS, Native for Windows, and a different Native implementation for Android.

## Sample.LibMpv.Avalonia

Sample project tested on Windows, Linux and Android.

Android MPV binaries are taken from the project https://github.com/mpv-android/mpv-android

## Contributions wanted!

As I will not be using these features myself for a while, someone else will need to complete the work or it may sit in the current state for a long time.

TODO:

- [Improve Native implementation for Windows](https://github.com/mysteryx93/MediaPlayerUI.NET/issues/7#issuecomment-1602399799)
- Android version works in project "AndroidSample" but not in "Sample.LibMpv.Avalonia". Once the main sample works, "AndroidSample" can be removed.
- Android version crashes when switching app.
- MpvContext: async property get doesn't work on string but works for other data types
- MpvContext: properties works on basic data types but more complex types need to be implemented and tested
- MpvContext: all properties and commands need to be properly tested. See unit test project. 
- Test on MacOS
- Compile MPV for iOS
- Implement for iOS

## License

This project is under [MIT license](LICENSE).

By Etienne Charland (mysteryx93) and Vadim Beloborodov (homov).