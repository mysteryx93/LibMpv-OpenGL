using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace HanumanInstitute.LibMpv.Tests.IntegrationTests;

public class MpvApiIntegrationTests : IntegrationTestBase
{
    public MpvApiIntegrationTests(ITestOutputHelper output) : base(output)
    { }

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
    public void LoadFile_WithPrefix_PathNotNull()
    {
        Mpv.LoadFile(SampleClip).Invoke(options: new MpvCommandOptions() { NoOsd = true });
        Thread.Sleep(50);
        var path = Mpv.Path.Get();

        Log(path);
        Assert.NotEmpty(path);
    }

    [Fact]
    public async Task LoadFileAsync_WithPrefix_PathNotNull()
    {
        await Mpv.LoadFile(SampleClip).InvokeAsync(options: new MpvCommandOptions() { NoOsd = true });
        await Task.Delay(50);
        var path = await Mpv.Path.GetAsync();

        Log(path);
        Assert.NotEmpty(path);
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

    //
    // [Fact]
    // public async Task Metadata_ValidFile_ReturnsDictionary()
    // {
    //     try
    //     {
    //         await Mpv.LoadFileAsync(app.SampleClip);
    //         var result = await Mpv.Metadata.Metadata.GetAsync();
    //
    //         Assert.NotEmpty(result);
    //     }
    //     finally
    //     {
    //         Log(response);
    //     }
    // }
    //
    // [Fact]
    // public async Task DemuxerCacheTime_ValidFile_ReturnsValue()
    // {
    //     try
    //     {
    //         await Mpv.LoadFileAsync(app.SampleClip);
    //         var result = await Mpv.DemuxerCacheTime.GetAsync();
    //
    //         Assert.NotNull(result);
    //     }
    //     finally
    //     {
    //         Log(response);
    //     }
    // }
    //
    // [Fact]
    // public async Task DemuxerCacheState_ValidFile_ReturnsParsedObject()
    // {
    //     try
    //     {
    //         await Mpv.LoadFileAsync(app.SampleClip);
    //         var result = await Mpv.DemuxerCacheState.GetAsync();
    //
    //         Assert.NotNull(result);
    //         Assert.NotEqual(0, result?.RawInputRate);
    //     }
    //     finally
    //     {
    //         Log(response);
    //     }
    // }
    //
    // [Fact]
    // public async Task VideoParamsRotate_ValidFile_ReturnsValue()
    // {
    //     try
    //     {
    //         await Mpv.LoadFileAsync(app.SampleClip);
    //         await Task.Delay(50);
    //         var result = await Mpv.VideoParams.Rotate.GetAsync();
    //
    //         Assert.NotNull(result);
    //     }
    //     finally
    //     {
    //         Log(response);
    //     }
    // }
    //
    // [Fact]
    // public async Task Z_RunCommand_ReturnsValue()
    // {
    //     using var app = await TestIntegrationSetup.CreateAsync();
    //     // app.Controller.DefaultOptions.ResponseTimeout = -1;
    //
    //     try
    //     {
    //         //await app.Api.LoadFileAsync(app.SampleClip);
    //         //await Task.Delay(100);
    //         var result = await Mpv.VideoSyncMaxVideoChange.GetAsync();
    //         //await app.Api.TerminalMsgLevel.SetAsync(new Dictionary<string, string>() { { "all", "warn" } });
    //         //var result = await app.Api.TerminalMsgLevel.GetAsync();
    //         //await Task.Delay(100);
    //
    //         // Assert.NotNull(result);
    //     }
    //     finally
    //     {
    //         Log(response);
    //     }
    // }
}
