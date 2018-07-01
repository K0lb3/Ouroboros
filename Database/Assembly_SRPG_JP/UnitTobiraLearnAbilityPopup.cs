// Decompiled with JetBrains decompiler
// Type: SRPG.UnitTobiraLearnAbilityPopup
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class UnitTobiraLearnAbilityPopup : MonoBehaviour
  {
    [SerializeField]
    private Text mTitleText;
    [SerializeField]
    private Text mNameText;
    [SerializeField]
    private Text mDescText;

    public UnitTobiraLearnAbilityPopup()
    {
      base.\u002Ector();
    }

    public void Setup(UnitData unit, AbilityParam new_ability, AbilityParam old_ability)
    {
      if (unit == null || new_ability == null)
        return;
      this.mTitleText.set_text(old_ability == null ? LocalizedText.Get("sys.TOBIRA_LEARN_NEW_ABILITY_TEXT") : LocalizedText.Get("sys.TOBIRA_LEARN_OVERRIDE_NEW_ABILITY_TEXT"));
      this.mNameText.set_text(new_ability.name);
      this.mDescText.set_text(new_ability.expr);
      DataSource.Bind<UnitData>(((Component) this).get_gameObject(), unit);
      GameParameter.UpdateAll(((Component) this).get_gameObject());
    }

    public void Setup(UnitData unit, SkillParam skill)
    {
      if (unit == null || skill == null)
        return;
      this.mTitleText.set_text(LocalizedText.Get("sys.TOBIRA_LEARN_NEW_LEADER_SKILL_TEXT"));
      this.mNameText.set_text(skill.name);
      this.mDescText.set_text(skill.expr);
      DataSource.Bind<UnitData>(((Component) this).get_gameObject(), unit);
      GameParameter.UpdateAll(((Component) this).get_gameObject());
    }
  }
}
