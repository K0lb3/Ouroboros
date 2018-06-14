// Decompiled with JetBrains decompiler
// Type: SRPG.EventShopBuySetConfirmWindow
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using System.Collections.Generic;
using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(1, "Refresh", FlowNode.PinTypes.Input, 1)]
  public class EventShopBuySetConfirmWindow : MonoBehaviour, IFlowInterface
  {
    [Description("リストアイテムとして使用するゲームオブジェクト")]
    public GameObject ItemTemplate;
    public GameObject ItemParent;
    private List<EventShopSetItemListElement> event_shop_item_set_list;

    public EventShopBuySetConfirmWindow()
    {
      base.\u002Ector();
    }

    private void Awake()
    {
    }

    private void Start()
    {
      this.Refresh();
    }

    public void Activated(int pinID)
    {
      if (pinID != 1)
        return;
      this.Refresh();
    }

    private void Refresh()
    {
      EventShopItem data = MonoSingleton<GameManager>.Instance.Player.GetEventShopData().items[GlobalVars.ShopBuyIndex];
      ItemData itemDataByItemId = MonoSingleton<GameManager>.Instance.Player.FindItemDataByItemID(data.iname);
      this.event_shop_item_set_list.Clear();
      DataSource.Bind<EventShopItem>(((Component) this).get_gameObject(), data);
      DataSource.Bind<ItemData>(((Component) this).get_gameObject(), itemDataByItemId);
      DataSource.Bind<ItemParam>(((Component) this).get_gameObject(), MonoSingleton<GameManager>.Instance.GetItemParam(data.iname));
      GameParameter.UpdateAll(((Component) this).get_gameObject());
    }
  }
}
