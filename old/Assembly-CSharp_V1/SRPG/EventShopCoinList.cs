// Decompiled with JetBrains decompiler
// Type: SRPG.EventShopCoinList
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
  [FlowNode.Pin(101, "イベントショップが押下された", FlowNode.PinTypes.Output, 101)]
  [FlowNode.Pin(1, "Refresh", FlowNode.PinTypes.Input, 1)]
  public class EventShopCoinList : MonoBehaviour, IFlowInterface
  {
    private const int PIN_ID_REFRESH = 1;
    private const int PIN_ID_SHOPBTN = 101;
    [SerializeField]
    private GameObject ItemTemplate;
    [SerializeField]
    private GameObject ArenaTemplate;
    [SerializeField]
    private GameObject MultiTemplate;
    [SerializeField]
    private ListExtras ScrollView;
    private List<GameObject> mEventCoinListItems;

    public EventShopCoinList()
    {
      base.\u002Ector();
    }

    private void ActivateOutputLinks(int pinID)
    {
      FlowNode_GameObject.ActivateOutputLinks((Component) this, pinID);
    }

    public void Activated(int pinID)
    {
      if (pinID != 1)
        return;
      this.OnRefresh();
    }

    private void Awake()
    {
      GlobalVars.SelectionEventShop = (EventShopListItem) null;
      GlobalVars.SelectionCoinListType = GlobalVars.CoinListSelectionType.None;
      if (Object.op_Inequality((Object) this.ItemTemplate, (Object) null) && this.ItemTemplate.get_activeInHierarchy())
        this.ItemTemplate.SetActive(false);
      if (Object.op_Inequality((Object) this.ArenaTemplate, (Object) null) && this.ArenaTemplate.get_activeInHierarchy())
        this.ArenaTemplate.SetActive(false);
      if (!Object.op_Inequality((Object) this.MultiTemplate, (Object) null) || !this.MultiTemplate.get_activeInHierarchy())
        return;
      this.MultiTemplate.SetActive(false);
    }

    private GameObject CreateListItem(EventCoinData eventcoin_data)
    {
      GameObject gameObject = (GameObject) Object.Instantiate<GameObject>((M0) this.ItemTemplate);
      EventCoinListItem component1 = (EventCoinListItem) gameObject.GetComponent<EventCoinListItem>();
      Button component2 = (Button) component1.Button.GetComponent<Button>();
      ListItemEvents component3 = (ListItemEvents) component1.Button.GetComponent<ListItemEvents>();
      if (Object.op_Inequality((Object) component2, (Object) null) && Object.op_Inequality((Object) component3, (Object) null))
      {
        EventShopListItem eventShopListItem = GlobalVars.EventShopListItems.Find((Predicate<EventShopListItem>) (f => f.shop_cost_iname.Equals(eventcoin_data.iname)));
        bool flag = false;
        if (Object.op_Inequality((Object) eventShopListItem, (Object) null) && eventShopListItem.shops != null && eventShopListItem.shops.unlock != null && (eventShopListItem.shops.unlock.flg != 1 ? 0 : 1) != 0)
          flag = true;
        if (flag)
        {
          component3.OnSelect = new ListItemEvents.ListItemEvent(this.OnSelect);
          ((Selectable) component2).set_interactable(true);
        }
        else
          ((Selectable) component2).set_interactable(false);
      }
      return gameObject;
    }

    private GameObject CreateOtherListItem(GameObject template, ListItemEvents.ListItemEvent func, bool is_button_enable)
    {
      GameObject gameObject = (GameObject) Object.Instantiate<GameObject>((M0) template);
      EventCoinListItem component1 = (EventCoinListItem) gameObject.GetComponent<EventCoinListItem>();
      Button component2 = (Button) component1.Button.GetComponent<Button>();
      ListItemEvents component3 = (ListItemEvents) component1.Button.GetComponent<ListItemEvents>();
      if (Object.op_Inequality((Object) component3, (Object) null) && is_button_enable)
      {
        component3.OnSelect = func;
        ((Selectable) component2).set_interactable(true);
      }
      else
        ((Selectable) component2).set_interactable(false);
      return gameObject;
    }

    private void UpdateItems()
    {
      MonoSingleton<GameManager>.Instance.Player.UpdateEventCoin();
      if (Object.op_Equality((Object) this.ItemTemplate, (Object) null))
        return;
      List<EventCoinData> eventCoinList = MonoSingleton<GameManager>.Instance.Player.EventCoinList;
      Transform transform = ((Component) this).get_transform();
      for (int index = 0; index < eventCoinList.Count; ++index)
      {
        GameObject listItem = this.CreateListItem(eventCoinList[index]);
        listItem.get_transform().SetParent(transform, false);
        this.mEventCoinListItems.Add(listItem);
        listItem.SetActive(true);
        EventCoinData data = eventCoinList[index];
        DataSource.Bind<EventCoinData>(listItem, data);
      }
      if (Object.op_Inequality((Object) this.ArenaTemplate, (Object) null))
      {
        GameObject otherListItem = this.CreateOtherListItem(this.ArenaTemplate, new ListItemEvents.ListItemEvent(this.OnSelectArenaShop), MonoSingleton<GameManager>.Instance.Player.CheckUnlock(UnlockTargets.Arena));
        otherListItem.get_transform().SetParent(transform, false);
        this.mEventCoinListItems.Add(otherListItem);
        otherListItem.SetActive(true);
      }
      if (Object.op_Inequality((Object) this.MultiTemplate, (Object) null))
      {
        GameObject otherListItem = this.CreateOtherListItem(this.MultiTemplate, new ListItemEvents.ListItemEvent(this.OnSelectMultiShop), true);
        otherListItem.get_transform().SetParent(transform, false);
        this.mEventCoinListItems.Add(otherListItem);
        otherListItem.SetActive(true);
      }
      GameParameter.UpdateAll(((Component) this).get_gameObject());
    }

    private void OnRefresh()
    {
      this.UpdateItems();
    }

    private void OnSelect(GameObject go)
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      EventShopCoinList.\u003COnSelect\u003Ec__AnonStorey246 selectCAnonStorey246 = new EventShopCoinList.\u003COnSelect\u003Ec__AnonStorey246();
      // ISSUE: reference to a compiler-generated field
      selectCAnonStorey246.coindata = DataSource.FindDataOfClass<EventCoinData>(go, (EventCoinData) null);
      // ISSUE: reference to a compiler-generated field
      if (selectCAnonStorey246.coindata.iname == null)
        return;
      GlobalVars.SelectionCoinListType = GlobalVars.CoinListSelectionType.EventShop;
      // ISSUE: reference to a compiler-generated method
      GlobalVars.SelectionEventShop = GlobalVars.EventShopListItems.Find(new Predicate<EventShopListItem>(selectCAnonStorey246.\u003C\u003Em__27A));
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 101);
    }

    private void OnSelectArenaShop(GameObject go)
    {
      GlobalVars.SelectionCoinListType = GlobalVars.CoinListSelectionType.ArenaShop;
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 101);
    }

    private void OnSelectMultiShop(GameObject go)
    {
      GlobalVars.SelectionCoinListType = GlobalVars.CoinListSelectionType.MultiShop;
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 101);
    }
  }
}
