// ReSharper disable CommentTypo
// ReSharper disable StringLiteralTypo
namespace HanumanInstitute.LibMpv;

public partial class MpvContext
{
    /// <summary>
    /// Specify a priority list of audio languages to use. Different container formats employ different language codes. DVDs use ISO 639-1 two-letter language codes, Matroska, MPEG-TS and NUT use ISO 639-2 three-letter language codes, while OGM uses a free-form identifier. See also --aid.
    /// </summary>
    private MpvOptionList? _audioLanguage;
    public MpvOptionList AudioLanguage => _audioLanguage ??= new(this, "alang");

    /// <summary>
    /// Specify a priority list of subtitle languages to use. Different container formats employ different language codes. DVDs use ISO 639-1 two letter language codes, Matroska uses ISO 639-2 three letter language codes while OGM uses a free-form identifier. See also --sid.
    /// </summary>
    private MpvOptionList? _subLanguage;
    public MpvOptionList SubLanguage => _subLanguage ??= new(this, "slang");

    /// <summary>
    /// Specify a priority list of video languages to use. Different container formats employ different language codes. DVDs use ISO 639-1 two-letter language codes, Matroska, MPEG-TS and NUT use ISO 639-2 three-letter language codes, while OGM uses a free-form identifier. See also --vid.
    /// </summary>
    private MpvOptionList? _videoLanguage;
    public MpvOptionList VideoLanguage => _videoLanguage ??= new(this, "vlang");

    /// <summary>
    /// Selects audio channel specified by ID. 'auto' selects the default, 'no' disables audio.
    /// </summary>
    private MpvOptionWithAutoNo<int>? _audioId;
    public MpvOptionWithAutoNo<int> AudioId => _audioId ??= new(this, "aid");

    /// <summary>
    /// Display the subtitle stream specified by ID. 'auto' selects the default, 'no' disables subtitles.
    /// </summary>
    private MpvOptionWithAutoNo<int>? _subId;
    public MpvOptionWithAutoNo<int> SubId => _subId ??= new(this, "sid");

    /// <summary>
    /// Select video channel specified by ID. 'auto' selects the default, 'no' disables video.
    /// </summary>
    private MpvOptionWithAutoNo<int>? _videoId;
    public MpvOptionWithAutoNo<int> VideoId => _videoId ??= new(this, "vid");

    /// <summary>
    /// (Matroska files only) Specify the edition (set of chapters) to use, where 0 is the first. If set to 'auto' (the default), mpv will choose the first edition declared as a default, or if there is no default, the first edition defined.
    /// </summary>
    private MpvOptionWithAuto<int>? _edition;
    public MpvOptionWithAuto<int> Edition => _edition ??= new(this, "edition");

    /// <summary>
    /// Enable the default track auto-selection (default: yes). Enabling this will make the player select streams according to --aid, --alang, and others. If it is disabled, no tracks are selected. In addition, the player will not exit if no tracks are selected, and wait instead (this wait mode is similar to pausing, but the pause option is not set).
    /// </summary>
    private MpvOption<bool>? _trackAutoSelection;
    public MpvOption<bool> TrackAutoSelection => _trackAutoSelection ??= new(this, "track-auto-selection");

    /// <summary>
    /// When auto-selecting a subtitle track, select a non-forced one even if the selected audio stream matches your preferred subtitle language (default: yes). Disable this if you'd like to only show subtitles for foreign audio or onscreen text.
    /// </summary>
    private MpvOption<bool>? _subsWithMatchingAudio;
    public MpvOption<bool> SubsWithMatchingAudio => _subsWithMatchingAudio ??= new(this, "subs-with-matching-audio");

    /// <summary>
    /// Seek to given time position.
    /// The general format for times is [+|-] [[hh:] mm:]ss[.ms]. If the time is prefixed with -, the time is considered relative from the end of the file(as signaled by the demuxer/the file). A + is usually ignored(but see below).
    /// The following alternative time specifications are recognized:
    /// pp% seeks to percent position pp(0-100).
    /// #c seeks to chapter number c. (Chapters start from 1.)
    /// none resets any previously set option(useful for libmpv).
    /// </summary>
    private MpvOptionString? _start;
    public MpvOptionString Start => _start ??= new(this, "start");

    /// <summary>
    /// Stop at given time. Use --length if the time should be relative to --start. See --start for valid option values and examples.
    /// </summary>
    private MpvOptionString? _end;
    public MpvOptionString End => _end ??= new(this, "end");

    /// <summary>
    /// Stop after a given time relative to the start time. See --start for valid option values and examples.
    /// If both --end and --length are provided, playback will stop when it reaches either of the two endpoints.
    /// Obscurity note: this does not work correctly if --rebase-start-time=no, and the specified time is not an "absolute" time, as defined in the --start option description.
    /// </summary>
    private MpvOptionString? _length;
    public MpvOptionString Length => _length ??= new(this, "length");

    /// <summary>
    /// Whether to move the file start time to 00:00:00 (default: yes). This is less awkward for files which start at a random timestamp, such as transport streams. On the other hand, if there are timestamp resets, the resulting behavior can be rather weird. For this reason, and in case you are actually interested in the real timestamps, this behavior can be disabled with no.
    /// </summary>
    private MpvOption<bool>? _rebaseStartTime;
    public MpvOption<bool> RebaseStartTime => _rebaseStartTime ??= new(this, "rebase-start-time");

    /// <summary>
    /// Slow down or speed up playback by the factor given as parameter.
    /// If --audio-pitch-correction(on by default) is used, playing with a speed higher than normal automatically inserts the scaletempo2 audio filter.
    /// </summary>
	private MpvOption<double>? _speed = null;
    public MpvOption<double> Speed => _speed ??= new(this, "speed");

    /// <summary>
    /// Whether the player is in a paused state.
    /// </summary>    
	private MpvOption<bool>? _pause = null;
    public MpvOption<bool> Pause => _pause ??= new(this, "pause");

    /// <summary>
    /// Whether to play files in a random order.
    /// </summary>
    private MpvOption<bool>? _shuffle;
    public MpvOption<bool> Shuffle => _shuffle ??= new(this, "shuffle");

    /// <summary>
    /// Set which file on the internal playlist to start playback with. The index is an integer, with 0 meaning the first file. The value auto means that the selection of the entry to play is left to the playback resume mechanism (default). If an entry with the given index doesn't exist, the behavior is unspecified and might change in future mpv versions. The same applies if the playlist contains further playlists (don't expect any reasonable behavior). Passing a playlist file to mpv should work with this option, though. E.g. mpv playlist.m3u --playlist-start=123 will work as expected, as long as playlist.m3u does not link to further playlists.
    /// </summary>
    private MpvOptionWithAuto<int>? _playlistStart;
    public MpvOptionWithAuto<int> PlaylistStart => _playlistStart ??= new(this, "playlist-start");

    /// <summary>
    /// Play files according to a playlist file (Supports some common formats. If no format is detected, it will be treated as list of files, separated by newline characters. Note that XML playlist formats are not supported.)
    /// You can play playlists directly and without this option, however, this option disables any security mechanisms that might be in place.You may also need this option to load plaintext files as playlist.
    /// Do NOT use Playlist with random internet sources or files you do not trust!
    /// </summary>
    private MpvOptionString? _playlist;
    public MpvOptionString Playlist => _playlist ??= new(this, "playlist");

    /// <summary>
    /// Threshold for merging almost consecutive ordered chapter parts in milliseconds (default: 100). Some Matroska files with ordered chapters have inaccurate chapter end timestamps, causing a small gap between the end of one chapter and the start of the next one when they should match. If the end of one playback part is less than the given threshold away from the start of the next one then keep playing video normally over the chapter change instead of doing a seek.
    /// </summary>
    private MpvOption<int>? _chapterMergeThreshold;
    public MpvOption<int> ChapterMergeThreshold => _chapterMergeThreshold ??= new(this, "chapter-merge-threshold");

    /// <summary>
    /// Distance in seconds from the beginning of a chapter within which a backward chapter seek will go to the previous chapter (default: 5.0). Past this threshold, a backward chapter seek will go to the beginning of the current chapter instead. A negative value means always go back to the previous chapter.
    /// </summary>
    private MpvOption<double>? _chapterSeekThreshold;
    public MpvOption<double> ChapterSeekThreshold => _chapterSeekThreshold ??= new(this, "chapter-seek-threshold");

    /// <summary>
    /// Select when to use precise seeks that are not limited to keyframes. Such seeks require decoding video from the previous keyframe up to the target position and so can take some time depending on decoding performance. For some video formats, precise seeks are disabled. This option selects the default choice to use for seeks; it is possible to explicitly override that default in the definition of key bindings and in input commands.
    /// </summary>
    private MpvOptionEnum<HrSeekOption>? _hrSeek;
    public MpvOptionEnum<HrSeekOption> HrSeek => _hrSeek ??= new(this, "hr-seek");

    /// <summary>
    /// This option exists to work around failures to do precise seeks (as in --hr-seek) caused by bugs or limitations in the demuxers for some file formats. Some demuxers fail to seek to a keyframe before the given target position, going to a later position instead. The value of this option is subtracted from the time stamp given to the demuxer. Thus, if you set this option to 1.5 and try to do a precise seek to 60 seconds, the demuxer is told to seek to time 58.5, which hopefully reduces the chance that it erroneously goes to some time later than 60 seconds. The downside of setting this option is that precise seeks become slower, as video between the earlier demuxer position and the real target may be unnecessarily decoded.
    /// </summary>
    private MpvOption<double>? _hrSeekDemuxerOffset;
    public MpvOption<double> HrSeekDemuxerOffset => _hrSeekDemuxerOffset ??= new(this, "hr-seek-demuxer-offset");

    /// <summary>
    /// Allow the video decoder to drop frames during seek, if these frames are before the seek target. If this is enabled, precise seeking can be faster, but if you're using video filters which modify timestamps or add new frames, it can lead to precise seeking skipping the target frame. This e.g. can break frame backstepping when deinterlacing is enabled.
    /// </summary>
    private MpvOption<bool>? _hrSeekFrameDrop;
    public MpvOption<bool> HrSeekFrameDrop => _hrSeekFrameDrop ??= new(this, "hr-seek-framedrop");

    /// <summary>
    /// Controls how to seek in files. Note that if the index is missing from a file, it will be built on the fly by default, so you don't need to change this. But it might help with some broken files.
    /// This option only works if the underlying media supports seeking (i.e. not with stdin, pipe, etc).
    /// </summary>
    private MpvOptionEnum<IndexMode>? _index;
    public MpvOptionEnum<IndexMode> Index => _index ??= new(this, "index");

    /// <summary>
    /// Load URLs from playlists which are considered unsafe (default: no). This includes special protocols and anything that doesn't refer to normal files. Local files and HTTP links on the other hand are always considered safe.
    /// In addition, if a playlist is loaded while this is set, the added playlist entries are not marked as originating from network or potentially unsafe location. (Instead, the behavior is as if the playlist entries were provided directly to mpv command line or loadfile command.)
    /// </summary>
    private MpvOption<bool>? _loadUnsafePlaylists;
    public MpvOption<bool> LoadUnsafePlaylists => _loadUnsafePlaylists ??= new(this, "load-unsafe-playlists");

    /// <summary>
    /// Follow any references in the file being opened (default: yes). Disabling this is helpful if the file is automatically scanned (e.g. thumbnail generation). If the thumbnail scanner for example encounters a playlist file, which contains network URLs, and the scanner should not open these, enabling this option will prevent it. This option also disables ordered chapters, mov reference files, opening of archives, and a number of other features.
    /// </summary>
    private MpvOption<bool>? _accessReferences;
    public MpvOption<bool> AccessReferences => _accessReferences ??= new(this, "access-references");

    /// <summary>
    /// Loops playback N times. A value of 1 plays it one time (default), 2 two times, etc. inf means forever. no is the same as 1 and disables looping. If several files are specified on command line, the entire playlist is looped. --loop-playlist is the same as --loop-playlist=inf.
    /// The force mode is like inf, but does not skip playlist entries which have been marked as failing.This means the player might waste CPU time trying to loop a file that doesn't exist. But it might be useful for playing webradios under very bad network conditions.
    /// </summary>
    private MpvOptionString? _loopPlaylist;
    public MpvOptionString LoopPlaylist => _loopPlaylist ??= new(this, "loop-playlist");

    /// <summary>
    /// Loop a single file N times. inf means forever, no means normal playback. For compatibility, --loop-file and --loop-file=yes are also accepted, and are the same as --loop-file=inf.
    /// The difference to --loop-playlist is that this doesn't loop the playlist, just the file itself. If the playlist contains only a single file, the difference between the two option is that this option performs a seek on loop, instead of reloading the file.
    /// </summary>
    private MpvOptionString? _loopFile;
    public MpvOptionString LoopFile => _loopFile ??= new(this, "loop-file");

    /// <summary>
    /// Set loop points. If playback passes the b timestamp, it will seek to the a timestamp. Seeking past the b point doesn't loop (this is intentional). If either options are set to no (or unset), looping is disabled.
    /// </summary>
    private MpvOptionString? _abLoopA;
    public MpvOptionString AbLoopA => _abLoopA ??= new(this, "ab-loop-a");

    /// <summary>
    /// Set loop points. If playback passes the b timestamp, it will seek to the a timestamp. Seeking past the b point doesn't loop (this is intentional). If either options are set to no (or unset), looping is disabled.
    /// </summary>
    private MpvOptionString? _abLoopB;
    public MpvOptionString AbLoopB => _abLoopB ??= new(this, "ab-loop-b");

    /// <summary>
    /// Run A-B loops only N times, then ignore the A-B loop points (default: inf). Every finished loop iteration will decrement this option by 1 (unless it is set to inf or 0). inf means that looping goes on forever. If this option is set to 0, A-B looping is ignored, and even the ab-loop command will not enable looping again (the command will show (disabled) on the OSD message if both loop points are set, but ab-loop-count is 0).
    /// </summary>
    private MpvOptionString? _abLoopCount;
    public MpvOptionString AbLoopCount => _abLoopCount ??= new(this, "ab-loop-count");

    /// <summary>
    /// Enabled by default. Whether to use Matroska ordered chapters. mpv will not load or search for video segments from other files, and will also ignore any chapter order specified for the main file.
    /// </summary>
    private MpvOption<bool>? _orderedChapters;
    public MpvOption<bool> OrderedChapters => _orderedChapters ??= new(this, "ordered-chapters");

    /// <summary>
    /// Loads the given file as playlist, and tries to use the files contained in it as reference files when opening a Matroska file that uses ordered chapters. This overrides the normal mechanism for loading referenced files by scanning the same directory the main file is located in.
    /// Useful for loading ordered chapter files that are not located on the local filesystem, or if the referenced files are in different directories.
    /// Note: a playlist can be as simple as a text file containing filenames separated by newlines.
    /// </summary>
    private MpvOptionString? _orderedChaptersFiles;
    public MpvOptionString OrderedChaptersFiles => _orderedChaptersFiles ??= new(this, "ordered-chapters-files");

    /// <summary>
    /// Load chapters from this file, instead of using the chapter metadata found in the main file.
    /// This accepts a media file(like mkv) or even a pseudo-format like ffmetadata and uses its chapters to replace the current file's chapters. This doesn't work with OGM or XML chapters directly.
    /// </summary>
    private MpvOptionString? _chaptersFiles;
    public MpvOptionString ChaptersFiles => _chaptersFiles ??= new(this, "chapters-file");

    /// <summary>
    /// Skip n seconds after every frame. Note: Without --hr-seek, skipping will snap to keyframes.
    /// </summary>
    private MpvOption<double>? _skipStep;
    public MpvOption<double> SkipStep => _skipStep ??= new(this, "sstep");

    /// <summary>
    /// Stop playback if either audio or video fails to initialize (default: no). With no, playback will continue in video-only or audio-only mode if one of them fails. This doesn't affect playback of audio-only or video-only files.
    /// </summary>
    private MpvOption<bool>? _stopPlaybackOnInitFailure;
    public MpvOption<bool> StopPlaybackOnInitFailure => _stopPlaybackOnInitFailure ??= new(this, "stop-playback-on-init-failure");

    /// <summary>
    /// Control the playback direction (default: forward). Setting backward will attempt to play the file in reverse direction, with decreasing playback time. If this is set on playback starts, playback will start from the end of the file. If this is changed at during playback, a hr-seek will be issued to change the direction.
    /// + and - are aliases for forward and backward.
    /// </summary>
    private MpvOptionString? _playDir;
    public MpvOptionString PlayDir => _playDir ??= new(this, "play-dir");

    /// <summary>
    /// For backward decoding. Backward decoding decodes forward in steps, and then reverses the decoder output. These options control the approximate maximum amount of bytes that can be buffered. The main use of this is to avoid unbounded resource usage; during normal backward playback, it's not supposed to hit the limit, and if it does, it will drop frames and complain about it.
    /// </summary>
    private MpvOption<int>? _videoReversalBuffer;
    public MpvOption<int> VideoReversalBuffer => _videoReversalBuffer ??= new(this, "video-reversal-buffer");

    /// <summary>
    /// For backward decoding. Backward decoding decodes forward in steps, and then reverses the decoder output. These options control the approximate maximum amount of bytes that can be buffered. The main use of this is to avoid unbounded resource usage; during normal backward playback, it's not supposed to hit the limit, and if it does, it will drop frames and complain about it.
    /// </summary>
    private MpvOption<int>? _audioReversalBuffer;
    public MpvOption<int> AudioReversalBuffer => _audioReversalBuffer ??= new(this, "audio-reversal-buffer");

    /// <summary>
    /// Number of overlapping keyframe ranges to use for backward decoding (default: auto) ("keyframe" to be understood as in the mpv/ffmpeg specific meaning). Backward decoding works by forward decoding in small steps. Some codecs cannot restart decoding from any packet (even if it's marked as seek point), which becomes noticeable with backward decoding (in theory this is a problem with seeking too, but --hr-seek-demuxer-offset can fix it for seeking). In particular, MDCT based audio codecs are affected.
    /// </summary>
    private MpvOptionWithAuto<int>? _videoBackwardOverlap;
    public MpvOptionWithAuto<int> VideoBackwardOverlap => _videoBackwardOverlap ??= new(this, "video-backward-overlap");

    /// <summary>
    /// Number of overlapping keyframe ranges to use for backward decoding (default: auto) ("keyframe" to be understood as in the mpv/ffmpeg specific meaning). Backward decoding works by forward decoding in small steps. Some codecs cannot restart decoding from any packet (even if it's marked as seek point), which becomes noticeable with backward decoding (in theory this is a problem with seeking too, but --hr-seek-demuxer-offset can fix it for seeking). In particular, MDCT based audio codecs are affected.
    /// </summary>
    private MpvOptionWithAuto<int>? _audioBackwardOverlap;
    public MpvOptionWithAuto<int> AudioBackwardOverlap => _audioBackwardOverlap ??= new(this, "audio-backward-overlap");

    /// <summary>
    /// Number of keyframe ranges to decode at once when backward decoding (default: 1 for video, 10 for audio). Another pointless tuning parameter nobody should use. This should affect performance only. In theory, setting a number higher than 1 for audio will reduce overhead due to less frequent backstep operations and less redundant decoding work due to fewer decoded overlap frames (see --audio-backward-overlap). On the other hand, it requires a larger reversal buffer, and could make playback less smooth due to breaking pipelining (e.g. by decoding a lot, and then doing nothing for a while).
    /// It probably never makes sense to set --video-backward-batch.But in theory, it could help with intra-only video codecs by reducing backstep operations.
    /// </summary>
    private MpvOption<int>? _videoBackwardBatch;
    public MpvOption<int> VideoBackwardBatch => _videoBackwardBatch ??= new(this, "video-backward-batch");

    /// <summary>
    /// Number of keyframe ranges to decode at once when backward decoding (default: 1 for video, 10 for audio). Another pointless tuning parameter nobody should use. This should affect performance only. In theory, setting a number higher than 1 for audio will reduce overhead due to less frequent backstep operations and less redundant decoding work due to fewer decoded overlap frames (see --audio-backward-overlap). On the other hand, it requires a larger reversal buffer, and could make playback less smooth due to breaking pipelining (e.g. by decoding a lot, and then doing nothing for a while).
    /// It probably never makes sense to set --video-backward-batch.But in theory, it could help with intra-only video codecs by reducing backstep operations.
    /// </summary>
    private MpvOption<int>? _audioBackwardBatch;
    public MpvOption<int> AudioBackwardBatch => _audioBackwardBatch ??= new(this, "audio-backward-batch");

    /// <summary>
    /// Number of seconds the demuxer should seek back to get new packets during backward playback (default: 60). This is useful for tuning backward playback, see --play-dir for details.
    /// Setting this to a very low value or 0 may make the player think seeking is broken, or may make it perform multiple seeks.
    /// Setting this to a high value may lead to quadratic runtime behavior.
    /// </summary>
    private MpvOption<double>? _demuxerBackwardPlaybackStep;
    public MpvOption<double> DemuxerBackwardPlaybackStep => _demuxerBackwardPlaybackStep ??= new(this, "demuxer-backward-playback-step");

    /// <summary>
    /// Opens the given path for writing, and print log messages to it. Existing files will be truncated. The log level is at least -v -v, but can be raised via --msg-level (the option cannot lower it below the forced minimum log level).
    /// A special case is the macOS bundle, it will create a log file at ~/Library/Logs/mpv.log by default.
    /// </summary>
    private MpvOptionString? _logFile;
    public MpvOptionString LogFile => _logFile ??= new(this, "log-file");

    /// <summary>
    /// Force a different configuration directory. If this is set, the given directory is used to load configuration files, and all other configuration directories are ignored. This means the global mpv configuration directory as well as per-user directories are ignored, and overrides through environment variables (MPV_HOME) are also ignored.
    /// Note that the --no-config option takes precedence over this option.
    /// </summary>
    private MpvOptionString? _configDir = null;
    public MpvOptionString ConfigDir => _configDir ??= new(this, "config-dir");



    private MpvOption<bool>? _config = null;
    public MpvOption<bool> Config => _config ??= new(this, "config");

    /// <summary>
    /// Always save the current playback position on quit. When this file is played again later, the player will seek to the old playback position on start. This does not happen if playback of a file is stopped in any other way than quitting. For example, going to the next file in the playlist will not save the position, and start playback at beginning the next time the file is played.
    /// This behavior is disabled by default, but is always available when quitting the player with Shift+Q.
    /// </summary>
    private MpvOption<bool>? _savePositionOnQuit;
    public MpvOption<bool> SavePositionOnQuit => _savePositionOnQuit ??= new(this, "save-position-on-quit");

    /// <summary>
    /// The directory in which to store the "watch later" temporary files.
    /// The default is a subdirectory named "watch_later" underneath the config directory(usually ~/.config/mpv/).
    /// </summary>
    private MpvOptionString? _watchLaterDirectory;
    public MpvOptionString WatchLaterDirectory => _watchLaterDirectory ??= new(this, "watch-later-directory");

    /// <summary>
    /// Write certain statistics to the given file. The file is truncated on opening. The file will contain raw samples, each with a timestamp. To make this file into a readable, the script TOOLS/stats-conv.py can be used (which currently displays it as a graph).
    /// This option is useful for debugging only.
    /// </summary>
    private MpvOptionString? _dumpStats;
    public MpvOptionString DumpStats => _dumpStats ??= new(this, "dump-stats");

    /// <summary>
    /// Yes|No|Once. Makes mpv wait idly instead of quitting when there is no file to play. Mostly useful in input mode, where mpv can be controlled through input commands. (Default: no)
    /// Once will only idle at start and let the player close once the first playlist has finished playing back.
    /// </summary>
    private MpvOptionString? _idleAfterPlay;
    public MpvOptionString IdleAfterPlay => _idleAfterPlay ??= new(this, "idle");

    /// <summary>
    /// Specify configuration file to be parsed after the default ones.
    /// </summary>
    private MpvOptionString? _include;
    public MpvOptionString Include => _include ??= new(this, "include");

    /// <summary>
    /// If set to False, don't auto-load scripts from the scripts configuration subdirectory (usually ~/.config/mpv/scripts/). (Default: True)
    /// </summary>
    private MpvOption<bool>? _loadScripts;
    public MpvOption<bool> LoadScripts => _loadScripts ??= new(this, "load-scripts");

    /// <summary>
    /// Load a Lua script.
    /// </summary>
    private MpvOptionString? _script;
    public MpvOptionString Script => _script ??= new(this, "script");

    /// <summary>
    /// Load multiple scripts by separating them with the path separator (: on Unix, ; on Windows).
    /// </summary>
    private MpvOptionList? _scripts;
    public MpvOptionList Scripts => _scripts ??= new(this, "scripts");

    /// <summary>
    /// Set options for scripts. A script can query an option by key. If an option is used and what semantics the option value has depends entirely on the loaded scripts. Values not claimed by any scripts are ignored.
    /// </summary>
    private MpvOptionDictionary? _scriptOptions;
    public MpvOptionDictionary ScriptOptions => _scriptOptions ??= new(this, "script-opts");

    /// <summary>
    /// Pretend that all files passed to mpv are concatenated into a single, big file. This uses timeline/EDL support internally.
    /// </summary>
    private MpvOption<bool>? _mergeFiles;
    public MpvOption<bool> MergeFiles => _mergeFiles ??= new(this, "merge-files");

    /// <summary>
    /// Whether to restore playback position from the watch_later configuration subdirectory (usually ~/.config/mpv/watch_later/). See quit-watch-later input command.
    /// </summary>
    private MpvOption<bool>? _resumePlayback;
    public MpvOption<bool> ResumePlayback => _resumePlayback ??= new(this, "resume-playback");

    /// <summary>
    /// Only restore the playback position from the watch_later configuration subdirectory (usually ~/.config/mpv/watch_later/) if the file's modification time is the same as at the time of saving. This may prevent skipping forward in files with the same name which have different content. (Default: False)
    /// </summary>
    private MpvOption<bool>? _resumePlaybackCheckMTime;
    public MpvOption<bool> ResumePlaybackCheckMTime => _resumePlaybackCheckMTime ??= new(this, "resume-playback-check-mtime");

    /// <summary>
    /// Use the given profile(s), --profile=help displays a list of the defined profiles.
    /// </summary>
    private MpvOptionList? _profile;
    public MpvOptionList Profile => _profile ??= new(this, "profile");

    /// <summary>
    /// Normally, mpv will try to keep all settings when playing the next file on the playlist, even if they were changed by the user during playback. (This behavior is the opposite of MPlayer's, which tries to reset all settings when starting next file.)
    /// Default: Do not reset anything.
    /// This can be changed with this option.It accepts a list of options, and mpv will reset the value of these options on playback start to the initial value. The initial value is either the default value, or as set by the config file or command line.
    /// In some cases, this might not work as expected.For example, --volume will only be reset if it is explicitly set in the config file or the command line.
    /// The special name 'all' resets as many options as possible.
    /// </summary>
    private MpvOptionList? _resetOnNextFile;
    public MpvOptionList ResetOnNextFile => _resetOnNextFile ??= new(this, "reset-on-next-file");

    /// <summary>
    /// Prepend the watch later config files with the name of the file they refer to. This is simply written as comment on the top of the file.
    /// This option may expose privacy-sensitive information and is thus disabled by default.
    /// </summary>
    private MpvOption<bool>? _writeFilenameInWatchLaterConfig;
    public MpvOption<bool> WriteFilenameInWatchLaterConfig => _writeFilenameInWatchLaterConfig ??= new(this, "write-filename-in-watch-later-config");

    /// <summary>
    /// Ignore path (i.e. use filename only) when using watch later feature. (Default: False)
    /// </summary>
    private MpvOption<bool>? _ignorePathInWatchLaterConfig;
    public MpvOption<bool> IgnorePathInWatchLaterConfig => _ignorePathInWatchLaterConfig ??= new(this, "ignore-path-in-watch-later-config");

    /// <summary>
    /// Show the description and content of a profile. Lists all profiles if no parameter is provided.
    /// </summary>
    private MpvOptionString? _showProfile;
    public MpvOptionString ShowProfile => _showProfile ??= new(this, "show-profile");

    /// <summary>
    /// Look for a file-specific configuration file in the same directory as the file that is being played.
    /// Warning: May be dangerous if playing from untrusted media.
    /// </summary>
    private MpvOption<bool>? _useFileDirConf;
    public MpvOption<bool> UseFileDirConf => _useFileDirConf ??= new(this, "use-filedir-conf");

    /// <summary>
    /// Enable the youtube-dl hook-script. It will look at the input URL, and will play the video located on the website. This works with many streaming sites, not just the one that the script is named after. This requires a recent version of youtube-dl to be installed on the system. (Enabled by default.)
    /// If the script can't do anything with an URL, it will do nothing.
    /// This accepts a set of options, which can be passed to it with the ScriptOpts option(using ytdl_hook- as prefix):
    /// </summary>
    private MpvOption<bool>? _youTubeDl;
    public MpvOption<bool> YouTubeDl => _youTubeDl ??= new(this, "ytdl");

    /// <summary>
    /// yes/no. If 'yes' will try parsing the URL with youtube-dl first, instead of the default where it's only after mpv failed to open it. This mostly depends on whether most of your URLs need youtube-dl parsing.
    /// </summary>
    private MpvScriptOption? _youTubeDlTryFirst;
    public MpvScriptOption YouTubeDlTryFirst => _youTubeDlTryFirst ??= new(this, "ytdl_hook-try_ytdl_first");

    /// <summary>
    /// A |-separated list of URL patterns which mpv should not use with youtube-dl. The patterns are matched after the http(s):// part of the URL.
    /// ^ matches the beginning of the URL, $ matches its end, and you should use % before any of the characters ^$()%|,.[]*+-? to match that character.
    /// </summary>
    private MpvScriptOption? _youTubeDlExclude;
    public MpvScriptOption YouTubeDlExclude => _youTubeDlExclude ??= new(this, "ytdl_hook-exclude");

    /// <summary>
    /// yes/no. If 'yes' will attempt to add all formats found reported by youtube-dl (default: no). Each format is added as a separate track. In addition, they are delay-loaded, and actually opened only when a track is selected (this should keep load times as low as without this option).
    /// It adds average bitrate metadata, if available, which means you can use --hls-bitrate to decide which track to select. (HLS used to be the only format whose alternative quality streams were exposed in a similar way, thus the option name.)
    /// </summary>
    private MpvScriptOption? _youTubeDlAllFormats;
    public MpvScriptOption YouTubeDlAllFormats => _youTubeDlAllFormats ??= new(this, "ytdl_hook-all_formats");

