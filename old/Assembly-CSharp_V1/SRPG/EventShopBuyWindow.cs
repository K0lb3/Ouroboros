// Decompiled with JetBrains decompiler
// Type: SRPG.EventShopBuyWindow
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SRPG
{
  [FlowNode.Pin(1, "Refresh", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(101, "アイテム更新", FlowNode.PinTypes.Output, 101)]
  [FlowNode.Pin(100, "アイテム選択", FlowNode.PinTypes.Output, 100)]
  public class EventShopBuyWindow : MonoBehaviour, IFlowInterface
  {
    public RectTransform ItemLayoutParent;
    public GameObject ItemTemplate;
    public Button BtnUpdated;
    public GameObject ObjUpdateBtn;
    public GameObject ObjUpdateTime;
    public GameObject ObjLineup;
    public GameObject ObjItemNumLimit;
    public Text TxtPossessionCoinNum;
    public GameObject ImgEventCoinType;
    public Text ShopName;
    public List<GameObject> mBuyItems;

    public EventShopBuyWindow()
    {
      base.\u002Ector();
    }

    private void Awake()
    {
      if (Object.op_Inequality((Object) this.ItemTemplate, (Object) null) && this.ItemTemplate.get_activeInHierarchy())
        this.ItemTemplate.SetActive(false);
      if (Object.op_Inequality((Object) this.BtnUpdated, (Object) null))
      {
        // ISSUE: method pointer
        ((UnityEvent) this.BtnUpdated.get_onClick()).AddListener(new UnityAction((object) this, __methodptr(OnItemUpdated)));
      }
      bool btnUpdate = GlobalVars.EventShopItem.btn_update;
      if (Object.op_Inequality((Object) this.ObjUpdateBtn, (Object) null))
        this.ObjUpdateBtn.SetActive(btnUpdate);
      if (Object.op_Inequality((Object) this.ObjUpdateTime, (Object) null))
        this.ObjUpdateTime.SetActive(btnUpdate);
      if (Object.op_Inequality((Object) this.ObjLineup, (Object) null))
        this.ObjLineup.SetActive(btnUpdate);
      if (Object.op_Inequality((Object) this.ObjItemNumLimit, (Object) null))
        this.ObjItemNumLimit.SetActive(!btnUpdate);
      if (!Object.op_Inequality((Object) this.ShopName, (Object) null))
        return;
      this.ShopName.set_text(LocalizedText.Get(GlobalVars.EventShopItem.shops.info.title));
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
      MonoSingleton<GameManager>.Instance.Player.UpdateEventCoin();
      if (Object.op_Equality((Object) this.ItemTemplate, (Object) null))
        return;
      this.SetPossessionCoinParam();
      PlayerData player = MonoSingleton<GameManager>.Instance.Player;
      EventShopData eventShopData = player.GetEventShopData();
      DebugUtility.Assert(eventShopData != null, "ショップ情報が存在しない");
      for (int index = 0; index < this.mBuyItems.Count; ++index)
        this.mBuyItems[index].get_gameObject().SetActive(false);
      int count = eventShopData.items.Count;
      for (int index = 0; index < count; ++index)
      {
        EventShopItem data = eventShopData.items[index];
        if (index >= this.mBuyItems.Count)
        {
          GameObject gameObject = (GameObject) Object.Instantiate<GameObject>((M0) this.ItemTemplate);
          gameObject.get_transform().SetParent((Transform) this.ItemLayoutParent, false);
          this.mBuyItems.Add(gameObject);
        }
        GameObject mBuyItem = this.mBuyItems[index];
        ((EventShopBuyList) mBuyItem.GetComponentInChildren<EventShopBuyList>()).eventShopItem = data;
        DataSource.Bind<EventShopItem>(mBuyItem, data);
        ItemData itemDataByItemId = player.FindItemDataByItemID(data.iname);
        DataSource.Bind<ItemData>(mBuyItem, itemDataByItemId);
        DataSource.Bind<ItemParam>(mBuyItem, MonoSingleton<GameManager>.Instance.GetItemParam(data.iname));
        ListItemEvents component1 = (ListItemEvents) mBuyItem.GetComponent<ListItemEvents>();
        if (Object.op_Inequality((Object) component1, (Object) null))
          component1.OnSelect = new ListItemEvents.ListItemEvent(this.OnSelect);
        Button component2 = (Button) mBuyItem.GetComponent<Button>();
        if (Object.op_Inequality((Object) component2, (Object) null))
          ((Selectable) component2).set_interactable(!data.is_soldout);
        mBuyItem.SetActive(true);
      }
      GameParameter.UpdateAll(((Component) this).get_gameObject());
    }

    private void OnSelect(GameObject go)
    {
      GlobalVars.ShopBuyIndex = this.mBuyItems.FindIndex((Predicate<GameObject>) (p => Object.op_Equality((Object) p, (Object) go)));
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 100);
    }

    private void OnItemUpdated()
    {
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 101);
    }

    private void SetPossessionCoinParam()
    {
      if (Object.op_Inequality((Object) this.ImgEventCoinType, (Object) null))
        DataSource.Bind<ItemParam>(this.ImgEventCoinType, MonoSingleton<GameManager>.Instance.GetItemParam(GlobalVars.EventShopItem.shop_cost_iname));
      if (!Object.op_Inequality((Object) this.TxtPossessionCoinNum, (Object) null))
        return;
      DataSource.Bind<EventCoinData>(((Component) this.TxtPossessionCoinNum).get_gameObject(), MonoSingleton<GameManager>.Instance.Player.EventCoinList.Find((Predicate<EventCoinData>) (f => f.iname.Equals(GlobalVars.EventShopItem.shop_cost_iname))));
    }
  }
}
