// Decompiled with JetBrains decompiler
// Type: SRPG.ConceptCardCheckWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class ConceptCardCheckWindow : MonoBehaviour
  {
    [SerializeField]
    private RectTransform ListParent;
    [SerializeField]
    private GameObject ListItemTemplate;
    [SerializeField]
    private Text GetExp;
    [SerializeField]
    private Text UsedZenny;
    [SerializeField]
    private Text GetTrust;
    private Dictionary<string, int> mSelectedCard;
    private List<ConceptCardIcon> mConceptCardIcon;

    public ConceptCardCheckWindow()
    {
      base.\u002Ector();
    }

    private void Start()
    {
      this.CreateMakeCardIcon();
      this.SetupText();
    }

    private void CreateMakeCardIcon()
    {
      if (Object.op_Equality((Object) this.ListParent, (Object) null) || Object.op_Equality((Object) this.ListItemTemplate, (Object) null))
        return;
      this.ListItemTemplate.SetActive(false);
      ConceptCardManager instance = ConceptCardManager.Instance;
      if (Object.op_Equality((Object) instance, (Object) null) || instance.BulkSelectedMaterialList.Count == 0)
        return;
      for (int index = 0; index < instance.BulkSelectedMaterialList.Count; ++index)
      {
        ConceptCardData mSelectedData = instance.BulkSelectedMaterialList[index].mSelectedData;
        if (mSelectedData != null)
        {
          GameObject gameObject = (GameObject) Object.Instantiate<GameObject>((M0) this.ListItemTemplate);
          gameObject.SetActive(true);
          gameObject.get_transform().SetParent((Transform) this.ListParent, false);
          ConceptCardIcon component = (ConceptCardIcon) gameObject.GetComponent<ConceptCardIcon>();
          if (Object.op_Equality((Object) component, (Object) null))
            return;
          component.Setup(mSelectedData);
          this.mConceptCardIcon.Add(component);
          this.mSelectedCard.Add(mSelectedData.Param.iname, instance.BulkSelectedMaterialList[index].mSelectNum);
        }
      }
      using (List<ConceptCardIcon>.Enumerator enumerator = this.mConceptCardIcon.GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          ConceptCardIcon current = enumerator.Current;
          int num = this.mSelectedCard[current.ConceptCard.Param.iname];
          current.SetCardNum(num);
        }
      }
    }

    private void SetupText()
    {
      ConceptCardManager instance = ConceptCardManager.Instance;
      if (Object.op_Equality((Object) instance, (Object) null) || instance.BulkSelectedMaterialList.Count == 0)
        return;
      int mixTotalExp;
      int mixTrustExp;
      ConceptCardManager.CalcTotalExpTrustMaterialData(out mixTotalExp, out mixTrustExp);
      if (Object.op_Inequality((Object) this.GetExp, (Object) null))
        this.GetExp.set_text(mixTotalExp.ToString());
      if (Object.op_Inequality((Object) this.GetTrust, (Object) null))
        ConceptCardManager.SubstituteTrustFormat(instance.SelectedConceptCardData, this.GetTrust, mixTrustExp, false);
      int totalMixZeny = 0;
      ConceptCardManager.GalcTotalMixZenyMaterialData(out totalMixZeny);
      if (!Object.op_Inequality((Object) this.UsedZenny, (Object) null))
        return;
      this.UsedZenny.set_text(totalMixZeny.ToString());
    }
  }
}
