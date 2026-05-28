using System.Threading;
using System.Threading.Tasks;
using Xunit;


namespace HanumanInstitute.LibMpv.Tests.IntegrationTests;

public class MpvApiIntegrationTests(ITestOutputHelper output) : IntegrationTestBase(output)
{
    // -------------------------------------------------------------------------
    // Idle-state properties (no file required)
    // -------------------------------------------------------------------------

    [Fact]
    public void MpvVersion_Get_ReturnsValue()
    {
        var response = Mpv.MpvVersion.Get();

        Assert.NotNull(response);
        Assert.NotEmpty(response);
        Log(response);
    }

    [Fact]
    public async Task MpvVersion_GetAsync_ReturnsValue()
    {
        var response = await Mpv.MpvVersion.GetAsync();

        Log(response);
        Assert.NotNull(response);
        Assert.NotEmpty(response);
    }

    [Fact]
    public void MpvConfiguration_Get_ReturnsValue()
    {
        var result = Mpv.MpvConfiguration.Get();

        Log(result);
        Assert.NotNull(result);
        Assert.NotEmpty(result);
    }

    [Fact]
    public async Task MpvConfiguration_GetAsync_ReturnsValue()
    {
        var result = await Mpv.MpvConfiguration.GetAsync();

        Log(result);
        Assert.NotNull(result);
        Assert.NotEmpty(result);
    }

    [Fact]
    public void FFmpegVersion_Get_ReturnsValue()
    {
        var result = Mpv.FFmpegVersion.Get();

        Log(result);
        Assert.NotNull(result);
        Assert.NotEmpty(result);
    }

    [Fact]
    public async Task FFmpegVersion_GetAsync_ReturnsValue()
    {
        var result = await Mpv.FFmpegVersion.GetAsync();

        Log(result);
        Assert.NotNull(result);
        Assert.NotEmpty(result);
    }

    [Fact]
    public void WorkingDirectory_Get_ReturnsValue()
    {
        var result = Mpv.WorkingDirectory.Get();

        Log(result);
        Assert.NotNull(result);
        Assert.NotEmpty(result);
    }

    [Fact]
    public async Task WorkingDirectory_GetAsync_ReturnsValue()
    {
        var result = await Mpv.WorkingDirectory.GetAsync();

        Log(result);
        Assert.NotNull(result);
        Assert.NotEmpty(result);
    }

    [Fact]
    public void IdleActive_Get_ReturnsTrue()
    {
        var result = Mpv.IdleActive.Get();

        Log(result);
        Assert.True(result);
    }

    [Fact]
    public async Task IdleActive_GetAsync_ReturnsTrue()
    {
        var result = await Mpv.IdleActive.GetAsync();

        Log(result);
        Assert.True(result);
    }

    [Fact]
    public void CoreIdle_Get_ReturnsTrue()
    {
        var result = Mpv.CoreIdle.Get();

        Log(result);
        Assert.True(result);
    }

    [Fact]
    public async Task CoreIdle_GetAsync_ReturnsTrue()
    {
        var result = await Mpv.CoreIdle.GetAsync();

        Log(result);
        Assert.True(result);
    }

    // -------------------------------------------------------------------------
    // File-required properties
    // -------------------------------------------------------------------------

    [Fact]
    public void LoadFile_WithPrefix_PathNotNull()
    {
        Mpv.LoadFile(SampleClip).Invoke(options: new MpvCommandOptions() { NoOsd = true });
        Thread.Sleep(50);
        var path = Mpv.Path.Get();

        Log(path);
        Assert.NotNull(path);
        Assert.NotEmpty(path);
    }

    [Fact]
    public async Task LoadFileAsync_WithPrefix_PathNotNull()
    {
        await Mpv.LoadFile(SampleClip).InvokeAsync(options: new MpvCommandOptions() { NoOsd = true });
        await Task.Delay(50, TestContext.Current.CancellationToken);
        var path = await Mpv.Path.GetAsync();

        Log(path);
        Assert.NotNull(path);
        Assert.NotEmpty(path);
    }

    [Fact]
    public void FileName_LoadedFile_Get_ReturnsValue()
    {
        LoadVideo();
        var result = Mpv.FileName.Get();

        Log(result);
        Assert.NotNull(result);
        Assert.NotEmpty(result);
    }

    [Fact]
    public async Task FileName_LoadedFile_GetAsync_ReturnsValue()
    {
        LoadVideo();
        var result = await Mpv.FileName.GetAsync();

        Log(result);
        Assert.NotNull(result);
        Assert.NotEmpty(result);
    }

    [Fact]
    public void FileNameNoExt_LoadedFile_Get_ReturnsValue()
    {
        LoadVideo();
        var result = Mpv.FileNameNoExt.Get();

        Log(result);
        Assert.NotNull(result);
        Assert.NotEmpty(result);
    }

