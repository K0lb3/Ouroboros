namespace SRPG
{
    using System;

    [Flags]
    public enum CriticalSections
    {
        Default = 1,
        Network = 2,
        SceneChange = 4,
        ExDownload = 8
    }
}

