namespace SRPG
{
    using GR;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public class JobData
    {
        public const int MAX_RANKUP_EQUIPS = 6;
        public const int MAX_LARNING_ABILITY = 8;
        public const int MAX_ABILITY_SLOT = 5;
        public const int MAX_ARTIFACT_SLOT = 3;
        public const int FIXED_ABILITY_SLOT_INDEX = 0;
        private UnitData mOwner;
        private long mUniqueID;
        private JobParam mJobParam;
        private OInt mRank;
        private SkillData mNormalAttackSkill;
        private SkillData mJobMaster;
        private EquipData[] mEquips;
        private List<AbilityData> mLearnAbilitys;
        public static EAbilitySlot[] ABILITY_SLOT_TYPES;
        private long[] mAbilitySlots;
        public static ArtifactTypes[] ARTIFACT_SLOT_TYPES;
        private long[] mArtifacts;
        private ArtifactData[] mArtifactDatas;
        private string mSelectSkin;
        private ArtifactData mSelectSkinData;
        [CompilerGenerated]
        private static Comparison<Json_Ability> <>f__am$cacheF;

        static JobData()
        {
            ArtifactTypes[] typesArray1;
            EAbilitySlot[] slotArray1;
            slotArray1 = new EAbilitySlot[5];
            slotArray1[2] = 2;
            slotArray1[3] = 1;
            slotArray1[4] = 1;
            ABILITY_SLOT_TYPES = slotArray1;
            typesArray1 = new ArtifactTypes[] { 1, 2, 3 };
            ARTIFACT_SLOT_TYPES = typesArray1;
            return;
        }

        public JobData()
        {
            int num;
            this.mRank = 0;
            this.mNormalAttackSkill = new SkillData();
            this.mEquips = new EquipData[6];
            this.mLearnAbilitys = new List<AbilityData>();
            this.mAbilitySlots = new long[5];
            this.mArtifacts = new long[3];
            this.mArtifactDatas = new ArtifactData[3];
            base..ctor();
            num = 0;
            goto Label_0070;
        Label_005F:
            this.mEquips[num] = new EquipData();
            num += 1;
        Label_0070:
            if (num < ((int) this.mEquips.Length))
            {
                goto Label_005F;
            }
            return;
        }

        [CompilerGenerated]
        private static int <Deserialize>m__49D(Json_Ability src, Json_Ability dsc)
        {
            return (int) (src.iid - dsc.iid);
        }

        [CompilerGenerated]
        private bool <GetSelectedSkinData>m__4A0(ArtifactParam a)
        {
            return (a.iname == this.mSelectSkin);
        }

        [CompilerGenerated]
        private bool <JobRankUp>m__49E(AbilityData p)
        {
            return (p.AbilityID == this.mJobParam.fixed_ability);
        }

        public bool CanAllEquip(ref int cost, ref Dictionary<string, int> equips, ref Dictionary<string, int> consumes, NeedEquipItemList item_list)
        {
            return MonoSingleton<GameManager>.Instance.Player.CheckEnableCreateEquipItemAll(this.Owner, this.Equips, consumes, cost, item_list);
        }

        public bool CanAllEquip(ref int cost, ref Dictionary<string, int> equips, ref Dictionary<string, int> consumes, ref int target_rank, ref bool can_jobmaster, ref bool can_jobmax, NeedEquipItemList item_list, bool all)
        {
            if (all == null)
            {
                goto Label_002C;
            }
            return MonoSingleton<GameManager>.Instance.Player.CheckEnable2(this.Owner, this.Equips, consumes, cost, target_rank, can_jobmaster, can_jobmax, null);
        Label_002C:
            return MonoSingleton<GameManager>.Instance.Player.CheckEnableCreateEquipItemAll(this.Owner, this.Equips, consumes, cost, item_list);
        }

        public bool CheckEnableEquipSlot(int index)
        {
            int num;
            if (index < 0)
            {
                goto Label_0015;
            }
            if (index < ((int) this.mEquips.Length))
            {
                goto Label_0017;
            }
        Label_0015:
            return 0;
        Label_0017:
            if (this.mEquips[index] != null)
            {
                goto Label_0026;
            }
            return 0;
        Label_0026:
            if (this.mEquips[index].IsEquiped() == null)
            {
                goto Label_003A;
            }
            return 0;
        Label_003A:
            if (this.Owner == null)
            {
                goto Label_0060;
            }
            if (this.GetEnableEquipUnitLevel(index) <= this.Owner.Lv)
            {
                goto Label_0060;
            }
            return 0;
        Label_0060:
            return 1;
        }

        public bool CheckEquipArtifact(int slot, ArtifactData artifact)
        {
            int num;
            ArtifactParam param;
            if (artifact == null)
            {
                goto Label_0026;
            }
            if (this.mArtifacts == null)
            {
                goto Label_0026;
            }
            if (slot < 0)
            {
                goto Label_0026;
            }
            if (slot < ((int) this.mArtifacts.Length))
            {
                goto Label_0028;
            }
        Label_0026:
            return 0;
        Label_0028:
            if (this.Owner != null)
            {
                goto Label_0035;
            }
            return 0;
        Label_0035:
            num = Array.IndexOf<JobData>(this.Owner.Jobs, this);
            if (num < 0)
            {
                goto Label_0060;
            }
            if (artifact.CheckEnableEquip(this.Owner, num) != null)
            {
                goto Label_0062;
            }
        Label_0060:
            return 0;
        Label_0062:
            param = artifact.ArtifactParam;
            if (param.type == 3)
            {
                goto Label_0093;
            }
            if (param.type == ARTIFACT_SLOT_TYPES[slot])
            {
                goto Label_0093;
            }
            DebugUtility.LogError("ArtifactSlot mismatch");
            return 0;
        Label_0093:
            return 1;
        }

        public bool CheckJobMaster()
        {
            bool flag;
            int num;
            EquipData data;
            if (MonoSingleton<GameManager>.Instance.MasterParam.FixParam.IsJobMaster != null)
            {
                goto Label_001B;
            }
            return 0;
        Label_001B:
            if (this.Equips != null)
            {
                goto Label_0028;
            }
            return 0;
        Label_0028:
            if (this.Rank >= JobParam.MAX_JOB_RANK)
            {
                goto Label_003A;
            }
            return 0;
        Label_003A:
            flag = 1;
            num = 0;
            goto Label_0073;
        Label_0043:
            data = this.Equips[num];
            if (data == null)
            {
                goto Label_0068;
            }
            if (data.IsValid() == null)
            {
                goto Label_0068;
            }
            if (data.IsEquiped() != null)
            {
                goto Label_006F;
            }
        Label_0068:
            flag = 0;
            goto Label_0081;
        Label_006F:
            num += 1;
        Label_0073:
            if (num < ((int) this.Equips.Length))
            {
                goto Label_0043;
            }
        Label_0081:
            return flag;
        }

        public bool CheckJobRankUp(UnitData self, bool canCreate, bool useCommon)
        {
            int num;
            int num2;
            PlayerData data;
            NeedEquipItemList list;
            int num3;
            if (this.Param != null)
            {
                goto Label_000D;
            }
            return 0;
        Label_000D:
            num = this.GetJobRankCap(self);
            if (this.Rank < num)
            {
                goto Label_0025;
            }
            return 0;
        Label_0025:
            data = MonoSingleton<GameManager>.Instance.Player;
            list = new NeedEquipItemList();
            if (canCreate == null)
            {
                goto Label_006D;
            }
            if (data.CheckEnableCreateEquipItemAll(self, this.mEquips, (useCommon == null) ? null : list) != null)
            {
                goto Label_00A8;
            }
            if (list.IsEnoughCommon() != null)
            {
                goto Label_00A8;
            }
            return 0;
            goto Label_00A8;
        Label_006D:
            num3 = 0;
            goto Label_00A0;
        Label_0075:
            if (this.mEquips[num3] != null)
            {
                goto Label_0085;
            }
            return 0;
        Label_0085:
            if (this.mEquips[num3].IsEquiped() != null)
            {
                goto Label_009A;
            }
            return 0;
        Label_009A:
            num3 += 1;
        Label_00A0:
            if (num3 < 6)
            {
                goto Label_0075;
            }
        Label_00A8:
            return 1;
        }

        public void Deserialize(UnitData owner, Json_Job json)
        {
            int num;
            int num2;
            int num3;
            AbilityData data;
            string str;
            long num4;
            int num5;
            int num6;
            int num7;
            int num8;
            int num9;
            ArtifactData data2;
            int num10;
            ArtifactData data3;
            Json_Artifact artifact;
            if (json != null)
            {
                goto Label_000C;
            }
            throw new InvalidJSONException();
        Label_000C:
            this.mJobParam = MonoSingleton<GameManager>.GetInstanceDirect().GetJobParam(json.iname);
            this.mUniqueID = json.iid;
            this.mRank = json.rank;
            this.mOwner = owner;
            this.mSelectSkin = json.cur_skin;
            num = 0;
            goto Label_0082;
        Label_0059:
            this.mEquips[num].Setup(this.mJobParam.GetRankupItemID(this.mRank, num));
            num += 1;
        Label_0082:
            if (num < ((int) this.mEquips.Length))
            {
                goto Label_0059;
            }
            if (json.equips == null)
            {
                goto Label_00C9;
            }
            num2 = 0;
            goto Label_00BB;
        Label_00A2:
            this.mEquips[num2].Equip(json.equips[num2]);
            num2 += 1;
        Label_00BB:
            if (num2 < ((int) json.equips.Length))
            {
                goto Label_00A2;
            }
        Label_00C9:
            if (string.IsNullOrEmpty(this.Param.atkskill[0]) != null)
            {
                goto Label_0100;
            }
            this.mNormalAttackSkill.Setup(this.Param.atkskill[0], 1, 1, null);
            goto Label_0125;
        Label_0100:
            this.mNormalAttackSkill.Setup(this.Param.atkskill[owner.UnitParam.element], 1, 1, null);
        Label_0125:
            if (string.IsNullOrEmpty(this.Param.master) != null)
            {
                goto Label_0182;
            }
            if (MonoSingleton<GameManager>.Instance.MasterParam.FixParam.IsJobMaster == null)
            {
                goto Label_0182;
            }
            if (this.mJobMaster != null)
            {
                goto Label_0169;
            }
            this.mJobMaster = new SkillData();
        Label_0169:
            this.mJobMaster.Setup(this.Param.master, 1, 1, null);
        Label_0182:
            if (json.abils == null)
            {
                goto Label_0220;
            }
            if (<>f__am$cacheF != null)
            {
                goto Label_01AB;
            }
            <>f__am$cacheF = new Comparison<Json_Ability>(JobData.<Deserialize>m__49D);
        Label_01AB:
            Array.Sort<Json_Ability>(json.abils, <>f__am$cacheF);
            num3 = 0;
            goto Label_0212;
        Label_01BC:
            data = new AbilityData();
            str = json.abils[num3].iname;
            num4 = json.abils[num3].iid;
            num5 = json.abils[num3].exp;
            data.Setup(this.mOwner, num4, str, num5, 0);
            this.mLearnAbilitys.Add(data);
            num3 += 1;
        Label_0212:
            if (num3 < ((int) json.abils.Length))
            {
                goto Label_01BC;
            }
        Label_0220:
            Array.Clear(this.mAbilitySlots, 0, (int) this.mAbilitySlots.Length);
            if (json.select == null)
            {
                goto Label_0297;
            }
            if (json.select.abils == null)
            {
                goto Label_0297;
            }
            num6 = 0;
            goto Label_0274;
        Label_0257:
            this.mAbilitySlots[num6] = json.select.abils[num6];
            num6 += 1;
        Label_0274:
            if (num6 >= ((int) json.select.abils.Length))
            {
                goto Label_0297;
            }
            if (num6 < ((int) this.mAbilitySlots.Length))
            {
                goto Label_0257;
            }
        Label_0297:
            num7 = 0;
            goto Label_02AF;
        Label_029F:
            this.mArtifactDatas[num7] = null;
            num7 += 1;
        Label_02AF:
            if (num7 < ((int) this.mArtifactDatas.Length))
            {
                goto Label_029F;
            }
            Array.Clear(this.mArtifacts, 0, (int) this.mArtifacts.Length);
            if (json.select == null)
            {
                goto Label_0335;
            }
            if (json.select.artifacts == null)
            {
                goto Label_0335;
            }
            num8 = 0;
            goto Label_0312;
        Label_02F5:
            this.mArtifacts[num8] = json.select.artifacts[num8];
            num8 += 1;
        Label_0312:
            if (num8 >= ((int) json.select.artifacts.Length))
            {
                goto Label_0335;
            }
            if (num8 < ((int) this.mArtifacts.Length))
            {
                goto Label_02F5;
            }
        Label_0335:
            if (json.artis == null)
            {
                goto Label_03BD;
            }
            num9 = 0;
            goto Label_03AE;
        Label_0348:
            data2 = null;
            if (json.artis[num9] != null)
            {
                goto Label_035E;
            }
            goto Label_03A8;
        Label_035E:
            num10 = Array.IndexOf<long>(this.mArtifacts, json.artis[num9].iid);
            if (num10 >= 0)
            {
                goto Label_0386;
            }
            goto Label_03A8;
        Label_0386:
            data2 = new ArtifactData();
            data2.Deserialize(json.artis[num9]);
            this.mArtifactDatas[num10] = data2;
        Label_03A8:
            num9 += 1;
        Label_03AE:
            if (num9 < ((int) json.artis.Length))
            {
                goto Label_0348;
            }
        Label_03BD:
            if (string.IsNullOrEmpty(json.cur_skin) != null)
            {
                goto Label_03F9;
            }
            data3 = new ArtifactData();
            artifact = new Json_Artifact();
            artifact.iname = json.cur_skin;
            data3.Deserialize(artifact);
            this.mSelectSkinData = data3;
        Label_03F9:
            return;
        }

        public bool Equip(int index)
        {
            string str;
            if (this.CheckEnableEquipSlot(index) == null)
            {
                goto Label_0039;
            }
            str = this.GetRankupItemID(this.Rank, index);
            this.mEquips[index].Setup(str);
            this.mEquips[index].Equip(null);
            return 1;
        Label_0039:
            return 0;
        }

        public int FindEquipSlotByItemID(string iname)
        {
            int num;
            int num2;
            string str;
            num = -1;
            num2 = 0;
            goto Label_0043;
        Label_0009:
            str = this.GetRankupItemID(this.Rank, num2);
            if (string.IsNullOrEmpty(str) == null)
            {
                goto Label_0027;
            }
            goto Label_003F;
        Label_0027:
            if ((str != iname) == null)
            {
                goto Label_0038;
            }
            goto Label_003F;
        Label_0038:
            num = num2;
            goto Label_004A;
        Label_003F:
            num2 += 1;
        Label_0043:
            if (num2 < 6)
            {
                goto Label_0009;
            }
        Label_004A:
            return num;
        }

        public static int GetArtifactSlotIndex(ArtifactTypes type)
        {
            int num;
            num = 0;
            goto Label_001A;
        Label_0007:
            if (type != ARTIFACT_SLOT_TYPES[num])
            {
                goto Label_0016;
            }
            return num;
        Label_0016:
            num += 1;
        Label_001A:
            if (num < ((int) ARTIFACT_SLOT_TYPES.Length))
            {
                goto Label_0007;
            }
            return -1;
        }

        public SkillData GetAttackSkill()
        {
            return this.mNormalAttackSkill;
        }

        public int GetEnableEquipUnitLevel(int slot)
        {
            if (this.mEquips[slot] == null)
            {
                goto Label_001F;
            }
            if (this.mEquips[slot].ItemParam != null)
            {
                goto Label_0021;
            }
        Label_001F:
            return 0;
        Label_0021:
            return this.mEquips[slot].ItemParam.equipLv;
        }

        public int GetJobChangeCost(int rank)
        {
            return ((this.Param == null) ? 0 : this.Param.GetJobChangeCost(rank));
        }

        public int[] GetJobChangeItemNums(int rank)
        {
            return ((this.Param == null) ? null : this.Param.GetJobChangeItemNums(rank));
        }

        public string[] GetJobChangeItems(int rank)
        {
            return ((this.Param == null) ? null : this.Param.GetJobChangeItems(rank));
        }

        public int GetJobRankAvoidRate()
        {
            return this.GetJobRankAvoidRate(this.Rank);
        }

        public int GetJobRankAvoidRate(int rank)
        {
            return ((this.Param == null) ? 0 : this.Param.GetJobRankAvoidRate(rank));
        }

        public int GetJobRankCap(UnitData self)
        {
            return JobParam.GetJobRankCap(self.Rarity);
        }

        public int GetJobRankInitJewelRate()
        {
            return this.GetJobRankInitJewelRate(this.Rank);
        }

        public int GetJobRankInitJewelRate(int rank)
        {
            return ((this.Param == null) ? 0 : this.Param.GetJobRankInitJewelRate(rank));
        }

        public StatusParam GetJobRankStatus()
        {
            return this.GetJobRankStatus(this.Rank);
        }

        public StatusParam GetJobRankStatus(int rank)
        {
            return ((this.Param == null) ? null : this.Param.GetJobRankStatus(rank));
        }

        public BaseStatus GetJobTransfarStatus(EElement element)
        {
            return this.GetJobTransfarStatus(this.Rank, element);
        }

        public BaseStatus GetJobTransfarStatus(int rank, EElement element)
        {
            return ((this.Param == null) ? null : this.Param.GetJobTransfarStatus(rank, element));
        }

        public OString[] GetLearningAbilitys(int rank)
        {
            return ((this.Param == null) ? null : this.Param.GetLearningAbilitys(rank));
        }

        public int GetRankupCost(int rank)
        {
            return ((this.Param == null) ? 0 : this.Param.GetRankupCost(rank));
        }

        public string GetRankupItemID(int rank, int index)
        {
            return ((this.Param == null) ? null : this.Param.GetRankupItemID(rank, index));
        }

        public string[] GetRankupItems(int rank)
        {
            return ((this.Param == null) ? null : this.Param.GetRankupItems(rank));
        }

        public ArtifactData GetSelectedSkinData()
        {
            ArtifactParam param;
            ArtifactData data;
            Json_Artifact artifact;
            if (this.mSelectSkinData == null)
            {
                goto Label_0032;
            }
            if ((this.mSelectSkin == this.mSelectSkinData.ArtifactParam.iname) == null)
            {
                goto Label_0032;
            }
            return this.mSelectSkinData;
        Label_0032:
            param = Array.Find<ArtifactParam>(MonoSingleton<GameManager>.Instance.MasterParam.Artifacts.ToArray(), new Predicate<ArtifactParam>(this.<GetSelectedSkinData>m__4A0));
            if (param != null)
            {
                goto Label_0060;
            }
            return null;
        Label_0060:
            data = new ArtifactData();
            artifact = new Json_Artifact();
            artifact.iname = param.iname;
            data.Deserialize(artifact);
            this.mSelectSkinData = data;
            return this.mSelectSkinData;
        }

        public unsafe void JobRankUp()
        {
            int num;
            AbilityData data;
            List<AbilityData> list;
            string str;
            long num2;
            int num3;
            int num4;
            Exception exception;
            OString[] strArray;
            int num5;
            AbilityData data2;
            string str2;
            List<AbilityData> list2;
            long num6;
            int num7;
            int num8;
            Exception exception2;
            <JobRankUp>c__AnonStorey3EA storeyea;
            this.mRank = OInt.op_Increment(this.mRank);
            num = 0;
            goto Label_0037;
        Label_0018:
            this.mEquips[num].Setup(this.GetRankupItemID(this.Rank, num));
            num += 1;
        Label_0037:
            if (num < ((int) this.mEquips.Length))
            {
                goto Label_0018;
            }
            if (this.mRank != 1)
            {
                goto Label_011E;
            }
            if (string.IsNullOrEmpty(this.mJobParam.fixed_ability) != null)
            {
                goto Label_011E;
            }
            if (this.mLearnAbilitys.Find(new Predicate<AbilityData>(this.<JobRankUp>m__49E)) != null)
            {
                goto Label_011E;
            }
            data = new AbilityData();
            list = this.mOwner.GetAllLearnedAbilities(0);
            str = this.mJobParam.fixed_ability;
            num2 = 0L;
            num3 = 0;
            num4 = 0;
            goto Label_00D1;
        Label_00B5:
            num2 = Math.Max(num2, list[num4].UniqueID);
            num4 += 1;
        Label_00D1:
            if (num4 < list.Count)
            {
                goto Label_00B5;
            }
            num2 += 1L;
        Label_00E5:
            try
            {
                data.Setup(this.mOwner, num2, str, num3, 0);
                this.mLearnAbilitys.Add(data);
                this.SetAbilitySlot(0, data);
                goto Label_011E;
            }
            catch (Exception exception1)
            {
            Label_0110:
                exception = exception1;
                DebugUtility.LogException(exception);
                goto Label_011E;
            }
        Label_011E:
            strArray = this.GetLearningAbilitys(this.mRank);
            if (strArray == null)
            {
                goto Label_023D;
            }
            num5 = 0;
            goto Label_0232;
        Label_0140:
            storeyea = new <JobRankUp>c__AnonStorey3EA();
            storeyea.abilityID = *(&(strArray[num5]));
            if (string.IsNullOrEmpty(storeyea.abilityID) == null)
            {
                goto Label_0177;
            }
            goto Label_022C;
        Label_0177:
            if (this.mLearnAbilitys.Find(new Predicate<AbilityData>(storeyea.<>m__49F)) == null)
            {
                goto Label_0199;
            }
            goto Label_022C;
        Label_0199:
            data2 = new AbilityData();
            str2 = storeyea.abilityID;
            list2 = this.mOwner.GetAllLearnedAbilities(0);
            num6 = 0L;
            num7 = 0;
            num8 = 0;
            goto Label_01E3;
        Label_01C6:
            num6 = Math.Max(num6, list2[num8].UniqueID);
            num8 += 1;
        Label_01E3:
            if (num8 < list2.Count)
            {
                goto Label_01C6;
            }
            num6 += 1L;
        Label_01F8:
            try
            {
                data2.Setup(this.mOwner, num6, str2, num7, 0);
                this.mLearnAbilitys.Add(data2);
                goto Label_022C;
            }
            catch (Exception exception3)
            {
            Label_021E:
                exception2 = exception3;
                DebugUtility.LogException(exception2);
                goto Label_022C;
            }
        Label_022C:
            num5 += 1;
        Label_0232:
            if (num5 < ((int) strArray.Length))
            {
                goto Label_0140;
            }
        Label_023D:
            return;
        }

        public void SetAbilitySlot(int slot, AbilityData ability)
        {
            long num;
            AbilityParam param;
            DebugUtility.Assert((slot < 0) ? 0 : (slot < ((int) this.mAbilitySlots.Length)), "EquipAbility Out of Length");
            num = 0L;
            if (ability == null)
            {
                goto Label_006F;
            }
            param = ability.Param;
            if (param.slot == ABILITY_SLOT_TYPES[slot])
            {
                goto Label_004C;
            }
            DebugUtility.LogError("指定スロットに対応するアビリティではない");
            return;
        Label_004C:
            if (slot == null)
            {
                goto Label_0068;
            }
            if (param.is_fixed == null)
            {
                goto Label_0068;
            }
            DebugUtility.LogError("指定スロットには固定アビリティは装備不可能");
            return;
        Label_0068:
            num = ability.UniqueID;
        Label_006F:
            this.mAbilitySlots[slot] = num;
            return;
        }

        public bool SetEquipArtifact(int slot, ArtifactData artifact)
        {
            long num;
            DebugUtility.Assert((slot < 0) ? 0 : (slot < ((int) this.mArtifacts.Length)), "SetArtifact Out of Length");
            num = 0L;
            if (artifact == null)
            {
                goto Label_0043;
            }
            if (this.CheckEquipArtifact(slot, artifact) != null)
            {
                goto Label_0037;
            }
            return 0;
        Label_0037:
            num = artifact.UniqueID;
        Label_0043:
            this.mArtifacts[slot] = num;
            this.mArtifactDatas[slot] = artifact;
            return 1;
        }

        public void UnlockSkillAll()
        {
            int num;
            num = 0;
            goto Label_001E;
        Label_0007:
            this.LearnAbilitys[num].UpdateLearningsSkill(0, null);
            num += 1;
        Label_001E:
            if (num < this.LearnAbilitys.Count)
            {
                goto Label_0007;
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

        public JobParam Param
        {
            get
            {
                return this.mJobParam;
            }
        }

        public long UniqueID
        {
            get
            {
                return this.mUniqueID;
            }
            set
            {
                this.mUniqueID = value;
                return;
            }
        }

        public string JobID
        {
            get
            {
                return ((this.Param == null) ? null : this.Param.iname);
            }
        }

        public string Name
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

        public JobTypes JobType
        {
            get
            {
                return ((this.Param == null) ? 1 : this.Param.type);
            }
        }

        public RoleTypes RoleType
        {
            get
            {
                return ((this.Param == null) ? 1 : this.Param.role);
            }
        }

        public string JobResourceID
        {
            get
            {
                return ((this.Param == null) ? null : this.Param.model);
            }
        }

        public EquipData[] Equips
        {
            get
            {
                return this.mEquips;
            }
        }

        public List<AbilityData> LearnAbilitys
        {
            get
            {
                return this.mLearnAbilitys;
            }
        }

        public long[] AbilitySlots
        {
            get
            {
                return this.mAbilitySlots;
            }
        }

        public long[] Artifacts
        {
            get
            {
                return this.mArtifacts;
            }
        }

        public ArtifactData[] ArtifactDatas
        {
            get
            {
                return this.mArtifactDatas;
            }
        }

        public string SelectedSkin
        {
            get
            {
                return this.mSelectSkin;
            }
            set
            {
                this.mSelectSkin = value;
                return;
            }
        }

        public bool IsActivated
        {
            get
            {
                return (this.Rank > 0);
            }
        }

        public SkillData JobMaster
        {
            get
            {
                if (this.CheckJobMaster() == null)
                {
                    goto Label_0012;
                }
                return this.mJobMaster;
            Label_0012:
                return null;
            }
        }

        [CompilerGenerated]
        private sealed class <JobRankUp>c__AnonStorey3EA
        {
            internal string abilityID;

            public <JobRankUp>c__AnonStorey3EA()
            {
                base..ctor();
                return;
            }

            internal bool <>m__49F(AbilityData p)
            {
                return (p.AbilityID == this.abilityID);
            }
        }
    }
}

