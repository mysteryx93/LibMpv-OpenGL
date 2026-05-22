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
- libmpv 0.40.0 or greater

Native libmpv binaries for Windows, macOS.

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

## Getting the Native libmpv Libraries

LibMpv requires native libmpv 0.40.0 binaries. Note that mpv does not publish official prebuilt packages — the options below are community-maintained builds.

**Windows**

Download a prebuilt `libmpv-2.dll` from one of these community build sources:

- [shinchiro/mpv-winbuild-cmake](https://github.com/shinchiro/mpv-winbuild-cmake/releases) — download `mpv-dev-x86_64-<date>.7z`, extract and place `libmpv-2.dll` alongside your application.
- [zhongfly/mpv-winbuild](https://github.com/zhongfly/mpv-winbuild/releases) — alternative CI builds; `mpv-dev-x86_64-<date>.7z` contains the same `libmpv-2.dll`.

**macOS**

Install via Homebrew (requires macOS 11 or later):

```bash
brew install mpv
```

The dylib will be available at `/opt/homebrew/lib/libmpv.dylib` (Apple Silicon) or `/usr/local/lib/libmpv.dylib` (Intel).

**Linux (Ubuntu/Debian)**

The version of `libmpv` in the standard Ubuntu repositories may be older than 0.40.0. To get 0.40.0, use the unofficial PPA:

```bash
sudo add-apt-repository ppa:ubuntuhandbook1/mpv
sudo apt update
sudo apt install libmpv-dev
```

For other distros, install via your package manager (e.g. `dnf install mpv-libs-devel` on Fedora) or build from source using [mpv-build](https://github.com/mpv-player/mpv-build).

## Sample Project

A sample application is included and has been tested on Windows, Linux, and macOS.

## License

This project is licensed under the [MIT License](LICENSE).

Originally created by Etienne Charland (mysteryx93) and Vadim Beloborodov (homov).

Maintained by Jeff Baxter (warden-vt).