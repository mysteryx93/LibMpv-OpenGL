namespace HanumanInstitute.LibMpv;

public class MpvOption<T> : MpvPropertyWrite<T>
    where T : struct
{
    public MpvOption(MpvContext mpv, string name) : base(mpv, name)
    {
    }
}

public class MpvOptionRef<T> : MpvPropertyWriteRef<T>
    where T : class
{
    public MpvOptionRef(MpvContext mpv, string name) : base(mpv, name)
    {
    }
}

public class MpvOptionString : MpvOptionRef<string>
{
    public MpvOptionString(MpvContext mpv, string name) : base(mpv, name)
    {
    }
}
