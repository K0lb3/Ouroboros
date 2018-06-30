namespace SRPG
{
    using GR;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public class AbilityData
    {
        public const long UNIQUE_ID_MAP_EFFECT = -1L;
        private UnitData mOwner;
        private OLong mUniqueID;
        private AbilityParam mAbilityParam;
        private OInt mExp;
        private OInt mRank;
        private OInt mRankCap;
        private List<SkillData> mSkills;
        public bool IsNoneCategory;
        public bool IsHideList;
        private AbilityData m_BaseAbility;
        private List<AbilityData> m_DeriveAbility;
        private List<SkillData> m_DeriveSkills;
        private AbilityDeriveParam m_AbilityDeriveParam;
        [CompilerGenerated]
        private static Func<SkillData, string> <>f__am$cacheD;

        public AbilityData()
        {
            this.mUniqueID = 0L;
            this.mExp = 0;
            this.mRank = 1;
            this.mRankCap = 1;
            base..ctor();
            return;
        }

        [CompilerGenerated]
        private static string <FindDeriveSkillIDs>m__D7(SkillData skillData)
        {
            return skillData.SkillID;
        }

        public void AddDeriveAbility(AbilityData deriveAbility)
        {
            if (this.m_DeriveAbility != null)
            {
                goto Label_0016;
            }
            this.m_DeriveAbility = new List<AbilityData>();
        Label_0016:
            this.m_DeriveAbility.Add(deriveAbility);
            return;
        }

        public void AddDeriveSkill(SkillData skillData)
        {
            if (this.m_DeriveSkills != null)
            {
                goto Label_0016;
            }
            this.m_DeriveSkills = new List<SkillData>();
        Label_0016:
            this.m_DeriveSkills.Add(skillData);
            return;
        }

        public int CalcRank()
        {
            int num;
            int num2;
            num = this.mExp + 1;
            num2 = this.GetRankMaxCap();
            return Math.Min(num, num2);
        }

        public bool CheckEnableUseAbility(UnitData self, int job_index)
        {
            return ((this.Param == null) ? 0 : this.Param.CheckEnableUseAbility(self, job_index));
        }

        public AbilityData CreateDeriveAbility(AbilityDeriveParam deriveParam)
        {
            AbilityData data;
            data = new AbilityData();
            data.Setup(this.Owner, this.UniqueID, deriveParam.DeriveAbilityIname, this.Exp, 0);
            data.m_BaseAbility = this;
            data.IsNoneCategory = this.IsNoneCategory;
            data.m_AbilityDeriveParam = deriveParam;
            return data;
        }

        public string[] FindDeriveSkillIDs(string baseSkillIname)
        {
            <FindDeriveSkillIDs>c__AnonStorey1FC storeyfc;
            storeyfc = new <FindDeriveSkillIDs>c__AnonStorey1FC();
            storeyfc.baseSkillIname = baseSkillIname;
            if (this.m_DeriveSkills == null)
            {
                goto Label_0057;
            }
            if (<>f__am$cacheD != null)
            {
                goto Label_0047;
            }
            <>f__am$cacheD = new Func<SkillData, string>(AbilityData.<FindDeriveSkillIDs>m__D7);
        Label_0047:
            return Enumerable.ToArray<string>(Enumerable.Select<SkillData, string>(Enumerable.Where<SkillData>(this.m_DeriveSkills, new Func<SkillData, bool>(storeyfc.<>m__D6)), <>f__am$cacheD));
        Label_0057:
            return new string[0];
        }

        public SkillData[] FindDeriveSkills(string baseSkillIname)
        {
            <FindDeriveSkills>c__AnonStorey1FD storeyfd;
            storeyfd = new <FindDeriveSkills>c__AnonStorey1FD();
            storeyfd.baseSkillIname = baseSkillIname;
            if (this.m_DeriveSkills == null)
            {
                goto Label_0035;
            }
            return Enumerable.ToArray<SkillData>(Enumerable.Where<SkillData>(this.m_DeriveSkills, new Func<SkillData, bool>(storeyfd.<>m__D8)));
        Label_0035:
            return new SkillData[0];
        }

        public unsafe SkillData FindSkillDataInSkills(string iname)
        {
            List<SkillData> list;
            SkillData data;
            List<SkillData>.Enumerator enumerator;
            SkillData data2;
            enumerator = this.Skills.GetEnumerator();
        Label_000E:
            try
            {
                goto Label_004E;
            Label_0013:
                data = &enumerator.Current;
                if (data == null)
                {
                    goto Label_004E;
                }
                if (data.IsValid() != null)
                {
                    goto Label_0031;
                }
                goto Label_004E;
            Label_0031:
                if ((data.SkillID != iname) == null)
                {
                    goto Label_0047;
                }
                goto Label_004E;
            Label_0047:
                data2 = data;
                goto Label_006D;
            Label_004E:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_0013;
                }
                goto Label_006B;
            }
            finally
            {
            Label_005F:
                ((List<SkillData>.Enumerator) enumerator).Dispose();
            }
        Label_006B:
            return null;
        Label_006D:
            return data2;
        }

        public void GainExp(int exp)
        {
            int num;
            int num2;
            int num3;
            num = this.GetRankCap();
            if (num >= this.Rank)
            {
                goto Label_0014;
            }
            return;
        Label_0014:
            num2 = this.mRank;
            this.mExp += exp;
            this.mRank = Math.Min(this.CalcRank(), num);
            this.mExp = Math.Max(this.mRank - 1, 0);
            if (this.mRank == num2)
            {
                goto Label_00E4;
            }
            num3 = 0;
            goto Label_00BD;
        Label_0085:
            this.mSkills[num3].Setup(this.mSkills[num3].SkillID, this.mRank, this.GetRankMaxCap(), null);
            num3 += 1;
        Label_00BD:
            if (num3 < this.mSkills.Count)
            {
                goto Label_0085;
            }
            if (this.Owner == null)
            {
                goto Label_00E4;
            }
            this.Owner.UpdateAbilityRankUp();
        Label_00E4:
            return;
        }

        public List<string> GetLearningSkillList(int rank)
        {
            List<string> list;
            int num;
            if (this.Param == null)
            {
                goto Label_001B;
            }
            if (this.Param.skills != null)
            {
                goto Label_001D;
            }
        Label_001B:
            return null;
        Label_001D:
            list = new List<string>();
            num = 0;
            goto Label_0084;
        Label_002A:
            if (string.IsNullOrEmpty(this.Param.skills[num].iname) == null)
            {
                goto Label_004B;
            }
            goto Label_0080;
        Label_004B:
            if (rank == this.Param.skills[num].locklv)
            {
                goto Label_0068;
            }
            goto Label_0080;
        Label_0068:
            list.Add(this.Param.skills[num].iname);
        Label_0080:
            num += 1;
        Label_0084:
            if (num < ((int) this.Param.skills.Length))
            {
                goto Label_002A;
            }
            return list;
        }

        public List<string> GetLearningSkillList2(int rank)
        {
            List<string> list;
            int num;
            bool flag;
            int num2;
            string str;
            if (this.Param == null)
            {
                goto Label_001B;
            }
            if (this.Param.skills != null)
            {
                goto Label_001D;
            }
        Label_001B:
            return null;
        Label_001D:
            list = new List<string>();
            num = 0;
            goto Label_0141;
        Label_002A:
            if (string.IsNullOrEmpty(this.Param.skills[num].iname) == null)
            {
                goto Label_004B;
            }
            goto Label_013D;
        Label_004B:
            flag = 0;
            num2 = 0;
            goto Label_00EC;
        Label_0054:
            str = this.Skills[num2].SkillParam.iname;
            if (string.IsNullOrEmpty(this.Skills[num2].ReplaceSkillId) != null)
            {
                goto Label_009A;
            }
            str = this.Skills[num2].ReplaceSkillId;
        Label_009A:
            if (this.Skills[num2].IsDerivedSkill == null)
            {
                goto Label_00C3;
            }
            str = this.Skills[num2].m_BaseSkillIname;
        Label_00C3:
            if ((str == this.Param.skills[num].iname) == null)
            {
                goto Label_00E8;
            }
            flag = 1;
            goto Label_00FD;
        Label_00E8:
            num2 += 1;
        Label_00EC:
            if (num2 < this.Skills.Count)
            {
                goto Label_0054;
            }
        Label_00FD:
            if (flag == null)
            {
                goto Label_0108;
            }
            goto Label_013D;
        Label_0108:
            if (this.Param.skills[num].locklv <= rank)
            {
                goto Label_0125;
            }
            goto Label_013D;
        Label_0125:
            list.Add(this.Param.skills[num].iname);
        Label_013D:
            num += 1;
        Label_0141:
            if (num < ((int) this.Param.skills.Length))
            {
                goto Label_002A;
            }
            return list;
        }

        public int GetNextGold()
        {
            if (this.mRank >= this.GetRankMaxCap())
            {
                goto Label_0031;
            }
            return MonoSingleton<GameManager>.Instance.MasterParam.GetAbilityNextGold(this.mRank);
        Label_0031:
            return 0;
        }

        public int GetRankCap()
        {
            if (this.Owner != null)
            {
                goto Label_0017;
            }
            return this.mRankCap;
        Label_0017:
            return Math.Min(this.mRankCap, this.Owner.Lv);
        }

        public int GetRankMaxCap()
        {
            return Math.Max(this.mRankCap, 1);
        }

        public bool IsValid()
        {
            return ((this.mAbilityParam == null) == 0);
        }

        private static List<SkillData> MakeDerivedSkillList(List<SkillData> originSkills, List<SkillData> deriveSkills)
        {
            List<SkillData> list;
            List<SkillData> list2;
            <MakeDerivedSkillList>c__AnonStorey1FA storeyfa;
            <MakeDerivedSkillList>c__AnonStorey1FB storeyfb;
            storeyfa = new <MakeDerivedSkillList>c__AnonStorey1FA();
            storeyfa.originSkills = originSkills;
            if (deriveSkills != null)
            {
                goto Label_001A;
            }
            return storeyfa.originSkills;
        Label_001A:
            list = new List<SkillData>();
            storeyfb = new <MakeDerivedSkillList>c__AnonStorey1FB();
            storeyfb.<>f__ref$506 = storeyfa;
            storeyfb.i = 0;
            goto Label_008F;
        Label_0039:
            list2 = deriveSkills.FindAll(new Predicate<SkillData>(storeyfb.<>m__D5));
            if (list2 == null)
            {
                goto Label_006A;
            }
            if (list2.Count <= 0)
            {
                goto Label_006A;
            }
            list.AddRange(list2);
            goto Label_0081;
        Label_006A:
            list.Add(storeyfa.originSkills[storeyfb.i]);
        Label_0081:
            storeyfb.i += 1;
        Label_008F:
            if (storeyfb.i < storeyfa.originSkills.Count)
            {
                goto Label_0039;
            }
            return list;
        }

        private void Reset()
        {
            this.mUniqueID = 0L;
            this.mAbilityParam = null;
            this.mExp = 0;
            this.mRank = 1;
            this.mSkills = null;
            return;
        }

        public void ResetDeriveAbility()
        {
            if (this.IsDerivedAbility == null)
            {
                goto Label_0016;
            }
            this.m_BaseAbility.ResetDeriveAbility();
        Label_0016:
            if (this.m_DeriveAbility == null)
            {
                goto Label_002C;
            }
            this.m_DeriveAbility.Clear();
        Label_002C:
            if (this.m_DeriveSkills == null)
            {
                goto Label_0042;
            }
            this.m_DeriveSkills.Clear();
        Label_0042:
            return;
        }

        public void Setup(UnitData owner, long iid, string iname, int exp, int rank_cap)
        {
            GameManager manager;
            AbilityParam param;
            int num;
            if (string.IsNullOrEmpty(iname) == null)
            {
                goto Label_0012;
            }
            this.Reset();
            return;
        Label_0012:
            manager = MonoSingleton<GameManager>.Instance;
            this.mOwner = owner;
            this.mAbilityParam = manager.GetAbilityParam(iname);
            this.mUniqueID = iid;
            this.mExp = exp;
            this.mRankCap = this.mAbilityParam.GetRankCap();
            if (rank_cap <= 0)
            {
                goto Label_0070;
            }
            this.mRankCap = rank_cap;
        Label_0070:
            this.mRank = this.CalcRank();
            this.mSkills = null;
            param = this.Param;
            if (param.skills == null)
            {
                goto Label_00B7;
            }
            num = (int) param.skills.Length;
            this.mSkills = new List<SkillData>(num);
            this.UpdateLearningsSkill(1, null);
        Label_00B7:
            return;
        }

        public static unsafe MixedAbilityData ToMix(AbilityData[] abilitys, string name, string iconName)
        {
            MixedAbilityData data;
            int num;
            AbilityData data2;
            SkillData data3;
            List<SkillData>.Enumerator enumerator;
            data = new MixedAbilityData();
            data.mSkills = new List<SkillData>();
            num = 0;
            goto Label_0064;
        Label_0018:
            data2 = abilitys[num];
            enumerator = data2.Skills.GetEnumerator();
        Label_0029:
            try
            {
                goto Label_0042;
            Label_002E:
                data3 = &enumerator.Current;
                data.mSkills.Add(data3);
            Label_0042:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_002E;
                }
                goto Label_0060;
            }
            finally
            {
            Label_0053:
                ((List<SkillData>.Enumerator) enumerator).Dispose();
            }
        Label_0060:
            num += 1;
        Label_0064:
            if (num < ((int) abilitys.Length))
            {
                goto Label_0018;
            }
            data.mAbilityParam = new AbilityParam();
            data.mAbilityParam.name = name;
            data.mAbilityParam.icon = iconName;
            data.mAbilityParam.type = 0;
            data.mAbilityParam.slot = 0;
            data.mAbilityParam.skills = new LearningSkill[1];
            return data;
        }

        public override string ToString()
        {
            object[] objArray1;
            objArray1 = new object[] { (long) this.UniqueID, this.Param, this.Param.iname, this.AbilityName, (int) this.Rank, (int) this.Exp, (EAbilityType) this.AbilityType, (EAbilitySlot) this.SlotType, this.LearningSkills, this.Skills };
            return string.Format("[AbilityData: UniqueID={0}, Param={1}, AbilityID={2}, AbilityName={3}, Rank={4}, Exp={5}, AbilityType={6}, SlotType={7}, LearningSkills={8}, Skills={9}]", objArray1);
        }

        public void UpdateLearningsSkill(bool locked, List<SkillData> sd_lists)
        {
            AbilityParam param;
            int num;
            int num2;
            SkillData data;
            int num3;
            SkillData data2;
            string str;
            SkillData data3;
            int num4;
            SkillData data4;
            <UpdateLearningsSkill>c__AnonStorey1F7 storeyf;
            <UpdateLearningsSkill>c__AnonStorey1F6 storeyf2;
            <UpdateLearningsSkill>c__AnonStorey1F8 storeyf3;
            storeyf = new <UpdateLearningsSkill>c__AnonStorey1F7();
            storeyf.<>f__this = this;
            if (this.mSkills != null)
            {
                goto Label_001B;
            }
            return;
        Label_001B:
            param = this.Param;
            this.mSkills.Clear();
            if (param == null)
            {
                goto Label_0040;
            }
            if (((int) param.skills.Length) != null)
            {
                goto Label_0041;
            }
        Label_0040:
            return;
        Label_0041:
            storeyf.unlocks = null;
            if (this.Owner == null)
            {
                goto Label_0066;
            }
            storeyf.unlocks = this.Owner.UnlockedSkills;
        Label_0066:
            num = 0;
            goto Label_01BC;
        Label_006D:
            storeyf2 = new <UpdateLearningsSkill>c__AnonStorey1F6();
            if (locked == null)
            {
                goto Label_009C;
            }
            if (this.mRank >= param.skills[num].locklv)
            {
                goto Label_009C;
            }
            goto Label_01B8;
        Label_009C:
            storeyf2.skillId = param.skills[num].iname;
            if (storeyf.unlocks == null)
            {
                goto Label_0123;
            }
            num2 = Array.FindIndex<QuestClearUnlockUnitDataParam>(storeyf.unlocks, new Predicate<QuestClearUnlockUnitDataParam>(storeyf2.<>m__D1));
            if (num2 == -1)
            {
                goto Label_0123;
            }
            if (storeyf.unlocks[num2].add != null)
            {
                goto Label_0123;
            }
            if ((storeyf.unlocks[num2].parent_id == this.AbilityID) == null)
            {
                goto Label_0123;
            }
            storeyf2.skillId = storeyf.unlocks[num2].new_id;
        Label_0123:
            data = null;
            if (sd_lists == null)
            {
                goto Label_017C;
            }
            num3 = 0;
            goto Label_016F;
        Label_0133:
            data2 = sd_lists[num3];
            if (data2 != null)
            {
                goto Label_0149;
            }
            goto Label_0169;
        Label_0149:
            if ((data2.SkillID == storeyf2.skillId) == null)
            {
                goto Label_0169;
            }
            data = data2;
            goto Label_017C;
        Label_0169:
            num3 += 1;
        Label_016F:
            if (num3 < sd_lists.Count)
            {
                goto Label_0133;
            }
        Label_017C:
            if (data != null)
            {
                goto Label_01AC;
            }
            data = new SkillData();
            data.Setup(storeyf2.skillId, this.mRank, this.mRankCap, null);
        Label_01AC:
            this.mSkills.Add(data);
        Label_01B8:
            num += 1;
        Label_01BC:
            if (num < ((int) param.skills.Length))
            {
                goto Label_006D;
            }
            if (storeyf.unlocks == null)
            {
                goto Label_031E;
            }
            storeyf3 = new <UpdateLearningsSkill>c__AnonStorey1F8();
            storeyf3.<>f__ref$503 = storeyf;
            storeyf3.i = 0;
            goto Label_0309;
        Label_01F3:
            if (storeyf.unlocks[storeyf3.i].add == null)
            {
                goto Label_02F9;
            }
            if ((storeyf.unlocks[storeyf3.i].parent_id != this.AbilityID) != null)
            {
                goto Label_02F9;
            }
            if (this.mSkills.Find(new Predicate<SkillData>(storeyf3.<>m__D2)) == null)
            {
                goto Label_0252;
            }
            goto Label_02F9;
        Label_0252:
            str = storeyf.unlocks[storeyf3.i].new_id;
            data3 = null;
            if (sd_lists == null)
            {
                goto Label_02BE;
            }
            num4 = 0;
            goto Label_02B1;
        Label_0279:
            data4 = sd_lists[num4];
            if (data4 != null)
            {
                goto Label_028F;
            }
            goto Label_02AB;
        Label_028F:
            if ((data4.SkillID == str) == null)
            {
                goto Label_02AB;
            }
            data3 = data4;
            goto Label_02BE;
        Label_02AB:
            num4 += 1;
        Label_02B1:
            if (num4 < sd_lists.Count)
            {
                goto Label_0279;
            }
        Label_02BE:
            if (data3 != null)
            {
                goto Label_02EC;
            }
            data3 = new SkillData();
            data3.Setup(str, this.mRank, this.mRankCap, null);
        Label_02EC:
            this.mSkills.Add(data3);
        Label_02F9:
            storeyf3.i += 1;
        Label_0309:
            if (storeyf3.i < ((int) storeyf.unlocks.Length))
            {
                goto Label_01F3;
            }
        Label_031E:
            this.mSkills.ForEach(new Action<SkillData>(storeyf.<>m__D3));
            return;
        }

        public void UpdateLearningsSkillCollabo(Json_CollaboSkill[] skills)
        {
            AbilityParam param;
            List<LearningSkill> list;
            Json_CollaboSkill[] skillArray;
            int num;
            LearningSkill skill;
            SkillData data;
            <UpdateLearningsSkillCollabo>c__AnonStorey1F9 storeyf;
            if (this.mSkills != null)
            {
                goto Label_000C;
            }
            return;
        Label_000C:
            this.mSkills.Clear();
            if (skills == null)
            {
                goto Label_0025;
            }
            if (((int) skills.Length) != null)
            {
                goto Label_0026;
            }
        Label_0025:
            return;
        Label_0026:
            param = this.Param;
            if (param == null)
            {
                goto Label_0040;
            }
            if (((int) param.skills.Length) != null)
            {
                goto Label_0041;
            }
        Label_0040:
            return;
        Label_0041:
            list = new List<LearningSkill>(param.skills);
            storeyf = new <UpdateLearningsSkillCollabo>c__AnonStorey1F9();
            skillArray = skills;
            num = 0;
            goto Label_00F2;
        Label_005D:
            storeyf.cs = skillArray[num];
            if (string.IsNullOrEmpty(storeyf.cs.iname) == null)
            {
                goto Label_0082;
            }
            goto Label_00EE;
        Label_0082:
            if (list.Find(new Predicate<LearningSkill>(storeyf.<>m__D4)) != null)
            {
                goto Label_00A3;
            }
            goto Label_00EE;
        Label_00A3:
            data = new SkillData();
            data.Setup(storeyf.cs.iname, this.mRank, this.mRankCap, null);
            data.IsCollabo = 1;
            this.mSkills.Add(data);
        Label_00EE:
            num += 1;
        Label_00F2:
            if (num < ((int) skillArray.Length))
            {
                goto Label_005D;
            }
            return;
        }

        public UnitData Owner
        {
            get
            {
                return this.mOwner;
            }
        }

        public long UniqueID
        {
            get
            {
                return this.mUniqueID;
            }
        }

        public AbilityParam Param
        {
            get
            {
                return this.mAbilityParam;
            }
        }

        public string AbilityID
        {
            get
            {
                return ((this.Param == null) ? null : this.Param.iname);
            }
        }

        public string AbilityName
        {
            get
            {
                return ((this.Param == null) ? null : this.Param.name);
            }
        }

        public int Rank
        {
            get
            {
                return this.mRank;
            }
        }

        public int Exp
        {
            get
            {
                return this.mExp;
            }
        }

        public EAbilityType AbilityType
        {
            get
            {
                return ((this.Param == null) ? 0 : this.Param.type);
            }
        }

        public EAbilitySlot SlotType
        {
            get
            {
                return ((this.Param == null) ? 0 : this.Param.slot);
            }
        }

        public LearningSkill[] LearningSkills
        {
            get
            {
                return ((this.Param == null) ? null : this.Param.skills);
            }
        }

        public List<SkillData> Skills
        {
            get
            {
                return MakeDerivedSkillList(this.mSkills, this.m_DeriveSkills);
            }
        }

        public bool IsDerivedAbility
        {
            get
            {
                return ((this.m_BaseAbility == null) == 0);
            }
        }

        public bool IsDeriveBaseAbility
        {
            get
            {
                return ((this.m_DeriveAbility == null) ? 0 : (this.m_DeriveAbility.Count > 0));
            }
        }

        public AbilityData DeriveBaseAbility
        {
            get
            {
                return this.m_BaseAbility;
            }
        }

        public AbilityDeriveParam DeriveParam
        {
            get
            {
                return this.m_AbilityDeriveParam;
            }
        }

        public AbilityData DerivedAbility
        {
            get
            {
                if (this.IsDeriveBaseAbility == null)
                {
                    goto Label_0018;
                }
                return this.m_DeriveAbility[0];
            Label_0018:
                return null;
            }
        }

        [CompilerGenerated]
        private sealed class <FindDeriveSkillIDs>c__AnonStorey1FC
        {
            internal string baseSkillIname;

            public <FindDeriveSkillIDs>c__AnonStorey1FC()
            {
                base..ctor();
                return;
            }

            internal bool <>m__D6(SkillData skillData)
            {
                return (skillData.m_BaseSkillIname == this.baseSkillIname);
            }
        }

        [CompilerGenerated]
        private sealed class <FindDeriveSkills>c__AnonStorey1FD
        {
            internal string baseSkillIname;

            public <FindDeriveSkills>c__AnonStorey1FD()
            {
                base..ctor();
                return;
            }

            internal bool <>m__D8(SkillData skillData)
            {
                return (skillData.m_BaseSkillIname == this.baseSkillIname);
            }
        }

        [CompilerGenerated]
        private sealed class <MakeDerivedSkillList>c__AnonStorey1FA
        {
            internal List<SkillData> originSkills;

            public <MakeDerivedSkillList>c__AnonStorey1FA()
            {
                base..ctor();
                return;
            }
        }

        [CompilerGenerated]
        private sealed class <MakeDerivedSkillList>c__AnonStorey1FB
        {
            internal int i;
            internal AbilityData.<MakeDerivedSkillList>c__AnonStorey1FA <>f__ref$506;

            public <MakeDerivedSkillList>c__AnonStorey1FB()
            {
                base..ctor();
                return;
            }

            internal bool <>m__D5(SkillData ds)
            {
                return (ds.m_BaseSkillIname == this.<>f__ref$506.originSkills[this.i].SkillParam.iname);
            }
        }

        [CompilerGenerated]
        private sealed class <UpdateLearningsSkill>c__AnonStorey1F6
        {
            internal string skillId;

            public <UpdateLearningsSkill>c__AnonStorey1F6()
            {
                base..ctor();
                return;
            }

            internal bool <>m__D1(QuestClearUnlockUnitDataParam p)
            {
                return (p.old_id == this.skillId);
            }
        }

        [CompilerGenerated]
        private sealed class <UpdateLearningsSkill>c__AnonStorey1F7
        {
            internal QuestClearUnlockUnitDataParam[] unlocks;
            internal AbilityData <>f__this;

            public <UpdateLearningsSkill>c__AnonStorey1F7()
            {
                base..ctor();
                return;
            }

            internal void <>m__D3(SkillData skillData)
            {
                skillData.SetOwnerAbility(this.<>f__this);
                return;
            }
        }

        [CompilerGenerated]
        private sealed class <UpdateLearningsSkill>c__AnonStorey1F8
        {
            internal int i;
            internal AbilityData.<UpdateLearningsSkill>c__AnonStorey1F7 <>f__ref$503;

            public <UpdateLearningsSkill>c__AnonStorey1F8()
            {
                base..ctor();
                return;
            }

            internal bool <>m__D2(SkillData p)
            {
                return (p.SkillID == this.<>f__ref$503.unlocks[this.i].new_id);
            }
        }

        [CompilerGenerated]
        private sealed class <UpdateLearningsSkillCollabo>c__AnonStorey1F9
        {
            internal Json_CollaboSkill cs;

            public <UpdateLearningsSkillCollabo>c__AnonStorey1F9()
            {
                base..ctor();
                return;
            }

            internal bool <>m__D4(LearningSkill tls)
            {
                return (tls.iname == this.cs.iname);
            }
        }
    }
}

