// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_BuyItemUpdate
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using System.Collections.Generic;
using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(103, "ゴールド不足", FlowNode.PinTypes.Output, 13)]
  [FlowNode.Pin(109, "イベントコイン不足", FlowNode.PinTypes.Output, 19)]
  [FlowNode.Pin(108, "マルチコイン不足", FlowNode.PinTypes.Output, 18)]
  [FlowNode.Pin(107, "欠片ポイント不足", FlowNode.PinTypes.Output, 17)]
  [FlowNode.Pin(106, "アリーナコイン不足", FlowNode.PinTypes.Output, 16)]
  [FlowNode.Pin(105, "遠征コイン不足", FlowNode.PinTypes.Output, 15)]
  [FlowNode.Pin(104, "課金コイン不足", FlowNode.PinTypes.Output, 14)]
  [FlowNode.Pin(101, "ショップ情報が存在しない", FlowNode.PinTypes.Output, 11)]
  [FlowNode.Pin(100, "Success", FlowNode.PinTypes.Output, 10)]
  [FlowNode.Pin(1, "Request", FlowNode.PinTypes.Input, 0)]
  [FlowNode.NodeType("System/BuyItemUpdate", 32741)]
  public class FlowNode_BuyItemUpdate : FlowNode_Network
  {
    private EShopType mShopType;

    public override void OnActivate(int pinID)
    {
      if (pinID != 1 || ((Behaviour) this).get_enabled())
        return;
      this.mShopType = GlobalVars.ShopType;
      ShopData shopData = MonoSingleton<GameManager>.Instance.Player.GetShopData(this.mShopType);
      ShopParam shopParam = MonoSingleton<GameManager>.Instance.MasterParam.GetShopParam(this.mShopType);
      ((Behaviour) this).set_enabled(false);
      if (shopData == null || shopParam == null)
      {
        this.ActivateOutputLinks(101);
      }
      else
      {
        PlayerData player = MonoSingleton<GameManager>.Instance.Player;
        int shopUpdateCost = player.GetShopUpdateCost(this.mShopType, false);
        switch (shopParam.UpdateCostType)
        {
          case ESaleType.Gold:
            if (player.Gold < shopUpdateCost)
            {
              this.ActivateOutputLinks(103);
              return;
            }
            break;
          case ESaleType.Coin:
            if (player.Coin < shopUpdateCost)
            {
              this.ActivateOutputLinks(104);
              return;
            }
            break;
          case ESaleType.TourCoin:
            if (player.TourCoin < shopUpdateCost)
            {
              this.ActivateOutputLinks(105);
              return;
            }
            break;
          case ESaleType.ArenaCoin:
            if (player.ArenaCoin < shopUpdateCost)
            {
              this.ActivateOutputLinks(106);
              return;
            }
            break;
          case ESaleType.PiecePoint:
            if (player.PiecePoint < shopUpdateCost)
            {
              this.ActivateOutputLinks(107);
              return;
            }
            break;
          case ESaleType.MultiCoin:
            if (player.MultiCoin < shopUpdateCost)
            {
              this.ActivateOutputLinks(108);
              return;
            }
            break;
          case ESaleType.EventCoin:
            if (player.EventCoinNum(GlobalVars.EventShopItem.shop_cost_iname) < shopUpdateCost)
            {
              this.ActivateOutputLinks(109);
              return;
            }
            break;
        }
        if (Network.Mode == Network.EConnectMode.Online)
        {
          if (this.mShopType == EShopType.Event)
            this.ExecRequest((WebAPI) new ReqItemEventShopUpdate(GlobalVars.EventShopItem.shops.gname, GlobalVars.EventShopItem.shop_cost_iname, new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
          else if (this.mShopType == EShopType.Limited)
            this.ExecRequest((WebAPI) new ReqItemShopUpdate(GlobalVars.LimitedShopItem.shops.gname, new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
          else
            this.ExecRequest((WebAPI) new ReqItemShopUpdate(this.mShopType.ToString(), new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
          ((Behaviour) this).set_enabled(true);
        }
        else if (!player.CheckShopUpdateCost(GlobalVars.ShopType))
          this.ActivateOutputLinks(104);
        else
          this.Success();
      }
    }

    private void Success()
    {
      ((Behaviour) this).set_enabled(false);
      this.ActivateOutputLinks(100);
    }

    public override void OnSuccess(WWWResult www)
    {
      if (Network.IsError)
      {
        switch (Network.ErrCode)
        {
          case Network.EErrCode.ShopRefreshCostShort:
          case Network.EErrCode.ShopRefreshLvSort:
            this.OnBack();
            break;
          default:
            this.OnRetry();
            break;
        }
      }
      else
      {
        switch (GlobalVars.ShopType)
        {
          case EShopType.Event:
            WebAPI.JSON_BodyResponse<Json_EventShopUpdateResponse> jsonObject1 = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json_EventShopUpdateResponse>>(www.text);
            DebugUtility.Assert(jsonObject1 != null, "res == null");
            if (jsonObject1.body == null)
            {
              this.OnRetry();
              return;
            }
            List<JSON_EventShopItemListSet> eventShopItemListSetList = new List<JSON_EventShopItemListSet>((IEnumerable<JSON_EventShopItemListSet>) jsonObject1.body.shopitems);
            jsonObject1.body.shopitems = eventShopItemListSetList.ToArray();
            Network.RemoveAPI();
            EventShopData shop1 = MonoSingleton<GameManager>.Instance.Player.GetEventShopData() ?? new EventShopData();
            if (!shop1.Deserialize(jsonObject1.body))
            {
              this.OnFailed();
              return;
            }
            MonoSingleton<GameManager>.Instance.Player.SetEventShopData(shop1);
            break;
          case EShopType.Limited:
            WebAPI.JSON_BodyResponse<Json_LimitedShopUpdateResponse> jsonObject2 = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json_LimitedShopUpdateResponse>>(www.text);
            DebugUtility.Assert(jsonObject2 != null, "res == null");
            if (jsonObject2.body == null)
            {
              this.OnRetry();
              return;
            }
            List<JSON_LimitedShopItemListSet> limitedShopItemListSetList = new List<JSON_LimitedShopItemListSet>((IEnumerable<JSON_LimitedShopItemListSet>) jsonObject2.body.shopitems);
            jsonObject2.body.shopitems = limitedShopItemListSetList.ToArray();
            Network.RemoveAPI();
            LimitedShopData shop2 = MonoSingleton<GameManager>.Instance.Player.GetLimitedShopData() ?? new LimitedShopData();
            if (!shop2.Deserialize(jsonObject2.body))
            {
              this.OnFailed();
              return;
            }
            MonoSingleton<GameManager>.Instance.Player.SetLimitedShopData(shop2);
            break;
          default:
            WebAPI.JSON_BodyResponse<Json_ShopUpdateResponse> jsonObject3 = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json_ShopUpdateResponse>>(www.text);
            DebugUtility.Assert(jsonObject3 != null, "res == null");
            if (jsonObject3.body == null)
            {
              this.OnRetry();
              return;
            }
            Network.RemoveAPI();
            ShopData shop3 = MonoSingleton<GameManager>.Instance.Player.GetShopData(this.mShopType) ?? new ShopData();
            if (!shop3.Deserialize(jsonObject3.body))
            {
              this.OnFailed();
              return;
            }
            MonoSingleton<GameManager>.Instance.Player.SetShopData(this.mShopType, shop3);
            break;
        }
        ShopParam shopParam = MonoSingleton<GameManager>.Instance.MasterParam.GetShopParam(this.mShopType);
        if (shopParam != null)
          AnalyticsManager.TrackOriginalCurrencyUse(shopParam.UpdateCostType, MonoSingleton<GameManager>.Instance.Player.GetShopUpdateCost(this.mShopType, true), "ShopUpdate." + (object) this.mShopType);
        this.Success();
      }
    }
  }
}
