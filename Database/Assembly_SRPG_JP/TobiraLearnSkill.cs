// Decompiled with JetBrains decompiler
// Type: SRPG.TobiraLearnSkill
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class TobiraLearnSkill : MonoBehaviour
  {
    [SerializeField]
    private Text m_LearnSkillName;
    [SerializeField]
    private Text m_LearnSkillEffect;

    public TobiraLearnSkill()
    {
      base.\u002Ector();
    }

    public void Setup(AbilityData newAbility)
    {
      this.m_LearnSkillName.set_text("アビリティ：" + newAbility.AbilityName);
      this.m_LearnSkillEffect.set_text(newAbility.Param.expr);
    }

    public void Setup(SkillData skill)
    {
      this.m_LearnSkillName.set_text("リーダースキル：" + skill.Name);
      this.m_LearnSkillEffect.set_text(skill.SkillParam.expr);
    }
  }
}
