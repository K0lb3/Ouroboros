namespace SRPG
{
    using System;

    public class BuffBit
    {
        private static readonly int MaxBitArray;
        public int[] bits;

        static BuffBit()
        {
            MaxBitArray = (SkillParam.MAX_PARAMTYPES / 0x20) + 1;
            return;
        }

        public BuffBit()
        {
            this.bits = new int[MaxBitArray];
            base..ctor();
            return;
        }

        public bool CheckBit(ParamTypes type)
        {
            int num;
            int num2;
            int num3;
            num = type;
            num2 = num / 0x20;
            num3 = num % 0x20;
            return (((this.bits[num2] & (1 << (num3 & 0x1f))) == 0) == 0);
        }

        public bool CheckEffect()
        {
            int num;
            num = 0;
            goto Label_001A;
        Label_0007:
            if (this.bits[num] == null)
            {
                goto Label_0016;
            }
            return 1;
        Label_0016:
            num += 1;
        Label_001A:
            if (num < ((int) this.bits.Length))
            {
                goto Label_0007;
            }
            return 0;
        }

        public void Clear()
        {
            Array.Clear(this.bits, 0, (int) this.bits.Length);
            return;
        }

        public void CopyTo(BuffBit dsc)
        {
            int num;
            num = 0;
            goto Label_001B;
        Label_0007:
            dsc.bits[num] = this.bits[num];
            num += 1;
        Label_001B:
            if (num < ((int) this.bits.Length))
            {
                goto Label_0007;
            }
            return;
        }

        public unsafe void ResetBit(ParamTypes type)
        {
            int num;
            int num2;
            int num3;
            num = type;
            num2 = num / 0x20;
            num3 = num % 0x20;
            *((int*) &(this.bits[num2])) &= ~(1 << ((num3 & 0x1f) & 0x1f));
            return;
        }

        public unsafe void SetBit(ParamTypes type)
        {
            int num;
            int num2;
            int num3;
            num = type;
            num2 = num / 0x20;
            num3 = num % 0x20;
            *((int*) &(this.bits[num2])) |= 1 << ((num3 & 0x1f) & 0x1f);
            return;
        }
    }
}

