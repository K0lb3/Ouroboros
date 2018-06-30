namespace SRPG
{
    using System;
    using System.Runtime.InteropServices;

    [Serializable, StructLayout(LayoutKind.Sequential)]
    public struct TargetID
    {
        public IDType Type;
        public string ID;
        public enum IDType
        {
            ObjectID,
            UnitID,
            ActorID
        }
    }
}

