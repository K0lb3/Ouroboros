// Decompiled with JetBrains decompiler
// Type: SRPG.UnitLearnSkillWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

namespace SRPG
{
  public class UnitLearnSkillWindow : MonoBehaviour, IFlowInterface
  {
    public List<SkillParam> Skills;
    public Transform SkillParent;
    public GameObject SkillTemplate;

    public UnitLearnSkillWindow()
    {
      base.\u002Ector();
    }

    public void Activated(int pinID)
    {
    }

    private void Awake()
    {
      if (!Object.op_Inequality((Object) this.SkillTemplate, (Object) null))
        return;
      this.SkillTemplate.SetActive(false);
    }

    private void Start()
    {
      this.Refresh();
    }

    private void Refresh()
    {
      if (this.Skills == null)
        return;
      for (int index = 0; index < this.Skills.Count; ++index)
      {
        SkillParam skill = this.Skills[index];
        if (skill != null)
        {
          GameObject gameObject = (GameObject) Object.Instantiate<GameObject>((M0) this.SkillTemplate);
          DataSource.Bind<SkillParam>(gameObject, skill);
          gameObject.get_transform().SetParent(this.SkillParent, false);
          gameObject.SetActive(true);
        }
      }
    }
  }
}
