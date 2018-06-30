namespace SRPG
{
    using GR;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using UnityEngine;

    public class ConceptCardData
    {
        private OLong mUniqueID;
        private OInt mLv;
        private OInt mExp;
        private OInt mTrust;
        private bool mFavorite;
        private bool mTrustBonus;
        private OInt mAwakeCount;
        private ConceptCardParam mConceptCardParam;
        private List<ConceptCardEquipEffect> mEquipEffects;
        private BaseStatus mFixStatus;
        private BaseStatus mScaleSatus;
        private ConceptCardListFilterWindow.Type mFilterType;

        public ConceptCardData()
        {
            this.mUniqueID = 0L;
            this.mFixStatus = new BaseStatus();
            this.mScaleSatus = new BaseStatus();
            base..ctor();
            return;
        }

        [CompilerGenerated]
        private bool <GetOwner>m__EB(UnitData u)
        {
            return ((u.ConceptCard == null) ? 0 : (u.ConceptCard.UniqueID == this.UniqueID));
        }

        private int CalcCardLevel()
        {
            return MonoSingleton<GameManager>.Instance.MasterParam.CalcConceptCardLevel(this.Rarity, this.mExp, this.CurrentLvCap);
        }

        public static ConceptCardData CreateConceptCardDataForDisplay(string iname)
        {
            ConceptCardData data;
            JSON_ConceptCard card;
            data = new ConceptCardData();
            card = new JSON_ConceptCard();
            card.iid = 1L;
            card.iname = iname;
            card.exp = 0;
            card.trust = 0;
            card.fav = 0;
            data.Deserialize(card);
            return data;
        }

        public bool Deserialize(JSON_ConceptCard json)
        {
            this.mUniqueID = json.iid;
            this.mExp = json.exp;
            this.mTrust = json.trust;
            this.mFavorite = (json.fav == 0) == 0;
            this.mTrustBonus = (json.trust_bonus == 0) == 0;
            this.mAwakeCount = json.plus;
            this.mConceptCardParam = MonoSingleton<GameManager>.Instance.MasterParam.GetConceptCardParam(json.iname);
            this.mLv = this.CalcCardLevel();
            this.UpdateEquipEffect();
            this.RefreshFilterType();
            return 1;
        }

        public bool Filter(ConceptCardListFilterWindow.Type filter_type)
        {
            return (((filter_type & this.mFilterType) == 0) == 0);
        }

        public bool FilterEnhance(string filter_iname)
        {
            return (this.mConceptCardParam.iname == filter_iname);
        }

        public List<ConceptCardEquipEffect> GetAbilities()
        {
            List<ConceptCardEquipEffect> list;
            int num;
            ConceptCardEquipEffect effect;
            list = new List<ConceptCardEquipEffect>();
            if (this.mEquipEffects != null)
            {
                goto Label_0013;
            }
            return list;
        Label_0013:
            num = 0;
            goto Label_0042;
        Label_001A:
            effect = this.mEquipEffects[num];
            if (effect.Ability != null)
            {
                goto Label_0037;
            }
            goto Label_003E;
        Label_0037:
            list.Add(effect);
        Label_003E:
            num += 1;
        Label_0042:
            if (num < this.mEquipEffects.Count)
            {
                goto Label_001A;
            }
            return list;
        }

        public List<ConceptCardSkillDatailData> GetAbilityDatailData()
        {
            List<ConceptCardSkillDatailData> list;
            List<ConceptCardEquipEffect> list2;
            List<ConceptCardEquipEffect> list3;
            int num;
            int num2;
            AbilityData data;
            int num3;
            ConceptCardDetailAbility.ShowType type;
            SkillParam param;
            <GetAbilityDatailData>c__AnonStorey204 storey;
            <GetAbilityDatailData>c__AnonStorey205 storey2;
            list = new List<ConceptCardSkillDatailData>();
            list2 = this.GetAbilities();
            list3 = this.GetCardSkills();
            num = 0;
            goto Label_007E;
        Label_001B:
            storey = new <GetAbilityDatailData>c__AnonStorey204();
            storey.skill = list3[num].CardSkill;
            if (storey.skill != null)
            {
                goto Label_0046;
            }
            goto Label_007A;
        Label_0046:
            if (list.FindIndex(new Predicate<ConceptCardSkillDatailData>(storey.<>m__E8)) > -1)
            {
                goto Label_007A;
            }
            list.Add(new ConceptCardSkillDatailData(list3[num], storey.skill, 1, null));
        Label_007A:
            num += 1;
        Label_007E:
            if (num < list3.Count)
            {
                goto Label_001B;
            }
            num2 = 0;
            goto Label_01A7;
        Label_0092:
            data = list2[num2].Ability;
            if (data != null)
            {
                goto Label_00AD;
            }
            goto Label_01A1;
        Label_00AD:
            num3 = 0;
            goto Label_0191;
        Label_00B5:
            storey2 = new <GetAbilityDatailData>c__AnonStorey205();
            storey2.learning_skill = data.LearningSkills[num3];
            if (storey2.learning_skill != null)
            {
                goto Label_00DE;
            }
            goto Label_018B;
        Label_00DE:
            type = 2;
            storey2.data = data.Skills.Find(new Predicate<SkillData>(storey2.<>m__E9));
            if (storey2.data != null)
            {
                goto Label_014F;
            }
            param = MonoSingleton<GameManager>.Instance.MasterParam.GetSkillParam(storey2.learning_skill.iname);
            storey2.data = new SkillData();
            storey2.data.Setup(param.iname, 1, 1, null);
            type = 3;
        Label_014F:
            if (list.FindIndex(new Predicate<ConceptCardSkillDatailData>(storey2.<>m__EA)) > -1)
            {
                goto Label_018B;
            }
            list.Add(new ConceptCardSkillDatailData(list2[num2], storey2.data, type, storey2.learning_skill));
        Label_018B:
            num3 += 1;
        Label_0191:
            if (num3 < ((int) data.LearningSkills.Length))
            {
                goto Label_00B5;
            }
        Label_01A1:
            num2 += 1;
        Label_01A7:
            if (num2 < list2.Count)
            {
                goto Label_0092;
            }
            return list;
        }

        public List<ConceptCardEquipEffect> GetCardSkills()
        {
            List<ConceptCardEquipEffect> list;
            int num;
            ConceptCardEquipEffect effect;
            list = new List<ConceptCardEquipEffect>();
            if (this.mEquipEffects != null)
            {
                goto Label_0013;
            }
            return list;
        Label_0013:
            num = 0;
            goto Label_0042;
        Label_001A:
            effect = this.mEquipEffects[num];
            if (effect.CardSkill != null)
            {
                goto Label_0037;
            }
            goto Label_003E;
        Label_0037:
            list.Add(effect);
        Label_003E:
            num += 1;
        Label_0042:
            if (num < this.mEquipEffects.Count)
            {
                goto Label_001A;
            }
            return list;
        }

        public List<BuffEffect> GetEnableCardSkillAddBuffs(UnitData unit, SkillParam parent_card_skill)
        {
            List<BuffEffect> list;
            List<ConceptCardEquipEffect> list2;
            int num;
            list = new List<BuffEffect>();
            if (unit != null)
            {
                goto Label_000E;
            }
            return list;
        Label_000E:
            list2 = this.GetEnableEquipEffects(unit, unit.Jobs[unit.JobIndex]);
            num = 0;
            goto Label_00B0;
        Label_002A:
            if (list2[num].CardSkill != null)
            {
                goto Label_0040;
            }
            goto Label_00AC;
        Label_0040:
            if ((list2[num].CardSkill.SkillID != parent_card_skill.iname) == null)
            {
                goto Label_0066;
            }
            goto Label_00AC;
        Label_0066:
            if (list2[num].AddCardSkillBuffEffectAwake == null)
            {
                goto Label_0089;
            }
            list.Add(list2[num].AddCardSkillBuffEffectAwake);
        Label_0089:
            if (list2[num].AddCardSkillBuffEffectLvMax == null)
            {
                goto Label_00AC;
            }
            list.Add(list2[num].AddCardSkillBuffEffectLvMax);
        Label_00AC:
            num += 1;
        Label_00B0:
            if (num < list2.Count)
            {
                goto Label_002A;
            }
            return list;
        }

        public List<SkillData> GetEnableCardSkills(UnitData unit)
        {
            List<SkillData> list;
            List<ConceptCardEquipEffect> list2;
            int num;
            list = new List<SkillData>();
            if (unit != null)
            {
                goto Label_000E;
            }
            return list;
        Label_000E:
            list2 = this.GetEnableEquipEffects(unit, unit.Jobs[unit.JobIndex]);
            num = 0;
            goto Label_0056;
        Label_002A:
            if (list2[num].CardSkill != null)
            {
                goto Label_0040;
            }
            goto Label_0052;
        Label_0040:
            list.Add(list2[num].CardSkill);
        Label_0052:
            num += 1;
        Label_0056:
            if (num < list2.Count)
            {
                goto Label_002A;
            }
            return list;
        }

        public List<ConceptCardEquipEffect> GetEnableEquipEffects(UnitData unit_data, JobData job_data)
        {
            List<ConceptCardEquipEffect> list;
            int num;
            list = new List<ConceptCardEquipEffect>();
            if (this.mEquipEffects != null)
            {
                goto Label_0013;
            }
            return list;
        Label_0013:
            num = 0;
            goto Label_0053;
        Label_001A:
            if (this.IsMatchConditions(unit_data.UnitParam, job_data, this.mEquipEffects[num].ConditionsIname) == null)
            {
                goto Label_004F;
            }
            list.Add(this.mEquipEffects[num]);
        Label_004F:
            num += 1;
        Label_0053:
            if (num < this.mEquipEffects.Count)
            {
                goto Label_001A;
            }
            return list;
        }

        public int GetExpToLevelMax()
        {
            int num;
            return (MonoSingleton<GameManager>.Instance.MasterParam.GetConceptCardLevelExp(this.Rarity, this.CurrentLvCap) - this.mExp);
        }

        public List<ConceptCardEquipEffect> GetNoConditionsEquipEffects()
        {
            List<ConceptCardEquipEffect> list;
            int num;
            list = new List<ConceptCardEquipEffect>();
            if (this.mEquipEffects == null)
            {
                goto Label_005A;
            }
            num = 0;
            goto Label_0049;
        Label_0018:
            if (this.mEquipEffects[num].GetCondition() == null)
            {
                goto Label_0033;
            }
            goto Label_0045;
        Label_0033:
            list.Add(this.mEquipEffects[num]);
        Label_0045:
            num += 1;
        Label_0049:
            if (num < this.mEquipEffects.Count)
            {
                goto Label_0018;
            }
        Label_005A:
            return list;
        }

        public unsafe BaseStatus GetNoConditionsEquipEffectStatus()
        {
            BaseStatus status;
            List<ConceptCardEquipEffect> list;
            int num;
            BaseStatus status2;
            BaseStatus status3;
            status = new BaseStatus();
            list = this.GetNoConditionsEquipEffects();
            num = 0;
            goto Label_0042;
        Label_0014:
            status2 = new BaseStatus();
            status3 = new BaseStatus();
            SkillData.GetHomePassiveBuffStatus(list[num].EquipSkill, 0, &status2, &status3);
            status.Add(status2);
            num += 1;
        Label_0042:
            if (num < list.Count)
            {
                goto Label_0014;
            }
            return status;
        }

        public UnitData GetOwner()
        {
            return MonoSingleton<GameManager>.Instance.Player.Units.Find(new Predicate<UnitData>(this.<GetOwner>m__EB));
        }

        public ConceptCardTrustRewardItemParam GetReward()
        {
            ConceptCardTrustRewardParam param;
            ConceptCardTrustRewardItemParam param2;
            param = MonoSingleton<GameManager>.Instance.MasterParam.GetTrustReward(this.Param.trust_reward);
            if (param == null)
            {
                goto Label_003A;
            }
            if (param.rewards == null)
            {
                goto Label_003A;
            }
            if (((int) param.rewards.Length) > 0)
            {
                goto Label_003C;
            }
        Label_003A:
            return null;
        Label_003C:
            param2 = param.rewards[0];
            return param2;
        }

        public long GetSortData(ConceptCardListSortWindow.Type type)
        {
            BaseStatus status;
            ConceptCardListSortWindow.Type type2;
            status = this.GetNoConditionsEquipEffectStatus();
            type2 = type;
            switch ((type2 - 1))
            {
                case 0:
                    goto Label_0091;

                case 1:
                    goto Label_009E;

                case 2:
                    goto Label_0031;

                case 3:
                    goto Label_00AB;

                case 4:
                    goto Label_0031;

                case 5:
                    goto Label_0031;

                case 6:
                    goto Label_0031;

                case 7:
                    goto Label_00BD;
            }
        Label_0031:
            if (type2 == 0x10)
            {
                goto Label_00CF;
            }
            if (type2 == 0x20)
            {
                goto Label_00E1;
            }
            if (type2 == 0x40)
            {
                goto Label_00F3;
            }
            if (type2 == 0x80)
            {
                goto Label_0105;
            }
            if (type2 == 0x100)
            {
                goto Label_0117;
            }
            if (type2 == 0x200)
            {
                goto Label_0085;
            }
            if (type2 == 0x400)
            {
                goto Label_0129;
            }
            if (type2 == 0x800)
            {
                goto Label_0136;
            }
            goto Label_0143;
        Label_0085:
            return this.UniqueID;
        Label_0091:
            return (long) this.Lv;
        Label_009E:
            return (long) this.Rarity;
        Label_00AB:
            return (long) status.param.atk;
        Label_00BD:
            return (long) status.param.def;
        Label_00CF:
            return (long) status.param.mag;
        Label_00E1:
            return (long) status.param.mnd;
        Label_00F3:
            return (long) status.param.spd;
        Label_0105:
            return (long) status.param.luk;
        Label_0117:
            return (long) status.param.hp;
        Label_0129:
            return (long) this.Trust;
        Label_0136:
            return (long) this.AwakeCount;
        Label_0143:
            return 0L;
        }

        public int GetSortParam(ParamTypes types)
        {
            if (this.mFixStatus != null)
            {
                goto Label_000D;
            }
            return 0;
        Label_000D:
            return this.mFixStatus[types];
        }

        public void GetStatus(ref BaseStatus fix, ref BaseStatus scale)
        {
            *(fix) = this.mFixStatus;
            *(scale) = this.mScaleSatus;
            return;
        }

        public int GetTrustToLevelMax()
        {
            int num;
            return (MonoSingleton<GameManager>.Instance.MasterParam.FixParam.CardTrustMax - this.mTrust);
        }

        public bool IsMatchConditions(UnitParam unit_param, JobData job_data, string conditions_iname)
        {
            ConceptCardConditionsParam param;
            if (unit_param != null)
            {
                goto Label_0008;
            }
            return 0;
        Label_0008:
            param = MonoSingleton<GameManager>.Instance.MasterParam.GetConceptCardConditions(conditions_iname);
            if (param != null)
            {
                goto Label_0021;
            }
            return 1;
        Label_0021:
            if (param.IsMatchElement(unit_param.element) != null)
            {
                goto Label_0034;
            }
            return 0;
        Label_0034:
            if (param.sex == null)
            {
                goto Label_0052;
            }
            if (param.sex == unit_param.sex)
            {
                goto Label_0052;
            }
            return 0;
        Label_0052:
            if (param.IsMatchBirth(unit_param.birthID) != null)
            {
                goto Label_0065;
            }
            return 0;
        Label_0065:
            if (param.IsMatchUnitGroup(unit_param.iname) != null)
            {
                goto Label_0078;
            }
            return 0;
        Label_0078:
            if (param.IsMatchJobGroup(job_data.JobID) != null)
            {
                goto Label_008B;
            }
            return 0;
        Label_008B:
            return 1;
        }

        private void RefreshFilterType()
        {
            int num;
            this.mFilterType = 0;
            switch (this.Rarity)
            {
                case 0:
                    goto Label_0032;

                case 1:
                    goto Label_0045;

                case 2:
                    goto Label_0058;

                case 3:
                    goto Label_006B;

                case 4:
                    goto Label_007E;
            }
            goto Label_0092;
        Label_0032:
            this.mFilterType |= 1;
            goto Label_0092;
        Label_0045:
            this.mFilterType |= 2;
            goto Label_0092;
        Label_0058:
            this.mFilterType |= 4;
            goto Label_0092;
        Label_006B:
            this.mFilterType |= 8;
            goto Label_0092;
        Label_007E:
            this.mFilterType |= 0x10;
        Label_0092:
            return;
        }

        public void SetBonus(bool bonus)
        {
            this.mTrustBonus = bonus;
            return;
        }

        public void SetTrust(int trust)
        {
            this.mTrust = trust;
            return;
        }

        private unsafe void UpdateEquipEffect()
        {
            int num;
            ConceptCardEquipEffect effect;
            this.mEquipEffects = null;
            if (this.mConceptCardParam.effects == null)
            {
                goto Label_009F;
            }
            if (((int) this.mConceptCardParam.effects.Length) <= 0)
            {
                goto Label_009F;
            }
            this.mEquipEffects = new List<ConceptCardEquipEffect>();
            num = 0;
            goto Label_008C;
        Label_003C:
            effect = new ConceptCardEquipEffect();
            effect.Setup(this.mConceptCardParam.effects[num], this.Lv, this.LvCap, this.AwakeCount, this.AwakeCountCap);
            this.mEquipEffects.Add(effect);
            num += 1;
        Label_008C:
            if (num < ((int) this.mConceptCardParam.effects.Length))
            {
                goto Label_003C;
            }
        Label_009F:
            this.UpdateStatus(&this.mFixStatus, &this.mScaleSatus);
            return;
        }

        public void UpdateStatus(ref BaseStatus fix, ref BaseStatus scale)
        {
            int num;
            if (this.EquipEffects != null)
            {
                goto Label_000C;
            }
            return;
        Label_000C:
            num = 0;
            goto Label_002A;
        Label_0013:
            this.EquipEffects[num].GetStatus(fix, scale);
            num += 1;
        Label_002A:
            if (num < this.EquipEffects.Count)
            {
                goto Label_0013;
            }
            return;
        }

        public OLong UniqueID
        {
            get
            {
                return this.mUniqueID;
            }
        }

        public OInt Rarity
        {
            get
            {
                return this.mConceptCardParam.rare;
            }
        }

        public OInt Lv
        {
            get
            {
                return this.mLv;
            }
        }

        public OInt Exp
        {
            get
            {
                return this.mExp;
            }
        }

        public OInt Trust
        {
            get
            {
                return this.mTrust;
            }
        }

        public bool Favorite
        {
            get
            {
                return this.mFavorite;
            }
        }

        public bool TrustBonus
        {
            get
            {
                return this.mTrustBonus;
            }
        }

        public ConceptCardParam Param
        {
            get
            {
                return this.mConceptCardParam;
            }
        }

        public List<ConceptCardEquipEffect> EquipEffects
        {
            get
            {
                return this.mEquipEffects;
            }
        }

        public OInt CurrentLvCap
        {
            get
            {
                return (this.mConceptCardParam.lvcap + this.AwakeLevel);
            }
        }

        public OInt LvCap
        {
            get
            {
                return (this.mConceptCardParam.lvcap + this.AwakeLevelCap);
            }
        }

        public OInt AwakeLevel
        {
            get
            {
                return (this.AwakeCount * MonoSingleton<GameManager>.Instance.MasterParam.FixParam.CardAwakeUnlockLevelCap);
            }
        }

        public bool IsEnableAwake
        {
            get
            {
                return this.mConceptCardParam.IsEnableAwake;
            }
        }

        public int AwakeCountCap
        {
            get
            {
                return this.mConceptCardParam.AwakeCountCap;
            }
        }

        public int AwakeLevelCap
        {
            get
            {
                return this.mConceptCardParam.AwakeLevelCap;
            }
        }

        public OInt AwakeCount
        {
            get
            {
                RarityParam param;
                if (this.IsEnableAwake == null)
                {
                    goto Label_004D;
                }
                param = MonoSingleton<GameManager>.Instance.MasterParam.GetRarityParam(this.Rarity);
                if (param == null)
                {
                    goto Label_004D;
                }
                return Mathf.Min(this.mAwakeCount, param.ConceptCardAwakeCountMax);
            Label_004D:
                return 0;
            }
        }

        public int SellGold
        {
            get
            {
                return (this.Param.sell + ((((this.Lv - 1) * this.Param.sell) * MonoSingleton<GameManager>.Instance.MasterParam.FixParam.CardSellMul) / 100));
            }
        }

        public int MixExp
        {
            get
            {
                return (this.Param.en_exp + ((((this.Lv - 1) * this.Param.en_exp) * MonoSingleton<GameManager>.Instance.MasterParam.FixParam.CardExpMul) / 100));
            }
        }

        [CompilerGenerated]
        private sealed class <GetAbilityDatailData>c__AnonStorey204
        {
            internal SkillData skill;

            public <GetAbilityDatailData>c__AnonStorey204()
            {
                base..ctor();
                return;
            }

            internal bool <>m__E8(ConceptCardSkillDatailData abi)
            {
                return (abi.skill_data.SkillParam.iname == this.skill.SkillParam.iname);
            }
        }

        [CompilerGenerated]
        private sealed class <GetAbilityDatailData>c__AnonStorey205
        {
            internal LearningSkill learning_skill;
            internal SkillData data;

            public <GetAbilityDatailData>c__AnonStorey205()
            {
                base..ctor();
                return;
            }

            internal bool <>m__E9(SkillData x)
            {
                return (x.SkillParam.iname == this.learning_skill.iname);
            }

            internal bool <>m__EA(ConceptCardSkillDatailData abi)
            {
                return (abi.skill_data.SkillParam.iname == this.data.SkillParam.iname);
            }
        }
    }
}

