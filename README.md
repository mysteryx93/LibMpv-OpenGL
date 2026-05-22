# LibMpv for .NET

Cross-platform MPV video player library for .NET, with an Avalonia 12 implementation supporting Windows, macOS, and Linux.

## Overview

LibMpv wraps the MPV media player API in a clean, idiomatic .NET interface. It is structured in three layers:

- **MpvApi** — low-level static P/Invoke calls to the native libmpv library
- **MpvContextBase** — .NET-friendly wrappers around the raw API
- **MpvContext** — fully strongly-typed commands, properties, and options

LibMpv targets `netstandard2.0` and can be used independently of any UI framework.

## LibMpv.Avalonia

An Avalonia 12 implementation is provided via the `LibMpv.Avalonia` package. Drop `MpvVideoView` into your view and bind `MpvContext` from your ViewModel:

```xml
<mpv:MpvVideoView MpvContext="{Binding Mpv}" />
```

### Renderers

| Platform | Default Renderer |
|----------|-----------------|
| Windows  | Native          |
| macOS    | OpenGL          |
| Linux    | OpenGL          |

Software rendering is also available as a fallback on all platforms.

## Requirements

- .NET 10 or later
- Avalonia 12

Native libmpv binaries for Windows, macOS, and Linux are bundled with the NuGet packages — no separate installation required.

## Getting Started

1. Install the NuGet packages:

```
dotnet add package LibMpv
dotnet add package LibMpv.Avalonia
```

2. Add `MpvVideoView` to your Avalonia view and bind a `MpvContext` instance from your ViewModel.

3. Use `MpvContext` to control playback:

```csharp
Mpv.LoadFile("path/to/video.mp4");
Mpv.Play();
```

## Sample Project

A sample application is included and has been tested on Windows, Linux, and macOS.

## License

This project is licensed under the [MIT License](LICENSE).

Originally created by Etienne Charland (mysteryx93) and Vadim Beloborodov (homov).

Maintained by Jeff Baxter (warden-vt).
