namespace SRPG
{
    using System;
    using System.Reflection;

    public class StatusParam
    {
        public static readonly int MAX_STATUS;
        public OInt values_hp;
        public OShort[] values;
        public static readonly ParamTypes[] ConvertParamTypes;

        static StatusParam()
        {
            ParamTypes[] typesArray1;
            MAX_STATUS = (int) Enum.GetNames(typeof(StatusTypes)).Length;
            typesArray1 = new ParamTypes[] { 1, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 };
            ConvertParamTypes = typesArray1;
            return;
        }

        public StatusParam()
        {
            this.values_hp = 0;
            this.values = new OShort[MAX_STATUS - 1];
            base..ctor();
            return;
        }

        public unsafe void Add(StatusParam src)
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
            this.values_hp += src.values_hp;
            return;
        }

        public unsafe void AddConvRate(StatusParam scale, StatusParam base_status)
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
            this.values_hp += (scale.values_hp * base_status.values_hp) / 100;
            return;
        }

        public unsafe void AddRate(StatusParam src)
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
            this.values_hp += (this.values_hp * src.values_hp) / 100;
            return;
        }

        public unsafe void ApplyMinVal()
        {
            int num;
            StatusValues values;
            num = 0;
            goto Label_009A;
        Label_0007:
            if (num == 1)
            {
                goto Label_0096;
            }
            if (num != 6)
            {
                goto Label_001A;
            }
            goto Label_0096;
        Label_001A:
            values = num;
            if (values == 8)
            {
                goto Label_0028;
            }
            goto Label_005F;
        Label_0028:
            *(&(this.values[num])) = Math.Max(*(&(this.values[num])), 1);
            goto Label_0096;
        Label_005F:
            *(&(this.values[num])) = Math.Max(*(&(this.values[num])), 0);
        Label_0096:
            num += 1;
        Label_009A:
            if (num < ((int) this.values.Length))
            {
                goto Label_0007;
            }
            this.values_hp = Math.Max(this.values_hp, 1);
            return;
        }

        public unsafe void ChoiceHighest(StatusParam scale, StatusParam base_status)
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
            if (this.values_hp >= ((scale.values_hp * base_status.values_hp) / 100))
            {
                goto Label_00D2;
            }
            this.values_hp = 0;
            goto Label_00DE;
        Label_00D2:
            scale.values_hp = 0;
        Label_00DE:
            return;
        }

        public unsafe void ChoiceLowest(StatusParam scale, StatusParam base_status)
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
            if (this.values_hp <= ((scale.values_hp * base_status.values_hp) / 100))
            {
                goto Label_00D2;
            }
            this.values_hp = 0;
            goto Label_00DE;
        Label_00D2:
            scale.values_hp = 0;
        Label_00DE:
            return;
        }

        public void Clear()
        {
            this.values_hp = 0;
            Array.Clear(this.values, 0, (int) this.values.Length);
            return;
        }

        public unsafe void CopyTo(StatusParam dsc)
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
            dsc.values_hp = this.values_hp;
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
            this.values_hp /= div_val;
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
            this.values_hp *= mul_val;
            return;
        }

        public unsafe void ReplceHighest(StatusParam comp)
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
            if (this.values_hp >= comp.values_hp)
            {
                goto Label_0093;
            }
            this.values_hp = comp.values_hp;
        Label_0093:
            return;
        }

        public unsafe void ReplceLowest(StatusParam comp)
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
            if (this.values_hp <= comp.values_hp)
            {
                goto Label_0093;
            }
            this.values_hp = comp.values_hp;
        Label_0093:
            return;
        }

        public unsafe void Sub(StatusParam src)
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
            this.values_hp -= src.values_hp;
            return;
        }

        public unsafe void SubConvRate(StatusParam scale, StatusParam base_status)
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
            this.values_hp -= (scale.values_hp * base_status.values_hp) / 100;
            return;
        }

        public unsafe void Swap(StatusParam src, bool is_rev)
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
            GameUtility.swap<OInt>(&this.values_hp, &src.values_hp);
            if (is_rev == null)
            {
                goto Label_00C9;
            }
            this.values_hp *= -1;
            src.values_hp *= -1;
        Label_00C9:
            return;
        }

        public int Length
        {
            get
            {
                return MAX_STATUS;
            }
        }

        public OInt this[StatusTypes type]
        {
            get
            {
                if (type != null)
                {
                    goto Label_000D;
                }
                return this.hp;
            Label_000D:
                return *(&(this.values[type - 1]));
            }
            set
            {
                if (type != null)
                {
                    goto Label_0012;
                }
                this.hp = value;
                goto Label_002B;
            Label_0012:
                *(&(this.values[type - 1])) = value;
            Label_002B:
                return;
            }
        }

        public OInt hp
        {
            get
            {
                return this.values_hp;
            }
            set
            {
                this.values_hp = value;
                return;
            }
        }

        public OShort mp
        {
            get
            {
                return *(&(this.values[0]));
            }
            set
            {
                *(&(this.values[0])) = value;
                return;
            }
        }

        public OShort imp
        {
            get
            {
                return *(&(this.values[1]));
            }
            set
            {
                *(&(this.values[1])) = value;
                return;
            }
        }

        public OShort atk
        {
            get
            {
                return *(&(this.values[2]));
            }
            set
            {
                *(&(this.values[2])) = value;
                return;
            }
        }

        public OShort def
        {
            get
            {
                return *(&(this.values[3]));
            }
            set
            {
                *(&(this.values[3])) = value;
                return;
            }
        }

        public OShort mag
        {
            get
            {
                return *(&(this.values[4]));
            }
            set
            {
                *(&(this.values[4])) = value;
                return;
            }
        }

        public OShort mnd
        {
            get
            {
                return *(&(this.values[5]));
            }
            set
            {
                *(&(this.values[5])) = value;
                return;
            }
        }

        public OShort rec
        {
            get
            {
                return *(&(this.values[6]));
            }
            set
            {
                *(&(this.values[6])) = value;
                return;
            }
        }

        public OShort dex
        {
            get
            {
                return *(&(this.values[7]));
            }
            set
            {
                *(&(this.values[7])) = value;
                return;
            }
        }

        public OShort spd
        {
            get
            {
                return *(&(this.values[8]));
            }
            set
            {
                *(&(this.values[8])) = value;
                return;
            }
        }

        public OShort cri
        {
            get
            {
                return *(&(this.values[9]));
            }
            set
            {
                *(&(this.values[9])) = value;
                return;
            }
        }

        public OShort luk
        {
            get
            {
                return *(&(this.values[10]));
            }
            set
            {
                *(&(this.values[10])) = value;
                return;
            }
        }

        public OShort mov
        {
            get
            {
                return *(&(this.values[11]));
            }
            set
            {
                *(&(this.values[11])) = value;
                return;
            }
        }

        public OShort jmp
        {
            get
            {
                return *(&(this.values[12]));
            }
            set
            {
                *(&(this.values[12])) = value;
                return;
            }
        }

        public int total
        {
            get
            {
                int num;
                int num2;
                num = 0;
                num2 = 0;
                goto Label_0026;
            Label_0009:
                num += *(&(this.values[num2]));
                num2 += 1;
            Label_0026:
                if (num2 < ((int) this.values.Length))
                {
                    goto Label_0009;
                }
                num += this.values_hp;
                return num;
            }
        }
    }
}