    /// <summary>
    /// yes/no. If set to 'yes', and all_formats is also set to 'yes', this will try to represent all youtube-dl reported formats as tracks, even if mpv would normally use the direct URL reported by it (default: yes).
    /// It appears this normally makes a difference if youtube-dl works on a master HLS playlist.
    /// If this is set to 'no', this specific kind of stream is treated like all_formats is set to 'no', and the stream selection as done by youtube-dl (via --ytdl-format) is used.
    /// </summary>
    private MpvScriptOption? _youTubeDlForceAllFormats;
    public MpvScriptOption YouTubeDlForceAllFormats => _youTubeDlForceAllFormats ??= new(this, "ytdl_hook-force_all_formats");

    /// <summary>
    /// yes/no. Make mpv use the master manifest URL for formats like HLS and DASH, if available, allowing for video/audio selection in runtime (default: no). It's disabled ("no") by default for performance reasons.
    /// </summary>
    private MpvScriptOption? _youTubeDlUseManifests;
    public MpvScriptOption YouTubeDlUseManifests => _youTubeDlUseManifests ??= new(this, "ytdl_hook-use_manifests");

    /// <summary>
    /// Configure path to youtube-dl executable or a compatible fork's. The default "youtube-dl" looks for the executable in PATH. In a Windows environment the suffix extension ".exe" is always appended.
    /// </summary>
    private MpvScriptOption? _youTubeDlPath;
    public MpvScriptOption YouTubeDlPath => _youTubeDlPath ??= new(this, "ytdl_hook-ytdl_path");

    /// <summary>
    /// ytdl|best|worst|mp4|webm|... Video format/quality that is directly passed to youtube-dl. The possible values are specific to the website and the video, for a given url the available formats can be found with the command youtube-dl --list-formats URL. See youtube-dl's documentation for available aliases. (Default: bestvideo+bestaudio/best)
    /// </summary>
    private MpvOptionString? _youTubeDlFormat;
    public MpvOptionString YouTubeDlFormat => _youTubeDlFormat ??= new(this, "ytdl-format");

    /// <summary>
    /// Pass arbitrary options to youtube-dl. Parameter and argument should be passed as a key-value pair.
    /// </summary>
    private MpvOptionDictionary? _youTubeDlRawOptions;
    public MpvOptionDictionary YouTubeDlRawOptions => _youTubeDlRawOptions ??= new(this, "ytdl-raw-options");

    /// <summary>
    /// Enable the builtin script that shows useful playback information on a key binding (default: True). By default, the i key is used (I to make the overlay permanent).
    /// </summary>
    private MpvOption<bool>? _loadStatsOverlay;
    public MpvOption<bool> LoadStatsOverlay => _loadStatsOverlay ??= new(this, "load-stats-overlay");

    /// <summary>
    /// Enable the builtin script that shows a console on a key binding and lets you enter commands (default: True). By default, the ´ key is used to show the console, and ESC to hide it again. (This is based on a user script called repl.lua.)
    /// </summary>
    private MpvOption<bool>? _loadOsdConsole;
    public MpvOption<bool> LoadOsdConsole => _loadOsdConsole ??= new(this, "load-osd-console");

    /// <summary>
    /// Enable the builtin script that does auto profiles (default: auto). See Conditional auto profiles for details. auto will load the script, but immediately unload it if there are no conditional profiles.
    /// </summary>
    private MpvOptionWithAuto<bool>? _loadAutoProfiles;
    public MpvOptionWithAuto<bool> LoadAutoProfiles => _loadAutoProfiles ??= new(this, "load-auto-profiles");

    /// <summary>
    /// cplayer|pseudo-gui. For enabling "pseudo GUI mode", which means that the defaults for some options are changed. This option should not normally be used directly, but only by mpv internally, or mpv-provided scripts, config files, or .desktop files. See PSEUDO GUI MODE for details.
    /// </summary>
    private MpvOptionString? _playerOperationMode;
    public MpvOptionString PlayerOperationMode => _playerOperationMode ??= new(this, "player-operation-mode");

    /// <summary>
    /// Specify the video output backend to be used. See VIDEO OUTPUT DRIVERS for details and descriptions of available drivers.
    /// </summary>
    private MpvOptionString? _videoOutput;
    public MpvOptionString VideoOutput => _videoOutput ??= new(this, "vo");

    /// <summary>
    /// Specify a priority list of video decoders to be used, according to their family and name. See --ad for further details. Both of these options use the same syntax and semantics; the only difference is that they operate on different codec lists.
    /// </summary>
    private MpvOptionString? _videoDecoder;
    public MpvOptionString VideoDecoder => _videoDecoder ??= new(this, "vd");

    /// <summary>
    /// Specify a list of video filters to apply to the video stream. See VIDEO FILTERS for details and descriptions of the available filters.
    /// </summary>
    private MpvOptionList? _videoFilters;
    public MpvOptionList VideoFilters => _videoFilters ??= new(this, "vf");

    /// <summary>
    /// Do not sleep when outputting video frames. Useful for benchmarks when used with --no-audio.
    /// </summary>
    private MpvOption<bool>? _untimed;
    public MpvOption<bool> Untimed => _untimed ??= new(this, "untimed");

    /// <summary>
    /// Skip displaying some frames to maintain A/V sync on slow systems, or playing high framerate video on video outputs that have an upper framerate limit.
    /// </summary>
    private MpvOptionEnum<FrameDropOption>? _frameDrop;
    public MpvOptionEnum<FrameDropOption> FrameDrop => _frameDrop ??= new(this, "framedrop");

    /// <summary>
    /// Enable some things which tend to reduce video latency by 1 or 2 frames (default: no). Note that this option might be removed without notice once the player's timing code does not inherently need to do these things anymore.
    /// </summary>
    private MpvOption<bool>? _videoLatencyHacks;
    public MpvOption<bool> VideoLatencyHacks => _videoLatencyHacks ??= new(this, "video-latency-hacks");

    /// <summary>
    /// Set the display FPS used with the --video-sync=display-* modes. By default, a detected value is used. Keep in mind that setting an incorrect value (even if slightly incorrect) can ruin video playback. On multi-monitor systems, there is a chance that the detected value is from the wrong monitor.
    /// Set this option only if you have reason to believe the automatically determined value is wrong.
    /// </summary>
    private MpvOption<double>? _overrideDisplayFps;
    public MpvOption<double> OverrideDisplayFps => _overrideDisplayFps ??= new(this, "override-display-fps");

    /// <summary>
    /// Specify the hardware video decoding API that should be used if possible. Whether hardware decoding is actually done depends on the video codec. If hardware decoding is not possible, mpv will fall back on software decoding.
    /// Hardware decoding is not enabled by default, because it is typically an additional source of errors.It is worth using only if your CPU is too slow to decode a specific video.
    /// </summary>
    private MpvOptionString? _hardwareDecoder;
    public MpvOptionString HardwareDecoder => _hardwareDecoder ??= new(this, "hwdec");

    /// <summary>
    /// This option is for troubleshooting hwdec interop issues. Since it's a debugging option, its semantics may change at any time.
    /// </summary>
    private MpvOptionString? _hardwareDecoderGpuInterop;
    public MpvOptionString HardwareDecoderGpuInterop => _hardwareDecoderGpuInterop ??= new(this, "gpu-hwdec-interop");

    /// <summary>
    /// Number of GPU frames hardware decoding should preallocate (default: see --list-options output). If this is too low, frame allocation may fail during decoding, and video frames might get dropped and/or corrupted. Setting it too high simply wastes GPU memory and has no advantages.
    /// </summary>
    private MpvOption<int>? _hardwareDecoderExtraFrames;
    public MpvOption<int> HardwareDecoderExtraFrames => _hardwareDecoderExtraFrames ??= new(this, "hwdec-extra-frames");

    /// <summary>
    /// Set the internal pixel format used by hardware decoding via --hwdec (default no). The special value no selects an implementation specific standard format. Most decoder implementations support only one format, and will fail to initialize if the format is not supported.
    /// Some implementations might support multiple formats. In particular, videotoolbox is known to require uyvy422 for good performance on some older hardware. d3d11va can always use yuv420p, which uses an opaque format, with likely no advantages.
    /// </summary>
    private MpvOptionString? _hardwareDecoderImageFormat;
    public MpvOptionString HardwareDecoderImageFormat => _hardwareDecoderImageFormat ??= new(this, "hwdec-image-format");

    /// <summary>
    /// Choose the GPU device used for decoding when using the cuda or nvdec hwdecs with the OpenGL GPU backend, and with the cuda-copy or nvdec-copy hwdecs in all cases.
    /// </summary>
    private MpvOptionWithAuto<int>? _hardwareDecoderCudaDevice;
    public MpvOptionWithAuto<int> HardwareDecoderCudaDevice => _hardwareDecoderCudaDevice ??= new(this, "cuda-decode-device");

    /// <summary>
    /// Choose the DRM device for vaapi-copy. This should be the path to a DRM device file. (Default: /dev/dri/renderD128)
    /// </summary>
    private MpvOptionString? _hardwareDecoderVaapiDevice;
    public MpvOptionString HardwareDecoderVaapiDevice => _hardwareDecoderVaapiDevice ??= new(this, "vaapi-device");

    /// <summary>
    /// Enables pan-and-scan functionality (cropping the sides of e.g. a 16:9 video to make it fit a 4:3 display without black bands). The range controls how much of the image is cropped. May not work with all video output drivers.
    /// This option has no effect if --video-unscaled option is used.
    /// </summary>
    private MpvOption<double>? _panScan;
    public MpvOption<double> PanScan => _panScan ??= new(this, "panscan");

    /// <summary>
    /// Override video aspect ratio, in case aspect information is incorrect or missing in the file being played.
    /// These values have special meaning:
    /// 0:	disable aspect ratio handling, pretend the video has square pixels
    /// no:	same as 0
    /// -1:	use the video stream or container aspect(default)
    /// But note that handling of these special values might change in the future.
    /// </summary>
    private MpvOptionWithNo<int>? _videoAspectOverride;
    public MpvOptionWithNo<int> VideoAspectOverride => _videoAspectOverride ??= new(this, "video-aspect-override");

    /// <summary>
    /// This sets the default video aspect determination method (if the aspect is _not_ overridden by the user with --video-aspect-override or others).
    /// </summary>
    private MpvOptionEnum<VideoAspectMethod>? _videoAspectMethod;
    public MpvOptionEnum<VideoAspectMethod> VideoAspectMethod => _videoAspectMethod ??= new(this, "video-aspect-method");

    /// <summary>
    /// Disable scaling of the video. If the window is larger than the video, black bars are added. Otherwise, the video is cropped, unless the option is set to downscale-big, in which case the video is fit to window. The video still can be influenced by the other --video-... options. This option disables the effect of --panscan.
    /// Note that the scaler algorithm may still be used, even if the video isn't scaled. For example, this can influence chroma conversion. The video will also still be scaled in one dimension if the source uses non-square pixels (e.g. anamorphic widescreen DVDs).
    /// This option is disabled if the --no-keepaspect option is used.
    /// </summary>
    private MpvOptionEnum<VideoUnscaledOption>? _videoUnscaled;
    public MpvOptionEnum<VideoUnscaledOption> VideoUnscaled => _videoUnscaled ??= new(this, "video-unscaled");

    /// <summary>
    /// Moves the displayed video rectangle by the given value in the X direction. The unit is in fractions of the size of the scaled video (the full size, even if parts of the video are not visible due to panscan or other options).
    /// </summary>
    private MpvOption<double>? _videoPanX;
    public MpvOption<double> VideoPanX => _videoPanX ??= new(this, "video-pan-x");

    /// <summary>
    /// Moves the displayed video rectangle by the given value in the Y direction. The unit is in fractions of the size of the scaled video (the full size, even if parts of the video are not visible due to panscan or other options).
    /// </summary>
    private MpvOption<double>? _videoPanY;
    public MpvOption<double> VideoPanY => _videoPanY ??= new(this, "video-pan-y");

    /// <summary>
    /// Rotate the video clockwise, in degrees. Currently supports 90° steps only. If no is given, the video is never rotated, even if the file has rotation metadata. (The rotation value is added to the rotation metadata, which means the value 0 would rotate the video according to the rotation metadata.)
    /// </summary>
    private MpvOptionWithNo<int>? _videoRotate;
    public MpvOptionWithNo<int> VideoRotate => _videoRotate ??= new(this, "video-rotate");

    /// <summary>
    /// Adjust the video display scale factor by the given value. The parameter is given log 2. For example, --video-zoom=0 is unscaled, --video-zoom=1 is twice the size, --video-zoom=-2 is one fourth of the size, and so on.
    /// </summary>
    private MpvOption<double>? _videoZoom;
    public MpvOption<double> VideoZoom => _videoZoom ??= new(this, "video-zoom");

    /// <summary>
    /// Multiply the video display size with the given value (default: 1.0). If a non-default value is used, this will be different from the window size, so video will be either cut off, or black bars are added.
    /// This value is multiplied with the value derived from --video-zoom and the normal video aspect aspect ratio.This option is disabled if the --no-keepaspect option is used.
    /// </summary>
    private MpvOption<double>? _videoScaleX;
    public MpvOption<double> VideoScaleX => _videoScaleX ??= new(this, "video-scale-x");

    /// <summary>
    /// Multiply the video display size with the given value (default: 1.0). If a non-default value is used, this will be different from the window size, so video will be either cut off, or black bars are added.
    /// This value is multiplied with the value derived from --video-zoom and the normal video aspect aspect ratio.This option is disabled if the --no-keepaspect option is used.
    /// </summary>
    private MpvOption<double>? _videoScaleY;
    public MpvOption<double> VideoScaleY => _videoScaleY ??= new(this, "video-scale-y");

    /// <summary>
    /// Moves the video rectangle within the black borders, which are usually added to pad the video to screen if video and screen aspect ratios are different. --video-align-y=-1 would move the video to the top of the screen (leaving a border only on the bottom), a value of 0 centers it (default), and a value of 1 would put the video at the bottom of the screen.
    /// If video and screen aspect match perfectly, these options do nothing.
    /// </summary>
    private MpvOption<double>? _videoAlignX;
    public MpvOption<double> VideoAlignX => _videoAlignX ??= new(this, "video-align-x");

    /// <summary>
    /// Moves the video rectangle within the black borders, which are usually added to pad the video to screen if video and screen aspect ratios are different. --video-align-y=-1 would move the video to the top of the screen (leaving a border only on the bottom), a value of 0 centers it (default), and a value of 1 would put the video at the bottom of the screen.
    /// If video and screen aspect match perfectly, these options do nothing.
    /// </summary>
    private MpvOption<double>? _videoAlignY;
    public MpvOption<double> VideoAlignY => _videoAlignY ??= new(this, "video-align-y");

    /// <summary>
    /// Set extra video margins on each border (default: 0). Each value is a ratio of the window size, using a range 0.0-1.0. For example, setting the option --video-margin-ratio-right=0.2 at a window size of 1000 pixels will add a 200 pixels border on the right side of the window.
    /// </summary>
    private MpvOption<double>? _videoMarginRatioLeft;
    public MpvOption<double> VideoMarginRatioLeft => _videoMarginRatioLeft ??= new(this, "video-margin-ratio-left");

    /// <summary>
    /// Set extra video margins on each border (default: 0). Each value is a ratio of the window size, using a range 0.0-1.0. For example, setting the option --video-margin-ratio-right=0.2 at a window size of 1000 pixels will add a 200 pixels border on the right side of the window.
    /// </summary>
    private MpvOption<double>? _videoMarginRatioRight;
    public MpvOption<double> VideoMarginRatioRight => _videoMarginRatioRight ??= new(this, "video-margin-ratio-right");

    /// <summary>
    /// Set extra video margins on each border (default: 0). Each value is a ratio of the window size, using a range 0.0-1.0. For example, setting the option --video-margin-ratio-right=0.2 at a window size of 1000 pixels will add a 200 pixels border on the right side of the window.
    /// </summary>
    private MpvOption<double>? _videoMarginRatioTop;
    public MpvOption<double> VideoMarginRatioTop => _videoMarginRatioTop ??= new(this, "video-margin-ratio-top");

    /// <summary>
    /// Set extra video margins on each border (default: 0). Each value is a ratio of the window size, using a range 0.0-1.0. For example, setting the option --video-margin-ratio-right=0.2 at a window size of 1000 pixels will add a 200 pixels border on the right side of the window.
    /// </summary>
    private MpvOption<double>? _videoMarginRatioBottom;
    public MpvOption<double> VideoMarginRatioBottom => _videoMarginRatioBottom ??= new(this, "video-margin-ratio-bottom");

    /// <summary>
    /// False switches mpv to a mode where video timing is determined using a fixed framerate value (either using the --fps option, or using file information). Sometimes, files with very broken timestamps can be played somewhat well in this mode. Note that video filters, subtitle rendering, seeking (including hr-seeks and backstepping), and audio synchronization can be completely broken in this mode.
    /// </summary>
    private MpvOption<bool>? _correctPts;
    public MpvOption<bool> CorrectPts => _correctPts ??= new(this, "correct-pts");

    /// <summary>
    /// Override video framerate. Useful if the original value is wrong or missing.
    /// Works in --no-correct-pts mode only.
    /// </summary>
    private MpvOption<double>? _fps;
    public MpvOption<double> Fps => _fps ??= new(this, "fps");

    /// <summary>
    /// Enable or disable interlacing (default: no). Interlaced video shows ugly comb-like artifacts, which are visible on fast movement. Enabling this typically inserts the yadif video filter in order to deinterlace the video, or lets the video output apply deinterlacing if supported.
    /// </summary>
    private MpvOption<bool>? _deinterlace;
    public MpvOption<bool> Deinterlace => _deinterlace ??= new(this, "deinterlace");

    /// <summary>
    /// Play/convert only first number video frames, then quit.
    /// --frames=0 loads the file, but immediately quits before initializing playback. (Might be useful for scripts which just want to determine some file properties.)
    /// For audio-only playback, any value greater than 0 will quit playback immediately after initialization.The value 0 works as with video.
    /// </summary>
    private MpvOption<int>? _frames;
    public MpvOption<int> Frames => _frames ??= new(this, "frames");

    /// <summary>
    /// RGB color levels used with YUV to RGB conversion. Normally, output devices such as PC monitors use full range color levels. However, some TVs and video monitors expect studio RGB levels. Providing full range output to a device expecting studio level input results in crushed blacks and whites, the reverse in dim gray blacks and dim whites.
    /// </summary>
    private MpvOptionEnum<VideoOutputLevels>? _videoOutputLevels;
    public MpvOptionEnum<VideoOutputLevels> VideoOutputLevels => _videoOutputLevels ??= new(this, "video-output-levels");

    /// <summary>
    /// Allow hardware decoding for a given list of codecs only. The special value all always allows all codecs.
    /// </summary>
    private MpvOptionString? _hardwareDecoderCodecs;
    public MpvOptionString HardwareDecoderCodecs => _hardwareDecoderCodecs ??= new(this, "hwdec-codecs");

    /// <summary>
    /// Check hardware decoder profile (default: yes). If no is set, the highest profile of the hardware decoder is unconditionally selected, and decoding is forced even if the profile of the video is higher than that. The result is most likely broken decoding, but may also help if the detected or reported profiles are somehow incorrect.
    /// </summary>
    private MpvOption<bool>? _vdLavcCheckHwProfile;
    public MpvOption<bool> VdLavcCheckHwProfile => _vdLavcCheckHwProfile ??= new(this, "vd-lavc-check-hw-profile");

    /// <summary>
    /// Fallback to software decoding if the hardware-accelerated decoder fails (default: 3). If this is a number, then fallback will be triggered if N frames fail to decode in a row. 1 is equivalent to yes.
    /// Setting this to a higher number might break the playback start fallback: if a fallback happens, parts of the file will be skipped, approximately by to the number of packets that could not be decoded. Values below an unspecified count will not have this problem, because mpv retains the packets.
    /// </summary>
    private MpvOptionWithYesNo<int>? _vdLavcSoftwareFallback;
    public MpvOptionWithYesNo<int> VdLavcSoftwareFallback => _vdLavcSoftwareFallback ??= new(this, "vd-lavc-software-fallback");

    /// <summary>
    /// Enable direct rendering (default: yes). If this is set to yes, the video will be decoded directly to GPU video memory (or staging buffers). This can speed up video upload, and may help with large resolutions or slow hardware.
    /// </summary>
    private MpvOption<bool>? _vdLavcDr;
    public MpvOption<bool> VdLavcDr => _vdLavcDr ??= new(this, "vd-lavc-dr");

    /// <summary>
    /// Only use bit-exact algorithms in all decoding steps (for codec testing).
    /// </summary>
    private MpvOption<bool>? _vdLavcBitExact;
    public MpvOption<bool> VdLavcBitExact => _vdLavcBitExact ??= new(this, "vd-lavc-bitexact");

    /// <summary>
    /// Enable optimizations (MPEG-2, MPEG-4, and H.264 only) which do not comply with the format specification and potentially cause problems, like simpler dequantization, simpler motion compensation, assuming use of the default quantization matrix, assuming YUV 4:2:0 and skipping a few checks to detect damaged bitstreams.
    /// </summary>
    private MpvOption<bool>? _vdLavcFast;
    public MpvOption<bool> VdLavcFast => _vdLavcFast ??= new(this, "vd-lavc-fast");

    /// <summary>
    /// Pass AVOptions to libavcodec decoder. Note, a patch to make the o= unneeded and pass all unknown options through the AVOption system is welcome. A full list of AVOptions can be found in the FFmpeg manual.
    /// Some options which used to be direct options can be set with this mechanism, like bug, gray, idct, ec, vismv, skip_top (was st), skip_bottom (was sb), debug.
    /// </summary>
    private MpvOptionDictionary? _vdLavcOptions;
    public MpvOptionDictionary VdLavcOptions => _vdLavcOptions ??= new(this, "vd-lavc-o");

    /// <summary>
    /// Show even broken/corrupt frames (default: no). If this option is set to no, libavcodec won't output frames that were either decoded before an initial keyframe was decoded, or frames that are recognized as corrupted.
    /// </summary>
    private MpvOption<bool>? _vdLavcShowAll;
    public MpvOption<bool> VdLavcShowAll => _vdLavcShowAll ??= new(this, "vd-lavc-show-all");

    /// <summary>
    /// Skips the loop filter (AKA deblocking) during H.264 decoding. Since the filtered frame is supposed to be used as reference for decoding dependent frames, this has a worse effect on quality than not doing deblocking on e.g. MPEG-2 video. But at least for high bitrate HDTV, this provides a big speedup with little visible quality loss.
    /// </summary>
    private MpvOptionEnum<SkipFilterOption>? _vdLavcSkipLoopFilter;
    public MpvOptionEnum<SkipFilterOption> VdLavcSkipLoopFilter => _vdLavcSkipLoopFilter ??= new(this, "vd-lavc-skiploopfilter");

    /// <summary>
    /// Skips the IDCT step. This degrades quality a lot in almost all cases.
    /// </summary>
    private MpvOptionEnum<SkipFilterOption>? _vdLavcSkipIdct;
    public MpvOptionEnum<SkipFilterOption> VdLavcSkipIdct => _vdLavcSkipIdct ??= new(this, "vd-lavc-skipidct");

    /// <summary>
    /// Skips decoding of frames completely. Big speedup, but jerky motion and sometimes bad artifacts.
    /// </summary>
    private MpvOptionEnum<SkipFilterOption>? _vdLavcSkipFrame;
    public MpvOptionEnum<SkipFilterOption> VdLavcSkipFrame => _vdLavcSkipFrame ??= new(this, "vd-lavc-skipframe");

    /// <summary>
    /// Set framedropping mode used with --framedrop.
    /// </summary>
    private MpvOptionEnum<SkipFilterOption>? _vdLavcFrameDrop;
    public MpvOptionEnum<SkipFilterOption> VdLavcFrameDrop => _vdLavcFrameDrop ??= new(this, "vd-lavc-framedrop");

    /// <summary>
    /// Number of threads to use for decoding. Whether threading is actually supported depends on codec (default: 0). 0 means autodetect number of cores on the machine and use that, up to the maximum of 16. You can set more than 16 threads manually.
    /// </summary>
    private MpvOption<int>? _vdLavcThreads;
    public MpvOption<int> VdLavcThreads => _vdLavcThreads ??= new(this, "vd-lavc-threads");

    /// <summary>
    /// Assume the video was encoded by an old, buggy x264 version (default: no). Normally, this is autodetected by libavcodec. But if the bitstream contains no x264 version info (or it was somehow skipped), and the stream was in fact encoded by an old x264 version (build 150 or earlier), and if the stream uses 4:4:4 chroma, then libavcodec will by default show corrupted video. This option sets the libavcodec x264_build option to 150, which means that if the stream contains no version info, or was not encoded by x264 at all, it assumes it was encoded by the old version. Enabling this option is pretty safe if you want your broken files to work, but in theory this can break on streams not encoded by x264, or if a stream encoded by a newer x264 version contains no version info.
    /// </summary>
    private MpvOption<bool>? _vdLavcAssumeOldX264;
    public MpvOption<bool> VdLavcAssumeOldX264 => _vdLavcAssumeOldX264 ??= new(this, "vd-lavc-assume-old-x264");

    /// <summary>
    /// Allow up to N in-flight frames. This essentially controls the frame latency. Increasing the swapchain depth can improve pipelining and prevent missed vsyncs, but increases visible latency. This option only mandates an upper limit, the implementation can use a lower latency than requested internally. A setting of 1 means that the VO will wait for every frame to become visible before starting to render the next frame. (Default: 3)
    /// </summary>
    private MpvOption<int>? _swapchainDepth;
    public MpvOption<int> SwapchainDepth => _swapchainDepth ??= new(this, "swapchain-depth");

    /// <summary>
    /// If this is enabled (default), playing with a speed different from normal automatically inserts the scaletempo audio filter. For details, see audio filter section.
    /// </summary>
    private MpvOption<bool>? _audioPitchCorrection;
    public MpvOption<bool> AudioPitchCorrection => _audioPitchCorrection ??= new(this, "audio-pitch-correction");

    /// <summary>
    /// Use the given audio device. This consists of the audio output name, e.g. alsa, followed by /, followed by the audio output specific device name. The default value for this option is auto, which tries every audio output in preference order with the default device.
    /// </summary>
    private MpvOptionString? _audioDevice;
    public MpvOptionString AudioDevice => _audioDevice ??= new(this, "audio-device");

    /// <summary>
    /// Enable exclusive output mode. In this mode, the system is usually locked out, and only mpv will be able to output audio.
    /// </summary>
    private MpvOption<bool>? _audioExclusive;
    public MpvOption<bool> AudioExclusive => _audioExclusive ??= new(this, "audio-exclusive");

    /// <summary>
    /// If no audio device can be opened, behave as if --ao=null was given. This is useful in combination with --audio-device: instead of causing an error if the selected device does not exist, the client API user (or a Lua script) could let playback continue normally, and check the current-ao and audio-device-list properties to make high-level decisions about how to continue.
    /// </summary>
    private MpvOption<bool>? _audioFallbackToNull;
    public MpvOption<bool> AudioFallbackToNull => _audioFallbackToNull ??= new(this, "audio-fallback-to-null");

    /// <summary>
    /// Specify the audio output drivers to be used. See AUDIO OUTPUT DRIVERS for details and descriptions of available drivers.
    /// </summary>
    private MpvOptionString? _audioOutput;
    public MpvOptionString AudioOutput => _audioOutput ??= new(this, "ao");

    /// <summary>
    /// Specify a list of audio filters to apply to the audio stream. See AUDIO FILTERS for details and descriptions of the available filters.
    /// </summary>
    private MpvOptionList? _audioFilters;
    public MpvOptionList AudioFilters => _audioFilters ??= new(this, "af");

    /// <summary>
    /// List of codecs for which compressed audio passthrough should be used. This works for both classic S/PDIF and HDMI.
    /// </summary>
    private MpvOptionString? _audioSpdif;
    public MpvOptionString AudioSpdif => _audioSpdif ??= new(this, "audio-spdif");

    /// <summary>
    /// Specify a priority list of audio decoders to be used, according to their decoder name. When determining which decoder to use, the first decoder that matches the audio format is selected. If that is unavailable, the next decoder is used. Finally, it tries all other decoders that are not explicitly selected or rejected by the option.
    /// </summary>
    private MpvOptionString? _audioDecoders;
    public MpvOptionString AudioDecoders => _audioDecoders ??= new(this, "ad");

    /// <summary>
    /// Set the startup volume. 0 means silence, 100 means no volume reduction or amplification. Negative values can be passed for compatibility, but are treated as 0.
    /// </summary>
    private MpvOption<double>? _volume;
    public MpvOption<double> Volume => _volume ??= new(this, "volume");

    /// <summary>
    /// Adjust volume gain according to replaygain values stored in the file metadata.
    /// </summary>
    private MpvOptionEnum<ReplayGainOption>? _replayGain;
    public MpvOptionEnum<ReplayGainOption> ReplayGain => _replayGain ??= new(this, "replaygain");

    /// <summary>
    /// Pre-amplification gain in dB to apply to the selected replaygain gain (default: 0).
    /// </summary>
    private MpvOption<double>? _replayGainPreamp;
    public MpvOption<double> ReplayGainPreamp => _replayGainPreamp ??= new(this, "replaygain-preamp");

    /// <summary>
    /// Prevent clipping caused by replaygain by automatically lowering the gain (default). Use --replaygain-clip=no to disable this.
    /// </summary>
    private MpvOption<bool>? _replayGainClip;
    public MpvOption<bool> ReplayGainClip => _replayGainClip ??= new(this, "replaygain-clip");

    /// <summary>
    /// Gain in dB to apply if the file has no replay gain tags. This option is always applied if the replaygain logic is somehow inactive. If this is applied, no other replaygain options are applied.
    /// </summary>
    private MpvOption<double>? _replayGainFallback;
    public MpvOption<double> ReplayGainFallback => _replayGainFallback ??= new(this, "replaygain-fallback");

    /// <summary>
    /// Audio delay in seconds (positive or negative float value). Positive values delay the audio, and negative values delay the video.
    /// </summary>
    private MpvOption<double>? _audioDelay;
    public MpvOption<double> AudioDelay => _audioDelay ??= new(this, "audio-delay");

    /// <summary>
    /// Set startup audio mute status (default: False).
    /// </summary>
    private MpvOption<bool>? _mute;
    public MpvOption<bool> Mute => _mute ??= new(this, "mute");

    /// <summary>
    /// Use this audio demuxer type when using --audio-file. Use a '+' before the name to force it; this will skip some checks. Give the demuxer name as printed by --audio-demuxer=help.
    /// </summary>
    private MpvOptionString? _audioDemuxer;
    public MpvOptionString AudioDemuxer => _audioDemuxer ??= new(this, "audio-demuxer");

