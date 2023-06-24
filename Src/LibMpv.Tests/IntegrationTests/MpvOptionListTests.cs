using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace HanumanInstitute.LibMpv.Tests.IntegrationTests;

public class MpvOptionListTests
{
    private readonly ITestOutputHelper _output;
    // private const string Prop = "volume";
    private readonly IEnumerable<string> propList = new[] { "volume", "vid", "aid" };

    public MpvOptionListTests(ITestOutputHelper output)
    {
        _output = output;
    }

    private static object[] GetProp() => new[]
    {
        new[] { "%6%VOLume" },
        new[] { "m\"&\"n" },
        new[] { "漢字" },
        new[] { "Me, Myself & I" },
        new[] { "%1%2%3%4%5" },
        new[] { "[ABC]abc" }
    };

    [Fact]
    public async Task GetAsync_ResetOnNextFile_ReturnsEmptyList()
    {
        try
        {
            var result = await Mpv.ResetOnNextFile.GetAsync();

            Assert.NotNull(result);
            Assert.Empty(result);
        }
        finally
        {
            Log(response);
        }
    }

    [Theory]
    [MemberData(nameof(GetProp))]
    public async Task SetAsync_SingleValue_HasSingleValue(string prop)
    {
        try
        {
            await Mpv.ResetOnNextFile.SetAsync(prop);

            var result = await Mpv.ResetOnNextFile.GetAsync();
            Assert.Single(result);
            Assert.Equal(prop, result.First());
        }
        finally
        {
            Log(response);
        }
    }

    [Fact]
    public async Task SetAsync_MultipleValues_HasMultipleValues()
    {
        try
        {
            await Mpv.ResetOnNextFile.SetAsync(propList);

            var result = await Mpv.ResetOnNextFile.GetAsync();
            Assert.Equal(propList, result);
        }
        finally
        {
            Log(response);
        }
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public async Task SetAsync_Empty_ListEmpty(string prop)
    {
        try
        {
            await Mpv.ResetOnNextFile.SetAsync(prop);

            var result = await Mpv.ResetOnNextFile.GetAsync();
            Assert.Empty(result);
        }
        finally
        {
            Log(response);
        }
    }

    [Theory]
    [MemberData(nameof(GetProp))]
    public async Task AddAsync_SingleValue_ReturnsValue(string prop)
    {
        try
        {
            await Mpv.ResetOnNextFile.AddAsync(prop);

            var result = await Mpv.ResetOnNextFile.GetAsync();
            Assert.Single(result);
            Assert.Equal(prop, result.First());
        }
        finally
        {
            Log(response);
        }
    }

    [Fact]
    public async Task AddAsync_MultipleValues_ReturnsValue()
    {
        try
        {
            await Mpv.ResetOnNextFile.AddAsync(propList);

            var result = await Mpv.ResetOnNextFile.GetAsync();
            Assert.Equal(propList, result);
        }
        finally
        {
            Log(response);
        }
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public async Task AddAsync_Empty_ThrowsException(string prop)
    {
        try
        {
            Task Act() => Mpv.ResetOnNextFile.AddAsync(prop);

            await Assert.ThrowsAnyAsync<ArgumentException>(Act);
        }
        finally
        {
            Log(response);
        }
    }

    [Theory]
    [MemberData(nameof(GetProp))]
    public async Task RemoveAsync_SingleValue_ListEmpty(string prop)
    {
        using var app = await IntegrationTestBase.CreateAsync();
        await Mpv.ResetOnNextFile.AddAsync(prop);

        try
        {
            await Mpv.ResetOnNextFile.RemoveAsync(prop);

            var result = await Mpv.ResetOnNextFile.GetAsync();
            Assert.Empty(result);
        }
        finally
        {
            Log(response);
        }
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public async Task RemoveAsync_Empty_ThrowsException(string prop)
    {
        try
        {
            Task Act() => Mpv.ResetOnNextFile.RemoveAsync(prop);

            await Assert.ThrowsAnyAsync<ArgumentException>(Act);
        }
        finally
        {
            Log(response);
        }
    }

    [Theory]
    [InlineData("volume")]
    public async Task ClearAsync_SingleValue_ListEmpty(string prop)
    {
        using var app = await IntegrationTestBase.CreateAsync();
        await Mpv.ResetOnNextFile.AddAsync(prop);

        try
        {
            await Mpv.ResetOnNextFile.ClearAsync();

            var result = await Mpv.ResetOnNextFile.GetAsync();
            Assert.Empty(result);
        }
        finally
        {
            Log(response);
        }
    }
}
