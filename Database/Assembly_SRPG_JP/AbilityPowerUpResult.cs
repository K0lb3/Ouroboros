// Decompiled with JetBrains decompiler
// Type: SRPG.AbilityPowerUpResult
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

namespace SRPG
{
  public class AbilityPowerUpResult : MonoBehaviour
  {
    [SerializeField]
    private AbilityPowerUpResultContent contentBase;
    [SerializeField]
    private Transform contanteParent;
    [SerializeField]
    private int onePageContentsMax;
    private List<AbilityPowerUpResultContent.Param> paramList;
    private List<AbilityPowerUpResultContent> contentList;

    public AbilityPowerUpResult()
    {
      base.\u002Ector();
    }

    public bool IsEnd
    {
      get
      {
        return this.paramList.Count == 0;
      }
    }

    private void Start()
    {
      if (!Object.op_Inequality((Object) this.contentBase, (Object) null) || !((Component) this.contentBase).get_gameObject().get_activeInHierarchy())
        return;
      ((Component) this.contentBase).get_gameObject().SetActive(false);
    }

    public void SetData(ConceptCardData currentCardData, int prevAwakeCount)
    {
      List<ConceptCardEquipEffect> abilities = currentCardData.GetAbilities();
      int count = abilities.Count;
      for (int index = 0; index < count; ++index)
      {
        AbilityData ability = abilities[index].Ability;
        this.paramList.Add(new AbilityPowerUpResultContent.Param()
        {
          data = ability.Param
        });
      }
    }

    public void ApplyContent()
    {
      int count = this.paramList.Count;
      int num = count >= this.onePageContentsMax ? this.onePageContentsMax : count;
      if (this.contentList.Count == 0)
      {
        for (int index = 0; index < num; ++index)
        {
          AbilityPowerUpResultContent powerUpResultContent = (AbilityPowerUpResultContent) Object.Instantiate<AbilityPowerUpResultContent>((M0) this.contentBase);
          ((Component) powerUpResultContent).get_transform().SetParent(this.contanteParent, false);
          this.contentList.Add(powerUpResultContent);
        }
      }
      else if (num > count)
      {
        for (int index = count - 1; index < num; ++index)
          ((Component) this.contentList[index]).get_gameObject().SetActive(false);
      }
      for (int index = 0; index < num; ++index)
        this.contentList[index].SetData(this.paramList[index]);
      for (int index = 0; index < num; ++index)
        this.paramList.RemoveAt(0);
    }
  }
}
