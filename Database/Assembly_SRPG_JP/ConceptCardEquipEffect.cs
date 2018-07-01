// Decompiled with JetBrains decompiler
// Type: SRPG.ConceptCardEquipEffect
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace SRPG
{
  public class ConceptCardEquipEffect
  {
    private string mConditionsIname;
    private string mSkin;
    private ConceptCardEffectsParam mEffectParam;
    private SkillData mCardSkill;
    private SkillData mEquipSkill;
    private AbilityData mAbilityDefault;
    private AbilityData mAbilityLvMax;
    private BuffEffect mAddCardSkillBuffEffectAwake;
    private BuffEffect mAddCardSkillBuffEffectLvMax;
    private bool is_levelmax;

    public string ConditionsIname
    {
      get
      {
        return this.mConditionsIname;
      }
    }

    public string Skin
    {
      get
      {
        return this.mSkin;
      }
    }

    public SkillData CardSkill
    {
      get
      {
        return this.mCardSkill;
      }
    }

    public SkillData EquipSkill
    {
      get
      {
        return this.mEquipSkill;
      }
    }

    public AbilityData Ability
    {
      get
      {
        if (this.is_levelmax && this.mAbilityLvMax != null)
          return this.mAbilityLvMax;
        return this.mAbilityDefault;
      }
    }

    public BuffEffect AddCardSkillBuffEffectAwake
    {
      get
      {
        return this.mAddCardSkillBuffEffectAwake;
      }
    }

    public BuffEffect AddCardSkillBuffEffectLvMax
    {
      get
      {
        return this.mAddCardSkillBuffEffectLvMax;
      }
    }

    public bool IsExistAbilityLvMax
    {
      get
      {
        if (this.mEffectParam != null && !string.IsNullOrEmpty(this.mEffectParam.abil_iname))
          return !string.IsNullOrEmpty(this.mEffectParam.abil_iname_lvmax);
        return false;
      }
    }

    public static ConceptCardEquipEffect CreateFromAbility(AbilityData abilityData)
    {
      return new ConceptCardEquipEffect()
      {
        mAbilityDefault = abilityData
      };
    }

    public static ConceptCardEquipEffect CreateFromGroupSkill(SkillData skillData)
    {
      return new ConceptCardEquipEffect()
      {
        mCardSkill = skillData
      };
    }

    public void Setup(ConceptCardEffectsParam param, int lv, int lvcap, int awake_count, int awake_count_cap)
    {
      this.mEffectParam = param;
      this.mConditionsIname = param.cnds_iname;
      this.is_levelmax = lv >= lvcap;
      if (!string.IsNullOrEmpty(param.card_skill))
      {
        this.mCardSkill = new SkillData();
        this.mCardSkill.Setup(param.card_skill, lv, lvcap, (MasterParam) null);
      }
      if (!string.IsNullOrEmpty(param.add_card_skill_buff_awake) && awake_count > 0)
      {
        BuffEffectParam buffEffectParam = MonoSingleton<GameManager>.Instance.MasterParam.GetBuffEffectParam(param.add_card_skill_buff_awake);
        if (buffEffectParam != null)
          this.mAddCardSkillBuffEffectAwake = BuffEffect.CreateBuffEffect(buffEffectParam, awake_count, awake_count_cap);
      }
      if (!string.IsNullOrEmpty(param.add_card_skill_buff_lvmax) && lv >= lvcap && awake_count > 0)
      {
        BuffEffectParam buffEffectParam = MonoSingleton<GameManager>.Instance.MasterParam.GetBuffEffectParam(param.add_card_skill_buff_lvmax);
        if (buffEffectParam != null)
          this.mAddCardSkillBuffEffectLvMax = BuffEffect.CreateBuffEffect(buffEffectParam, 1, 1);
      }
      if (!string.IsNullOrEmpty(param.statusup_skill))
      {
        this.mEquipSkill = new SkillData();
        this.mEquipSkill.Setup(param.statusup_skill, lv, lvcap, (MasterParam) null);
      }
      if (!string.IsNullOrEmpty(param.skin))
        this.mSkin = param.skin;
      if (!string.IsNullOrEmpty(param.abil_iname) && MonoSingleton<GameManager>.Instance.GetAbilityParam(param.abil_iname) != null)
      {
        this.mAbilityDefault = new AbilityData();
        this.mAbilityDefault.Setup((UnitData) null, 0L, param.abil_iname, lv - 1, lvcap);
        this.mAbilityDefault.IsNoneCategory = true;
        this.mAbilityDefault.IsHideList = false;
      }
      if (string.IsNullOrEmpty(param.abil_iname) || string.IsNullOrEmpty(param.abil_iname_lvmax) || MonoSingleton<GameManager>.Instance.GetAbilityParam(param.abil_iname_lvmax) == null)
        return;
      this.mAbilityLvMax = new AbilityData();
      this.mAbilityLvMax.Setup((UnitData) null, 0L, param.abil_iname_lvmax, lv - 1, lvcap);
      this.mAbilityLvMax.IsNoneCategory = true;
      this.mAbilityLvMax.IsHideList = false;
    }

    public void GetStatus(ref BaseStatus fixed_status, ref BaseStatus scale_status)
    {
      BaseStatus scale_status1 = new BaseStatus();
      fixed_status.Clear();
      scale_status.Clear();
      SkillData.GetHomePassiveBuffStatus(this.mEquipSkill, EElement.None, ref fixed_status, ref scale_status1);
      scale_status.Add(scale_status1);
      if (this.Ability == null || this.Ability.Skills == null)
        return;
      for (int index = 0; index < this.Ability.Skills.Count; ++index)
      {
        scale_status1.Clear();
        SkillData.GetHomePassiveBuffStatus(this.Ability.Skills[index], EElement.None, ref fixed_status, ref scale_status1);
        scale_status.Add(scale_status1);
      }
    }

    public string GetBufText(BuffEffect effect, BuffEffect.BuffTarget target)
    {
      MasterParam masterParam = MonoSingleton<GameManager>.Instance.MasterParam;
      StringBuilder stringBuilder = new StringBuilder();
      ConceptCardConditionsParam conceptCardConditions = masterParam.GetConceptCardConditions(this.ConditionsIname);
      stringBuilder.Append(conceptCardConditions.GetConditionDescriptionEquip());
      UnitGroupParam unitGroup = masterParam.GetUnitGroup(effect.param.un_group);
      if (unitGroup != null)
        stringBuilder.Append(unitGroup.GetName());
      stringBuilder.Append(LocalizedText.Get("sys.CONCEPT_CARD_SKILL_DESCRIPTION_OF"));
      string str = LocalizedText.Get("sys." + target.paramType.ToString());
      stringBuilder.Append(str);
      bool flag = 0 <= (int) target.value;
      int num = Mathf.Abs((int) target.value);
      string empty = string.Empty;
      switch (target.calcType)
      {
        case SkillParamCalcTypes.Add:
          if (flag)
          {
            empty = LocalizedText.Get("sys.CONCEPT_CARD_SKILL_DESCRIPTION_CALC_ADD_PLUS", new object[1]
            {
              (object) num.ToString()
            });
            break;
          }
          empty = LocalizedText.Get("sys.CONCEPT_CARD_SKILL_DESCRIPTION_CALC_ADD_MINUS", new object[1]
          {
            (object) num.ToString()
          });
          break;
        case SkillParamCalcTypes.Scale:
          if (flag)
          {
            empty = LocalizedText.Get("sys.CONCEPT_CARD_SKILL_DESCRIPTION_CALC_UP", new object[1]
            {
              (object) num.ToString()
            });
            break;
          }
          empty = LocalizedText.Get("sys.CONCEPT_CARD_SKILL_DESCRIPTION_CALC_DOWN", new object[1]
          {
            (object) num.ToString()
          });
          break;
      }
      stringBuilder.Append(empty);
      return stringBuilder.ToString();
    }

    public ConceptCardConditionsParam GetCondition()
    {
      return MonoSingleton<GameManager>.Instance.MasterParam.GetConceptCardConditions(this.ConditionsIname);
    }

    public UnitParam[] GetConditionUnit()
    {
      ConceptCardConditionsParam condition = this.GetCondition();
      if (condition == null)
        return (UnitParam[]) null;
      UnitGroupParam unitGroupParam = condition.GetUnitGroupParam();
      if (unitGroupParam == null || unitGroupParam.units == null)
        return (UnitParam[]) null;
      List<UnitParam> unitParamList = new List<UnitParam>();
      for (int index = 0; index < unitGroupParam.units.Length; ++index)
        unitParamList.Add(MonoSingleton<GameManager>.Instance.MasterParam.GetUnitParam(unitGroupParam.units[index]));
      return unitParamList.ToArray();
    }

    public JobParam[] GetConditionJob()
    {
      ConceptCardConditionsParam condition = this.GetCondition();
      if (condition == null)
        return (JobParam[]) null;
      JobGroupParam jobGroupParam = condition.GetJobGroupParam();
      if (jobGroupParam == null || jobGroupParam.jobs == null)
        return (JobParam[]) null;
      List<JobParam> jobParamList = new List<JobParam>();
      for (int index = 0; index < jobGroupParam.jobs.Length; ++index)
        jobParamList.Add(MonoSingleton<GameManager>.Instance.MasterParam.GetJobParam(jobGroupParam.jobs[index]));
      return jobParamList.ToArray();
    }

    public BuffEffect CreateAddCardSkillBuffEffectAwake(int awake, int awake_cap)
    {
      if (this.mEffectParam == null)
        return (BuffEffect) null;
      return this.mEffectParam.CreateAddCardSkillBuffEffectAwake(awake, awake_cap);
    }

    public BuffEffect CreateAddCardSkillBuffEffectLvMax(int lv, int lv_cap, int awake)
    {
      if (this.mEffectParam == null)
        return (BuffEffect) null;
      return this.mEffectParam.CreateAddCardSkillBuffEffectLvMax(lv, lv_cap, awake);
    }

    public void GetAddCardSkillBuffStatusAwake(int awake, int awake_cap, ref BaseStatus total_add, ref BaseStatus total_scale)
    {
      if (this.mEffectParam == null)
      {
        total_add.Clear();
        total_scale.Clear();
      }
      else
        this.mEffectParam.GetAddCardSkillBuffStatusAwake(awake, awake_cap, ref total_add, ref total_scale);
    }

    public void GetAddCardSkillBuffStatusLvMax(int lv, int lv_cap, int awake, ref BaseStatus total_add, ref BaseStatus total_scale)
    {
      if (this.mEffectParam == null)
      {
        total_add.Clear();
        total_scale.Clear();
      }
      else
        this.mEffectParam.GetAddCardSkillBuffStatusLvMax(lv, lv_cap, awake, ref total_add, ref total_scale);
    }
  }
}
