namespace SRPG
{
    using GR;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public class AbilityParam
    {
        public string iname;
        public string name;
        public string expr;
        public string icon;
        public EAbilityType type;
        public EAbilitySlot slot;
        public OInt lvcap;
        public bool is_fixed;
        public LearningSkill[] skills;
        public string[] condition_units;
        public EUseConditionsType units_conditions_type;
        public string[] condition_jobs;
        public EUseConditionsType jobs_conditions_type;
        public string condition_birth;
        public ESex condition_sex;
        public EElement condition_element;
        public OInt condition_raremin;
        public OInt condition_raremax;
        public EAbilityTypeDetail type_detail;

        public AbilityParam()
        {
            base..ctor();
            return;
        }

        public bool CheckEnableUseAbility(UnitData self, int job_index)
        {
            bool flag;
            bool flag2;
            <CheckEnableUseAbility>c__AnonStorey2B4 storeyb;
            <CheckEnableUseAbility>c__AnonStorey2B5 storeyb2;
            <CheckEnableUseAbility>c__AnonStorey2B6 storeyb3;
            storeyb = new <CheckEnableUseAbility>c__AnonStorey2B4();
            storeyb.self = self;
            if (this.condition_units == null)
            {
                goto Label_0054;
            }
            flag = (Array.Find<string>(this.condition_units, new Predicate<string>(storeyb.<>m__22A)) == null) == 0;
            if (((this.units_conditions_type != null) ? (flag == 0) : flag) != null)
            {
                goto Label_0054;
            }
            return 0;
        Label_0054:
            if (this.condition_jobs == null)
            {
                goto Label_0133;
            }
            storeyb2 = new <CheckEnableUseAbility>c__AnonStorey2B5();
            storeyb2.job = storeyb.self.GetJobData(job_index);
            if (storeyb2.job != null)
            {
                goto Label_0084;
            }
            return 0;
        Label_0084:
            flag2 = (Array.Find<string>(this.condition_jobs, new Predicate<string>(storeyb2.<>m__22B)) == null) == 0;
            if ((flag2 != null) || (string.IsNullOrEmpty(storeyb2.job.Param.origin) != null))
            {
                goto Label_0115;
            }
            storeyb3 = new <CheckEnableUseAbility>c__AnonStorey2B6();
            storeyb3.originJobParam = MonoSingleton<GameManager>.GetInstanceDirect().GetJobParam(storeyb2.job.Param.origin);
            if (storeyb3.originJobParam == null)
            {
                goto Label_0115;
            }
            flag2 = (Array.Find<string>(this.condition_jobs, new Predicate<string>(storeyb3.<>m__22C)) == null) == 0;
        Label_0115:
            if (((this.jobs_conditions_type != null) ? (flag2 == 0) : flag2) != null)
            {
                goto Label_0133;
            }
            return 0;
        Label_0133:
            if (string.IsNullOrEmpty(this.condition_birth) != null)
            {
                goto Label_016A;
            }
            if ((storeyb.self.UnitParam.birth != this.condition_birth) == null)
            {
                goto Label_016A;
            }
            return 0;
        Label_016A:
            if (this.condition_sex == null)
            {
                goto Label_0192;
            }
            if (storeyb.self.UnitParam.sex == this.condition_sex)
            {
                goto Label_0192;
            }
            return 0;
        Label_0192:
            if (this.condition_element == null)
            {
                goto Label_01B5;
            }
            if (storeyb.self.Element == this.condition_element)
            {
                goto Label_01B5;
            }
            return 0;
        Label_01B5:
            if (this.condition_raremax == null)
            {
                goto Label_0218;
            }
            if (this.condition_raremin > storeyb.self.Rarity)
            {
                goto Label_0216;
            }
            if (this.condition_raremax < storeyb.self.Rarity)
            {
                goto Label_0216;
            }
            if (this.condition_raremax >= this.condition_raremin)
            {
                goto Label_0218;
            }
        Label_0216:
            return 0;
        Label_0218:
            return 1;
        }

        public bool Deserialize(JSON_AbilityParam json)
        {
            int[] numArray1;
            string[] textArray1;
            int num;
            string[] strArray;
            int num2;
            int[] numArray;
            int num3;
            int num4;
            int num5;
            if (json != null)
            {
                goto Label_0008;
            }
            return 0;
        Label_0008:
            this.iname = json.iname;
            this.name = json.name;
            this.expr = json.expr;
            this.icon = json.icon;
            this.type = json.type;
            this.slot = json.slot;
            this.lvcap = Math.Max(json.cap, 1);
            this.is_fixed = (json.fix == 0) == 0;
            num = 0;
            textArray1 = new string[] { json.skl1, json.skl2, json.skl3, json.skl4, json.skl5, json.skl6, json.skl7, json.skl8, json.skl9, json.skl10 };
            strArray = textArray1;
            num2 = 0;
            goto Label_00FF;
        Label_00E5:
            if (string.IsNullOrEmpty(strArray[num2]) == null)
            {
                goto Label_00F7;
            }
            goto Label_0108;
        Label_00F7:
            num += 1;
            num2 += 1;
        Label_00FF:
            if (num2 < ((int) strArray.Length))
            {
                goto Label_00E5;
            }
        Label_0108:
            if (num <= 0)
            {
                goto Label_01C6;
            }
            numArray1 = new int[] { json.lv1, json.lv2, json.lv3, json.lv4, json.lv5, json.lv6, json.lv7, json.lv8, json.lv9, json.lv10 };
            numArray = numArray1;
            this.skills = new LearningSkill[num];
            num3 = 0;
            goto Label_01BE;
        Label_0186:
            this.skills[num3] = new LearningSkill();
            this.skills[num3].iname = strArray[num3];
            this.skills[num3].locklv = numArray[num3];
            num3 += 1;
        Label_01BE:
            if (num3 < num)
            {
                goto Label_0186;
            }
        Label_01C6:
            this.condition_units = null;
            if (json.units == null)
            {
                goto Label_0228;
            }
            if (((int) json.units.Length) <= 0)
            {
                goto Label_0228;
            }
            this.condition_units = new string[(int) json.units.Length];
            num4 = 0;
            goto Label_0219;
        Label_0201:
            this.condition_units[num4] = json.units[num4];
            num4 += 1;
        Label_0219:
            if (num4 < ((int) json.units.Length))
            {
                goto Label_0201;
            }
        Label_0228:
            this.units_conditions_type = json.units_cnds_type;
            this.condition_jobs = null;
            if (json.jobs == null)
            {
                goto Label_0296;
            }
            if (((int) json.jobs.Length) <= 0)
            {
                goto Label_0296;
            }
            this.condition_jobs = new string[(int) json.jobs.Length];
            num5 = 0;
            goto Label_0287;
        Label_026F:
            this.condition_jobs[num5] = json.jobs[num5];
            num5 += 1;
        Label_0287:
            if (num5 < ((int) json.jobs.Length))
            {
                goto Label_026F;
            }
        Label_0296:
            this.jobs_conditions_type = json.jobs_cnds_type;
            this.condition_birth = json.birth;
            this.condition_sex = json.sex;
            this.condition_element = json.elem;
            this.condition_raremin = json.rmin;
            this.condition_raremax = json.rmax;
            this.type_detail = json.type_detail;
            return 1;
        }

        public List<JobParam> FindConditionJobParams(MasterParam masterParam)
        {
            List<JobParam> list;
            int num;
            JobParam param;
            string str;
            GameManager manager;
            list = new List<JobParam>();
            if (this.condition_jobs == null)
            {
                goto Label_0089;
            }
            num = 0;
            goto Label_007B;
        Label_0018:
            if (string.IsNullOrEmpty(this.condition_jobs[num]) == null)
            {
                goto Label_002F;
            }
            goto Label_0077;
        Label_002F:
            param = null;
            str = this.condition_jobs[num];
            if (masterParam != null)
            {
                goto Label_0062;
            }
            manager = MonoSingleton<GameManager>.GetInstanceDirect();
            if ((manager != null) == null)
            {
                goto Label_006A;
            }
            param = manager.GetJobParam(str);
            goto Label_006A;
        Label_0062:
            param = masterParam.GetJobParam(str);
        Label_006A:
            if (param == null)
            {
                goto Label_0077;
            }
            list.Add(param);
        Label_0077:
            num += 1;
        Label_007B:
            if (num < ((int) this.condition_jobs.Length))
            {
                goto Label_0018;
            }
        Label_0089:
            return list;
        }

        public List<UnitParam> FindConditionUnitParams(MasterParam masterParam)
        {
            List<UnitParam> list;
            int num;
            UnitParam param;
            string str;
            GameManager manager;
            list = new List<UnitParam>();
            if (this.condition_units == null)
            {
                goto Label_0089;
            }
            num = 0;
            goto Label_007B;
        Label_0018:
            if (string.IsNullOrEmpty(this.condition_units[num]) == null)
            {
                goto Label_002F;
            }
            goto Label_0077;
        Label_002F:
            param = null;
            str = this.condition_units[num];
            if (masterParam != null)
            {
                goto Label_0062;
            }
            manager = MonoSingleton<GameManager>.GetInstanceDirect();
            if ((manager != null) == null)
            {
                goto Label_006A;
            }
            param = manager.GetUnitParam(str);
            goto Label_006A;
        Label_0062:
            param = masterParam.GetUnitParam(str);
        Label_006A:
            if (param == null)
            {
                goto Label_0077;
            }
            list.Add(param);
        Label_0077:
            num += 1;
        Label_007B:
            if (num < ((int) this.condition_units.Length))
            {
                goto Label_0018;
            }
        Label_0089:
            return list;
        }

        public int GetRankCap()
        {
            return this.lvcap;
        }

        public static string TypeDetailToSpriteSheetKey(EAbilityTypeDetail typeDetail)
        {
            string str;
            EAbilityTypeDetail detail;
            str = string.Empty;
            detail = typeDetail;
            switch ((detail - 1))
            {
                case 0:
                    goto Label_0035;

                case 1:
                    goto Label_0035;

                case 2:
                    goto Label_0035;

                case 3:
                    goto Label_0035;

                case 4:
                    goto Label_0040;

                case 5:
                    goto Label_0061;

                case 6:
                    goto Label_004B;

                case 7:
                    goto Label_0056;
            }
            goto Label_0061;
        Label_0035:
            str = "ABILITY_TITLE_NORMAL";
            goto Label_0066;
        Label_0040:
            str = "ABILITY_TITLE_MASTER";
            goto Label_0066;
        Label_004B:
            str = "ABILITY_TITLE_WEAPON";
            goto Label_0066;
        Label_0056:
            str = "ABILITY_TITLE_VISION";
            goto Label_0066;
        Label_0061:;
        Label_0066:
            return str;
        }

        [CompilerGenerated]
        private sealed class <CheckEnableUseAbility>c__AnonStorey2B4
        {
            internal UnitData self;

            public <CheckEnableUseAbility>c__AnonStorey2B4()
            {
                base..ctor();
                return;
            }

            internal bool <>m__22A(string p)
            {
                return (p == this.self.UnitParam.iname);
            }
        }

        [CompilerGenerated]
        private sealed class <CheckEnableUseAbility>c__AnonStorey2B5
        {
            internal JobData job;

            public <CheckEnableUseAbility>c__AnonStorey2B5()
            {
                base..ctor();
                return;
            }

            internal bool <>m__22B(string p)
            {
                return (p == this.job.JobID);
            }
        }

        [CompilerGenerated]
        private sealed class <CheckEnableUseAbility>c__AnonStorey2B6
        {
            internal JobParam originJobParam;

            public <CheckEnableUseAbility>c__AnonStorey2B6()
            {
                base..ctor();
                return;
            }

            internal bool <>m__22C(string p)
            {
                return (p == this.originJobParam.iname);
            }
        }
    }
}

