using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace HanumanInstitute.LibMpv.Tests.IntegrationTests;

public class MpvScriptOptionTests
{
    private readonly ITestOutputHelper _output;

    public MpvScriptOptionTests(ITestOutputHelper output)
    {
        _output = output;
    }

    private static object[] GetValues() => new[]
    {
        new[] { "%6%VOLume" },
        new[] { "m\"&\"n" },
        new[] { "漢字" },
        new[] { "Me, Myself & I" },
        new[] { "%1%2%3%4%5" },
        new[] { "[ABC]abc" }
    };

    [Fact]
    public async Task GetAsync_YouTubeDlTryFirst_ReturnsNull()
    {
        try
        {
            var result = await Mpv.YouTubeDlTryFirst.GetAsync();

            Assert.Null(result);
        }
        finally
        {
            Log(response);
        }
    }

    [Theory]
    [MemberData(nameof(GetValues))]
    public async Task SetAsync_YouTubeDlTryFirst_ReturnsValue(string value)
    {
        try
        {
            await Mpv.YouTubeDlTryFirst.SetAsync(value);

            var result = await Mpv.YouTubeDlTryFirst.GetAsync();
            Assert.Equal(value, result);
        }
        finally
        {
            Log(response);
        }
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public async Task SetAsync_Empty_ReturnsValue(string value)
    {
        try
        {
            await Mpv.YouTubeDlTryFirst.SetAsync(value);

            var result = await Mpv.YouTubeDlTryFirst.GetAsync();
            Assert.Null(result);
        }
        finally
        {
            Log(response);
        }
    }

    [Theory]
    [MemberData(nameof(GetValues))]
    public async Task RemoveAsync_Value_ValueReturnsNull(string value)
    {
        try
        {
            await Mpv.YouTubeDlTryFirst.SetAsync(value);
            await Mpv.YouTubeDlTryFirst.RemoveAsync();

            var result = await Mpv.YouTubeDlTryFirst.GetAsync();
            Assert.Null(result);
        }
        finally
        {
            Log(response);
        }
    }
}
