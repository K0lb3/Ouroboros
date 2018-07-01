// Decompiled with JetBrains decompiler
// Type: SRPG.ShopTimeOutItem
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class ShopTimeOutItem : MonoBehaviour
  {
    public Text Amount;
    public Text Name;
    public GameObject ItemIcon;
    public GameObject ArtifactIcon;
    public ConceptCardIcon m_ConceptCardIcon;

    public ShopTimeOutItem()
    {
      base.\u002Ector();
    }

    public void SetShopItemInfo(ShopItem shop_item, string name)
    {
      this.ItemIcon.SetActive(false);
      this.ArtifactIcon.SetActive(false);
      ((Component) this.m_ConceptCardIcon).get_gameObject().SetActive(false);
      if (shop_item.IsItem)
        this.ItemIcon.SetActive(true);
      else if (shop_item.IsArtifact)
        this.ArtifactIcon.SetActive(true);
      else if (shop_item.IsConceptCard)
        ((Component) this.m_ConceptCardIcon).get_gameObject().SetActive(true);
      else
        this.ItemIcon.SetActive(true);
      this.Amount.set_text(shop_item.num.ToString());
      this.Name.set_text(name);
    }
  }
}
