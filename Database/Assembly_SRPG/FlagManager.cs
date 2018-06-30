namespace SRPG
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct FlagManager
    {
        private int box;
        private bool Check(int id)
        {
            if (id < 0x20)
            {
                goto Label_0024;
            }
            DebugUtility.LogError("BoolManager: over is max id [" + ((int) id) + "]");
            return 0;
        Label_0024:
            return 1;
        }

        public void Set(int id, bool flag)
        {
            if (flag == null)
            {
                goto Label_0012;
            }
            this.True(id);
            goto Label_0019;
        Label_0012:
            this.False(id);
        Label_0019:
            return;
        }

        private void True(int id)
        {
            if (this.Check(id) != null)
            {
                goto Label_000D;
            }
            return;
        Label_000D:
            this.box |= 1 << ((id & 0x1f) & 0x1f);
            return;
        }

        private void False(int id)
        {
            if (this.Check(id) != null)
            {
                goto Label_000D;
            }
            return;
        Label_000D:
            this.box &= ~(1 << ((id & 0x1f) & 0x1f));
            return;
        }

        public bool Is(int id)
        {
            return (((this.box & (1 << (id & 0x1f))) == 0) == 0);
        }
    }
}

