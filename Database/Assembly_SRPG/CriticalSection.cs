namespace SRPG
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using UnityEngine;

    public static class CriticalSection
    {
        private const int NumMasks = 4;
        private static int[] mCounts;

        static CriticalSection()
        {
            mCounts = new int[0x20];
            return;
        }

        public static unsafe void Enter(CriticalSections mask)
        {
            CriticalSections sections;
            int num;
            sections = 0;
            num = 3;
            goto Label_0040;
        Label_0009:
            if ((mask & (1 << (num & 0x1f))) == null)
            {
                goto Label_003C;
            }
            *((int*) &(mCounts[num])) += 1;
            if (mCounts[num] != 1)
            {
                goto Label_003C;
            }
            sections |= 1 << (num & 0x1f);
        Label_003C:
            num -= 1;
        Label_0040:
            if (num >= 0)
            {
                goto Label_0009;
            }
            if (sections == null)
            {
                goto Label_0058;
            }
            UIValidator.UpdateValidators(sections, GetActive());
        Label_0058:
            return;
        }

        public static void ForceReset()
        {
            mCounts = new int[0x20];
            return;
        }

        public static CriticalSections GetActive()
        {
            CriticalSections sections;
            int num;
            sections = 0;
            num = 3;
            goto Label_0023;
        Label_0009:
            if (mCounts[num] <= 0)
            {
                goto Label_001F;
            }
            sections |= 1 << (num & 0x1f);
        Label_001F:
            num -= 1;
        Label_0023:
            if (num >= 0)
            {
                goto Label_0009;
            }
            return sections;
        }

        public static unsafe void Leave(CriticalSections mask)
        {
            CriticalSections sections;
            int num;
            sections = 0;
            num = 3;
            goto Label_003F;
        Label_0009:
            if ((mask & (1 << (num & 0x1f))) == null)
            {
                goto Label_003B;
            }
            *((int*) &(mCounts[num])) -= 1;
            if (mCounts[num] != null)
            {
                goto Label_003B;
            }
            sections |= 1 << (num & 0x1f);
        Label_003B:
            num -= 1;
        Label_003F:
            if (num >= 0)
            {
                goto Label_0009;
            }
            if (sections == null)
            {
                goto Label_0057;
            }
            UIValidator.UpdateValidators(sections, GetActive());
        Label_0057:
            return;
        }

        [DebuggerHidden]
        public static IEnumerator Wait()
        {
            <Wait>c__Iterator6C iteratorc;
            iteratorc = new <Wait>c__Iterator6C();
            return iteratorc;
        }

        public static bool IsActive
        {
            get
            {
                return ((GetActive() == 0) == 0);
            }
        }

        [CompilerGenerated]
        private sealed class <Wait>c__Iterator6C : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal int $PC;
            internal object $current;

            public <Wait>c__Iterator6C()
            {
                base..ctor();
                return;
            }

            [DebuggerHidden]
            public void Dispose()
            {
                this.$PC = -1;
                return;
            }

            public bool MoveNext()
            {
                uint num;
                bool flag;
                num = this.$PC;
                this.$PC = -1;
                switch (num)
                {
                    case 0:
                        goto Label_0021;

                    case 1:
                        goto Label_003D;
                }
                goto Label_004E;
            Label_0021:
                goto Label_003D;
            Label_0026:
                this.$current = new WaitForEndOfFrame();
                this.$PC = 1;
                goto Label_0050;
            Label_003D:
                if (CriticalSection.IsActive != null)
                {
                    goto Label_0026;
                }
                this.$PC = -1;
            Label_004E:
                return 0;
            Label_0050:
                return 1;
                return flag;
            }

            [DebuggerHidden]
            public void Reset()
            {
                throw new NotSupportedException();
            }

            object IEnumerator<object>.Current
            {
                [DebuggerHidden]
                get
                {
                    return this.$current;
                }
            }

            object IEnumerator.Current
            {
                [DebuggerHidden]
                get
                {
                    return this.$current;
                }
            }
        }
    }
}

