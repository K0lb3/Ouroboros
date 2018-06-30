namespace SRPG
{
    using System;
    using System.Runtime.InteropServices;
    using UnityEngine;

    [Serializable, StructLayout(LayoutKind.Sequential)]
    public struct Quad
    {
        public Vector2 v0;
        public Color32 c0;
        public Vector2 v1;
        public Color32 c1;
        public Vector2 v2;
        public Color32 c2;
        public Vector2 v3;
        public Color32 c3;
    }
}