    /// <summary>
    /// Select the Dynamic Range Compression level for AC-3 audio streams. level is a float value ranging from 0 to 1, where 0 means no compression (which is the default) and 1 means full compression (make loud passages more silent and vice versa). Values up to 6 are also accepted, but are purely experimental. This option only shows an effect if the AC-3 stream contains the required range compression information.
    /// The standard mandates that DRC is enabled by default, but mpv(and some other players) ignore this for the sake of better audio quality.
    /// </summary>
    private MpvOption<double>? _adLavcAc3Drc;
    public MpvOption<double> AdLavcAc3Drc => _adLavcAc3Drc ??= new(this, "ad-lavc-ac3drc");

    /// <summary>
    /// Whether to request audio channel downmixing from the decoder (default: no). Some decoders, like AC-3, AAC and DTS, can remix audio on decoding. The requested number of output channels is set with the --audio-channels option. Useful for playing surround audio on a stereo system.
    /// </summary>
    private MpvOption<bool>? _adLavcDownmix;
    public MpvOption<bool> AdLavcDownmix => _adLavcDownmix ??= new(this, "ad-lavc-downmix");

    /// <summary>
    /// Number of threads to use for decoding. Whether threading is actually supported depends on codec. As of this writing, it's supported for some lossless codecs only. 0 means autodetect number of cores on the machine and use that, up to the maximum of 16 (default: 1).
    /// </summary>
    private MpvOption<int>? _adLavcThreads;
    public MpvOption<int> AdLavcThreads => _adLavcThreads ??= new(this, "ad-lavc-threads");

    /// <summary>
    /// Pass AVOptions to libavcodec decoder.
    /// </summary>
    private MpvOptionDictionary? _adLavcOptions;
    public MpvOptionDictionary AdLavcOptions => _adLavcOptions ??= new(this, "ad-lavc-o");

    /// <summary>
    /// Control which audio channels are output (e.g. surround vs. stereo).
    /// </summary>
    private MpvOptionString? _audioChannels;
    public MpvOptionString AudioChannels => _audioChannels ??= new(this, "audio-channels");

    /// <summary>
    /// Setting this option to attachment (default) will display image attachments (e.g. album cover art) when playing audio files. It will display the first image found, and additional images are available as video tracks.
    /// Setting this option to no disables display of video entirely when playing audio files.
    /// </summary>
    private MpvOptionEnum<AudioDisplayOption>? _audioDisplay;
    public MpvOptionEnum<AudioDisplayOption> AudioDisplay => _audioDisplay ??= new(this, "audio-display");

    /// <summary>
    /// Play audio from an external file while viewing a video.
    /// </summary>
    private MpvOptionList? _audioFiles;
    public MpvOptionList AudioFiles => _audioFiles ??= new(this, "audio-files");

    /// <summary>
    /// Select the sample format used for output from the audio filter layer to the sound card. The values that format can adopt are listed below in the description of the format audio filter.
    /// </summary>
    private MpvOptionString? _audioFormat;
    public MpvOptionString AudioFormat => _audioFormat ??= new(this, "audio-format");

    /// <summary>
    /// Select the output sample rate to be used (of course sound cards have limits on this). If the sample frequency selected is different from that of the current media, the lavrresample audio filter will be inserted into the audio filter layer to compensate for the difference.
    /// </summary>
    private MpvOption<int>? _audioSampleRate;
    public MpvOption<int> AudioSampleRate => _audioSampleRate ??= new(this, "audio-samplerate");

    /// <summary>
    /// Try to play consecutive audio files with no silence or disruption at the point of file change. Default: weak.
    /// </summary>
    private MpvOptionEnum<GaplessAudioOption>? _gaplessAudio;
    public MpvOptionEnum<GaplessAudioOption> GaplessAudio => _gaplessAudio ??= new(this, "gapless-audio");

    /// <summary>
    /// When starting a video file or after events such as seeking, mpv will by default modify the audio stream to make it start from the same timestamp as video, by either inserting silence at the start or cutting away the first samples. Disabling this option makes the player behave like older mpv versions did: video and audio are both started immediately even if their start timestamps differ, and then video timing is gradually adjusted if necessary to reach correct synchronization later.
    /// </summary>
    private MpvOption<bool>? _initialAudioSync;
    public MpvOption<bool> InitialAudioSync => _initialAudioSync ??= new(this, "initial-audio-sync");

    /// <summary>
    /// Set the maximum amplification level in percent (default: 130). A value of 130 will allow you to adjust the volume up to about double the normal level.
    /// </summary>
    private MpvOption<double>? _volumeMax;
    public MpvOption<double> VolumeMax => _volumeMax ??= new(this, "volume-max");

    /// <summary>
    /// Load additional audio files matching the video filename. The parameter specifies how external audio files are matched.
    /// </summary>
    private MpvOptionEnum<FileAutoLoadOption>? _audioFileAuto;
    public MpvOptionEnum<FileAutoLoadOption> AudioFileAuto => _audioFileAuto ??= new(this, "audio-file-auto");

    /// <summary>
    /// Equivalent to --sub-file-paths option, but for auto-loaded audio files.
    /// </summary>
    private MpvOptionList? _audioFilePaths;
    public MpvOptionList AudioFilePaths => _audioFilePaths ??= new(this, "audio-file-paths");

    /// <summary>
    /// The application name the player reports to the audio API. Can be useful if you want to force a different audio profile (e.g. with PulseAudio), or to set your own application name when using libmpv.
    /// </summary>
    private MpvOptionString? _audioClientName;
    public MpvOptionString AudioClientName => _audioClientName ??= new(this, "audio-client-name");

    /// <summary>
    /// Set the audio output minimum buffer. The audio device might actually create a larger buffer if it pleases. If the device creates a smaller buffer, additional audio is buffered in an additional software buffer.
    /// Making this larger will make soft-volume and other filters react slower, introduce additional issues on playback speed change, and block the player on audio format changes. A smaller buffer might lead to audio dropouts.
    /// This option should be used for testing only. If a non-default value helps significantly, the mpv developers should be contacted.
    /// Default: 0.2 (200 ms).
    /// </summary>
    private MpvOption<double>? _audioBuffer;
    public MpvOption<double> AudioBuffer => _audioBuffer ??= new(this, "audio-buffer");

    /// <summary>
    /// Cash-grab consumer audio hardware (such as A/V receivers) often ignore initial audio sent over HDMI. This can happen every time audio over HDMI is stopped and resumed. In order to compensate for this, you can enable this option to not to stop and restart audio on seeks, and fill the gaps with silence. Likewise, when pausing playback, audio is not stopped, and silence is played while paused. Note that if no audio track is selected, the audio device will still be closed immediately.
    /// This modifies certain subtle player behavior, like A/V-sync and underrun handling. Enabling this option is strongly discouraged.
    /// </summary>
    private MpvOption<bool>? _audioStreamSilence;
    public MpvOption<bool> AudioStreamSilence => _audioStreamSilence ??= new(this, "audio-stream-silence");

    /// <summary>
    /// This makes sense for use with --audio-stream-silence=yes. If this option is given, the player will wait for the given amount of seconds after opening the audio device before sending actual audio data to it. Useful if your expensive hardware discards the first 1 or 2 seconds of audio data sent to it. If --audio-stream-silence=yes is not set, this option will likely just waste time.
    /// </summary>
    private MpvOption<double>? _audioWaitOpen;
    public MpvOption<double> AudioWaitOpen => _audioWaitOpen ??= new(this, "audio-wait-open");

    /// <summary>
    /// Force subtitle demuxer type for --sub-file. Give the demuxer name as printed by --sub-demuxer=help.
    /// </summary>
    private MpvOptionString? _subDemuxer;
    public MpvOptionString SubDemuxer => _subDemuxer ??= new(this, "sub-demuxer");

    /// <summary>
    /// Delays subtitles by sec seconds. Can be negative.
    /// </summary>
    private MpvOption<double>? _subDelay;
    public MpvOption<double> SubDelay => _subDelay ??= new(this, "sub-delay");

    /// <summary>
    /// Add a subtitle file to the list of external subtitles.
    /// </summary>
    private MpvOptionList? _subFiles;
    public MpvOptionList SubFiles => _subFiles ??= new(this, "sub-files");

    /// <summary>
    /// Select a secondary subtitle stream. This is similar to --sid. If a secondary subtitle is selected, it will be rendered as toptitle (i.e. on the top of the screen) alongside the normal subtitle, and provides a way to render two subtitles at once.
    /// </summary>
    private MpvOptionWithAutoNo<int>? _subIdSecondary;
    public MpvOptionWithAutoNo<int> SubIdSecondary => _subIdSecondary ??= new(this, "secondary-sid");

    /// <summary>
    /// Factor for the text subtitle font size (default: 1).
    /// This affects ASS subtitles as well, and may lead to incorrect subtitle rendering. Use with care, or use --sub-font-size instead.
    /// </summary>
    private MpvOption<int>? _subScale;
    public MpvOption<int> SubScale => _subScale ??= new(this, "sub-scale");

    /// <summary>
    /// Whether to scale subtitles with the window size (default: yes). If this is disabled, changing the window size won't change the subtitle font size.
    /// Like --sub-scale, this can break ASS subtitles.
    /// </summary>
    private MpvOption<bool>? _subScaleByWindow;
    public MpvOption<bool> SubScaleByWindow => _subScaleByWindow ??= new(this, "sub-scale-by-window");

    /// <summary>
    /// Make the subtitle font size relative to the window, instead of the video. This is useful if you always want the same font size, even if the video doesn't cover the window fully, e.g. because screen aspect and window aspect mismatch (and the player adds black bars).
    /// Default: yes.
    /// </summary>
    private MpvOption<bool>? _subScaleWithWindow;
    public MpvOption<bool> SubScaleWithWindow => _subScaleWithWindow ??= new(this, "sub-scale-with-window");

    /// <summary>
    /// Like --sub-scale-with-window, but affects subtitles in ASS format only. Like --sub-scale, this can break ASS subtitles.
    /// Default: no.
    /// </summary>
    private MpvOption<bool>? _subAssScaleWithWindow;
    public MpvOption<bool> SubAssScaleWithWindow => _subAssScaleWithWindow ??= new(this, "sub-ass-scale-with-window");

    /// <summary>
    /// Use fonts embedded in Matroska container files and ASS scripts (default: yes). These fonts can be used for SSA/ASS subtitle rendering.
    /// </summary>
    private MpvOption<bool>? _embeddedFonts;
    public MpvOption<bool> EmbeddedFonts => _embeddedFonts ??= new(this, "embeddedfonts");

    /// <summary>
    /// Specify the position of subtitles on the screen. The value is the vertical position of the subtitle in % of the screen height. 100 is the original position, which is often not the absolute bottom of the screen, but with some margin between the bottom and the subtitle. Values above 100 move the subtitle further down.
    /// </summary>
    private MpvOption<int>? _subPos;
    public MpvOption<int> SubPos => _subPos ??= new(this, "sub-pos");

    /// <summary>
    /// Multiply the subtitle event timestamps with the given value. Can be used to fix the playback speed for frame-based subtitle formats. Affects text subtitles only.
    /// </summary>
    private MpvOption<double>? _subSpeed;
    public MpvOption<double> SubSpeed => _subSpeed ??= new(this, "sub-speed");

    /// <summary>
    /// Override some style or script info parameters.
    /// </summary>
    private MpvOptionList? _subAssForceStyle;
    public MpvOptionList SubAssForceStyle => _subAssForceStyle ??= new(this, "sub-ass-force-style");

    /// <summary>
    /// Set font hinting type.
    /// Enabling hinting can lead to mispositioned text (in situations it's supposed to match up video background), or reduce the smoothness of animations with some badly authored ASS scripts. It is recommended to not use this option, unless really needed.
    /// </summary>
    private MpvOptionEnum<SubAssHinting>? _subAssHinting;
    public MpvOptionEnum<SubAssHinting> SubAssHinting => _subAssHinting ??= new(this, "sub-ass-hinting");

    /// <summary>
    /// Set line spacing value for SSA/ASS renderer.
    /// </summary>
    private MpvOption<double>? _subAssLineSpacing;
    public MpvOption<double> SubAssLineSpacing => _subAssLineSpacing ??= new(this, "sub-ass-line-spacing");

    /// <summary>
    /// Set the text layout engine used by libass.
    /// Complex is the default. If libass hasn't been compiled against HarfBuzz, libass silently reverts to simple.
    /// </summary>
    private MpvOptionEnum<SubAssShaper>? _subAssShaper;
    public MpvOptionEnum<SubAssShaper> SubAssShaper => _subAssShaper ??= new(this, "sub-ass-shaper");

    /// <summary>
    /// Load all SSA/ASS styles found in the specified file and use them for rendering text subtitles. The syntax of the file is exactly like the [V4 Styles] / [V4+ Styles] section of SSA/ASS.
    /// </summary>
    private MpvOptionString? _subAssStyles;
    public MpvOptionString SubAssStyles => _subAssStyles ??= new(this, "sub-ass-styles");

    /// <summary>
    /// Control whether user style overrides should be applied. Note that all of these overrides try to be somewhat smart about figuring out whether or not a subtitle is considered a "sign".
    /// This also controls some bitmap subtitle overrides, as well as HTML tags in formats like SRT, despite the name of the option.
    /// </summary>
    private MpvOptionEnum<SubAssOverride>? _subAssOverride;
    public MpvOptionEnum<SubAssOverride> SubAssOverride => _subAssOverride ??= new(this, "sub-ass-override");

    /// <summary>
    /// Enables placing toptitles and subtitles in black borders when they are available, if the subtitles are in the ASS format.
    /// Default: no.
    /// </summary>
    private MpvOption<bool>? _subAssForceMargins;
    public MpvOption<bool> SubAssForceMargins => _subAssForceMargins ??= new(this, "sub-ass-force-margins");

    /// <summary>
    /// Enables placing toptitles and subtitles in black borders when they are available, if the subtitles are in a plain text format (or ASS if --sub-ass-override is set high enough).
    /// Default: yes.
    /// </summary>
    private MpvOption<bool>? _subAssUseMargins;
    public MpvOption<bool> SubAssUseMargins => _subAssUseMargins ??= new(this, "sub-use-margins");

    /// <summary>
    /// Stretch SSA/ASS subtitles when playing anamorphic videos for compatibility with traditional VSFilter behavior. This switch has no effect when the video is stored with square pixels.
    /// </summary>
    private MpvOption<bool>? _subAssVsFilterAspectCompact;
    public MpvOption<bool> SubAssVsFilterAspectCompact => _subAssVsFilterAspectCompact ??= new(this, "sub-ass-vsfilter-aspect-compat");

    /// <summary>
    /// Scale \blur tags by video resolution instead of script resolution (enabled by default). This is bug in VSFilter, which according to some, can't be fixed anymore in the name of compatibility.
    /// Note that this uses the actual video resolution for calculating the offset scale factor, not what the video filter chain or the video output use.
    /// </summary>
    private MpvOption<bool>? _subAssVsFilterBlurCompat;
    public MpvOption<bool> SubAssVsFilterBlurCompat => _subAssVsFilterBlurCompat ??= new(this, "sub-ass-vsfilter-blur-compat");

    /// <summary>
    /// Mangle colors like (xy-)vsfilter do (default: basic). Historically, VSFilter was not color space aware. This was no problem as long as the color space used for SD video (BT.601) was used. But when everything switched to HD (BT.709), VSFilter was still converting RGB colors to BT.601, rendered them into the video frame, and handled the frame to the video output, which would use BT.709 for conversion to RGB. The result were mangled subtitle colors. Later on, bad hacks were added on top of the ASS format to control how colors are to be mangled.
    /// </summary>
    private MpvOptionEnum<SubAssVsFilterColor>? _subAssVsFilterColorCompat;
    public MpvOptionEnum<SubAssVsFilterColor> SubAssVsFilterColorCompat => _subAssVsFilterColorCompat ??= new(this, "sub-ass-vsfilter-color-compat");

    /// <summary>
    /// Stretch DVD subtitles when playing anamorphic videos for better looking fonts on badly mastered DVDs. This switch has no effect when the video is stored with square pixels - which for DVD input cannot be the case though.
    /// Many studios tend to use bitmap fonts designed for square pixels when authoring DVDs, causing the fonts to look stretched on playback on DVD players.This option fixes them, however at the price of possibly misaligning some subtitles(e.g.sign translations).
    /// Disabled by default.
    /// </summary>
    private MpvOption<bool>? _stretchDvdSubs;
    public MpvOption<bool> StretchDvdSubs => _stretchDvdSubs ??= new(this, "stretch-dvd-subs");

    /// <summary>
    /// Stretch DVD and other image subtitles to the screen, ignoring the video margins. This has a similar effect as --sub-use-margins for text subtitles, except that the text itself will be stretched, not only just repositioned. (At least in general it is unavoidable, as an image bitmap can in theory consist of a single bitmap covering the whole screen, and the player won't know where exactly the text parts are located.)
    /// This option does not display subtitles correctly.Use with care.
    /// Disabled by default.
    /// </summary>
    private MpvOption<bool>? _stretchImageSubsToScreen;
    public MpvOption<bool> StretchImageSubsToScreen => _stretchImageSubsToScreen ??= new(this, "stretch-image-subs-to-screen");

    /// <summary>
    /// Override the image subtitle resolution with the video resolution (default: no). Normally, the subtitle canvas is fit into the video canvas (e.g. letterboxed). Setting this option uses the video size as subtitle canvas size. Can be useful to test broken subtitles, which often happen when the video was trancoded, while attempting to keep the old subtitles.
    /// </summary>
    private MpvOption<bool>? _imageSubsVideoResolution;
    public MpvOption<bool> ImageSubsVideoResolution => _imageSubsVideoResolution ??= new(this, "image-subs-video-resolution");

    /// <summary>
    /// Load additional subtitle files matching the video filename. The parameter specifies how external subtitle files are matched. exact is enabled by default.
    /// </summary>
    private MpvOptionEnum<FileAutoLoadOption>? _subAuto;
    public MpvOptionEnum<FileAutoLoadOption> SubAuto => _subAuto ??= new(this, "sub-auto");

    /// <summary>
    /// You can use this option to specify the subtitle codepage. uchardet will be used to guess the charset. (If mpv was not compiled with uchardet, then utf-8 is the effective default.)
    /// The default value for this option is auto, which enables autodetection.
    /// </summary>
    private MpvOptionString? _subCodepage;
    public MpvOptionString SubCodepage => _subCodepage ??= new(this, "sub-codepage");

    /// <summary>
    /// Adjust subtitle timing is to remove minor gaps or overlaps between subtitles (if the difference is smaller than 210 ms, the gap or overlap is removed).
    /// </summary>
    private MpvOption<bool>? _subFixTiming;
    public MpvOption<bool> SubFixTiming => _subFixTiming ??= new(this, "sub-fix-timing");

    /// <summary>
    /// Display only forced subtitles for the DVD subtitle stream selected by e.g. --slang (default: auto). When set to auto, enabled when the --subs-with-matching-audio option is on and a non-forced stream is selected. Enabling this will hide all subtitles in streams that don't make a distinction between forced and unforced events within a stream.
    /// </summary>
    private MpvOptionWithAuto<bool>? _subForcedOnly;
    public MpvOptionWithAuto<bool> SubForcedOnly => _subForcedOnly ??= new(this, "sub-forced-only");

    /// <summary>
    /// Specify the framerate of the subtitle file (default: video fps). Affects text subtitles only.
    /// </summary>
    private MpvOption<double>? _subFps;
    public MpvOption<double> SubFps => _subFps ??= new(this, "sub-fps");

    /// <summary>
    /// Apply Gaussian blur to image subtitles (default: 0). This can help to make pixelated DVD/Vobsubs look nicer. A value other than 0 also switches to software subtitle scaling. Might be slow.
    /// </summary>
    private MpvOption<double>? _subGauss;
    public MpvOption<double> SubGauss => _subGauss ??= new(this, "sub-gauss");

    /// <summary>
    /// Convert image subtitles to grayscale. Can help to make yellow DVD/Vobsubs look nicer.
    /// </summary>
    private MpvOption<bool>? _subGray;
    public MpvOption<bool> SubGray => _subGray ??= new(this, "sub-gray");

    /// <summary>
    /// Specify extra directories to search for subtitles matching the video. Paths can be relative or absolute. Relative paths are interpreted relative to video file directory. If the file is a URL, only absolute paths and sub configuration subdirectory will be scanned.
    /// </summary>
    private MpvOptionList? _subFilePaths;
    public MpvOptionList SubFilePaths => _subFilePaths ??= new(this, "sub-file-paths");

    /// <summary>
    /// Can be used to disable display of subtitles, but still select and decode them.
    /// </summary>
    private MpvOption<bool>? _subVisibility;
    public MpvOption<bool> SubVisibility => _subVisibility ??= new(this, "sub-visibility");

    /// <summary>
    /// (Obscure, rarely useful.) Can be used to play broken mkv files with duplicate ReadOrder fields. ReadOrder is the first field in a Matroska-style ASS subtitle packets. It should be unique, and libass uses it for fast elimination of duplicates. This option disables caching of subtitles across seeks, so after a seek libass can't eliminate subtitle packets with the same ReadOrder as earlier packets.
    /// </summary>
    private MpvOption<bool>? _subClearOnSeek;
    public MpvOption<bool> SubClearOnSeek => _subClearOnSeek ??= new(this, "sub-clear-on-seek");

    /// <summary>
    /// This works for dvb_teletext subtitle streams, and if FFmpeg has been compiled with support for it.
    /// </summary>
    private MpvOption<int>? _teletextPage;
    public MpvOption<int> TeletextPage => _teletextPage ??= new(this, "teletext-page");

    /// <summary>
    /// Specify font to use for subtitles that do not themselves specify a particular font. The default is sans-serif.
    /// </summary>
    private MpvOptionString? _subFont;
    public MpvOptionString SubFont => _subFont ??= new(this, "sub-font");

    /// <summary>
    /// Specify the sub font size. The unit is the size in scaled pixels at a window height of 720. The actual pixel size is scaled with the window height: if the window height is larger or smaller than 720, the actual size of the text increases or decreases as well.
    /// Default: 55.
    /// </summary>
    private MpvOption<int>? _subFontSize;
    public MpvOption<int> SubFontSize => _subFontSize ??= new(this, "sub-font-size");

    /// <summary>
    /// See --sub-color. Color used for sub text background. You can use --sub-shadow-offset to change its size relative to the text.
    /// </summary>
    private MpvOptionString? _subBackColor;
    public MpvOptionString SubBackColor => _subBackColor ??= new(this, "sub-back-color");

    /// <summary>
    /// Gaussian blur factor. 0 means no blur applied (default).
    /// </summary>
    private MpvOption<double>? _subBlur;
    public MpvOption<double> SubBlur => _subBlur ??= new(this, "sub-blur");

    /// <summary>
    /// Format text on bold.
    /// </summary>
    private MpvOption<bool>? _subBold;
    public MpvOption<bool> SubBold => _subBold ??= new(this, "sub-bold");

    /// <summary>
    /// Format text on italic.
    /// </summary>
    private MpvOption<bool>? _subItalic;
    public MpvOption<bool> SubItalic => _subItalic ??= new(this, "sub-italic");

    /// <summary>
    /// Color used for the sub font border.
    /// Ignored when --sub-back-color is specified (or more exactly: when that option is not set to completely transparent).
    /// </summary>
    private MpvOptionString? _subBorderColor;
    public MpvOptionString SubBorderColor => _subBorderColor ??= new(this, "sub-border-color");

    /// <summary>
    /// Size of the sub font border in scaled pixels (see --sub-font-size for details). A value of 0 disables borders.
    /// Default: 3.
    /// </summary>
    private MpvOption<int>? _subBorderSize;
    public MpvOption<int> SubBorderSize => _subBorderSize ??= new(this, "sub-border-size");

    /// <summary>
    /// Specify the color used for unstyled text subtitles.
    /// The color is specified in the form r/g/b, where each color component is specified as number in the range 0.0 to 1.0. It's also possible to specify the transparency by using r/g/b/a, where the alpha value 0 means fully transparent, and 1.0 means opaque. If the alpha component is not given, the color is 100% opaque.
    /// Passing a single number to the option sets the sub to gray, and the form gray/a lets you specify alpha additionally.
    /// Alternatively, the color can be specified as a RGB hex triplet in the form #RRGGBB, where each 2-digit group expresses a color value in the range 0 (00) to 255 (FF). For example, #FF0000 is red. This is similar to web colors. Alpha is given with #AARRGGBB.
    /// </summary>
    private MpvOptionString? _subColor;
    public MpvOptionString SubColor => _subColor ??= new(this, "sub-color");

    /// <summary>
    /// Left and right screen margin for the subs in scaled pixels (see --sub-font-size for details).
    /// This option specifies the distance of the sub to the left, as well as at which distance from the right border long sub text will be broken.
    /// Default: 25.
    /// </summary>
    private MpvOption<int>? _subMarginX;
    public MpvOption<int> SubMarginX => _subMarginX ??= new(this, "sub-margin-x");

    /// <summary>
    /// Top and bottom screen margin for the subs in scaled pixels (see --sub-font-size for details).
    /// This option specifies the vertical margins of unstyled text subtitles.If you just want to raise the vertical subtitle position, use --sub-pos.
    /// Default: 22.
    /// </summary>
    private MpvOption<int>? _subMarginY;
    public MpvOption<int> SubMarginY => _subMarginY ??= new(this, "sub-margin-y");

    /// <summary>
    /// Control to which corner of the screen text subtitles should be aligned to (default: center).
    /// Never applied to ASS subtitles, except in --no-sub-ass mode.Likewise, this does not apply to image subtitles.
    /// </summary>
    private MpvOptionEnum<AlignHorizontal>? _subAlignX;
    public MpvOptionEnum<AlignHorizontal> SubAlignX => _subAlignX ??= new(this, "sub-align-x");

    /// <summary>
    /// Vertical position (default: bottom).
    /// </summary>
    private MpvOptionEnum<AlignVertical>? _subAlignY;
    public MpvOptionEnum<AlignVertical> SubAlignY => _subAlignY ??= new(this, "sub-align-y");

    /// <summary>
    /// Control how multi line subs are justified irrespective of where they are aligned (default: auto which justifies as defined by --sub-align-y). Left justification is recommended to make the subs easier to read as it is easier for the eyes.
    /// </summary>
    private MpvOptionEnum<AlignHorizontal>? _subJustify;
    public MpvOptionEnum<AlignHorizontal> SubJustify => _subJustify ??= new(this, "sub-justify");

    /// <summary>
    /// Applies justification as defined by --sub-justify on ASS subtitles if --sub-ass-override is not set to no. Default: no.
    /// </summary>
    private MpvOption<bool>? _subAssJustify;
    public MpvOption<bool> SubAssJustify => _subAssJustify ??= new(this, "sub-ass-justify");

    /// <summary>
    /// Color used for sub text shadow.
    /// </summary>
    private MpvOptionString? _subShadowColor;
    public MpvOptionString SubShadowColor => _subShadowColor ??= new(this, "sub-shadow-color");

    /// <summary>
    /// Displacement of the sub text shadow in scaled pixels (see --sub-font-size for details). A value of 0 disables shadows.
    /// Default: 0.
    /// </summary>
    private MpvOption<int>? _subShadowOffset;
    public MpvOption<int> SubShadowOffset => _subShadowOffset ??= new(this, "sub-shadow-offset");

    /// <summary>
    /// Horizontal sub font spacing in scaled pixels (see --sub-font-size for details). This value is added to the normal letter spacing. Negative values are allowed.
    /// Default: 0.
    /// </summary>
    private MpvOption<int>? _subSpacing;
    public MpvOption<int> SubSpacing => _subSpacing ??= new(this, "sub-spacing");

    /// <summary>
    /// Applies filter removing subtitle additions for the deaf or hard-of-hearing (SDH). This is intended for English, but may in part work for other languages too. The intention is that it can be always enabled so may not remove all parts added. It removes speaker labels (like MAN:), upper case text in parentheses and any text in brackets.
    /// Default: no.
    /// </summary>
    private MpvOption<bool>? _subFilterSdh;
    public MpvOption<bool> SubFilterSdh => _subFilterSdh ??= new(this, "sub-filter-sdh");

    /// <summary>
    /// Do harder SDH filtering (if enabled by --sub-filter-sdh). Will also remove speaker labels and text within parentheses using both lower and upper case letters.
    /// Default: no.
    /// </summary>
    private MpvOption<bool>? _subFilterSdhHarder;
    public MpvOption<bool> SubFilterSdhHarder => _subFilterSdhHarder ??= new(this, "sub-filter-sdh-harder");

    /// <summary>
    /// Set a list of regular expressions to match on text subtitles, and remove any lines that match (default: empty).
    /// </summary>
    private MpvOptionList? _subFilterRegex;
    public MpvOptionList SubFilterRegex => _subFilterRegex ??= new(this, "sub-filter-regex");

    /// <summary>
    /// Log dropped lines with warning log level, instead of verbose (default: no). Helpful for testing.
    /// </summary>
    private MpvOption<bool>? _subFilterRegexWarn;
    public MpvOption<bool> SubFilterRegexWarn => _subFilterRegexWarn ??= new(this, "sub-filter-regex-warn");

    /// <summary>
    /// Whether to enable regex filtering (default: yes). Note that if no regexes are added to the --sub-filter-regex list, setting this option to yes has no effect. It's meant to easily disable or enable filtering temporarily.
    /// </summary>
    private MpvOption<bool>? _subFilterRegexEnable;
    public MpvOption<bool> SubFilterRegexEnable => _subFilterRegexEnable ??= new(this, "sub-filter-regex-enable");

    /// <summary>
    /// For every video stream, create a closed captions track (default: no). The only purpose is to make the track available for selection at the start of playback, instead of creating it lazily. This applies only to ATSC A53 Part 4 Closed Captions (displayed by mpv as subtitle tracks using the codec eia_608). The CC track is marked "default" and selected according to the normal subtitle track selection rules. You can then use --sid to explicitly select the correct track too.
    /// If the video stream contains no closed captions, or if no video is being decoded, the CC track will remain empty and will not show any text.
    /// </summary>
    private MpvOption<bool>? _subCreateCcTrack;
    public MpvOption<bool> SubCreateCcTrack => _subCreateCcTrack ??= new(this, "sub-create-cc-track");

    /// <summary>
    /// Which libass font provider backend to use (default: auto).
    /// </summary>
    private MpvOptionEnum<FontProvider>? _subFontProvider;
    public MpvOptionEnum<FontProvider> SubFontProvider => _subFontProvider ??= new(this, "sub-font-provider");

