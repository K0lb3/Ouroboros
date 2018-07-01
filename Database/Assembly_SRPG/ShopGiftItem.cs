// Decompiled with JetBrains decompiler
// Type: SRPG.ShopGiftItem
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

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

    public ShopGiftItem()
    {
      base.\u002Ector();
    }

    public void SetShopItemInfo(Json_ShopItemDesc shop_item_desc, string name)
    {
      this.ItemIcon.SetActive(!shop_item_desc.IsArtifact);
      this.ArtifactIcon.SetActive(shop_item_desc.IsArtifact);
      this.Amount.set_text(shop_item_desc.num.ToString());
      this.Name.set_text(name);
    }
  }
}
