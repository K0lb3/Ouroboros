// Decompiled with JetBrains decompiler
// Type: SRPG.UnitLearnAbilityWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

namespace SRPG
{
  public class UnitLearnAbilityWindow : MonoBehaviour, IFlowInterface
  {
    public List<AbilityData> AbilityList;
    public Transform LearnAbilityParent;
    public GameObject LearnAbilityTemplate;
    public GameObject LearnAbilitySkillTemplate;
    public GameObject[] LearningSkills;

    public UnitLearnAbilityWindow()
    {
      base.\u002Ector();
    }

    public void Activated(int pinID)
    {
      this.Refresh();
    }

    private void Awake()
    {
      if (Object.op_Inequality((Object) this.LearnAbilityTemplate, (Object) null))
        this.LearnAbilityTemplate.SetActive(false);
      if (!Object.op_Inequality((Object) this.LearnAbilitySkillTemplate, (Object) null))
        return;
      this.LearnAbilitySkillTemplate.SetActive(false);
    }

    private void Start()
    {
      if (this.AbilityList == null)
      {
        this.AbilityList = GlobalVars.LearningAbilities;
        GlobalVars.LearningAbilities = (List<AbilityData>) null;
      }
      this.Refresh();
    }

    private void Refresh()
    {
      if (this.AbilityList != null)
      {
        for (int index = 0; index < this.AbilityList.Count; ++index)
        {
          AbilityData ability = this.AbilityList[index];
          GameObject gameObject = ability.LearningSkills == null || ability.LearningSkills.Length == 1 ? (GameObject) Object.Instantiate<GameObject>((M0) this.LearnAbilityTemplate) : (GameObject) Object.Instantiate<GameObject>((M0) this.LearnAbilitySkillTemplate);
          DataSource.Bind<AbilityData>(gameObject, ability);
          UnitLearnAbilityElement component = (UnitLearnAbilityElement) gameObject.GetComponent<UnitLearnAbilityElement>();
          if (Object.op_Inequality((Object) component, (Object) null))
            component.Refresh();
          gameObject.get_transform().SetParent(this.LearnAbilityParent, false);
          gameObject.SetActive(true);
        }
      }
      ((Behaviour) this).set_enabled(true);
    }
  }
}