    /// <summary>
    /// Set the window title. This is used for the video window, and if possible, also sets the audio stream title.
    /// </summary>
    private MpvOptionString? _title;
    public MpvOptionString Title => _title ??= new(this, "title");

    /// <summary>
    /// In multi-monitor configurations (i.e. a single desktop that spans across multiple displays), this option tells mpv which screen to display the video on.
    /// </summary>
    private MpvOptionWithDefault<int>? _screen;
    public MpvOptionWithDefault<int> Screen => _screen ??= new(this, "screen");

    /// <summary>
    /// Fullscreen playback.
    /// </summary>
    private MpvOption<bool>? _fullscreen;
    public MpvOption<bool> Fullscreen => _fullscreen ??= new(this, "fullscreen");

    /// <summary>
    /// In multi-monitor configurations (i.e. a single desktop that spans across multiple displays), this option tells mpv which screen to go fullscreen to. If current is used mpv will fallback on what the user provided with the screen option.
    /// </summary>
    private MpvOptionWithAllCurrent<int>? _fullscreenScreen;
    public MpvOptionWithAllCurrent<int> FullscreenScreen => _fullscreenScreen ??= new(this, "fs-screen");

    /// <summary>
    /// Do not terminate when playing or seeking beyond the end of the file, and there is not next file to be played (and --loop is not used). Instead, pause the player. When trying to seek beyond end of the file, the player will attempt to seek to the last frame.
    /// Normally, this will act like set pause yes on EOF, unless the --keep-open-pause=no option is set.
    /// </summary>
    private MpvOptionEnum<KeepOpenOption>? _keepOpen;
    public MpvOptionEnum<KeepOpenOption> KeepOpen => _keepOpen ??= new(this, "keep-open");

    /// <summary>
    /// If set to no, instead of pausing when --keep-open is active, just stop at end of file and continue playing forward when you seek backwards until end where it stops again. Default: yes.
    /// </summary>
    private MpvOption<bool>? _keepOpenPause;
    public MpvOption<bool> KeepOpenPause => _keepOpenPause ??= new(this, "keep-open-pause");

    /// <summary>
    /// If the current file is an image, play the image for the given amount of seconds (default: 1). inf means the file is kept open forever (until the user stops playback manually).
    /// </summary>
    private MpvOptionWithInf<double>? _imageDisplayDuration;
    public MpvOptionWithInf<double> ImageDisplayDuration => _imageDisplayDuration ??= new(this, "image-display-duration");

    /// <summary>
    /// Create a video output window even if there is no video. This can be useful when pretending that mpv is a GUI application. Currently, the window always has the size 640x480, and is subject to --geometry, --autofit, and similar options.
    /// </summary>
    private MpvOptionEnum<ForceWindowOption>? _forceWindow;
    public MpvOptionEnum<ForceWindowOption> ForceWindow => _forceWindow ??= new(this, "force-window");

    /// <summary>
    /// (Windows only) Enable/disable playback progress rendering in taskbar (Windows 7 and above).
    /// Enabled by default.
    /// </summary>
    private MpvOption<bool>? _taskbarProgress;
    public MpvOption<bool> TaskbarProgress => _taskbarProgress ??= new(this, "taskbar-progress");

    /// <summary>
    /// (Windows only) Snap the player window to screen edges.
    /// </summary>
    private MpvOption<bool>? _snapWindow;
    public MpvOption<bool> SnapWindow => _snapWindow ??= new(this, "snap-window");

    /// <summary>
    /// Makes the player window stay on top of other windows.
    /// On Windows, if combined with fullscreen mode, this causes mpv to be treated as exclusive fullscreen window that bypasses the Desktop Window Manager.
    /// </summary>
    private MpvOption<bool>? _onTop;
    public MpvOption<bool> OnTop => _onTop ??= new(this, "ontop");

    /// <summary>
    /// (OS X only) Sets the level of an ontop window (default: window).
    /// Valid values: window|system|desktop|level
    /// </summary>
    private MpvOptionString? _onTopLevel;
    public MpvOptionString OnTopLevel => _onTopLevel ??= new(this, "ontop-level");

    /// <summary>
    /// (macOS only) Focus the video window on creation and makes it the front most window. This is on by default.
    /// </summary>
    private MpvOption<bool>? _focusOnOpen;
    public MpvOption<bool> FocusOnOpen => _focusOnOpen ??= new(this, "focus-on-open");

    /// <summary>
    /// Play video with window border and decorations. Since this is on by default, use --no-border to disable the standard window decorations.
    /// </summary>
    private MpvOption<bool>? _border;
    public MpvOption<bool> Border => _border ??= new(this, "border");

    /// <summary>
    /// (Windows only) Fit the whole window with border and decorations on the screen. Since this is on by default, use --no-fit-border to make mpv try to only fit client area with video on the screen. This behavior only applied to window/video with size exceeding size of the screen.
    /// </summary>
    private MpvOption<bool>? _fitBorder;
    public MpvOption<bool> FitBorder => _fitBorder ??= new(this, "fit-border");

    /// <summary>
    /// (X11 only) Show the video window on all virtual desktops.
    /// </summary>
    private MpvOption<bool>? _onAllWorkspaces;
    public MpvOption<bool> OnAllWorkspaces => _onAllWorkspaces ??= new(this, "on-all-workspaces");

    /// <summary>
    /// Adjust the initial window position or size. W and H set the window size in pixels. x and y set the window position, measured in pixels from the top-left corner of the screen to the top-left corner of the image being displayed. If a percentage sign (%) is given after the argument, it turns the value into a percentage of the screen size in that direction. Positions are specified similar to the standard X11 --geometry option format, in which e.g. +10-50 means "place 10 pixels from the left border and 50 pixels from the lower border" and "--20+-10" means "place 20 pixels beyond the right and 10 pixels beyond the top border". A trailing / followed by an integer denotes on which workspace (virtual desktop) the window should appear (X11 only).
    /// </summary>
    private MpvOptionString? _geometry;
    public MpvOptionString Geometry => _geometry ??= new(this, "geometry");

    /// <summary>
    /// Set the initial window size to a maximum size specified by WxH, without changing the window's aspect ratio. The size is measured in pixels, or if a number is followed by a percentage sign (%), in percents of the screen size.
    /// This option never changes the aspect ratio of the window.If the aspect ratio mismatches, the window's size is reduced until it fits into the specified size.
    /// Window position is not taken into account, nor is it modified by this option (the window manager still may place the window differently depending on size). Use --geometry to change the window position.Its effects are applied after this option.
    /// </summary>
    private MpvOptionString? _autofit;
    public MpvOptionString Autofit => _autofit ??= new(this, "autofit");

    /// <summary>
    /// This option behaves exactly like --autofit, except the window size is only changed if the window would be larger than the specified size.
    /// </summary>
    private MpvOptionString? _autofitLarger;
    public MpvOptionString AutofitLarger => _autofitLarger ??= new(this, "autofit-larger");

    /// <summary>
    /// This option behaves exactly like --autofit, except that it sets the minimum size of the window (just as --autofit-larger sets the maximum).
    /// </summary>
    private MpvOptionString? _autofitSmaller;
    public MpvOptionString AutofitSmaller => _autofitSmaller ??= new(this, "autofit-smaller");

    /// <summary>
    /// Resize the video window to a multiple (or fraction) of the video size. This option is applied before --autofit and other options are applied (so they override this option).
    /// </summary>
    private MpvOption<double>? _windowScale;
    public MpvOption<double> WindowScale => _windowScale ??= new(this, "window-scale");

    /// <summary>
    /// Whether the video window is minimized or not. Setting this will minimize, or unminimize, the video window if the current VO supports it. Note that some VOs may support minimization while not supporting unminimization (eg: Wayland).
    /// </summary>
    private MpvOption<bool>? _windowMinimized;
    public MpvOption<bool> WindowMinimized => _windowMinimized ??= new(this, "window-minimized");

    /// <summary>
    /// Whether the video window is maximized or not. Setting this will maximize, or unmaximize, the video window if the current VO supports it.
    /// </summary>
    private MpvOption<bool>? _windowMaximized;
    public MpvOption<bool> WindowMaximized => _windowMaximized ??= new(this, "window-maximized");

    /// <summary>
    /// Make mouse cursor automatically hide after given number of milliseconds. no will disable cursor autohide. always means the cursor will stay hidden.
    /// </summary>
    private MpvOptionWithNoAlways<int>? _cursorAutohide;
    public MpvOptionWithNoAlways<int> CursorAutohide => _cursorAutohide ??= new(this, "cursor-autohide");

    /// <summary>
    /// If this option is given, the cursor is always visible in windowed mode. In fullscreen mode, the cursor is shown or hidden according to --cursor-autohide.
    /// </summary>
    private MpvOption<bool>? _cursorAutohideFsOnly;
    public MpvOption<bool> CursorAutohideFsOnly => _cursorAutohideFsOnly ??= new(this, "cursor-autohide-fs-only");

    /// <summary>
    /// False enforces closing and reopening the video window for multiple files (one (un)initialization for each file). (default: true)
    /// </summary>
    private MpvOption<bool>? _fixedVideoOutput;
    public MpvOption<bool> FixedVideoOutput => _fixedVideoOutput ??= new(this, "fixed-vo");

    /// <summary>
    /// Change how some video outputs render the OSD and text subtitles. This does not change appearance of the subtitles and only has performance implications. For VOs which support native ASS rendering (like gpu, vdpau, direct3d), this can be slightly faster or slower, depending on GPU drivers and hardware. For other VOs, this just makes rendering slower.
    /// </summary>
    private MpvOption<bool>? _forceRgbaOsdRendering;
    public MpvOption<bool> ForceRgbaOsdRendering => _forceRgbaOsdRendering ??= new(this, "force-rgba-osd-rendering");

    /// <summary>
    /// Forcefully move mpv's video output window to default location whenever there is a change in video parameters, video stream or file. This used to be the default behavior. Currently only affects X11 VOs.
    /// </summary>
    private MpvOption<bool>? _forceWindowPosition;
    public MpvOption<bool> ForceWindowPosition => _forceWindowPosition ??= new(this, "force-window-position");

    /// <summary>
    /// False will always stretch the video to window size, and will disable the window manager hints that force the window aspect ratio. (Ignored in fullscreen mode.) (default: true)
    /// </summary>
    private MpvOption<bool>? _keepAspect;
    public MpvOption<bool> KeepAspect => _keepAspect ??= new(this, "keepaspect");

    /// <summary>
    /// True (the default) will lock the window size to the video aspect. False disables this behavior, and will instead add black bars if window aspect and video aspect mismatch. Whether this actually works depends on the VO backend. (Ignored in fullscreen mode.)
    /// </summary>
    private MpvOption<bool>? _keepAspectWindow;
    public MpvOption<bool> KeepAspectWindow => _keepAspectWindow ??= new(this, "keepaspect-window");

    /// <summary>
    /// Set the aspect ratio of your monitor or TV screen. A value of 0 disables a previous setting (e.g. in the config file). Overrides the --monitorpixelaspect setting if enabled.
    /// </summary>
    private MpvOptionString? _monitorAspect;
    public MpvOptionString MonitorAspect => _monitorAspect ??= new(this, "monitoraspect");

    /// <summary>
    /// (OS X, Windows, X11, and Wayland only) Scale the window size according to the backing scale factor (default: yes). On regular HiDPI resolutions the window opens with double the size but appears as having the same size as on non-HiDPI resolutions. This is the default OS X behavior.
    /// </summary>
    private MpvOption<bool>? _highDpiWindowScale;
    public MpvOption<bool> HighDpiWindowScale => _highDpiWindowScale ??= new(this, "hidpi-window-scale");

    /// <summary>
    /// (OS X only) Uses the native fullscreen mechanism of the OS (default: yes).
    /// </summary>
    private MpvOption<bool>? _nativeFullscreen;
    public MpvOption<bool> NativeFullscreen => _nativeFullscreen ??= new(this, "native-fs");

    /// <summary>
    /// Set the aspect of a single pixel of your monitor or TV screen (default: 1). A value of 1 means square pixels (correct for (almost?) all LCDs).
    /// </summary>
    private MpvOption<double>? _monitorPixelAspect;
    public MpvOption<double> MonitorPixelAspect => _monitorPixelAspect ??= new(this, "monitorpixelaspect");

    /// <summary>
    /// Turns off the screensaver (or screen blanker and similar mechanisms) at startup and turns it on again on exit (default: yes). The screensaver is always re-enabled when the player is paused.
    /// </summary>
    private MpvOption<bool>? _stopScreensaver;
    public MpvOption<bool> StopScreensaver => _stopScreensaver ??= new(this, "stop-screensaver");

    /// <summary>
    /// This tells mpv to attach to an existing window. If a VO is selected that supports this option, it will use that window for video output. mpv will scale the video to the size of this window, and will add black bars to compensate if the aspect ratio of the video is different.
    /// On X11, the ID is interpreted as a Window on X11.Unlike MPlayer/mplayer2, mpv always creates its own window, and sets the wid window as parent.The window will always be resized to cover the parent window fully. The value 0 is interpreted specially, and mpv will draw directly on the root window.
    /// On win32, the ID is interpreted as HWND.Pass it as value cast to intptr_t. mpv will create its own window, and set the wid window as parent, like with X11.
    /// On OSX/Cocoa, the ID is interpreted as NSView*. Pass it as value cast to intptr_t. mpv will create its own sub-view.Because OSX does not support window embedding of foreign processes, this works only with libmpv, and will crash when used from the command line.
    /// On Android, the ID is interpreted as android.view.Surface.Pass it as a value cast to intptr_t.Use with --vo= mediacodec_embed and --hwdec= mediacodec for direct rendering using MediaCodec, or with --vo=gpu --gpu-context=android(with or without --hwdec= mediacodec - copy).
    /// </summary>
    private MpvOption<int>? _windowId;
    public MpvOption<int> WindowId => _windowId ??= new(this, "wid");

    /// <summary>
    /// Whether to move the window when clicking on it and moving the mouse pointer.
    /// </summary>
    private MpvOption<bool>? _windowDragging;
    public MpvOption<bool> WindowDragging => _windowDragging ??= new(this, "window-dragging");

    /// <summary>
    /// Set the window class name for X11-based video output methods.
    /// </summary>
    private MpvOptionString? _x11Name;
    public MpvOptionString X11Name => _x11Name ??= new(this, "x11-name");

    /// <summary>
    /// (X11 only) Control the use of NetWM protocol features.
    /// </summary>
    private MpvOptionWithAuto<bool>? _x11NetWm;
    public MpvOptionWithAuto<bool> X11NetWm => _x11NetWm ??= new(this, "x11-netwm");

    /// <summary>
    /// (X11 only) Specifies when to bypass the compositor.
    /// </summary>
    private MpvOptionEnum<BypassCompositorOption>? _x11BypassCompositor;
    public MpvOptionEnum<BypassCompositorOption> X11BypassCompositor => _x11BypassCompositor ??= new(this, "x11-bypass-compositor");

    /// <summary>
    /// Specify the CD-ROM device (default: /dev/cdrom).
    /// </summary>
    private MpvOptionString? _cdRomDevice;
    public MpvOptionString CdRomDevice => _cdRomDevice ??= new(this, "cdrom-device");

    /// <summary>
    /// Specify the DVD device or .iso filename (default: /dev/dvd). You can also specify a directory that contains files previously copied directly from a DVD (with e.g. vobcopy).
    /// </summary>
    private MpvOptionString? _dvdDevice;
    public MpvOptionString DvdDevice => _dvdDevice ??= new(this, "dvd-device");

    /// <summary>
    /// (Blu-ray only) Specify the Blu-ray disc location. Must be a directory with Blu-ray structure.
    /// </summary>
    private MpvOptionString? _bluerayDevice;
    public MpvOptionString BluerayDevice => _bluerayDevice ??= new(this, "bluray-device");

    /// <summary>
    /// Set CD spin speed.
    /// </summary>
    private MpvOption<int>? _cdRomSpeed;
    public MpvOption<int> CdRomSpeed => _cdRomSpeed ??= new(this, "cdda-speed");

    /// <summary>
    /// Set paranoia level. Values other than 0 seem to break playback of anything but the first track.
    /// 0:	disable checking(default)
    /// 1:	overlap checking only
    /// 2:	full data correction and verification
    /// </summary>
    private MpvOption<int>? _cdRomParanoia;
    public MpvOption<int> CdRomParanoia => _cdRomParanoia ??= new(this, "cdda-paranoia");

    /// <summary>
    /// Set atomic read size.
    /// </summary>
    private MpvOption<int>? _cdRomSectorSize;
    public MpvOption<int> CdRomSectorSize => _cdRomSectorSize ??= new(this, "cdda-sector-size");

    /// <summary>
    /// Force minimum overlap search during verification to value sectors.
    /// </summary>
    private MpvOption<int>? _cdRomOverlap;
    public MpvOption<int> CdRomOverlap => _cdRomOverlap ??= new(this, "cdda-overlap");

    /// <summary>
    /// Assume that the beginning offset of track 1 as reported in the TOC will be addressed as LBA 0. Some discs need this for getting track boundaries correctly.
    /// </summary>
    private MpvOption<bool>? _cdRomTocBias;
    public MpvOption<bool> CdRomTocBias => _cdRomTocBias ??= new(this, "cdda-toc-bias");

    /// <summary>
    /// Add sectors to the values reported when addressing tracks. May be negative.
    /// </summary>
    private MpvOption<int>? _cdRomTocOffset;
    public MpvOption<int> CdRomTocOffset => _cdRomTocOffset ??= new(this, "cdda-toc-offset");

    /// <summary>
    /// Whether or not to accept imperfect data reconstruction.
    /// </summary>
    private MpvOption<bool>? _cdRomSkip;
    public MpvOption<bool> CdRomSkip => _cdRomSkip ??= new(this, "cdda-skip");

    /// <summary>
    /// Print CD text. This is disabled by default, because it ruins performance with CD-ROM drives for unknown reasons.
    /// </summary>
    private MpvOption<bool>? _cdRomCdText;
    public MpvOption<bool> CdRomCdText => _cdRomCdText ??= new(this, "cdda-cdtext");

    /// <summary>
    /// Try to limit DVD speed (default: 0, no change). DVD base speed is 1385 kB/s, so an 8x drive can read at speeds up to 11080 kB/s. Slower speeds make the drive more quiet. For watching DVDs, 2700 kB/s should be quiet and fast enough. mpv resets the speed to the drive default value on close. Values of at least 100 mean speed in kB/s. Values less than 100 mean multiples of 1385 kB/s, i.e. --dvd-speed=8 selects 11080 kB/s.
    /// </summary>
    private MpvOption<int>? _dvdSpeed;
    public MpvOption<int> DvdSpeed => _dvdSpeed ??= new(this, "dvd-speed");

    /// <summary>
    /// Some DVDs contain scenes that can be viewed from multiple angles. This option tells mpv which angle to use (default: 1).
    /// </summary>
    private MpvOption<int>? _dvdAngle;
    public MpvOption<int> DvdAngle => _dvdAngle ??= new(this, "dvd-angle");

    /// <summary>
    /// Adjust the brightness of the video signal (default: 0). Not supported by all video output drivers.
    /// </summary>
    private MpvOption<int>? _brightness;
    public MpvOption<int> Brightness => _brightness ??= new(this, "brightness");

    /// <summary>
    /// Adjust the contrast of the video signal (default: 0). Not supported by all video output drivers.
    /// </summary>
    private MpvOption<int>? _contrast;
    public MpvOption<int> Contrast => _contrast ??= new(this, "contrast");

    /// <summary>
    /// Adjust the saturation of the video signal (default: 0). You can get grayscale output with this option. Not supported by all video output drivers.
    /// </summary>
    private MpvOption<int>? _saturation;
    public MpvOption<int> Saturation => _saturation ??= new(this, "saturation");

    /// <summary>
    /// Adjust the gamma of the video signal (default: 0). Not supported by all video output drivers.
    /// </summary>
    private MpvOption<int>? _gamma;
    public MpvOption<int> Gamma => _gamma ??= new(this, "gamma");

    /// <summary>
    /// Adjust the hue of the video signal (default: 0). You can get a colored negative of the image with this option. Not supported by all video output drivers.
    /// </summary>
    private MpvOption<int>? _hue;
    public MpvOption<int> Hue => _hue ??= new(this, "hue");

    /// <summary>
    /// Force demuxer type. Use a '+' before the name to force it; this will skip some checks. Give the demuxer name as printed by --demuxer=help.
    /// </summary>
    private MpvOptionString? _demuxer;
    public MpvOptionString Demuxer => _demuxer ??= new(this, "demuxer");

    /// <summary>
    /// Maximum length in seconds to analyze the stream properties.
    /// </summary>
    private MpvOption<double>? _demuxerLavfAnalyzeDuration;
    public MpvOption<double> DemuxerLavfAnalyzeDuration => _demuxerLavfAnalyzeDuration ??= new(this, "demuxer-lavf-analyzeduration");

    /// <summary>
    /// Whether to probe stream information (default: auto). Technically, this controls whether libavformat's avformat_find_stream_info() function is called. Usually it's safer to call it, but it can also make startup slower.
    /// </summary>
    private MpvOptionEnum<ProbeOption>? _demuxerLavfProbeInfo;
    public MpvOptionEnum<ProbeOption> DemuxerLavfProbeInfo => _demuxerLavfProbeInfo ??= new(this, "demuxer-lavf-probe-info");

    /// <summary>
    /// Minimum required libavformat probe score. Lower values will require less data to be loaded (makes streams start faster), but makes file format detection less reliable. Can be used to force auto-detected libavformat demuxers, even if libavformat considers the detection not reliable enough. (Default: 26.)
    /// </summary>
    private MpvOption<int>? _demuxerLavfProbeScore;
    public MpvOption<int> DemuxerLavfProbeScore => _demuxerLavfProbeScore ??= new(this, "demuxer-lavf-probescore");

    /// <summary>
    /// Allow deriving the format from the HTTP MIME type. Set this to no in case playing things from HTTP mysteriously fails, even though the same files work from local disk.
    /// Default is True in order to reduce latency when opening HTTP streams.
    /// </summary>
    private MpvOption<bool>? _demuxerLavfAllowMimeType;
    public MpvOption<bool> DemuxerLavfAllowMimeType => _demuxerLavfAllowMimeType ??= new(this, "demuxer-lavf-allow-mimetype");

    /// <summary>
    /// Force a specific libavformat demuxer.
    /// </summary>
    private MpvOptionString? _demuxerLavfFormat;
    public MpvOptionString DemuxerLavfFormat => _demuxerLavfFormat ??= new(this, "demuxer-lavf-format");

    /// <summary>
    /// By default, some formats will be handled differently from other formats by explicitly checking for them. Most of these compensate for weird or imperfect behavior from libavformat demuxers. Passing no disables these. For debugging and testing only.
    /// </summary>
    private MpvOption<bool>? _demuxerLavfHacks;
    public MpvOption<bool> DemuxerLavfHacks => _demuxerLavfHacks ??= new(this, "demuxer-lavf-hacks");

    /// <summary>
    /// Pass AVOptions to libavformat demuxer.
    /// </summary>
    private MpvOptionDictionary? _demuxerLavfOptions;
    public MpvOptionDictionary DemuxerLavfOptions => _demuxerLavfOptions ??= new(this, "demuxer-lavf-o");

    /// <summary>
    /// Maximum amount of data to probe during the detection phase. In the case of MPEG-TS this value identifies the maximum number of TS packets to scan.
    /// </summary>
    private MpvOption<int>? _demuxerLavfProbeSize;
    public MpvOption<int> DemuxerLavfProbeSize => _demuxerLavfProbeSize ??= new(this, "demuxer-lavf-probesize");

    /// <summary>
    /// Size of the stream read buffer allocated for libavformat in bytes (default: 32768). Lowering the size could lower latency. Note that libavformat might reallocate the buffer internally, or not fully use all of it.
    /// </summary>
    private MpvOption<int>? _demuxerLavfBufferSize;
    public MpvOption<int> DemuxerLavfBufferSize => _demuxerLavfBufferSize ??= new(this, "demuxer-lavf-buffersize");

    /// <summary>
    /// Attempt to linearize timestamp resets in demuxed streams (default: auto). This was tested only for single audio streams. It's unknown whether it works correctly for video (but likely won't). Note that the implementation is slightly incorrect either way, and will introduce a discontinuity by about 1 codec frame size.
    /// The auto mode enables this for OGG audio stream.This covers the common and annoying case of OGG web radio streams.Some of these will reset timestamps to 0 every time a new song begins.This breaks the mpv seekable cache, which can't deal with timestamp resets. Note that FFmpeg/libavformat's seeking API can't deal with this either; it's likely that if this option breaks this even more, while if it's disabled, you can at least seek within the first song in the stream. Well, you won't get anything useful either way if the seek is outside of mpv's cache.
    /// </summary>
    private MpvOptionWithAuto<bool>? _demuxerLavfLinearizeTimestamps;
    public MpvOptionWithAuto<bool> DemuxerLavfLinearizeTimestamps => _demuxerLavfLinearizeTimestamps ??= new(this, "demuxer-lavf-linearize-timestamps");

    /// <summary>
    /// Propagate FFmpeg-level options to recursively opened connections (default: yes). This is needed because FFmpeg will apply these settings to nested AVIO contexts automatically. On the other hand, this could break in certain situations - it's the FFmpeg API, you just can't win.
    /// </summary>
    private MpvOption<bool>? _demuxerLavfPropagateOpts;
    public MpvOption<bool> DemuxerLavfPropagateOpts => _demuxerLavfPropagateOpts ??= new(this, "demuxer-lavf-propagate-opts");

    /// <summary>
    /// Try harder to show embedded soft subtitles when seeking somewhere. Normally, it can happen that the subtitle at the seek target is not shown due to how some container file formats are designed. The subtitles appear only if seeking before or exactly to the position a subtitle first appears. To make this worse, subtitles are often timed to appear a very small amount before the associated video frame, so that seeking to the video frame typically does not demux the subtitle at that position.
    /// </summary>
    private MpvOptionWithIndex<bool>? _demuxerMkvSubtitlePreroll;
    public MpvOptionWithIndex<bool> DemuxerMkvSubtitlePreroll => _demuxerMkvSubtitlePreroll ??= new(this, "demuxer-mkv-subtitle-preroll");

    /// <summary>
    /// Specify how much data the demuxer should pre-read at most in order to find subtitle packets that may overlap. Setting this to 0 will effectively disable this preroll mechanism. Setting a very large value can make seeking very slow, and an extremely large value would completely reread the entire file from start to seek target on every seek - seeking can become slower towards the end of the file. The details are messy, and the value is actually rounded down to the cluster with the previous video keyframe.
    /// </summary>
    private MpvOption<double>? _demuxerMkvSubtitlePrerollSecs;
    public MpvOption<double> DemuxerMkvSubtitlePrerollSecs => _demuxerMkvSubtitlePrerollSecs ??= new(this, "demuxer-mkv-subtitle-preroll-secs");

    /// <summary>
    /// Some files, especially files muxed with newer mkvmerge versions, have information embedded that can be used to determine what subtitle packets overlap with a seek target. In these cases, mpv will reduce the amount of data read to a minimum. (Although it will still read all data between the cluster that contains the first wanted subtitle packet, and the seek target.) If the index choice (which is the default) is specified, then prerolling will be done only if this information is actually available. If this method is used, the maximum amount of data to skip can be additionally controlled by --demuxer-mkv-subtitle-preroll-secs-index (it still uses the value of the option without -index if that is higher).
    /// </summary>
    private MpvOption<int>? _demuxerMkvSubtitlePrerollSecsIndex;
    public MpvOption<int> DemuxerMkvSubtitlePrerollSecsIndex => _demuxerMkvSubtitlePrerollSecsIndex ??= new(this, "demuxer-mkv-subtitle-preroll-secs-index");

    /// <summary>
    /// Check the start time of Matroska files (default: yes). This simply reads the first cluster timestamps and assumes it is the start time. Technically, this also reads the first timestamp, which may increase latency by one frame (which may be relevant for live streams).
    /// </summary>
    private MpvOption<bool>? _demuxerMkvProbeStartTime;
    public MpvOption<bool> DemuxerMkvProbeStartTime => _demuxerMkvProbeStartTime ??= new(this, "demuxer-mkv-probe-start-time");

    /// <summary>
    /// When opening the file, seek to the end of it, and check what timestamp the last video packet has, and report that as file duration. This is strictly for compatibility with Haali only. In this mode, it's possible that opening will be slower (especially when playing over http), or that behavior with broken files is much worse. So don't use this option.
    /// The yes mode merely uses the index and reads a small number of blocks from the end of the file.The full mode actually traverses the entire file and can make a reliable estimate even without an index present (such as partial files).
    /// </summary>
    private MpvOption<bool>? _demuxerMkvProbeVideoDuration;
    public MpvOption<bool> DemuxerMkvProbeVideoDuration => _demuxerMkvProbeVideoDuration ??= new(this, "demuxer-mkv-probe-video-duration");

    /// <summary>
    /// Number of channels (or channel layout) if --demuxer=rawaudio is used (default: stereo).
    /// </summary>
    private MpvOption<int>? _demuxerRawAudioChannels;
    public MpvOption<int> DemuxerRawAudioChannels => _demuxerRawAudioChannels ??= new(this, "demuxer-rawaudio-channels");

    /// <summary>
    /// Sample format for --demuxer=rawaudio (default: s16le). Use --demuxer-rawaudio-format=help to get a list of all formats.
    /// </summary>
    private MpvOptionString? _demuxerRawAudioFormat;
    public MpvOptionString DemuxerRawAudioFormat => _demuxerRawAudioFormat ??= new(this, "demuxer-rawaudio-format");

    /// <summary>
    /// Sample rate for --demuxer=rawaudio (default: 44 kHz).
    /// </summary>
    private MpvOption<int>? _demuxerRawAudioRate;
    public MpvOption<int> DemuxerRawAudioRate => _demuxerRawAudioRate ??= new(this, "demuxer-rawaudio-rate");

    /// <summary>
    /// Rate in frames per second for --demuxer=rawvideo (default: 25.0).
    /// </summary>
    private MpvOption<double>? _demuxerRawVideoFps;
    public MpvOption<double> DemuxerRawVideoFps => _demuxerRawVideoFps ??= new(this, "demuxer-rawvideo-fps");

    /// <summary>
    /// Image dimension in pixels for --demuxer=rawvideo.
    /// </summary>
    private MpvOption<int>? _demuxerRawVideoW;
    public MpvOption<int> DemuxerRawVideoW => _demuxerRawVideoW ??= new(this, "demuxer-rawvideo-w");

