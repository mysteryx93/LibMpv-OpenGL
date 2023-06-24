using System;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace HanumanInstitute.LibMpv.Tests.IntegrationTests;

public class MpvOptionEnumTests
{
    private readonly ITestOutputHelper _output;

    public MpvOptionEnumTests(ITestOutputHelper output)
    {
        _output = output;
    }

    [Fact]
    public async Task ObserveAsync_HrSeekOption_RaisesPropertyChanged()
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
            await Mpv.HrSeek.ObserveAsync(observeId);
            await Mpv.HrSeek.SetAsync(HrSeekOption.Absolute);

            Assert.Equal(Mpv.HrSeek.PropertyName, changedName);
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
        await Mpv.HrSeek.ObserveAsync(observeId);

        try
        {
            await Mpv.UnobservePropertyAsync(observeId);
            await Task.Delay(100);

            Mpv.PropertyChanged += (s, e) =>
            {
                changedName = e.Name;
            };
            await Mpv.HrSeek.SetAsync(HrSeekOption.Absolute);

            Assert.Null(changedName);
        }
        finally
        {
            Log(response);
        }
    }

    [Fact]
    public async Task GetAsync_HrSeek_ReturnsNull()
    {
        try
        {
            var result = await Mpv.HrSeek.GetAsync();

            Assert.Equal(HrSeekOption.Default, result);
        }
        finally
        {
            Log(response);
        }
    }


    [Theory]
    [InlineData(HrSeekOption.Default)]
    [InlineData(HrSeekOption.Absolute)]
    [InlineData(HrSeekOption.Yes)]
    [InlineData(HrSeekOption.No)]
    public async Task SetAsync_HrSeek_HasNewValue(HrSeekOption value)
    {
        try
        {
            await Mpv.HrSeek.SetAsync(value);

            var result = await Mpv.HrSeek.GetAsync();
            Assert.Equal(value, result);
        }
        finally
        {
            Log(response);
        }
    }

    [Fact]
    public async Task AddAsync_HrSeek_ThrowsException()
    {
        try
        {
            Task Act() => Mpv.HrSeek.AddAsync(HrSeekOption.Yes);

            await Assert.ThrowsAsync<NotImplementedException>(Act);
        }
        finally
        {
            Log(response);
        }
    }

    [Fact]
    public async Task MultiplyAsync_HrSeek_ThrowsException()
    {
        try
        {
            Task Act() => Mpv.HrSeek.MultiplyAsync(1);

            await Assert.ThrowsAsync<NotImplementedException>(Act);
        }
        finally
        {
            Log(response);
        }
    }

    [Fact]
    public async Task CycleAsync_HrSeekUp_HasGreaterValue()
    {
        try
        {
            await Mpv.HrSeek.CycleAsync();

            var result = await Mpv.HrSeek.GetAsync();
            Assert.NotEqual(HrSeekOption.Default, result);
        }
        finally
        {
            Log(response);
        }
    }

    [Fact]
    public async Task CycleAsync_HrSeekDown_HasLowerValue()
    {
        try
        {
            await Mpv.HrSeek.CycleAsync(CycleDirection.Down);

            var result = await Mpv.HrSeek.GetAsync();
            Assert.NotEqual(HrSeekOption.Default, result);
        }
        finally
        {
            Log(response);
        }
    }
}
