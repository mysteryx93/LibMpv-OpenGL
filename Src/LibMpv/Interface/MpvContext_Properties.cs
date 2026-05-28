namespace HanumanInstitute.LibMpv;

/// <summary>
/// Exposes MPV's API in a strongly-typed way.
/// </summary>
public partial class MpvContext
{
    /// <summary>
    /// Factor multiplied with speed at which the player attempts to play the file. Usually it's exactly 1. (Display sync mode will make this useful.)
    /// </summary>
    private MpvPropertyRead<float>? _audioSpeedCorrection;
    public MpvPropertyRead<float> AudioSpeedCorrection => _audioSpeedCorrection ??= new(this, "audio-speed-correction");

    /// <summary>
    /// Factor multiplied with speed at which the player attempts to play the file. Usually it's exactly 1. (Display sync mode will make this useful.)
    /// </summary>
    private MpvPropertyRead<float>? _videoSpeedCorrection;
    public MpvPropertyRead<float> VideoSpeedCorrection => _videoSpeedCorrection ??= new(this, "video-speed-correction");

    /// <summary>
    /// Return whether --video-sync=display is actually active.
    /// </summary>
    private MpvPropertyRead<bool>? _displaySyncActive;
    public MpvPropertyRead<bool> DisplaySyncActive => _displaySyncActive ??= new(this, "display-sync-active");

    /// <summary>
    /// Currently played file, with path stripped. If this is an URL, try to undo percent encoding as well. (The result is not necessarily correct, but looks better for display purposes. Use the path property to get an unmodified filename.)
    /// </summary>
    private MpvPropertyReadRef<string>? _fileName;
    public MpvPropertyReadRef<string> FileName => _fileName ??= new(this, "filename");

    /// <summary>
    /// Like the filename property, but if the text contains a ., strip all text after the last .. Usually this removes the file extension.
    /// </summary>
    private MpvPropertyReadString? _fileNameNoExt;
    public MpvPropertyReadString FileNameNoExt => _fileNameNoExt ??= new(this, "filename/no-ext");

    /// <summary>
    /// Length in bytes of the source file/stream. (This is the same as ${stream-end}. For segmented/multi-part files, this will return the size of the main or manifest file, whatever it is.)
    /// </summary>
    private MpvPropertyRead<long>? _fileSize;
    public MpvPropertyRead<long> FileSize => _fileSize ??= new(this, "file-size");

    /// <summary>
    /// Total number of frames in current file. This is only an estimate. (It's computed from two unreliable quantities: fps and stream length.)
    /// </summary>
    private MpvPropertyRead<long>? _estimatedFrameCount;
    public MpvPropertyRead<long> EstimatedFrameCount => _estimatedFrameCount ??= new(this, "estimated-frame-count");

    /// <summary>
    /// Number of current frame in current stream.This is only an estimate. (It's computed from two unreliable quantities: fps and possibly rounded timestamps.)
    /// </summary>
    private MpvPropertyRead<long>? _estimatedFrameNumber;
    public MpvPropertyRead<long> EstimatedFrameNumber => _estimatedFrameNumber ??= new(this, "estimated-frame-number");

    /// <summary>
    /// Full path of the currently played file. Usually this is exactly the same string you pass on the mpv command line or the loadfile command, even if it's a relative path. If you expect an absolute path, you will have to determine it yourself, for example by using the working-directory property.
    /// </summary>
    private MpvPropertyReadString? _path;
    public MpvPropertyReadString Path => _path ??= new(this, "path");

    /// <summary>
    /// The full path to the currently played media. This is different only from path in special cases. In particular, if --ytdl=yes is used, and the URL is detected by youtube-dl, then the script will set this property to the actual media URL. This property should be set only during the on_load or on_load_fail hooks, otherwise it will have no effect (or may do something implementation defined in the future). The property is reset if playback of the current media ends.
    /// </summary>
    private MpvPropertyReadString? _streamOpenFileName;
    public MpvPropertyReadString StreamOpenFileName => _streamOpenFileName ??= new(this, "stream-open-filename");

    /// <summary>
    /// If the currently played file has a title tag, use that. Otherwise, return the filename property.
    /// </summary>
    private MpvPropertyReadString? _mediaTitle;
    public MpvPropertyReadString MediaTitle => _mediaTitle ??= new(this, "media-title");

    /// <summary>
    /// Symbolic name of the file format. In some cases, this is a comma-separated list of format names, e.g. mp4 is mov,mp4,m4a,3gp,3g2,mj2 (the list may grow in the future for any format).
    /// </summary>
    private MpvPropertyReadString? _fileFormat;
    public MpvPropertyReadString FileFormat => _fileFormat ??= new(this, "file-format");

    /// <summary>
    /// Filename (full path) of the stream layer filename. (This is probably useless and is almost never different from path.)
    /// </summary>
    private MpvPropertyReadString? _streamPath;
    public MpvPropertyReadString StreamPath => _streamPath ??= new(this, "stream-path");

    /// <summary>
    /// Raw byte position in source stream. Technically, this returns the position of the most recent packet passed to a decoder.
    /// </summary>
    private MpvPropertyRead<long>? _streamPos;
    public MpvPropertyRead<long> StreamPos => _streamPos ??= new(this, "stream-pos");

    /// <summary>
    /// Raw end position in bytes in source stream.
    /// </summary>
    private MpvPropertyRead<long>? _streamEnd;
    public MpvPropertyRead<long> StreamEnd => _streamEnd ??= new(this, "stream-end");

    /// <summary>
    /// Duration of the current file in seconds. If the duration is unknown, the property is unavailable. Note that the file duration is not always exactly known, so this is an estimate.
    /// </summary>
    private MpvPropertyRead<double>? _duration;
    public MpvPropertyRead<double> Duration => _duration ??= new(this, "duration");

    /// <summary>
    /// Last A/V synchronization difference. Unavailable if audio or video is disabled.
    /// </summary>
    private MpvPropertyRead<float>? _aVSync;
    public MpvPropertyRead<float> AVSync => _aVSync ??= new(this, "avsync");

    /// <summary>
    /// Total A-V sync correction done. Unavailable if audio or video is disabled.
    /// </summary>
    private MpvPropertyRead<float>? _totalAVSyncChange;
    public MpvPropertyRead<float> TotalAVSyncChange => _totalAVSyncChange ??= new(this, "total-avsync-change");

    /// <summary>
    /// Video frames dropped by decoder, because video is too far behind audio (when using --framedrop=decoder). Sometimes, this may be incremented in other situations, e.g. when video packets are damaged, or the decoder doesn't follow the usual rules. Unavailable if video is disabled.
    /// </summary>
    private MpvPropertyRead<long>? _decoderFrameDropCount;
    public MpvPropertyRead<long> DecoderFrameDropCount => _decoderFrameDropCount ??= new(this, "decoder-frame-drop-count");

    /// <summary>
    /// Frames dropped by VO (when using --framedrop=vo).
    /// </summary>
    private MpvPropertyRead<long>? _frameDropCount;
    public MpvPropertyRead<long> FrameDropCount => _frameDropCount ??= new(this, "frame-drop-count");

    /// <summary>
    /// Number of video frames that were not timed correctly in display-sync mode for the sake of keeping A/V sync. This does not include external circumstances, such as video rendering being too slow or the graphics driver somehow skipping a vsync. It does not include rounding errors either (which can happen especially with bad source timestamps). For example, using the display-desync mode should never change this value from 0.
    /// </summary>
    private MpvPropertyRead<long>? _mistimedFrameCount;
    public MpvPropertyRead<long> MistimedFrameCount => _mistimedFrameCount ??= new(this, "mistimed-frame-count");

