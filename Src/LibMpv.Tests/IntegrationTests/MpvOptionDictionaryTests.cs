using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace HanumanInstitute.LibMpv.Tests.IntegrationTests;

public class MpvOptionDictionaryTests
{
    private readonly ITestOutputHelper _output;

    public MpvOptionDictionaryTests(ITestOutputHelper output)
    {
        _output = output;
    }

    private static object[] GetKeyValues() => new[]
    {
        new[] { "key1", "value1" },
        new[] { "m\"&\"n", "漢字" },
        new[] { "漢字", "" },
        new[] { "Me, Myself & I", "-=<->=-" },
        new[] { "%1%2%3%4%5", "%1%2%3%4%5" },
        new[] { "[ABC]abc", "Val[ABC]abc" }
    };

    private static IDictionary<string, string> GetKeyValuesList()
    {
        var result = new Dictionary<string, string>();
        foreach (IList<string> item in GetKeyValues())
        {
            result.Add(item[0], item[1]);
        }
        return result;
    }

    [Fact]
    public async Task GetAsync_YouTubeDlRawOptions_ReturnsEmptyList()
    {
        try
        {
            var result = await Mpv.YouTubeDlRawOptions.GetAsync();

            Assert.NotNull(result);
            Assert.Empty(result);
        }
        finally
        {
            Log(response);
        }
    }

    [Fact]
    public async Task SetAsync_MultipleValues_HasMultipleValues()
    {
        using var app = await IntegrationTestBase.CreateAsync();
        var values = GetKeyValuesList();

        try
        {
            // ScriptOptions isn't empty, it should override existing values.
            await Mpv.ScriptOptions.SetAsync(values);

            var result = await Mpv.ScriptOptions.GetAsync();
            Assert.Equal(values.ToList(), result?.ToList());
        }
        finally
        {
            Log(response);
        }
    }

    [Theory]
    [MemberData(nameof(GetKeyValues))]
    public async Task AddAsync_SingleValue_ReturnsValue(string key, string value)
    {
        try
        {
            await Mpv.YouTubeDlRawOptions.AddAsync(key, value);

            var result = await Mpv.YouTubeDlRawOptions.GetAsync();
            Assert.Single(result);
            Assert.Equal(new[] { new KeyValuePair<string, string>(key, value ?? "") }, result);
        }
        finally
        {
            Log(response);
        }
    }

    [Fact]
    public async Task AddAsync_MultipleValues_ReturnsValue()
    {
        using var app = await IntegrationTestBase.CreateAsync();
        var values = GetKeyValuesList();

        try
        {
            await Mpv.YouTubeDlRawOptions.AddAsync(values);

            var result = await Mpv.YouTubeDlRawOptions.GetAsync();
            Assert.Equal(values, result);
        }
        finally
        {
            Log(response);
        }
    }

    [Theory]
    [InlineData(null, "a")]
    [InlineData("", null)]
    public async Task AddAsync_EmptyKey_ThrowsException(string key, string value)
    {
        try
        {
            Task Act() => Mpv.YouTubeDlRawOptions.AddAsync(key, value);

            await Assert.ThrowsAnyAsync<ArgumentException>(Act);
        }
        finally
        {
            Log(response);
        }
    }

    [Theory]
    [MemberData(nameof(GetKeyValues))]
    public async Task RemoveAsync_SingleValue_ListEmpty(string key, string value)
    {
        using var app = await IntegrationTestBase.CreateAsync();
        await Mpv.YouTubeDlRawOptions.AddAsync(key, value);

        try
        {
            await Mpv.YouTubeDlRawOptions.RemoveAsync(key);

            var result = await Mpv.YouTubeDlRawOptions.GetAsync();
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
    public async Task RemoveAsync_KeyEmpty_ThrowsException(string key)
    {
        try
        {
            Task Act() => Mpv.YouTubeDlRawOptions.RemoveAsync(key);

            await Assert.ThrowsAnyAsync<ArgumentException>(Act);
        }
        finally
        {
            Log(response);
        }
    }
}
