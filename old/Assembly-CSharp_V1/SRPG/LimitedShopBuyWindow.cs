// Decompiled with JetBrains decompiler
// Type: SRPG.LimitedShopBuyWindow
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  [FlowNode.Pin(1, "Refresh", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(101, "アイテム選択セット", FlowNode.PinTypes.Output, 101)]
  [FlowNode.Pin(100, "アイテム選択単品", FlowNode.PinTypes.Output, 100)]
  [FlowNode.Pin(102, "アイテム更新", FlowNode.PinTypes.Output, 102)]
  public class LimitedShopBuyWindow : MonoBehaviour, IFlowInterface
  {
    public RectTransform ItemLayoutParent;
    public GameObject ItemTemplate;
    public Button BtnUpdated;
    public Text TxtTitle;
    public Text TxtUpdateTime;
    public Text TxtUpdated;
    public Text ShopName;
    public List<GameObject> mBuyItems;

    public LimitedShopBuyWindow()
    {
      base.\u002Ector();
    }

    private void Awake()
    {
      if (Object.op_Inequality((Object) this.ItemTemplate, (Object) null) && this.ItemTemplate.get_activeInHierarchy())
        this.ItemTemplate.SetActive(false);
      if (Object.op_Inequality((Object) this.TxtTitle, (Object) null))
        this.TxtTitle.set_text(LocalizedText.Get("sys.SHOP_BUY_TITLE"));
      if (Object.op_Inequality((Object) this.TxtUpdateTime, (Object) null))
        this.TxtUpdateTime.set_text(LocalizedText.Get("sys.UPDATE_TIME"));
      if (!Object.op_Inequality((Object) this.TxtUpdated, (Object) null))
        return;
      this.TxtUpdated.set_text(LocalizedText.Get("sys.CMD_UPDATED"));
    }

    private void Start()
    {
    }

    public void Activated(int pinID)
    {
      this.Refresh();
    }

    private void Refresh()
    {
      if (Object.op_Equality((Object) this.ItemTemplate, (Object) null))
        return;
      LimitedShopData limitedShopData = MonoSingleton<GameManager>.Instance.Player.GetLimitedShopData();
      DebugUtility.Assert(limitedShopData != null, "Shop information doesn't exist.");
      this.ShopName.set_text(LocalizedText.Get(GlobalVars.LimitedShopItem.shops.info.title));
      for (int index = 0; index < this.mBuyItems.Count; ++index)
        this.mBuyItems[index].get_gameObject().SetActive(false);
      int count = limitedShopData.items.Count;
      for (int index = 0; index < count; ++index)
      {
        LimitedShopItem data1 = limitedShopData.items[index];
        if (index >= this.mBuyItems.Count)
        {
          GameObject gameObject = (GameObject) Object.Instantiate<GameObject>((M0) this.ItemTemplate);
          gameObject.get_transform().SetParent((Transform) this.ItemLayoutParent, false);
          this.mBuyItems.Add(gameObject);
        }
        GameObject mBuyItem = this.mBuyItems[index];
        LimitedShopBuyList componentInChildren = (LimitedShopBuyList) mBuyItem.GetComponentInChildren<LimitedShopBuyList>();
        componentInChildren.limitedShopItem = data1;
        componentInChildren.amount.SetActive(!data1.IsSet);
        DataSource.Bind<LimitedShopItem>(mBuyItem, data1);
        ItemData data2 = new ItemData();
        data2.Setup(0L, data1.iname, data1.num);
        DataSource.Bind<ItemData>(mBuyItem, data2);
        DataSource.Bind<ItemParam>(mBuyItem, MonoSingleton<GameManager>.Instance.GetItemParam(data1.iname));
        ListItemEvents component1 = (ListItemEvents) mBuyItem.GetComponent<ListItemEvents>();
        if (Object.op_Inequality((Object) component1, (Object) null))
          component1.OnSelect = new ListItemEvents.ListItemEvent(this.OnSelect);
        Button component2 = (Button) mBuyItem.GetComponent<Button>();
        if (Object.op_Inequality((Object) component2, (Object) null))
          ((Selectable) component2).set_interactable(!data1.is_soldout);
        mBuyItem.SetActive(true);
      }
      GameParameter.UpdateAll(((Component) this).get_gameObject());
    }

    private void OnSelect(GameObject go)
    {
      GlobalVars.ShopBuyIndex = this.mBuyItems.FindIndex((Predicate<GameObject>) (p => Object.op_Equality((Object) p, (Object) go)));
      if (!((LimitedShopBuyList) this.mBuyItems[GlobalVars.ShopBuyIndex].GetComponent<LimitedShopBuyList>()).limitedShopItem.IsSet)
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 100);
      else
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 101);
    }
  }
}
