namespace SRPG
{
    using GR;
    using System;
    using System.Runtime.CompilerServices;

    public class QuestClearUnlockUnitDataParam
    {
        public string iname;
        public string uid;
        public bool add;
        public EUnlockTypes type;
        public string new_id;
        public string old_id;
        public string parent_id;
        public int ulv;
        public string aid;
        public int alv;
        public string[] qids;
        public bool qcnd;

        public QuestClearUnlockUnitDataParam()
        {
            this.type = -1;
            base..ctor();
            return;
        }

        [CompilerGenerated]
        private bool <GetCondText>m__25F(LearningSkill p)
        {
            return (p.iname == ((this.add == null) ? this.old_id : this.parent_id));
        }

        public void Deserialize(JSON_QuestClearUnlockUnitDataParam json)
        {
            if (json != null)
            {
                goto Label_0007;
            }
            return;
        Label_0007:
            this.iname = json.iname;
            this.uid = json.uid;
            this.add = json.add > 0;
            this.type = json.type;
            this.new_id = json.new_id;
            this.old_id = json.old_id;
            this.parent_id = json.parent_id;
            this.ulv = json.ulv;
            this.aid = json.aid;
            this.alv = json.alv;
            this.qcnd = json.qcnd > 0;
            if (json.qids == null)
            {
                goto Label_00C1;
            }
            this.qids = new string[(int) json.qids.Length];
            json.qids.CopyTo(this.qids, 0);
        Label_00C1:
            return;
        }

        public string GetAbilityLevelCond()
        {
            object[] objArray1;
            AbilityParam param;
            if (string.IsNullOrEmpty(this.aid) != null)
            {
                goto Label_0060;
            }
            if (this.alv <= 0)
            {
                goto Label_0060;
            }
            param = MonoSingleton<GameManager>.Instance.MasterParam.GetAbilityParam(this.aid);
            if (param == null)
            {
                goto Label_0060;
            }
            objArray1 = new object[] { param.name, (int) this.alv };
            return LocalizedText.Get("sys.SKILL_UNLOCKCOND_ABILITYLV", objArray1);
        Label_0060:
            return string.Empty;
        }

        public string GetClearQuestCond()
        {
            string str;
            int num;
            QuestParam param;
            if (this.qids == null)
            {
                goto Label_007E;
            }
            str = string.Empty;
            num = 0;
            goto Label_005F;
        Label_0018:
            param = MonoSingleton<GameManager>.Instance.FindQuest(this.qids[num]);
            if (param == null)
            {
                goto Label_005B;
            }
            if (string.IsNullOrEmpty(str) != null)
            {
                goto Label_0048;
            }
            str = str + ", ";
        Label_0048:
            str = str + param.title + param.name;
        Label_005B:
            num += 1;
        Label_005F:
            if (num < ((int) this.qids.Length))
            {
                goto Label_0018;
            }
            return (LocalizedText.Get("sys.SKILL_UNLOCKCOND_CLEARQUEST") + str);
        Label_007E:
            return string.Empty;
        }