    /// <summary>
    /// For how many vsyncs a frame is displayed on average. This is available if display-sync is active only. For 30 FPS video on a 60 Hz screen, this will be 2. This is the moving average of what actually has been scheduled, so 24 FPS on 60 Hz will never remain exactly on 2.5, but jitter depending on the last frame displayed.
    /// </summary>
    private MpvPropertyRead<float>? _vSyncRatio;
    public MpvPropertyRead<float> VSyncRatio => _vSyncRatio ??= new(this, "vsync-ratio");

    /// <summary>
    /// Estimated number of frames delayed due to external circumstances in display-sync mode. Note that in general, mpv has to guess that this is happening, and the guess can be inaccurate.
    /// </summary>
    private MpvPropertyRead<long>? _voDelayedFrameCount;
    public MpvPropertyRead<long> VoDelayedFrameCount => _voDelayedFrameCount ??= new(this, "vo-delayed-frame-count");

    /// <summary>
    /// Position in current file (0-100). The advantage over using this instead of calculating it out of other properties is that it properly falls back to estimating the playback position from the byte position, if the file duration is not known.
    /// </summary>
    private MpvPropertyWrite<double>? _percentPos;
    public MpvPropertyWrite<double> PercentPos => _percentPos ??= new(this, "percent-pos");

    /// <summary>
    /// Position in current file in seconds.
    /// </summary>
    private MpvPropertyWrite<double>? _timePos = null;
	public MpvPropertyWrite<double> TimePos => _timePos ??= new(this, "time-pos");

    /// <summary>
    /// Remaining length of the file in seconds. Note that the file duration is not always exactly known, so this is an estimate.
    /// </summary>
    private MpvPropertyRead<double>? _timeRemaining;
    public MpvPropertyRead<double> TimeRemaining => _timeRemaining ??= new(this, "time-remaining");

    /// <summary>
    /// Current audio playback position in current file in seconds. Unlike time-pos, this updates more often than once per frame. For audio-only files, it is mostly equivalent to time-pos, while for video-only files this property is not available.
    /// </summary>
    private MpvPropertyRead<double>? _audioPts;
    public MpvPropertyRead<double> AudioPts => _audioPts ??= new(this, "audio-pts");

    /// <summary>
    /// TimeRemaining scaled by the current speed.
    /// </summary>
    private MpvPropertyRead<double>? _playtimeRemaining;
    public MpvPropertyRead<double> PlaytimeRemaining => _playtimeRemaining ??= new(this, "playtime-remaining");

    /// <summary>
    /// Position in current file in seconds. Unlike time-pos, the time is clamped to the range of the file. (Inaccurate file durations etc. could make it go out of range. Useful on attempts to seek outside of the file, as the seek target time is considered the current position during seeking.)
    /// </summary>
    private MpvPropertyWrite<double>? _playbackTime;
    public MpvPropertyWrite<double> PlaybackTime => _playbackTime ??= new(this, "playback-time");

    /// <summary>
    /// Current chapter number. The number of the first chapter is 0.
    /// </summary>
    private MpvPropertyWrite<int>? _chapter;
    public MpvPropertyWrite<int> Chapter => _chapter ??= new(this, "chapter ");

    /// <summary>
    /// Currently selected edition. This property is unavailable if no file is loaded, or the file has no editions. (Matroska files make a difference between having no editions and a single edition, which will be reflected by the property, although in practice it does not matter.)
    /// </summary>
    private MpvPropertyRead<int>? _currentEdition;
    public MpvPropertyRead<int> CurrentEdition => _currentEdition ??= new(this, "current-edition");

    /// <summary>
    /// Number of chapters.
    /// </summary>
    private MpvPropertyRead<int>? _chapters;
    public MpvPropertyRead<int> Chapters => _chapters ??= new(this, "chapters");

    /// <summary>
    /// Number of MKV editions.
    /// </summary>
    private MpvPropertyRead<int>? _editions;
    public MpvPropertyRead<int> Editions => _editions ??= new(this, "editions");

    /// <summary>
    /// Number of editions. If there are no editions, this can be 0 or 1 (1 if there's a useless dummy edition).
    /// </summary>
    private MpvPropertyRead<int>? _editionListCount;
    public MpvPropertyRead<int> EditionListCount => _editionListCount ??= new(this, "edition-list/count");

    /// <summary>
    /// Edition ID as integer. Use this to set the edition property. Currently, this is the same as the edition index.
    /// </summary>
    private MpvPropertyIndexWrite<int, int>? _editionListId;
    public MpvPropertyIndexWrite<int, int> EditionListId => _editionListId ??= new(this, "edition-list/{0}/id");

    /// <summary>
    /// True if this is the default edition, otherwise false.
    /// </summary>
    private MpvPropertyIndexRead<int, bool>? _editionListDefault;
    public MpvPropertyIndexRead<int, bool> EditionListDefault => _editionListDefault ??= new(this, "edition-list/{0}/default");

    /// <summary>
    /// Edition title as stored in the file. Not always available.
    /// </summary>
    private MpvPropertyIndexReadRef<int, string>? _editionListTitle;
    public MpvPropertyIndexReadRef<int, string> EditionListTitle => _editionListTitle ??= new(this, "edition-list/{0}/title");

    /// <summary>
    /// Metadata key/value pairs.
    /// </summary>
    private MetadataProperties? _metadata;
    public MetadataProperties Metadata => _metadata ??= new(this, "metadata");

    /// <summary>
    /// Like metadata, but includes only fields listed in the --display-tags option. This is the same set of tags that is printed to the terminal.
    /// </summary>
    private MetadataProperties? _filteredMetadata;
    public MetadataProperties FilteredMetadata => _filteredMetadata ??= new(this, "filtered-metadata");

    /// <summary>
    /// Metadata of current chapter. Works similar to metadata property. It also allows the same access methods (using sub-properties).
    /// Per-chapter metadata is very rare.Usually, only the chapter name (title) is set.
    /// For accessing other information, like chapter start, see the chapter-list property.
    /// </summary>
    private MetadataProperties? _chapterMetadata;
    public MetadataProperties ChapterMetadata => _chapterMetadata ??= new(this, "chapter-metadata");

    /// <summary>
    /// Metadata added by video filters. Accessed by the filter label, which, if not explicitly specified using the @filter-label: syntax, will be filter-nameNN.
    /// </summary>
    /// <param name="filterLabel">The label of the filter.</param>
    public MetadataProperties VideoFilterMetadata(string filterLabel) => new(this, $"vf-metadata/{filterLabel}");

    /// <summary>
    /// Metadata added by audio filters. Accessed by the filter label, which, if not explicitly specified using the @filter-label: syntax, will be filter-nameNN.
    /// </summary>
    /// <param name="filterLabel">The label of the filter.</param>
    public MetadataProperties AudioFilterMetadata(string filterLabel) => new(this, $"af-metadata/{filterLabel}");

    /// <summary>
    /// Return yes if no file is loaded, but the player is staying around because of the --idle option.
    /// </summary>
    private MpvPropertyRead<bool>? _idleActive;
    public MpvPropertyRead<bool> IdleActive => _idleActive ??= new(this, "idle-active");

    /// <summary>
    /// Return yes if the playback core is paused, otherwise no. This can be different pause in special situations, such as when the player pauses itself due to low network cache.
    /// This also returns yes if playback is restarting or if nothing is playing at all.In other words, it's only no if there's actually video playing. (Behavior since mpv 0.7.0.)
    /// </summary>
    private MpvPropertyRead<bool>? _coreIdle;
    public MpvPropertyRead<bool> CoreIdle => _coreIdle ??= new(this, "core-idle");

    /// <summary>
    /// Current I/O read speed between the cache and the lower layer (like network). This gives the number bytes per seconds over a 1 second window.
    /// </summary>
    private MpvPropertyRead<long>? _cacheSpeed;
    public MpvPropertyRead<long> CacheSpeed => _cacheSpeed ??= new(this, "cache-speed");

