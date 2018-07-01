// Decompiled with JetBrains decompiler
// Type: SRPG.ConceptCardDetailStatus
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

namespace SRPG
{
  public class ConceptCardDetailStatus : ConceptCardDetailBase
  {
    private List<GameObject> mInstantiateItems = new List<GameObject>();
    [SerializeField]
    private GameObject mParentObject;
    [SerializeField]
    private GameObject mStatusBaseItem;
    private int mAddExp;
    private int mAddAwakeLv;
    private bool mIsEnhance;

    public override void SetParam(ConceptCardData card_data, int addExp, int addTrust, int addAwakeLv)
    {
      this.mConceptCardData = card_data;
      this.mAddExp = addExp;
      this.mAddAwakeLv = addAwakeLv;
      this.mIsEnhance = ConceptCardDescription.IsEnhance;
      if (!Object.op_Inequality((Object) this.mStatusBaseItem, (Object) null))
        return;
      this.mStatusBaseItem.SetActive(false);
    }

    public override void Refresh()
    {
      if (this.mConceptCardData == null || Object.op_Equality((Object) this.mParentObject, (Object) null) || Object.op_Equality((Object) this.mStatusBaseItem, (Object) null))
        return;
      this.InitStatusItems();
      int index = 0;
      int num = index + 1;
      ConceptCardDetailStatusItems componentInChildren1 = (ConceptCardDetailStatusItems) this.CreateStatusItem(index).GetComponentInChildren<ConceptCardDetailStatusItems>();
      if (Object.op_Inequality((Object) componentInChildren1, (Object) null))
      {
        componentInChildren1.SetParam(this.mConceptCardData, this.GetBaseEffectParam(), this.mAddExp, this.mAddAwakeLv, this.mIsEnhance, true);
        componentInChildren1.Refresh();
      }
      ConceptCardEffectsParam[] effects = this.mConceptCardData.Param.effects;
      if (effects == null || 0 >= effects.Length)
        return;
      foreach (ConceptCardEffectsParam conceptCardEffectsParam in effects)
      {
        if (conceptCardEffectsParam != null && !string.IsNullOrEmpty(conceptCardEffectsParam.cnds_iname) && !string.IsNullOrEmpty(conceptCardEffectsParam.statusup_skill))
        {
          ConceptCardDetailStatusItems componentInChildren2 = (ConceptCardDetailStatusItems) this.CreateStatusItem(num++).GetComponentInChildren<ConceptCardDetailStatusItems>();
          if (Object.op_Inequality((Object) componentInChildren2, (Object) null))
          {
            componentInChildren2.SetParam(this.mConceptCardData, conceptCardEffectsParam, this.mAddExp, this.mAddAwakeLv, this.mIsEnhance, false);
            componentInChildren2.Refresh();
          }
        }
      }
    }

    private ConceptCardEffectsParam GetBaseEffectParam()
    {
      ConceptCardEffectsParam[] effects = this.mConceptCardData.Param.effects;
      if (effects == null || 0 >= effects.Length)
        return (ConceptCardEffectsParam) null;
      ConceptCardEffectsParam cardEffectsParam1 = (ConceptCardEffectsParam) null;
      foreach (ConceptCardEffectsParam cardEffectsParam2 in effects)
      {
        if (cardEffectsParam2 != null && string.IsNullOrEmpty(cardEffectsParam2.cnds_iname) && !string.IsNullOrEmpty(cardEffectsParam2.statusup_skill))
        {
          if (cardEffectsParam1 != null)
            DebugUtility.LogError("基準パラメータが２つ以上設定されています");
          cardEffectsParam1 = cardEffectsParam2;
        }
      }
      return cardEffectsParam1;
    }

    private void InitStatusItems()
    {
      using (List<GameObject>.Enumerator enumerator = this.mInstantiateItems.GetEnumerator())
      {
        while (enumerator.MoveNext())
          enumerator.Current.SetActive(false);
      }
    }

    private GameObject CreateStatusItem(int index)
    {
      if (index < this.mInstantiateItems.Count)
      {
        this.mInstantiateItems[index].SetActive(true);
        return this.mInstantiateItems[index];
      }
      GameObject gameObject = (GameObject) Object.Instantiate<GameObject>((M0) this.mStatusBaseItem);
      gameObject.get_transform().SetParent(this.mParentObject.get_transform());
      this.mInstantiateItems.Add(gameObject);
      gameObject.SetActive(true);
      gameObject.get_transform().set_localScale(new Vector3(1f, 1f, 1f));
      return gameObject;
    }
  }
}
