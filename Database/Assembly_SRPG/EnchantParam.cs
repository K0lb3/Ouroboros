namespace SRPG
{
    using System;
    using System.Reflection;

    public class EnchantParam
    {
        public static readonly int MAX_ENCHANGT;
        public OShort[] values;
        public static readonly ParamTypes[] ConvertAssistParamTypes;
        public static readonly ParamTypes[] ConvertResistParamTypes;

        static EnchantParam()
        {
            ParamTypes[] typesArray2;
            ParamTypes[] typesArray1;
            MAX_ENCHANGT = (int) Enum.GetNames(typeof(EnchantTypes)).Length;
            typesArray1 = new ParamTypes[] { 
                0x19, 0x1a, 0x1b, 0x1c, 0x1d, 30, 0x1f, 0x20, 0x21, 0x22, 0x23, 0x24, 0x25, 0x26, 0x27, 40,
                0x1b, 0x2a, 0x2b, 0x2c, 0x2d, 0x2e, 0x2f, 0x67, 0x69, 0x7b, 0x7c, 0x7f, 0x80, 0x83, 0x84, 0x85,
                0x86, 0x87, 0x88, 0x95, 150
            };
            ConvertAssistParamTypes = typesArray1;
            typesArray2 = new ParamTypes[] { 
                0x37, 0x38, 0x39, 0x3a, 0x3b, 60, 0x3d, 0x3e, 0x3f, 0x40, 0x41, 0x42, 0x43, 0x44, 0x45, 70,
                0x39, 0x48, 0x49, 0x4a, 0x4b, 0x4c, 0x4d, 0x68, 0x6a, 0x7d, 0x7e, 0x81, 130, 0x89, 0x8a, 0x8b,
                140, 0x8d, 0x8e, 0x97, 0x98
            };
            ConvertResistParamTypes = typesArray2;
            return;
        }

        public EnchantParam()
        {
            this.values = new OShort[MAX_ENCHANGT];
            base..ctor();
            return;
        }

        public unsafe void Add(EnchantParam src)
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

        public unsafe void AddConvRate(EnchantParam scale, EnchantParam base_status)
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

        public unsafe void AddRate(EnchantParam src)
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

        public unsafe void ChoiceHighest(EnchantParam scale, EnchantParam base_status)
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

        public unsafe void ChoiceLowest(EnchantParam scale, EnchantParam base_status)
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

        public unsafe void CopyTo(EnchantParam dsc)
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

        public unsafe void ReplceHighest(EnchantParam comp)
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

        public unsafe void ReplceLowest(EnchantParam comp)
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

        public unsafe void Sub(EnchantParam src)
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

        public unsafe void SubConvRate(EnchantParam scale, EnchantParam base_status)
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

        public unsafe void Swap(EnchantParam src, bool is_rev)
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

        public OShort this[EnchantTypes type]
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

        public OShort poison
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

        public OShort paralyse
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

        public OShort stun
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

        public OShort sleep
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

        public OShort charm
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

        public OShort stone
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

        public OShort blind
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

        public OShort notskl
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

        public OShort notmov
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

        public OShort notatk
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

        public OShort zombie
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

        public OShort death
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

        public OShort berserk
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

        public OShort knockback
        {
            get
            {
                return *(&(this.values[13]));
            }
            set
            {
                *(&(this.values[13])) = value;
                return;
            }
        }

        public OShort resist_buff
        {
            get
            {
                return *(&(this.values[14]));
            }
            set
            {
                *(&(this.values[14])) = value;
                return;
            }
        }

        public OShort resist_debuff
        {
            get
            {
                return *(&(this.values[15]));
            }
            set
            {
                *(&(this.values[15])) = value;
                return;
            }
        }

        public OShort stop
        {
            get
            {
                return *(&(this.values[0x10]));
            }
            set
            {
                *(&(this.values[0x10])) = value;
                return;
            }
        }

        public OShort fast
        {
            get
            {
                return *(&(this.values[0x11]));
            }
            set
            {
                *(&(this.values[0x11])) = value;
                return;
            }
        }

        public OShort slow
        {
            get
            {
                return *(&(this.values[0x12]));
            }
            set
            {
                *(&(this.values[0x12])) = value;
                return;
            }
        }

        public OShort auto_heal
        {
            get
            {
                return *(&(this.values[0x13]));
            }
            set
            {
                *(&(this.values[0x13])) = value;
                return;
            }
        }

        public OShort donsoku
        {
            get
            {
                return *(&(this.values[20]));
            }
            set
            {
                *(&(this.values[20])) = value;
                return;
            }
        }

        public OShort rage
        {
            get
            {
                return *(&(this.values[0x15]));
            }
            set
            {
                *(&(this.values[0x15])) = value;
                return;
            }
        }

        public OShort good_sleep
        {
            get
            {
                return *(&(this.values[0x16]));
            }
            set
            {
                *(&(this.values[0x16])) = value;
                return;
            }
        }

        public OShort auto_jewel
        {
            get
            {
                return *(&(this.values[0x17]));
            }
            set
            {
                *(&(this.values[0x17])) = value;
                return;
            }
        }

        public OShort notheal
        {
            get
            {
                return *(&(this.values[0x18]));
            }
            set
            {
                *(&(this.values[0x18])) = value;
                return;
            }
        }

        public OShort single_attack
        {
            get
            {
                return *(&(this.values[0x19]));
            }
            set
            {
                *(&(this.values[0x19])) = value;
                return;
            }
        }

        public OShort area_attack
        {
            get
            {
                return *(&(this.values[0x1a]));
            }
            set
            {
                *(&(this.values[0x1a])) = value;
                return;
            }
        }

        public OShort dec_ct
        {
            get
            {
                return *(&(this.values[0x1b]));
            }
            set
            {
                *(&(this.values[0x1b])) = value;
                return;
            }
        }

        public OShort inc_ct
        {
            get
            {
                return *(&(this.values[0x1c]));
            }
            set
            {
                *(&(this.values[0x1c])) = value;
                return;
            }
        }

        public OShort esa_fire
        {
            get
            {
                return *(&(this.values[0x1d]));
            }
            set
            {
                *(&(this.values[0x1d])) = value;
                return;
            }
        }

        public OShort esa_water
        {
            get
            {
                return *(&(this.values[30]));
            }
            set
            {
                *(&(this.values[30])) = value;
                return;
            }
        }

        public OShort esa_wind
        {
            get
            {
                return *(&(this.values[0x1f]));
            }
            set
            {
                *(&(this.values[0x1f])) = value;
                return;
            }
        }

        public OShort esa_thunder
        {
            get
            {
                return *(&(this.values[0x20]));
            }
            set
            {
                *(&(this.values[0x20])) = value;
                return;
            }
        }

        public OShort esa_shine
        {
            get
            {
                return *(&(this.values[0x21]));
            }
            set
            {
                *(&(this.values[0x21])) = value;
                return;
            }
        }

        public OShort esa_dark
        {
            get
            {
                return *(&(this.values[0x22]));
            }
            set
            {
                *(&(this.values[0x22])) = value;
                return;
            }
        }

        public OShort max_damage_hp
        {
            get
            {
                return *(&(this.values[0x23]));
            }
            set
            {
                *(&(this.values[0x23])) = value;
                return;
            }
        }

        public OShort max_damage_mp
        {
            get
            {
                return *(&(this.values[0x24]));
            }
            set
            {
                *(&(this.values[0x24])) = value;
                return;
            }
        }

        public OShort this[EUnitCondition condition]
        {
            get
            {
                EUnitCondition condition2;
                condition2 = condition;
                if (condition2 < 1L)
                {
                    goto Label_003C;
                }
                if (condition2 > 8L)
                {
                    goto Label_003C;
                }
                switch (((int) (condition2 - 1L)))
                {
                    case 0:
                        goto Label_0110;

                    case 1:
                        goto Label_0117;

                    case 2:
                        goto Label_003C;

                    case 3:
                        goto Label_011E;

                    case 4:
                        goto Label_003C;

                    case 5:
                        goto Label_003C;

                    case 6:
                        goto Label_003C;

                    case 7:
                        goto Label_0125;
                }
            Label_003C:
                if (condition2 == 0x10L)
                {
                    goto Label_012C;
                }
                if (condition2 == 0x20L)
                {
                    goto Label_0133;
                }
                if (condition2 == 0x40L)
                {
                    goto Label_013A;
                }
                if (condition2 == 0x80L)
                {
                    goto Label_0141;
                }
                if (condition2 == 0x100L)
                {
                    goto Label_0148;
                }
                if (condition2 == 0x200L)
                {
                    goto Label_014F;
                }
                if (condition2 == 0x400L)
                {
                    goto Label_0156;
                }
                if (condition2 == 0x800L)
                {
                    goto Label_015D;
                }
                if (condition2 == 0x1000L)
                {
                    goto Label_0164;
                }
                if (condition2 == 0x10000L)
                {
                    goto Label_016B;
                }
                if (condition2 == 0x20000L)
                {
                    goto Label_0172;
                }
                if (condition2 == 0x40000L)
                {
                    goto Label_0179;
                }
                if (condition2 == 0x80000L)
                {
                    goto Label_0180;
                }
                if (condition2 == 0x100000L)
                {
                    goto Label_0187;
                }
                if (condition2 == 0x200000L)
                {
                    goto Label_018E;
                }
                if (condition2 == 0x400000L)
                {
                    goto Label_0195;
                }
                if (condition2 == 0x800000L)
                {
                    goto Label_019C;
                }
                if (condition2 == 0x1000000L)
                {
                    goto Label_01A3;
                }
                goto Label_01AA;
            Label_0110:
                return this.poison;
            Label_0117:
                return this.paralyse;
            Label_011E:
                return this.stun;
            Label_0125:
                return this.sleep;
            Label_012C:
                return this.charm;
            Label_0133:
                return this.stone;
            Label_013A:
                return this.blind;
            Label_0141:
                return this.notskl;
            Label_0148:
                return this.notmov;
            Label_014F:
                return this.notatk;
            Label_0156:
                return this.zombie;
            Label_015D:
                return this.death;
            Label_0164:
                return this.berserk;
            Label_016B:
                return this.stop;
            Label_0172:
                return this.fast;
            Label_0179:
                return this.slow;
            Label_0180:
                return this.auto_heal;
            Label_0187:
                return this.donsoku;
            Label_018E:
                return this.rage;
            Label_0195:
                return this.good_sleep;
            Label_019C:
                return this.auto_jewel;
            Label_01A3:
                return this.notheal;
            Label_01AA:
                return 0;
            }
            set
            {
                EUnitCondition condition2;
                condition2 = condition;
                if (condition2 < 1L)
                {
                    goto Label_003C;
                }
                if (condition2 > 8L)
                {
                    goto Label_003C;
                }
                switch (((int) (condition2 - 1L)))
                {
                    case 0:
                        goto Label_0110;

                    case 1:
                        goto Label_011C;

                    case 2:
                        goto Label_003C;

                    case 3:
                        goto Label_0128;

                    case 4:
                        goto Label_003C;

                    case 5:
                        goto Label_003C;

                    case 6:
                        goto Label_003C;

                    case 7:
                        goto Label_0134;
                }
            Label_003C:
                if (condition2 == 0x10L)
                {
                    goto Label_0140;
                }
                if (condition2 == 0x20L)
                {
                    goto Label_014C;
                }
                if (condition2 == 0x40L)
                {
                    goto Label_0158;
                }
                if (condition2 == 0x80L)
                {
                    goto Label_0164;
                }
                if (condition2 == 0x100L)
                {
                    goto Label_0170;
                }
                if (condition2 == 0x200L)
                {
                    goto Label_017C;
                }
                if (condition2 == 0x400L)
                {
                    goto Label_0188;
                }
                if (condition2 == 0x800L)
                {
                    goto Label_0194;
                }
                if (condition2 == 0x1000L)
                {
                    goto Label_01A0;
                }
                if (condition2 == 0x10000L)
                {
                    goto Label_01AC;
                }
                if (condition2 == 0x20000L)
                {
                    goto Label_01B8;
                }
                if (condition2 == 0x40000L)
                {
                    goto Label_01C4;
                }
                if (condition2 == 0x80000L)
                {
                    goto Label_01D0;
                }
                if (condition2 == 0x100000L)
                {
                    goto Label_01DC;
                }
                if (condition2 == 0x200000L)
                {
                    goto Label_01E8;
                }
                if (condition2 == 0x400000L)
                {
                    goto Label_01F4;
                }
                if (condition2 == 0x800000L)
                {
                    goto Label_0200;
                }
                if (condition2 == 0x1000000L)
                {
                    goto Label_020C;
                }
                goto Label_0218;
            Label_0110:
                this.poison = value;
                goto Label_0218;
            Label_011C:
                this.paralyse = value;
                goto Label_0218;
            Label_0128:
                this.stun = value;
                goto Label_0218;
            Label_0134:
                this.sleep = value;
                goto Label_0218;
            Label_0140:
                this.charm = value;
                goto Label_0218;
            Label_014C:
                this.stone = value;
                goto Label_0218;
            Label_0158:
                this.blind = value;
                goto Label_0218;
            Label_0164:
                this.notskl = value;
                goto Label_0218;
            Label_0170:
                this.notmov = value;
                goto Label_0218;
            Label_017C:
                this.notatk = value;
                goto Label_0218;
            Label_0188:
                this.zombie = value;
                goto Label_0218;
            Label_0194:
                this.death = value;
                goto Label_0218;
            Label_01A0:
                this.berserk = value;
                goto Label_0218;
            Label_01AC:
                this.stop = value;
                goto Label_0218;
            Label_01B8:
                this.fast = value;
                goto Label_0218;
            Label_01C4:
                this.slow = value;
                goto Label_0218;
            Label_01D0:
                this.auto_heal = value;
                goto Label_0218;
            Label_01DC:
                this.donsoku = value;
                goto Label_0218;
            Label_01E8:
                this.rage = value;
                goto Label_0218;
            Label_01F4:
                this.good_sleep = value;
                goto Label_0218;
            Label_0200:
                this.auto_jewel = value;
                goto Label_0218;
            Label_020C:
                this.notheal = value;
            Label_0218:
                return;
            }
        }
    }
}