    /// <summary>
    /// Approximate duration of video buffered in the demuxer, in seconds. The guess is very unreliable, and often the property will not be available at all, even if data is buffered
    /// </summary>
    private MpvPropertyRead<double>? _demuxerCacheDuration;
    public MpvPropertyRead<double> DemuxerCacheDuration => _demuxerCacheDuration ??= new(this, "demuxer-cache-duration");

    /// <summary>
    /// Approximate time of video buffered in the demuxer, in seconds. Same as demuxer-cache-duration but returns the last timestamp of buffered data in demuxer.
    /// </summary>
    private MpvPropertyRead<double>? _demuxerCacheTime;
    public MpvPropertyRead<double> DemuxerCacheTime => _demuxerCacheTime ??= new(this, "demuxer-cache-time");

    /// <summary>
    /// Returns yes if the demuxer is idle, which means the demuxer cache is filled to the requested amount, and is currently not reading more data.
    /// </summary>
    private MpvPropertyRead<bool>? _demuxerCacheIdle;
    public MpvPropertyRead<bool> DemuxerCacheIdle => _demuxerCacheIdle ??= new(this, "demuxer-cache-idle");

    /// <summary>
    /// Returns information about the demuxer cache.
    /// </summary>
    private MpvPropertyReadRef<DemuxerCacheState>? _demuxerCacheState;
    public MpvPropertyReadRef<DemuxerCacheState> DemuxerCacheState => _demuxerCacheState ??= new(this, "demuxer-cache-state");

    /// <summary>
    /// Returns true if the stream demuxed via the main demuxer is most likely played via network. What constitutes "network" is not always clear, might be used for other types of untrusted streams, could be wrong in certain cases, and its definition might be changing. Also, external files (like separate audio files or streams) do not influence the value of this property (currently).
    /// </summary>
    private MpvPropertyRead<bool>? _demuxerViaNetwork;
    public MpvPropertyRead<bool> DemuxerViaNetwork => _demuxerViaNetwork ??= new(this, "demuxer-via-network");

    /// <summary>
    /// Returns the start time reported by the demuxer in fractional seconds.
    /// </summary>
    private MpvPropertyRead<double>? _demuxerStartTime;
    public MpvPropertyRead<double> DemuxerStartTime => _demuxerStartTime ??= new(this, "demuxer-start-time");

    /// <summary>
    /// Returns True when playback is paused because of waiting for the cache.
    /// </summary>
    private MpvPropertyRead<bool>? _pausedForCache;
    public MpvPropertyRead<bool> PausedForCache => _pausedForCache ??= new(this, "paused-for-cache");

    /// <summary>
    /// Returns the percentage (0-100) of the cache fill status until the player will unpause (related to paused-for-cache).
    /// </summary>
    private MpvPropertyRead<double>? _cacheBufferingState;
    public MpvPropertyRead<double> CacheBufferingState => _cacheBufferingState ??= new(this, "cache-buffering-state");

    /// <summary>
    /// Returns true if end of playback was reached, no otherwise. Note that this is usually interesting only if --keep-open is enabled, since otherwise the player will immediately play the next file (or exit or enter idle mode), and in these cases the eof-reached property will logically be cleared immediately after it's set.
    /// </summary>
    private MpvPropertyRead<bool>? _eofReached;
    public MpvPropertyRead<bool> EofReached => _eofReached ??= new(this, "eof-reached");

    /// <summary>
    /// Returns True if the player is currently seeking, or otherwise trying to restart playback. (It's possible that it returns True while a file is loaded. This is because the same underlying code is used for seeking and resyncing.)
    /// </summary>
    private MpvPropertyRead<bool>? _seeking;
    public MpvPropertyRead<bool> Seeking => _seeking ??= new(this, "seeking");

    /// <summary>
    /// Return yes if the audio mixer is active, no otherwise.
    /// This option is relatively useless.Before mpv 0.18.1, it could be used to infer behavior of the volume property.
    /// </summary>
    private MpvPropertyRead<bool>? _mixerActive;
    public MpvPropertyRead<bool> MixerActive => _mixerActive ??= new(this, "mixer-active");

    /// <summary>
    /// System volume (0-100). This property is available only if mpv audio output is currently active, and only if the underlying implementation supports volume control. What this option does depends on the API. For example, on ALSA this usually changes system-wide audio, while with PulseAudio this controls per-application volume.
    /// </summary>
    private MpvPropertyWrite<double>? _aoVolume;
    public MpvPropertyWrite<double> AoVolume => _aoVolume ??= new(this, "ao-volume");

    /// <summary>
    /// Similar to AoVolume, but controls the mute state. May be unimplemented even if AoVolume works.
    /// </summary>
    private MpvPropertyWrite<double>? _aoMute;
    public MpvPropertyWrite<double> AoMute => _aoMute ??= new(this, "ao-mute");

    /// <summary>
    /// Audio codec selected for decoding.
    /// </summary>
    private MpvPropertyReadString? _audioCodec;
    public MpvPropertyReadString AudioCodec => _audioCodec ??= new(this, "audio-codec");

    /// <summary>
    /// Audio codec.
    /// </summary>
    private MpvPropertyReadString? _audioCodecName;
    public MpvPropertyReadString AudioCodecName => _audioCodecName ??= new(this, "audio-codec-name");

    /// <summary>
    /// Audio format as output by the audio decoder. This has a number of sub-properties.
    /// </summary>
    private AudioProperties? _audioParams;
    public AudioProperties AudioParams => _audioParams ??= new(this, "audio-params");

    /// <summary>
    /// Audio format as output by the audio decoder. This has a number of sub-properties.
    /// </summary>
    private AudioProperties? _audioOutParams;
    public AudioProperties AudioOutParams => _audioOutParams ??= new(this, "audio-out-params");

    /// <summary>
    /// Reflects the --hwdec option.
    /// Writing to it may change the currently used hardware decoder, if possible. (Internally, the player may reinitialize the decoder, and will perform a seek to refresh the video properly.) You can watch the other hwdec properties to see whether this was successful.
    /// </summary>
    private MpvPropertyReadString? _hwdec;
    public MpvPropertyReadString Hwdec => _hwdec ??= new(this, "hwdec");

    /// <summary>
    /// Returns the current hardware decoding in use. If decoding is active, return one of the values used by the hwdec option/property. no indicates software decoding. If no decoder is loaded, the property is unavailable.
    /// </summary>
    private MpvPropertyReadString? _hwdecCurrent;
    public MpvPropertyReadString HwdecCurrent => _hwdecCurrent ??= new(this, "hwdec-current");

    /// <summary>
    /// This returns the currently loaded hardware decoding/output interop driver. This is known only once the VO has opened (and possibly later). With some VOs (like gpu), this might be never known in advance, but only when the decoder attempted to create the hw decoder successfully. (Using --gpu-hwdec-interop can load it eagerly.) If there are multiple drivers loaded, they will be separated by ,.
    /// If no VO is active or no interop driver is known, this property is unavailable.
    /// This does not necessarily use the same values as hwdec.There can be multiple interop drivers for the same hardware decoder, depending on platform and VO.
    /// </summary>
    private MpvPropertyReadString? _hwdecInterop;
    public MpvPropertyReadString HwdecInterop => _hwdecInterop ??= new(this, "hwdec-interop");

    /// <summary>
    /// Video format as string.
    /// </summary>
    private MpvPropertyReadString? _videoFormat;
    public MpvPropertyReadString VideoFormat => _videoFormat ??= new(this, "video-format");

    /// <summary>
    /// Video codec selected for decoding.
    /// </summary>
    private MpvPropertyReadString? _videoCodec;
    public MpvPropertyReadString VideoCodec => _videoCodec ??= new(this, "video-codec");

    /// <summary>
    /// Returns the width of the video as decoded, or if no video frame has been decoded yet, the (possibly incorrect) container indicated size.
    /// </summary>
    private MpvPropertyRead<int>? _width;
    public MpvPropertyRead<int> Width => _width ??= new(this, "width");