        public unsafe string GetCondText(UnitParam unit)
        {
            object[] objArray2;
            object[] objArray1;
            string str;
            string str2;
            string str3;
            string[] strArray;
            MasterParam param;
            JobParam[] paramArray;
            int num;
            JobSetParam param2;
            int num2;
            int num3;
            int num4;
            OString[] strArray2;
            int num5;
            AbilityParam param3;
            AbilityParam param4;
            SkillParam param5;
            JobParam param6;
            AbilityParam param7;
            AbilityParam param8;
            SkillParam param9;
            EUnlockTypes types;
            if (unit != null)
            {
                goto Label_000C;
            }
            return string.Empty;
        Label_000C:
            str = string.Empty;
            str2 = string.Empty;
            str3 = string.Empty;
            if ((this.type != 2) && (this.type != 1))
            {
                goto Label_0233;
            }
            strArray = unit.jobsets;
            if (strArray == null)
            {
                goto Label_0233;
            }
            param = MonoSingleton<GameManager>.Instance.MasterParam;
            paramArray = new JobParam[(int) strArray.Length];
            num = 0;
            goto Label_0096;
        Label_0061:
            param2 = param.GetJobSetParam(strArray[num]);
            if (param2 == null)
            {
                goto Label_0090;
            }
            paramArray[num] = MonoSingleton<GameManager>.Instance.MasterParam.GetJobParam(param2.job);
        Label_0090:
            num += 1;
        Label_0096:
            if (num < ((int) strArray.Length))
            {
                goto Label_0061;
            }
            if (this.type != 2)
            {
                goto Label_00FB;
            }
            if (this.add != null)
            {
                goto Label_0233;
            }
            num2 = 0;
            goto Label_00EB;
        Label_00BF:
            if (paramArray[num2].FindRankOfAbility(this.old_id) == -1)
            {
                goto Label_00E5;
            }
            str = paramArray[num2].name;
            goto Label_00F6;
        Label_00E5:
            num2 += 1;
        Label_00EB:
            if (num2 < ((int) paramArray.Length))
            {
                goto Label_00BF;
            }
        Label_00F6:
            goto Label_0233;
        Label_00FB:
            if (this.type != 1)
            {
                goto Label_0233;
            }
            num3 = 0;
            goto Label_0228;
        Label_010F:
            num4 = 0;
            goto Label_0206;
        Label_0117:
            strArray2 = paramArray[num3].GetLearningAbilitys(num4);
            if (strArray2 == null)
            {
                goto Label_0200;
            }
            if (((int) strArray2.Length) >= 1)
            {
                goto Label_013B;
            }
            goto Label_0200;
        Label_013B:
            num5 = 0;
            goto Label_01E5;
        Label_0143:
            if (this.add == null)
            {
                goto Label_0186;
            }
            if ((*(&(strArray2[num5])) == this.parent_id) == null)
            {
                goto Label_01DF;
            }
            str = paramArray[num3].name;
            goto Label_01F0;
            goto Label_01DF;
        Label_0186:
            param3 = MonoSingleton<GameManager>.Instance.MasterParam.GetAbilityParam(*(&(strArray2[num5])));
            if ((param3 == null) || (Array.FindIndex<LearningSkill>(param3.skills, new Predicate<LearningSkill>(this.<GetCondText>m__25F)) == -1))
            {
                goto Label_01DF;
            }
            str = paramArray[num3].name;
            goto Label_01F0;
        Label_01DF:
            num5 += 1;
        Label_01E5:
            if (num5 < ((int) strArray2.Length))
            {
                goto Label_0143;
            }
        Label_01F0:
            if (string.IsNullOrEmpty(str) != null)
            {
                goto Label_0200;
            }
            goto Label_0212;
        Label_0200:
            num4 += 1;
        Label_0206:
            if (num4 < JobParam.MAX_JOB_RANK)
            {
                goto Label_0117;
            }
        Label_0212:
            if (string.IsNullOrEmpty(str) != null)
            {
                goto Label_0222;
            }
            goto Label_0233;
        Label_0222:
            num3 += 1;
        Label_0228:
            if (num3 < ((int) paramArray.Length))
            {
                goto Label_010F;
            }
        Label_0233:
            if (string.IsNullOrEmpty(str) != null)
            {
                goto Label_0253;
            }
            objArray1 = new object[] { str };
            str = LocalizedText.Get("sys.PARTYEDITOR_COND_UNLOCK_TEXTFRAME", objArray1);
        Label_0253:
            switch ((this.type + 1))
            {
                case 0:
                    goto Label_043F;

                case 1:
                    goto Label_0281;

                case 2:
                    goto Label_0286;

                case 3:
                    goto Label_034F;

                case 4:
                    goto Label_0286;

                case 5:
                    goto Label_03D2;
            }
            goto Label_043F;
        Label_0281:
            goto Label_0444;
        Label_0286:
            if (this.add == null)
            {
                goto Label_02E2;
            }
            str2 = LocalizedText.Get((string.IsNullOrEmpty(str) == null) ? "sys.UNITLIST_REWRITE_ABILITY" : "sys.UNITLIST_REWRITE_MASTERABILITY");
            param4 = MonoSingleton<GameManager>.Instance.MasterParam.GetAbilityParam(this.parent_id);
            str3 = (param4 == null) ? str3 : param4.name;
            goto Label_034A;
        Label_02E2:
            if (this.type != 3)
            {
                goto Label_02FE;
            }
            str2 = LocalizedText.Get("sys.UNITLIST_REWRITE_LEADERSKILL");
            goto Label_031E;
        Label_02FE:
            str2 = LocalizedText.Get((string.IsNullOrEmpty(str) == null) ? "sys.UNITLIST_REWRITE_SKILL" : "sys.UNITLIST_REWRITE_MASTERABILITY");
        Label_031E:
            param5 = MonoSingleton<GameManager>.Instance.MasterParam.GetSkillParam(this.old_id);
            str3 = (param5 == null) ? str3 : param5.name;
        Label_034A:
            goto Label_0444;
        Label_034F:
            if (this.add == null)
            {
                goto Label_0396;
            }
            str2 = LocalizedText.Get("sys.UNITLIST_REWRITE_JOB");
            param6 = MonoSingleton<GameManager>.Instance.MasterParam.GetJobParam(this.parent_id);
            str3 = (param6 == null) ? str3 : param6.name;
            goto Label_03CD;
        Label_0396:
            str2 = LocalizedText.Get("sys.UNITLIST_REWRITE_ABILITY");
            param7 = MonoSingleton<GameManager>.Instance.MasterParam.GetAbilityParam(this.old_id);
            str3 = (param7 == null) ? str3 : param7.name;
        Label_03CD:
            goto Label_0444;
        Label_03D2:
            if (this.add != null)
            {
                goto Label_0444;
            }
            str2 = LocalizedText.Get("sys.UNITLIST_REWRITE_MASTERABILITY");
            param8 = MonoSingleton<GameManager>.Instance.MasterParam.GetAbilityParam(this.old_id);
            if (param8 == null)
            {
                goto Label_0444;
            }
            param9 = MonoSingleton<GameManager>.Instance.MasterParam.GetSkillParam(param8.skills[0].iname);
            str3 = (param9 == null) ? str3 : param9.name;
            goto Label_0444;
        Label_043F:;
        Label_0444:
            if (string.IsNullOrEmpty(str2) != null)
            {
                goto Label_045A;
            }
            if (string.IsNullOrEmpty(str3) == null)
            {
                goto Label_0460;
            }
        Label_045A:
            return string.Empty;
        Label_0460:
            objArray2 = new object[] { str2, str, str3 };
            return LocalizedText.Get("sys.PARTYEDITOR_COND_UNLOCK_PARENT", objArray2);
        }