    /// <summary>
    /// Image dimension in pixels for --demuxer=rawvideo.
    /// </summary>
    private MpvOption<int>? _demuxerRawVideoH;
    public MpvOption<int> DemuxerRawVideoH => _demuxerRawVideoH ??= new(this, "demuxer-rawvideo-h");

    /// <summary>
    /// Color space (fourcc) in hex or string for --demuxer=rawvideo (default: YV12).
    /// </summary>
    private MpvOptionString? _demuxerRawVideoFormat;
    public MpvOptionString DemuxerRawVideoFormat => _demuxerRawVideoFormat ??= new(this, "demuxer-rawvideo-format");

    /// <summary>
    /// Color space by internal video format for --demuxer=rawvideo. Use --demuxer-rawvideo-mp-format=help for a list of possible formats.
    /// </summary>
    private MpvOptionString? _demuxerRawVideoMpFormat;
    public MpvOptionString DemuxerRawVideoMpFormat => _demuxerRawVideoMpFormat ??= new(this, "demuxer-rawvideo-mp-format");

    /// <summary>
    /// Set the video codec instead of selecting the rawvideo codec when using --demuxer=rawvideo. This uses the same values as codec names in --vd (but it does not accept decoder names).
    /// </summary>
    private MpvOptionString? _demuxerRawVideoCodec;
    public MpvOptionString DemuxerRawVideoCodec => _demuxerRawVideoCodec ??= new(this, "demuxer-rawvideo-codec");

    /// <summary>
    /// Frame size in bytes when using --demuxer=rawvideo.
    /// </summary>
    private MpvOption<int>? _demuxerRawVideoSize;
    public MpvOption<int> DemuxerRawVideoSize => _demuxerRawVideoSize ??= new(this, "demuxer-rawvideo-size");

    /// <summary>
    /// Specify the CUE sheet codepage. (See --sub-codepage for details.)
    /// </summary>
    private MpvOptionString? _demuxerCueCodepage;
    public MpvOptionString DemuxerCueCodepage => _demuxerCueCodepage ??= new(this, "demuxer-cue-codepage");

    /// <summary>
    /// This controls how much the demuxer is allowed to buffer ahead. The demuxer will normally try to read ahead as much as necessary, or as much is requested with --demuxer-readahead-secs. The option can be used to restrict the maximum readahead. This limits excessive readahead in case of broken files or desynced playback. The demuxer will stop reading additional packets as soon as one of the limits is reached. (The limits still can be slightly overstepped due to technical reasons.)
    /// Set these limits higher if you get a packet queue overflow warning, and you think normal playback would be possible with a larger packet queue.
    /// </summary>
    private MpvOption<int>? _demuxerMaxBytes;
    public MpvOption<int> DemuxerMaxBytes => _demuxerMaxBytes ??= new(this, "demuxer-max-bytes");

    /// <summary>
    /// This controls how much past data the demuxer is allowed to preserve. This is useful only if the cache is enabled.
    /// Unlike the forward cache, there is no control how many seconds are actually cached - it will simply use as much memory this option allows.Setting this option to 0 will strictly disable any back buffer, but this will lead to the situation that the forward seek range starts after the current playback position (as it removes past packets that are seek points).
    /// If the end of the file is reached, the remaining unused forward buffer space is "donated" to the backbuffer(unless the backbuffer size is set to 0, or --demuxer-donate-buffer is set to no). This still limits the total cache usage to the sum of the forward and backward cache, and effectively makes better use of the total allowed memory budget. (The opposite does not happen: free backward buffer is never "donated" to the forward buffer.)
    /// Keep in mind that other buffers in the player(like decoders) will cause the demuxer to cache "future" frames in the back buffer, which can skew the impression about how much data the backbuffer contains.
    /// </summary>
    private MpvOption<int>? _demuxerMaxBackBytes;
    public MpvOption<int> DemuxerMaxBackBytes => _demuxerMaxBackBytes ??= new(this, "demuxer-max-back-bytes");

    /// <summary>
    /// Whether to let the back buffer use part of the forward buffer (default: yes). If set to yes, the "donation" behavior described in the option description for --demuxer-max-back-bytes is enabled. This means the back buffer may use up memory up to the sum of the forward and back buffer options, minus the active size of the forward buffer. If set to no, the options strictly limit the forward and back buffer sizes separately.
    /// Note that if the end of the file is reached, the buffered data stays the same, even if you seek back within the cache.This is because the back buffer is only reduced when new data is read.
    /// </summary>
    private MpvOption<bool>? _demuxerDonateBuffer;
    public MpvOption<bool> DemuxerDonateBuffer => _demuxerDonateBuffer ??= new(this, "demuxer-donate-buffer");

    /// <summary>
    /// Debugging option to control whether seeking can use the demuxer cache (default: auto). Normally you don't ever need to set this; the default auto does the right thing and enables cache seeking it if --cache is set to yes (or is implied yes if --cache=auto).
    /// If enabled, short seek offsets will not trigger a low level demuxer seek(which means for example that slow network round trips or FFmpeg seek bugs can be avoided). If a seek cannot happen within the cached range, a low level seek will be triggered.Seeking outside of the cache will start a new cached range, but can discard the old cache range if the demuxer exhibits certain unsupported behavior.
    /// The special value auto means yes in the same situation as --cache-secs is used (i.e.when the stream appears to be a network stream or the stream cache is enabled).
    /// </summary>
    private MpvOptionWithAuto<bool>? _demuxerSeekableCache;
    public MpvOptionWithAuto<bool> DemuxerSeekableCache => _demuxerSeekableCache ??= new(this, "demuxer-seekable-cache");

    /// <summary>
    /// Run the demuxer in a separate thread, and let it prefetch a certain amount of packets (default: yes). Having this enabled leads to smoother playback, enables features like prefetching, and prevents that stuck network freezes the player. On the other hand, it can add overhead, or the background prefetching can hog CPU resources.
    /// Disabling this option is not recommended. Use it for debugging only.
    /// </summary>
    private MpvOption<bool>? _demuxerThread;
    public MpvOption<bool> DemuxerThread => _demuxerThread ??= new(this, "demuxer-thread");

    /// <summary>
    /// Number of seconds the player should wait to shutdown the demuxer (default: 0.1). The player will wait up to this much time before it closes the stream layer forcefully. Forceful closing usually means the network I/O is given no chance to close its connections gracefully (of course the OS can still close TCP connections properly), and might result in annoying messages being logged, and in some cases, confused remote servers.
    /// This timeout is usually only applied when loading has finished properly.If loading is aborted by the user, or in some corner cases like removing external tracks sourced from network during playback, forceful closing is always used.
    /// </summary>
    private MpvOption<double>? _demuxerTerminationTimeout;
    public MpvOption<double> DemuxerTerminationTimeout => _demuxerTerminationTimeout ??= new(this, "demuxer-termination-timeout");

    /// <summary>
    /// If --demuxer-thread is enabled, this controls how much the demuxer should buffer ahead in seconds (default: 1). As long as no packet has a timestamp difference higher than the readahead amount relative to the last packet returned to the decoder, the demuxer keeps reading.
    /// Note that enabling the cache(such as --cache= yes, or if the input is considered a network stream, and --cache= auto is used), this option is mostly ignored. (--cache-secs will override this. Technically, the maximum of both options is used.)
    /// The main purpose of this option is to limit the readhead for local playback, since a large readahead value is not overly useful in this case.
    /// (This value tends to be fuzzy, because many file formats don't store linear timestamps.)
    /// </summary>
    private MpvOption<double>? _demuxerReadAheadSecs;
    public MpvOption<double> DemuxerReadAheadSecs => _demuxerReadAheadSecs ??= new(this, "demuxer-readahead-secs");

    /// <summary>
    /// Prefetch next playlist entry while playback of the current entry is ending (default: no). This merely opens the URL of the next playlist entry as soon as the current URL is fully read.
    /// This does not work with URLs resolved by the youtube-dl wrapper, and it won't.
    /// This does not affect HLS(.m3u8 URLs) - HLS prefetching depends on the demuxer cache settings and is on by default.
    /// This can give subtly wrong results if per-file options are used, or if options are changed in the time window between prefetching start and next file played.
    /// This can occasionally make wrong prefetching decisions. For example, it can't predict whether you go backwards in the playlist, and assumes you won't edit the playlist.
    /// Highly experimental.
    /// </summary>
    private MpvOption<bool>? _prefetchPlaylist;
    public MpvOption<bool> PrefetchPlaylist => _prefetchPlaylist ??= new(this, "prefetch-playlist");

    /// <summary>
    /// If the player thinks that the media is not seekable (e.g. playing from a pipe, or it's an http stream with a server that doesn't support range requests), seeking will be disabled. This option can forcibly enable it. For seeks within the cache, there's a good chance of success.
    /// </summary>
    private MpvOption<bool>? _forceSeekable;
    public MpvOption<bool> ForceSeekable => _forceSeekable ??= new(this, "force-seekable");

    /// <summary>
    /// Before starting playback, read data until either the end of the file was reached, or the demuxer cache has reached maximum capacity. Only once this is done, playback starts. This intentionally happens before the initial seek triggered with --start. This does not change any runtime behavior after the initial caching. This option is useless if the file cannot be cached completely.
    /// </summary>
    private MpvOption<bool>? _demuxerCacheWait;
    public MpvOption<bool> DemuxerCacheWait => _demuxerCacheWait ??= new(this, "demuxer-cache-wait");

    /// <summary>
    /// When opening multi-volume rar files, open all volumes to create a full list of contained files (default: no). If disabled, only the archive entries whose headers are located within the first volume are listed (and thus played when opening a .rar file with mpv). Doing so speeds up opening, and the typical idiotic use-case of playing uncompressed multi-volume rar files that contain a single media file is made faster.
    /// </summary>
    private MpvOption<bool>? _rarListAllVolumes;
    public MpvOption<bool> RarListAllVolumes => _rarListAllVolumes ??= new(this, "rar-list-all-volumes");

    /// <summary>
    /// Use system settings for keyrepeat delay and rate, instead of --input-ar-delay and --input-ar-rate. (Whether this applies depends on the VO backend and how it handles keyboard input. Does not apply to terminal input.)
    /// </summary>
    private MpvOption<bool>? _nativeKeyRepeat;
    public MpvOption<bool> NativeKeyRepeat => _nativeKeyRepeat ??= new(this, "native-keyrepeat");

    /// <summary>
    /// Delay in milliseconds before we start to autorepeat a key (0 to disable).
    /// </summary>
    private MpvOption<int>? _inputAutoRepeatDelay;
    public MpvOption<int> InputAutoRepeatDelay => _inputAutoRepeatDelay ??= new(this, "input-ar-delay");

    /// <summary>
    /// Number of key presses to generate per second on autorepeat.
    /// </summary>
    private MpvOption<double>? _inputAutoRepeatRate;
    public MpvOption<double> InputAutoRepeatRate => _inputAutoRepeatRate ??= new(this, "input-ar-rate");

    /// <summary>
    /// Specify input configuration file other than the default location in the mpv configuration directory (usually ~/.config/mpv/input.conf).
    /// </summary>
    private MpvOptionString? _inputConf = null;
    public MpvOptionString InputConf => _inputConf ??= new(this, "input-conf");

    /// <summary>
    /// Enables or disables mpv default (built-in) key bindings.
    /// </summary>
    ///
    private MpvOption<bool>? _inputDefaultBindings = null;
    public MpvOption<bool> InputDefaultBindings => _inputDefaultBindings ??= new(this, "input-default-bindings");

    private MpvOption<bool>? _inputBuiltinBindings = null;
    public MpvOption<bool> InputBuiltinBindings => _inputBuiltinBindings ??= new(this, "input-builtin-bindings");

    /// <summary>
    /// Time in milliseconds to recognize two consecutive button presses as a double-click (default: 300).
    /// </summary>
    private MpvOption<int>? _inputDoubleClickTime;
    public MpvOption<int> InputDoubleClickTime => _inputDoubleClickTime ??= new(this, "input-doubleclick-time");

    /// <summary>
    /// Specify the size of the FIFO that buffers key events (default: 7). If it is too small, some events may be lost. The main disadvantage of setting it to a very large value is that if you hold down a key triggering some particularly slow command then the player may be unresponsive while it processes all the queued commands.
    /// </summary>
    private MpvOption<int>? _inputKeyFifoSize;
    public MpvOption<int> InputKeyFifoSize => _inputKeyFifoSize ??= new(this, "input-key-fifo-size");

    /// <summary>
    /// Input test mode. Instead of executing commands on key presses, mpv will show the keys and the bound commands on the OSD. Has to be used with a dummy video, and the normal ways to quit the player will not work (key bindings that normally quit will be shown on OSD only, just like any other binding). See INPUT.CONF.
    /// </summary>
    private MpvOption<bool>? _inputText;
    public MpvOption<bool> InputText => _inputText ??= new(this, "input-test");

    /// <summary>
    /// False prevents the player from reading key events from standard input. Useful when reading data from standard input. This is automatically enabled when - is found on the command line. There are situations where you have to set it manually, e.g. if you open /dev/stdin (or the equivalent on your system), use stdin in a playlist or intend to read from stdin later on via the loadfile or loadlist input commands.
    /// </summary>
    private MpvOption<bool>? _inputTerminal;
    public MpvOption<bool> InputTerminal => _inputTerminal ??= new(this, "input-terminal");

    /// <summary>
    /// Enable the IPC support and create the listening socket at the given path.
    /// On Linux and Unix, the given path is a regular filesystem path.On Windows, named pipes are used, so the path refers to the pipe namespace (\\.\pipe\name). If the \\.\pipe\ prefix is missing, mpv will add it automatically before creating the pipe, so --input-ipc-server=/tmp/mpv-socket and --input-ipc-server=\\.\pipe\tmp\mpv-socket are equivalent for IPC on Windows.
    /// </summary>
    private MpvOptionString? _inputIpcServer;
    public MpvOptionString InputIpcServer => _inputIpcServer ??= new(this, "input-ipc-server");

    /// <summary>
    /// Connect a single IPC client to the given FD. This is somewhat similar to --input-ipc-server, except no socket is created, and instead the passed FD is treated like a socket connection received from accept(). In practice, you could pass either a FD created by socketpair(), or a pipe. In both cases, you must sure the FD is actually inherited by mpv (do not set the POSIX CLOEXEC flag).
    /// The player quits when the connection is closed.
    /// This is somewhat similar to the removed --input-file option, except it supports only integer FDs, and cannot open actual paths.
    /// </summary>
    private MpvOptionString? _inputIpcClient;
    public MpvOptionString InputIpcClient => _inputIpcClient ??= new(this, "input-ipc-client");

    /// <summary>
    /// Enable/disable SDL2 Gamepad support. Disabled by default.
    /// </summary>
    private MpvOption<bool>? _inputGamepad;
    public MpvOption<bool> InputGamepad => _inputGamepad ??= new(this, "input-gamepad");

    /// <summary>
    /// Permit mpv to receive pointer events reported by the video output driver. Necessary to use the OSC, or to select the buttons in DVD menus. Support depends on the VO in use.
    /// </summary>
    private MpvOption<bool>? _inputCursor;
    public MpvOption<bool> InputCursor => _inputCursor ??= new(this, "input-cursor");

    /// <summary>
    /// (OS X and Windows only) Enable/disable media keys support. Enabled by default (except for libmpv).
    /// </summary>
    private MpvOption<bool>? _inputMediaKeys;
    public MpvOption<bool> InputMediaKeys => _inputMediaKeys ??= new(this, "input-media-keys");

    /// <summary>
    /// (Cocoa and Windows only) Use the right Alt key as Alt Gr to produce special characters. If disabled, count the right Alt as an Alt modifier key. Enabled by default.
    /// </summary>
    private MpvOption<bool>? _inputRightAltGr;
    public MpvOption<bool> InputRightAltGr => _inputRightAltGr ??= new(this, "input-right-alt-gr");

    /// <summary>
    /// Disable all keyboard input on for VOs which can't participate in proper keyboard input dispatching. May not affect all VOs. Generally useful for embedding only.
    /// </summary>
    private MpvOption<bool>? _inputVoKeyboard;
    public MpvOption<bool> InputVoKeyboard => _inputVoKeyboard ??= new(this, "input-vo-keyboard");

    /// <summary>
    /// Whether to load the on-screen-controller (default: yes).
    /// </summary>
    private MpvOption<bool>? _osc = null;
    public MpvOption<bool> Osc => _osc ??= new(this, "osc");

    /// <summary>
    /// Disable display of the OSD bar.
    /// </summary>
    private MpvOption<bool>? _osdBar;
    public MpvOption<bool> OsdBar => _osdBar ??= new(this, "osd-bar");

    /// <summary>
    /// Set what is displayed on the OSD during seeks. The default is bar.
    /// </summary>
    private MpvOptionEnum<OsdDisplay>? _osdOnSeek;
    public MpvOptionEnum<OsdDisplay> OsdOnSeek => _osdOnSeek ??= new(this, "osd-on-seek");

    /// <summary>
    /// Set the duration of the OSD messages in ms (default: 1000).
    /// </summary>
    private MpvOption<int>? _osdDuration = null;
    public MpvOption<int> OsdDuration => _osdDuration ??= new(this, "osd-duration");

    /// <summary>
    /// Specify font to use for OSD. The default is sans-serif.
    /// </summary>
    private MpvOptionString? _osdFont;
    public MpvOptionString OsdFont => _osdFont ??= new(this, "osd-font");

    /// <summary>
    /// Specify the OSD font size. See --sub-font-size for details.
    /// Default: 55.
    /// </summary>
    private MpvOption<int>? _osdFontSize;
    public MpvOption<int> OsdFontSize => _osdFontSize ??= new(this, "osd-font-size");

    /// <summary>
    /// Show this string as message on OSD with OSD level 1 (visible by default). The message will be visible by default, and as long as no other message covers it, and the OSD level isn't changed (see --osd-level). Expands properties; see Property Expansion.
    /// </summary>
    private MpvOptionString? _osdMsg1;
    public MpvOptionString OsdMsg1 => _osdMsg1 ??= new(this, "osd-msg1");

    /// <summary>
    /// Similar to OsdMsg1, but for OSD level 2. If this is an empty string (default), then the playback time is shown.
    /// </summary>
    private MpvOptionString? _osdMsg2;
    public MpvOptionString OsdMsg2 => _osdMsg2 ??= new(this, "osd-msg2");

    /// <summary>
    /// Similar to --osd-msg1, but for OSD level 3. If this is an empty string (default), then the playback time, duration, and some more information is shown.
    /// This is used for the show-progress command(by default mapped to P), and when seeking if enabled with --osd-on-seek or by osd- prefixes in input.conf(see Input Command Prefixes).
    /// </summary>
    private MpvOptionString? _osdMsg3;
    public MpvOptionString OsdMsg3 => _osdMsg3 ??= new(this, "osd-msg3");

    /// <summary>
    /// Show a message on OSD when playback starts. The string is expanded for properties, e.g. --osd-playing-msg='file: ${filename}' will show the message file: followed by a space and the currently played filename.
    /// </summary>
    private MpvOptionString? _osdPlayingMsg;
    public MpvOptionString OsdPlayingMsg => _osdPlayingMsg ??= new(this, "osd-playing-msg");

    /// <summary>
    /// Position of the OSD bar. -1 is far left, 0 is centered, 1 is far right. Fractional values (like 0.5) are allowed.
    /// </summary>
    private MpvOption<double>? _osdBarAlignX;
    public MpvOption<double> OsdBarAlignX => _osdBarAlignX ??= new(this, "osd-bar-align-x");

    /// <summary>
    /// Position of the OSD bar. -1 is top, 0 is centered, 1 is bottom. Fractional values (like 0.5) are allowed.
    /// </summary>
    private MpvOption<double>? _osdBarAlignY;
    public MpvOption<double> OsdBarAlignY => _osdBarAlignY ??= new(this, "osd-bar-align-y");

    /// <summary>
    /// Width of the OSD bar, in percentage of the screen width (default: 75). A value of 50 means the bar is half the screen wide.
    /// </summary>
    private MpvOption<double>? _osdBarW;
    public MpvOption<double> OsdBarW => _osdBarW ??= new(this, "osd-bar-w");

    /// <summary>
    /// Height of the OSD bar, in percentage of the screen height (default: 3.125).
    /// </summary>
    private MpvOption<double>? _osdBarH;
    public MpvOption<double> OsdBarH => _osdBarH ??= new(this, "osd-bar-h");

    /// <summary>
    /// Color used for OSD text background.
    /// </summary>
    private MpvOptionString? _osdBackColor;
    public MpvOptionString OsdBackColor => _osdBackColor ??= new(this, "osd-back-color");

    /// <summary>
    /// Gaussian blur factor. 0 means no blur applied (default).
    /// </summary>
    private MpvOption<double>? _osdBlur;
    public MpvOption<double> OsdBlur => _osdBlur ??= new(this, "osd-blur");

    /// <summary>
    /// Format text on bold.
    /// </summary>
    private MpvOption<bool>? _osdBold;
    public MpvOption<bool> OsdBold => _osdBold ??= new(this, "osd-bold");

    /// <summary>
    /// Format text on italic.
    /// </summary>
    private MpvOption<bool>? _osdItalic;
    public MpvOption<bool> OsdItalic => _osdItalic ??= new(this, "osd-italic");

    /// <summary>
    /// Color used for the OSD font border.
    /// </summary>
    private MpvOptionString? _osdBorderColor;
    public MpvOptionString OsdBorderColor => _osdBorderColor ??= new(this, "osd-border-color");

    /// <summary>
    /// Size of the OSD font border in scaled pixels (see --sub-font-size for details). A value of 0 disables borders.
    /// Default: 3.
    /// </summary>
    private MpvOption<int>? _osdBorderSize;
    public MpvOption<int> OsdBorderSize => _osdBorderSize ??= new(this, "osd-border-size");

    /// <summary>
    /// Specify the color used for OSD.
    /// </summary>
    private MpvOptionString? _osdColor;
    public MpvOptionString OsdColor => _osdColor ??= new(this, "osd-color");

    /// <summary>
    /// Show OSD times with fractions of seconds (in millisecond precision). Useful to see the exact timestamp of a video frame.
    /// </summary>
    private MpvOption<bool>? _osdFractions;
    public MpvOption<bool> OsdFractions => _osdFractions ??= new(this, "osd-fractions");

    /// <summary>
    /// Specifies which mode the OSD should start in.
    /// 0:	OSD completely disabled(subtitles only)
    /// 1:	enabled(shows up only on user interaction)
    /// 2:	enabled + current time visible by default
    /// 3:	enabled + --osd-status-msg(current time and status by default)
    /// </summary>
    private MpvOption<int>? _osdLevel;
    public MpvOption<int> OsdLevel => _osdLevel ??= new(this, "osd-level");

    /// <summary>
    /// Left and right screen margin for the OSD in scaled pixels (see --sub-font-size for details).
    /// This option specifies the distance of the OSD to the left, as well as at which distance from the right border long OSD text will be broken. Default: 25.
    /// </summary>
    private MpvOption<int>? _osdMarginX;
    public MpvOption<int> OsdMarginX => _osdMarginX ??= new(this, "osd-margin-x");

    /// <summary>
    /// Top and bottom screen margin for the OSD in scaled pixels (see --sub-font-size for details).
    /// This option specifies the vertical margins of the OSD. Default: 22.
    /// </summary>
    private MpvOption<int>? _osdMarginY;
    public MpvOption<int> OsdMarginY => _osdMarginY ??= new(this, "osd-margin-y");

    /// <summary>
    /// Control to which corner of the screen OSD should be aligned to (default: left).
    /// </summary>
    private MpvOption<AlignHorizontal>? _osdAlignX;
    public MpvOption<AlignHorizontal> OsdAlignX => _osdAlignX ??= new(this, "osd-align-x");

    /// <summary>
    /// Vertical position (default: top).
    /// </summary>
    private MpvOption<AlignVertical>? _osdAlignY;
    public MpvOption<AlignVertical> OsdAlignY => _osdAlignY ??= new(this, "osd-align-y");

    /// <summary>
    /// OSD font size multiplier, multiplied with OsdFontSize value.
    /// </summary>
    private MpvOption<double>? _osdScale;
    public MpvOption<double> OsdScale => _osdScale ??= new(this, "osd-scale");

    /// <summary>
    /// Whether to scale the OSD with the window size (default: yes). If this is disabled, --osd-font-size and other OSD options that use scaled pixels are always in actual pixels. The effect is that changing the window size won't change the OSD font size.
    /// </summary>
    private MpvOption<bool>? _osdScaleByWindow;
    public MpvOption<bool> OsdScaleByWindow => _osdScaleByWindow ??= new(this, "osd-scale-by-window");

    /// <summary>
    /// Color used for OSD shadow.
    /// </summary>
    private MpvOptionString? _osdShadowColor;
    public MpvOptionString OsdShadowColor => _osdShadowColor ??= new(this, "osd-shadow-color");

    /// <summary>
    /// Displacement of the OSD shadow in scaled pixels (see --sub-font-size for details). A value of 0 disables shadows. Default: 0.
    /// </summary>
    private MpvOption<int>? _osdShadowOffset;
    public MpvOption<int> OsdShadowOffset => _osdShadowOffset ??= new(this, "osd-shadow-offset");

    /// <summary>
    /// Horizontal OSD/sub font spacing in scaled pixels (see --sub-font-size for details). This value is added to the normal letter spacing. Negative values are allowed. Default: 0.
    /// </summary>
    private MpvOption<int>? _osdSpacing;
    public MpvOption<int> OsdSpacing => _osdSpacing ??= new(this, "osd-spacing");

    /// <summary>
    /// Enabled OSD rendering on the video window (default: yes). This can be used in situations where terminal OSD is preferred. If you just want to disable all OSD rendering, use --osd-level=0.
    /// It does not affect subtitles or overlays created by scripts(in particular, the OSC needs to be disabled with --no-osc).
    /// </summary>
    private MpvOption<bool>? _videoOsd;
    public MpvOption<bool> VideoOsd => _videoOsd ??= new(this, "video-osd");

    /// <summary>
    /// See --sub-font-provider for details and accepted values. Note that unlike subtitles, OSD never uses embedded fonts from media files.
    /// </summary>
    private MpvOptionString? _osdFontProvider;
    public MpvOptionString OsdFontProvider => _osdFontProvider ??= new(this, "osd-font-provider");

    /// <summary>
    /// Set the image file type used for saving screenshots.
    /// </summary>
    private MpvOptionEnum<ImageFormat>? _screenshotFormat;
    public MpvOptionEnum<ImageFormat> ScreenshotFormat => _screenshotFormat ??= new(this, "screenshot-format");

    /// <summary>
    /// Tag screenshots with the appropriate colorspace.
    /// Note that not all formats are supported. Default: no.
    /// </summary>
    private MpvOption<bool>? _screenshotTagColorspace;
    public MpvOption<bool> ScreenshotTagColorspace => _screenshotTagColorspace ??= new(this, "screenshot-tag-colorspace");

    /// <summary>
    /// If possible, write screenshots with a bit depth similar to the source video (default: yes). This is interesting in particular for PNG, as this sometimes triggers writing 16 bit PNGs with huge file sizes. This will also include an unused alpha channel in the resulting files if 16 bit is used.
    /// </summary>
    private MpvOption<bool>? _screenshotHighBitDepth;
    public MpvOption<bool> ScreenshotHighBitDepth => _screenshotHighBitDepth ??= new(this, "screenshot-high-bit-depth");

    /// <summary>
    /// Specify the filename template used to save screenshots. The template specifies the filename without file extension, and can contain format specifiers, which will be substituted when taking a screenshot. By default, the template is mpv-shot%n, which results in filenames like mpv-shot0012.png for example.
    /// The template can start with a relative or absolute path, in order to specify a directory location where screenshots should be saved.
    /// </summary>
    private MpvOptionString? _screenshotTemplate;
    public MpvOptionString ScreenshotTemplate => _screenshotTemplate ??= new(this, "screenshot-template");

    /// <summary>
    /// Store screenshots in this directory. This path is joined with the filename generated by --screenshot-template. If the template filename is already absolute, the directory is ignored.
    /// If the directory does not exist, it is created on the first screenshot.If it is not a directory, an error is generated when trying to write a screenshot.
    /// </summary>
    private MpvOptionString? _screenshotDirectory;
    public MpvOptionString ScreenshotDirectory => _screenshotDirectory ??= new(this, "screenshot-directory");

    /// <summary>
    /// Set the JPEG quality level. Higher means better quality. The default is 90.
    /// </summary>
    private MpvOption<int>? _screenshotJpegQuality;
    public MpvOption<int> ScreenshotJpegQuality => _screenshotJpegQuality ??= new(this, "screenshot-jpeg-quality");

    /// <summary>
    /// Write JPEG files with the same chroma subsampling as the video (default: yes). If disabled, the libjpeg default is used.
    /// </summary>
    private MpvOption<bool>? _screenshotJpegSourceChroma;
    public MpvOption<bool> ScreenshotJpegSourceChroma => _screenshotJpegSourceChroma ??= new(this, "screenshot-jpeg-source-chroma");

    /// <summary>
    /// Set the PNG compression level (0-9). Higher means better compression. This will affect the file size of the written screenshot file and the time it takes to write a screenshot. Too high compression might occupy enough CPU time to interrupt playback. The default is 7.
    /// </summary>
    private MpvOption<int>? _screenshotPngCompression;
    public MpvOption<int> ScreenshotPngCompression => _screenshotPngCompression ??= new(this, "screenshot-png-compression");

    /// <summary>
    /// Set the filter applied prior to PNG compression. 0 is none, 1 is "sub", 2 is "up", 3 is "average", 4 is "Paeth", and 5 is "mixed". This affects the level of compression that can be achieved. For most images, "mixed" achieves the best compression ratio, hence it is the default.
    /// </summary>
    private MpvOption<int>? _screenshotPngFilter;
    public MpvOption<int> ScreenshotPngFilter => _screenshotPngFilter ??= new(this, "screenshot-png-filter");

    /// <summary>
    /// Write lossless WebP files. --screenshot-webp-quality is ignored if this is set. The default is no.
    /// </summary>
    private MpvOption<bool>? _screenshotWebpLossless;
    public MpvOption<bool> ScreenshotWebpLossless => _screenshotWebpLossless ??= new(this, "screenshot-webp-lossless");

    /// <summary>
    /// Set the WebP quality level (0-100). Higher means better quality. The default is 75.
    /// </summary>
    private MpvOption<int>? _screenshotWebpQuality;
    public MpvOption<int> ScreenshotWebpQuality => _screenshotWebpQuality ??= new(this, "screenshot-webp-quality");