    /// <summary>
    /// Returns the height of the video as decoded, or if no video frame has been decoded yet, the (possibly incorrect) container indicated size.
    /// </summary>
    private MpvPropertyRead<int>? _height;
    public MpvPropertyRead<int> Height => _height ??= new(this, "height");

    /// <summary>
    /// Video parameters, as output by the decoder (with overrides like aspect etc. applied). This has a number of sub-properties:
    /// </summary>
    private VideoProperties? _videoParams;
    public VideoProperties VideoParams => _videoParams ??= new(this, "video-params");

    /// <summary>
    /// This is the video display width after filters and aspect scaling have been applied. The actual video window size can still be different from this, e.g. if the user resized the video window manually. This has the same values as VideoOutParams/DisplayWidth.
    /// </summary>
    private MpvPropertyRead<int>? _displayWidth;
    public MpvPropertyRead<int> DisplayWidth => _displayWidth ??= new(this, "dwidth");

    /// <summary>
    /// This is the video display height after filters and aspect scaling have been applied. The actual video window size can still be different from this, e.g. if the user resized the video window manually. This has the same values as VideoOutParams/DisplayHeight.
    /// </summary>
    private MpvPropertyRead<int>? _displayHeight;
    public MpvPropertyRead<int> DisplayHeight => _displayHeight ??= new(this, "dheight");

    /// <summary>
    /// Exactly like VideoParams, but no overrides applied.
    /// </summary>
    private VideoProperties? _videoDecParams;
    public VideoProperties VideoDecParams => _videoDecParams ??= new(this, "video-dec-params");

    /// <summary>
    /// Same as video-params, but after video filters have been applied. If there are no video filters in use, this will contain the same values as video-params. Note that this is still not necessarily what the video window uses, since the user can change the window size, and all real VOs do their own scaling independently from the filter chain.
    /// </summary>
    private VideoProperties? _videoOutParams;
    public VideoProperties VideoOutParams => _videoOutParams ??= new(this, "video-out-params");

    /// <summary>
    /// The type of the picture. It can be "I" (intra), "P" (predicted), "B" (bi-dir predicted) or unavailable.
    /// </summary>
    private MpvPropertyReadString? _videoFramePictureType;
    public MpvPropertyReadString VideoFramePictureType => _videoFramePictureType ??= new(this, "video-frame-info/picture-type");

    /// <summary>
    /// Whether the content of the frame is interlaced.
    /// </summary>
    private MpvPropertyRead<bool>? _videoFrameInterlaced;
    public MpvPropertyRead<bool> VideoFrameInterlaced => _videoFrameInterlaced ??= new(this, "video-frame-info/interlaced");

    /// <summary>
    /// If the content is interlaced, whether the top field is displayed first.
    /// </summary>
    private MpvPropertyRead<bool>? _videoFrameTff;
    public MpvPropertyRead<bool> VideoFrameTff => _videoFrameTff ??= new(this, "video-frame-info/tff");

    /// <summary>
    /// Whether the frame must be delayed when decoding.
    /// </summary>
    private MpvPropertyRead<bool>? _videoFrameRepeat;
    public MpvPropertyRead<bool> VideoFrameRepeat => _videoFrameRepeat ??= new(this, "video-frame-info/repeat");

    /// <summary>
    /// Container FPS. This can easily contain bogus values. For videos that use modern container formats or video codecs, this will often be incorrect.
    /// </summary>
    private MpvPropertyRead<float>? _containerFps;
    public MpvPropertyRead<float> ContainerFps => _containerFps ??= new(this, "container-fps");

    /// <summary>
    /// Estimated/measured FPS of the video filter chain output. (If no filters are used, this corresponds to decoder output.) This uses the average of the 10 past frame durations to calculate the FPS. It will be inaccurate if frame-dropping is involved (such as when framedrop is explicitly enabled, or after precise seeking). Files with imprecise timestamps (such as Matroska) might lead to unstable results.
    /// </summary>
    private MpvPropertyRead<float>? _estimatedVideoFilterFps;
    public MpvPropertyRead<float> EstimatedVideoFilterFps => _estimatedVideoFilterFps ??= new(this, "estimated-vf-fps");

    /// <summary>
    /// The window-scale value calculated from the current window size. This has the same value as ``window-scale`` if the window size was not changed since setting the option, and the window size was not restricted in other ways.The property is unavailable if no video is active.
    /// </summary>
    private MpvPropertyWrite<double>? _currentWindowScale;
    public MpvPropertyWrite<double> CurrentWindowScale => _currentWindowScale ??= new(this, "current-window-scale");

    /// <summary>
    /// Whether the window has focus. Currently works only on X11 and Wayland.
    /// </summary>
    private MpvPropertyRead<bool>? _focused;
    public MpvPropertyRead<bool> Focused => _focused ??= new(this, "focused");

    /// <summary>
    /// Names of the displays that the mpv window covers. On X11, these are the xrandr names (LVDS1, HDMI1, DP1, VGA1, etc.). On Windows, these are the GDI names (\.DISPLAY1, \.DISPLAY2, etc.) and the first display in the list will be the one that Windows considers associated with the window (as determined by the MonitorFromWindow API.) On macOS these are the Display Product Names as used in the System Information and only one display name is returned since a window can only be on one screen.
    /// </summary>
    private MpvPropertyReadRef<IList<string>>? _displayNames;
    public MpvPropertyReadRef<IList<string>> DisplayNames => _displayNames ??= new(this, "display-names");

    // /// <summary>
    // /// The refresh rate of the current display. Currently, this is the lowest FPS of any display covered by the video, as retrieved by the underlying system APIs (e.g. xrandr on X11). It is not the measured FPS. It's not necessarily available on all platforms. Note that any of the listed facts may change any time without a warning.
    // /// </summary>
    // public MpvPropertyRead<float> DisplayFps => new(this, "display-fps");

    /// <summary>
    /// Only available if display-sync mode (as selected by --video-sync) is active. Returns the actual rate at which display refreshes seem to occur, measured by system time.
    /// </summary>
    private MpvPropertyRead<float>? _estimatedDisplayFps;
    public MpvPropertyRead<float> EstimatedDisplayFps => _estimatedDisplayFps ??= new(this, "estimated-display-fps");

    /// <summary>
    /// Estimated deviation factor of the vsync duration.
    /// </summary>
    private MpvPropertyRead<float>? _vSyncJitter;
    public MpvPropertyRead<float> VSyncJitter => _vSyncJitter ??= new(this, "vsync-jitter");

    /// <summary>
    /// The HiDPI scale factor as reported by the windowing backend. If no VO is active, or if the VO does not report a value, this property is unavailable. It may be saner to report an absolute DPI, however, this is the way HiDPI support is implemented on most OS APIs. See also --hidpi-window-scale.
    /// </summary>
    private MpvPropertyRead<float>? _displayHiDpiScale;
    public MpvPropertyRead<float> DisplayHiDpiScale => _displayHiDpiScale ??= new(this, "display-hidpi-scale");

    /// <summary>
    /// Width of the VO window in OSD render units (usually pixels, but may be scaled pixels with VOs like xv).
    /// </summary>
    private MpvPropertyRead<int>? _osdWidth;
    public MpvPropertyRead<int> OsdWidth => _osdWidth ??= new(this, "osd-dimensions/w");

    /// <summary>
    /// Height of the VO window in OSD render units,
    /// </summary>
    private MpvPropertyRead<int>? _osdHeight;
    public MpvPropertyRead<int> OsdHeight => _osdHeight ??= new(this, "osd-dimensions/h");

