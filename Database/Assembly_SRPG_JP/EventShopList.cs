// Decompiled with JetBrains decompiler
// Type: SRPG.EventShopList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;
using System;
using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(103, "アリーナショップが選択された", FlowNode.PinTypes.Output, 103)]
  [FlowNode.Pin(102, "更新なし：イベントショップが選択された", FlowNode.PinTypes.Output, 102)]
  [FlowNode.Pin(101, "更新あり：イベントショップが選択された", FlowNode.PinTypes.Output, 101)]
  [FlowNode.Pin(1, "所持コイン更新", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(0, "指定イベントショップの商品を開く", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(104, "マルチショップが選択された", FlowNode.PinTypes.Output, 104)]
  public class EventShopList : SRPG_ListBase, IFlowInterface
  {
    [Description("リストアイテムとして使用するゲームオブジェクト")]
    public GameObject ItemTemplate;
    public GameObject ArenaShopTemplate;
    public GameObject MultiShopTemplate;

    public void Activated(int pinID)
    {
      if (pinID == 0)
      {
        switch (GlobalVars.SelectionCoinListType)
        {
          case GlobalVars.CoinListSelectionType.EventShop:
            if (UnityEngine.Object.op_Inequality((UnityEngine.Object) GlobalVars.SelectionEventShop, (UnityEngine.Object) null))
            {
              GlobalVars.EventShopItem = GlobalVars.SelectionEventShop.EventShopInfo;
              GlobalVars.ShopType = EShopType.Event;
              Network.RequestAPI((WebAPI) new ReqGetCoinNum(new Network.ResponseCallback(this.ResponseCallback)), false);
              break;
            }
            break;
          case GlobalVars.CoinListSelectionType.ArenaShop:
            FlowNode_GameObject.ActivateOutputLinks((Component) this, 103);
            break;
          case GlobalVars.CoinListSelectionType.MultiShop:
            FlowNode_GameObject.ActivateOutputLinks((Component) this, 104);
            break;
        }
      }
      if (pinID != 1)
        return;
      GameParameter.UpdateAll(((Component) this).get_gameObject());
    }

    protected override void Start()
    {
      if (UnityEngine.Object.op_Implicit((UnityEngine.Object) this.ItemTemplate))
        this.ItemTemplate.SetActive(false);
      if (UnityEngine.Object.op_Implicit((UnityEngine.Object) this.ArenaShopTemplate))
        this.ArenaShopTemplate.SetActive(false);
      if (UnityEngine.Object.op_Implicit((UnityEngine.Object) this.MultiShopTemplate))
        this.MultiShopTemplate.SetActive(false);
      base.Start();
    }

    public void DestroyItems()
    {
      if (GlobalVars.EventShopListItems.Count <= 0)
        return;
      for (int index = GlobalVars.EventShopListItems.Count - 1; index >= 0; --index)
      {
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) GlobalVars.EventShopListItems[index], (UnityEngine.Object) null))
        {
          ((ListItemEvents) ((Component) GlobalVars.EventShopListItems[index]).GetComponent<ListItemEvents>()).OnSelect = (ListItemEvents.ListItemEvent) null;
          UnityEngine.Object.Destroy((UnityEngine.Object) GlobalVars.EventShopListItems[index]);
        }
      }
      GlobalVars.EventShopListItems.Clear();
    }

    private void OnSelectItem(GameObject go)
    {
      EventShopListItem component = (EventShopListItem) go.GetComponent<EventShopListItem>();
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) component, (UnityEngine.Object) null))
        return;
      GlobalVars.EventShopItem = component.EventShopInfo;
      GlobalVars.ShopType = EShopType.Event;
      Network.RequestAPI((WebAPI) new ReqGetCoinNum(new Network.ResponseCallback(this.ResponseCallback)), false);
    }

    private void ResponseCallback(WWWResult www)
    {
      if (FlowNode_Network.HasCommonError(www))
        return;
      if (Network.IsError)
      {
        FlowNode_Network.Retry();
      }
      else
      {
        WebAPI.JSON_BodyResponse<EventShopList.JSON_CoinNum> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<EventShopList.JSON_CoinNum>>(www.text);
        Network.RemoveAPI();
        if (jsonObject.body != null && jsonObject.body.item != null && jsonObject.body.item.Length > 0)
          MonoSingleton<GameManager>.Instance.Player.Deserialize(jsonObject.body.item);
        if (jsonObject.body != null && jsonObject.body.newcoin != null)
          GlobalVars.NewSummonCoinInfo = new GlobalVars.SummonCoinInfo()
          {
            Period = jsonObject.body.newcoin.period
          };
        if (GlobalVars.EventShopItem.btn_update)
          FlowNode_GameObject.ActivateOutputLinks((Component) this, 101);
        else
          FlowNode_GameObject.ActivateOutputLinks((Component) this, 102);
      }
    }

    public void AddEventShopList(JSON_ShopListArray.Shops[] shops)
    {
      for (int index = 0; index < shops.Length; ++index)
      {
        Json_ShopMsgResponse msg = EventShopList.ParseMsg(shops[index]);
        if (msg != null && msg.hide == 0)
        {
          GameObject gameObject = (GameObject) UnityEngine.Object.Instantiate<GameObject>((M0) this.ItemTemplate);
          EventShopListItem component = (EventShopListItem) gameObject.GetComponent<EventShopListItem>();
          component.SetShopList(shops[index], msg);
          gameObject.get_transform().SetParent(((Component) this).get_transform());
          gameObject.get_transform().set_localScale(this.ItemTemplate.get_transform().get_localScale());
          ((ListItemEvents) gameObject.GetComponent<ListItemEvents>()).OnSelect = new ListItemEvents.ListItemEvent(this.OnSelectItem);
          gameObject.SetActive(true);
          GlobalVars.EventShopListItems.Add(component);
        }
      }
    }

    public static Json_ShopMsgResponse ParseMsg(JSON_ShopListArray.Shops shops)
    {
      try
      {
        return JSONParser.parseJSONObject<Json_ShopMsgResponse>(shops.info.msg);
      }
      catch (Exception ex)
      {
        Debug.LogError((object) ("Parse failed: " + shops.info.msg));
        return (Json_ShopMsgResponse) null;
      }
    }

    private void OnSelectArenaItem(GameObject go)
    {
      GlobalVars.ShopType = EShopType.Arena;
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 103);
    }

    public void AddArenaShopList()
    {
      GameObject gameObject = (GameObject) UnityEngine.Object.Instantiate<GameObject>((M0) this.ArenaShopTemplate);
      gameObject.get_transform().SetParent(((Component) this).get_transform());
      gameObject.get_transform().set_localScale(this.ItemTemplate.get_transform().get_localScale());
      ((ListItemEvents) gameObject.GetComponent<ListItemEvents>()).OnSelect = new ListItemEvents.ListItemEvent(this.OnSelectArenaItem);
      gameObject.SetActive(true);
    }

    private void OnSelectMultiItem(GameObject go)
    {
      GlobalVars.ShopType = EShopType.Multi;
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 104);
    }

    public void AddMultiShopList()
    {
      GameObject gameObject = (GameObject) UnityEngine.Object.Instantiate<GameObject>((M0) this.MultiShopTemplate);
      gameObject.get_transform().SetParent(((Component) this).get_transform());
      gameObject.get_transform().set_localScale(this.ItemTemplate.get_transform().get_localScale());
      ((ListItemEvents) gameObject.GetComponent<ListItemEvents>()).OnSelect = new ListItemEvents.ListItemEvent(this.OnSelectMultiItem);
      gameObject.SetActive(true);
    }

    private class JSON_CoinNum
    {
      public Json_Item[] item;
      public EventShopList.JSON_NewCoin newcoin;
    }

    private class JSON_NewCoin
    {
      public long period;
    }
  }
}