        public string GetRewriteName()
        {
            string str;
            AbilityParam param;
            EUnlockTypes types;
            str = string.Empty;
            if (this.add != null)
            {
                goto Label_00F0;
            }
            if (this.old_id == null)
            {
                goto Label_00F0;
            }
            switch ((this.type + 1))
            {
                case 0:
                    goto Label_00EB;

                case 1:
                    goto Label_0048;

                case 2:
                    goto Label_0068;

                case 3:
                    goto Label_0088;

                case 4:
                    goto Label_0068;

                case 5:
                    goto Label_00A8;
            }
            goto Label_00EB;
        Label_0048:
            str = MonoSingleton<GameManager>.Instance.MasterParam.GetJobParam(this.old_id).name;
            goto Label_00F0;
        Label_0068:
            str = MonoSingleton<GameManager>.Instance.MasterParam.GetSkillParam(this.old_id).name;
            goto Label_00F0;
        Label_0088:
            str = MonoSingleton<GameManager>.Instance.MasterParam.GetAbilityParam(this.old_id).name;
            goto Label_00F0;
        Label_00A8:
            param = MonoSingleton<GameManager>.Instance.MasterParam.GetAbilityParam(this.old_id);
            if (param == null)
            {
                goto Label_00F0;
            }
            str = MonoSingleton<GameManager>.Instance.MasterParam.GetSkillParam(param.skills[0].iname).name;
            goto Label_00F0;
        Label_00EB:;
        Label_00F0:
            return str;
        }

        public string GetUnitLevelCond()
        {
            object[] objArray1;
            return ((this.ulv > 0) ? LocalizedText.Get("sys.SKILL_UNLOCKCOND_UNITLV", objArray1 = new object[] { (int) this.ulv }) : string.Empty);
        }

        public string GetUnlockTypeText()
        {
            string str;
            EUnlockTypes types;
            str = string.Empty;
            switch ((this.type + 1))
            {
                case 0:
                    goto Label_0082;

                case 1:
                    goto Label_0032;

                case 2:
                    goto Label_0042;

                case 3:
                    goto Label_0052;

                case 4:
                    goto Label_0062;

                case 5:
                    goto Label_0072;
            }
            goto Label_0082;
        Label_0032:
            str = LocalizedText.Get("sys.UNITLIST_REWRITE_JOB");
            goto Label_0087;
        Label_0042:
            str = LocalizedText.Get("sys.UNITLIST_REWRITE_SKILL");
            goto Label_0087;
        Label_0052:
            str = LocalizedText.Get("sys.UNITLIST_REWRITE_ABILITY");
            goto Label_0087;
        Label_0062:
            str = LocalizedText.Get("sys.UNITLIST_REWRITE_LEADERSKILL");
            goto Label_0087;
        Label_0072:
            str = LocalizedText.Get("sys.UNITLIST_REWRITE_MASTERABILITY");
            goto Label_0087;
        Label_0082:;
        Label_0087:
            return str;
        }

        public bool IsMasterAbility()
        {
            return (this.type == 4);
        }

        public enum EUnlockTypes
        {
            None = -1,
            Job = 0,
            Skill = 1,
            Ability = 2,
            LeaderSkill = 3,
            MasterAbility = 4,
            CollaboAbility = 5
        }
    }
}

