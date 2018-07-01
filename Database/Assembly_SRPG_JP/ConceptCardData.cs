// Decompiled with JetBrains decompiler
// Type: SRPG.ConceptCardData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace SRPG
{
  public class ConceptCardData
  {
    private OLong mUniqueID = (OLong) 0L;
    private BaseStatus mFixStatus = new BaseStatus();
    private BaseStatus mScaleSatus = new BaseStatus();
    private OInt mLv;
    private OInt mExp;
    private OInt mTrust;
    private bool mFavorite;
    private bool mTrustBonus;
    private OInt mAwakeCount;
    private ConceptCardParam mConceptCardParam;
    private List<ConceptCardEquipEffect> mEquipEffects;
    private ConceptCardListFilterWindow.Type mFilterType;

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
        return (OInt) this.mConceptCardParam.rare;
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
        return (OInt) (this.mConceptCardParam.lvcap + (int) this.AwakeLevel);
      }
    }

    public OInt LvCap
    {
      get
      {
        return (OInt) (this.mConceptCardParam.lvcap + this.AwakeLevelCap);
      }
    }

    public OInt AwakeLevel
    {
      get
      {
        return (OInt) ((int) this.AwakeCount * (int) MonoSingleton<GameManager>.Instance.MasterParam.FixParam.CardAwakeUnlockLevelCap);
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
        if (this.IsEnableAwake)
        {
          RarityParam rarityParam = MonoSingleton<GameManager>.Instance.MasterParam.GetRarityParam((int) this.Rarity);
          if (rarityParam != null)
            return (OInt) Mathf.Min((int) this.mAwakeCount, (int) rarityParam.ConceptCardAwakeCountMax);
        }
        return (OInt) 0;
      }
    }

    public bool Deserialize(JSON_ConceptCard json)
    {
      this.mUniqueID = (OLong) json.iid;
      this.mExp = (OInt) json.exp;
      this.mTrust = (OInt) json.trust;
      this.mFavorite = json.fav != 0;
      this.mTrustBonus = json.trust_bonus != 0;
      this.mAwakeCount = (OInt) json.plus;
      this.mConceptCardParam = MonoSingleton<GameManager>.Instance.MasterParam.GetConceptCardParam(json.iname);
      this.mLv = (OInt) this.CalcCardLevel();
      this.UpdateEquipEffect();
      this.RefreshFilterType();
      return true;
    }

    public int SellGold
    {
      get
      {
        return this.Param.sell + ((int) this.Lv - 1) * this.Param.sell * MonoSingleton<GameManager>.Instance.MasterParam.FixParam.CardSellMul / 100;
      }
    }

    public int MixExp
    {
      get
      {
        return this.Param.en_exp + ((int) this.Lv - 1) * this.Param.en_exp * MonoSingleton<GameManager>.Instance.MasterParam.FixParam.CardExpMul / 100;
      }
    }

    public void SetTrust(int trust)
    {
      this.mTrust = (OInt) trust;
    }

    public void SetBonus(bool bonus)
    {
      this.mTrustBonus = bonus;
    }

    private void UpdateEquipEffect()
    {
      this.mEquipEffects = (List<ConceptCardEquipEffect>) null;
      if (this.mConceptCardParam.effects != null && this.mConceptCardParam.effects.Length > 0)
      {
        this.mEquipEffects = new List<ConceptCardEquipEffect>();
        for (int index = 0; index < this.mConceptCardParam.effects.Length; ++index)
        {
          ConceptCardEquipEffect conceptCardEquipEffect = new ConceptCardEquipEffect();
          conceptCardEquipEffect.Setup(this.mConceptCardParam.effects[index], (int) this.Lv, (int) this.LvCap, (int) this.AwakeCount, this.AwakeCountCap);
          this.mEquipEffects.Add(conceptCardEquipEffect);
        }
      }
      this.UpdateStatus(ref this.mFixStatus, ref this.mScaleSatus);
    }

    private int CalcCardLevel()
    {
      return MonoSingleton<GameManager>.Instance.MasterParam.CalcConceptCardLevel((int) this.Rarity, (int) this.mExp, (int) this.CurrentLvCap);
    }

    public int GetExpToLevelMax()
    {
      return MonoSingleton<GameManager>.Instance.MasterParam.GetConceptCardLevelExp((int) this.Rarity, (int) this.CurrentLvCap) - (int) this.mExp;
    }

    public int GetTrustToLevelMax()
    {
      return (int) MonoSingleton<GameManager>.Instance.MasterParam.FixParam.CardTrustMax - (int) this.mTrust;
    }

    public List<ConceptCardEquipEffect> GetEnableEquipEffects(UnitData unit_data, JobData job_data)
    {
      List<ConceptCardEquipEffect> conceptCardEquipEffectList = new List<ConceptCardEquipEffect>();
      if (this.mEquipEffects == null)
        return conceptCardEquipEffectList;
      for (int index = 0; index < this.mEquipEffects.Count; ++index)
      {
        if (this.IsMatchConditions(unit_data.UnitParam, job_data, this.mEquipEffects[index].ConditionsIname))
          conceptCardEquipEffectList.Add(this.mEquipEffects[index]);
      }
      return conceptCardEquipEffectList;
    }

    public bool IsMatchConditions(UnitParam unit_param, JobData job_data, string conditions_iname)
    {
      if (unit_param == null)
        return false;
      ConceptCardConditionsParam conceptCardConditions = MonoSingleton<GameManager>.Instance.MasterParam.GetConceptCardConditions(conditions_iname);
      return conceptCardConditions == null || conceptCardConditions.IsMatchElement(unit_param.element) && (conceptCardConditions.sex == ESex.Unknown || conceptCardConditions.sex == unit_param.sex) && (conceptCardConditions.IsMatchBirth(unit_param.birthID) && conceptCardConditions.IsMatchUnitGroup(unit_param.iname) && conceptCardConditions.IsMatchJobGroup(job_data.JobID));
    }

    public static ConceptCardData CreateConceptCardDataForDisplay(string iname)
    {
      ConceptCardData conceptCardData = new ConceptCardData();
      conceptCardData.Deserialize(new JSON_ConceptCard()
      {
        iid = 1L,
        iname = iname,
        exp = 0,
        trust = 0,
        fav = 0
      });
      return conceptCardData;
    }

    private void RefreshFilterType()
    {
      this.mFilterType = ConceptCardListFilterWindow.Type.NONE;
      switch ((int) this.Rarity)
      {
        case 0:
          this.mFilterType |= ConceptCardListFilterWindow.Type.RARITY_1;
          break;
        case 1:
          this.mFilterType |= ConceptCardListFilterWindow.Type.RARITY_2;
          break;
        case 2:
          this.mFilterType |= ConceptCardListFilterWindow.Type.RARITY_3;
          break;
        case 3:
          this.mFilterType |= ConceptCardListFilterWindow.Type.RARITY_4;
          break;
        case 4:
          this.mFilterType |= ConceptCardListFilterWindow.Type.RARITY_5;
          break;
      }
    }

    public bool Filter(ConceptCardListFilterWindow.Type filter_type)
    {
      return (filter_type & this.mFilterType) != ConceptCardListFilterWindow.Type.NONE;
    }

    public bool FilterEnhance(string filter_iname)
    {
      return this.mConceptCardParam.iname == filter_iname;
    }

    public long GetSortData(ConceptCardListSortWindow.Type type)
    {
      BaseStatus equipEffectStatus = this.GetNoConditionsEquipEffectStatus();
      ConceptCardListSortWindow.Type type1 = type;
      switch (type1)
      {
        case ConceptCardListSortWindow.Type.LEVEL:
          return (long) (int) this.Lv;
        case ConceptCardListSortWindow.Type.RARITY:
          return (long) (int) this.Rarity;
        case ConceptCardListSortWindow.Type.ATK:
          return (long) (int) equipEffectStatus.param.atk;
        case ConceptCardListSortWindow.Type.DEF:
          return (long) (int) equipEffectStatus.param.def;
        default:
          if (type1 == ConceptCardListSortWindow.Type.MAG)
            return (long) (int) equipEffectStatus.param.mag;
          if (type1 == ConceptCardListSortWindow.Type.MND)
            return (long) (int) equipEffectStatus.param.mnd;
          if (type1 == ConceptCardListSortWindow.Type.SPD)
            return (long) (int) equipEffectStatus.param.spd;
          if (type1 == ConceptCardListSortWindow.Type.LUCK)
            return (long) (int) equipEffectStatus.param.luk;
          if (type1 == ConceptCardListSortWindow.Type.HP)
            return (long) (int) equipEffectStatus.param.hp;
          if (type1 == ConceptCardListSortWindow.Type.TIME)
            return (long) this.UniqueID;
          if (type1 == ConceptCardListSortWindow.Type.TRUST)
            return (long) (int) this.Trust;
          if (type1 == ConceptCardListSortWindow.Type.AWAKE)
            return (long) (int) this.AwakeCount;
          return 0;
      }
    }

    public int GetSortParam(ParamTypes types)
    {
      if (this.mFixStatus == null)
        return 0;
      return (int) this.mFixStatus[types];
    }

    public void UpdateStatus(ref BaseStatus fix, ref BaseStatus scale)
    {
      if (this.EquipEffects == null)
        return;
      for (int index = 0; index < this.EquipEffects.Count; ++index)
        this.EquipEffects[index].GetStatus(ref fix, ref scale);
    }

    public void GetStatus(ref BaseStatus fix, ref BaseStatus scale)
    {
      fix = this.mFixStatus;
      scale = this.mScaleSatus;
    }

    public List<ConceptCardEquipEffect> GetNoConditionsEquipEffects()
    {
      List<ConceptCardEquipEffect> conceptCardEquipEffectList = new List<ConceptCardEquipEffect>();
      if (this.mEquipEffects != null)
      {
        for (int index = 0; index < this.mEquipEffects.Count; ++index)
        {
          if (this.mEquipEffects[index].GetCondition() == null)
            conceptCardEquipEffectList.Add(this.mEquipEffects[index]);
        }
      }
      return conceptCardEquipEffectList;
    }

    public BaseStatus GetNoConditionsEquipEffectStatus()
    {
      BaseStatus baseStatus = new BaseStatus();
      List<ConceptCardEquipEffect> conditionsEquipEffects = this.GetNoConditionsEquipEffects();
      for (int index = 0; index < conditionsEquipEffects.Count; ++index)
      {
        BaseStatus status = new BaseStatus();
        BaseStatus scale_status = new BaseStatus();
        SkillData.GetHomePassiveBuffStatus(conditionsEquipEffects[index].EquipSkill, EElement.None, ref status, ref scale_status);
        baseStatus.Add(status);
      }
      return baseStatus;
    }

    public List<SkillData> GetEnableCardSkills(UnitData unit)
    {
      List<SkillData> skillDataList = new List<SkillData>();
      if (unit == null)
        return skillDataList;
      List<ConceptCardEquipEffect> enableEquipEffects = this.GetEnableEquipEffects(unit, unit.Jobs[unit.JobIndex]);
      for (int index = 0; index < enableEquipEffects.Count; ++index)
      {
        if (enableEquipEffects[index].CardSkill != null)
          skillDataList.Add(enableEquipEffects[index].CardSkill);
      }
      return skillDataList;
    }

    public List<BuffEffect> GetEnableCardSkillAddBuffs(UnitData unit, SkillParam parent_card_skill)
    {
      List<BuffEffect> buffEffectList = new List<BuffEffect>();
      if (unit == null)
        return buffEffectList;
      List<ConceptCardEquipEffect> enableEquipEffects = this.GetEnableEquipEffects(unit, unit.Jobs[unit.JobIndex]);
      for (int index = 0; index < enableEquipEffects.Count; ++index)
      {
        if (enableEquipEffects[index].CardSkill != null && !(enableEquipEffects[index].CardSkill.SkillID != parent_card_skill.iname))
        {
          if (enableEquipEffects[index].AddCardSkillBuffEffectAwake != null)
            buffEffectList.Add(enableEquipEffects[index].AddCardSkillBuffEffectAwake);
          if (enableEquipEffects[index].AddCardSkillBuffEffectLvMax != null)
            buffEffectList.Add(enableEquipEffects[index].AddCardSkillBuffEffectLvMax);
        }
      }
      return buffEffectList;
    }

    public List<ConceptCardEquipEffect> GetAbilities()
    {
      List<ConceptCardEquipEffect> conceptCardEquipEffectList = new List<ConceptCardEquipEffect>();
      if (this.mEquipEffects == null)
        return conceptCardEquipEffectList;
      for (int index = 0; index < this.mEquipEffects.Count; ++index)
      {
        ConceptCardEquipEffect mEquipEffect = this.mEquipEffects[index];
        if (mEquipEffect.Ability != null)
          conceptCardEquipEffectList.Add(mEquipEffect);
      }
      return conceptCardEquipEffectList;
    }

    public List<ConceptCardEquipEffect> GetCardSkills()
    {
      List<ConceptCardEquipEffect> conceptCardEquipEffectList = new List<ConceptCardEquipEffect>();
      if (this.mEquipEffects == null)
        return conceptCardEquipEffectList;
      for (int index = 0; index < this.mEquipEffects.Count; ++index)
      {
        ConceptCardEquipEffect mEquipEffect = this.mEquipEffects[index];
        if (mEquipEffect.CardSkill != null)
          conceptCardEquipEffectList.Add(mEquipEffect);
      }
      return conceptCardEquipEffectList;
    }

    public List<ConceptCardSkillDatailData> GetAbilityDatailData()
    {
      List<ConceptCardSkillDatailData> cardSkillDatailDataList = new List<ConceptCardSkillDatailData>();
      List<ConceptCardEquipEffect> abilities = this.GetAbilities();
      List<ConceptCardEquipEffect> cardSkills = this.GetCardSkills();
      for (int index = 0; index < cardSkills.Count; ++index)
      {
        SkillData skill = cardSkills[index].CardSkill;
        if (skill != null && cardSkillDatailDataList.FindIndex((Predicate<ConceptCardSkillDatailData>) (abi => abi.skill_data.SkillParam.iname == skill.SkillParam.iname)) <= -1)
          cardSkillDatailDataList.Add(new ConceptCardSkillDatailData(cardSkills[index], skill, ConceptCardDetailAbility.ShowType.Skill, (LearningSkill) null));
      }
      for (int index1 = 0; index1 < abilities.Count; ++index1)
      {
        AbilityData ability = abilities[index1].Ability;
        if (ability != null)
        {
          for (int index2 = 0; index2 < ability.LearningSkills.Length; ++index2)
          {
            LearningSkill learning_skill = ability.LearningSkills[index2];
            if (learning_skill != null)
            {
              ConceptCardDetailAbility.ShowType _type = ConceptCardDetailAbility.ShowType.Ability;
              SkillData data = ability.Skills.Find((Predicate<SkillData>) (x => x.SkillParam.iname == learning_skill.iname));
              if (data == null)
              {
                SkillParam skillParam = MonoSingleton<GameManager>.Instance.MasterParam.GetSkillParam(learning_skill.iname);
                data = new SkillData();
                data.Setup(skillParam.iname, 1, 1, (MasterParam) null);
                _type = ConceptCardDetailAbility.ShowType.LockSkill;
              }
              if (cardSkillDatailDataList.FindIndex((Predicate<ConceptCardSkillDatailData>) (abi => abi.skill_data.SkillParam.iname == data.SkillParam.iname)) <= -1)
                cardSkillDatailDataList.Add(new ConceptCardSkillDatailData(abilities[index1], data, _type, learning_skill));
            }
          }
        }
      }
      return cardSkillDatailDataList;
    }

    public ConceptCardTrustRewardItemParam GetReward()
    {
      ConceptCardTrustRewardParam trustReward = MonoSingleton<GameManager>.Instance.MasterParam.GetTrustReward(this.Param.trust_reward);
      if (trustReward == null || trustReward.rewards == null || trustReward.rewards.Length <= 0)
        return (ConceptCardTrustRewardItemParam) null;
      return trustReward.rewards[0];
    }

    public UnitData GetOwner()
    {
      return MonoSingleton<GameManager>.Instance.Player.Units.Find((Predicate<UnitData>) (u =>
      {
        if (u.ConceptCard != null)
          return (long) u.ConceptCard.UniqueID == (long) this.UniqueID;
        return false;
      }));
    }
  }
}
