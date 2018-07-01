// Decompiled with JetBrains decompiler
// Type: SRPG.ConceptCardBonusWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

namespace SRPG
{
  public class ConceptCardBonusWindow : MonoBehaviour
  {
    [SerializeField]
    private Transform mAwakeBonusParent;
    [SerializeField]
    private Transform mLvMaxBonusParent;
    [SerializeField]
    private ConceptCardBonusContentAwake mAwakeBonusTemplate;
    [SerializeField]
    private ConceptCardBonusContentLvMax mLvMaxBonusSkillTemplate;
    [SerializeField]
    private ConceptCardBonusContentLvMax mLvMaxBonusAbilityTemplate;

    public ConceptCardBonusWindow()
    {
      base.\u002Ector();
    }

    public void Initailize(ConceptCardParam param, int current_awake_count, bool is_level_max)
    {
      List<ConceptCardBonusContentAwake> bonusContentAwakeList = new List<ConceptCardBonusContentAwake>();
      if (Object.op_Inequality((Object) this.mAwakeBonusTemplate, (Object) null) && Object.op_Inequality((Object) this.mAwakeBonusParent, (Object) null))
      {
        for (int awake_count = 1; awake_count <= param.AwakeCountCap; ++awake_count)
        {
          bool is_enable = current_awake_count >= awake_count;
          ConceptCardBonusContentAwake bonusContentAwake = (ConceptCardBonusContentAwake) Object.Instantiate<ConceptCardBonusContentAwake>((M0) this.mAwakeBonusTemplate);
          ((Component) bonusContentAwake).get_transform().SetParent(this.mAwakeBonusParent, false);
          bonusContentAwake.Setup(param.effects, awake_count, param.AwakeCountCap, is_enable);
          bonusContentAwakeList.Add(bonusContentAwake);
        }
        for (int index1 = 0; index1 < bonusContentAwakeList.Count; ++index1)
        {
          int index2 = index1 + 1;
          bool is_enable = false;
          bool is_active = true;
          if (bonusContentAwakeList.Count > index2)
            is_enable = bonusContentAwakeList[index2].IsEnable;
          else
            is_active = false;
          bonusContentAwakeList[index1].SetProgressLineImage(is_enable, is_active);
        }
        ((Component) this.mAwakeBonusTemplate).get_gameObject().SetActive(false);
      }
      if (Object.op_Inequality((Object) this.mLvMaxBonusSkillTemplate, (Object) null) && Object.op_Inequality((Object) this.mLvMaxBonusParent, (Object) null))
      {
        bool is_enable = is_level_max;
        ConceptCardBonusContentLvMax bonusContentLvMax = (ConceptCardBonusContentLvMax) Object.Instantiate<ConceptCardBonusContentLvMax>((M0) this.mLvMaxBonusSkillTemplate);
        ((Component) bonusContentLvMax).get_transform().SetParent(this.mLvMaxBonusParent, false);
        bonusContentLvMax.Setup(param.effects, param.lvcap, param.lvcap, param.AwakeCountCap, is_enable, ConceptCardBonusWindow.eViewType.CARD_SKILL);
        ((Component) this.mLvMaxBonusSkillTemplate).get_gameObject().SetActive(false);
      }
      if (!Object.op_Inequality((Object) this.mLvMaxBonusAbilityTemplate, (Object) null) || !Object.op_Inequality((Object) this.mLvMaxBonusParent, (Object) null))
        return;
      bool is_enable1 = is_level_max;
      ConceptCardBonusContentLvMax bonusContentLvMax1 = (ConceptCardBonusContentLvMax) Object.Instantiate<ConceptCardBonusContentLvMax>((M0) this.mLvMaxBonusAbilityTemplate);
      ((Component) bonusContentLvMax1).get_transform().SetParent(this.mLvMaxBonusParent, false);
      bonusContentLvMax1.Setup(param.effects, param.lvcap, param.lvcap, param.AwakeCountCap, is_enable1, ConceptCardBonusWindow.eViewType.ABILITY);
      ((Component) this.mLvMaxBonusAbilityTemplate).get_gameObject().SetActive(false);
    }

    public enum eViewType
    {
      CARD_SKILL,
      ABILITY,
    }
  }
}
