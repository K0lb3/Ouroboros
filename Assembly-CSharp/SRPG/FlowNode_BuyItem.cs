// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_BuyItem
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using System.Collections.Generic;
using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(111, "カケラポイント不足", FlowNode.PinTypes.Output, 21)]
  [FlowNode.Pin(100, "Success", FlowNode.PinTypes.Output, 10)]
  [FlowNode.Pin(104, "ショップ情報がない", FlowNode.PinTypes.Output, 14)]
  [FlowNode.Pin(105, "購入済み", FlowNode.PinTypes.Output, 15)]
  [FlowNode.Pin(106, "アイテム所持上限", FlowNode.PinTypes.Output, 16)]
  [FlowNode.Pin(107, "ゴールド不足", FlowNode.PinTypes.Output, 17)]
  [FlowNode.Pin(108, "課金コイン不足", FlowNode.PinTypes.Output, 18)]
  [FlowNode.Pin(109, "遠征コイン不足", FlowNode.PinTypes.Output, 19)]
  [FlowNode.Pin(110, "アリーナコイン不足", FlowNode.PinTypes.Output, 20)]
  [FlowNode.NodeType("System/BuyItem", 32741)]
  [FlowNode.Pin(1, "Request", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(112, "マルチコイン不足", FlowNode.PinTypes.Output, 22)]
  [FlowNode.Pin(113, "イベントコイン不足", FlowNode.PinTypes.Output, 23)]
  public class FlowNode_BuyItem : FlowNode_Network
  {
    private EShopType mShopType;

    private void UpsightSpendEvent()
    {
      ShopItem shopItem = MonoSingleton<GameManager>.Instance.Player.GetShopData(GlobalVars.ShopType).items[GlobalVars.ShopBuyIndex];
      ItemParam itemParam = MonoSingleton<GameManager>.Instance.GetItemParam(shopItem.iname);
      int inSpendAmount = -1;
      switch (shopItem.saleType)
      {
        case ESaleType.Gold:
          inSpendAmount = (int) itemParam.buy * shopItem.num;
          break;
        case ESaleType.Coin:
          inSpendAmount = (int) itemParam.coin * shopItem.num;
          break;
        case ESaleType.TourCoin:
          inSpendAmount = (int) itemParam.tour_coin * shopItem.num;
          break;
        case ESaleType.ArenaCoin:
          inSpendAmount = (int) itemParam.arena_coin * shopItem.num;
          break;
        case ESaleType.PiecePoint:
          inSpendAmount = (int) itemParam.piece_point * shopItem.num;
          break;
        case ESaleType.MultiCoin:
          inSpendAmount = (int) itemParam.multi_coin * shopItem.num;
          break;
        case ESaleType.EventCoin:
          return;
      }
      if (inSpendAmount <= -1)
        return;
      AnalyticsManager.TrackSpendShop(shopItem.saleType, GlobalVars.ShopType, inSpendAmount);
    }

    public override void OnActivate(int pinID)
    {
      if (pinID != 1)
        return;
      PlayerData player = MonoSingleton<GameManager>.Instance.Player;
      ((Behaviour) this).set_enabled(false);
      ShopData shopData = player.GetShopData(GlobalVars.ShopType);
      if (shopData == null)
      {
        this.ActivateOutputLinks(104);
      }
      else
      {
        ShopItem shopItem = shopData.items[GlobalVars.ShopBuyIndex];
        if (shopItem.is_soldout)
        {
          this.ActivateOutputLinks(105);
        }
        else
        {
          ItemParam itemParam = MonoSingleton<GameManager>.Instance.GetItemParam(shopItem.iname);
          if (!player.CheckItemCapacity(itemParam, shopItem.num))
          {
            this.ActivateOutputLinks(106);
          }
          else
          {
            switch (shopItem.saleType)
            {
              case ESaleType.Gold:
                if (player.Gold < (int) itemParam.buy * shopItem.num)
                {
                  this.ActivateOutputLinks(107);
                  return;
                }
                break;
              case ESaleType.Coin:
                if (player.Coin < (int) itemParam.coin * shopItem.num)
                {
                  this.ActivateOutputLinks(108);
                  return;
                }
                break;
              case ESaleType.TourCoin:
                if (player.TourCoin < (int) itemParam.tour_coin * shopItem.num)
                {
                  this.ActivateOutputLinks(109);
                  return;
                }
                break;
              case ESaleType.ArenaCoin:
                if (player.ArenaCoin < (int) itemParam.arena_coin * shopItem.num)
                {
                  this.ActivateOutputLinks(110);
                  return;
                }
                break;
              case ESaleType.PiecePoint:
                if (player.PiecePoint < (int) itemParam.piece_point * shopItem.num)
                {
                  this.ActivateOutputLinks(111);
                  return;
                }
                break;
              case ESaleType.MultiCoin:
                if (player.MultiCoin < (int) itemParam.multi_coin * shopItem.num)
                {
                  this.ActivateOutputLinks(112);
                  return;
                }
                break;
              case ESaleType.EventCoin:
                DebugUtility.Assert("There is no common price in the event coin.");
                this.ActivateOutputLinks(113);
                return;
            }
            this.mShopType = GlobalVars.ShopType;
            int shopBuyIndex = GlobalVars.ShopBuyIndex;
            if (Network.Mode == Network.EConnectMode.Offline)
            {
              player.DEBUG_BUY_ITEM(this.mShopType, shopBuyIndex);
              ShopParam shopParam = MonoSingleton<GameManager>.Instance.MasterParam.GetShopParam(this.mShopType);
              player.OnBuyAtShop(shopParam.iname, itemParam.iname, shopItem.num);
              this.Success();
            }
            else
            {
              this.ExecRequest((WebAPI) new ReqItemShopBuypaid(this.mShopType.ToString(), shopBuyIndex, new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
              ((Behaviour) this).set_enabled(true);
            }
          }
        }
      }
    }

    private void Success()
    {
      this.UpsightSpendEvent();
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
        WebAPI.JSON_BodyResponse<Json_ShopBuyResponse> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json_ShopBuyResponse>>(www.text);
        DebugUtility.Assert(jsonObject != null, "res == null");
        if (jsonObject.body == null)
        {
          this.OnRetry();
        }
        else
        {
          Network.RemoveAPI();
          ShopData shop = MonoSingleton<GameManager>.Instance.Player.GetShopData(this.mShopType) ?? new ShopData();
          if (!shop.Deserialize(jsonObject.body))
          {
            this.OnFailed();
          }
          else
          {
            MonoSingleton<GameManager>.Instance.Player.SetShopData(this.mShopType, shop);
            ShopParam shopParam = MonoSingleton<GameManager>.Instance.MasterParam.GetShopParam(this.mShopType);
            if (shopParam != null)
            {
              PlayerData player = MonoSingleton<GameManager>.Instance.Player;
              ShopItem shopItem = shop.items[GlobalVars.ShopBuyIndex];
              ItemParam itemParam = MonoSingleton<GameManager>.Instance.GetItemParam(shopItem.iname);
              int num = 0;
              switch (shopItem.saleType)
              {
                case ESaleType.Gold:
                  num = (int) itemParam.buy * shopItem.num;
                  break;
                case ESaleType.Coin:
                  num = (int) itemParam.coin * shopItem.num;
                  break;
                case ESaleType.TourCoin:
                  num = (int) itemParam.tour_coin * shopItem.num;
                  break;
                case ESaleType.ArenaCoin:
                  num = (int) itemParam.arena_coin * shopItem.num;
                  break;
                case ESaleType.PiecePoint:
                  num = (int) itemParam.piece_point * shopItem.num;
                  break;
                case ESaleType.MultiCoin:
                  num = (int) itemParam.multi_coin * shopItem.num;
                  break;
                case ESaleType.EventCoin:
                  num = 0;
                  DebugUtility.Assert("There is no common price in the event coin.");
                  break;
              }
              player.OnBuyAtShop(shopParam.iname, itemParam.iname, shopItem.num);
              AnalyticsManager.TrackCurrencyObtain((AnalyticsManager.CurrencyType) (itemParam.type != EItemType.Ticket ? 4 : 2), AnalyticsManager.CurrencySubType.FREE, (long) shopItem.num, "Shop", new Dictionary<string, object>()
              {
                {
                  "item_id",
                  (object) shopItem.iname
                }
              });
            }
            this.Success();
          }
        }
      }
    }
  }
}
