using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace HanumanInstitute.LibMpv.Tests.IntegrationTests;

public class MpvOptionAutoTests : IntegrationTestBase
{
    public MpvOptionAutoTests(ITestOutputHelper output) : base(output)
    {
    }

    [Fact]
    public void Get_VideoId_ReturnsFalse()
    {
        LoadVideo();
        var response = Mpv.VideoId.Get();

        Log(response);
        Assert.Equal(1, response);
    }

    [Fact]
    public async Task GetAsync_VideoId_ReturnsFalse()
    {
        LoadVideo();
        var response = await Mpv.VideoId.GetAsync();

        Log(response);
        Assert.Equal(1, response);
    }

    [Fact]
    public void GetAuto_VideoId_ReturnsFalse()
    {
        Mpv.VideoId.Set(0);
        var response = Mpv.VideoId.GetAuto();

        Log(response);
        Assert.False(response);
    }

    [Fact]
    public async Task GetAutoAsync_VideoId_ReturnsFalse()
    {
        await Mpv.VideoId.SetAsync(0);
        var response = await Mpv.VideoId.GetAutoAsync();

        Log(response);
        Assert.False(response);
    }

    [Fact]
    public void GetNo_VideoId_ReturnsFalse()
    {
        Mpv.VideoId.Set(0);
        var response = Mpv.VideoId.GetNo();

        Log(response);
        Assert.False(response);
    }

    [Fact]
    public async Task GetNoAsync_VideoId_ReturnsFalse()
    {
        await Mpv.VideoId.SetAsync(0);
        var response = await Mpv.VideoId.GetNoAsync();

        Log(response);
        Assert.False(response);
    }

    [Fact]
    public void SetAuto_VideoId_VideoIdSet()
    {
        Mpv.VideoId.SetAuto();

        var result = Mpv.VideoId.GetAuto();

        Log(result);
        Assert.True(result);
    }

    [Fact]
    public async Task SetAutoAsync_VideoId_VideoIdSet()
    {
        await Mpv.VideoId.SetAutoAsync();

        var result = await Mpv.VideoId.GetAutoAsync();

        Log(result);
        Assert.True(result);
    }

    [Fact]
    public void SetNo_VideoId_VideoIdSet()
    {
        Mpv.VideoId.SetNo();

        var result = Mpv.VideoId.GetNo();

        Log(result);
        Assert.True(result);
    }

    [Fact]
    public async Task SetNoAsync_VideoId_VideoIdSet()
    {
        await Mpv.VideoId.SetNoAsync();

        var result = await Mpv.VideoId.GetNoAsync();

        Log(result);
        Assert.True(result);
    }

    [Fact]
    public void Get_ValueAuto_ReturnsNull()
    {
        Mpv.VideoId.SetAuto();

        var result = Mpv.VideoId.Get();

        Log(result);
        Assert.Null(result);
    }

    [Fact]
    public async Task GetAsync_ValueAuto_ReturnsNull()
    {
        await Mpv.VideoId.SetAutoAsync();

        var result = await Mpv.VideoId.GetAsync();

        Log(result);
        Assert.Null(result);
    }

    [Fact]
    public void Get_ValueNo_ReturnsNull()
    {
        Mpv.VideoId.SetNo();

        var result = Mpv.VideoId.Get();

        Log(result);
        Assert.Null(result);
    }

    [Fact]
    public async Task GetAsync_ValueNo_ReturnsNull()
    {
        await Mpv.VideoId.SetNoAsync();

        var result = await Mpv.VideoId.GetAsync();

        Log(result);
        Assert.Null(result);
    }
}
