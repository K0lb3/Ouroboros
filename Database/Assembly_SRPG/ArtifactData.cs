namespace SRPG
{
    using GR;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public class ArtifactData
    {
        private OLong mUniqueID;
        private SRPG.ArtifactParam mArtifactParam;
        private SRPG.RarityParam mRarityParam;
        private OInt mRarity;
        private OInt mLv;
        private OInt mExp;
        private bool mFavorite;
        private SkillData mEquipSkill;
        private SkillData mBattleEffectSkill;
        private List<AbilityData> mLearningAbilities;
        private static BaseStatus WorkScaleStatus;

        static ArtifactData()
        {
            WorkScaleStatus = new BaseStatus();
            return;
        }

        public ArtifactData()
        {
            this.mUniqueID = 0L;
            this.mRarity = 0;
            this.mLv = 1;
            base..ctor();
            return;
        }

        public bool CheckEnableCreate()
        {
            ItemData data;
            int num;
            int num2;
            data = this.KakeraData;
            if (data == null)
            {
                goto Label_0019;
            }
            if (data.Num > 0)
            {
                goto Label_001B;
            }
        Label_0019:
            return 0;
        Label_001B:
            if (this.GetKakeraCreateNum() <= data.Num)
            {
                goto Label_0030;
            }
            return 0;
        Label_0030:
            if (MonoSingleton<GameManager>.GetInstanceDirect().Player.Gold >= this.mRarityParam.ArtifactCreateCost)
            {
                goto Label_0058;
            }
            return 0;
        Label_0058:
            return 1;
        }

        public bool CheckEnableEquip(UnitData unit, int jobIndex)
        {
            return ((this.IsValid() == null) ? 0 : this.mArtifactParam.CheckEnableEquip(unit, jobIndex));
        }

        public RarityUpResults CheckEnableRarityUp()
        {
            RarityUpResults results;
            int num;
            results = 0;
            if (this.mRarity < this.RarityCap)
            {
                goto Label_0021;
            }
            results |= 8;
        Label_0021:
            if (this.mLv >= this.LvCap)
            {
                goto Label_0040;
            }
            results |= 1;
        Label_0040:
            if (this.GetKakeraNumForRarityUp() >= this.GetKakeraNeedNum())
            {
                goto Label_0055;
            }
            results |= 4;
        Label_0055:
            if (MonoSingleton<GameManager>.GetInstanceDirect().Player.Gold >= this.mRarityParam.ArtifactRarityUpCost)
            {
                goto Label_007F;
            }
            results |= 2;
        Label_007F:
            return results;
        }

        public unsafe bool CheckEquiped()
        {
            UnitData data;
            JobData data2;
            data = null;
            data2 = null;
            return MonoSingleton<GameManager>.Instance.Player.FindOwner(this, &data, &data2);
        }

        public bool ConsumeKakera(int num)
        {
            ItemData[] dataArray1;
            int num2;
            ItemData[] dataArray;
            int num3;
            int num4;
            int num5;
            num2 = this.GetKakeraNumForRarityUp();
            if (num >= 1)
            {
                goto Label_0010;
            }
            return 0;
        Label_0010:
            if (num2 >= num)
            {
                goto Label_0019;
            }
            return 0;
        Label_0019:
            dataArray1 = new ItemData[] { this.KakeraData, this.RarityKakeraData, this.CommonKakeraData };
            dataArray = dataArray1;
            num3 = num;
            num4 = 0;
            goto Label_0076;
        Label_0044:
            if (dataArray[num4] != null)
            {
                goto Label_0051;
            }
            goto Label_0072;
        Label_0051:
            if (num3 >= 1)
            {
                goto Label_005D;
            }
            goto Label_007F;
        Label_005D:
            num5 = Math.Min(dataArray[num4].Num, num3);
            num3 -= num5;
        Label_0072:
            num4 += 1;
        Label_0076:
            if (num4 < ((int) dataArray.Length))
            {
                goto Label_0044;
            }
        Label_007F:
            return (num3 < 1);
        }

        public ArtifactData Copy()
        {
            ArtifactData data;
            Json_Artifact artifact;
            data = new ArtifactData();
            artifact = new Json_Artifact();
            artifact.iname = this.mArtifactParam.iname;
            artifact.iid = this.mUniqueID;
            artifact.rare = this.mRarity;
            artifact.exp = this.mExp;
            artifact.fav = (this.mFavorite == null) ? 0 : 1;
            data.Deserialize(artifact);
            return data;
        }

        public void Deserialize(Json_Artifact json)
        {
            GameManager manager;
            if (json == null)
            {
                goto Label_0016;
            }
            if (string.IsNullOrEmpty(json.iname) == null)
            {
                goto Label_001D;
            }
        Label_0016:
            this.Reset();
            return;
        Label_001D:
            manager = MonoSingleton<GameManager>.GetInstanceDirect();
            this.mArtifactParam = manager.MasterParam.GetArtifactParam(json.iname);
            this.mUniqueID = json.iid;
            this.mRarity = Math.Min(Math.Max(json.rare, this.mArtifactParam.rareini), this.mArtifactParam.raremax);
            this.mRarityParam = manager.GetRarityParam(this.mRarity);
            this.mExp = json.exp;
            this.mLv = this.GetLevelFromExp(this.mExp);
            this.mFavorite = (json.fav == 0) == 0;
            this.UpdateEquipEffect();
            this.UpdateLearningAbilities(0);
            return;
        }

        public void GainExp(int exp)
        {
            int num;
            int num2;
            num = this.GetTotalExpFromLevel(this.GetLevelCap());
            num2 = this.mLv;
            this.mExp = Math.Min(this.mExp + exp, num);
            this.mLv = this.GetLevelFromExp(this.mExp);
            if (this.mLv == num2)
            {
                goto Label_006A;
            }
            this.LevelUp();
        Label_006A:
            return;
        }

        public void GainExp(ItemData item, int num)
        {
            ItemParam param;
            if (item == null)
            {
                goto Label_0012;
            }
            if (item.Num >= num)
            {
                goto Label_0013;
            }
        Label_0012:
            return;
        Label_0013:
            param = item.Param;
            if (param != null)
            {
                goto Label_0021;
            }
            return;
        Label_0021:
            if (param.type == 13)
            {
                goto Label_002F;
            }
            return;
        Label_002F:
            this.GainExp(param.value * num);
            item.Used(num);
            return;
        }

        public List<AbilityData> GetEnableAbitilies(UnitData unit, int job_index)
        {
            List<AbilityData> list;
            int num;
            if (this.mLearningAbilities == null)
            {
                goto Label_001B;
            }
            if (this.mLearningAbilities.Count != null)
            {
                goto Label_001D;
            }
        Label_001B:
            return null;
        Label_001D:
            list = new List<AbilityData>(this.mLearningAbilities.Count);
            num = 0;
            goto Label_0068;
        Label_0035:
            if (this.mLearningAbilities[num].CheckEnableUseAbility(unit, job_index) != null)
            {
                goto Label_0052;
            }
            goto Label_0064;
        Label_0052:
            list.Add(this.mLearningAbilities[num]);
        Label_0064:
            num += 1;
        Label_0068:
            if (num < this.mLearningAbilities.Count)
            {
                goto Label_0035;
            }
            return list;
        }

        public int GetGainExpCap()
        {
            return this.GetTotalExpFromLevel(this.GetLevelCap());
        }

        public unsafe void GetHomePassiveBuffStatus(ref BaseStatus fixed_status, ref BaseStatus scale_status, UnitData user, int job_index, bool bCheckCondition)
        {
            int num;
            AbilityData data;
            int num2;
            SkillData data2;
            *(fixed_status).Clear();
            *(scale_status).Clear();
            WorkScaleStatus.Clear();
            SkillData.GetHomePassiveBuffStatus(this.EquipSkill, 0, fixed_status, &WorkScaleStatus);
            *(scale_status).Add(WorkScaleStatus);
            if (this.mLearningAbilities != null)
            {
                goto Label_0042;
            }
            return;
        Label_0042:
            num = 0;
            goto Label_00D1;
        Label_0049:
            data = this.mLearningAbilities[num];
            if (user == null)
            {
                goto Label_0076;
            }
            if (data.CheckEnableUseAbility(user, job_index) != null)
            {
                goto Label_0076;
            }
            if (bCheckCondition != null)
            {
                goto Label_0076;
            }
            goto Label_00CD;
        Label_0076:
            if (data.Skills == null)
            {
                goto Label_00CD;
            }
            num2 = 0;
            goto Label_00BC;
        Label_0088:
            data2 = data.Skills[num2];
            WorkScaleStatus.Clear();
            SkillData.GetHomePassiveBuffStatus(data2, 0, fixed_status, &WorkScaleStatus);
            *(scale_status).Add(WorkScaleStatus);
            num2 += 1;
        Label_00BC:
            if (num2 < data.Skills.Count)
            {
                goto Label_0088;
            }
        Label_00CD:
            num += 1;
        Label_00D1:
            if (num < this.mLearningAbilities.Count)
            {
                goto Label_0049;
            }
            return;
        }

        public int GetKakeraChangeNum()
        {
            if (this.mRarityParam != null)
            {
                goto Label_000D;
            }
            return 0;
        Label_000D:
            return this.mRarityParam.ArtifactChangePieceNum;
        }

        public int GetKakeraCreateNum()
        {
            if (this.mRarityParam != null)
            {
                goto Label_000D;
            }
            return 0;
        Label_000D:
            return this.mRarityParam.ArtifactCreatePieceNum;
        }

        public List<ItemData> GetKakeraDataListForRarityUp()
        {
            Action<ItemData> action;
            <GetKakeraDataListForRarityUp>c__AnonStorey200 storey;
            storey = new <GetKakeraDataListForRarityUp>c__AnonStorey200();
            storey.result = new List<ItemData>();
            action = new Action<ItemData>(storey.<>m__DC);
            action(this.KakeraData);
            action(this.RarityKakeraData);
            action(this.CommonKakeraData);
            return storey.result;
        }

        public int GetKakeraNeedNum()
        {
            if (this.mRarityParam != null)
            {
                goto Label_000D;
            }
            return 0;
        Label_000D:
            return this.mRarityParam.ArtifactGouseiPieceNum;
        }

        public int GetKakeraNumForRarityUp()
        {
            Action<ItemData> action;
            <GetKakeraNumForRarityUp>c__AnonStorey1FF storeyff;
            storeyff = new <GetKakeraNumForRarityUp>c__AnonStorey1FF();
            storeyff.result = 0;
            action = new Action<ItemData>(storeyff.<>m__DB);
            action(this.KakeraData);
            action(this.RarityKakeraData);
            action(this.CommonKakeraData);
            return storeyff.result;
        }

        public int GetLevelCap()
        {
            if (this.mRarityParam != null)
            {
                goto Label_000D;
            }
            return 1;
        Label_000D:
            return this.mRarityParam.ArtifactLvCap;
        }

        public int GetLevelFromExp(int exp)
        {
            int num;
            int num2;
            num = StaticCalcLevelFromExp(exp);
            num2 = this.GetLevelCap();
            return Math.Min(num, num2);
        }

        public int GetNextExp()
        {
            int num;
            int num2;
            num2 = this.GetTotalExpFromLevel(this.mLv + 1) - this.mExp;
            return num2;
        }

        public unsafe int GetNextExpFromLevel(int lv)
        {
            int num;
            OInt[] numArray;
            num = Math.Max(Math.Min(lv, this.GetLevelCap()) - 1, 0);
            numArray = MonoSingleton<GameManager>.GetInstanceDirect().MasterParam.GetArtifactExpTable();
            if (numArray != null)
            {
                goto Label_002D;
            }
            return 0;
        Label_002D:
            return *(&(numArray[num]));
        }

        public int GetSellPrice()
        {
            int num;
            int num2;
            num = this.mArtifactParam.sell;
            num2 = 100;
            if (this.mRarityParam == null)
            {
                goto Label_002B;
            }
            num2 = this.mRarityParam.ArtifactCostRate;
        Label_002B:
            num = (num * num2) / 100;
            return num;
        }

        public int GetShowExp()
        {
            int num;
            int num2;
            num = this.GetTotalExpFromLevel(this.mLv);
            num2 = this.mExp - num;
            return num2;
        }

        public int GetTotalExpFromLevel(int lv)
        {
            int num;
            return StaticCalcExpFromLevel(Math.Min(lv, this.GetLevelCap()));
        }

        public bool IsValid()
        {
            return ((this.mArtifactParam == null) == 0);
        }

        public void LevelUp()
        {
            this.UpdateEquipEffect();
            this.UpdateLearningAbilities(0);
            return;
        }

        public void RarityUp()
        {
            int num;
            int num2;
            GameManager manager;
            int num3;
            int num4;
            OInt num5;
            if (this.mLv >= this.GetLevelCap())
            {
                goto Label_0017;
            }
            return;
        Label_0017:
            num = this.GetKakeraNumForRarityUp();
            num2 = this.GetKakeraNeedNum();
            if (num >= num2)
            {
                goto Label_002D;
            }
            return;
        Label_002D:
            if (this.ConsumeKakera(num2) != null)
            {
                goto Label_0044;
            }
            DebugUtility.LogWarning("カケラが不足している場合");
            return;
        Label_0044:
            manager = MonoSingleton<GameManager>.GetInstanceDirect();
            num3 = this.mRarityParam.ArtifactRarityUpCost;
            if (manager.Player.Gold >= num3)
            {
                goto Label_0071;
            }
            return;
        Label_0071:
            manager.Player.GainGold(-num3);
            this.mRarity = Math.Min(Math.Max(this.mRarity = OInt.op_Increment(this.mRarity), this.mArtifactParam.rareini), this.mArtifactParam.raremax);
            this.mRarityParam = manager.GetRarityParam(this.mRarity);
            this.UpdateEquipEffect();
            this.UpdateLearningAbilities(1);
            return;
        }

        public void Reset()
        {
            this.mArtifactParam = null;
            this.mRarityParam = null;
            this.mRarity = 0;
            this.mExp = 0;
            this.mLv = 1;
            this.mUniqueID = 0L;
            this.mFavorite = 0;
            return;
        }

        public static unsafe int StaticCalcExpFromLevel(int lv)
        {
            int num;
            OInt[] numArray;
            int num2;
            num = 0;
            numArray = MonoSingleton<GameManager>.GetInstanceDirect().MasterParam.GetArtifactExpTable();
            num2 = 0;
            goto Label_0031;
        Label_0019:
            num += *(&(numArray[num2]));
            num2 += 1;
        Label_0031:
            if (num2 < lv)
            {
                goto Label_0019;
            }
            return num;
        }

        public static unsafe int StaticCalcLevelFromExp(int exp)
        {
            int num;
            int num2;
            OInt[] numArray;
            int num3;
            int num4;
            num = 0;
            num2 = 0;
            numArray = MonoSingleton<GameManager>.GetInstanceDirect().MasterParam.GetArtifactExpTable();
            num3 = ((int) numArray.Length) + 1;
            num4 = 0;
            goto Label_004F;
        Label_0022:
            num2 += *(&(numArray[num4 + 1]));
            if (num2 <= exp)
            {
                goto Label_0045;
            }
            goto Label_0057;
        Label_0045:
            num += 1;
            num4 += 1;
        Label_004F:
            if (num4 < num3)
            {
                goto Label_0022;
            }
        Label_0057:
            num += 1;
            return num;
        }

        private void UpdateEquipEffect()
        {
            GameManager manager;
            SRPG.RarityParam param;
            int num;
            param = MonoSingleton<GameManager>.GetInstanceDirect().GetRarityParam(this.mArtifactParam.raremax);
            num = 1;
            if (param == null)
            {
                goto Label_002C;
            }
            num = param.ArtifactLvCap;
        Label_002C:
            if (this.mArtifactParam.equip_effects == null)
            {
                goto Label_00D5;
            }
            if (this.mRarity < 0)
            {
                goto Label_00D5;
            }
            if (this.mRarity >= ((int) this.mArtifactParam.equip_effects.Length))
            {
                goto Label_00D5;
            }
            if (string.IsNullOrEmpty(this.mArtifactParam.equip_effects[this.mRarity]) != null)
            {
                goto Label_00DC;
            }
            if (this.mEquipSkill != null)
            {
                goto Label_00A1;
            }
            this.mEquipSkill = new SkillData();
        Label_00A1:
            this.mEquipSkill.Setup(this.mArtifactParam.equip_effects[this.mRarity], this.mLv, num, null);
            goto Label_00DC;
        Label_00D5:
            this.mEquipSkill = null;
        Label_00DC:
            if (this.mArtifactParam.attack_effects == null)
            {
                goto Label_0185;
            }
            if (this.mRarity < 0)
            {
                goto Label_0185;
            }
            if (this.mRarity >= ((int) this.mArtifactParam.attack_effects.Length))
            {
                goto Label_0185;
            }
            if (string.IsNullOrEmpty(this.mArtifactParam.attack_effects[this.mRarity]) != null)
            {
                goto Label_018C;
            }
            if (this.mBattleEffectSkill != null)
            {
                goto Label_0151;
            }
            this.mBattleEffectSkill = new SkillData();
        Label_0151:
            this.mBattleEffectSkill.Setup(this.mArtifactParam.attack_effects[this.mRarity], this.mLv, num, null);
            goto Label_018C;
        Label_0185:
            this.mBattleEffectSkill = null;
        Label_018C:
            return;
        }

        private void UpdateLearningAbilities(bool bRarityUp)
        {
            GameManager manager;
            int num;
            AbilityData data;
            <UpdateLearningAbilities>c__AnonStorey1FE storeyfe;
            if (this.IsValid() != null)
            {
                goto Label_000C;
            }
            return;
        Label_000C:
            if (this.mArtifactParam == null)
            {
                goto Label_0027;
            }
            if (this.mArtifactParam.abil_inames != null)
            {
                goto Label_0028;
            }
        Label_0027:
            return;
        Label_0028:
            if (this.mLearningAbilities != null)
            {
                goto Label_004B;
            }
            this.mLearningAbilities = new List<AbilityData>((int) this.mArtifactParam.abil_inames.Length);
        Label_004B:
            manager = MonoSingleton<GameManager>.GetInstanceDirect();
            num = 0;
            goto Label_0167;
        Label_0058:
            storeyfe = new <UpdateLearningAbilities>c__AnonStorey1FE();
            if (this.mLv >= this.mArtifactParam.abil_levels[num])
            {
                goto Label_0080;
            }
            goto Label_0163;
        Label_0080:
            if (this.mRarity >= this.mArtifactParam.abil_rareties[num])
            {
                goto Label_00A2;
            }
            goto Label_0163;
        Label_00A2:
            storeyfe.param = manager.GetAbilityParam(this.mArtifactParam.abil_inames[num]);
            if (storeyfe.param != null)
            {
                goto Label_00CB;
            }
            goto Label_0163;
        Label_00CB:
            data = this.mLearningAbilities.Find(new Predicate<AbilityData>(storeyfe.<>m__DA));
            if (data != null)
            {
                goto Label_013D;
            }
            data = new AbilityData();
            data.Setup(null, 0L, storeyfe.param.iname, this.mRarity, 0);
            data.IsNoneCategory = 1;
            data.IsHideList = this.mArtifactParam.abil_shows[num] == 0;
            this.mLearningAbilities.Add(data);
            goto Label_0163;
        Label_013D:
            if (bRarityUp == null)
            {
                goto Label_0163;
            }
            data.Setup(null, 0L, storeyfe.param.iname, this.mRarity, 0);
        Label_0163:
            num += 1;
        Label_0167:
            if (num < ((int) this.mArtifactParam.abil_inames.Length))
            {
                goto Label_0058;
            }
            return;
        }

        public int Exp
        {
            get
            {
                return this.mExp;
            }
        }

        public OLong UniqueID
        {
            get
            {
                return this.mUniqueID;
            }
        }

        public SRPG.ArtifactParam ArtifactParam
        {
            get
            {
                return this.mArtifactParam;
            }
        }

        public SRPG.RarityParam RarityParam
        {
            get
            {
                return this.mRarityParam;
            }
        }

        public OInt Rarity
        {
            get
            {
                return this.mRarity;
            }
        }

        public OInt RarityCap
        {
            get
            {
                return this.mArtifactParam.raremax;
            }
        }

        public OInt Lv
        {
            get
            {
                return this.mLv;
            }
        }

        public OInt LvCap
        {
            get
            {
                return this.GetLevelCap();
            }
        }

        public ItemParam Kakera
        {
            get
            {
                return MonoSingleton<GameManager>.Instance.GetItemParam(this.mArtifactParam.kakera);
            }
        }

        public ItemData KakeraData
        {
            get
            {
                return MonoSingleton<GameManager>.Instance.Player.FindItemDataByItemParam(this.Kakera);
            }
        }

        public ItemParam RarityKakera
        {
            get
            {
                FixParam param;
                param = MonoSingleton<GameManager>.Instance.MasterParam.FixParam;
                if (param.ArtifactRarePiece != null)
                {
                    goto Label_001D;
                }
                return null;
            Label_001D:
                if (((int) param.ArtifactRarePiece.Length) > this.Rarity)
                {
                    goto Label_0037;
                }
                return null;
            Label_0037:
                return MonoSingleton<GameManager>.Instance.GetItemParam(*(&(param.ArtifactRarePiece[this.Rarity])));
            }
        }

        public ItemData RarityKakeraData
        {
            get
            {
                return MonoSingleton<GameManager>.Instance.Player.FindItemDataByItemParam(this.RarityKakera);
            }
        }

        public ItemParam CommonKakera
        {
            get
            {
                return MonoSingleton<GameManager>.Instance.GetItemParam(MonoSingleton<GameManager>.Instance.MasterParam.FixParam.ArtifactCommonPiece);
            }
        }

        public ItemData CommonKakeraData
        {
            get
            {
                return MonoSingleton<GameManager>.Instance.Player.FindItemDataByItemParam(this.CommonKakera);
            }
        }

        public bool IsFavorite
        {
            get
            {
                return this.mFavorite;
            }
            set
            {
                this.mFavorite = value;
                return;
            }
        }

        public SkillData EquipSkill
        {
            get
            {
                return this.mEquipSkill;
            }
        }

        public SkillData BattleEffectSkill
        {
            get
            {
                return this.mBattleEffectSkill;
            }
        }

        public List<AbilityData> LearningAbilities
        {
            get
            {
                return this.mLearningAbilities;
            }
        }

        [CompilerGenerated]
        private sealed class <GetKakeraDataListForRarityUp>c__AnonStorey200
        {
            internal List<ItemData> result;

            public <GetKakeraDataListForRarityUp>c__AnonStorey200()
            {
                base..ctor();
                return;
            }

            internal void <>m__DC(ItemData itemData)
            {
                if (itemData == null)
                {
                    goto Label_0012;
                }
                this.result.Add(itemData);
            Label_0012:
                return;
            }
        }

        [CompilerGenerated]
        private sealed class <GetKakeraNumForRarityUp>c__AnonStorey1FF
        {
            internal int result;

            public <GetKakeraNumForRarityUp>c__AnonStorey1FF()
            {
                base..ctor();
                return;
            }

            internal void <>m__DB(ItemData itemData)
            {
                if (itemData == null)
                {
                    goto Label_0019;
                }
                this.result += itemData.Num;
            Label_0019:
                return;
            }
        }

        [CompilerGenerated]
        private sealed class <UpdateLearningAbilities>c__AnonStorey1FE
        {
            internal AbilityParam param;

            public <UpdateLearningAbilities>c__AnonStorey1FE()
            {
                base..ctor();
                return;
            }

            internal bool <>m__DA(AbilityData p)
            {
                return (p.Param == this.param);
            }
        }

        [Flags]
        public enum RarityUpResults
        {
            Success = 0,
            NoLv = 1,
            NoGold = 2,
            NoKakera = 4,
            RarityMaxed = 8
        }
    }
}

