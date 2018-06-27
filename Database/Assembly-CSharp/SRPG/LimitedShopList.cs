// Decompiled with JetBrains decompiler
// Type: SRPG.LimitedShopList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(101, "限定ショップが選択された", FlowNode.PinTypes.Output, 101)]
  public class LimitedShopList : SRPG_ListBase, IFlowInterface
  {
    private List<LimitedShopListItem> limited_shop_list = new List<LimitedShopListItem>();
    [Description("リストアイテムとして使用するゲームオブジェクト")]
    public GameObject ItemTemplate;
    [Range(0.0f, 100f)]
    public int NowOpenShopCount;

    protected override void Start()
    {
      base.Start();
    }

    private void RefreshItems(JSON_ShopListArray.Shops[] shops)
    {
      this.NowOpenShopCount = shops.Length;
      for (int count = this.limited_shop_list.Count; count < this.NowOpenShopCount; ++count)
      {
        GameObject gameObject = (GameObject) Object.Instantiate<GameObject>((M0) this.ItemTemplate);
        LimitedShopListItem component = (LimitedShopListItem) gameObject.GetComponent<LimitedShopListItem>();
        component.SetShopList(shops[count]);
        gameObject.get_transform().SetParent(((Component) this).get_transform());
        gameObject.get_transform().set_localScale(this.ItemTemplate.get_transform().get_localScale());
        ((ListItemEvents) gameObject.GetComponent<ListItemEvents>()).OnSelect = new ListItemEvents.ListItemEvent(this.OnSelectItem);
        gameObject.SetActive(true);
        this.limited_shop_list.Add(component);
      }
    }

    private void DestroyItems()
    {
      if (this.limited_shop_list.Count <= 0)
        return;
      for (int index = this.NowOpenShopCount - 1; index >= 0; --index)
      {
        ((ListItemEvents) ((Component) this.limited_shop_list[index]).GetComponent<ListItemEvents>()).OnSelect = (ListItemEvents.ListItemEvent) null;
        Object.Destroy((Object) this.limited_shop_list[index]);
      }
      this.limited_shop_list.Clear();
    }

    private void OnSelectItem(GameObject go)
    {
      LimitedShopListItem component = (LimitedShopListItem) go.GetComponent<LimitedShopListItem>();
      if (Object.op_Equality((Object) component, (Object) null))
        return;
      GlobalVars.LimitedShopItem = component;
      GlobalVars.ShopType = EShopType.Limited;
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 101);
    }

    public void Activated(int pinID)
    {
    }

    public void SetLimitedShopList(JSON_ShopListArray.Shops[] shops)
    {
      this.DestroyItems();
      this.RefreshItems(shops);
    }
  }
}