    /// <summary>
    /// Set the WebP compression level (0-6). Higher means better compression, but takes more CPU time. Note that this also affects the screenshot quality when used with lossy WebP files. The default is 4.
    /// </summary>
    private MpvOption<int>? _screenshotWebpCompression;
    public MpvOption<int> ScreenshotWebpCompression => _screenshotWebpCompression ??= new(this, "screenshot-webp-compression");

    /// <summary>
    /// Whether to use software rendering for screenshots (default: no).
    /// If set to no, the screenshot will be rendered by the current VO if possible(only vo_gpu currently). The advantage is that this will (probably) always show up as in the video window, because the same code is used for rendering.But since the renderer needs to be reinitialized, this can be slow and interrupt playback. (Unless the window mode is used with the screenshot command.)
    /// If set to yes, the software scaler is used to convert the video to RGB (or whatever the target screenshot requires). In this case, conversion will run in a separate thread and will probably not interrupt playback.The software renderer may lack some capabilities, such as HDR rendering.
    /// </summary>
    private MpvOption<bool>? _screenshotSw;
    public MpvOption<bool> ScreenshotSw => _screenshotSw ??= new(this, "screenshot-sw");

    /// <summary>
    /// Specify the software scaler algorithm to be used with --vf=scale. This also affects video output drivers which lack hardware acceleration, e.g. x11. See also --vf=scale.
    /// To get a list of available scalers, run --sws-scaler=help.
    /// Default: bicubic.
    /// </summary>
    private MpvOptionString? _swsScaler;
    public MpvOptionString SwsScaler => _swsScaler ??= new(this, "sws-scaler");

    /// <summary>
    /// Software scaler Gaussian blur filter for luma (0-100).
    /// </summary>
    private MpvOption<int>? _swsBlurLuma;
    public MpvOption<int> SwsBlurLuma => _swsBlurLuma ??= new(this, "sws-lgb");

    /// <summary>
    /// Software scaler Gaussian blur filter for luma (0-100).
    /// </summary>
    private MpvOption<int>? _swsBlurChroma;
    public MpvOption<int> SwsBlurChroma => _swsBlurChroma ??= new(this, "sws-cgb");

    /// <summary>
    /// Software scaler sharpen filter for luma (-100 - 100).
    /// </summary>
    private MpvOption<int>? _swsSharpenLuma;
    public MpvOption<int> SwsSharpenLuma => _swsSharpenLuma ??= new(this, "sws-ls");

    /// <summary>
    /// Software scaler sharpen filter for chroma (-100 - 100).
    /// </summary>
    private MpvOption<int>? _swsSharpenChroma;
    public MpvOption<int> SwsSharpenChroma => _swsSharpenChroma ??= new(this, "sws-cs");

    /// <summary>
    /// Software scaler chroma horizontal shifting.
    /// </summary>
    private MpvOption<int>? _swsHorizontalShifting;
    public MpvOption<int> SwsHorizontalShifting => _swsHorizontalShifting ??= new(this, "sws-chs");

    /// <summary>
    /// Software scaler chroma vertical shifting.
    /// </summary>
    private MpvOption<int>? _swsVerticalShifting;
    public MpvOption<int> SwsVerticalShifting => _swsVerticalShifting ??= new(this, "sws-cvs");

    /// <summary>
    /// Unknown functionality (default: no). Consult libswscale source code. The primary purpose of this, as far as libswscale API goes), is to produce exactly the same output for the same input on all platforms (output has the same "bits" everywhere, thus "bitexact"). Typically disables optimizations.
    /// </summary>
    private MpvOption<bool>? _swsBitExact;
    public MpvOption<bool> SwsBitExact => _swsBitExact ??= new(this, "sws-bitexact");

    /// <summary>
    /// Allow optimizations that help with performance, but reduce quality (default: no).
    /// VOs like drm and x11 will benefit a lot from using --sws-fast.You may need to set other options, like --sws-scaler.The builtin sws-fast profile sets this option and some others to gain performance for reduced quality. Also see --sws-allow-zimg.
    /// </summary>
    private MpvOption<bool>? _swsFast;
    public MpvOption<bool> SwsFast => _swsFast ??= new(this, "sws-fast");

    /// <summary>
    /// Allow using zimg (if the component using the internal swscale wrapper explicitly allows so) (default: yes). In this case, zimg may be used, if the internal zimg wrapper supports the input and output formats. It will silently or noisily fall back to libswscale if one of these conditions does not apply.
    /// If zimg is used, the other --sws- options are ignored, and the --zimg- options are used instead.
    /// If the internal component using the swscale wrapper hooks up logging correctly, a verbose priority log message will indicate whether zimg is being used.
    /// Most things which need software conversion can make use of this.
    /// </summary>
    private MpvOption<bool>? _swsAllowZimg;
    public MpvOption<bool> SwsAllowZimg => _swsAllowZimg ??= new(this, "sws-allow-zimg");

    /// <summary>
    /// Zimg luma scaler to use (default: lanczos).
    /// </summary>
    private MpvOptionEnum<ZimgScaler>? _zimgScaler;
    public MpvOptionEnum<ZimgScaler> ZimgScaler => _zimgScaler ??= new(this, "zimg-scaler");

    /// <summary>
    /// Set scaler parameters. By default, these are set to the special string default, which maps to a scaler-specific default value. Ignored if the scaler is not tunable.
    /// </summary>
    private MpvOptionWithDefault<double>? _zimgScalerParamA;
    public MpvOptionWithDefault<double> ZimgScalerParamA => _zimgScalerParamA ??= new(this, "zimg-scaler-param-a");

    /// <summary>
    /// Set scaler parameters. By default, these are set to the special string default, which maps to a scaler-specific default value. Ignored if the scaler is not tunable.
    /// </summary>
    private MpvOptionWithDefault<double>? _zimgScalerParamB;
    public MpvOptionWithDefault<double> ZimgScalerParamB => _zimgScalerParamB ??= new(this, "zimg-scaler-param-b");

    /// <summary>
    /// Zimg chroma scaler to use (default: lanczos).
    /// </summary>
    private MpvOptionEnum<ZimgScaler>? _zimgScalerChroma;
    public MpvOptionEnum<ZimgScaler> ZimgScalerChroma => _zimgScalerChroma ??= new(this, "zimg-scaler-chroma");

    /// <summary>
    /// Set scaler parameters. By default, these are set to the special string default, which maps to a scaler-specific default value. Ignored if the scaler is not tunable.
    /// </summary>
    private MpvOptionWithDefault<double>? _zimgScalerChromaParamA;
    public MpvOptionWithDefault<double> ZimgScalerChromaParamA => _zimgScalerChromaParamA ??= new(this, "zimg-scaler-chroma-param-a");

    /// <summary>
    /// Set scaler parameters. By default, these are set to the special string default, which maps to a scaler-specific default value. Ignored if the scaler is not tunable.
    /// </summary>
    private MpvOptionWithDefault<double>? _zimgScalerChromaParamB;
    public MpvOptionWithDefault<double> ZimgScalerChromaParamB => _zimgScalerChromaParamB ??= new(this, "zimg-scaler-chroma-param-b");

    /// <summary>
    /// Dithering (default: random).
    /// </summary>
    private MpvOptionEnum<DitherMode>? _zimgDither;
    public MpvOptionEnum<DitherMode> ZimgDither => _zimgDither ??= new(this, "zimg-dither");

    /// <summary>
    /// Set the maximum number of threads to use for scaling (default: auto). auto uses the number of logical cores on the current machine. Note that the scaler may use less threads (or even just 1 thread) depending on stuff. Passing a value of 1 disables threading and always scales the image in a single operation. Higher thread counts waste resources, but make it typically faster.
    /// Note that some zimg git versions had bugs that will corrupt the output if threads are used.
    /// </summary>
    private MpvOptionWithAuto<int>? _zimgThreads;
    public MpvOptionWithAuto<int> ZimgThreads => _zimgThreads ??= new(this, "zimg-threads");

    /// <summary>
    /// Allow optimizations that help with performance, but reduce quality (default: yes). Currently, this may simplify gamma conversion operations.
    /// </summary>
    private MpvOption<bool>? _zimgFast;
    public MpvOption<bool> ZimgFast => _zimgFast ??= new(this, "zimg-fast");

    /// <summary>
    /// Length of the filter with respect to the lower sampling rate. (default: 16)
    /// </summary>
    private MpvOption<int>? _audioResampleFilterSize;
    public MpvOption<int> AudioResampleFilterSize => _audioResampleFilterSize ??= new(this, "audio-resample-filter-size");

    /// <summary>
    /// Log2 of the number of polyphase entries. (..., 10->1024, 11->2048, 12->4096, ...) (default: 10->1024)
    /// </summary>
    private MpvOption<int>? _audioResamplePhaseShift;
    public MpvOption<int> AudioResamplePhaseShift => _audioResamplePhaseShift ??= new(this, "audio-resample-phase-shift");

    /// <summary>
    /// Cutoff frequency (0.0-1.0), default set depending upon filter length.
    /// </summary>
    private MpvOption<double>? _audioResampleCutoff;
    public MpvOption<double> AudioResampleCutoff => _audioResampleCutoff ??= new(this, "audio-resample-cutoff");

    /// <summary>
    /// If set then filters will be linearly interpolated between polyphase entries. (default: no)
    /// </summary>
    private MpvOption<bool>? _audioResampleLinear;
    public MpvOption<bool> AudioResampleLinear => _audioResampleLinear ??= new(this, "audio-resample-linear");

    /// <summary>
    /// Enable/disable normalization if surround audio is downmixed to stereo (default: no). If this is disabled, downmix can cause clipping. If it's enabled, the output might be too quiet. It depends on the source audio.
    /// Technically, this changes the normalize suboption of the lavrresample audio filter, which performs the downmixing.
    /// If downmix happens outside of mpv for some reason, or in the decoder (decoder downmixing), or in the audio output (system mixer), this has no effect.
    /// </summary>
    private MpvOption<bool>? _audioNormalizeDownmix;
    public MpvOption<bool> AudioNormalizeDownmix => _audioNormalizeDownmix ??= new(this, "audio-normalize-downmix");

    /// <summary>
    /// Limit maximum size of audio frames filtered at once, in ms (default: 40). The output size size is limited in order to make resample speed changes react faster. This is necessary especially if decoders or filters output very large frame sizes (like some lossless codecs or some DRC filters). This option does not affect the resampling algorithm in any way.
    /// </summary>
    private MpvOption<int>? _audioResampleMaxOutputSize;
    public MpvOption<int> AudioResampleMaxOutputSize => _audioResampleMaxOutputSize ??= new(this, "audio-resample-max-output-size");

    /// <summary>
    /// Set AVOptions on the SwrContext or AVAudioResampleContext. These should be documented by FFmpeg or Libav.
    /// </summary>
    private MpvOptionDictionary? _audioResampleOptions;
    public MpvOptionDictionary AudioResampleOptions => _audioResampleOptions ??= new(this, "audio-swresample-o");

    /// <summary>
    /// Make console output less verbose; in particular, prevents the status line (i.e. AV: 3.4 (00:00:03.37) / 5320.6 ...) from being displayed. Particularly useful on slow terminals or broken ones which do not properly handle carriage return (i.e. \r).
    /// </summary>
    private MpvOption<bool>? _quiet;
    public MpvOption<bool> Quiet => _quiet ??= new(this, "quiet");

    /// <summary>
    /// Display even less output and status messages than with --quiet.
    /// </summary>
    private MpvOption<bool>? _reallyQuiet;
    public MpvOption<bool> ReallyQuiet => _reallyQuiet ??= new(this, "really-quiet");

    /// <summary>
    /// Setting false disables any use of the terminal and stdin/stdout/stderr. This completely silences any message output.
    /// Unlike --really-quiet, this disables input and terminal initialization as well.
    /// </summary>
    private MpvOption<bool>? _terminal = null;
    public MpvOption<bool> Terminal => _terminal ??= new(this, "terminal");

    /// <summary>
    /// Disable colorful console output on terminals.
    /// </summary>
    private MpvOption<bool>? _terminalNoMsgColor;
    public MpvOption<bool> TerminalNoMsgColor => _terminalNoMsgColor ??= new(this, "no-msg-color");

    /// <summary>
    /// Control verbosity directly for each module. The all module changes the verbosity of all the modules. The verbosity changes from this option are applied in order from left to right, and each item can override a previous one.
    /// Available levels:
    /// no:	complete silence
    /// fatal:	fatal messages only
    /// error:	error messages
    /// warn:	warning messages
    /// info:	informational messages
    /// status:	status messages(default)
    /// v:	verbose messages
    /// debug:	debug messages
    /// trace:	very noisy debug messages
    /// </summary>
    private MpvOptionRefDictionary? _terminalMsgLevel = null;
    public MpvOptionRefDictionary TerminalMsgLevel => _terminalMsgLevel ??= new(this, "msg-level");

    /// <summary>
    /// Control whether OSD messages are shown on the console when no video output is available (default: auto).
    /// </summary>
    private MpvOptionEnum<TermOsdMode>? _terminalOsd;
    public MpvOptionEnum<TermOsdMode> TerminalOsd => _terminalOsd ??= new(this, "term-osd");

    /// <summary>
    /// Enable printing a progress bar under the status line on the terminal. (Disabled by default.)
    /// </summary>
    private MpvOption<bool>? _terminalOsdBar;
    public MpvOption<bool> TerminalOsdBar => _terminalOsdBar ??= new(this, "term-osd-bar");

    /// <summary>
    /// Customize the --term-osd-bar feature. The string is expected to consist of 5 characters (start, left space, position indicator, right space, end). You can use Unicode characters, but note that double- width characters will not be treated correctly.
    /// Default: [-+-].
    /// </summary>
    private MpvOption<bool>? _terminalOsdBarChars;
    public MpvOption<bool> TerminalOsdBarChars => _terminalOsdBarChars ??= new(this, "term-osd-bar-chars");

    /// <summary>
    /// Print out a string after starting playback. The string is expanded for properties, e.g. --term-playing-msg='file: ${filename}' will print the string file: followed by a space and the currently played filename.
    /// </summary>
    private MpvOptionString? _terminalPlayingMessage;
    public MpvOptionString TerminalPlayingMessage => _terminalPlayingMessage ??= new(this, "term-playing-msg");

    /// <summary>
    /// Print out a custom string during playback instead of the standard status line. Expands properties. See Property Expansion.
    /// </summary>
    private MpvOptionString? _terminalStatusMessage;
    public MpvOptionString TerminalStatusMessage => _terminalStatusMessage ??= new(this, "term-status-msg");

    /// <summary>
    /// Set the terminal title. Currently, this simply concatenates the escape sequence setting the window title with the provided (property expanded) string. This will mess up if the expanded string contain bytes that end the escape sequence, or if the terminal does not understand the sequence. The latter probably includes the regrettable win32.
    /// </summary>
    private MpvOptionString? _terminalTitle;
    public MpvOptionString TerminalTitle => _terminalTitle ??= new(this, "term-title");

    /// <summary>
    /// Prepend module name to each console message.
    /// </summary>
    private MpvOption<bool>? _terminalMsgModule;
    public MpvOption<bool> TerminalMsgModule => _terminalMsgModule ??= new(this, "msg-module");

    /// <summary>
    /// Prepend timing information to each console message. The time is in seconds since the player process was started (technically, slightly later actually), using a monotonic time source depending on the OS. This is CLOCK_MONOTONIC on sane UNIX variants.
    /// </summary>
    private MpvOption<bool>? _terminalMsgTime;
    public MpvOption<bool> TerminalMsgTime => _terminalMsgTime ??= new(this, "msg-time");

    /// <summary>
    /// Decide whether to use network cache settings (default: auto).
    /// If enabled, use up to --cache-secs for the cache size(but still limited to --demuxer-max-bytes), and make the cached data seekable(if possible). If disabled, --cache-pause and related are implicitly disabled.
    /// The auto choice enables this depending on whether the stream is thought to involve network accesses or other slow media (this is an imperfect heuristic).
    /// </summary>
    private MpvOptionWithAuto<bool>? _cache;
    public MpvOptionWithAuto<bool> Cache => _cache ??= new(this, "cache");

    /// <summary>
    /// Write packet data to a temporary file, instead of keeping them in memory. This makes sense only with --cache. If the normal cache is disabled, this option is ignored.
    /// You need to set CacheDir to use this.
    /// </summary>
    private MpvOption<bool>? _cacheOnDisk;
    public MpvOption<bool> CacheOnDisk => _cacheOnDisk ??= new(this, "cache-on-disk");

    /// <summary>
    /// Directory where to create temporary files (default: none).
    /// Currently, this is used for CacheOnDisk only.
    /// </summary>
    private MpvOptionString? _cacheDir;
    public MpvOptionString CacheDir => _cacheDir ??= new(this, "cache-dir");

    /// <summary>
    /// Whether the player should automatically pause when the cache runs out of data and stalls decoding/playback (default: yes). If enabled, it will pause and unpause once more data is available, aka "buffering".
    /// </summary>
    private MpvOption<bool>? _cachePause;
    public MpvOption<bool> CachePause => _cachePause ??= new(this, "cache-pause");

    /// <summary>
    /// Number of seconds the packet cache should have buffered before starting playback again if "buffering" was entered (default: 1). This can be used to control how long the player rebuffers if --cache-pause is enabled, and the demuxer underruns. If the given time is higher than the maximum set with --cache-secs or --demuxer-readahead-secs, or prefetching ends before that for some other reason (like file end or maximum configured cache size reached), playback resumes earlier.
    /// </summary>
    private MpvOption<double>? _cachePauseWait;
    public MpvOption<double> CachePauseWait => _cachePauseWait ??= new(this, "cache-pause-wait");

    /// <summary>
    /// Enter "buffering" mode before starting playback (default: no). This can be used to ensure playback starts smoothly, in exchange for waiting some time to prefetch network data (as controlled by --cache-pause-wait). For example, some common behavior is that playback starts, but network caches immediately underrun when trying to decode more data as playback progresses.
    /// Another thing that can happen is that the network prefetching is so CPU demanding(due to demuxing in the background) that playback drops frames at first.In these cases, it helps enabling this option, and setting --cache-secs and --cache-pause-wait to roughly the same value.
    /// This option also triggers when playback is restarted after seeking.
    /// </summary>
    private MpvOption<bool>? _cachePauseInitial;
    public MpvOption<bool> CachePauseInitial => _cachePauseInitial ??= new(this, "cache-pause-initial");

    /// <summary>
    /// Whether or when to unlink cache files (default: immediate). This affects cache files which are inherently temporary, and which make no sense to remain on disk after the player terminates. This is a debugging option.
    /// </summary>
    private MpvOptionEnum<CacheUnlinkMode>? _cacheUnlinkFiles;
    public MpvOptionEnum<CacheUnlinkMode> CacheUnlinkFiles => _cacheUnlinkFiles ??= new(this, "cache-unlink-files");

    /// <summary>
    /// Size of the low level stream byte buffer (default: 128KB). This is used as buffer between demuxer and low level I/O (e.g. sockets). Generally, this can be very small, and the main purpose is similar to the internal buffer FILE in the C standard library will have.
    /// </summary>
    private MpvOption<int>? _streamBufferSize;
    public MpvOption<int> StreamBufferSize => _streamBufferSize ??= new(this, "stream-buffer-size");

    /// <summary>
    /// Enable running the video decoder on a separate thread (default: no). If enabled, the decoder is run on a separate thread, and a frame queue is put between decoder and higher level playback logic. The size of the frame queue is defined by the other options below.
    /// </summary>
    private MpvOption<bool>? _decoderQueueVideoEnable;
    public MpvOption<bool> DecoderQueueVideoEnable => _decoderQueueVideoEnable ??= new(this, "vd-queue-enable");

    /// <summary>
    /// Enable running the audio decoder on a separate thread (default: no). If enabled, the decoder is run on a separate thread, and a frame queue is put between decoder and higher level playback logic. The size of the frame queue is defined by the other options below.
    /// </summary>
    private MpvOption<bool>? _decoderQueueAudioEnable;
    public MpvOption<bool> DecoderQueueAudioEnable => _decoderQueueAudioEnable ??= new(this, "ad-queue-enable");

    /// <summary>
    /// Maximum approximate allowed size of the queue. If exceeded, decoding will be stopped. The maximum size can be exceeded by about 1 frame.
    /// </summary>
    private MpvOption<int>? _decoderQueueVideoMaxBytes;
    public MpvOption<int> DecoderQueueVideoMaxBytes => _decoderQueueVideoMaxBytes ??= new(this, "vd-queue-max-bytes");

    /// <summary>
    /// Maximum approximate allowed size of the queue. If exceeded, decoding will be stopped. The maximum size can be exceeded by about 1 frame.
    /// </summary>
    private MpvOption<int>? _decoderQueueAudioMaxBytes;
    public MpvOption<int> DecoderQueueAudioMaxBytes => _decoderQueueAudioMaxBytes ??= new(this, "ad-queue-max-bytes");

    /// <summary>
    /// Maximum number of frames of the queue. The audio size may be exceeded by about 1 frame.
    /// </summary>
    private MpvOption<int>? _decoderQueueVideoMaxSamples;
    public MpvOption<int> DecoderQueueVideoMaxSamples => _decoderQueueVideoMaxSamples ??= new(this, "vd-queue-max-samples");

    /// <summary>
    /// Maximum number of samples of the queue. The audio size may be exceeded by about 1 frame.
    /// </summary>
    private MpvOption<int>? _decoderQueueAudioMaxSamples;
    public MpvOption<int> DecoderQueueAudioMaxSamples => _decoderQueueAudioMaxSamples ??= new(this, "ad-queue-max-samples");

    /// <summary>
    /// Maximum number of seconds of media in the queue. The special value 0 means no limit is set. The queue size may be exceeded by about 2 frames. Timestamp resets may lead to random queue size usage.
    /// </summary>
    private MpvOption<double>? _decoderQueueVideoMaxSecs;
    public MpvOption<double> DecoderQueueVideoMaxSecs => _decoderQueueVideoMaxSecs ??= new(this, "vd-queue-max-secs");

    /// <summary>
    /// Maximum number of seconds of media in the queue. The special value 0 means no limit is set. The queue size may be exceeded by about 2 frames. Timestamp resets may lead to random queue size usage.
    /// </summary>
    private MpvOption<double>? _decoderQueueAudioMaxSecs;
    public MpvOption<double> DecoderQueueAudioMaxSecs => _decoderQueueAudioMaxSecs ??= new(this, "ad-queue-max-secs");

    /// <summary>
    /// Use value as user agent for HTTP streaming.
    /// </summary>
    private MpvOptionString? _userAgent;
    public MpvOptionString UserAgent => _userAgent ??= new(this, "user-agent");

    /// <summary>
    /// Support cookies when making HTTP requests. Disabled by default.
    /// </summary>
    private MpvOption<bool>? _cookies;
    public MpvOption<bool> Cookies => _cookies ??= new(this, "cookies");

    /// <summary>
    /// Read HTTP cookies from filename. The file is assumed to be in Netscape format.
    /// </summary>
    private MpvOptionString? _cookiesFile;
    public MpvOptionString CookiesFile => _cookiesFile ??= new(this, "cookies-file");

    /// <summary>
    /// Set custom HTTP fields when accessing HTTP stream.
    /// </summary>
    private MpvOptionDictionary? _httpHeaderFields = null;
    public MpvOptionDictionary HttpHeaderFields => _httpHeaderFields ??= new(this, "http-header-fields");

    /// <summary>
    /// URL of the HTTP/HTTPS proxy. If this is set, the http_proxy environment is ignored. The no_proxy environment variable is still respected. This option is silently ignored if it does not start with http://. Proxies are not used for https URLs. Setting this option does not try to make the ytdl script use the proxy.
    /// </summary>
    private MpvOptionString? _httpProxy;
    public MpvOptionString HttpProxy => _httpProxy ??= new(this, "http-proxy");

    /// <summary>
    /// Certificate authority database file for use with TLS. (Silently fails with older FFmpeg or Libav versions.)
    /// </summary>
    private MpvOptionString? _tlsCaFile;
    public MpvOptionString TlsCaFile => _tlsCaFile ??= new(this, "tls-ca-file");

    /// <summary>
    /// Verify peer certificates when using TLS (e.g. with https://...). (Silently fails with older FFmpeg or Libav versions.)
    /// </summary>
    private MpvOption<bool>? _tlsVerify;
    public MpvOption<bool> TlsVerify => _tlsVerify ??= new(this, "tls-verify");

    /// <summary>
    /// A file containing a certificate to use in the handshake with the peer.
    /// </summary>
    private MpvOptionString? _tlsCertFile;
    public MpvOptionString TlsCertFile => _tlsCertFile ??= new(this, "tls-cert-file");

    /// <summary>
    /// A file containing the private key for the certificate.
    /// </summary>
    private MpvOptionString? _tlsKeyFile;
    public MpvOptionString TlsKeyFile => _tlsKeyFile ??= new(this, "tls-key-file");

    /// <summary>
    /// Specify a referrer path or URL for HTTP requests.
    /// </summary>
    private MpvOptionString? _referrer;
    public MpvOptionString Referrer => _referrer ??= new(this, "referrer");

    /// <summary>
    /// Specify the network timeout in seconds (default: 60 seconds). This affects at least HTTP. The special value 0 uses the FFmpeg/Libav defaults. If a protocol is used which does not support timeouts, this option is silently ignored.
    /// </summary>
    private MpvOption<double>? _networkTimeout;
    public MpvOption<double> NetworkTimeout => _networkTimeout ??= new(this, "network-timeout");

    /// <summary>
    /// Select RTSP transport method (default: tcp). This selects the underlying network transport when playing rtsp://... URLs. The value lavf leaves the decision to libavformat.
    /// </summary>
    private MpvOptionEnum<RtspTransportMode>? _rtspTransport;
    public MpvOptionEnum<RtspTransportMode> RtspTransport => _rtspTransport ??= new(this, "rtsp-transport");

    /// <summary>
    /// If HLS streams are played, this option controls what streams are selected by default. The option allows the following parameters:
    /// no:	Don't do anything special. Typically, this will simply pick the first audio/video streams it can find.
    /// min:	Pick the streams with the lowest bitrate.
    /// max:	Same, but highest bitrate. (Default.)
    /// Additionally, if the option is a number, the stream with the highest rate equal or below the option value is selected.
    /// </summary>
    private MpvOptionString? _hlsBitrate;
    public MpvOptionString HlsBitrate => _hlsBitrate ??= new(this, "hls-bitrate");

    /// <summary>
    /// This defines the program to tune to. Usually, you may specify this by using a stream URI like "dvb://ZDF HD", but you can tune to a different channel by writing to this property at runtime. Also see dvbin-channel-switch-offset for more useful channel switching functionality.
    /// </summary>
    private MpvOptionString? _dvdbinProgram;
    public MpvOptionString DvdbinProgram => _dvdbinProgram ??= new(this, "dvbin-prog");

    /// <summary>
    /// Specifies using card number 0-15 (default: 0).
    /// </summary>
    private MpvOption<int>? _dvdbinCard;
    public MpvOption<int> DvdbinCard => _dvdbinCard ??= new(this, "dvbin-card");

    /// <summary>
    /// Instructs mpv to read the channels list from filename. The default is in the mpv configuration directory (usually ~/.config/mpv) with the filename channels.conf.{sat,ter,cbl,atsc} (based on your card type) or channels.conf as a last resort. For DVB-S/2 cards, a VDR 1.7.x format channel list is recommended as it allows tuning to DVB-S2 channels, enabling subtitles and decoding the PMT (which largely improves the demuxing). Classic mplayer format channel lists are still supported (without these improvements), and for other card types, only limited VDR format channel list support is implemented (patches welcome). For channels with dynamic PID switching or incomplete channels.conf, --dvbin-full-transponder or the magic PID 8192 are recommended.
    /// </summary>
    private MpvOptionString? _dvdbinFile;
    public MpvOptionString DvdbinFile => _dvdbinFile ??= new(this, "dvbin-file");

    /// <summary>
    /// Maximum number of seconds (1-30) to wait when trying to tune a frequency before giving up (default: 30).
    /// </summary>
    private MpvOption<int>? _dvdbinTimeout;
    public MpvOption<int> DvdbinTimeout => _dvdbinTimeout ??= new(this, "dvbin-timeout");

    /// <summary>
    /// Apply no filters on program PIDs, only tune to frequency and pass full transponder to demuxer. The player frontend selects the streams from the full TS in this case, so the program which is shown initially may not match the chosen channel. Switching between the programs is possible by cycling the program property. This is useful to record multiple programs on a single transponder, or to work around issues in the channels.conf. It is also recommended to use this for channels which switch PIDs on-the-fly, e.g. for regional news.
    /// Default: no
    /// </summary>
    private MpvOption<bool>? _dvdbinFullTransponder;
    public MpvOption<bool> DvdbinFullTransponder => _dvdbinFullTransponder ??= new(this, "dvbin-full-transponder");

    /// <summary>
    /// This value is not meant for setting via configuration, but used in channel switching. An input.conf can cycle this value up and down to perform channel switching. This number effectively gives the offset to the initially tuned to channel in the channel list.
    /// </summary>
    private MpvOption<int>? _dvdbinChannelSwitchOffset;
    public MpvOption<int> DvdbinChannelSwitchOffset => _dvdbinChannelSwitchOffset ??= new(this, "dvbin-channel-switch-offset");

    /// <summary>
    /// Enable ALSA resampling plugin. (This is disabled by default, because some drivers report incorrect audio delay in some cases.)
    /// </summary>
    private MpvOption<bool>? _alsaResample;
    public MpvOption<bool> AlsaResample => _alsaResample ??= new(this, "alsa-resample");

    /// <summary>
    /// Set the mixer device used with ao-volume (default: default).
    /// </summary>
    private MpvOptionString? _alsaMixerDevice;
    public MpvOptionString AlsaMixerDevice => _alsaMixerDevice ??= new(this, "alsa-mixer-device");

    /// <summary>
    /// Set the name of the mixer element (default: Master). This is for example PCM or Master.
    /// </summary>
    private MpvOptionString? _alsaMixerName;
    public MpvOptionString AlsaMixerName => _alsaMixerName ??= new(this, "alsa-mixer-name");

    /// <summary>
    /// Set the index of the mixer channel (default: 0). Consider the output of "amixer scontrols", then the index is the number that follows the name of the element.
    /// </summary>
    private MpvOption<int>? _alsaMixerIndex;
    public MpvOption<int> AlsaMixerIndex => _alsaMixerIndex ??= new(this, "alsa-mixer-index");

