// Decompiled with JetBrains decompiler
// Type: SRPG.ConceptCardUtility
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;
using System;
using System.Collections.Generic;

namespace SRPG
{
  public class ConceptCardUtility
  {
    public static bool IsEnableCardSkillForUnit(Unit target, SkillData card_skill)
    {
      if (target == null || card_skill == null || card_skill.SkillParam.condition != ESkillCondition.CardSkill)
        return false;
      BuffEffectParam buffEffectParam = MonoSingleton<GameManager>.GetInstanceDirect().MasterParam.GetBuffEffectParam(card_skill.SkillParam.target_buff_iname);
      if (buffEffectParam == null)
        return false;
      return BuffEffect.CreateBuffEffect(buffEffectParam, card_skill.Rank, card_skill.GetRankCap()).CheckEnableBuffTarget(target);
    }

    public static bool IsGetUnitConceptCard(string iname)
    {
      if (string.IsNullOrEmpty(iname))
        return false;
      ConceptCardParam conceptCardParam = MonoSingleton<GameManager>.Instance.MasterParam.GetConceptCardParam(iname);
      return conceptCardParam != null && !string.IsNullOrEmpty(conceptCardParam.first_get_unit);
    }

    public static void GetExpParameter(int rarity, int exp, int current_lvcap, out int lv, out int nextExp, out int expTbl)
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) MonoSingleton<GameManager>.Instance, (UnityEngine.Object) null) || MonoSingleton<GameManager>.Instance.MasterParam == null)
      {
        lv = 1;
        expTbl = 1;
        nextExp = 0;
      }
      else
      {
        lv = MonoSingleton<GameManager>.Instance.MasterParam.CalcConceptCardLevel(rarity, exp, current_lvcap);
        int conceptCardLevelExp = MonoSingleton<GameManager>.Instance.MasterParam.GetConceptCardLevelExp(rarity, lv);
        if (lv < current_lvcap)
        {
          expTbl = MonoSingleton<GameManager>.Instance.MasterParam.GetConceptCardNextExp(rarity, lv + 1);
          nextExp = expTbl - (exp - conceptCardLevelExp);
        }
        else
        {
          expTbl = 1;
          nextExp = 0;
        }
      }
    }

    public static List<ConceptCardSkillDatailData> CreateConceptCardSkillDatailData(AbilityData abilityData)
    {
      List<ConceptCardSkillDatailData> cardSkillDatailDataList = new List<ConceptCardSkillDatailData>();
      if (abilityData == null)
        return cardSkillDatailDataList;
      ConceptCardEquipEffect fromAbility = ConceptCardEquipEffect.CreateFromAbility(abilityData);
      for (int index = 0; index < abilityData.LearningSkills.Length; ++index)
      {
        LearningSkill learning_skill = abilityData.LearningSkills[index];
        if (learning_skill != null)
        {
          ConceptCardDetailAbility.ShowType _type = ConceptCardDetailAbility.ShowType.Ability;
          SkillData data = abilityData.Skills.Find((Predicate<SkillData>) (x => x.SkillParam.iname == learning_skill.iname));
          if (data == null)
          {
            SkillParam skillParam = MonoSingleton<GameManager>.Instance.MasterParam.GetSkillParam(learning_skill.iname);
            data = new SkillData();
            data.Setup(skillParam.iname, 1, 1, (MasterParam) null);
            _type = ConceptCardDetailAbility.ShowType.LockSkill;
          }
          if (cardSkillDatailDataList.FindIndex((Predicate<ConceptCardSkillDatailData>) (abi => abi.skill_data.SkillParam.iname == data.SkillParam.iname)) <= -1)
            cardSkillDatailDataList.Add(new ConceptCardSkillDatailData(fromAbility, data, _type, learning_skill));
        }
      }
      return cardSkillDatailDataList;
    }

    public static ConceptCardSkillDatailData CreateConceptCardSkillDatailData(SkillData groupSkill)
    {
      ConceptCardSkillDatailData cardSkillDatailData = (ConceptCardSkillDatailData) null;
      if (groupSkill == null)
        return cardSkillDatailData;
      return new ConceptCardSkillDatailData(ConceptCardEquipEffect.CreateFromGroupSkill(groupSkill), groupSkill, ConceptCardDetailAbility.ShowType.Skill, (LearningSkill) null);
    }
  }
}
