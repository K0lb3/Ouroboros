namespace SRPG
{
    using GR;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class VersusDraftUnitParam
    {
        private long mId;
        private long mDraftUnitId;
        private int mWeight;
        private long mDummyIID;
        private string mUnitIName;
        private int mRare;
        private int mAwake;
        private int mLevel;
        private int mSelectJobIndex;
        private List<VersusDraftUnitJob> mVersusDraftUnitJobs;
        private Dictionary<string, VersusDraftUnitAbility> mAbilities;
        private List<VersusDraftUnitArtifact> mVersusDraftUnitArtifacts;
        private string mConceptCardIName;
        private int mConceptCardLevel;
        private List<VersusDraftUnitDoor> mVersusDraftUnitDoors;
        private string mMasterAbilityIName;
        private string mSkinIName;
        [CompilerGenerated]
        private bool <IsHidden>k__BackingField;

        public VersusDraftUnitParam()
        {
            base..ctor();
            return;
        }

        public bool Deserialize(long dummy_iid, JSON_VersusDraftUnitParam param)
        {
            VersusDraftUnitJob job;
            VersusDraftUnitJob job2;
            VersusDraftUnitJob job3;
            int num;
            VersusDraftUnitAbility ability;
            VersusDraftUnitAbility ability2;
            VersusDraftUnitAbility ability3;
            VersusDraftUnitAbility ability4;
            VersusDraftUnitAbility ability5;
            VersusDraftUnitArtifact artifact;
            VersusDraftUnitArtifact artifact2;
            VersusDraftUnitArtifact artifact3;
            VersusDraftUnitDoor door;
            VersusDraftUnitDoor door2;
            VersusDraftUnitDoor door3;
            VersusDraftUnitDoor door4;
            VersusDraftUnitDoor door5;
            VersusDraftUnitDoor door6;
            VersusDraftUnitDoor door7;
            VersusDraftUnitDoor door8;
            if (dummy_iid > 0L)
            {
                goto Label_000A;
            }
            return 0;
        Label_000A:
            if (param != null)
            {
                goto Label_0012;
            }
            return 0;
        Label_0012:
            this.mDummyIID = dummy_iid;
            this.mId = param.id;
            this.mDraftUnitId = param.draft_unit_id;
            this.mWeight = param.weight;
            this.mUnitIName = param.unit_iname;
            this.mRare = param.rare;
            this.mAwake = param.awake;
            this.mLevel = param.lv;
            this.mSelectJobIndex = param.select_job_idx;
            this.mVersusDraftUnitJobs = new List<VersusDraftUnitJob>();
            if (string.IsNullOrEmpty(param.job1_iname) != null)
            {
                goto Label_00CD;
            }
            job = new VersusDraftUnitJob();
            job.mIName = param.job1_iname;
            job.mRank = param.job1_lv;
            job.mEquip = param.job1_equip == 1;
            this.mVersusDraftUnitJobs.Add(job);
        Label_00CD:
            if (string.IsNullOrEmpty(param.job2_iname) != null)
            {
                goto Label_0116;
            }
            job2 = new VersusDraftUnitJob();
            job2.mIName = param.job2_iname;
            job2.mRank = param.job2_lv;
            job2.mEquip = param.job2_equip == 1;
            this.mVersusDraftUnitJobs.Add(job2);
        Label_0116:
            if (string.IsNullOrEmpty(param.job3_iname) != null)
            {
                goto Label_015F;
            }
            job3 = new VersusDraftUnitJob();
            job3.mIName = param.job3_iname;
            job3.mRank = param.job3_lv;
            job3.mEquip = param.job3_equip == 1;
            this.mVersusDraftUnitJobs.Add(job3);
        Label_015F:
            this.mAbilities = new Dictionary<string, VersusDraftUnitAbility>();
            num = 0;
            if (string.IsNullOrEmpty(param.abil1_iname) != null)
            {
                goto Label_01C8;
            }
            ability = new VersusDraftUnitAbility();
            ability.mIName = param.abil1_iname;
            ability.mLevel = param.abil1_lv;
            ability.mIID = (this.mDummyIID * 10L) + ((long) num);
            this.mAbilities.Add(param.abil1_iname, ability);
            num += 1;
        Label_01C8:
            if (string.IsNullOrEmpty(param.abil2_iname) != null)
            {
                goto Label_0224;
            }
            ability2 = new VersusDraftUnitAbility();
            ability2.mIName = param.abil2_iname;
            ability2.mLevel = param.abil2_lv;
            ability2.mIID = (this.mDummyIID * 10L) + ((long) num);
            this.mAbilities.Add(param.abil2_iname, ability2);
            num += 1;
        Label_0224:
            if (string.IsNullOrEmpty(param.abil3_iname) != null)
            {
                goto Label_0280;
            }
            ability3 = new VersusDraftUnitAbility();
            ability3.mIName = param.abil3_iname;
            ability3.mLevel = param.abil3_lv;
            ability3.mIID = (this.mDummyIID * 10L) + ((long) num);
            this.mAbilities.Add(param.abil3_iname, ability3);
            num += 1;
        Label_0280:
            if (string.IsNullOrEmpty(param.abil4_iname) != null)
            {
                goto Label_02DC;
            }
            ability4 = new VersusDraftUnitAbility();
            ability4.mIName = param.abil4_iname;
            ability4.mLevel = param.abil4_lv;
            ability4.mIID = (this.mDummyIID * 10L) + ((long) num);
            this.mAbilities.Add(param.abil4_iname, ability4);
            num += 1;
        Label_02DC:
            if (string.IsNullOrEmpty(param.abil5_iname) != null)
            {
                goto Label_0338;
            }
            ability5 = new VersusDraftUnitAbility();
            ability5.mIName = param.abil5_iname;
            ability5.mLevel = param.abil5_lv;
            ability5.mIID = (this.mDummyIID * 10L) + ((long) num);
            this.mAbilities.Add(param.abil5_iname, ability5);
            num += 1;
        Label_0338:
            this.mVersusDraftUnitArtifacts = new List<VersusDraftUnitArtifact>();
            if (string.IsNullOrEmpty(param.arti1_iname) != null)
            {
                goto Label_038E;
            }
            artifact = new VersusDraftUnitArtifact();
            artifact.mIName = param.arti1_iname;
            artifact.mRare = param.arti1_rare;
            artifact.mLevel = param.arti1_lv;
            this.mVersusDraftUnitArtifacts.Add(artifact);
        Label_038E:
            if (string.IsNullOrEmpty(param.arti2_iname) != null)
            {
                goto Label_03D9;
            }
            artifact2 = new VersusDraftUnitArtifact();
            artifact2.mIName = param.arti2_iname;
            artifact2.mRare = param.arti2_rare;
            artifact2.mLevel = param.arti2_lv;
            this.mVersusDraftUnitArtifacts.Add(artifact2);
        Label_03D9:
            if (string.IsNullOrEmpty(param.arti3_iname) != null)
            {
                goto Label_0424;
            }
            artifact3 = new VersusDraftUnitArtifact();
            artifact3.mIName = param.arti3_iname;
            artifact3.mRare = param.arti3_rare;
            artifact3.mLevel = param.arti3_lv;
            this.mVersusDraftUnitArtifacts.Add(artifact3);
        Label_0424:
            this.mConceptCardIName = (string.IsNullOrEmpty(param.card_iname) == null) ? param.card_iname : string.Empty;
            this.mConceptCardLevel = param.card_lv;
            this.mVersusDraftUnitDoors = new List<VersusDraftUnitDoor>();
            if (param.door1_lv <= 0)
            {
                goto Label_0496;
            }
            door = new VersusDraftUnitDoor();
            door.mCategory = 1;
            door.mLevel = param.door1_lv;
            this.mVersusDraftUnitDoors.Add(door);
        Label_0496:
            if (param.door2_lv <= 0)
            {
                goto Label_04CB;
            }
            door2 = new VersusDraftUnitDoor();
            door2.mCategory = 2;
            door2.mLevel = param.door2_lv;
            this.mVersusDraftUnitDoors.Add(door2);
        Label_04CB:
            if (param.door3_lv <= 0)
            {
                goto Label_0500;
            }
            door3 = new VersusDraftUnitDoor();
            door3.mCategory = 3;
            door3.mLevel = param.door3_lv;
            this.mVersusDraftUnitDoors.Add(door3);
        Label_0500:
            if (param.door4_lv <= 0)
            {
                goto Label_0535;
            }
            door4 = new VersusDraftUnitDoor();
            door4.mCategory = 4;
            door4.mLevel = param.door4_lv;
            this.mVersusDraftUnitDoors.Add(door4);
        Label_0535:
            if (param.door5_lv <= 0)
            {
                goto Label_056A;
            }
            door5 = new VersusDraftUnitDoor();
            door5.mCategory = 5;
            door5.mLevel = param.door5_lv;
            this.mVersusDraftUnitDoors.Add(door5);
        Label_056A:
            if (param.door6_lv <= 0)
            {
                goto Label_059F;
            }
            door6 = new VersusDraftUnitDoor();
            door6.mCategory = 6;
            door6.mLevel = param.door6_lv;
            this.mVersusDraftUnitDoors.Add(door6);
        Label_059F:
            if (param.door7_lv <= 0)
            {
                goto Label_05D4;
            }
            door7 = new VersusDraftUnitDoor();
            door7.mCategory = 7;
            door7.mLevel = param.door7_lv;
            this.mVersusDraftUnitDoors.Add(door7);
        Label_05D4:
            if (this.mVersusDraftUnitDoors.Count <= 0)
            {
                goto Label_060A;
            }
            door8 = new VersusDraftUnitDoor();
            door8.mCategory = 0;
            door8.mLevel = 1;
            this.mVersusDraftUnitDoors.Insert(0, door8);
        Label_060A:
            this.mMasterAbilityIName = param.master_abil;
            this.mSkinIName = param.skin;
            return 1;
        }

        public unsafe Json_Unit GetJson_Unit()
        {
            GameManager manager;
            UnitParam param;
            Json_Unit unit;
            int num;
            JobParam param2;
            JobRankParam param3;
            Json_Job job;
            int num2;
            Json_Equip equip;
            List<Json_Ability> list;
            List<string> list2;
            int num3;
            int num4;
            int num5;
            Json_Ability ability;
            int num6;
            Json_Artifact artifact;
            int num7;
            VersusDraftUnitAbility ability2;
            Dictionary<string, VersusDraftUnitAbility>.ValueCollection.Enumerator enumerator;
            ConceptCardParam param4;
            RarityParam param5;
            List<Json_Ability> list3;
            int num8;
            Json_Tobira tobira;
            TobiraParam param6;
            int num9;
            TobiraLearnAbilityParam param7;
            int num10;
            int num11;
            Json_Ability ability3;
            TobiraLearnAbilityParam.AddType type;
            manager = MonoSingleton<GameManager>.Instance;
            if ((manager == null) == null)
            {
                goto Label_0014;
            }
            return null;
        Label_0014:
            if (manager.GetUnitParam(this.mUnitIName) != null)
            {
                goto Label_0029;
            }
            return null;
        Label_0029:
            unit = new Json_Unit();
            unit.iid = this.mDraftUnitId;
            unit.iname = this.mUnitIName;
            unit.rare = this.mRare;
            unit.plus = this.mAwake;
            unit.exp = manager.MasterParam.GetUnitLevelExp(this.mLevel);
            unit.lv = this.mLevel;
            unit.fav = 0;
            unit.elem = 0;
            unit.select = new Json_UnitSelectable();
            unit.jobs = new Json_Job[this.mVersusDraftUnitJobs.Count];
            num = 0;
            goto Label_04E8;
        Label_00B8:
            param2 = manager.GetJobParam(this.mVersusDraftUnitJobs[num].mIName);
            if (param2 != null)
            {
                goto Label_00DD;
            }
            goto Label_04E4;
        Label_00DD:
            if (((int) param2.ranks.Length) > this.mVersusDraftUnitJobs[num].mRank)
            {
                goto Label_0101;
            }
            goto Label_04E4;
        Label_0101:
            param3 = param2.ranks[this.mVersusDraftUnitJobs[num].mRank];
            job = new Json_Job();
            job.iid = (this.mDummyIID * 10L) + ((long) num);
            job.iname = this.mVersusDraftUnitJobs[num].mIName;
            job.rank = this.mVersusDraftUnitJobs[num].mRank;
            job.equips = new Json_Equip[JobRankParam.MAX_RANKUP_EQUIPS];
            if (this.mVersusDraftUnitJobs[num].mEquip == null)
            {
                goto Label_01E2;
            }
            num2 = 0;
            goto Label_01D6;
        Label_0196:
            equip = new Json_Equip();
            equip.iid = (job.iid * 10L) + ((long) num2);
            equip.iname = param3.equips[num2];
            job.equips[num2] = equip;
            num2 += 1;
        Label_01D6:
            if (num2 < JobRankParam.MAX_RANKUP_EQUIPS)
            {
                goto Label_0196;
            }
        Label_01E2:
            job.select = new Json_JobSelectable();
            job.select.abils = new long[5];
            job.select.artifacts = new long[3];
            list = new List<Json_Ability>();
            list2 = new List<string>();
            list2.Add(param2.fixed_ability);
            num3 = 1;
            goto Label_02CB;
        Label_0236:
            if (((int) param2.ranks.Length) >= num3)
            {
                goto Label_024B;
            }
            goto Label_02C5;
        Label_024B:
            if (param2.ranks[num3] != null)
            {
                goto Label_025F;
            }
            goto Label_02C5;
        Label_025F:
            if (param2.ranks[num3].learnings != null)
            {
                goto Label_0278;
            }
            goto Label_02C5;
        Label_0278:
            num4 = 0;
            goto Label_02AD;
        Label_0280:
            list2.Add(*(&(param2.ranks[num3].learnings[num4])));
            num4 += 1;
        Label_02AD:
            if (num4 < ((int) param2.ranks[num3].learnings.Length))
            {
                goto Label_0280;
            }
        Label_02C5:
            num3 += 1;
        Label_02CB:
            if (num3 <= job.rank)
            {
                goto Label_0236;
            }
            num5 = 0;
            goto Label_037A;
        Label_02E1:
            ability = new Json_Ability();
            ability.iid = (job.iid * 10L) + ((long) num5);
            ability.iname = list2[num5];
            ability.exp = 0;
            list.Add(ability);
            if (this.mAbilities.ContainsKey(ability.iname) == null)
            {
                goto Label_0374;
            }
            ability.exp = this.mAbilities[ability.iname].mLevel - 1;
            ability.iid = this.mAbilities[ability.iname].mIID;
        Label_0374:
            num5 += 1;
        Label_037A:
            if (num5 < list2.Count)
            {
                goto Label_02E1;
            }
            job.abils = list.ToArray();
            if (num != this.mSelectJobIndex)
            {
                goto Label_04DA;
            }
            unit.select.job = job.iid;
            job.artis = new Json_Artifact[3];
            num6 = 0;
            goto Label_045E;
        Label_03C9:
            artifact = new Json_Artifact();
            artifact.iid = (job.iid * 100L) + ((long) num6);
            artifact.iname = this.mVersusDraftUnitArtifacts[num6].mIName;
            artifact.rare = this.mVersusDraftUnitArtifacts[num6].mRare;
            artifact.exp = ArtifactData.StaticCalcExpFromLevel(this.mVersusDraftUnitArtifacts[num6].mLevel);
            job.artis[num6] = artifact;
            job.select.artifacts[num6] = artifact.iid;
            num6 += 1;
        Label_045E:
            if (num6 < this.mVersusDraftUnitArtifacts.Count)
            {
                goto Label_03C9;
            }
            num7 = 0;
            enumerator = this.mAbilities.Values.GetEnumerator();
        Label_0485:
            try
            {
                goto Label_04AF;
            Label_048A:
                ability2 = &enumerator.Current;
                job.select.abils[num7] = ability2.mIID;
                num7 += 1;
            Label_04AF:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_048A;
                }
                goto Label_04CD;
            }
            finally
            {
            Label_04C0:
                ((Dictionary<string, VersusDraftUnitAbility>.ValueCollection.Enumerator) enumerator).Dispose();
            }
        Label_04CD:
            job.cur_skin = this.mSkinIName;
        Label_04DA:
            unit.jobs[num] = job;
        Label_04E4:
            num += 1;
        Label_04E8:
            if (num < this.mVersusDraftUnitJobs.Count)
            {
                goto Label_00B8;
            }
            if (string.IsNullOrEmpty(this.mMasterAbilityIName) != null)
            {
                goto Label_0542;
            }
            unit.abil = new Json_MasterAbility();
            unit.abil.iid = this.mDummyIID;
            unit.abil.iname = this.mMasterAbilityIName;
            unit.abil.exp = 0;
        Label_0542:
            param4 = manager.MasterParam.GetConceptCardParam(this.mConceptCardIName);
            if (param4 == null)
            {
                goto Label_05F6;
            }
            param5 = manager.GetRarityParam(param4.rare);
            unit.concept_card = new JSON_ConceptCard();
            unit.concept_card.iname = this.mConceptCardIName;
            unit.concept_card.iid = this.mDummyIID;
            unit.concept_card.plus = param5.ConceptCardAwakeCountMax;
            unit.concept_card.exp = manager.MasterParam.GetConceptCardLevelExp(param4.rare, this.mConceptCardLevel);
            unit.concept_card.trust = 0;
            unit.concept_card.trust_bonus = 0;
            unit.concept_card.fav = 0;
        Label_05F6:
            unit.doors = new Json_Tobira[this.mVersusDraftUnitDoors.Count];
            list3 = new List<Json_Ability>();
            num8 = 0;
            goto Label_0845;
        Label_061B:
            tobira = new Json_Tobira();
            tobira.category = this.mVersusDraftUnitDoors[num8].mCategory;
            tobira.lv = this.mVersusDraftUnitDoors[num8].mLevel;
            unit.doors[num8] = tobira;
            param6 = manager.MasterParam.GetTobiraParam(this.mUnitIName, this.mVersusDraftUnitDoors[num8].mCategory);
            if (param6 != null)
            {
                goto Label_0690;
            }
            goto Label_083F;
        Label_0690:
            num9 = 0;
            goto Label_082F;
        Label_0698:
            param7 = param6.LeanAbilityParam[num9];
            if (param7.Level <= tobira.lv)
            {
                goto Label_06BC;
            }
            goto Label_0829;
        Label_06BC:
            switch ((param7.AbilityAddType - 1))
            {
                case 0:
                    goto Label_06DF;

                case 1:
                    goto Label_07DD;

                case 2:
                    goto Label_0824;
            }
            goto Label_0829;
        Label_06DF:
            num10 = 0;
            goto Label_07C9;
        Label_06E7:
            num11 = 0;
            goto Label_07AC;
        Label_06EF:
            if ((unit.jobs[num10].abils[num11].iname == param7.AbilityOverwrite) == null)
            {
                goto Label_07A6;
            }
            unit.jobs[num10].abils[num11].iname = param7.AbilityIname;
            if (this.mAbilities.ContainsKey(param7.AbilityIname) == null)
            {
                goto Label_07A6;
            }
            unit.jobs[num10].abils[num11].iid = this.mAbilities[param7.AbilityIname].mIID;
            unit.jobs[num10].abils[num11].exp = this.mAbilities[param7.AbilityIname].mLevel - 1;
        Label_07A6:
            num11 += 1;
        Label_07AC:
            if (num11 < ((int) unit.jobs[num10].abils.Length))
            {
                goto Label_06EF;
            }
            num10 += 1;
        Label_07C9:
            if (num10 < ((int) unit.jobs.Length))
            {
                goto Label_06E7;
            }
            goto Label_0829;
        Label_07DD:
            ability3 = new Json_Ability();
            ability3.iid = ((this.mDummyIID * 100L) + ((long) (num8 * 10))) + ((long) num9);
            ability3.iname = param7.AbilityIname;
            ability3.exp = 0;
            list3.Add(ability3);
            goto Label_0829;
        Label_0824:;
        Label_0829:
            num9 += 1;
        Label_082F:
            if (num9 < ((int) param6.LeanAbilityParam.Length))
            {
                goto Label_0698;
            }
        Label_083F:
            num8 += 1;
        Label_0845:
            if (num8 < this.mVersusDraftUnitDoors.Count)
            {
                goto Label_061B;
            }
            unit.door_abils = list3.ToArray();
            return unit;
        }

        public bool IsHidden
        {
            [CompilerGenerated]
            get
            {
                return this.<IsHidden>k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this.<IsHidden>k__BackingField = value;
                return;
            }
        }

        public long Id
        {
            get
            {
                return this.mId;
            }
        }

        public long DraftUnitId
        {
            get
            {
                return this.mDraftUnitId;
            }
        }

        public int Weight
        {
            get
            {
                return this.mWeight;
            }
        }
    }
}

