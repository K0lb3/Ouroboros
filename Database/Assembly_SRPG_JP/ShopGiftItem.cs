// Decompiled with JetBrains decompiler
// Type: SRPG.ShopGiftItem
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class ShopGiftItem : MonoBehaviour
  {
    public Text Amount;
    public Text Name;
    public GameObject ItemIcon;
    public GameObject ArtifactIcon;
    public ConceptCardIcon m_ConceptCardIcon;

    public ShopGiftItem()
    {
      base.\u002Ector();
    }

    public void SetShopItemInfo(Json_ShopItemDesc shop_item_desc, string name, int amount)
    {
      this.ItemIcon.SetActive(false);
      this.ArtifactIcon.SetActive(false);
      ((Component) this.m_ConceptCardIcon).get_gameObject().SetActive(false);
      if (shop_item_desc.IsItem)
        this.ItemIcon.SetActive(true);
      else if (shop_item_desc.IsArtifact)
        this.ArtifactIcon.SetActive(true);
      else if (shop_item_desc.IsConceptCard)
        ((Component) this.m_ConceptCardIcon).get_gameObject().SetActive(true);
      this.Amount.set_text((shop_item_desc.num * amount).ToString());
      this.Name.set_text(name);
    }
  }
}
