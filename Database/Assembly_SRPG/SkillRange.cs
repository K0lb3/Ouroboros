namespace SRPG
{
    using System;
    using System.Runtime.InteropServices;
    using UnityEngine;

    [StructLayout(LayoutKind.Sequential)]
    public struct SkillRange
    {
        private int w;
        private int h;
        private int[] bit;
        private int count;
        public SkillRange(int _w, int _h)
        {
            if (_w <= 0x20)
            {
                goto Label_001A;
            }
            Debug.LogError("横32以上は未対応");
            this.w = 0x20;
        Label_001A:
            this.w = _w;
            this.h = _h;
            this.bit = new int[this.h];
            this.count = 0;
            return;
        }

        public void Clear()
        {
            int num;
            num = 0;
            goto Label_0014;
        Label_0007:
            this.bit[num] = 0;
            num += 1;
        Label_0014:
            if (num < ((int) this.bit.Length))
            {
                goto Label_0007;
            }
            this.count = 0;
            return;
        }

        public unsafe void Set(int x, int y)
        {
            object[] objArray1;
            if (x < 0)
            {
                goto Label_0026;
            }
            if (y < 0)
            {
                goto Label_0026;
            }
            if (x >= this.w)
            {
                goto Label_0026;
            }
            if (y < this.h)
            {
                goto Label_0059;
            }
        Label_0026:
            objArray1 = new object[] { "failed range over > x=", (int) x, ", y=", (int) y };
            Debug.LogError(string.Concat(objArray1));
            return;
        Label_0059:
            if (this.Get(x, y) != null)
            {
                goto Label_008D;
            }
            *((int*) &(this.bit[y])) |= 1 << ((x & 0x1f) & 0x1f);
            this.count += 1;
        Label_008D:
            return;
        }

        public bool Get(int x, int y)
        {
            object[] objArray1;
            if (x < 0)
            {
                goto Label_0026;
            }
            if (y < 0)
            {
                goto Label_0026;
            }
            if (x >= this.w)
            {
                goto Label_0026;
            }
            if (y < this.h)
            {
                goto Label_005A;
            }
        Label_0026:
            objArray1 = new object[] { "failed range over > x=", (int) x, ", y=", (int) y };
            Debug.LogError(string.Concat(objArray1));
            return 0;
        Label_005A:
            return ((this.bit[y] & (1 << (x & 0x1f))) > 0);
        }

        public int Count()
        {
            return this.count;
        }
    }
}

