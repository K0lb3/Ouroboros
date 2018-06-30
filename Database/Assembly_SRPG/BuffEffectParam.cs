namespace SRPG
{
    using System;
    using System.Reflection;
    using System.Runtime.CompilerServices;

    public class BuffEffectParam
    {
        public string iname;
        public string job;
        public string buki;
        public string birth;
        public ESex sex;
        public string un_group;
        public int elem;
        public ESkillCondition cond;
        public OInt rate;
        public OInt turn;
        public EffectCheckTargets chk_target;
        public EffectCheckTimings chk_timing;
        public OBool mIsUpBuff;
        public EffectCheckTimings mUpTiming;
        public EAppType mAppType;
        public int mAppMct;
        public EEffRange mEffRange;
        public BuffFlags mFlags;
        public string[] custom_targets;
        public Buff[] buffs;

        public BuffEffectParam()
        {
            base..ctor();
            return;
        }

        public unsafe bool Deserialize(JSON_BuffEffectParam json)
        {
            string str;
            ParamTypes types;
            ParamTypes types2;
            ParamTypes types3;
            ParamTypes types4;
            ParamTypes types5;
            ParamTypes types6;
            ParamTypes types7;
            ParamTypes types8;
            ParamTypes types9;
            ParamTypes types10;
            ParamTypes types11;
            int num;
            int num2;
            Buff buff;
            Buff[] buffArray;
            int num3;
            int num4;
            if (json != null)
            {
                goto Label_0008;
            }
            return 0;
        Label_0008:
            this.iname = json.iname;
            this.job = json.job;
            this.buki = json.buki;
            this.birth = json.birth;
            this.sex = json.sex;
            this.un_group = json.un_group;
            str = &json.elem.ToString("d7");
            this.elem = Convert.ToInt32(str, 2);
            this.rate = json.rate;
            this.turn = json.turn;
            this.chk_target = json.chktgt;
            this.chk_timing = json.timing;
            this.cond = json.cond;
            this.mIsUpBuff = 0;
            this.mUpTiming = json.up_timing;
            this.mAppType = json.app_type;
            this.mAppMct = json.app_mct;
            this.mEffRange = json.eff_range;
            this.mFlags = 0;
            if (json.is_up_rep == null)
            {
                goto Label_0110;
            }
            this.mFlags |= 1;
        Label_0110:
            if (json.is_no_dis == null)
            {
                goto Label_0129;
            }
            this.mFlags |= 2;
        Label_0129:
            if (json.is_no_bt == null)
            {
                goto Label_0142;
            }
            this.mFlags |= 4;
        Label_0142:
            types = (short) json.type1;
            types2 = (short) json.type2;
            types3 = (short) json.type3;
            types4 = (short) json.type4;
            types5 = (short) json.type5;
            types6 = (short) json.type6;
            types7 = (short) json.type7;
            types8 = (short) json.type8;
            types9 = (short) json.type9;
            types10 = (short) json.type10;
            types11 = (short) json.type11;
            num = 0;
            if (types == null)
            {
                goto Label_01B1;
            }
            num += 1;
        Label_01B1:
            if (types2 == null)
            {
                goto Label_01BD;
            }
            num += 1;
        Label_01BD:
            if (types3 == null)
            {
                goto Label_01C9;
            }
            num += 1;
        Label_01C9:
            if (types4 == null)
            {
                goto Label_01D6;
            }
            num += 1;
        Label_01D6:
            if (types5 == null)
            {
                goto Label_01E3;
            }
            num += 1;
        Label_01E3:
            if (types6 == null)
            {
                goto Label_01F0;
            }
            num += 1;
        Label_01F0:
            if (types7 == null)
            {
                goto Label_01FD;
            }
            num += 1;
        Label_01FD:
            if (types8 == null)
            {
                goto Label_020A;
            }
            num += 1;
        Label_020A:
            if (types9 == null)
            {
                goto Label_0217;
            }
            num += 1;
        Label_0217:
            if (types10 == null)
            {
                goto Label_0224;
            }
            num += 1;
        Label_0224:
            if (types11 == null)
            {
                goto Label_0231;
            }
            num += 1;
        Label_0231:
            if (num <= 0)
            {
                goto Label_087B;
            }
            this.buffs = new Buff[num];
            num2 = 0;
            if (types == null)
            {
                goto Label_02D1;
            }
            this.buffs[num2] = new Buff();
            this.buffs[num2].type = types;
            this.buffs[num2].value_ini = json.vini1;
            this.buffs[num2].value_max = json.vmax1;
            this.buffs[num2].value_one = json.vone1;
            this.buffs[num2].calc = json.calc1;
            num2 += 1;
        Label_02D1:
            if (types2 == null)
            {
                goto Label_0359;
            }
            this.buffs[num2] = new Buff();
            this.buffs[num2].type = types2;
            this.buffs[num2].value_ini = json.vini2;
            this.buffs[num2].value_max = json.vmax2;
            this.buffs[num2].value_one = json.vone2;
            this.buffs[num2].calc = json.calc2;
            num2 += 1;
        Label_0359:
            if (types3 == null)
            {
                goto Label_03E1;
            }
            this.buffs[num2] = new Buff();
            this.buffs[num2].type = types3;
            this.buffs[num2].value_ini = json.vini3;
            this.buffs[num2].value_max = json.vmax3;
            this.buffs[num2].value_one = json.vone3;
            this.buffs[num2].calc = json.calc3;
            num2 += 1;
        Label_03E1:
            if (types4 == null)
            {
                goto Label_046B;
            }
            this.buffs[num2] = new Buff();
            this.buffs[num2].type = types4;
            this.buffs[num2].value_ini = json.vini4;
            this.buffs[num2].value_max = json.vmax4;
            this.buffs[num2].value_one = json.vone4;
            this.buffs[num2].calc = json.calc4;
            num2 += 1;
        Label_046B:
            if (types5 == null)
            {
                goto Label_04F5;
            }
            this.buffs[num2] = new Buff();
            this.buffs[num2].type = types5;
            this.buffs[num2].value_ini = json.vini5;
            this.buffs[num2].value_max = json.vmax5;
            this.buffs[num2].value_one = json.vone5;
            this.buffs[num2].calc = json.calc5;
            num2 += 1;
        Label_04F5:
            if (types6 == null)
            {
                goto Label_057F;
            }
            this.buffs[num2] = new Buff();
            this.buffs[num2].type = types6;
            this.buffs[num2].value_ini = json.vini6;
            this.buffs[num2].value_max = json.vmax6;
            this.buffs[num2].value_one = json.vone6;
            this.buffs[num2].calc = json.calc6;
            num2 += 1;
        Label_057F:
            if (types7 == null)
            {
                goto Label_0609;
            }
            this.buffs[num2] = new Buff();
            this.buffs[num2].type = types7;
            this.buffs[num2].value_ini = json.vini7;
            this.buffs[num2].value_max = json.vmax7;
            this.buffs[num2].value_one = json.vone7;
            this.buffs[num2].calc = json.calc7;
            num2 += 1;
        Label_0609:
            if (types8 == null)
            {
                goto Label_0693;
            }
            this.buffs[num2] = new Buff();
            this.buffs[num2].type = types8;
            this.buffs[num2].value_ini = json.vini8;
            this.buffs[num2].value_max = json.vmax8;
            this.buffs[num2].value_one = json.vone8;
            this.buffs[num2].calc = json.calc8;
            num2 += 1;
        Label_0693:
            if (types9 == null)
            {
                goto Label_071D;
            }
            this.buffs[num2] = new Buff();
            this.buffs[num2].type = types9;
            this.buffs[num2].value_ini = json.vini9;
            this.buffs[num2].value_max = json.vmax9;
            this.buffs[num2].value_one = json.vone9;
            this.buffs[num2].calc = json.calc9;
            num2 += 1;
        Label_071D:
            if (types10 == null)
            {
                goto Label_07A7;
            }
            this.buffs[num2] = new Buff();
            this.buffs[num2].type = types10;
            this.buffs[num2].value_ini = json.vini10;
            this.buffs[num2].value_max = json.vmax10;
            this.buffs[num2].value_one = json.vone10;
            this.buffs[num2].calc = json.calc10;
            num2 += 1;
        Label_07A7:
            if (types11 == null)
            {
                goto Label_0831;
            }
            this.buffs[num2] = new Buff();
            this.buffs[num2].type = types11;
            this.buffs[num2].value_ini = json.vini11;
            this.buffs[num2].value_max = json.vmax11;
            this.buffs[num2].value_one = json.vone11;
            this.buffs[num2].calc = json.calc11;
            num2 += 1;
        Label_0831:
            buffArray = this.buffs;
            num3 = 0;
            goto Label_0870;
        Label_0841:
            buff = buffArray[num3];
            if (buff.value_one == null)
            {
                goto Label_086A;
            }
            this.mIsUpBuff = 1;
            goto Label_087B;
        Label_086A:
            num3 += 1;
        Label_0870:
            if (num3 < ((int) buffArray.Length))
            {
                goto Label_0841;
            }
        Label_087B:
            if (json.custom_targets == null)
            {
                goto Label_08C8;
            }
            this.custom_targets = new string[(int) json.custom_targets.Length];
            num4 = 0;
            goto Label_08B9;
        Label_08A1:
            this.custom_targets[num4] = json.custom_targets[num4];
            num4 += 1;
        Label_08B9:
            if (num4 < ((int) json.custom_targets.Length))
            {
                goto Label_08A1;
            }
        Label_08C8:
            return 1;
        }

        public static bool IsNegativeValueIsBuff(ParamTypes param_type)
        {
            ParamTypes types;
            types = param_type;
            switch ((types - 0x5e))
            {
                case 0:
                    goto Label_0038;

                case 1:
                    goto Label_0038;

                case 2:
                    goto Label_0023;

                case 3:
                    goto Label_0023;

                case 4:
                    goto Label_0023;

                case 5:
                    goto Label_0038;
            }
        Label_0023:
            if (types == 0x53)
            {
                goto Label_0038;
            }
            if (types == 0x7a)
            {
                goto Label_0038;
            }
            goto Label_003A;
        Label_0038:
            return 1;
        Label_003A:
            return 0;
        }

        public Buff this[ParamTypes type]
        {
            get
            {
                <>c__AnonStorey2B7 storeyb;
                storeyb = new <>c__AnonStorey2B7();
                storeyb.type = type;
                return ((this.buffs == null) ? null : Array.Find<Buff>(this.buffs, new Predicate<Buff>(storeyb.<>m__22D)));
            }
        }

        public bool IsUpReplenish
        {
            get
            {
                return (((this.mFlags & 1) == 0) == 0);
            }
        }

        public bool IsNoDisabled
        {
            get
            {
                return (((this.mFlags & 2) == 0) == 0);
            }
        }

        public bool IsNoBuffTurn
        {
            get
            {
                return (((this.mFlags & 4) == 0) == 0);
            }
        }

        [CompilerGenerated]
        private sealed class <>c__AnonStorey2B7
        {
            internal ParamTypes type;

            public <>c__AnonStorey2B7()
            {
                base..ctor();
                return;
            }

            internal bool <>m__22D(BuffEffectParam.Buff p)
            {
                return (p.type == this.type);
            }
        }

        public class Buff
        {
            public ParamTypes type;
            public OInt value_ini;
            public OInt value_max;
            public OInt value_one;
            public SkillParamCalcTypes calc;

            public Buff()
            {
                base..ctor();
                return;
            }
        }
    }
}

