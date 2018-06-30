namespace SRPG
{
    using GR;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class CollaboSkillParam
    {
        private string mIname;
        private string mUnitIname;
        private string mAbilityIname;
        private List<LearnSkill> mLearnSkillLists;

        public CollaboSkillParam()
        {
            this.mLearnSkillLists = new List<LearnSkill>();
            base..ctor();
            return;
        }

        public void Deserialize(JSON_CollaboSkillParam json)
        {
            string str;
            string[] strArray;
            int num;
            if (json != null)
            {
                goto Label_0007;
            }
            return;
        Label_0007:
            this.mIname = json.iname;
            this.mUnitIname = json.uname;
            this.mAbilityIname = json.abid;
            this.mLearnSkillLists.Clear();
            if (json.lqs == null)
            {
                goto Label_0071;
            }
            strArray = json.lqs;
            num = 0;
            goto Label_0068;
        Label_004F:
            str = strArray[num];
            this.mLearnSkillLists.Add(new LearnSkill(str));
            num += 1;
        Label_0068:
            if (num < ((int) strArray.Length))
            {
                goto Label_004F;
            }
        Label_0071:
            return;
        }

        public static unsafe List<string> GetLearnSkill(string quest_iname, string unit_iname)
        {
            List<string> list;
            GameManager manager;
            CollaboSkillParam param;
            LearnSkill skill;
            List<LearnSkill>.Enumerator enumerator;
            <GetLearnSkill>c__AnonStorey2BA storeyba;
            storeyba = new <GetLearnSkill>c__AnonStorey2BA();
            storeyba.unit_iname = unit_iname;
            list = new List<string>();
            if (string.IsNullOrEmpty(quest_iname) != null)
            {
                goto Label_0031;
            }
            if (string.IsNullOrEmpty(storeyba.unit_iname) == null)
            {
                goto Label_0033;
            }
        Label_0031:
            return list;
        Label_0033:
            manager = MonoSingleton<GameManager>.GetInstanceDirect();
            if ((manager == null) == null)
            {
                goto Label_0047;
            }
            return list;
        Label_0047:
            param = manager.MasterParam.CollaboSkills.Find(new Predicate<CollaboSkillParam>(storeyba.<>m__231));
            if (param != null)
            {
                goto Label_006D;
            }
            return list;
        Label_006D:
            enumerator = param.mLearnSkillLists.GetEnumerator();
        Label_007A:
            try
            {
                goto Label_00A9;
            Label_007F:
                skill = &enumerator.Current;
                if ((skill.QuestIname != quest_iname) == null)
                {
                    goto Label_009D;
                }
                goto Label_00A9;
            Label_009D:
                list.Add(skill.SkillIname);
            Label_00A9:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_007F;
                }
                goto Label_00C7;
            }
            finally
            {
            Label_00BA:
                ((List<LearnSkill>.Enumerator) enumerator).Dispose();
            }
        Label_00C7:
            return list;
        }

        public static unsafe List<Pair> GetPairLists()
        {
            List<Pair> list;
            GameManager manager;
            List<CollaboSkillParam>.Enumerator enumerator;
            List<LearnSkill>.Enumerator enumerator2;
            Pair pair;
            <GetPairLists>c__AnonStorey2BB storeybb;
            <GetPairLists>c__AnonStorey2BC storeybc;
            list = new List<Pair>();
            manager = MonoSingleton<GameManager>.GetInstanceDirect();
            if ((manager == null) == null)
            {
                goto Label_001A;
            }
            return list;
        Label_001A:
            storeybb = new <GetPairLists>c__AnonStorey2BB();
            enumerator = manager.MasterParam.CollaboSkills.GetEnumerator();
        Label_0032:
            try
            {
                goto Label_00F1;
            Label_0037:
                storeybb.csp = &enumerator.Current;
                storeybc = new <GetPairLists>c__AnonStorey2BC();
                storeybc.<>f__ref$699 = storeybb;
                enumerator2 = storeybb.csp.mLearnSkillLists.GetEnumerator();
            Label_0067:
                try
                {
                    goto Label_00D4;
                Label_006C:
                    storeybc.ls = &enumerator2.Current;
                    if (list.Find(new Predicate<Pair>(storeybc.<>m__232)) == null)
                    {
                        goto Label_009B;
                    }
                    goto Label_00D4;
                Label_009B:
                    list.Add(new Pair(manager.MasterParam.GetUnitParam(storeybb.csp.mUnitIname), manager.MasterParam.GetUnitParam(storeybc.ls.PartnerUnitIname)));
                Label_00D4:
                    if (&enumerator2.MoveNext() != null)
                    {
                        goto Label_006C;
                    }
                    goto Label_00F1;
                }
                finally
                {
                Label_00E5:
                    ((List<LearnSkill>.Enumerator) enumerator2).Dispose();
                }
            Label_00F1:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_0037;
                }
                goto Label_010E;
            }
            finally
            {
            Label_0102:
                ((List<CollaboSkillParam>.Enumerator) enumerator).Dispose();
            }
        Label_010E:
            return list;
        }

        public static string GetPartnerIname(string unit_iname, string skill_iname)
        {
            GameManager manager;
            CollaboSkillParam param;
            LearnSkill skill;
            <GetPartnerIname>c__AnonStorey2B9 storeyb;
            storeyb = new <GetPartnerIname>c__AnonStorey2B9();
            storeyb.unit_iname = unit_iname;
            storeyb.skill_iname = skill_iname;
            if (string.IsNullOrEmpty(storeyb.unit_iname) != null)
            {
                goto Label_0034;
            }
            if (string.IsNullOrEmpty(storeyb.skill_iname) == null)
            {
                goto Label_0036;
            }
        Label_0034:
            return null;
        Label_0036:
            manager = MonoSingleton<GameManager>.GetInstanceDirect();
            if ((manager == null) == null)
            {
                goto Label_004A;
            }
            return null;
        Label_004A:
            param = manager.MasterParam.CollaboSkills.Find(new Predicate<CollaboSkillParam>(storeyb.<>m__22F));
            if (param != null)
            {
                goto Label_0084;
            }
            DebugUtility.LogError(string.Format("CollaboSkillParam/GetPartnerIname CollaboSkillParam not found. unit_iname={0}", storeyb.unit_iname));
            return null;
        Label_0084:
            skill = param.mLearnSkillLists.Find(new Predicate<LearnSkill>(storeyb.<>m__230));
            if (skill != null)
            {
                goto Label_00B9;
            }
            DebugUtility.LogError(string.Format("CollaboSkillParam/GetPartnerIname LearnSkill not found. skill_iname={0}", storeyb.skill_iname));
            return null;
        Label_00B9:
            return skill.PartnerUnitIname;
        }

        public static unsafe Pair IsLearnQuest(string quest_id)
        {
            GameManager manager;
            List<string> list;
            CollaboSkillParam param;
            List<CollaboSkillParam>.Enumerator enumerator;
            LearnSkill skill;
            List<LearnSkill>.Enumerator enumerator2;
            manager = MonoSingleton<GameManager>.GetInstanceDirect();
            if ((manager == null) == null)
            {
                goto Label_0014;
            }
            return null;
        Label_0014:
            list = new List<string>();
            enumerator = manager.MasterParam.CollaboSkills.GetEnumerator();
        Label_002B:
            try
            {
                goto Label_0090;
            Label_0030:
                param = &enumerator.Current;
                enumerator2 = param.mLearnSkillLists.GetEnumerator();
            Label_0045:
                try
                {
                    goto Label_0072;
                Label_004A:
                    skill = &enumerator2.Current;
                    if ((skill.QuestIname == quest_id) == null)
                    {
                        goto Label_0072;
                    }
                    list.Add(skill.PartnerUnitIname);
                Label_0072:
                    if (&enumerator2.MoveNext() != null)
                    {
                        goto Label_004A;
                    }
                    goto Label_0090;
                }
                finally
                {
                Label_0083:
                    ((List<LearnSkill>.Enumerator) enumerator2).Dispose();
                }
            Label_0090:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_0030;
                }
                goto Label_00AD;
            }
            finally
            {
            Label_00A1:
                ((List<CollaboSkillParam>.Enumerator) enumerator).Dispose();
            }
        Label_00AD:
            if (list.Count != 2)
            {
                goto Label_00E3;
            }
            return new Pair(manager.MasterParam.GetUnitParam(list[1]), manager.MasterParam.GetUnitParam(list[0]));
        Label_00E3:
            return null;
        }

        public static unsafe void UpdateCollaboSkill(List<CollaboSkillParam> csp_lists)
        {
            GameManager manager;
            CollaboSkillParam param;
            List<CollaboSkillParam>.Enumerator enumerator;
            int num;
            AbilityParam param2;
            CollaboSkillParam param3;
            List<CollaboSkillParam>.Enumerator enumerator2;
            AbilityParam param4;
            List<LearningSkill> list;
            LearningSkill skill;
            <UpdateCollaboSkill>c__AnonStorey2B8 storeyb;
            manager = MonoSingleton<GameManager>.GetInstanceDirect();
            if ((manager == null) == null)
            {
                goto Label_0013;
            }
            return;
        Label_0013:
            enumerator = csp_lists.GetEnumerator();
        Label_001A:
            try
            {
                goto Label_018D;
            Label_001F:
                param = &enumerator.Current;
                num = 0;
                goto Label_017C;
            Label_002E:
                storeyb = new <UpdateCollaboSkill>c__AnonStorey2B8();
                storeyb.ls = param.mLearnSkillLists[num];
                if (string.IsNullOrEmpty(storeyb.ls.QuestIname) == null)
                {
                    goto Label_0063;
                }
                goto Label_0178;
            Label_0063:
                param2 = manager.MasterParam.GetAbilityParam(param.AbilityIname);
                if (param2 != null)
                {
                    goto Label_0097;
                }
                DebugUtility.LogError(string.Format("CollaboSkillParam/Deserialize AbilityParam not found. ability_iname={0}", param.mAbilityIname));
                goto Label_0178;
            Label_0097:
                if (num < ((int) param2.skills.Length))
                {
                    goto Label_00AB;
                }
                goto Label_0178;
            Label_00AB:
                storeyb.ls.SkillIname = param2.skills[num].iname;
                enumerator2 = csp_lists.GetEnumerator();
            Label_00CD:
                try
                {
                    goto Label_015A;
                Label_00D2:
                    param3 = &enumerator2.Current;
                    if ((param3.mUnitIname == param.mUnitIname) == null)
                    {
                        goto Label_00F7;
                    }
                    goto Label_015A;
                Label_00F7:
                    param4 = manager.MasterParam.GetAbilityParam(param3.AbilityIname);
                    if (param4 != null)
                    {
                        goto Label_0117;
                    }
                    goto Label_015A;
                Label_0117:
                    list = new List<LearningSkill>(param4.skills);
                    if (list.Find(new Predicate<LearningSkill>(storeyb.<>m__22E)) == null)
                    {
                        goto Label_015A;
                    }
                    storeyb.ls.PartnerUnitIname = param3.UnitIname;
                    goto Label_0166;
                Label_015A:
                    if (&enumerator2.MoveNext() != null)
                    {
                        goto Label_00D2;
                    }
                Label_0166:
                    goto Label_0178;
                }
                finally
                {
                Label_016B:
                    ((List<CollaboSkillParam>.Enumerator) enumerator2).Dispose();
                }
            Label_0178:
                num += 1;
            Label_017C:
                if (num < param.mLearnSkillLists.Count)
                {
                    goto Label_002E;
                }
            Label_018D:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_001F;
                }
                goto Label_01AA;
            }
            finally
            {
            Label_019E:
                ((List<CollaboSkillParam>.Enumerator) enumerator).Dispose();
            }
        Label_01AA:
            return;
        }

        public string Iname
        {
            get
            {
                return this.mIname;
            }
        }

        public string UnitIname
        {
            get
            {
                return this.mUnitIname;
            }
        }

        public string AbilityIname
        {
            get
            {
                return this.mAbilityIname;
            }
        }

        public List<LearnSkill> LearnSkillLists
        {
            get
            {
                return this.mLearnSkillLists;
            }
        }

        [CompilerGenerated]
        private sealed class <GetLearnSkill>c__AnonStorey2BA
        {
            internal string unit_iname;

            public <GetLearnSkill>c__AnonStorey2BA()
            {
                base..ctor();
                return;
            }

            internal bool <>m__231(CollaboSkillParam fcs)
            {
                return (fcs.UnitIname == this.unit_iname);
            }
        }

        [CompilerGenerated]
        private sealed class <GetPairLists>c__AnonStorey2BB
        {
            internal CollaboSkillParam csp;

            public <GetPairLists>c__AnonStorey2BB()
            {
                base..ctor();
                return;
            }
        }

        [CompilerGenerated]
        private sealed class <GetPairLists>c__AnonStorey2BC
        {
            internal CollaboSkillParam.LearnSkill ls;
            internal CollaboSkillParam.<GetPairLists>c__AnonStorey2BB <>f__ref$699;

            public <GetPairLists>c__AnonStorey2BC()
            {
                base..ctor();
                return;
            }

            internal bool <>m__232(CollaboSkillParam.Pair tp)
            {
                return ((((tp.UnitParam1.iname == this.<>f__ref$699.csp.mUnitIname) != null) && ((tp.UnitParam2.iname == this.ls.PartnerUnitIname) != null)) ? 1 : (((tp.UnitParam1.iname == this.ls.PartnerUnitIname) == null) ? 0 : (tp.UnitParam2.iname == this.<>f__ref$699.csp.mUnitIname)));
            }
        }

        [CompilerGenerated]
        private sealed class <GetPartnerIname>c__AnonStorey2B9
        {
            internal string unit_iname;
            internal string skill_iname;

            public <GetPartnerIname>c__AnonStorey2B9()
            {
                base..ctor();
                return;
            }

            internal bool <>m__22F(CollaboSkillParam fcs)
            {
                return (fcs.UnitIname == this.unit_iname);
            }

            internal bool <>m__230(CollaboSkillParam.LearnSkill fls)
            {
                return (fls.SkillIname == this.skill_iname);
            }
        }

        [CompilerGenerated]
        private sealed class <UpdateCollaboSkill>c__AnonStorey2B8
        {
            internal CollaboSkillParam.LearnSkill ls;

            public <UpdateCollaboSkill>c__AnonStorey2B8()
            {
                base..ctor();
                return;
            }

            internal bool <>m__22E(LearningSkill flgs)
            {
                return (flgs.iname == this.ls.SkillIname);
            }
        }

        public class LearnSkill
        {
            public string QuestIname;
            public string SkillIname;
            public string PartnerUnitIname;

            public LearnSkill(string q_iname)
            {
                base..ctor();
                this.QuestIname = q_iname;
                return;
            }
        }

        public class Pair
        {
            public UnitParam UnitParam1;
            public UnitParam UnitParam2;

            public Pair(UnitParam u_param1, UnitParam u_param2)
            {
                base..ctor();
                this.UnitParam1 = u_param1;
                this.UnitParam2 = u_param2;
                return;
            }
        }
    }
}

