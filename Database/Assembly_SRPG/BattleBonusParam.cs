namespace SRPG
{
    using System;
    using System.Reflection;

    public class BattleBonusParam
    {
        public static readonly int MAX_BATTLE_BONUS;
        public OShort[] values;
        public static readonly ParamTypes[] ConvertParamTypes;

        static BattleBonusParam()
        {
            ParamTypes[] typesArray1;
            MAX_BATTLE_BONUS = (int) Enum.GetNames(typeof(BattleBonus)).Length;
            typesArray1 = new ParamTypes[] { 
                0x10, 0x11, 0x12, 0x4f, 80, 0x51, 0x55, 0x56, 0x57, 0x58, 0x59, 90, 0x5b, 0x52, 0x53, 0x54,
                0x5c, 0x5d, 0x5e, 0x5f, 0x60, 0x61, 0x62, 0x63, 100, 0x65, 0x66, 0x6b, 0x6c, 0x6d, 110, 0x6f,
                0x70, 0x71, 0x72, 0x73, 0x74, 0x75, 0x76, 0x77, 120, 0x79, 0x7a, 0x8f, 0x90, 0x91, 0x92, 0x93,
                0x94
            };
            ConvertParamTypes = typesArray1;
            return;
        }

        public BattleBonusParam()
        {
            this.values = new OShort[MAX_BATTLE_BONUS];
            base..ctor();
            return;
        }

        public unsafe void Add(BattleBonusParam src)
        {
            int num;
            if (src != null)
            {
                goto Label_0007;
            }
            return;
        Label_0007:
            num = 0;
            goto Label_004A;
        Label_000E:
            *(&(this.values[num])) += *(&(src.values[num]));
            num += 1;
        Label_004A:
            if (num < ((int) this.values.Length))
            {
                goto Label_000E;
            }
            return;
        }

        public unsafe void AddConvRate(BattleBonusParam scale, BattleBonusParam base_status)
        {
            int num;
            num = 0;
            goto Label_005D;
        Label_0007:
            *(&(this.values[num])) += (*(&(scale.values[num])) * *(&(base_status.values[num]))) / 100;
            num += 1;
        Label_005D:
            if (num < ((int) this.values.Length))
            {
                goto Label_0007;
            }
            return;
        }

        public unsafe void AddRate(BattleBonusParam src)
        {
            int num;
            num = 0;
            goto Label_005D;
        Label_0007:
            *(&(this.values[num])) += (*(&(this.values[num])) * *(&(src.values[num]))) / 100;
            num += 1;
        Label_005D:
            if (num < ((int) this.values.Length))
            {
                goto Label_0007;
            }
            return;
        }

        public unsafe void ChoiceHighest(BattleBonusParam scale, BattleBonusParam base_status)
        {
            int num;
            num = 0;
            goto Label_0089;
        Label_0007:
            if (*(&(this.values[num])) >= ((*(&(scale.values[num])) * *(&(base_status.values[num]))) / 100))
            {
                goto Label_006E;
            }
            *(&(this.values[num])) = 0;
            goto Label_0085;
        Label_006E:
            *(&(scale.values[num])) = 0;
        Label_0085:
            num += 1;
        Label_0089:
            if (num < ((int) this.values.Length))
            {
                goto Label_0007;
            }
            return;
        }

        public unsafe void ChoiceLowest(BattleBonusParam scale, BattleBonusParam base_status)
        {
            int num;
            num = 0;
            goto Label_0089;
        Label_0007:
            if (*(&(this.values[num])) <= ((*(&(scale.values[num])) * *(&(base_status.values[num]))) / 100))
            {
                goto Label_006E;
            }
            *(&(this.values[num])) = 0;
            goto Label_0085;
        Label_006E:
            *(&(scale.values[num])) = 0;
        Label_0085:
            num += 1;
        Label_0089:
            if (num < ((int) this.values.Length))
            {
                goto Label_0007;
            }
            return;
        }

        public void Clear()
        {
            Array.Clear(this.values, 0, (int) this.values.Length);
            return;
        }

        public unsafe void CopyTo(BattleBonusParam dsc)
        {
            int num;
            if (dsc != null)
            {
                goto Label_0007;
            }
            return;
        Label_0007:
            num = 0;
            goto Label_0034;
        Label_000E:
            *(&(dsc.values[num])) = *(&(this.values[num]));
            num += 1;
        Label_0034:
            if (num < ((int) this.values.Length))
            {
                goto Label_000E;
            }
            return;
        }

        public unsafe void Div(int div_val)
        {
            int num;
            if (div_val != null)
            {
                goto Label_0007;
            }
            return;
        Label_0007:
            num = 0;
            goto Label_0035;
        Label_000E:
            *(&(this.values[num])) /= div_val;
            num += 1;
        Label_0035:
            if (num < ((int) this.values.Length))
            {
                goto Label_000E;
            }
            return;
        }

        public ParamTypes GetParamTypes(int index)
        {
            return ConvertParamTypes[index];
        }

        public unsafe void Mul(int mul_val)
        {
            int num;
            if (mul_val != null)
            {
                goto Label_0007;
            }
            return;
        Label_0007:
            num = 0;
            goto Label_0035;
        Label_000E:
            *(&(this.values[num])) *= mul_val;
            num += 1;
        Label_0035:
            if (num < ((int) this.values.Length))
            {
                goto Label_000E;
            }
            return;
        }

        public unsafe void ReplceHighest(BattleBonusParam comp)
        {
            int num;
            num = 0;
            goto Label_005E;
        Label_0007:
            if (*(&(this.values[num])) >= *(&(comp.values[num])))
            {
                goto Label_005A;
            }
            *(&(this.values[num])) = *(&(comp.values[num]));
        Label_005A:
            num += 1;
        Label_005E:
            if (num < ((int) this.values.Length))
            {
                goto Label_0007;
            }
            return;
        }

        public unsafe void ReplceLowest(BattleBonusParam comp)
        {
            int num;
            num = 0;
            goto Label_005E;
        Label_0007:
            if (*(&(this.values[num])) <= *(&(comp.values[num])))
            {
                goto Label_005A;
            }
            *(&(this.values[num])) = *(&(comp.values[num]));
        Label_005A:
            num += 1;
        Label_005E:
            if (num < ((int) this.values.Length))
            {
                goto Label_0007;
            }
            return;
        }

        public unsafe void Sub(BattleBonusParam src)
        {
            int num;
            if (src != null)
            {
                goto Label_0007;
            }
            return;
        Label_0007:
            num = 0;
            goto Label_004A;
        Label_000E:
            *(&(this.values[num])) -= *(&(src.values[num]));
            num += 1;
        Label_004A:
            if (num < ((int) this.values.Length))
            {
                goto Label_000E;
            }
            return;
        }

        public unsafe void SubConvRate(BattleBonusParam scale, BattleBonusParam base_status)
        {
            int num;
            num = 0;
            goto Label_005D;
        Label_0007:
            *(&(this.values[num])) -= (*(&(scale.values[num])) * *(&(base_status.values[num]))) / 100;
            num += 1;
        Label_005D:
            if (num < ((int) this.values.Length))
            {
                goto Label_0007;
            }
            return;
        }

        public unsafe void Swap(BattleBonusParam src, bool is_rev)
        {
            int num;
            num = 0;
            goto Label_0074;
        Label_0007:
            GameUtility.swap<OShort>(&(this.values[num]), &(src.values[num]));
            if (is_rev == null)
            {
                goto Label_0070;
            }
            *(&(this.values[num])) *= -1;
            *(&(src.values[num])) *= -1;
        Label_0070:
            num += 1;
        Label_0074:
            if (num < ((int) this.values.Length))
            {
                goto Label_0007;
            }
            return;
        }

        public OShort this[BattleBonus type]
        {
            get
            {
                return *(&(this.values[type]));
            }
            set
            {
                *(&(this.values[type])) = value;
                return;
            }
        }
    }
}

