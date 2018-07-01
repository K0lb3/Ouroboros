// Decompiled with JetBrains decompiler
// Type: SRPG.ConceptCardRarityCheck
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

namespace SRPG
{
  public class ConceptCardRarityCheck : MonoBehaviour
  {
    [SerializeField]
    private GameObject mCardObjectTemplate;
    [SerializeField]
    private RectTransform mCardObjectParent;
    [SerializeField]
    private LText mLText;
    [SerializeField]
    private GameObject mButtonEnhance;
    [SerializeField]
    private GameObject mButtonSell;

    public ConceptCardRarityCheck()
    {
      base.\u002Ector();
    }

    private void Start()
    {
      if (Object.op_Equality((Object) this.mCardObjectTemplate, (Object) null) && Object.op_Equality((Object) this.mCardObjectParent, (Object) null))
      {
        Debug.LogWarning((object) "mCardObject is null");
      }
      else
      {
        this.mCardObjectTemplate.SetActive(false);
        ConceptCardManager instance = ConceptCardManager.Instance;
        if (Object.op_Equality((Object) instance, (Object) null))
          return;
        this.mLText.set_text(string.Format(LocalizedText.Get(this.mLText.get_text()), (object) instance.CostConceptCardRare.ToString()));
        if (instance.IsDetailActive)
        {
          this.mButtonEnhance.SetActive(true);
          this.mButtonSell.SetActive(false);
        }
        else if (instance.IsSellListActive)
        {
          this.mButtonEnhance.SetActive(false);
          this.mButtonSell.SetActive(true);
        }
        else
        {
          Debug.LogWarning((object) "Must be from Sell or Enhance");
          return;
        }
        using (List<ConceptCardData>.Enumerator enumerator = instance.SelectedMaterials.GetList().GetEnumerator())
        {
          while (enumerator.MoveNext())
          {
            ConceptCardData current = enumerator.Current;
            if ((int) current.Rarity + 1 >= instance.CostConceptCardRare)
            {
              GameObject gameObject = (GameObject) Object.Instantiate<GameObject>((M0) this.mCardObjectTemplate);
              gameObject.get_transform().SetParent((Transform) this.mCardObjectParent, false);
              ConceptCardIcon component = (ConceptCardIcon) gameObject.GetComponent<ConceptCardIcon>();
              if (Object.op_Inequality((Object) component, (Object) null))
                component.Setup(current);
              gameObject.SetActive(true);
            }
          }
        }
      }
    }
  }
}
