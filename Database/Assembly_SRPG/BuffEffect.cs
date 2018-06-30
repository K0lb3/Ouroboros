namespace SRPG
{
    using GR;
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using UnityEngine;

    public class BuffEffect
    {
        public BuffEffectParam param;
        public List<BuffTarget> targets;

        public BuffEffect()
        {
            base..ctor();
            return;
        }

        public void CalcBuffStatus(ref BaseStatus status, EElement element, BuffTypes buffType, bool is_check_negative_value, bool is_negative_value_is_buff, SkillParamCalcTypes calcType, int up_count)
        {
            int num;
            BuffTarget target;
            bool flag;
            BuffMethodTypes types;
            ParamTypes types2;
            int num2;
            ParamTypes types3;
            num = 0;
            goto Label_019F;
        Label_0007:
            target = this.targets[num];
            if (target.buffType == buffType)
            {
                goto Label_0025;
            }
            goto Label_019B;
        Label_0025:
            if (is_check_negative_value == null)
            {
                goto Label_0043;
            }
            if (BuffEffectParam.IsNegativeValueIsBuff(target.paramType) == is_negative_value_is_buff)
            {
                goto Label_0043;
            }
            goto Label_019B;
        Label_0043:
            if (target.calcType == calcType)
            {
                goto Label_0055;
            }
            goto Label_019B;
        Label_0055:
            if (element == null)
            {
                goto Label_010A;
            }
            flag = 0;
            types3 = target.paramType;
            switch ((types3 - 0x13))
            {
                case 0:
                    goto Label_00B1;

                case 1:
                    goto Label_00BE;

                case 2:
                    goto Label_00CB;

                case 3:
                    goto Label_00D8;

                case 4:
                    goto Label_00E5;

                case 5:
                    goto Label_00F2;
            }
            switch ((types3 - 0x8f))
            {
                case 0:
                    goto Label_00B1;

                case 1:
                    goto Label_00BE;

                case 2:
                    goto Label_00CB;

                case 3:
                    goto Label_00D8;

                case 4:
                    goto Label_00E5;

                case 5:
                    goto Label_00F2;
            }
            goto Label_00FF;
        Label_00B1:
            flag = (element == 1) == 0;
            goto Label_00FF;
        Label_00BE:
            flag = (element == 2) == 0;
            goto Label_00FF;
        Label_00CB:
            flag = (element == 3) == 0;
            goto Label_00FF;
        Label_00D8:
            flag = (element == 4) == 0;
            goto Label_00FF;
        Label_00E5:
            flag = (element == 5) == 0;
            goto Label_00FF;
        Label_00F2:
            flag = (element == 6) == 0;
        Label_00FF:
            if (flag == null)
            {
                goto Label_010A;
            }
            goto Label_019B;
        Label_010A:
            types = this.GetBuffMethodType(target.buffType, calcType);
            types2 = target.paramType;
            num2 = target.value;
            if (this.param.mIsUpBuff == null)
            {
                goto Label_0190;
            }
            num2 = target.value_one * up_count;
            if (num2 <= 0)
            {
                goto Label_0174;
            }
            num2 = Math.Min(num2, target.value);
            goto Label_0190;
        Label_0174:
            if (num2 >= 0)
            {
                goto Label_0190;
            }
            num2 = Math.Max(num2, target.value);
        Label_0190:
            SetBuffValues(types2, types, status, num2);
        Label_019B:
            num += 1;
        Label_019F:
            if (num < this.targets.Count)
            {
                goto Label_0007;
            }
            return;
        }

        public bool CheckBuffCalcType(BuffTypes buff, SkillParamCalcTypes calc)
        {
            int num;
            num = 0;
            goto Label_003B;
        Label_0007:
            if (buff != this.targets[num].buffType)
            {
                goto Label_0037;
            }
            if (calc != this.targets[num].calcType)
            {
                goto Label_0037;
            }
            return 1;
        Label_0037:
            num += 1;
        Label_003B:
            if (num < this.targets.Count)
            {
                goto Label_0007;
            }
            return 0;
        }

        public bool CheckBuffCalcType(BuffTypes buff, SkillParamCalcTypes calc, bool is_negative_value_is_buff)
        {
            int num;
            BuffTarget target;
            num = 0;
            goto Label_0043;
        Label_0007:
            target = this.targets[num];
            if (buff != target.buffType)
            {
                goto Label_003F;
            }
            if (calc != target.calcType)
            {
                goto Label_003F;
            }
            if (BuffEffectParam.IsNegativeValueIsBuff(target.paramType) != is_negative_value_is_buff)
            {
                goto Label_003F;
            }
            return 1;
        Label_003F:
            num += 1;
        Label_0043:
            if (num < this.targets.Count)
            {
                goto Label_0007;
            }
            return 0;
        }

        private bool CheckCustomTarget(Unit target)
        {
            string str;
            string[] strArray;
            int num;
            CustomTargetParam param;
            bool flag;
            string str2;
            string[] strArray2;
            int num2;
            bool flag2;
            string str3;
            string[] strArray3;
            int num3;
            bool flag3;
            string str4;
            string[] strArray4;
            int num4;
            UnitGroupParam param2;
            bool flag4;
            string str5;
            string[] strArray5;
            int num5;
            JobGroupParam param3;
            JobData[] dataArray;
            int num6;
            strArray = this.param.custom_targets;
            num = 0;
            goto Label_037F;
        Label_0013:
            str = strArray[num];
            if (string.IsNullOrEmpty(str) == null)
            {
                goto Label_0027;
            }
            goto Label_037B;
        Label_0027:
            param = MonoSingleton<GameManager>.GetInstanceDirect().MasterParam.GetCustomTarget(str);
            if (param == null)
            {
                goto Label_037B;
            }
            if (param.units == null)
            {
                goto Label_009F;
            }
            flag = 0;
            strArray2 = param.units;
            num2 = 0;
            goto Label_0088;
        Label_005C:
            str2 = strArray2[num2];
            if ((target.UnitParam.iname == str2) == null)
            {
                goto Label_0082;
            }
            flag = 1;
            goto Label_0093;
        Label_0082:
            num2 += 1;
        Label_0088:
            if (num2 < ((int) strArray2.Length))
            {
                goto Label_005C;
            }
        Label_0093:
            if (flag != null)
            {
                goto Label_009F;
            }
            goto Label_037B;
        Label_009F:
            if (param.jobs == null)
            {
                goto Label_0110;
            }
            if (target.Job != null)
            {
                goto Label_00BA;
            }
            goto Label_037B;
        Label_00BA:
            flag2 = 0;
            strArray3 = param.jobs;
            num3 = 0;
            goto Label_00F9;
        Label_00CD:
            str3 = strArray3[num3];
            if ((target.Job.JobID == str3) == null)
            {
                goto Label_00F3;
            }
            flag2 = 1;
            goto Label_0104;
        Label_00F3:
            num3 += 1;
        Label_00F9:
            if (num3 < ((int) strArray3.Length))
            {
                goto Label_00CD;
            }
        Label_0104:
            if (flag2 != null)
            {
                goto Label_0110;
            }
            goto Label_037B;
        Label_0110:
            if (param.unit_groups == null)
            {
                goto Label_019A;
            }
            flag3 = 0;
            strArray4 = param.unit_groups;
            num4 = 0;
            goto Label_0183;
        Label_012E:
            str4 = strArray4[num4];
            param2 = MonoSingleton<GameManager>.GetInstanceDirect().MasterParam.GetUnitGroup(str4);
            if (param2 != null)
            {
                goto Label_015E;
            }
            Debug.LogWarning("存在しないユニットグループ識別子が設定されている : CustomTarget");
            goto Label_017D;
        Label_015E:
            if (param2.IsInGroup(target.UnitParam.iname) == null)
            {
                goto Label_017D;
            }
            flag3 = 1;
            goto Label_018E;
        Label_017D:
            num4 += 1;
        Label_0183:
            if (num4 < ((int) strArray4.Length))
            {
                goto Label_012E;
            }
        Label_018E:
            if (flag3 != null)
            {
                goto Label_019A;
            }
            goto Label_037B;
        Label_019A:
            if (param.job_groups == null)
            {
                goto Label_0234;
            }
            if (target.Job != null)
            {
                goto Label_01B5;
            }
            goto Label_037B;
        Label_01B5:
            flag4 = 0;
            strArray5 = param.job_groups;
            num5 = 0;
            goto Label_021D;
        Label_01C8:
            str5 = strArray5[num5];
            param3 = MonoSingleton<GameManager>.GetInstanceDirect().MasterParam.GetJobGroup(str5);
            if (param3 != null)
            {
                goto Label_01F8;
            }
            Debug.LogWarning("存在しないジョブグループ識別子が設定されている : CustomTarget");
            goto Label_0217;
        Label_01F8:
            if (param3.IsInGroup(target.Job.JobID) == null)
            {
                goto Label_0217;
            }
            flag4 = 1;
            goto Label_0228;
        Label_0217:
            num5 += 1;
        Label_021D:
            if (num5 < ((int) strArray5.Length))
            {
                goto Label_01C8;
            }
        Label_0228:
            if (flag4 != null)
            {
                goto Label_0234;
            }
            goto Label_037B;
        Label_0234:
            dataArray = target.UnitData.Jobs;
            if (string.IsNullOrEmpty(param.first_job) != null)
            {
                goto Label_0280;
            }
            if (dataArray == null)
            {
                goto Label_037B;
            }
            if (((int) dataArray.Length) < 1)
            {
                goto Label_037B;
            }
            if ((dataArray[0].JobID != param.first_job) == null)
            {
                goto Label_0280;
            }
            goto Label_037B;
        Label_0280:
            if (string.IsNullOrEmpty(param.second_job) != null)
            {
                goto Label_02BF;
            }
            if (dataArray == null)
            {
                goto Label_037B;
            }
            if (((int) dataArray.Length) < 2)
            {
                goto Label_037B;
            }
            if ((dataArray[1].JobID != param.second_job) == null)
            {
                goto Label_02BF;
            }
            goto Label_037B;
        Label_02BF:
            if (string.IsNullOrEmpty(param.third_job) != null)
            {
                goto Label_02FE;
            }
            if (dataArray == null)
            {
                goto Label_037B;
            }
            if (((int) dataArray.Length) < 3)
            {
                goto Label_037B;
            }
            if ((dataArray[2].JobID != param.third_job) == null)
            {
                goto Label_02FE;
            }
            goto Label_037B;
        Label_02FE:
            if (param.sex == null)
            {
                goto Label_0324;
            }
            if (param.sex == target.UnitParam.sex)
            {
                goto Label_0324;
            }
            goto Label_037B;
        Label_0324:
            if (param.birth_id == null)
            {
                goto Label_034A;
            }
            if (param.birth_id == target.UnitParam.birthID)
            {
                goto Label_034A;
            }
            goto Label_037B;
        Label_034A:
            if (param.element == null)
            {
                goto Label_0379;
            }
            num6 = 1 << ((target.Element - 1) & 0x1f);
            if ((param.element & num6) == num6)
            {
                goto Label_0379;
            }
            goto Label_037B;
        Label_0379:
            return 1;
        Label_037B:
            num += 1;
        Label_037F:
            if (num < ((int) strArray.Length))
            {
                goto Label_0013;
            }
            return 0;
        }

        public bool CheckEnableBuffTarget(Unit target)
        {
            bool flag;
            int num;
            UnitGroupParam param;
            if (this.param != null)
            {
                goto Label_000D;
            }
            return 0;
        Label_000D:
            flag = 1;
            if (this.param.sex == null)
            {
                goto Label_003A;
            }
            flag &= this.param.sex == target.UnitParam.sex;
        Label_003A:
            if (this.param.elem == null)
            {
                goto Label_006B;
            }
            num = 1 << ((target.Element - 1) & 0x1f);
            flag &= (this.param.elem & num) == num;
        Label_006B:
            if (string.IsNullOrEmpty(this.param.job) != null)
            {
                goto Label_00AE;
            }
            if (target.Job == null)
            {
                goto Label_00AE;
            }
            flag &= this.param.job == target.Job.Param.origin;
        Label_00AE:
            if (string.IsNullOrEmpty(this.param.buki) != null)
            {
                goto Label_00F1;
            }
            if (target.Job == null)
            {
                goto Label_00F1;
            }
            flag &= this.param.job == target.Job.Param.buki;
        Label_00F1:
            if (string.IsNullOrEmpty(this.param.birth) != null)
            {
                goto Label_0129;
            }
            flag &= this.param.birth == target.UnitParam.birth;
        Label_0129:
            if (string.IsNullOrEmpty(this.param.un_group) != null)
            {
                goto Label_0173;
            }
            param = MonoSingleton<GameManager>.GetInstanceDirect().MasterParam.GetUnitGroup(this.param.un_group);
            if (param == null)
            {
                goto Label_0173;
            }
            flag &= param.IsInGroup(target.UnitParam.iname);
        Label_0173:
            if (this.param.custom_targets == null)
            {
                goto Label_019E;
            }
            if (this.param.cond != 4)
            {
                goto Label_019E;
            }
            flag &= this.CheckCustomTarget(target);
        Label_019E:
            return flag;
        }

        private void Clear()
        {
            this.param = null;
            this.targets = null;
            return;
        }

        public static BuffEffect CreateBuffEffect(BuffEffectParam param, int rank, int rankcap)
        {
            BuffEffect effect;
            if (param == null)
            {
                goto Label_001E;
            }
            if (param.buffs == null)
            {
                goto Label_001E;
            }
            if (((int) param.buffs.Length) != null)
            {
                goto Label_0020;
            }
        Label_001E:
            return null;
        Label_0020:
            effect = new BuffEffect();
            effect.param = param;
            effect.targets = new List<BuffTarget>((int) param.buffs.Length);
            effect.UpdateCurrentValues(rank, rankcap);
            return effect;
        }

        public unsafe void GetBaseStatus(ref BaseStatus total_add, ref BaseStatus total_scale)
        {
            BaseStatus status;
            BaseStatus status2;
            BaseStatus status3;
            BaseStatus status4;
            BaseStatus status5;
            BaseStatus status6;
            if (*(total_add) == null)
            {
                goto Label_000E;
            }
            if (*(total_scale) != null)
            {
                goto Label_000F;
            }
        Label_000E:
            return;
        Label_000F:
            *(total_add).Clear();
            *(total_scale).Clear();
            status = new BaseStatus();
            status2 = new BaseStatus();
            status3 = new BaseStatus();
            status4 = new BaseStatus();
            status5 = new BaseStatus();
            status6 = new BaseStatus();
            this.CalcBuffStatus(&status, 0, 0, 1, 0, 0, 0);
            this.CalcBuffStatus(&status2, 0, 0, 1, 1, 0, 0);
            this.CalcBuffStatus(&status3, 0, 0, 0, 0, 1, 0);
            this.CalcBuffStatus(&status4, 0, 1, 1, 0, 0, 0);
            this.CalcBuffStatus(&status5, 0, 1, 1, 1, 0, 0);
            this.CalcBuffStatus(&status6, 0, 1, 0, 0, 1, 0);
            *(total_add).Add(status);
            *(total_add).Add(status2);
            *(total_add).Add(status4);
            *(total_add).Add(status5);
            *(total_scale).Add(status3);
            *(total_scale).Add(status6);
            return;
        }

        private BuffMethodTypes GetBuffMethodType(BuffTypes buff, SkillParamCalcTypes calc)
        {
            if (calc == 1)
            {
                goto Label_0009;
            }
            return 0;
        Label_0009:
            return ((buff != null) ? 2 : 1);
        }

        private int GetRankValue(int rank, int rankcap, int ini, int max)
        {
            int num;
            int num2;
            int num3;
            int num4;
            num = rankcap - 1;
            num2 = rank - 1;
            if (ini != max)
            {
                goto Label_0012;
            }
            return ini;
        Label_0012:
            if (num2 < 1)
            {
                goto Label_0020;
            }
            if (num >= 1)
            {
                goto Label_0022;
            }
        Label_0020:
            return ini;
        Label_0022:
            if (num2 < num)
            {
                goto Label_002C;
            }
            return max;
        Label_002C:
            num3 = ((max - ini) * 100) / num;
            num4 = ini + ((num3 * num2) / 100);
            return num4;
        }

        private static void SetBuffValue(BuffMethodTypes type, ref OInt param, int value)
        {
            BuffMethodTypes types;
            types = type;
            switch (types)
            {
                case 0:
                    goto Label_0019;

                case 1:
                    goto Label_0036;

                case 2:
                    goto Label_0058;
            }
            goto Label_007A;
        Label_0019:
            *(param) += value;
            goto Label_007A;
        Label_0036:
            if (*(param) >= value)
            {
                goto Label_007A;
            }
            *(param) = value;
            goto Label_007A;
        Label_0058:
            if (*(param) <= value)
            {
                goto Label_007A;
            }
            *(param) = value;
        Label_007A:
            return;
        }

        private static void SetBuffValue(BuffMethodTypes type, ref OShort param, int value)
        {
            BuffMethodTypes types;
            types = type;
            switch (types)
            {
                case 0:
                    goto Label_0019;

                case 1:
                    goto Label_0036;

                case 2:
                    goto Label_0058;
            }
            goto Label_007A;
        Label_0019:
            *(param) += value;
            goto Label_007A;
        Label_0036:
            if (*(param) >= value)
            {
                goto Label_007A;
            }
            *(param) = value;
            goto Label_007A;
        Label_0058:
            if (*(param) <= value)
            {
                goto Label_007A;
            }
            *(param) = value;
        Label_007A:
            return;
        }

        public static unsafe void SetBuffValues(ParamTypes param_type, BuffMethodTypes method_type, ref BaseStatus status, int value)
        {
            ParamTypes types;
            types = param_type;
            switch (types)
            {
                case 0:
                    goto Label_17E3;

                case 1:
                    goto Label_0271;

                case 2:
                    goto Label_0289;

                case 3:
                    goto Label_02A1;

                case 4:
                    goto Label_02BF;

                case 5:
                    goto Label_02DD;

                case 6:
                    goto Label_02FB;

                case 7:
                    goto Label_0319;

                case 8:
                    goto Label_0337;

                case 9:
                    goto Label_0355;

                case 10:
                    goto Label_0373;

                case 11:
                    goto Label_0391;

                case 12:
                    goto Label_03AF;

                case 13:
                    goto Label_03CE;

                case 14:
                    goto Label_03ED;

                case 15:
                    goto Label_040C;

                case 0x10:
                    goto Label_042B;

                case 0x11:
                    goto Label_0449;

                case 0x12:
                    goto Label_0467;

                case 0x13:
                    goto Label_0485;

                case 20:
                    goto Label_04A3;

                case 0x15:
                    goto Label_04C1;

                case 0x16:
                    goto Label_04DF;

                case 0x17:
                    goto Label_04FD;

                case 0x18:
                    goto Label_051B;

                case 0x19:
                    goto Label_0539;

                case 0x1a:
                    goto Label_0557;

                case 0x1b:
                    goto Label_0575;

                case 0x1c:
                    goto Label_0593;

                case 0x1d:
                    goto Label_05B1;

                case 30:
                    goto Label_05CF;

                case 0x1f:
                    goto Label_05ED;

                case 0x20:
                    goto Label_060B;

                case 0x21:
                    goto Label_0629;

                case 0x22:
                    goto Label_0647;

                case 0x23:
                    goto Label_0666;

                case 0x24:
                    goto Label_0685;

                case 0x25:
                    goto Label_06A4;

                case 0x26:
                    goto Label_06C3;

                case 0x27:
                    goto Label_06E2;

                case 40:
                    goto Label_0701;

                case 0x29:
                    goto Label_0720;

                case 0x2a:
                    goto Label_073F;

                case 0x2b:
                    goto Label_075E;

                case 0x2c:
                    goto Label_077D;

                case 0x2d:
                    goto Label_079C;

                case 0x2e:
                    goto Label_07BB;

                case 0x2f:
                    goto Label_07DA;

                case 0x30:
                    goto Label_0837;

                case 0x31:
                    goto Label_0A07;

                case 50:
                    goto Label_0A25;

                case 0x33:
                    goto Label_0A43;

                case 0x34:
                    goto Label_0A61;

                case 0x35:
                    goto Label_0A7F;

                case 0x36:
                    goto Label_0A9D;

                case 0x37:
                    goto Label_0ABB;

                case 0x38:
                    goto Label_0AD9;

                case 0x39:
                    goto Label_0AF7;

                case 0x3a:
                    goto Label_0B15;

                case 0x3b:
                    goto Label_0B33;

                case 60:
                    goto Label_0B51;

                case 0x3d:
                    goto Label_0B6F;

                case 0x3e:
                    goto Label_0B8D;

                case 0x3f:
                    goto Label_0BAB;

                case 0x40:
                    goto Label_0BC9;

                case 0x41:
                    goto Label_0BE8;

                case 0x42:
                    goto Label_0C07;

                case 0x43:
                    goto Label_0C26;

                case 0x44:
                    goto Label_0C45;

                case 0x45:
                    goto Label_0C64;

                case 70:
                    goto Label_0C83;

                case 0x47:
                    goto Label_0CA2;

                case 0x48:
                    goto Label_0CC1;

                case 0x49:
                    goto Label_0CE0;

                case 0x4a:
                    goto Label_0CFF;

                case 0x4b:
                    goto Label_0D1E;

                case 0x4c:
                    goto Label_0D3D;

                case 0x4d:
                    goto Label_0D5C;

                case 0x4e:
                    goto Label_0DB9;

                case 0x4f:
                    goto Label_0F6F;

                case 80:
                    goto Label_0F8D;

                case 0x51:
                    goto Label_0FAB;

                case 0x52:
                    goto Label_0FC9;

                case 0x53:
                    goto Label_0FE8;

                case 0x54:
                    goto Label_1007;

                case 0x55:
                    goto Label_1026;

                case 0x56:
                    goto Label_1044;

                case 0x57:
                    goto Label_1062;

                case 0x58:
                    goto Label_1080;

                case 0x59:
                    goto Label_109F;

                case 90:
                    goto Label_10BE;

                case 0x5b:
                    goto Label_10DD;

                case 0x5c:
                    goto Label_10FC;

                case 0x5d:
                    goto Label_111B;

                case 0x5e:
                    goto Label_113A;

                case 0x5f:
                    goto Label_1159;

                case 0x60:
                    goto Label_1178;

                case 0x61:
                    goto Label_1197;

                case 0x62:
                    goto Label_11B6;

                case 0x63:
                    goto Label_11D5;

                case 100:
                    goto Label_11F4;

                case 0x65:
                    goto Label_1213;

                case 0x66:
                    goto Label_1232;

                case 0x67:
                    goto Label_07F9;

                case 0x68:
                    goto Label_0D7B;

                case 0x69:
                    goto Label_0818;

                case 0x6a:
                    goto Label_0D9A;

                case 0x6b:
                    goto Label_1251;

                case 0x6c:
                    goto Label_1270;

                case 0x6d:
                    goto Label_128F;

                case 110:
                    goto Label_12AE;

                case 0x6f:
                    goto Label_12CD;

                case 0x70:
                    goto Label_12EC;

                case 0x71:
                    goto Label_130B;

                case 0x72:
                    goto Label_132A;

                case 0x73:
                    goto Label_1349;

                case 0x74:
                    goto Label_1368;

                case 0x75:
                    goto Label_1387;

                case 0x76:
                    goto Label_13A6;

                case 0x77:
                    goto Label_13C5;

                case 120:
                    goto Label_13E4;

                case 0x79:
                    goto Label_1403;

                case 0x7a:
                    goto Label_1422;

                case 0x7b:
                    goto Label_1441;

                case 0x7c:
                    goto Label_1460;

                case 0x7d:
                    goto Label_147F;

                case 0x7e:
                    goto Label_149E;

                case 0x7f:
                    goto Label_14BD;

                case 0x80:
                    goto Label_14DC;

                case 0x81:
                    goto Label_14FB;

                case 130:
                    goto Label_151A;

                case 0x83:
                    goto Label_1539;

                case 0x84:
                    goto Label_1558;

                case 0x85:
                    goto Label_1577;

                case 0x86:
                    goto Label_1596;

                case 0x87:
                    goto Label_15B5;

                case 0x88:
                    goto Label_15D4;

                case 0x89:
                    goto Label_15F3;

                case 0x8a:
                    goto Label_1612;

                case 0x8b:
                    goto Label_1631;

                case 140:
                    goto Label_1650;

                case 0x8d:
                    goto Label_166F;

                case 0x8e:
                    goto Label_168E;

                case 0x8f:
                    goto Label_16AD;

                case 0x90:
                    goto Label_16CC;

                case 0x91:
                    goto Label_16EB;

                case 0x92:
                    goto Label_170A;

                case 0x93:
                    goto Label_1729;

                case 0x94:
                    goto Label_1748;

                case 0x95:
                    goto Label_1767;

                case 150:
                    goto Label_1786;

                case 0x97:
                    goto Label_17A5;

                case 0x98:
                    goto Label_17C4;
            }
            goto Label_17E3;
        Label_0271:
            SetBuffValue(method_type, &*(status).param.values_hp, value);
            goto Label_17E8;
        Label_0289:
            SetBuffValue(method_type, &*(status).param.values_hp, value);
            goto Label_17E8;
        Label_02A1:
            SetBuffValue(method_type, &(*(status).param.values[0]), value);
            goto Label_17E8;
        Label_02BF:
            SetBuffValue(method_type, &(*(status).param.values[1]), value);
            goto Label_17E8;
        Label_02DD:
            SetBuffValue(method_type, &(*(status).param.values[2]), value);
            goto Label_17E8;
        Label_02FB:
            SetBuffValue(method_type, &(*(status).param.values[3]), value);
            goto Label_17E8;
        Label_0319:
            SetBuffValue(method_type, &(*(status).param.values[4]), value);
            goto Label_17E8;
        Label_0337:
            SetBuffValue(method_type, &(*(status).param.values[5]), value);
            goto Label_17E8;
        Label_0355:
            SetBuffValue(method_type, &(*(status).param.values[6]), value);
            goto Label_17E8;
        Label_0373:
            SetBuffValue(method_type, &(*(status).param.values[7]), value);
            goto Label_17E8;
        Label_0391:
            SetBuffValue(method_type, &(*(status).param.values[8]), value);
            goto Label_17E8;
        Label_03AF:
            SetBuffValue(method_type, &(*(status).param.values[9]), value);
            goto Label_17E8;
        Label_03CE:
            SetBuffValue(method_type, &(*(status).param.values[10]), value);
            goto Label_17E8;
        Label_03ED:
            SetBuffValue(method_type, &(*(status).param.values[11]), value);
            goto Label_17E8;
        Label_040C:
            SetBuffValue(method_type, &(*(status).param.values[12]), value);
            goto Label_17E8;
        Label_042B:
            SetBuffValue(method_type, &(*(status).bonus.values[0]), value);
            goto Label_17E8;
        Label_0449:
            SetBuffValue(method_type, &(*(status).bonus.values[1]), value);
            goto Label_17E8;
        Label_0467:
            SetBuffValue(method_type, &(*(status).bonus.values[2]), value);
            goto Label_17E8;
        Label_0485:
            SetBuffValue(method_type, &(*(status).element_assist.values[1]), value);
            goto Label_17E8;
        Label_04A3:
            SetBuffValue(method_type, &(*(status).element_assist.values[2]), value);
            goto Label_17E8;
        Label_04C1:
            SetBuffValue(method_type, &(*(status).element_assist.values[3]), value);
            goto Label_17E8;
        Label_04DF:
            SetBuffValue(method_type, &(*(status).element_assist.values[4]), value);
            goto Label_17E8;
        Label_04FD:
            SetBuffValue(method_type, &(*(status).element_assist.values[5]), value);
            goto Label_17E8;
        Label_051B:
            SetBuffValue(method_type, &(*(status).element_assist.values[6]), value);
            goto Label_17E8;
        Label_0539:
            SetBuffValue(method_type, &(*(status).enchant_assist.values[0]), value);
            goto Label_17E8;
        Label_0557:
            SetBuffValue(method_type, &(*(status).enchant_assist.values[1]), value);
            goto Label_17E8;
        Label_0575:
            SetBuffValue(method_type, &(*(status).enchant_assist.values[2]), value);
            goto Label_17E8;
        Label_0593:
            SetBuffValue(method_type, &(*(status).enchant_assist.values[3]), value);
            goto Label_17E8;
        Label_05B1:
            SetBuffValue(method_type, &(*(status).enchant_assist.values[4]), value);
            goto Label_17E8;
        Label_05CF:
            SetBuffValue(method_type, &(*(status).enchant_assist.values[5]), value);
            goto Label_17E8;
        Label_05ED:
            SetBuffValue(method_type, &(*(status).enchant_assist.values[6]), value);
            goto Label_17E8;
        Label_060B:
            SetBuffValue(method_type, &(*(status).enchant_assist.values[7]), value);
            goto Label_17E8;
        Label_0629:
            SetBuffValue(method_type, &(*(status).enchant_assist.values[8]), value);
            goto Label_17E8;
        Label_0647:
            SetBuffValue(method_type, &(*(status).enchant_assist.values[9]), value);
            goto Label_17E8;
        Label_0666:
            SetBuffValue(method_type, &(*(status).enchant_assist.values[10]), value);
            goto Label_17E8;
        Label_0685:
            SetBuffValue(method_type, &(*(status).enchant_assist.values[11]), value);
            goto Label_17E8;
        Label_06A4:
            SetBuffValue(method_type, &(*(status).enchant_assist.values[12]), value);
            goto Label_17E8;
        Label_06C3:
            SetBuffValue(method_type, &(*(status).enchant_assist.values[13]), value);
            goto Label_17E8;
        Label_06E2:
            SetBuffValue(method_type, &(*(status).enchant_assist.values[14]), value);
            goto Label_17E8;
        Label_0701:
            SetBuffValue(method_type, &(*(status).enchant_assist.values[15]), value);
            goto Label_17E8;
        Label_0720:
            SetBuffValue(method_type, &(*(status).enchant_assist.values[0x10]), value);
            goto Label_17E8;
        Label_073F:
            SetBuffValue(method_type, &(*(status).enchant_assist.values[0x11]), value);
            goto Label_17E8;
        Label_075E:
            SetBuffValue(method_type, &(*(status).enchant_assist.values[0x12]), value);
            goto Label_17E8;
        Label_077D:
            SetBuffValue(method_type, &(*(status).enchant_assist.values[0x13]), value);
            goto Label_17E8;
        Label_079C:
            SetBuffValue(method_type, &(*(status).enchant_assist.values[20]), value);
            goto Label_17E8;
        Label_07BB:
            SetBuffValue(method_type, &(*(status).enchant_assist.values[0x15]), value);
            goto Label_17E8;
        Label_07DA:
            SetBuffValue(method_type, &(*(status).enchant_assist.values[0x16]), value);
            goto Label_17E8;
        Label_07F9:
            SetBuffValue(method_type, &(*(status).enchant_assist.values[0x17]), value);
            goto Label_17E8;
        Label_0818:
            SetBuffValue(method_type, &(*(status).enchant_assist.values[0x18]), value);
            goto Label_17E8;
        Label_0837:
            SetBuffValue(method_type, &(*(status).enchant_assist.values[0]), value);
            SetBuffValue(method_type, &(*(status).enchant_assist.values[1]), value);
            SetBuffValue(method_type, &(*(status).enchant_assist.values[2]), value);
            SetBuffValue(method_type, &(*(status).enchant_assist.values[3]), value);
            SetBuffValue(method_type, &(*(status).enchant_assist.values[4]), value);
            SetBuffValue(method_type, &(*(status).enchant_assist.values[5]), value);
            SetBuffValue(method_type, &(*(status).enchant_assist.values[6]), value);
            SetBuffValue(method_type, &(*(status).enchant_assist.values[7]), value);
            SetBuffValue(method_type, &(*(status).enchant_assist.values[8]), value);
            SetBuffValue(method_type, &(*(status).enchant_assist.values[9]), value);
            SetBuffValue(method_type, &(*(status).enchant_assist.values[11]), value);
            SetBuffValue(method_type, &(*(status).enchant_assist.values[12]), value);
            SetBuffValue(method_type, &(*(status).enchant_assist.values[0x10]), value);
            SetBuffValue(method_type, &(*(status).enchant_assist.values[0x11]), value);
            SetBuffValue(method_type, &(*(status).enchant_assist.values[0x12]), value);
            SetBuffValue(method_type, &(*(status).enchant_assist.values[20]), value);
            SetBuffValue(method_type, &(*(status).enchant_assist.values[0x15]), value);
            SetBuffValue(method_type, &(*(status).enchant_assist.values[0x18]), value);
            goto Label_17E8;
        Label_0A07:
            SetBuffValue(method_type, &(*(status).element_resist.values[1]), value);
            goto Label_17E8;
        Label_0A25:
            SetBuffValue(method_type, &(*(status).element_resist.values[2]), value);
            goto Label_17E8;
        Label_0A43:
            SetBuffValue(method_type, &(*(status).element_resist.values[3]), value);
            goto Label_17E8;
        Label_0A61:
            SetBuffValue(method_type, &(*(status).element_resist.values[4]), value);
            goto Label_17E8;
        Label_0A7F:
            SetBuffValue(method_type, &(*(status).element_resist.values[5]), value);
            goto Label_17E8;
        Label_0A9D:
            SetBuffValue(method_type, &(*(status).element_resist.values[6]), value);
            goto Label_17E8;
        Label_0ABB:
            SetBuffValue(method_type, &(*(status).enchant_resist.values[0]), value);
            goto Label_17E8;
        Label_0AD9:
            SetBuffValue(method_type, &(*(status).enchant_resist.values[1]), value);
            goto Label_17E8;
        Label_0AF7:
            SetBuffValue(method_type, &(*(status).enchant_resist.values[2]), value);
            goto Label_17E8;
        Label_0B15:
            SetBuffValue(method_type, &(*(status).enchant_resist.values[3]), value);
            goto Label_17E8;
        Label_0B33:
            SetBuffValue(method_type, &(*(status).enchant_resist.values[4]), value);
            goto Label_17E8;
        Label_0B51:
            SetBuffValue(method_type, &(*(status).enchant_resist.values[5]), value);
            goto Label_17E8;
        Label_0B6F:
            SetBuffValue(method_type, &(*(status).enchant_resist.values[6]), value);
            goto Label_17E8;
        Label_0B8D:
            SetBuffValue(method_type, &(*(status).enchant_resist.values[7]), value);
            goto Label_17E8;
        Label_0BAB:
            SetBuffValue(method_type, &(*(status).enchant_resist.values[8]), value);
            goto Label_17E8;
        Label_0BC9:
            SetBuffValue(method_type, &(*(status).enchant_resist.values[9]), value);
            goto Label_17E8;
        Label_0BE8:
            SetBuffValue(method_type, &(*(status).enchant_resist.values[10]), value);
            goto Label_17E8;
        Label_0C07:
            SetBuffValue(method_type, &(*(status).enchant_resist.values[11]), value);
            goto Label_17E8;
        Label_0C26:
            SetBuffValue(method_type, &(*(status).enchant_resist.values[12]), value);
            goto Label_17E8;
        Label_0C45:
            SetBuffValue(method_type, &(*(status).enchant_resist.values[13]), value);
            goto Label_17E8;
        Label_0C64:
            SetBuffValue(method_type, &(*(status).enchant_resist.values[14]), value);
            goto Label_17E8;
        Label_0C83:
            SetBuffValue(method_type, &(*(status).enchant_resist.values[15]), value);
            goto Label_17E8;
        Label_0CA2:
            SetBuffValue(method_type, &(*(status).enchant_resist.values[0x10]), value);
            goto Label_17E8;
        Label_0CC1:
            SetBuffValue(method_type, &(*(status).enchant_resist.values[0x11]), value);
            goto Label_17E8;
        Label_0CE0:
            SetBuffValue(method_type, &(*(status).enchant_resist.values[0x12]), value);
            goto Label_17E8;
        Label_0CFF:
            SetBuffValue(method_type, &(*(status).enchant_resist.values[0x13]), value);
            goto Label_17E8;
        Label_0D1E:
            SetBuffValue(method_type, &(*(status).enchant_resist.values[20]), value);
            goto Label_17E8;
        Label_0D3D:
            SetBuffValue(method_type, &(*(status).enchant_resist.values[0x15]), value);
            goto Label_17E8;
        Label_0D5C:
            SetBuffValue(method_type, &(*(status).enchant_resist.values[0x16]), value);
            goto Label_17E8;
        Label_0D7B:
            SetBuffValue(method_type, &(*(status).enchant_resist.values[0x17]), value);
            goto Label_17E8;
        Label_0D9A:
            SetBuffValue(method_type, &(*(status).enchant_resist.values[0x18]), value);
            goto Label_17E8;
        Label_0DB9:
            SetBuffValue(method_type, &(*(status).enchant_resist.values[0]), value);
            SetBuffValue(method_type, &(*(status).enchant_resist.values[1]), value);
            SetBuffValue(method_type, &(*(status).enchant_resist.values[2]), value);
            SetBuffValue(method_type, &(*(status).enchant_resist.values[3]), value);
            SetBuffValue(method_type, &(*(status).enchant_resist.values[4]), value);
            SetBuffValue(method_type, &(*(status).enchant_resist.values[5]), value);
            SetBuffValue(method_type, &(*(status).enchant_resist.values[6]), value);
            SetBuffValue(method_type, &(*(status).enchant_resist.values[7]), value);
            SetBuffValue(method_type, &(*(status).enchant_resist.values[8]), value);
            SetBuffValue(method_type, &(*(status).enchant_resist.values[9]), value);
            SetBuffValue(method_type, &(*(status).enchant_resist.values[11]), value);
            SetBuffValue(method_type, &(*(status).enchant_resist.values[12]), value);
            SetBuffValue(method_type, &(*(status).enchant_resist.values[0x10]), value);
            SetBuffValue(method_type, &(*(status).enchant_resist.values[0x12]), value);
            SetBuffValue(method_type, &(*(status).enchant_resist.values[20]), value);
            SetBuffValue(method_type, &(*(status).enchant_resist.values[0x15]), value);
            SetBuffValue(method_type, &(*(status).enchant_resist.values[0x18]), value);
            goto Label_17E8;
        Label_0F6F:
            SetBuffValue(method_type, &(*(status).bonus.values[3]), value);
            goto Label_17E8;
        Label_0F8D:
            SetBuffValue(method_type, &(*(status).bonus.values[4]), value);
            goto Label_17E8;
        Label_0FAB:
            SetBuffValue(method_type, &(*(status).bonus.values[5]), value);
            goto Label_17E8;
        Label_0FC9:
            SetBuffValue(method_type, &(*(status).bonus.values[13]), value);
            goto Label_17E8;
        Label_0FE8:
            SetBuffValue(method_type, &(*(status).bonus.values[14]), value);
            goto Label_17E8;
        Label_1007:
            SetBuffValue(method_type, &(*(status).bonus.values[15]), value);
            goto Label_17E8;
        Label_1026:
            SetBuffValue(method_type, &(*(status).bonus.values[6]), value);
            goto Label_17E8;
        Label_1044:
            SetBuffValue(method_type, &(*(status).bonus.values[7]), value);
            goto Label_17E8;
        Label_1062:
            SetBuffValue(method_type, &(*(status).bonus.values[8]), value);
            goto Label_17E8;
        Label_1080:
            SetBuffValue(method_type, &(*(status).bonus.values[9]), value);
            goto Label_17E8;
        Label_109F:
            SetBuffValue(method_type, &(*(status).bonus.values[10]), value);
            goto Label_17E8;
        Label_10BE:
            SetBuffValue(method_type, &(*(status).bonus.values[11]), value);
            goto Label_17E8;
        Label_10DD:
            SetBuffValue(method_type, &(*(status).bonus.values[12]), value);
            goto Label_17E8;
        Label_10FC:
            SetBuffValue(method_type, &(*(status).bonus.values[0x10]), value);
            goto Label_17E8;
        Label_111B:
            SetBuffValue(method_type, &(*(status).bonus.values[0x11]), value);
            goto Label_17E8;
        Label_113A:
            SetBuffValue(method_type, &(*(status).bonus.values[0x12]), value);
            goto Label_17E8;
        Label_1159:
            SetBuffValue(method_type, &(*(status).bonus.values[0x13]), value);
            goto Label_17E8;
        Label_1178:
            SetBuffValue(method_type, &(*(status).bonus.values[20]), value);
            goto Label_17E8;
        Label_1197:
            SetBuffValue(method_type, &(*(status).bonus.values[0x15]), value);
            goto Label_17E8;
        Label_11B6:
            SetBuffValue(method_type, &(*(status).bonus.values[0x16]), value);
            goto Label_17E8;
        Label_11D5:
            SetBuffValue(method_type, &(*(status).bonus.values[0x17]), value);
            goto Label_17E8;
        Label_11F4:
            SetBuffValue(method_type, &(*(status).bonus.values[0x18]), value);
            goto Label_17E8;
        Label_1213:
            SetBuffValue(method_type, &(*(status).bonus.values[0x19]), value);
            goto Label_17E8;
        Label_1232:
            SetBuffValue(method_type, &(*(status).bonus.values[0x1a]), value);
            goto Label_17E8;
        Label_1251:
            SetBuffValue(method_type, &(*(status).bonus.values[0x1b]), value);
            goto Label_17E8;
        Label_1270:
            SetBuffValue(method_type, &(*(status).bonus.values[0x1c]), value);
            goto Label_17E8;
        Label_128F:
            SetBuffValue(method_type, &(*(status).bonus.values[0x1d]), value);
            goto Label_17E8;
        Label_12AE:
            SetBuffValue(method_type, &(*(status).bonus.values[30]), value);
            goto Label_17E8;
        Label_12CD:
            SetBuffValue(method_type, &(*(status).bonus.values[0x1f]), value);
            goto Label_17E8;
        Label_12EC:
            SetBuffValue(method_type, &(*(status).bonus.values[0x20]), value);
            goto Label_17E8;
        Label_130B:
            SetBuffValue(method_type, &(*(status).bonus.values[0x21]), value);
            goto Label_17E8;
        Label_132A:
            SetBuffValue(method_type, &(*(status).bonus.values[0x22]), value);
            goto Label_17E8;
        Label_1349:
            SetBuffValue(method_type, &(*(status).bonus.values[0x23]), value);
            goto Label_17E8;
        Label_1368:
            SetBuffValue(method_type, &(*(status).bonus.values[0x24]), value);
            goto Label_17E8;
        Label_1387:
            SetBuffValue(method_type, &(*(status).bonus.values[0x25]), value);
            goto Label_17E8;
        Label_13A6:
            SetBuffValue(method_type, &(*(status).bonus.values[0x26]), value);
            goto Label_17E8;
        Label_13C5:
            SetBuffValue(method_type, &(*(status).bonus.values[0x27]), value);
            goto Label_17E8;
        Label_13E4:
            SetBuffValue(method_type, &(*(status).bonus.values[40]), value);
            goto Label_17E8;
        Label_1403:
            SetBuffValue(method_type, &(*(status).bonus.values[0x29]), value);
            goto Label_17E8;
        Label_1422:
            SetBuffValue(method_type, &(*(status).bonus.values[0x2a]), value);
            goto Label_17E8;
        Label_1441:
            SetBuffValue(method_type, &(*(status).enchant_assist.values[0x19]), value);
            goto Label_17E8;
        Label_1460:
            SetBuffValue(method_type, &(*(status).enchant_assist.values[0x1a]), value);
            goto Label_17E8;
        Label_147F:
            SetBuffValue(method_type, &(*(status).enchant_resist.values[0x19]), value);
            goto Label_17E8;
        Label_149E:
            SetBuffValue(method_type, &(*(status).enchant_resist.values[0x1a]), value);
            goto Label_17E8;
        Label_14BD:
            SetBuffValue(method_type, &(*(status).enchant_assist.values[0x1b]), value);
            goto Label_17E8;
        Label_14DC:
            SetBuffValue(method_type, &(*(status).enchant_assist.values[0x1c]), value);
            goto Label_17E8;
        Label_14FB:
            SetBuffValue(method_type, &(*(status).enchant_resist.values[0x1b]), value);
            goto Label_17E8;
        Label_151A:
            SetBuffValue(method_type, &(*(status).enchant_resist.values[0x1c]), value);
            goto Label_17E8;
        Label_1539:
            SetBuffValue(method_type, &(*(status).enchant_assist.values[0x1d]), value);
            goto Label_17E8;
        Label_1558:
            SetBuffValue(method_type, &(*(status).enchant_assist.values[30]), value);
            goto Label_17E8;
        Label_1577:
            SetBuffValue(method_type, &(*(status).enchant_assist.values[0x1f]), value);
            goto Label_17E8;
        Label_1596:
            SetBuffValue(method_type, &(*(status).enchant_assist.values[0x20]), value);
            goto Label_17E8;
        Label_15B5:
            SetBuffValue(method_type, &(*(status).enchant_assist.values[0x21]), value);
            goto Label_17E8;
        Label_15D4:
            SetBuffValue(method_type, &(*(status).enchant_assist.values[0x22]), value);
            goto Label_17E8;
        Label_15F3:
            SetBuffValue(method_type, &(*(status).enchant_resist.values[0x1d]), value);
            goto Label_17E8;
        Label_1612:
            SetBuffValue(method_type, &(*(status).enchant_resist.values[30]), value);
            goto Label_17E8;
        Label_1631:
            SetBuffValue(method_type, &(*(status).enchant_resist.values[0x1f]), value);
            goto Label_17E8;
        Label_1650:
            SetBuffValue(method_type, &(*(status).enchant_resist.values[0x20]), value);
            goto Label_17E8;
        Label_166F:
            SetBuffValue(method_type, &(*(status).enchant_resist.values[0x21]), value);
            goto Label_17E8;
        Label_168E:
            SetBuffValue(method_type, &(*(status).enchant_resist.values[0x22]), value);
            goto Label_17E8;
        Label_16AD:
            SetBuffValue(method_type, &(*(status).bonus.values[0x2b]), value);
            goto Label_17E8;
        Label_16CC:
            SetBuffValue(method_type, &(*(status).bonus.values[0x2c]), value);
            goto Label_17E8;
        Label_16EB:
            SetBuffValue(method_type, &(*(status).bonus.values[0x2d]), value);
            goto Label_17E8;
        Label_170A:
            SetBuffValue(method_type, &(*(status).bonus.values[0x2e]), value);
            goto Label_17E8;
        Label_1729:
            SetBuffValue(method_type, &(*(status).bonus.values[0x2f]), value);
            goto Label_17E8;
        Label_1748:
            SetBuffValue(method_type, &(*(status).bonus.values[0x30]), value);
            goto Label_17E8;
        Label_1767:
            SetBuffValue(method_type, &(*(status).enchant_assist.values[0x23]), value);
            goto Label_17E8;
        Label_1786:
            SetBuffValue(method_type, &(*(status).enchant_assist.values[0x24]), value);
            goto Label_17E8;
        Label_17A5:
            SetBuffValue(method_type, &(*(status).enchant_resist.values[0x23]), value);
            goto Label_17E8;
        Label_17C4:
            SetBuffValue(method_type, &(*(status).enchant_resist.values[0x24]), value);
            goto Label_17E8;
        Label_17E3:;
        Label_17E8:
            return;
        }

        public void UpdateCurrentValues(int rank, int rankcap)
        {
            int num;
            int num2;
            int num3;
            int num4;
            int num5;
            if (((this.param != null) && (this.param.buffs != null)) && (((int) this.param.buffs.Length) != null))
            {
                goto Label_0034;
            }
            this.Clear();
            return;
        Label_0034:
            num = (int) this.param.buffs.Length;
            if (this.targets != null)
            {
                goto Label_0059;
            }
            this.targets = new List<BuffTarget>(num);
        Label_0059:
            if (this.targets.Count <= num)
            {
                goto Label_0098;
            }
            this.targets.RemoveRange(num, this.targets.Count - num);
            goto Label_0098;
        Label_0088:
            this.targets.Add(new BuffTarget());
        Label_0098:
            if (this.targets.Count < num)
            {
                goto Label_0088;
            }
            num2 = 0;
            goto Label_01D2;
        Label_00B0:
            num3 = this.param.buffs[num2].value_ini;
            num4 = this.param.buffs[num2].value_max;
            num5 = this.GetRankValue(rank, rankcap, num3, num4);
            this.targets[num2].value = num5;
            this.targets[num2].value_one = this.param.buffs[num2].value_one;
            this.targets[num2].calcType = this.param.buffs[num2].calc;
            this.targets[num2].paramType = this.param.buffs[num2].type;
            if (BuffEffectParam.IsNegativeValueIsBuff(this.param.buffs[num2].type) == null)
            {
                goto Label_01AE;
            }
            this.targets[num2].buffType = (num5 <= 0) ? 0 : 1;
            goto Label_01CE;
        Label_01AE:
            this.targets[num2].buffType = (num5 >= 0) ? 0 : 1;
        Label_01CE:
            num2 += 1;
        Label_01D2:
            if (num2 < num)
            {
                goto Label_00B0;
            }
            return;
        }

        public BuffTarget this[ParamTypes type]
        {
            get
            {
                <>c__AnonStorey203 storey;
                storey = new <>c__AnonStorey203();
                storey.type = type;
                return ((this.targets == null) ? null : this.targets.Find(new Predicate<BuffTarget>(storey.<>m__E7)));
            }
        }

        [CompilerGenerated]
        private sealed class <>c__AnonStorey203
        {
            internal ParamTypes type;

            public <>c__AnonStorey203()
            {
                base..ctor();
                return;
            }

            internal bool <>m__E7(BuffEffect.BuffTarget p)
            {
                return (p.paramType == this.type);
            }
        }

        public class BuffTarget
        {
            public BuffTypes buffType;
            public SkillParamCalcTypes calcType;
            public ParamTypes paramType;
            public OInt value;
            public OInt value_one;

            public BuffTarget()
            {
                base..ctor();
                return;
            }
        }

        [StructLayout(LayoutKind.Sequential, Size=1)]
        public struct BuffValues
        {
            [CompilerGenerated]
            private ParamTypes <param_type>k__BackingField;
            [CompilerGenerated]
            private BuffMethodTypes <method_type>k__BackingField;
            [CompilerGenerated]
            private int <value>k__BackingField;
            public ParamTypes param_type
            {
                [CompilerGenerated]
                get
                {
                    return this.<param_type>k__BackingField;
                }
                [CompilerGenerated]
                set
                {
                    this.<param_type>k__BackingField = value;
                    return;
                }
            }
            public BuffMethodTypes method_type
            {
                [CompilerGenerated]
                get
                {
                    return this.<method_type>k__BackingField;
                }
                [CompilerGenerated]
                set
                {
                    this.<method_type>k__BackingField = value;
                    return;
                }
            }
            public int value
            {
                [CompilerGenerated]
                get
                {
                    return this.<value>k__BackingField;
                }
                [CompilerGenerated]
                set
                {
                    this.<value>k__BackingField = value;
                    return;
                }
            }
        }
    }
}