    /// <summary>
    /// Pixel aspect ratio of the OSD (usually 1).
    /// </summary>
    private MpvPropertyRead<float>? _osdPixelAspectRatio;
    public MpvPropertyRead<float> OsdPixelAspectRatio => _osdPixelAspectRatio ??= new(this, "osd-dimensions/par");

    /// <summary>
    /// Display aspect ratio of the VO window. (Computing from the properties above.)
    /// </summary>
    private MpvPropertyRead<float>? _osdDisplayAspectRatio;
    public MpvPropertyRead<float> OsdDisplayAspectRatio => _osdDisplayAspectRatio ??= new(this, "osd-dimensions/aspect");

    /// <summary>
    /// OSD to top video margins. This describes the area into which the video is rendered.
    /// </summary>
    private MpvPropertyRead<int>? _osdMarginTop;
    public MpvPropertyRead<int> OsdMarginTop => _osdMarginTop ??= new(this, "osd-dimensions/mt");

    /// <summary>
    /// OSD to bottom video margins. This describes the area into which the video is rendered.
    /// </summary>
    private MpvPropertyRead<int>? _osdMarginBottom;
    public MpvPropertyRead<int> OsdMarginBottom => _osdMarginBottom ??= new(this, "osd-dimensions/mb");

    /// <summary>
    /// OSD to left video margins. This describes the area into which the video is rendered.
    /// </summary>
    private MpvPropertyRead<int>? _osdMarginLeft;
    public MpvPropertyRead<int> OsdMarginLeft => _osdMarginLeft ??= new(this, "osd-dimensions/ml");

    /// <summary>
    /// OSD to right video margins. This describes the area into which the video is rendered.
    /// </summary>
    private MpvPropertyRead<int>? _osdMarginRight;
    public MpvPropertyRead<int> OsdMarginRight => _osdMarginRight ??= new(this, "osd-dimensions/mr");

    /// <summary>
    /// Last known mouse X position, normalizd to OSD dimensions.
    /// </summary>
    private MpvPropertyRead<int>? _mousePosX;
    public MpvPropertyRead<int> MousePosX => _mousePosX ??= new(this, "mouse-pos/x");

    /// <summary>
    /// Last known mouse Y position, normalizd to OSD dimensions.
    /// </summary>
    private MpvPropertyRead<int>? _mousePosY;
    public MpvPropertyRead<int> MousePosY => _mousePosY ??= new(this, "mouse-pos/y");

    /// <summary>
    /// Whether the mouse pointer hovers the video window. The coordinates should be ignored when this value is false, because the video backends update them only when the pointer hovers the window.
    /// </summary>
    private MpvPropertyRead<bool>? _mousePosHover;
    public MpvPropertyRead<bool> MousePosHover => _mousePosHover ??= new(this, "mouse-pos/hover");

    /// <summary>
    /// Return the current subtitle text regardless of sub visibility. Formatting is stripped. If the subtitle is not text-based (i.e. DVD/BD subtitles), an empty string is returned.
    /// This property is experimental and might be removed in the future.
    /// </summary>
    private MpvPropertyReadString? _subText;
    public MpvPropertyReadString SubText => _subText ??= new(this, "sub-text");

    /// <summary>
    /// Returns the current subtitle start time (in seconds). If there's multiple current subtitles, returns the first start time. If no current subtitle is present null is returned instead.
    /// </summary>
    private MpvPropertyRead<float>? _subStart;
    public MpvPropertyRead<float> SubStart => _subStart ??= new(this, "sub-start");

    /// <summary>
    /// Returns the current subtitle start time (in seconds). If there's multiple current subtitles, return the last end time. If no current subtitle is present, or if it's present but has unknown or incorrect duration, null is returned instead.
    /// </summary>
    private MpvPropertyRead<float>? _subEnd;
    public MpvPropertyRead<float> SubEnd => _subEnd ??= new(this, "sub-end");

    /// <summary>
    /// Current position on playlist. The first entry is on position 0. Writing to the property will restart playback at the written entry.
    /// </summary>
    private MpvPropertyWrite<int>? _playlistPosition;
    public MpvPropertyWrite<int> PlaylistPosition => _playlistPosition ??= new(this, "playlist-pos");

    /// <summary>
    /// Number of total playlist entries.
    /// </summary>
    private MpvPropertyRead<int>? _playlistCount;
    public MpvPropertyRead<int> PlaylistCount => _playlistCount ??= new(this, "playlist-count");

    /// <summary>
    /// Filename of the Nth playlist entry.
    /// </summary>
    private MpvPropertyIndexReadRef<int, string>? _playlistFileName;
    public MpvPropertyIndexReadRef<int, string> PlaylistFileName => _playlistFileName ??= new(this, "playlist/{0}/filename");

    /// <summary>
    /// True if this entry is currently playing (or being loaded). Unavailable or False otherwise. When changing files, current and playing can be different, because the currently playing file hasn't been unloaded yet; in this case, current refers to the new selection.
    /// </summary>
    private MpvPropertyIndexRead<int, bool>? _playlistIsCurrent;
    public MpvPropertyIndexRead<int, bool> PlaylistIsCurrent => _playlistIsCurrent ??= new(this, "playlist/{0}/current");

    /// <summary>
    /// True if this entry is currently playing (or being loaded). Unavailable or False otherwise. When changing files, current and playing can be different, because the currently playing file hasn't been unloaded yet; in this case, current refers to the new selection.
    /// </summary>
    private MpvPropertyIndexRead<int, bool>? _playlistIsPlaying;
    public MpvPropertyIndexRead<int, bool> PlaylistIsPlaying => _playlistIsPlaying ??= new(this, "playlist/{0}/playing");

    /// <summary>
    /// Name of the Nth entry. Only available if the playlist file contains such fields, and only if mpv's parser supports it for the given playlist format.
    /// </summary>
    private MpvPropertyIndexReadRef<int, string>? _playlistTitle;
    public MpvPropertyIndexReadRef<int, string> PlaylistTitle => _playlistTitle ??= new(this, "playlist/{0}/title");

    /// <summary>
    /// Total number of tracks.
    /// </summary>
    private MpvPropertyRead<int>? _trackListCount;
    public MpvPropertyRead<int> TrackListCount => _trackListCount ??= new(this, "track-list/count");

    /// <summary>
    /// The ID as it's used for -sid/--aid/--vid. This is unique within tracks of the same type (sub/audio/video), but otherwise not.
    /// </summary>
    private MpvPropertyIndexRead<int, int>? _trackListId;
    public MpvPropertyIndexRead<int, int> TrackListId => _trackListId ??= new(this, "track-list/{0}/id");

    /// <summary>
    /// String describing the media type. One of audio, video, sub.
    /// </summary>
    private MpvPropertyIndexReadRef<int, string>? _trackListType;
    public MpvPropertyIndexReadRef<int, string> TrackListType => _trackListType ??= new(this, "track-list/{0}/type");

    /// <summary>
    /// Track ID as used in the source file. Not always available. (It is missing if the format has no native ID, if the track is a pseudo-track that does not exist in this way in the actual file, or if the format is handled by libavformat, and the format was not whitelisted as having track IDs.)
    /// </summary>
    private MpvPropertyIndexRead<int, int>? _trackListSrcId;
    public MpvPropertyIndexRead<int, int> TrackListSrcId => _trackListSrcId ??= new(this, "track-list/{0}/src-id");

    /// <summary>
    /// Track language as identified by the file. Not always available.
    /// </summary>
    private MpvPropertyIndexReadRef<int, string>? _trackListLanguage;
    public MpvPropertyIndexReadRef<int, string> TrackListLanguage => _trackListLanguage ??= new(this, "track-list/{0}/lang");

    /// <summary>
    /// True if this is a video track that consists of a single picture, False or unavailable otherwise. This is used for video tracks that are really attached pictures in audio files.
    /// </summary>
    private MpvPropertyIndexRead<int, bool>? _trackListHasAlbumArt;
    public MpvPropertyIndexRead<int, bool> TrackListHasAlbumArt => _trackListHasAlbumArt ??= new(this, "track-list/{0}/albumart");

