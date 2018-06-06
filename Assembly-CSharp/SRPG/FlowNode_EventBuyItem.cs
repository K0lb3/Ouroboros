// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_EventBuyItem
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(1, "Request", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(114, "イベントコイン不足", FlowNode.PinTypes.Output, 24)]
  [FlowNode.Pin(113, "有償石不足", FlowNode.PinTypes.Output, 23)]
  [FlowNode.Pin(112, "マルチコイン不足", FlowNode.PinTypes.Output, 22)]
  [FlowNode.Pin(111, "カケラポイント不足", FlowNode.PinTypes.Output, 21)]
  [FlowNode.Pin(110, "アリーナコイン不足", FlowNode.PinTypes.Output, 20)]
  [FlowNode.Pin(109, "遠征コイン不足", FlowNode.PinTypes.Output, 19)]
  [FlowNode.Pin(108, "課金コイン不足", FlowNode.PinTypes.Output, 18)]
  [FlowNode.Pin(107, "ゴールド不足", FlowNode.PinTypes.Output, 17)]
  [FlowNode.Pin(106, "アイテム所持上限", FlowNode.PinTypes.Output, 16)]
  [FlowNode.Pin(105, "購入済み", FlowNode.PinTypes.Output, 15)]
  [FlowNode.Pin(104, "ショップ情報がない", FlowNode.PinTypes.Output, 14)]
  [FlowNode.Pin(100, "Success", FlowNode.PinTypes.Output, 10)]
  [FlowNode.NodeType("System/EventBuyItem", 32741)]
  public class FlowNode_EventBuyItem : FlowNode_Network
  {
    private EShopType mShopType;

    public override void OnActivate(int pinID)
    {
      if (pinID != 1)
        return;
      PlayerData player = MonoSingleton<GameManager>.Instance.Player;
      ((Behaviour) this).set_enabled(false);
      EventShopData eventShopData = player.GetEventShopData();
      if (eventShopData == null)
      {
        this.ActivateOutputLinks(104);
      }
      else
      {
        EventShopItem eventShopItem = eventShopData.items[GlobalVars.ShopBuyIndex];
        if (eventShopItem.is_soldout)
        {
          this.ActivateOutputLinks(105);
        }
        else
        {
          ItemParam itemParam = MonoSingleton<GameManager>.Instance.GetItemParam(eventShopItem.iname);
          if (!eventShopItem.IsSet && !player.CheckItemCapacity(itemParam, eventShopItem.num))
          {
            this.ActivateOutputLinks(106);
          }
          else
          {
            switch (eventShopItem.saleType)
            {
              case ESaleType.Gold:
                int num1 = !eventShopItem.isSetSaleValue ? (int) itemParam.buy * eventShopItem.num : eventShopItem.saleValue;
                if (player.Gold < num1)
                {
                  this.ActivateOutputLinks(107);
                  return;
                }
                break;
              case ESaleType.Coin:
                int num2 = !eventShopItem.isSetSaleValue ? (int) itemParam.coin * eventShopItem.num : eventShopItem.saleValue;
                if (player.Coin < num2)
                {
                  this.ActivateOutputLinks(108);
                  return;
                }
                break;
              case ESaleType.TourCoin:
                int num3 = !eventShopItem.isSetSaleValue ? (int) itemParam.tour_coin * eventShopItem.num : eventShopItem.saleValue;
                if (player.TourCoin < num3)
                {
                  this.ActivateOutputLinks(109);
                  return;
                }
                break;
              case ESaleType.ArenaCoin:
                int num4 = !eventShopItem.isSetSaleValue ? (int) itemParam.arena_coin * eventShopItem.num : eventShopItem.saleValue;
                if (player.ArenaCoin < num4)
                {
                  this.ActivateOutputLinks(110);
                  return;
                }
                break;
              case ESaleType.PiecePoint:
                int num5 = !eventShopItem.isSetSaleValue ? (int) itemParam.piece_point * eventShopItem.num : eventShopItem.saleValue;
                if (player.PiecePoint < num5)
                {
                  this.ActivateOutputLinks(111);
                  return;
                }
                break;
              case ESaleType.MultiCoin:
                int num6 = !eventShopItem.isSetSaleValue ? (int) itemParam.multi_coin * eventShopItem.num : eventShopItem.saleValue;
                if (player.MultiCoin < num6)
                {
                  this.ActivateOutputLinks(112);
                  return;
                }
                break;
              case ESaleType.EventCoin:
                if (eventShopItem.isSetSaleValue)
                {
                  int saleValue = eventShopItem.saleValue;
                  if (player.EventCoinNum(eventShopItem.cost_iname) < saleValue)
                  {
                    this.ActivateOutputLinks(114);
                    return;
                  }
                  break;
                }
                break;
              case ESaleType.Coin_P:
                int num7 = !eventShopItem.isSetSaleValue ? (int) itemParam.coin * eventShopItem.num : eventShopItem.saleValue;
                if (player.PaidCoin < num7)
                {
                  this.ActivateOutputLinks(113);
                  return;
                }
                break;
            }
            if (Network.Mode == Network.EConnectMode.Offline)
            {
              player.DEBUG_BUY_ITEM(GlobalVars.ShopType, GlobalVars.ShopBuyIndex);
              this.Success();
            }
            else
            {
              this.mShopType = GlobalVars.ShopType;
              this.ExecRequest((WebAPI) new ReqItemEventShopBuypaid(GlobalVars.EventShopItem.shops.gname, GlobalVars.ShopBuyIndex, 1, new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
              ((Behaviour) this).set_enabled(true);
            }
          }
        }
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
          case Network.EErrCode.ShopSoldOut:
          case Network.EErrCode.ShopBuyCostShort:
          case Network.EErrCode.ShopBuyLvShort:
          case Network.EErrCode.ShopBuyNotFound:
          case Network.EErrCode.ShopBuyItemNotFound:
            this.OnBack();
            break;
          default:
            this.OnRetry();
            break;
        }
      }
      else
      {
        WebAPI.JSON_BodyResponse<Json_EventShopBuyResponse> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json_EventShopBuyResponse>>(www.text);
        DebugUtility.Assert(jsonObject != null, "res == null");
        if (jsonObject.body == null)
        {
          this.OnRetry();
        }
        else
        {
          Network.RemoveAPI();
          EventShopData shop = MonoSingleton<GameManager>.Instance.Player.GetEventShopData() ?? new EventShopData();
          if (!shop.Deserialize(jsonObject.body))
          {
            this.OnFailed();
          }
          else
          {
            MonoSingleton<GameManager>.Instance.Player.SetEventShopData(shop);
            EventShopItem eventShopItem = shop.items[GlobalVars.ShopBuyIndex];
            if (eventShopItem.saleValue > 0)
            {
              PlayerData player = MonoSingleton<GameManager>.Instance.Player;
              AnalyticsManager.TrackSpendShop(eventShopItem.saleType, this.mShopType, player.GetShopUpdateCost(this.mShopType, true));
            }
            this.Success();
          }
        }
      }
    }
  }
}