    [Fact]
    public async Task FileNameNoExt_LoadedFile_GetAsync_ReturnsValue()
    {
        LoadVideo();
        var result = await Mpv.FileNameNoExt.GetAsync();

        Log(result);
        Assert.NotNull(result);
        Assert.NotEmpty(result);
    }

    [Fact]
    public void MediaTitle_LoadedFile_Get_ReturnsValue()
    {
        LoadVideo();
        var result = Mpv.MediaTitle.Get();

        Log(result);
        Assert.NotNull(result);
        Assert.NotEmpty(result);
    }

    [Fact]
    public async Task MediaTitle_LoadedFile_GetAsync_ReturnsValue()
    {
        LoadVideo();
        var result = await Mpv.MediaTitle.GetAsync();

        Log(result);
        Assert.NotNull(result);
        Assert.NotEmpty(result);
    }

    [Fact]
    public void FileFormat_LoadedFile_Get_ReturnsValue()
    {
        LoadVideo();
        var result = Mpv.FileFormat.Get();

        Log(result);
        Assert.NotNull(result);
        Assert.NotEmpty(result);
    }

    [Fact]
    public async Task FileFormat_LoadedFile_GetAsync_ReturnsValue()
    {
        LoadVideo();
        var result = await Mpv.FileFormat.GetAsync();

        Log(result);
        Assert.NotNull(result);
        Assert.NotEmpty(result);
    }

    [Fact]
    public void Duration_LoadedFile_Get_ReturnsPositiveValue()
    {
        LoadVideo();
        var result = Mpv.Duration.Get();

        Log(result);
        Assert.True(result > 0);
    }

    [Fact]
    public async Task Duration_LoadedFile_GetAsync_ReturnsPositiveValue()
    {
        LoadVideo();
        var result = await Mpv.Duration.GetAsync();

        Log(result);
        Assert.True(result > 0);
    }

    [Fact]
    public void TimeRemaining_LoadedFile_Get_ReturnsPositiveValue()
    {
        LoadVideo();
        var result = Mpv.TimeRemaining.Get();

        Log(result);
        Assert.True(result > 0);
    }

    [Fact]
    public async Task TimeRemaining_LoadedFile_GetAsync_ReturnsPositiveValue()
    {
        LoadVideo();
        var result = await Mpv.TimeRemaining.GetAsync();

        Log(result);
        Assert.True(result > 0);
    }

    [Fact]
    public void PlaybackTime_LoadedFile_Get_ReturnsNonNegativeValue()
    {
        LoadVideo();
        var result = Mpv.PlaybackTime.Get();

        Log(result);
        Assert.True(result >= 0);
    }

    [Fact]
    public async Task PlaybackTime_LoadedFile_GetAsync_ReturnsNonNegativeValue()
    {
        LoadVideo();
        var result = await Mpv.PlaybackTime.GetAsync();

        Log(result);
        Assert.True(result >= 0);
    }

    [Fact]
    public void Width_LoadedFile_Get_ReturnsPositiveValue()
    {
        LoadVideo();
        var result = Mpv.Width.Get();

        Log(result);
        Assert.True(result > 0);
    }

    [Fact]
    public async Task Width_LoadedFile_GetAsync_ReturnsPositiveValue()
    {
        LoadVideo();
        var result = await Mpv.Width.GetAsync();

        Log(result);
        Assert.True(result > 0);
    }

    [Fact]
    public void Height_LoadedFile_Get_ReturnsPositiveValue()
    {
        LoadVideo();
        var result = Mpv.Height.Get();

        Log(result);
        Assert.True(result > 0);
    }

    [Fact]
    public async Task Height_LoadedFile_GetAsync_ReturnsPositiveValue()
    {
        LoadVideo();
        var result = await Mpv.Height.GetAsync();

        Log(result);
        Assert.True(result > 0);
    }

    [Fact]
    public void VideoCodec_LoadedFile_Get_ReturnsValue()
    {
        LoadVideo();
        var result = Mpv.VideoCodec.Get();

        Log(result);
        Assert.NotNull(result);
        Assert.NotEmpty(result);
    }

    [Fact]
    public async Task VideoCodec_LoadedFile_GetAsync_ReturnsValue()
    {
        LoadVideo();
        var result = await Mpv.VideoCodec.GetAsync();

        Log(result);
        Assert.NotNull(result);
        Assert.NotEmpty(result);
    }

    [Fact]
    public void TrackListCount_LoadedFile_Get_ReturnsPositiveValue()
    {
        LoadVideo();
        var result = Mpv.TrackListCount.Get();

        Log(result);
        Assert.True(result > 0);
    }

    [Fact]
    public async Task TrackListCount_LoadedFile_GetAsync_ReturnsPositiveValue()
    {
        LoadVideo();
        var result = await Mpv.TrackListCount.GetAsync();

        Log(result);
        Assert.True(result > 0);
    }