    /// <summary>
    /// True if the track has the default flag set in the file, otherwise False.
    /// </summary>
    private MpvPropertyIndexRead<int, bool>? _trackListIsDefault;
    public MpvPropertyIndexRead<int, bool> TrackListIsDefault => _trackListIsDefault ??= new(this, "track-list/{0}/default");

    /// <summary>
    /// True if the track has the forced flag set in the file, otherwise False.
    /// </summary>
    private MpvPropertyIndexRead<int, bool>? _trackListIsForced;
    public MpvPropertyIndexRead<int, bool> TrackListIsForced => _trackListIsForced ??= new(this, "track-list/{0}/forced");

    /// <summary>
    /// The codec name used by this track, for example h264. Unavailable in some rare cases.
    /// </summary>
    private MpvPropertyIndexReadRef<int, string>? _trackListCodec;
    public MpvPropertyIndexReadRef<int, string> TrackListCodec => _trackListCodec ??= new(this, "track-list/{0}/codec");

    /// <summary>
    /// True if the track is an external file, otherwise False. This is set for separate subtitle files.
    /// </summary>
    private MpvPropertyIndexRead<int, bool>? _trackListIsExternal;
    public MpvPropertyIndexRead<int, bool> TrackListIsExternal => _trackListIsExternal ??= new(this, "track-list/{0}/external");

    /// <summary>
    /// The filename if the track is from an external file, unavailable otherwise.
    /// </summary>
    private MpvPropertyIndexRead<int, bool>? _trackListExternalFileName;
    public MpvPropertyIndexRead<int, bool> TrackListExternalFileName => _trackListExternalFileName ??= new(this, "track-list/{0}/external-filename");

    /// <summary>
    /// True if the track is currently decoded, otherwise False.
    /// </summary>
    private MpvPropertyIndexRead<int, bool>? _trackListIsSelected;
    public MpvPropertyIndexRead<int, bool> TrackListIsSelected => _trackListIsSelected ??= new(this, "track-list/{0}/selected");

    /// <summary>
    /// It indicates the selection order of tracks for the same type. If a track is not selected, or is selected by the --lavfi-complex, it is not available.For subtitle tracks, 0 represents the sid, and 1 represents the secondary-sid.
    /// </summary>
    private MpvPropertyIndexRead<int, int>? _trackListMainSelection;
    public MpvPropertyIndexRead<int, int> TrackListMainSelection => _trackListMainSelection ??= new(this, "track-list/{0}/main-selection");

    /// <summary>
    /// The stream index as usually used by the FFmpeg utilities. Note that this can be potentially wrong if a demuxer other than libavformat (--demuxer=lavf) is used. For mkv files, the index will usually match even if the default (builtin) demuxer is used, but there is no hard guarantee.
    /// </summary>
    private MpvPropertyIndexRead<int, int>? _trackListFfIndex;
    public MpvPropertyIndexRead<int, int> TrackListFfIndex => _trackListFfIndex ??= new(this, "track-list/{0}/ff-index");

    /// <summary>
    /// If this track is being decoded, the human-readable decoder name.
    /// </summary>
    private MpvPropertyIndexReadRef<int, string>? _trackListDecoderDesc;
    public MpvPropertyIndexReadRef<int, string> TrackListDecoderDesc => _trackListDecoderDesc ??= new(this, "track-list/{0}/decoder-desc");

    /// <summary>
    /// Video width hint as indicated by the container. (Not always accurate.)
    /// </summary>
    private MpvPropertyIndexRead<int, int>? _trackListDemuxWidth;
    public MpvPropertyIndexRead<int, int> TrackListDemuxWidth => _trackListDemuxWidth ??= new(this, "track-list/{0}/demux-w");

    /// <summary>
    /// Video height hint as indicated by the container. (Not always accurate.)
    /// </summary>
    private MpvPropertyIndexRead<int, int>? _trackListDemuxHeight;
    public MpvPropertyIndexRead<int, int> TrackListDemuxHeight => _trackListDemuxHeight ??= new(this, "track-list/{0}/demux-h");

    /// <summary>
    /// Number of audio channels as indicated by the container. (Not always accurate - in particular, the track could be decoded as a different number of channels.)
    /// </summary>
    private MpvPropertyIndexRead<int, int>? _trackListDemuxChannelCount;
    public MpvPropertyIndexRead<int, int> TrackListDemuxChannelCount => _trackListDemuxChannelCount ??= new(this, "track-list/{0}/demux-channel-count");

    /// <summary>
    /// Channel layout as indicated by the container. (Not always accurate.)
    /// </summary>
    private MpvPropertyIndexReadRef<int, string>? _trackListDemuxChannels;
    public MpvPropertyIndexReadRef<int, string> TrackListDemuxChannels => _trackListDemuxChannels ??= new(this, "track-list/{0}/demux-channels");

    /// <summary>
    /// Audio sample rate as indicated by the container. (Not always accurate.)
    /// </summary>
    private MpvPropertyIndexRead<int, int>? _trackListDemuxSampleRate;
    public MpvPropertyIndexRead<int, int> TrackListDemuxSampleRate => _trackListDemuxSampleRate ??= new(this, "track-list/{0}/demux-samplerate");

    /// <summary>
    /// Video FPS as indicated by the container. (Not always accurate.)
    /// </summary>
    private MpvPropertyIndexRead<int, float>? _trackListDemuxFps;
    public MpvPropertyIndexRead<int, float> TrackListDemuxFps => _trackListDemuxFps ??= new(this, "track-list/{0}/demux-fps");

    /// <summary>
    /// Audio average bitrate, in bits per second. (Not always accurate.)
    /// </summary>
    private MpvPropertyIndexRead<int, int>? _trackListDemuxBitrate;
    public MpvPropertyIndexRead<int, int> TrackListDemuxBitrate => _trackListDemuxBitrate ??= new(this, "track-list/{0}/demux-bitrate");

    /// <summary>
    /// Video clockwise rotation metadata, in degrees.
    /// </summary>
    private MpvPropertyIndexRead<int, int>? _trackListDemuxRotation;
    public MpvPropertyIndexRead<int, int> TrackListDemuxRotation => _trackListDemuxRotation ??= new(this, "track-list/{0}/demux-rotation");

    /// <summary>
    /// Pixel aspect ratio.
    /// </summary>
    private MpvPropertyIndexRead<int, float>? _trackListDemuxPixelAspectRatio;
    public MpvPropertyIndexRead<int, float> TrackListDemuxPixelAspectRatio => _trackListDemuxPixelAspectRatio ??= new(this, "track-list/{0}/demux-par");

    /// <summary>
    /// Per-track replaygain values. Only available for audio tracks with corresponding information stored in the source file.
    /// </summary>
    private MpvPropertyIndexRead<int, float>? _trackListReplayGainTrackPeak;
    public MpvPropertyIndexRead<int, float> TrackListReplayGainTrackPeak => _trackListReplayGainTrackPeak ??= new(this, "track-list/{0}/replaygain-track-peak");

    /// <summary>
    /// Per-track replaygain values. Only available for audio tracks with corresponding information stored in the source file.
    /// </summary>
    private MpvPropertyIndexRead<int, float>? _trackListReplayGainTrackGain;
    public MpvPropertyIndexRead<int, float> TrackListReplayGainTrackGain => _trackListReplayGainTrackGain ??= new(this, "track-list/{0}/replaygain-track-gain");

    /// <summary>
    /// Per-album replaygain values. If the file has per-track but no per-album information, the per-album values will be copied from the per-track values currently. It's possible that future mpv versions will make these properties unavailable instead in this case.
    /// </summary>
    private MpvPropertyIndexRead<int, float>? _trackListReplayGainAlbumPeak;
    public MpvPropertyIndexRead<int, float> TrackListReplayGainAlbumPeak => _trackListReplayGainAlbumPeak ??= new(this, "track-list/{0}/replaygain-album-peak");

