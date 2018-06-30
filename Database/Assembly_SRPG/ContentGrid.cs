namespace SRPG
{
    using System;
    using System.Runtime.InteropServices;
    using UnityEngine;

    [StructLayout(LayoutKind.Sequential)]
    public struct ContentGrid
    {
        public int x;
        public int y;
        public ContentGrid(int _ix, int _iy)
        {
            this.x = _ix;
            this.y = _iy;
            return;
        }

        public ContentGrid(float _fx, float _fy)
        {
            this.x = FloatToInt(_fx);
            this.y = FloatToInt(_fy);
            return;
        }

        public static ContentGrid zero
        {
            get
            {
                return new ContentGrid(0, 0);
            }
        }
        public float fx
        {
            set
            {
                this.x = FloatToInt(value);
                return;
            }
        }
        public float fy
        {
            set
            {
                this.y = FloatToInt(value);
                return;
            }
        }
        public static int FloatToInt(float value)
        {
            return Mathf.FloorToInt(value);
        }

        public override string ToString()
        {
            object[] objArray1;
            objArray1 = new object[] { "[Grid: ", (int) this.x, ", ", (int) this.y, "]" };
            return string.Format(string.Concat(objArray1), new object[0]);
        }
    }
}