    [Fact]
    public void PlaylistCount_LoadedFile_Get_ReturnsPositiveValue()
    {
        LoadVideo();
        var result = Mpv.PlaylistCount.Get();

        Log(result);
        Assert.True(result > 0);
    }

    [Fact]
    public async Task PlaylistCount_LoadedFile_GetAsync_ReturnsPositiveValue()
    {
        LoadVideo();
        var result = await Mpv.PlaylistCount.GetAsync();

        Log(result);
        Assert.True(result > 0);
    }

    [Fact]
    public void Chapters_LoadedFile_Get_ReturnsNonNegativeValue()
    {
        LoadVideo();
        var result = Mpv.Chapters.Get();

        Log(result);
        Assert.True(result >= 0);
    }

    [Fact]
    public async Task Chapters_LoadedFile_GetAsync_ReturnsNonNegativeValue()
    {
        LoadVideo();
        var result = await Mpv.Chapters.GetAsync();

        Log(result);
        Assert.True(result >= 0);
    }

    [Fact]
    public void Seekable_LoadedFile_Get_ReturnsTrue()
    {
        LoadVideo();
        var result = Mpv.Seekable.Get();

        Log(result);
        Assert.True(result);
    }

    [Fact]
    public async Task Seekable_LoadedFile_GetAsync_ReturnsTrue()
    {
        LoadVideo();
        var result = await Mpv.Seekable.GetAsync();

        Log(result);
        Assert.True(result);
    }

    [Fact]
    public void DemuxerViaNetwork_LoadedFile_Get_ReturnsFalse()
    {
        LoadVideo();
        var result = Mpv.DemuxerViaNetwork.Get();

        Log(result);
        Assert.False(result);
    }

    [Fact]
    public async Task DemuxerViaNetwork_LoadedFile_GetAsync_ReturnsFalse()
    {
        LoadVideo();
        var result = await Mpv.DemuxerViaNetwork.GetAsync();

        Log(result);
        Assert.False(result);
    }

    [Fact]
    public void ContainerFps_LoadedFile_Get_ReturnsPositiveValue()
    {
        LoadVideo();
        var result = Mpv.ContainerFps.Get();

        Log(result);
        Assert.True(result > 0);
    }

    [Fact]
    public async Task ContainerFps_LoadedFile_GetAsync_ReturnsPositiveValue()
    {
        LoadVideo();
        var result = await Mpv.ContainerFps.GetAsync();

        Log(result);
        Assert.True(result > 0);
    }

    [Fact]
    public void FileSize_LoadedFile_Get_ReturnsPositiveValue()
    {
        LoadVideo();
        var result = Mpv.FileSize.Get();

        Log(result);
        Assert.True(result > 0);
    }

    [Fact]
    public async Task FileSize_LoadedFile_GetAsync_ReturnsPositiveValue()
    {
        LoadVideo();
        var result = await Mpv.FileSize.GetAsync();

        Log(result);
        Assert.True(result > 0);
    }

    [Fact]
    public void VideoFormat_LoadedFile_Get_ReturnsValue()
    {
        LoadVideo();
        var result = Mpv.VideoFormat.Get();

        Log(result);
        Assert.NotNull(result);
        Assert.NotEmpty(result);
    }

    [Fact]
    public async Task VideoFormat_LoadedFile_GetAsync_ReturnsValue()
    {
        LoadVideo();
        var result = await Mpv.VideoFormat.GetAsync();

        Log(result);
        Assert.NotNull(result);
        Assert.NotEmpty(result);
    }

    [Fact]
    public void EofReached_LoadedFile_Get_ReturnsFalse()
    {
        LoadVideo();
        var result = Mpv.EofReached.Get();

        Log(result);
        Assert.False(result);
    }

    [Fact]
    public async Task EofReached_LoadedFile_GetAsync_ReturnsFalse()
    {
        LoadVideo();
        var result = await Mpv.EofReached.GetAsync();

        Log(result);
        Assert.False(result);
    }

    [Fact]
    public void PlaytimeRemaining_LoadedFile_Get_ReturnsPositiveValue()
    {
        LoadVideo();
        var result = Mpv.PlaytimeRemaining.Get();

        Log(result);
        Assert.True(result > 0);
    }

    [Fact]
    public async Task PlaytimeRemaining_LoadedFile_GetAsync_ReturnsPositiveValue()
    {
        LoadVideo();
        var result = await Mpv.PlaytimeRemaining.GetAsync();

        Log(result);
        Assert.True(result > 0);
    }

    [Fact]
    public void ChapterListCount_LoadedFile_Get_ReturnsNonNegativeValue()
    {
        LoadVideo();
        var result = Mpv.ChapterListCount.Get();

        Log(result);
        Assert.True(result >= 0);
    }

    [Fact]
    public async Task ChapterListCount_LoadedFile_GetAsync_ReturnsNonNegativeValue()
    {
        LoadVideo();
        var result = await Mpv.ChapterListCount.GetAsync();

        Log(result);
        Assert.True(result >= 0);
    }

}