    /// <summary>
    /// Per-album replaygain values. If the file has per-track but no per-album information, the per-album values will be copied from the per-track values currently. It's possible that future mpv versions will make these properties unavailable instead in this case.
    /// </summary>
    private MpvPropertyIndexRead<int, float>? _trackListReplayGainAlbumGain;
    public MpvPropertyIndexRead<int, float> TrackListReplayGainAlbumGain => _trackListReplayGainAlbumGain ??= new(this, "track-list/{0}/replaygain-album-gain");

    /// <summary>
    /// Returns the amount of chapters.
    /// </summary>
    private MpvPropertyRead<int>? _chapterListCount;
    public MpvPropertyRead<int> ChapterListCount => _chapterListCount ??= new(this, "chapter-list/count");

    /// <summary>
    /// Chapter title as stored in the file. Not always available.
    /// </summary>
    private MpvPropertyIndexReadRef<int, string>? _chapterListTitle;
    public MpvPropertyIndexReadRef<int, string> ChapterListTitle => _chapterListTitle ??= new(this, "chapter-list/{0}/title");

    /// <summary>
    /// Chapter start time in seconds.
    /// </summary>
    private MpvPropertyIndexRead<int, double>? _chapterListTime;
    public MpvPropertyIndexRead<int, double> ChapterListTime => _chapterListTime ??= new(this, "chapter-list/{0}/time");

    /// <summary>
    /// Return whether it's generally possible to seek in the current file.
    /// </summary>
    private MpvPropertyRead<bool>? _seekable;
    public MpvPropertyRead<bool> Seekable => _seekable ??= new(this, "seekable");

    /// <summary>
    /// Return True if the current file is considered seekable, but only because the cache is active. This means small relative seeks may be fine, but larger seeks may fail anyway. Whether a seek will succeed or not is generally not known in advance.
    /// If this property returns True, seekable will also return True.
    /// </summary>
    private MpvPropertyRead<bool>? _partiallySeekable;
    public MpvPropertyRead<bool> PartiallySeekable => _partiallySeekable ??= new(this, "partially-seekable");

    /// <summary>
    /// Return whether playback is stopped or is to be stopped. (Useful in obscure situations like during on_load hook processing, when the user can stop playback, but the script has to explicitly end processing.)
    /// </summary>
    private MpvPropertyRead<bool>? _playbackAbort;
    public MpvPropertyRead<bool> PlaybackAbort => _playbackAbort ??= new(this, "playback-abort");

    /// <summary>
    /// Inserts the current OSD symbol as opaque OSD control code (cc). This makes sense only with the show-text command or options which set OSD messages. The control code is implementation specific and is useless for anything else.
    /// </summary>
    private MpvPropertyReadString? _osdSymCc;
    public MpvPropertyReadString OsdSymCc => _osdSymCc ??= new(this, "osd-sym-cc");

    /// <summary>
    /// Return whether the VO is configured right now. Usually this corresponds to whether the video window is visible. If the --force-window option is used, this is usually always returns yes.
    /// </summary>
    private MpvPropertyRead<bool>? _voConfigured;
    public MpvPropertyRead<bool> VoConfigured => _voConfigured ??= new(this, "vo-configured");

    /// <summary>
    /// Contains introspection about the VO's active render passes and their execution times. Not implemented by all VOs.
    /// Fresh passes have to be uploaded, scaled, etc.
    /// </summary>
    private VideoOutputPassProperties? _voPassFresh;
    public VideoOutputPassProperties VoPassFresh => _voPassFresh ??= new(this, "vo-passes/fresh");

    /// <summary>
    /// Contains introspection about the VO's active render passes and their execution times. Not implemented by all VOs.
    /// Redraw passes have to be re-painted.
    /// </summary>
    private VideoOutputPassProperties? _voPassRedraw;
    public VideoOutputPassProperties VoPassRedraw => _voPassRedraw ??= new(this, "vo-passes/redraw");

    /// <summary>
    /// Bitrate values calculated on the packet level. This works by dividing the bit size of all packets between two keyframes by their presentation timestamp distance. (This uses the timestamps are stored in the file, so e.g. playback speed does not influence the returned values.) In particular, the video bitrate will update only per keyframe, and show the "past" bitrate. To make the property more UI friendly, updates to these properties are throttled in a certain way.
    /// The unit is bits per second.OSD formatting turns these values in kilobits(or megabits, if appropriate), which can be prevented by using the raw property value, e.g.with ${=video-bitrate}.
    /// </summary>
    private MpvPropertyRead<long>? _videoBitrate;
    public MpvPropertyRead<long> VideoBitrate => _videoBitrate ??= new(this, "video-bitrate");

    /// <summary>
    /// Bitrate values calculated on the packet level. This works by dividing the bit size of all packets between two keyframes by their presentation timestamp distance. (This uses the timestamps are stored in the file, so e.g. playback speed does not influence the returned values.) In particular, the video bitrate will update only per keyframe, and show the "past" bitrate. To make the property more UI friendly, updates to these properties are throttled in a certain way.
    /// The unit is bits per second.OSD formatting turns these values in kilobits(or megabits, if appropriate), which can be prevented by using the raw property value, e.g.with ${=video-bitrate}.
    /// </summary>
    private MpvPropertyRead<long>? _audioBitrate;
    public MpvPropertyRead<long> AudioBitrate => _audioBitrate ??= new(this, "audio-bitrate");

    /// <summary>
    /// Bitrate values calculated on the packet level. This works by dividing the bit size of all packets between two keyframes by their presentation timestamp distance. (This uses the timestamps are stored in the file, so e.g. playback speed does not influence the returned values.) In particular, the video bitrate will update only per keyframe, and show the "past" bitrate. To make the property more UI friendly, updates to these properties are throttled in a certain way.
    /// The unit is bits per second.OSD formatting turns these values in kilobits(or megabits, if appropriate), which can be prevented by using the raw property value, e.g.with ${=video-bitrate}.
    /// </summary>
    private MpvPropertyRead<long>? _subBitrate;
    public MpvPropertyRead<long> SubBitrate => _subBitrate ??= new(this, "sub-bitrate");

    /// <summary>
    /// Return the list of discovered audio devices. Reflects what --audio-device=help with the command line player returns.
    /// </summary>
    private MpvPropertyReadRef<IList<AudioDeviceInfo>>? _audioDeviceList;
    public MpvPropertyReadRef<IList<AudioDeviceInfo>> AudioDeviceList => _audioDeviceList ??= new(this, "audio-device-list");

    /// <summary>
    /// Current video output driver (name as used with --vo).
    /// </summary>
    private MpvPropertyReadString? _currentVideoOutput;
    public MpvPropertyReadString CurrentVideoOutput => _currentVideoOutput ??= new(this, "current-vo");

    /// <summary>
    /// Current audio output driver (name as used with --ao).
    /// </summary>
    private MpvPropertyReadString? _currentAudioOutput;
    public MpvPropertyReadString CurrentAudioOutput => _currentAudioOutput ??= new(this, "current-ao");

    /// <summary>
    /// Return the working directory of the mpv process.
    /// </summary>
    private MpvPropertyReadString? _workingDirectory;
    public MpvPropertyReadString WorkingDirectory => _workingDirectory ??= new(this, "working-directory");

    /// <summary>
    /// List of protocol prefixes potentially recognized by the player. They are returned without trailing :// suffix (which is still always required). In some cases, the protocol will not actually be supported (consider https if ffmpeg is not compiled with TLS support).
    /// </summary>
    private MpvPropertyReadRef<IList<string>>? _protocolList;
    public MpvPropertyReadRef<IList<string>> ProtocolList => _protocolList ??= new(this, "protocol-list");