    /// <summary>
    /// Allow output of non-interleaved formats (if the audio decoder uses this format). Currently disabled by default, because some popular ALSA plugins are utterly broken with non-interleaved formats.
    /// </summary>
    private MpvOption<bool>? _alsaNonInterleaved;
    public MpvOption<bool> AlsaNonInterleaved => _alsaNonInterleaved ??= new(this, "alsa-non-interleaved");

    /// <summary>
    /// Don't read or set the channel map of the ALSA device - only request the required number of channels, and then pass the audio as-is to it. This option most likely should not be used. It can be useful for debugging, or for static setups with a specially engineered ALSA configuration (in this case you should always force the same layout with --audio-channels, or it will work only for files which use the layout implicit to your ALSA device).
    /// </summary>
    private MpvOption<bool>? _alsaIgnoreChmap;
    public MpvOption<bool> AlsaIgnoreChmap => _alsaIgnoreChmap ??= new(this, "alsa-ignore-chmap");

    /// <summary>
    /// Set the requested buffer time in microseconds. A value of 0 skips requesting anything from the ALSA API. This and the --alsa-periods option uses the ALSA near functions to set the requested parameters. If doing so results in an empty configuration set, setting these parameters is skipped.
    /// Both options control the buffer size.A low buffer size can lead to higher CPU usage and audio dropouts, while a high buffer size can lead to higher latency in volume changes and other filtering.
    /// </summary>
    private MpvOption<int>? _alsaBufferTime;
    public MpvOption<int> AlsaBufferTime => _alsaBufferTime ??= new(this, "alsa-buffer-time");

    /// <summary>
    /// Number of periods requested from the ALSA API. See --alsa-buffer-time for further remarks.
    /// </summary>
    private MpvOption<int>? _alsaPeriods;
    public MpvOption<int> AlsaPeriods => _alsaPeriods ??= new(this, "alsa-periods");

    /// <summary>
    /// The filter function to use when upscaling video.
    /// </summary>
    private ScaleOptions? _scale;
    public ScaleOptions Scale => _scale ??= new(this, "scale");

    /// <summary>
    /// As --scale, but for interpolating chroma information. If the image is not subsampled, this option is ignored entirely.
    /// </summary>
    private ScaleOptions? _scaleChroma;
    public ScaleOptions ScaleChroma => _scaleChroma ??= new(this, "cscale");

    /// <summary>
    /// Like --scale, but apply these filters on downscaling instead. If this option is unset, the filter implied by --scale will be applied.
    /// </summary>
    private ScaleOptions? _scaleDown;
    public ScaleOptions ScaleDown => _scaleDown ??= new(this, "dscale");

    /// <summary>
    /// The filter used for interpolating the temporal axis (frames). This is only used if --interpolation is enabled. The only valid choices for --tscale are separable convolution filters (use --tscale=help to get a list). The default is mitchell.
    /// Common --tscale choices include oversample, linear, catmull_rom, mitchell, gaussian, or bicubic.These are listed in increasing order of smoothness/blurriness, with bicubic being the smoothest/blurriest and oversample being the sharpest/least smooth.
    /// </summary>
    private ScaleOptions? _scaleTemporal;
    public ScaleOptions ScaleTemporal => _scaleTemporal ??= new(this, "tscale");

    /// <summary>
    /// Set the size of the lookup texture for scaler kernels (4-10, default: 6). The actual size of the texture is 2^N for an option value of N. So the lookup texture with the default setting uses 64 samples.
    /// All weights are linearly interpolated from those samples, so increasing the size of lookup table might improve the accuracy of scaler.
    /// </summary>
    private MpvOption<int>? _scalerLutSize;
    public MpvOption<int> ScalerLutSize => _scalerLutSize ??= new(this, "scaler-lut-size");

    /// <summary>
    /// Disable the scaler if the video image is not resized. In that case, bilinear is used instead of whatever is set with --scale. Bilinear will reproduce the source image perfectly if no scaling is performed. Enabled by default. Note that this option never affects --cscale.
    /// </summary>
    private MpvOption<bool>? _scalerResizesOnly;
    public MpvOption<bool> ScalerResizesOnly => _scalerResizesOnly ??= new(this, "scaler-resizes-only");

    /// <summary>
    /// When using convolution based filters, extend the filter size when downscaling. Increases quality, but reduces performance while downscaling.
    /// This will perform slightly sub-optimally for anamorphic video(but still better than without it) since it will extend the size to match only the milder of the scale factors between the axes.
    /// Note: this option is ignored when using bilinear downscaling(the default).
    /// </summary>
    private MpvOption<bool>? _correctDownscaling;
    public MpvOption<bool> CorrectDownscaling => _correctDownscaling ??= new(this, "correct-downscaling");

    /// <summary>
    /// Scale in linear light when downscaling. It should only be used with a --fbo-format that has at least 16 bit precision. This option has no effect on HDR content.
    /// </summary>
    private MpvOption<bool>? _linearDownscaling;
    public MpvOption<bool> LinearDownscaling => _linearDownscaling ??= new(this, "linear-downscaling");

    /// <summary>
    /// Scale in linear light when upscaling. Like --linear-downscaling, it should only be used with a --fbo-format that has at least 16 bits precisions. This is not usually recommended except for testing/specific purposes. Users are advised to either enable --sigmoid-upscaling or keep both options disabled (i.e. scaling in gamma light).
    /// </summary>
    private MpvOption<bool>? _linearUpscaling;
    public MpvOption<bool> LinearUpscaling => _linearUpscaling ??= new(this, "linear-upscaling");

    /// <summary>
    /// When upscaling, use a sigmoidal color transform to avoid emphasizing ringing artifacts. This is incompatible with and replaces --linear-upscaling. (Note that sigmoidization also requires linearization, so the LINEAR rendering step fires in both cases)
    /// </summary>
    private MpvOption<bool>? _sigmoidUpscaling;
    public MpvOption<bool> SigmoidUpscaling => _sigmoidUpscaling ??= new(this, "sigmoid-upscaling");

    /// <summary>
    /// The center of the sigmoid curve used for --sigmoid-upscaling, must be a float between 0.0 and 1.0. Defaults to 0.75 if not specified.
    /// </summary>
    private MpvOption<double>? _sigmoidCenter;
    public MpvOption<double> SigmoidCenter => _sigmoidCenter ??= new(this, "sigmoid-center");

    /// <summary>
    /// The slope of the sigmoid curve used for --sigmoid-upscaling, must be a float between 1.0 and 20.0. Defaults to 6.5 if not specified.
    /// </summary>
    private MpvOption<double>? _sigmoidSlope;
    public MpvOption<double> SigmoidSlope => _sigmoidSlope ??= new(this, "sigmoid-slope");

    /// <summary>
    /// Reduce stuttering caused by mismatches in the video fps and display refresh rate (also known as judder).
    /// This essentially attempts to interpolate the missing frames by convoluting the video along the temporal axis. The filter used can be controlled using the --tscale setting.
    /// </summary>
    private MpvOption<bool>? _interpolation;
    public MpvOption<bool> Interpolation => _interpolation ??= new(this, "interpolation");

    /// <summary>
    /// Threshold below which frame ratio interpolation gets disabled (default: 0.0001). This is calculated as abs(disphz/vfps - 1) &lt; threshold, where vfps is the speed-adjusted video FPS, and disphz the display refresh rate. (The speed-adjusted video FPS is roughly equal to the normal video FPS, but with slowdown and speedup applied. This matters if you use --video-sync=display-resample to make video run synchronously to the display FPS, or if you change the speed property.)
    /// The default is intended to almost always enable interpolation if the playback rate is even slightly different from the display refresh rate.But note that if you use e.g. --video-sync= display - vdrop, small deviations in the rate can disable interpolation and introduce a discontinuity every other minute.
    /// Set this to -1 to disable this logic.
    /// </summary>
    private MpvOption<double>? _interpolationThreshold;
    public MpvOption<double> InterpolationThreshold => _interpolationThreshold ??= new(this, "interpolation-threshold");

    /// <summary>
    /// Enable use of PBOs. On some drivers this can be faster, especially if the source video size is huge (e.g. so called "4K" video). On other drivers it might be slower or cause latency issues.
    /// </summary>
    private MpvOption<bool>? _openGlPbo;
    public MpvOption<bool> OpenGlPbo => _openGlPbo ??= new(this, "opengl-pbo");

    /// <summary>
    /// Set dither target depth to N. Default: no.
    /// no: Disable any dithering done by mpv.
    /// auto: Automatic selection. If output bit depth cannot be detected, 8 bits per component are assumed.
    /// 8: Dither to 8 bit output.
    /// Note that the depth of the connected video display device cannot be detected. Often, LCD panels will do dithering on their own, which conflicts with this option and leads to ugly output.
    /// </summary>
    private MpvOptionWithAutoNo<int>? _ditherDepth;
    public MpvOptionWithAutoNo<int> DitherDepth => _ditherDepth ??= new(this, "dither-depth");

    /// <summary>
    /// Set the size of the dither matrix (default: 6). The actual size of the matrix is (2^N) x (2^N) for an option value of N, so a value of 6 gives a size of 64x64. The matrix is generated at startup time, and a large matrix can take rather long to compute (seconds).
    /// Used in --dither=fruit mode only.
    /// </summary>
    private MpvOption<bool>? _ditherSizeFruit;
    public MpvOption<bool> DitherSizeFruit => _ditherSizeFruit ??= new(this, "dither-size-fruit");

    /// <summary>
    /// Select dithering algorithm (default: fruit). (Normally, the --dither-depth option controls whether dithering is enabled.)
    /// The error-diffusion option requires compute shader support.It also requires large amount of shared memory to run, the size of which depends on both the kernel (see --error-diffusion option below) and the height of video window.It will fallback to fruit dithering if there is no enough shared memory to run the shader.
    /// </summary>
    private MpvOptionEnum<DitherMode>? _dither;
    public MpvOptionEnum<DitherMode> Dither => _dither ??= new(this, "dither");

    /// <summary>
    /// Enable temporal dithering. (Only active if dithering is enabled in general.) This changes between 8 different dithering patterns on each frame by changing the orientation of the tiled dithering matrix. Unfortunately, this can lead to flicker on LCD displays, since these have a high reaction time.
    /// </summary>
    private MpvOption<bool>? _temporalDither;
    public MpvOption<bool> TemporalDither => _temporalDither ??= new(this, "temporal-dither");

    /// <summary>
    /// Determines how often the dithering pattern is updated when --temporal-dither is in use (1-128). 1 (the default) will update on every video frame, 2 on every other frame, etc.
    /// </summary>
    private MpvOption<int>? _temporalDitherPeriod;
    public MpvOption<int> TemporalDitherPeriod => _temporalDitherPeriod ??= new(this, "temporal-dither-period");

    /// <summary>
    /// The error diffusion kernel to use when --dither=error-diffusion is set.
    /// </summary>
    private MpvOptionString? _errorDiffusion;
    public MpvOptionString ErrorDiffusion => _errorDiffusion ??= new(this, "error-diffusion");

    /// <summary>
    /// Enables GPU debugging. What this means depends on the API type. For OpenGL, it calls glGetError(), and requests a debug context. For Vulkan, it enables validation layers.
    /// </summary>
    private MpvOption<bool>? _gpuDebug;
    public MpvOption<bool> GpuDebug => _gpuDebug ??= new(this, "gpu-debug");

    /// <summary>
    /// Interval in displayed frames between two buffer swaps. 1 is equivalent to enable VSYNC, 0 to disable VSYNC. Defaults to 1 if not specified.
    /// Note that this depends on proper OpenGL vsync support.On some platforms and drivers, this only works reliably when in fullscreen mode. It may also require driver-specific hacks if using multiple monitors, to ensure mpv syncs to the right one.Compositing window managers can also lead to bad results, as can missing or incorrect display FPS information (see --override-display-fps).
    /// </summary>
    private MpvOption<int>? _openGlSwapInterval;
    public MpvOption<int> OpenGlSwapInterval => _openGlSwapInterval ??= new(this, "opengl-swapinterval");

    /// <summary>
    /// Controls the presentation mode of the vulkan swapchain. This is similar to the --opengl-swapinterval option.
    /// </summary>
    private MpvOptionEnum<SwapMode>? _vulkanSwapMode;
    public MpvOptionEnum<SwapMode> VulkanSwapMode => _vulkanSwapMode ??= new(this, "vulkan-swap-mode");

    /// <summary>
    /// Controls the number of VkQueues used for rendering (limited by how many your device supports). In theory, using more queues could enable some parallelism between frames (when using a --swapchain-depth higher than 1), but it can also slow things down on hardware where there's no true parallelism between queues. (Default: 1)
    /// </summary>
    private MpvOption<int>? _vulkanQueueCount;
    public MpvOption<int> VulkanQueueCount => _vulkanQueueCount ??= new(this, "vulkan-queue-count");

    /// <summary>
    /// Enables the use of async transfer queues on supported vulkan devices. Using them allows transfer operations like texture uploads and blits to happen concurrently with the actual rendering, thus improving overall throughput and power consumption. Enabled by default, and should be relatively safe.
    /// </summary>
    private MpvOption<bool>? _vulkanAsyncTransfer;
    public MpvOption<bool> VulkanAsyncTransfer => _vulkanAsyncTransfer ??= new(this, "vulkan-async-transfer");

    /// <summary>
    /// Enables the use of async compute queues on supported vulkan devices. Using this, in theory, allows out-of-order scheduling of compute shaders with graphics shaders, thus enabling the hardware to do more effective work while waiting for pipeline bubbles and memory operations. Not beneficial on all GPUs. It's worth noting that if async compute is enabled, and the device supports more compute queues than graphics queues (bound by the restrictions set by --vulkan-queue-count), mpv will internally try and prefer the use of compute shaders over fragment shaders wherever possible. Enabled by default, although Nvidia users may want to disable it.
    /// </summary>
    private MpvOption<bool>? _vulkanAsyncCompute;
    public MpvOption<bool> VulkanAsyncCompute => _vulkanAsyncCompute ??= new(this, "vulkan-async-compute");

    /// <summary>
    /// Disable the use of VkEvents, for debugging purposes or for compatibility with some older drivers / vulkan portability layers that don't provide working VkEvent support.
    /// </summary>
    private MpvOption<bool>? _vulkanDisableEvents;
    public MpvOption<bool> VulkanDisableEvents => _vulkanDisableEvents ??= new(this, "vulkan-disable-events");

    /// <summary>
    /// Switches the D3D11 swap chain fullscreen state to 'fullscreen' when fullscreen video is requested. Also known as "exclusive fullscreen" or "D3D fullscreen" in other applications. Gives mpv full control of rendering on the swap chain's screen. Off by default.
    /// </summary>
    private MpvOption<bool>? _d3D11ExclusiveFullscreen;
    public MpvOption<bool> D3D11ExclusiveFullscreen => _d3D11ExclusiveFullscreen ??= new(this, "d3d11-exclusive-fs");

    /// <summary>
    /// Use WARP (Windows Advanced Rasterization Platform) with the D3D11 GPU backend (default: auto). This is a high performance software renderer. By default, it is only used when the system has no hardware adapters that support D3D11. While the extended GPU features will work with WARP, they can be very slow.
    /// </summary>
    private MpvOptionWithAuto<bool>? _d3D11Warp;
    public MpvOptionWithAuto<bool> D3D11Warp => _d3D11Warp ??= new(this, "d3d11-warp");

    /// <summary>
    /// Select a specific feature level when using the D3D11 GPU backend (12_1|12_0|11_1|11_0|10_1|10_0|9_3|9_2|9_1). By default, the highest available feature level is used. This option can be used to select a lower feature level, which is mainly useful for debugging. Most extended GPU features will not work at 9_x feature levels.
    /// </summary>
    private MpvOptionString? _d3D11FeatureLevel;
    public MpvOptionString D3D11FeatureLevel => _d3D11FeatureLevel ??= new(this, "d3d11-feature-level");

    /// <summary>
    /// Enable flip-model presentation, which avoids unnecessarily copying the backbuffer by sharing surfaces with the DWM (default: yes). This may cause performance issues with older drivers. If flip-model presentation is not supported (for example, on Windows 7 without the platform update), mpv will automatically fall back to the older bitblt presentation model.
    /// </summary>
    private MpvOption<bool>? _d3D11Flip;
    public MpvOption<bool> D3D11Flip => _d3D11Flip ??= new(this, "d3d11-flip");

    /// <summary>
    /// Schedule each frame to be presented for this number of VBlank intervals. (default: 1) Setting to 1 will enable VSync, setting to 0 will disable it.
    /// </summary>
    private MpvOption<int>? _d3D11SyncInterval;
    public MpvOption<int> D3D11SyncInterval => _d3D11SyncInterval ??= new(this, "d3d11-sync-interval");

    /// <summary>
    /// Select a specific D3D11 adapter to utilize for D3D11 rendering. Will pick the default adapter if unset. Alternatives are listed when the name "help" is given.
    /// Checks for matches based on the start of the string, case insensitive.Thus, if the description of the adapter starts with the vendor name, that can be utilized as the selection parameter.
    /// Hardware decoders utilizing the D3D11 rendering abstraction's helper functionality to receive a device, such as D3D11VA or DXVA2's DXGI mode, will be affected by this choice.
    /// </summary>
    private MpvOptionString? _d3D11Adapter;
    public MpvOptionString D3D11Adapter => _d3D11Adapter ??= new(this, "d3d11-adapter");

    /// <summary>
    /// Select a specific D3D11 output format to utilize for D3D11 rendering. "auto" is the default, which will pick either rgba8 or rgb10_a2 depending on the configured desktop bit depth. rgba16f and bgra8 are left out of the autodetection logic, and are available for manual testing.
    /// </summary>
    private MpvOptionEnum<D3DOutputFormat>? _d3DOutputFormat = null;
    public MpvOptionEnum<D3DOutputFormat> D3D11OutputFormat => _d3DOutputFormat ??= new(this, "d3d11-output-format");

    /// <summary>
    /// Select a specific D3D11 output color space to utilize for D3D11 rendering. "auto" is the default, which will select the color space of the desktop on which the swap chain is located.
    /// Values other than "srgb" and "pq" have had issues in testing, so they are mostly available for manual testing.
    /// </summary>
    private MpvOptionEnum<D3DOutputColorSpace>? _d3DOutputColorSpace = null;
    public MpvOptionEnum<D3DOutputColorSpace> D3D11OutputColorSpace => _d3DOutputColorSpace ??= new(this, "d3d11-output-csp");

    /// <summary>
    /// By default, when using hardware decoding with --gpu-api=d3d11, the video image will be copied (GPU-to-GPU) from the decoder surface to a shader resource. Set this option to avoid that copy by sampling directly from the decoder image. This may increase performance and reduce power usage, but can cause the image to be sampled incorrectly on the bottom and right edges due to padding, and may invoke driver bugs, since Direct3D 11 technically does not allow sampling from a decoder surface (though most drivers support it.)
    /// Currently only relevant for --gpu-api=d3d11.
    /// </summary>
    private MpvOption<bool>? _d3D11ZeroCopy;
    public MpvOption<bool> D3D11ZeroCopy => _d3D11ZeroCopy ??= new(this, "d3d11va-zero-copy");

    /// <summary>
    /// Set the client app id for Wayland-based video output methods. By default, "mpv" is used.
    /// </summary>
    private MpvOptionString? _waylandAppId;
    public MpvOptionString WaylandAppId => _waylandAppId ??= new(this, "wayland-app-id");

    /// <summary>
    /// Disable vsync for the wayland contexts (default: no). Useful for benchmarking the wayland context when combined with video-sync=display-desync, --no-audio, and --untimed=yes. Only works with --gpu-context=wayland and --gpu-context=waylandvk.
    /// </summary>
    private MpvOption<bool>? _waylandDisableVsync;
    public MpvOption<bool> WaylandDisableVsync => _waylandDisableVsync ??= new(this, "wayland-disable-vsync");

    /// <summary>
    /// Defines the size of an edge border (default: 10) to initiate client side resize events in the wayland contexts with the mouse. This is only active if there are no server side decorations from the compositor.
    /// </summary>
    private MpvOption<int>? _waylandEdgePixelsPointer;
    public MpvOption<int> WaylandEdgePixelsPointer => _waylandEdgePixelsPointer ??= new(this, "wayland-edge-pixels-pointer");

    /// <summary>
    /// Defines the size of an edge border (default: 64) to initiate client side resizes events in the wayland contexts with touch events.
    /// </summary>
    private MpvOption<int>? _waylandEdgePixelsTouch;
    public MpvOption<int> WaylandEdgePixelsTouch => _waylandEdgePixelsTouch ??= new(this, "wayland-edge-pixels-touch");

    /// <summary>
    /// Custom GLSL hooks. These are a flexible way to add custom fragment shaders, which can be injected at almost arbitrary points in the rendering pipeline, and access all previous intermediate textures.
    /// </summary>
    private MpvOptionList? _glslShaders;
    public MpvOptionList GlslShaders => _glslShaders ??= new(this, "glsl-shaders");

    /// <summary>
    /// Enable the debanding algorithm. This greatly reduces the amount of visible banding, blocking and other quantization artifacts, at the expense of very slightly blurring some of the finest details. In practice, it's virtually always an improvement - the only reason to disable it would be for performance.
    /// </summary>
    private MpvOption<bool>? _deband;
    public MpvOption<bool> Deband => _deband ??= new(this, "deband");

    /// <summary>
    /// The number of debanding steps to perform per sample (1-16). Each step reduces a bit more banding, but takes time to compute. Note that the strength of each step falls off very quickly, so high numbers (>4) are practically useless. (Default 1)
    /// </summary>
    private MpvOption<int>? _debandIterations;
    public MpvOption<int> DebandIterations => _debandIterations ??= new(this, "deband-iterations");

    /// <summary>
    /// The debanding filter's cut-off threshold (0-4096). Higher numbers increase the debanding strength dramatically but progressively diminish image details. (Default 64)
    /// </summary>
    private MpvOption<int>? _debandThreshold;
    public MpvOption<int> DebandThreshold => _debandThreshold ??= new(this, "deband-threshold");

    /// <summary>
    /// The debanding filter's initial radius (1-64). The radius increases linearly for each iteration. A higher radius will find more gradients, but a lower radius will smooth more aggressively. (Default 16)
    /// If you increase the --deband-iterations, you should probably decrease this to compensate.
    /// </summary>
    private MpvOption<int>? _debandRange;
    public MpvOption<int> DebandRange => _debandRange ??= new(this, "deband-range");

    /// <summary>
    /// Add some extra noise to the image (0-4096). This significantly helps cover up remaining quantization artifacts. Higher numbers add more noise. (Default 48)
    /// </summary>
    private MpvOption<int>? _debandGrain;
    public MpvOption<int> DebandGrain => _debandGrain ??= new(this, "deband-grain");

    /// <summary>
    /// If set to a value other than 0, enable an unsharp masking filter. Positive values will sharpen the image (but add more ringing and aliasing). Negative values will blur the image. If your GPU is powerful enough, consider alternatives like the ewa_lanczossharp scale filter, or the --scale-blur option.
    /// </summary>
    private MpvOption<double>? _sharpen;
    public MpvOption<double> Sharpen => _sharpen ??= new(this, "sharpen");

    /// <summary>
    /// Call glFinish() before swapping buffers (default: disabled). Slower, but might improve results when doing framedropping. Can completely ruin performance. The details depend entirely on the OpenGL driver.
    /// </summary>
    private MpvOption<bool>? _openGlFinish;
    public MpvOption<bool> OpenGlFinish => _openGlFinish ??= new(this, "opengl-glfinish");

    /// <summary>
    /// Call glXWaitVideoSyncSGI after each buffer swap (default: disabled). This may or may not help with video timing accuracy and frame drop. It's possible that this makes video output slower, or has no effect at all.
    /// X11/GLX only.
    /// </summary>
    private MpvOption<bool>? _openGlWaitVsync;
    public MpvOption<bool> OpenGlWaitVsync => _openGlWaitVsync ??= new(this, "opengl-waitvsync");

    /// <summary>
    /// Calls DwmFlush after swapping buffers on Windows (default: auto). It also sets SwapInterval(0) to ignore the OpenGL timing. Values are: no (disabled), windowed (only in windowed mode), yes (also in full screen).
    /// </summary>
    private MpvOptionEnum<OpenGlFlushMode>? _openGlDwmFlush;
    public MpvOptionEnum<OpenGlFlushMode> OpenGlDwmFlush => _openGlDwmFlush ??= new(this, "opengl-dwmflush");

    /// <summary>
    /// Selects a specific feature level when using the ANGLE backend with D3D11. By default, the highest available feature level is used. This option can be used to select a lower feature level, which is mainly useful for debugging. Note that OpenGL ES 3.0 is only supported at feature level 10_1 or higher. Most extended OpenGL features will not work at lower feature levels (similar to --gpu-dumb-mode).
    /// </summary>
    private MpvOptionString? _angleD3D11FeatureLevel;
    public MpvOptionString AngleD3D11FeatureLevel => _angleD3D11FeatureLevel ??= new(this, "angle-d3d11-feature-level");

    /// <summary>
    /// Use WARP (Windows Advanced Rasterization Platform) when using the ANGLE backend with D3D11 (default: auto). This is a high performance software renderer. By default, it is used when the Direct3D hardware does not support Direct3D 11 feature level 9_3. While the extended OpenGL features will work with WARP, they can be very slow.
    /// </summary>
    private MpvOptionWithAuto<bool>? _angleD3D11Warp;
    public MpvOptionWithAuto<bool> AngleD3D11Warp => _angleD3D11Warp ??= new(this, "angle-d3d11-warp");

    /// <summary>
    /// Use ANGLE's built in EGL windowing functions to create a swap chain (default: auto). If this is set to no and the D3D11 renderer is in use, ANGLE's built in swap chain will not be used and a custom swap chain that is optimized for video rendering will be created instead. If set to auto, a custom swap chain will be used for D3D11 and the built in swap chain will be used for D3D9. This option is mainly for debugging purposes, in case the custom swap chain has poor performance or does not work.
    /// If set to yes, the --angle-max-frame-latency, --angle-swapchain-length and --angle-flip options will have no effect.
    /// </summary>
    private MpvOptionWithAuto<bool>? _angleEglWindowing;
    public MpvOptionWithAuto<bool> AngleEglWindowing => _angleEglWindowing ??= new(this, "angle-egl-windowing");

    /// <summary>
    /// Enable flip-model presentation, which avoids unnecessarily copying the backbuffer by sharing surfaces with the DWM (default: yes). This may cause performance issues with older drivers. If flip-model presentation is not supported (for example, on Windows 7 without the platform update), mpv will automatically fall back to the older bitblt presentation model.
    /// If set to no, the --angle-swapchain-length option will have no effect.
    /// </summary>
    private MpvOption<bool>? _angleFlip;
    public MpvOption<bool> AngleFlip => _angleFlip ??= new(this, "angle-flip");

    /// <summary>
    /// Forces a specific renderer when using the ANGLE backend (default: auto). In auto mode this will pick D3D11 for systems that support Direct3D 11 feature level 9_3 or higher, and D3D9 otherwise. This option is mainly for debugging purposes. Normally there is no reason to force a specific renderer, though --angle-renderer=d3d9 may give slightly better performance on old hardware. Note that the D3D9 renderer only supports OpenGL ES 2.0, so most extended OpenGL features will not work if this renderer is selected (similar to --gpu-dumb-mode).
    /// </summary>
    private MpvOptionEnum<AngleRenderer>? _angleRenderer;
    public MpvOptionEnum<AngleRenderer> AngleRenderer => _angleRenderer ??= new(this, "angle-renderer");

    /// <summary>
    /// Deactivates the automatic graphics switching and forces the dedicated GPU. (default: no)
    /// OS X only.
    /// </summary>
    private MpvOption<bool>? _macOsForceDedicatedGpu;
    public MpvOption<bool> MacOsForceDedicatedGpu => _macOsForceDedicatedGpu ??= new(this, "macos-force-dedicated-gpu");

    /// <summary>
    /// Use the Apple Software Renderer when using cocoa-cb (default: auto). If set to no the software renderer is never used and instead fails when a the usual pixel format could not be created, yes will always only use the software renderer, and auto only falls back to the software renderer when the usual pixel format couldn't be created.
    /// OS X only.
    /// </summary>
    private MpvOptionWithAuto<bool>? _cocoaCbSoftwareRenderer;
    public MpvOptionWithAuto<bool> CocoaCbSoftwareRenderer => _cocoaCbSoftwareRenderer ??= new(this, "cocoa-cb-sw-renderer");

    /// <summary>
    /// Creates a 10bit capable pixel format for the context creation (default: yes). Instead of 8bit integer framebuffer a 16bit half-float framebuffer is requested.
    /// OS X only.
    /// </summary>
    private MpvOption<bool>? _cocoaCb10BitContext;
    public MpvOption<bool> CocoaCb10BitContext => _cocoaCb10BitContext ??= new(this, "cocoa-cb-10bit-context");

    /// <summary>
    /// Sets the appearance of the title bar (default: auto). Not all combinations of appearances and --macos-title-bar-material materials make sense or are unique. Appearances that are not supported by you current macOS version fall back to the default value. macOS and cocoa-cb only
    /// </summary>
    private MpvOptionString? _macOsTitleBarAppearance;
    public MpvOptionString MacOsTitleBarAppearance => _macOsTitleBarAppearance ??= new(this, "macos-title-bar-appearance");

    /// <summary>
    /// Sets the material of the title bar (default: titlebar). All deprecated materials should not be used on macOS 10.14+ because their functionality is not guaranteed. Not all combinations of materials and --macos-title-bar-appearance appearances make sense or are unique. Materials that are not supported by you current macOS version fall back to the default value. macOS and cocoa-cb only
    /// </summary>
    private MpvOptionString? _macOsTitleBarMaterial;
    public MpvOptionString MacOsTitleBarMaterial => _macOsTitleBarMaterial ??= new(this, "macos-title-bar-material");

    /// <summary>
    /// Sets the color of the title bar (default: completely transparent). Is influenced by --macos-title-bar-appearance and --macos-title-bar-material. See --sub-color for color syntax.
    /// </summary>
    private MpvOptionString? _macOsTitleBarColor;
    public MpvOptionString MacOsTitleBarColor => _macOsTitleBarColor ??= new(this, "macos-title-bar-color");

