namespace SRPG
{
    using System;
    using System.Reflection;

    public class ElementParam
    {
        public static readonly int MAX_ELEMENT;
        public OShort[] values;
        public static readonly ParamTypes[] ConvertAssistParamTypes;
        public static readonly ParamTypes[] ConvertResistParamTypes;

        static ElementParam()
        {
            ParamTypes[] typesArray2;
            ParamTypes[] typesArray1;
            MAX_ELEMENT = (int) Enum.GetNames(typeof(EElement)).Length;
            typesArray1 = new ParamTypes[7];
            typesArray1[1] = 0x13;
            typesArray1[2] = 20;
            typesArray1[3] = 0x15;
            typesArray1[4] = 0x16;
            typesArray1[5] = 0x17;
            typesArray1[6] = 0x18;
            ConvertAssistParamTypes = typesArray1;
            typesArray2 = new ParamTypes[7];
            typesArray2[1] = 0x31;
            typesArray2[2] = 50;
            typesArray2[3] = 0x33;
            typesArray2[4] = 0x34;
            typesArray2[5] = 0x35;
            typesArray2[6] = 0x36;
            ConvertResistParamTypes = typesArray2;
            return;
        }

        public ElementParam()
        {
            this.values = new OShort[MAX_ELEMENT];
            base..ctor();
            return;
        }

        public unsafe void Add(ElementParam src)
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

        public unsafe void AddConvRate(ElementParam scale, ElementParam base_status)
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

        public unsafe void AddRate(ElementParam src)
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

        public unsafe void ChoiceHighest(ElementParam scale, ElementParam base_status)
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

        public unsafe void ChoiceLowest(ElementParam scale, ElementParam base_status)
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

        public unsafe void CopyTo(ElementParam dsc)
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

        public ParamTypes GetAssistParamTypes(int index)
        {
            return ConvertAssistParamTypes[index];
        }

        public ParamTypes GetResistParamTypes(int index)
        {
            return ConvertResistParamTypes[index];
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

        public unsafe void ReplceHighest(ElementParam comp)
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

        public unsafe void ReplceLowest(ElementParam comp)
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

        public unsafe void Sub(ElementParam src)
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

        public unsafe void SubConvRate(ElementParam scale, ElementParam base_status)
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

        public unsafe void Swap(ElementParam src, bool is_rev)
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

        public OShort this[EElement type]
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

        public OShort fire
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

        public OShort water
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

        public OShort wind
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

        public OShort thunder
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

        public OShort shine
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

        public OShort dark
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
    }
}