    /// <summary>
    /// List of decoders supported. This lists decoders which can be passed to --vd and --ad.
    /// </summary>
    private MpvPropertyReadRef<IList<DecoderInfo>>? _decoderList;
    public MpvPropertyReadRef<IList<DecoderInfo>> DecoderList => _decoderList ??= new(this, "decoder-list");

    /// <summary>
    /// List of libavcodec encoders. The encoder names (driver entries) can be passed to --ovc and --oac (without the lavc: prefix required by --vd and --ad).
    /// </summary>
    private MpvPropertyReadRef<IList<DecoderInfo>>? _encoderList;
    public MpvPropertyReadRef<IList<DecoderInfo>> EncoderList => _encoderList ??= new(this, "encoder-list");

    /// <summary>
    /// List of available libavformat demuxers' names. This can be used to check for support for a specific format or use with --demuxer-lavf-format.
    /// </summary>
    private MpvPropertyReadRef<IList<string>>? _demuxerLavfList;
    public MpvPropertyReadRef<IList<string>> DemuxerLavfList => _demuxerLavfList ??= new(this, "demuxer-lavf-list");

    /// <summary>
    /// Return the mpv version/copyright string. Depending on how the binary was built, it might contain either a release version, or just a git hash.
    /// </summary>
    private MpvPropertyReadString? _mpvVersion;
    public MpvPropertyReadString MpvVersion => _mpvVersion ??= new(this, "mpv-version");

    /// <summary>
    /// Return the configuration arguments which were passed to the build system (typically the way ./waf configure ... was invoked).
    /// </summary>
    private MpvPropertyReadString? _mpvConfiguration;
    public MpvPropertyReadString MpvConfiguration => _mpvConfiguration ??= new(this, "mpv-configuration");

    /// <summary>
    /// Return the contents of the av_version_info() API call. This is a string which identifies the build in some way, either through a release version number, or a git hash. This applies to Libav as well (the property is still named the same.) This property is unavailable if mpv is linked against older FFmpeg and Libav versions.
    /// </summary>
    private MpvPropertyReadString? _fFmpegVersion;
    public MpvPropertyReadString FFmpegVersion => _fFmpegVersion ??= new(this, "ffmpeg-version");

    /// <summary>
    /// Read-only access to value of option --name. Most options can be changed at runtime by writing to this property. Note that many options require reloading the file for changes to take effect. If there is an equivalent property, prefer setting the property instead.
    /// There shouldn't be any reason to access options/name instead of name, except in situations in which the properties have different behavior or conflicting semantics.
    /// </summary>
    private MpvPropertyIndexWriteRef<string, string>? _option;
    public MpvPropertyIndexWriteRef<string, string> Option => _option ??= new(this, "options/{0}");

    /// <summary>
    /// Similar to Option, but when setting an option through this property, the option is reset to its old value once the current file has stopped playing. Trying to write an option while no file is playing (or is being loaded) results in an error.
    /// </summary>
    private MpvPropertyIndexWriteRef<string, string>? _fileLocalOption;
    public MpvPropertyIndexWriteRef<string, string> FileLocalOption => _fileLocalOption ??= new(this, "file-local-options/{0}");

    /// <summary>
    /// Returns the name of the option.
    /// </summary>
    private MpvPropertyIndexReadRef<string, string>? _optionName;
    public MpvPropertyIndexReadRef<string, string> OptionName => _optionName ??= new(this, "option-info/{0}/name");

    /// <summary>
    /// Returns the name of the option type, like String or Integer. For many complex types, this isn't very accurate.
    /// </summary>
    private MpvPropertyIndexReadRef<string, string>? _optionType;
    public MpvPropertyIndexReadRef<string, string> OptionType => _optionType ??= new(this, "option-info/{0}/type");

    /// <summary>
    /// Return True if the option was set from the mpv command line, otherwise False. What this is set to if the option is e.g. changed at runtime is left undefined (meaning it could change in the future).
    /// </summary>
    private MpvPropertyIndexRead<string, bool>? _optionSetFromCommandLine;
    public MpvPropertyIndexRead<string, bool> OptionSetFromCommandLine => _optionSetFromCommandLine ??= new(this, "option-info/{0}/set-from-commandline");

    /// <summary>
    /// Returns True if the option was set per-file. This is the case with automatically loaded profiles, file-dir configs, and other cases. It means the option value will be restored to the value before playback start when playback ends.
    /// </summary>
    private MpvPropertyIndexRead<string, bool>? _optionSetLocally;
    public MpvPropertyIndexRead<string, bool> OptionSetLocally => _optionSetLocally ??= new(this, "option-info/{0}/set-locally");

    /// <summary>
    /// The default value of the option. May not always be available.
    /// </summary>
    private MpvPropertyIndexReadRef<string, string>? _optionDefaultValue;
    public MpvPropertyIndexReadRef<string, string> OptionDefaultValue => _optionDefaultValue ??= new(this, "option-info/{0}/default-value");

    /// <summary>
    /// Integer minimum value allowed for the option. Only available if the options are numeric, and the minimum/maximum has been set internally. It's also possible that only one of these is set.
    /// </summary>
    private MpvPropertyIndexRead<string, int>? _optionMin;
    public MpvPropertyIndexRead<string, int> OptionMin => _optionMin ??= new(this, "option-info/{0}/min");

    /// <summary>
    /// Integer minimum value allowed for the option. Only available if the options are numeric, and the minimum/maximum has been set internally. It's also possible that only one of these is set.
    /// </summary>
    private MpvPropertyIndexRead<string, int>? _optionMax;
    public MpvPropertyIndexRead<string, int> OptionMax => _optionMax ??= new(this, "option-info/{0}/max");

    /// <summary>
    /// If the option is a choice option, the possible choices. Choices that are integers may or may not be included (they can be implied by min and max). Note that options which behave like choice options, but are not actual choice options internally, may not have this info available.
    /// </summary>
    private MpvPropertyIndexReadRef<string, IList<string>>? _optionChoices;
    public MpvPropertyIndexReadRef<string, IList<string>> OptionChoices => _optionChoices ??= new(this, "option-info/{0}/choices");

    /// <summary>
    /// Return the list of top-level properties.
    /// </summary>
    private MpvPropertyReadRef<IList<string>>? _propertyList;
    public MpvPropertyReadRef<IList<string>> PropertyList => _propertyList ??= new(this, "property-list");

    /// <summary>
    /// Return the list of profiles and their contents. This is highly implementation-specific, and may change any time. Currently, it returns an array of options for each profile. Each option has a name and a value, with the value currently always being a string. Note that the options array is not a map, as order matters and duplicate entries are possible. Recursive profiles are not expanded, and show up as special profile options.
    /// </summary>
    private MpvPropertyReadRef<IList<IList<KeyValuePair<string, string>>>>? _profileList;
    public MpvPropertyReadRef<IList<IList<KeyValuePair<string, string>>>> ProfileList => _profileList ??= new(this, "profile-list");

    /// <summary>
    /// Return the list of input commands. This returns an array of maps, where each map node represents a command. This map currently only has a single entry: name for the name of the command. (This property is supposed to be a replacement for --input-cmdlist. The option dumps some more information, but it's a valid feature request to extend this property if needed.)
    /// </summary>
    private MpvPropertyReadRef<IList<CommandInfo>>? _commandList;
    public MpvPropertyReadRef<IList<CommandInfo>> CommandList => _commandList ??= new(this, "command-list");

    /// <summary>
    /// Return list of current input key bindings. This returns an array of maps, where each map node represents a binding for a single key/command.
    /// </summary>
    private MpvPropertyReadRef<IList<CommandInfo>>? _inputBindings;
    public MpvPropertyReadRef<IList<CommandInfo>> InputBindings => _inputBindings ??= new(this, "input-bindings");
}