    /// <summary>
    /// Sets the fullscreen resize animation duration in ms (0-1000, default: default). The default value is slightly less than the system's animation duration (500ms) to prevent some problems when the end of an async animation happens at the same time as the end of the system wide fullscreen animation. Setting anything higher than 500ms will only prematurely cancel the resize animation after the system wide animation ended. The upper limit is still set at 1000ms since it's possible that Apple or the user changes the system defaults. Anything higher than 1000ms though seems too long and shouldn't be set anyway. OS X and cocoa-cb only
    /// </summary>
    private MpvOptionWithDefault<int>? _macOsFullscreenAnimationDuration;
    public MpvOptionWithDefault<int> MacOsFullscreenAnimationDuration => _macOsFullscreenAnimationDuration ??= new(this, "macos-fs-animation-duration");

    /// <summary>
    /// Changes the App activation policy. With accessory the mpv icon in the Dock can be hidden. (default: regular)
    /// macOS only.
    /// </summary>
    private MpvOptionEnum<AppActivationPolicy>? _macOsAppActivationPolicy;
    public MpvOptionEnum<AppActivationPolicy> MacOsAppActivationPolicy => _macOsAppActivationPolicy ??= new(this, "macos-app-activation-policy");

    /// <summary>
    /// Set dimensions of the rendering surface used by the Android gpu context. Needs to be set by the embedding application if the dimensions change during runtime (i.e. if the device is rotated), via the surfaceChanged callback.
    /// </summary>
    private MpvOptionString? _androidSurfaceSize;
    public MpvOptionString AndroidSurfaceSize => _androidSurfaceSize ??= new(this, "android-surface-size");

    /// <summary>
    /// Continue even if a software renderer is detected.
    /// </summary>
    private MpvOption<bool>? _gpuSoftwareRenderer;
    public MpvOption<bool> GpuSoftwareRenderer => _gpuSoftwareRenderer ??= new(this, "gpu-sw");

    /// <summary>
    /// The value auto (the default) selects the GPU context. You can also pass help to get a complete list of compiled in backends (sorted by autoprobe order).
    /// </summary>
    private MpvOptionString? _gpuContext;
    public MpvOptionString GpuContext => _gpuContext ??= new(this, "gpu-context");

    /// <summary>
    /// Controls which type of graphics APIs will be accepted:
    /// </summary>
    private MpvOptionEnum<GpuApiMode>? _gpuApi;
    public MpvOptionEnum<GpuApiMode> GpuApi => _gpuApi ??= new(this, "gpu-api");

    /// <summary>
    /// Controls which type of OpenGL context will be accepted:
    /// auto: Allow all types of OpenGL(default).
    /// yes: Only allow GLES.
    /// no: Only allow desktop/core GL.
    /// </summary>
    private MpvOptionWithAuto<bool>? _openGlEs;
    public MpvOptionWithAuto<bool> OpenGlEs => _openGlEs ??= new(this, "opengl-es");

    /// <summary>
    /// Restricts all OpenGL versions above a certain version. Versions are encoded in hundreds, i.e. OpenGL 4.5 -> 450. As an example, --opengl-restrict=300 would restrict OpenGL 3.0 and higher, effectively only allowing 2.x contexts. Note that this only imposes a limit on context creation APIs, the actual OpenGL context may still have a higher OpenGL version. (Default: 0)
    /// </summary>
    private MpvOption<int>? _openGlRestrict;
    public MpvOption<int> OpenGlRestrict => _openGlRestrict ??= new(this, "opengl-restrict");

    /// <summary>
    /// Selects the internal format of textures used for FBOs. The format can influence performance and quality of the video output. fmt can be one of: rgb8, rgb10, rgb10_a2, rgb16, rgb16f, rgb32f, rgba12, rgba16, rgba16f, rgba16hf, rgba32f.
    /// Default: auto, which first attempts to utilize 16bit float (rgba16f, rgba16hf), and falls back to rgba16 if those are not available.Finally, attempts to utilize rgb10_a2 or rgba8 if all of the previous formats are not available.
    /// </summary>
    private MpvOptionString? _fboFormat;
    public MpvOptionString FboFormat => _fboFormat ??= new(this, "fbo-format");

    /// <summary>
    /// Set an additional raw gamma factor (default: 1.0). If gamma is adjusted in other ways (like with the --gamma option or key bindings and the gamma property), the value is multiplied with the other gamma value.
    /// </summary>
    private MpvOption<double>? _gammaFactor;
    public MpvOption<double> GammaFactor => _gammaFactor ??= new(this, "gamma-factor");

    /// <summary>
    /// Automatically corrects the gamma value depending on ambient lighting conditions (adding a gamma boost for bright rooms).
    /// With ambient illuminance of 16 lux, mpv will pick the 1.0 gamma value(no boost), and slightly increase the boost up until 1.2 for 256 lux.
    /// NOTE: Only implemented on OS X.
    /// </summary>
    private MpvOption<bool>? _gammaAuto;
    public MpvOption<bool> GammaAuto => _gammaAuto ??= new(this, "gamma-auto");

    /// <summary>
    /// Specifies the primaries of the display. Video colors will be adapted to this colorspace when ICC color management is not being used.
    /// </summary>
    private MpvOptionString? _targetPrim;
    public MpvOptionString TargetPrim => _targetPrim ??= new(this, "target-prim");

    /// <summary>
    /// Specifies the transfer characteristics (gamma) of the display. Video colors will be adjusted to this curve when ICC color management is not being used.
    /// </summary>
    private MpvOptionString? _targetTrc;
    public MpvOptionString TargetTrc => _targetTrc ??= new(this, "target-trc");

    /// <summary>
    /// Specifies the measured peak brightness of the output display, in cd/m^2 (AKA nits). The interpretation of this brightness depends on the configured --target-trc. In all cases, it imposes a limit on the signal values that will be sent to the display. If the source exceeds this brightness level, a tone mapping filter will be inserted. For HLG, it has the additional effect of parametrizing the inverse OOTF, in order to get colorimetrically consistent results with the mastering display. For SDR, or when using an ICC (profile (--icc-profile), setting this to a value above 203 essentially causes the display to be treated as if it were an HDR display in disguise. (See the note below)
    /// In auto mode(the default), the chosen peak is an appropriate value based on the TRC in use.For SDR curves, it uses 203. For HDR curves, it uses 203 * the transfer function's nominal peak.
    /// </summary>
    private MpvOptionWithAuto<int>? _targetPeak;
    public MpvOptionWithAuto<int> TargetPeak => _targetPeak ??= new(this, "target-peak");

    /// <summary>
    /// Specifies the algorithm used for tone-mapping images onto the target display. This is relevant for both HDR->SDR conversion as well as gamut reduction (e.g. playing back BT.2020 content on a standard gamut display).
    /// </summary>
    private MpvOptionEnum<ToneMappingMode>? _toneMapping;
    public MpvOptionEnum<ToneMappingMode> ToneMapping => _toneMapping ??= new(this, "tone-mapping");

    /// <summary>
    /// Set tone mapping parameters. By default, this is set to the special string default, which maps to an algorithm-specific default value. Ignored if the tone mapping algorithm is not tunable. This affects the following tone mapping algorithms:
    /// </summary>
    private MpvOptionWithDefault<double>? _toneMappingParam;
    public MpvOptionWithDefault<double> ToneMappingParam => _toneMappingParam ??= new(this, "tone-mapping-param");

    /// <summary>
    /// Upper limit for how much the tone mapping algorithm is allowed to boost the average brightness by over-exposing the image (1.0 - 10.0). The default value of 1.0 allows no additional brightness boost. A value of 2.0 would allow over-exposing by a factor of 2, and so on. Raising this setting can help reveal details that would otherwise be hidden in dark scenes, but raising it too high will make dark scenes appear unnaturally bright.
    /// </summary>
    private MpvOption<double>? _toneMappingMaxBoost;
    public MpvOption<double> ToneMappingMaxBoost => _toneMappingMaxBoost ??= new(this, "tone-mapping-max-boost");

    /// <summary>
    /// Compute the HDR peak and frame average brightness per-frame instead of relying on tagged metadata. These values are averaged over local regions as well as over several frames to prevent the value from jittering around too much. This option basically gives you dynamic, per-scene tone mapping. Requires compute shaders, which is a fairly recent OpenGL feature, and will probably also perform horribly on some drivers, so enable at your own risk. The special value auto (default) will enable HDR peak computation automatically if compute shaders and SSBOs are supported.
    /// </summary>
    private MpvOptionWithAuto<bool>? _hdrComputePeak;
    public MpvOptionWithAuto<bool> HdrComputePeak => _hdrComputePeak ??= new(this, "hdr-compute-peak");

    /// <summary>
    /// The decay rate used for the HDR peak detection algorithm (1.0 - 1000.0, default: 100.0). This is only relevant when --hdr-compute-peak is enabled. Higher values make the peak decay more slowly, leading to more stable values at the cost of more "eye adaptation"-like effects (although this is mitigated somewhat by --hdr-scene-threshold). A value of 1.0 (the lowest possible) disables all averaging, meaning each frame's value is used directly as measured, but doing this is not recommended for "noisy" sources since it may lead to excessive flicker. (In signal theory terms, this controls the time constant "tau" of an IIR low pass filter)
    /// </summary>
    private MpvOption<double>? _hdrPeakDecayRate;
    public MpvOption<double> HdrPeakDecayRate => _hdrPeakDecayRate ??= new(this, "hdr-peak-decay-rate");

    /// <summary>
    /// The lower thresholds (in dB) for a brightness difference to be considered a scene change (default: 5.5 low). This is only relevant when --hdr-compute-peak is enabled. Normally, small fluctuations in the frame brightness are compensated for by the peak averaging mechanism, but for large jumps in the brightness this can result in the frame remaining too bright or too dark for up to several seconds, depending on the value of --hdr-peak-decay-rate. To counteract this, when the brightness between the running average and the current frame exceeds the low threshold, mpv will make the averaging filter more aggressive, up to the limit of the high threshold (at which point the filter becomes instant).
    /// </summary>
    private MpvOption<double>? _hdrSceneThresholdLow;
    public MpvOption<double> HdrSceneThresholdLow => _hdrSceneThresholdLow ??= new(this, "hdr-scene-threshold-low");

    /// <summary>
    /// The upper thresholds (in dB) for a brightness difference to be considered a scene change (default: 10.0 high). This is only relevant when --hdr-compute-peak is enabled. Normally, small fluctuations in the frame brightness are compensated for by the peak averaging mechanism, but for large jumps in the brightness this can result in the frame remaining too bright or too dark for up to several seconds, depending on the value of --hdr-peak-decay-rate. To counteract this, when the brightness between the running average and the current frame exceeds the low threshold, mpv will make the averaging filter more aggressive, up to the limit of the high threshold (at which point the filter becomes instant).
    /// </summary>
    private MpvOption<double>? _hdrSceneThresholdHigh;
    public MpvOption<double> HdrSceneThresholdHigh => _hdrSceneThresholdHigh ??= new(this, "hdr-scene-threshold-high");

    /// <summary>
    /// Apply desaturation for highlights (0-1, default: 0.75). The parameter controls the strength of the desaturation curve. A value of 0.0 completely disables it, while a value of 1.0 means that overly bright colors will tend towards white. (This is not always the case, especially not for highlights that are near primary colors)
    /// Values in between apply progressively more/less aggressive desaturation.This setting helps prevent unnaturally oversaturated colors for super-highlights, by(smoothly) turning them into less saturated(per channel tone mapped) colors instead.This makes images feel more natural, at the cost of chromatic distortions for out-of-range colors. The default value of 0.75 provides a good balance. Setting this to 0.0 preserves the chromatic accuracy of the tone mapping process.
    /// </summary>
    private MpvOption<double>? _toneMappingDesaturate;
    public MpvOption<double> ToneMappingDesaturate => _toneMappingDesaturate ??= new(this, "tone-mapping-desaturate");

    /// <summary>
    /// This setting controls the exponent of the desaturation curve, which controls how bright a color needs to be in order to start being desaturated (0-20). The default of 1.5 provides a reasonable balance. Decreasing this exponent makes the curve more aggressive.
    /// </summary>
    private MpvOption<double>? _toneMappingDesaturateExponent;
    public MpvOption<double> ToneMappingDesaturateExponent => _toneMappingDesaturateExponent ??= new(this, "tone-mapping-desaturate-exponent");

    /// <summary>
    /// If enabled, mpv will mark all clipped/out-of-gamut pixels that exceed a given threshold (currently hard-coded to 101%). The affected pixels will be inverted to make them stand out. Note: This option applies after the effects of all of mpv's color space transformation / tone mapping options, so it's a good idea to combine this with --tone-mapping=clip and use --target-prim to set the gamut to simulate. For example, --target-prim=bt.709 would make mpv highlight all pixels that exceed the gamut of a standard gamut (sRGB) display. This option also does not work well with ICC profiles, since the 3DLUTs are always generated against the source color space and have chromatically-accurate clipping built in.
    /// </summary>
    private MpvOption<bool>? _gamutWarning;
    public MpvOption<bool> GamutWarning => _gamutWarning ??= new(this, "gamut-warning");

    /// <summary>
    /// If enabled (default: yes), mpv will colorimetrically clip out-of-gamut colors by desaturating them (preserving luma), rather than hard-clipping each component individually. This should make playback of wide gamut content on typical (standard gamut) monitors look much more aesthetically pleasing and less blown-out.
    /// </summary>
    private MpvOption<bool>? _gamutClipping;
    public MpvOption<bool> GamutClipping => _gamutClipping ??= new(this, "gamut-clipping");

    /// <summary>
    /// Load the embedded ICC profile contained in media files such as PNG images. (Default: yes). Note that this option only works when also using a display ICC profile (--icc-profile or --icc-profile-auto), and also requires LittleCMS 2 support.
    /// </summary>
    private MpvOption<bool>? _usdEmbeddedIccProfile;
    public MpvOption<bool> UsdEmbeddedIccProfile => _usdEmbeddedIccProfile ??= new(this, "use-embedded-icc-profile");

    /// <summary>
    /// Load an ICC profile and use it to transform video RGB to screen output. Needs LittleCMS 2 support compiled in. This option overrides the --target-prim, --target-trc and --icc-profile-auto options.
    /// </summary>
    private MpvOptionString? _iccProfile;
    public MpvOptionString IccProfile => _iccProfile ??= new(this, "icc-profile");

    /// <summary>
    /// Automatically select the ICC display profile currently specified by the display settings of the operating system.
    /// NOTE: On Windows, the default profile must be an ICC profile.WCS profiles are not supported.
    /// </summary>
    private MpvOption<bool>? _iccProfileAuto;
    public MpvOption<bool> IccProfileAuto => _iccProfileAuto ??= new(this, "icc-profile-auto");

    /// <summary>
    /// Store and load the 3D LUTs created from the ICC profile in this directory. This can be used to speed up loading, since LittleCMS 2 can take a while to create a 3D LUT. Note that these files contain uncompressed LUTs. Their size depends on the --icc-3dlut-size, and can be very big.
    /// NOTE: This is not cleaned automatically, so old, unused cache files may stick around indefinitely.
    /// </summary>
    private MpvOptionString? _iccCacheDir;
    public MpvOptionString IccCacheDir => _iccCacheDir ??= new(this, "icc-cache-dir");

    /// <summary>
    /// Specifies the ICC intent used for the color transformation (when using --icc-profile).
    /// 0: Perceptual.
    /// 1: Relative colorimetric(default).
    /// 2: Saturation.
    /// 3: Absolute colorimetric.
    /// </summary>
    private MpvOption<int>? _iccIntent;
    public MpvOption<int> IccIntent => _iccIntent ??= new(this, "icc-intent");

    /// <summary>
    /// "{r}x{g}x{b}" Size of the 3D LUT generated from the ICC profile in each dimension. Default is 64x64x64. Sizes may range from 2 to 512.
    /// </summary>
    private MpvOptionString? _icc3DLutSize;
    public MpvOptionString Icc3DLutSize => _icc3DLutSize ??= new(this, "icc-3dlut-size");

    /// <summary>
    /// Specifies an upper limit on the target device's contrast ratio. This is detected automatically from the profile if possible, but for some profiles it might be missing, causing the contrast to be assumed as infinite. As a result, video may appear darker than intended. This only affects BT.1886 content. The default of 0 means no limit if the detected contrast is less than 100000, and limits to 1000 otherwise. Use --icc-contrast=inf to preserve the infinite contrast (most likely when using OLED displays).
    /// </summary>
    private MpvOptionWithInf<int>? _iccContrast;
    public MpvOptionWithInf<int> IccContrast => _iccContrast ??= new(this, "icc-contrast");

    /// <summary>
    /// Blend subtitles directly onto upscaled video frames, before interpolation and/or color management (default: no). Enabling this causes subtitles to be affected by --icc-profile, --target-prim, --target-trc, --interpolation, --gamma-factor and --glsl-shaders. It also increases subtitle performance when using --interpolation.
    /// The downside of enabling this is that it restricts subtitles to the visible portion of the video, so you can't have subtitles exist in the black margins below a video (for example).
    /// </summary>
    private MpvOptionEnum<BlendSubtitlesMode>? _blendSubtitles;
    public MpvOptionEnum<BlendSubtitlesMode> BlendSubtitles => _blendSubtitles ??= new(this, "blend-subtitles");

    /// <summary>
    /// Decides what to do if the input has an alpha component.
    /// </summary>
    private MpvOptionEnum<AlphaMode>? _alpha;
    public MpvOptionEnum<AlphaMode> Alpha => _alpha ??= new(this, "alpha");

    /// <summary>
    /// Force use of rectangle textures (default: no). Normally this shouldn't have any advantages over normal textures. Note that hardware decoding overrides this flag. Could be removed any time.
    /// </summary>
    private MpvOption<bool>? _openGlRectangleTextures;
    public MpvOption<bool> OpenGlRectangleTextures => _openGlRectangleTextures ??= new(this, "opengl-rectangle-textures");

    /// <summary>
    /// Color used to draw parts of the mpv window not covered by video. See the --sub-color option for how colors are defined.
    /// </summary>
    private MpvOptionString? _background;
    public MpvOptionString Background => _background ??= new(this, "background");

    /// <summary>
    /// Enlarge the video source textures by this many pixels. For debugging only (normally textures are sized exactly, but due to hardware decoding interop we may have to deal with additional padding, which can be tested with these options). Could be removed any time.
    /// </summary>
    private MpvOption<int>? _gpuTextPadX;
    public MpvOption<int> GpuTextPadX => _gpuTextPadX ??= new(this, "gpu-tex-pad-x");

    /// <summary>
    /// Enlarge the video source textures by this many pixels. For debugging only (normally textures are sized exactly, but due to hardware decoding interop we may have to deal with additional padding, which can be tested with these options). Could be removed any time.
    /// </summary>
    private MpvOption<int>? _gpuTextPadY;
    public MpvOption<int> GpuTextPadY => _gpuTextPadY ??= new(this, "gpu-tex-pad-y");

    /// <summary>
    /// Call glFlush() after rendering a frame and before attempting to display it (default: auto). Can fix stuttering in some cases, in other cases probably causes it. The auto mode will call glFlush() only if the renderer is going to wait for a while after rendering, instead of flipping GL front and backbuffers immediately (i.e. it doesn't call it in display-sync mode).
    /// On OSX this is always deactivated because it only causes performance problems and other regressions.
    /// </summary>
    private MpvOptionWithAuto<bool>? _openGlEarlyFlush;
    public MpvOptionWithAuto<bool> OpenGlEarlyFlush => _openGlEarlyFlush ??= new(this, "opengl-early-flush");

    /// <summary>
    /// This mode is extremely restricted, and will disable most extended features. That includes high quality scalers and custom shaders!
    /// It is intended for hardware that does not support FBOs(including GLES, which supports it insufficiently), or to get some more performance out of bad or old hardware.
    /// This mode is forced automatically if needed, and this option is mostly useful for debugging.The default of auto will enable it automatically if nothing uses features which require FBOs.
    /// </summary>
    private MpvOptionWithAuto<bool>? _gpuDumbMode;
    public MpvOptionWithAuto<bool> GpuDumbMode => _gpuDumbMode ??= new(this, "gpu-dumb-mode");

    /// <summary>
    /// Store and load compiled GLSL shaders in this directory. Normally, shader compilation is very fast, so this is usually not needed. It mostly matters for GPU APIs that require internally recompiling shaders to other languages, for example anything based on ANGLE or Vulkan. Enabling this can improve startup performance on these platforms.
    /// NOTE: This is not cleaned automatically, so old, unused cache files may stick around indefinitely.
    /// </summary>
    private MpvOptionString? _gpuShaderCacheDir;
    public MpvOptionString GpuShaderCacheDir => _gpuShaderCacheDir ??= new(this, "gpu-shader-cache-dir");

    /// <summary>
    /// Set the list of tags that should be displayed on the terminal. Tags that are in the list, but are not present in the played file, will not be shown. If a value ends with *, all tags are matched by prefix (though there is no general globbing). Just passing * essentially filtering.
    /// The default includes a common list of tags, call mpv with --list-options to see it.
    /// </summary>
    private MpvOptionList? _displayTags;
    public MpvOptionList DisplayTags => _displayTags ??= new(this, "display-tags");

    /// <summary>
    /// Maximum A-V sync correction per frame (in seconds).
    /// </summary>
    private MpvOption<double>? _maximumCorrection;
    public MpvOption<double> MaximumCorrection => _maximumCorrection ??= new(this, "mc");

    /// <summary>
    /// Gradually adjusts the A/V sync based on audio delay measurements. Specifying --autosync=0, the default, will cause frame timing to be based entirely on audio delay measurements. Specifying --autosync=1 will do the same, but will subtly change the A/V correction algorithm. An uneven video framerate in a video which plays fine with --no-audio can often be helped by setting this to an integer value greater than 1. The higher the value, the closer the timing will be to --no-audio. Try --autosync=30 to smooth out problems with sound drivers which do not implement a perfect audio delay measurement. With this value, if large A/V sync offsets occur, they will only take about 1 or 2 seconds to settle out. This delay in reaction time to sudden A/V offsets should be the only side effect of turning this option on, for all sound drivers.
    /// </summary>
    private MpvOption<int>? _autoSync;
    public MpvOption<int> AutoSync => _autoSync ??= new(this, "autosync");

    /// <summary>
    /// Control how long before video display target time the frame should be rendered (default: 0.050). If a video frame should be displayed at a certain time, the VO will start rendering the frame earlier, and then will perform a blocking wait until the display time, and only then "swap" the frame to display. The rendering cannot start before the previous frame is displayed, so this value is implicitly limited by the video framerate. With normal video frame rates, the default value will ensure that rendering is always immediately started after the previous frame was displayed. On the other hand, setting a too high value can reduce responsiveness with low FPS value.
    /// For client API users using the render API(or the deprecated opengl-cb API), this option is interesting, because you can stop the render API from limiting your FPS (see mpv_render_context_render() documentation).
    /// This applies only to audio timing modes (e.g. --video-sync=audio). In other modes (--video-sync=display-...), video timing relies on vsync blocking, and this option is not used.
    /// </summary>
    private MpvOption<double>? _videoTimingOffset;
    public MpvOption<double> VideoTimingOffset => _videoTimingOffset ??= new(this, "video-timing-offset");

    /// <summary>
    /// How the player synchronizes audio and video.
    /// </summary>
    private MpvOptionEnum<VideoSyncMode>? _videoSync;
    public MpvOptionEnum<VideoSyncMode> VideoSync => _videoSync ??= new(this, "video-sync");

    /// <summary>
    /// Maximum multiple for which to try to fit the video's FPS to the display's FPS (default: 5).
    /// </summary>
    private MpvOption<int>? _videoSyncMaxFactor;
    public MpvOption<int> VideoSyncMaxFactor => _videoSyncMaxFactor ??= new(this, "video-sync-max-factor");

    /// <summary>
    /// Maximum speed difference in percent that is applied to video with --video-sync=display-... (default: 1). Display sync mode will be disabled if the monitor and video refresh way do not match within the given range. It tries multiples as well: playing 30 fps video on a 60 Hz screen will duplicate every second frame. Playing 24 fps video on a 60 Hz screen will play video in a 2-3-2-3-... pattern.
    /// </summary>
    private MpvOption<double>? _videoSyncMaxVideoChange;
    public MpvOption<double> VideoSyncMaxVideoChange => _videoSyncMaxVideoChange ??= new(this, "video-sync-max-video-change");

    /// <summary>
    /// Maximum additional speed difference in percent that is applied to audio with --video-sync=display-... (default: 0.125). Normally, the player plays the audio at the speed of the video. But if the difference between audio and video position is too high, e.g. due to drift or other timing errors, it will attempt to speed up or slow down audio by this additional factor. Too low values could lead to video frame dropping or repeating if the A/V desync cannot be compensated, too high values could lead to chaotic frame dropping due to the audio "overshooting" and skipping multiple video frames before the sync logic can react.
    /// </summary>
    private MpvOption<double>? _videoSyncMaxAudioChange;
    public MpvOption<double> VideoSyncMaxAudioChange => _videoSyncMaxAudioChange ??= new(this, "video-sync-max-audio-change");

    /// <summary>
    /// Framerate used when decoding from multiple PNG or JPEG files with mf:// (default: 1).
    /// </summary>
    private MpvOption<double>? _mfFps;
    public MpvOption<double> MfFps => _mfFps ??= new(this, "mf-fps");

    /// <summary>
    /// Input file type for mf:// (available: jpeg, png, tga, sgi). By default, this is guessed from the file extension.
    /// </summary>
    private MpvOptionString? _mfType;
    public MpvOptionString MfType => _mfType ??= new(this, "mf-type");

    /// <summary>
    /// Instead of playing a file, read its byte stream and write it to the given destination file. The destination is overwritten. Can be useful to test network-related behavior.
    /// </summary>
    private MpvOptionString? _streamDump;
    public MpvOptionString StreamDump => _streamDump ??= new(this, "stream-dump");

    /// <summary>
    /// Set AVOptions on streams opened with libavformat. Unknown or misspelled options are silently ignored. (They are mentioned in the terminal output in verbose mode, i.e. --v. In general we can't print errors, because other options such as e.g. user agent are not available with all protocols, and printing errors for unknown options would end up being too noisy.)
    /// </summary>
    private MpvOptionDictionary? _streamLavfOptions;
    public MpvOptionDictionary StreamLavfOptions => _streamLavfOptions ??= new(this, "stream-lavf-o");

    /// <summary>
    /// (Windows only.) Set the MMCSS profile for the video renderer thread (default: Playback).
    /// </summary>
    private MpvOptionString? _voMmcssProfile;
    public MpvOptionString VoMmcssProfile => _voMmcssProfile ??= new(this, "vo-mmcss-profile");

    /// <summary>
    /// (Windows only.) Set process priority for mpv according to the predefined priorities available under Windows.
    /// </summary>
    private MpvOption<ProcessPriority>? _priority;
    public MpvOption<ProcessPriority> Priority => _priority ??= new(this, "priority");

    /// <summary>
    /// Force the contents of the media-title property to this value. Useful for scripts which want to set a title, without overriding the user's setting in --title.
    /// </summary>
    private MpvOptionString? _forceMediaTitle;
    public MpvOptionString ForceMediaTitle => _forceMediaTitle ??= new(this, "force-media-title");

    /// <summary>
    /// Load a file and add all of its tracks. This is useful to play different files together (for example audio from one file, video from another), or for advanced --lavfi-complex used (like playing two video files at the same time).
    /// Unlike --sub-files and --audio-files, this includes all tracks, and does not cause default stream selection over the "proper" file.This makes it slightly less intrusive. (In mpv 0.28.0 and before, this was not quite strictly enforced.)
    /// </summary>
    private MpvOptionList? _externalFiles;
    public MpvOptionList ExternalFiles => _externalFiles ??= new(this, "external-files");

    /// <summary>
    /// Use an external file as cover art while playing audio. This makes it appear on the track list and subject to automatic track selection. Options like --audio-display control whether such tracks are supposed to be selected.
    /// (The difference to loading a file with --external-files is that video tracks will be marked as being pictures, which affects the auto-selection method.If the passed file is a video, only the first frame will be decoded and displayed.Enabling the cover art track during playback may show a random frame if the source file is a video. Normally you're not supposed to pass videos to this option, so this paragraph describes the behavior coincidentally resulting from implementation details.)
    /// </summary>
    private MpvOptionList? _coverArtFiles;
    public MpvOptionList CoverArtFiles => _coverArtFiles ??= new(this, "cover-art-files");

    /// <summary>
    /// Whether to load _external_ cover art automatically (default: fuzzy). Similar to --sub-auto and --audio-file-auto. However, it's currently limited to picking up a whitelist of "album art" filenames (such as cover.jpg), so currently only the fuzzy choice is available. In addition, if a video already has tracks (which are not marked as cover art), external cover art will not be loaded.
    /// </summary>
    private MpvOptionEnum<CoverArtMode>? _coverArtAuto;
    public MpvOptionEnum<CoverArtMode> CoverArtAuto => _coverArtAuto ??= new(this, "cover-art-auto");

    /// <summary>
    /// Automatically load/select external files (default: yes).
    /// If set to no, then do not automatically load external files as specified by --sub-auto and --audio-file-auto.If external files are forcibly added(like with --sub-files), they will not be auto-selected.
    /// This does not affect playlist expansion, redirection, or other loading of referenced files like with ordered chapters.
    /// </summary>
    private MpvOption<bool>? _autoLoadFiles;
    public MpvOption<bool> AutoLoadFiles => _autoLoadFiles ??= new(this, "autoload-files");

    /// <summary>
    /// Write received/read data from the demuxer to the given output file. The output file will always be overwritten without asking. The output format is determined by the extension of the output file.
    /// </summary>
    private MpvOptionString? _streamRecord;
    public MpvOptionString StreamRecord => _streamRecord ??= new(this, "stream-record");

    /// <summary>
    /// Set a "complex" libavfilter filter, which means a single filter graph can take input from multiple source audio and video tracks. The graph can result in a single audio or video output (or both).
    /// </summary>
    private MpvOptionString? _lavfiComplex;
    public MpvOptionString LavfiComplex => _lavfiComplex ??= new(this, "lavfi-complex");

    /// <summary>
    /// Codepage for various input metadata (default: utf-8). This affects how file tags, chapter titles, etc. are interpreted. You can for example set this to auto to enable autodetection of the codepage. (This is not the default because non-UTF-8 codepages are an obscure fringe use-case.)
    /// </summary>
    private MpvOptionString? _metadataCodepage;
    public MpvOptionString MetadataCodepage => _metadataCodepage ??= new(this, "metadata-codepage");

    /// <summary>
    /// Run an internal unit test. There are multiple, and the name specifies which.
    /// The special value all-simple runs all tests which do not need further setup(other arguments and such). Some tests may need additional arguments to do anything useful.
    /// </summary>
    private MpvOptionString? _unitTest;
    public MpvOptionString UnitTest => _unitTest ??= new(this, "unittest");
}
