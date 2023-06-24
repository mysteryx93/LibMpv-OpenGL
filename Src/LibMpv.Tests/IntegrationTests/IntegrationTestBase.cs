using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using HanumanInstitute.Validators;
using Xunit.Abstractions;

namespace HanumanInstitute.LibMpv.Tests.IntegrationTests;

public class IntegrationTestBase : IDisposable
{
    public IntegrationTestBase(ITestOutputHelper output)
    {
        Output = output;
    }

    public ITestOutputHelper Output { get; set; }

    public MpvContext Mpv { get; } = new() { LogEnabled = true };

    public string SampleClip => Path.Combine(Environment.CurrentDirectory, "SampleClip.mp4");

    public void LoadVideo()
    {
        Mpv.LoadFile(SampleClip).Invoke();
        Thread.Sleep(50);
    }

    public void Log(object value) => Output.WriteLine(value.ToStringInvariant());

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
