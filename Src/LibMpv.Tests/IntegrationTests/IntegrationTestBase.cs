using System;
using System.IO;
using System.Threading;
using HanumanInstitute.Validators;
using Xunit;

namespace HanumanInstitute.LibMpv.Tests.IntegrationTests;

public class IntegrationTestBase : IDisposable
{
    protected IntegrationTestBase(ITestOutputHelper output)
    {
        Output = output;
    }

    public ITestOutputHelper Output { get; set; }

    protected MpvContext Mpv { get; } = new() { LogEnabled = true };

    protected string SampleClip => Path.Combine(Environment.CurrentDirectory, "SampleClip.mp4");

    protected void LoadVideo()
    {
        Mpv.LoadFile(SampleClip).Invoke();
        Thread.Sleep(50);
    }

    protected void Log(object value) => Output.WriteLine(value.ToStringInvariant());

    // public void LogAndQuit(ITestOutputHelper? output)
    // {
    //     output?.WriteLine(Mpv.Log?.ToString() ?? string.Empty);
    //     Mpv.Quit();
    // }


    private bool _disposedValue;
    protected virtual void Dispose(bool disposing)
    {
        if (!_disposedValue)
        {
            if (disposing)
            {
                Mpv.Dispose();
            }
            _disposedValue = true;
        }
    }

    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}
