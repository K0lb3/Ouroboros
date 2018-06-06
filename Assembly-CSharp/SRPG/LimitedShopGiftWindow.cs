// Decompiled with JetBrains decompiler
// Type: SRPG.LimitedShopGiftWindow
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using System.Collections.Generic;
using UnityEngine;

namespace SRPG
{
  public class LimitedShopGiftWindow : MonoBehaviour
  {
    [SerializeField]
    private GameObject ItemParent;
    [SerializeField]
    private GameObject ItemTemplate;
    private List<GameObject> mItems;

    public LimitedShopGiftWindow()
    {
      base.\u002Ector();
    }

    private void Start()
    {
      if (Object.op_Inequality((Object) this.ItemTemplate, (Object) null))
        this.ItemTemplate.SetActive(false);
      this.Refresh();
    }

    private void Refresh()
    {
      GameUtility.DestroyGameObjects(this.mItems);
      this.mItems.Clear();
      LimitedShopData limitedShopData = MonoSingleton<GameManager>.Instance.Player.GetLimitedShopData();
      if (limitedShopData == null || limitedShopData.items.Count <= 0)
        return;
      int shopBuyIndex = GlobalVars.ShopBuyIndex;
      if (!Object.op_Inequality((Object) this.ItemTemplate, (Object) null))
        return;
      Transform transform = !Object.op_Inequality((Object) this.ItemParent, (Object) null) ? this.ItemTemplate.get_transform().get_parent() : this.ItemParent.get_transform();
      LimitedShopItem limitedShopItem = limitedShopData.items[shopBuyIndex];
      if (limitedShopItem.children != null && limitedShopItem.children.Length > 0)
      {
        foreach (Json_ShopItemDesc child in limitedShopItem.children)
        {
          ItemData data = new ItemData();
          data.Setup(0L, child.iname, child.num);
          if (data != null)
          {
            GameObject gameObject = (GameObject) Object.Instantiate<GameObject>((M0) this.ItemTemplate);
            gameObject.get_transform().SetParent(transform, false);
            DataSource.Bind<ItemData>(gameObject, data);
            this.mItems.Add(gameObject);
            gameObject.SetActive(true);
          }
        }
      }
      GameParameter.UpdateAll(((Component) transform).get_gameObject());
    }
  }
}
