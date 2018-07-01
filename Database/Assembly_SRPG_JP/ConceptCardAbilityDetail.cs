// Decompiled with JetBrains decompiler
// Type: SRPG.ConceptCardAbilityDetail
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace SRPG
{
  public class ConceptCardAbilityDetail : MonoBehaviour
  {
    [SerializeField]
    private UnityEngine.UI.Text mAbilityName;
    [SerializeField]
    private UnityEngine.UI.Text mDescriptionText;

    public ConceptCardAbilityDetail()
    {
      base.\u002Ector();
    }

    private void Start()
    {
      ConceptCardSkillDatailData dataOfClass = DataSource.FindDataOfClass<ConceptCardSkillDatailData>(((Component) this).get_gameObject(), (ConceptCardSkillDatailData) null);
      if (dataOfClass == null)
        return;
      this.SetData(dataOfClass);
    }

    public void SetData(ConceptCardSkillDatailData data)
    {
      switch (data.type)
      {
        case ConceptCardDetailAbility.ShowType.Skill:
          this.SetGroup(data);
          break;
        case ConceptCardDetailAbility.ShowType.Ability:
          this.SetGroup(data);
          break;
        case ConceptCardDetailAbility.ShowType.LockSkill:
          this.SetGroup(data);
          break;
      }
    }

    public void SetSkillData(ConceptCardEquipEffect effect)
    {
      SkillData cardSkill = effect.CardSkill;
      if (cardSkill == null)
        return;
      this.SetText(this.mAbilityName, cardSkill.Name);
      StringBuilder stringBuilder = new StringBuilder();
      List<BuffEffect.BuffTarget> targets = cardSkill.mTargetBuffEffect.targets;
      for (int index = 0; index < targets.Count; ++index)
      {
        BuffEffect.BuffTarget target = targets[index];
        stringBuilder.Append(effect.GetBufText(cardSkill.mTargetBuffEffect, target));
      }
      UnitGroupParam unitGroup = MonoSingleton<GameManager>.Instance.MasterParam.GetUnitGroup(cardSkill.mTargetBuffEffect.param.un_group);
      if (unitGroup != null && !string.IsNullOrEmpty(unitGroup.name))
      {
        stringBuilder.Append(LocalizedText.Get("sys.CONCEPT_CARD_SKILL_DESCRIPTION_NEW_LINE"));
        stringBuilder.Append(LocalizedText.Get("sys.CONCEPT_CARD_SKILL_DESCRIPTION_GROUP", (object) unitGroup.name, (object) unitGroup.GetGroupUnitAllNameText()));
      }
      this.SetText(this.mDescriptionText, stringBuilder.ToString());
    }

    public void SetAbilityData(ConceptCardEquipEffect effect)
    {
      AbilityData ability = effect.Ability;
      if (ability == null)
        return;
      this.SetText(this.mAbilityName, ability.Param.name);
      this.SetText(this.mDescriptionText, ability.Param.expr);
    }

    public void SetGroup(ConceptCardSkillDatailData data)
    {
      if (data.skill_data == null)
        return;
      this.SetText(this.mAbilityName, data.skill_data.Name);
      if (data.type == ConceptCardDetailAbility.ShowType.LockSkill)
      {
        if (data.learning_skill == null)
          return;
        this.SetText(this.mDescriptionText, LocalizedText.Get("sys.CONCEPT_CARD_SKILL_DESCRIPTION_LV", new object[1]
        {
          (object) data.learning_skill.locklv
        }));
      }
      else
        this.SetText(this.mDescriptionText, GameUtility.GetExternalLocalizedText("skill", data.skill_data.SkillParam.iname, "CONCEPT_TXT"));
    }

    public void SetText(UnityEngine.UI.Text text, string str)
    {
      if (!Object.op_Inequality((Object) text, (Object) null))
        return;
      text.set_text(str);
    }
  }
}
