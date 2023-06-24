using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace HanumanInstitute.LibMpv.Tests.IntegrationTests;

public class MpvOptionTests
{
    private readonly ITestOutputHelper _output;

    public MpvOptionTests(ITestOutputHelper output)
    {
        _output = output;
    }

    [Fact]
    public async Task ObserveAsync_VideoIdProperty_RaisesPropertyChanged()
    {
        using var app = await IntegrationTestBase.CreateAsync();
        var observeId = 1;
        string? changedName = null;

        try
        {
            Mpv.PropertyChanged += (s, e) =>
            {
                changedName = e.Name;
            };
            await Mpv.VideoId.ObserveAsync(observeId);
            await app.LoadVideoAsync();

            Assert.Equal(Mpv.VideoId.PropertyName, changedName);
        }
        finally
        {
            Log(response);
        }
    }

    [Fact]
    public async Task UnobserveAsync_VideoIdProperty_DoesNotRaisePropertyChanged()
    {
        using var app = await IntegrationTestBase.CreateAsync();
        var observeId = 1;
        string? changedName = null;
        await Mpv.VideoId.ObserveAsync(observeId);

        try
        {
            Mpv.PropertyChanged += (s, e) =>
            {
                changedName = e.Name;
            };
            await Mpv.UnobservePropertyAsync(observeId);
            await Task.Delay(100);
            await app.LoadVideoAsync();

            Assert.Null(changedName);
        }
        finally
        {
            Log(response);
        }
    }

    [Fact]
    public async Task GetAsync_Volume_ReturnsValue()
    {
        try
        {
            var result = await Mpv.Volume.GetAsync();

            Assert.True(result > 0);
        }
        finally
        {
            Log(response);
        }
    }

    [Fact]
    public async Task SetAsync_Volume_HasNewValue()
    {
        using var app = await IntegrationTestBase.CreateAsync();
        var volume = 100;

        try
        {
            await Mpv.Volume.SetAsync(volume);

            var result = await Mpv.Volume.GetAsync();
            Assert.Equal(volume, result);
        }
        finally
        {
            Log(response);
        }
    }

    [Fact]
    public async Task AddAsync_Volume_HasAddedValue()
    {
        using var app = await IntegrationTestBase.CreateAsync();
        var volume = 50;
        var volumeAdd = 10;
        await Mpv.Volume.SetAsync(volume);

        try
        {
            await Mpv.Volume.AddAsync(volumeAdd);

            var result = await Mpv.Volume.GetAsync();
            Assert.Equal(volume + volumeAdd, result);
        }
        finally
        {
            Log(response);
        }
    }

    [Fact]
    public async Task MultiplyAsync_Volume_HasMultipliedValue()
    {
        using var app = await IntegrationTestBase.CreateAsync();
        var volume = 50;
        var volumeMul = 1.1;
        await Mpv.Volume.SetAsync(volume);

        try
        {
            await Mpv.Volume.MultiplyAsync(volumeMul);

            var result = await Mpv.Volume.GetAsync();
            Assert.Equal((int)(volume * volumeMul), result);
        }
        finally
        {
            Log(response);
        }
    }

    [Fact]
    public async Task CycleAsync_VolumeUp_HasGreaterValue()
    {
        using var app = await IntegrationTestBase.CreateAsync();
        var volume = 50;
        await Mpv.Volume.SetAsync(volume);

        try
        {
            await Mpv.Volume.CycleAsync();

            var result = await Mpv.Volume.GetAsync();
            Assert.True(result > volume);
        }
        finally
        {
            Log(response);
        }
    }

    [Fact]
    public async Task CycleAsync_VolumeDown_HasLowerValue()
    {
        using var app = await IntegrationTestBase.CreateAsync();
        var volume = 50;
        await Mpv.Volume.SetAsync(volume);

        try
        {
            await Mpv.Volume.CycleAsync(CycleDirection.Down);

            var result = await Mpv.Volume.GetAsync();
            Assert.True(result < volume);
        }
        finally
        {
            Log(response);
        }
    }
}
