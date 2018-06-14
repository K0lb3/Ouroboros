// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_BuyItem
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(111, "カケラポイント不足", FlowNode.PinTypes.Output, 21)]
  [FlowNode.Pin(113, "イベントコイン不足", FlowNode.PinTypes.Output, 23)]
  [FlowNode.NodeType("System/BuyItem", 32741)]
  [FlowNode.Pin(100, "Success", FlowNode.PinTypes.Output, 10)]
  [FlowNode.Pin(104, "ショップ情報がない", FlowNode.PinTypes.Output, 14)]
  [FlowNode.Pin(105, "購入済み", FlowNode.PinTypes.Output, 15)]
  [FlowNode.Pin(106, "アイテム所持上限", FlowNode.PinTypes.Output, 16)]
  [FlowNode.Pin(107, "ゴールド不足", FlowNode.PinTypes.Output, 17)]
  [FlowNode.Pin(108, "課金コイン不足", FlowNode.PinTypes.Output, 18)]
  [FlowNode.Pin(109, "遠征コイン不足", FlowNode.PinTypes.Output, 19)]
  [FlowNode.Pin(110, "アリーナコイン不足", FlowNode.PinTypes.Output, 20)]
  [FlowNode.Pin(112, "マルチコイン不足", FlowNode.PinTypes.Output, 22)]
  [FlowNode.Pin(114, "有償石不足", FlowNode.PinTypes.Output, 24)]
  [FlowNode.Pin(1, "Request", FlowNode.PinTypes.Input, 0)]
  public class FlowNode_BuyItem : FlowNode_Network
  {
    private EShopType mShopType;

    private void TrackSpendingEvent()
    {
      ShopItem shopItem = MonoSingleton<GameManager>.Instance.Player.GetShopData(GlobalVars.ShopType).items[GlobalVars.ShopBuyIndex];
      ItemParam itemParam = MonoSingleton<GameManager>.Instance.GetItemParam(shopItem.iname);
      int inAmount = -1;
      switch (shopItem.saleType)
      {
        case ESaleType.Gold:
          inAmount = (int) itemParam.buy * shopItem.num;
          break;
        case ESaleType.Coin:
          inAmount = (int) itemParam.coin * shopItem.num;
          break;
        case ESaleType.TourCoin:
          inAmount = (int) itemParam.tour_coin * shopItem.num;
          break;
        case ESaleType.ArenaCoin:
          inAmount = (int) itemParam.arena_coin * shopItem.num;
          break;
        case ESaleType.PiecePoint:
          inAmount = (int) itemParam.piece_point * shopItem.num;
          break;
        case ESaleType.MultiCoin:
          inAmount = (int) itemParam.multi_coin * shopItem.num;
          break;
        case ESaleType.EventCoin:
          return;
      }
      if (inAmount <= -1)
        return;
      AnalyticsManager.TrackOriginalCurrencyUse(shopItem.saleType, inAmount, "ShopBuy." + (object) GlobalVars.ShopType);
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
        ShopItem shopitem = shopData.items[GlobalVars.ShopBuyIndex];
        if (shopitem.is_soldout)
        {
          this.ActivateOutputLinks(105);
        }
        else
        {
          ItemParam itemParam = (ItemParam) null;
          int buyNum;
          if (shopitem.IsArtifact)
          {
            buyNum = MonoSingleton<GameManager>.Instance.MasterParam.GetArtifactParam(shopitem.iname).GetBuyNum(shopitem.saleType);
          }
          else
          {
            itemParam = MonoSingleton<GameManager>.Instance.GetItemParam(shopitem.iname);
            if (!shopitem.IsSet && !player.CheckItemCapacity(itemParam, shopitem.num))
            {
              this.ActivateOutputLinks(106);
              return;
            }
            buyNum = itemParam.GetBuyNum(shopitem.saleType);
          }
          switch (shopitem.saleType)
          {
            case ESaleType.Gold:
              if (!this.CheckCanBuy(shopitem, buyNum, player.Gold, 107))
                return;
              break;
            case ESaleType.Coin:
              if (!this.CheckCanBuy(shopitem, buyNum, player.Coin, 108))
                return;
              break;
            case ESaleType.TourCoin:
              if (!this.CheckCanBuy(shopitem, buyNum, player.TourCoin, 109))
                return;
              break;
            case ESaleType.ArenaCoin:
              if (!this.CheckCanBuy(shopitem, buyNum, player.ArenaCoin, 110))
                return;
              break;
            case ESaleType.PiecePoint:
              if (!this.CheckCanBuy(shopitem, buyNum, player.PiecePoint, 111))
                return;
              break;
            case ESaleType.MultiCoin:
              if (!this.CheckCanBuy(shopitem, buyNum, player.MultiCoin, 112))
                return;
              break;
            case ESaleType.EventCoin:
              DebugUtility.Assert("There is no common price in the event coin.");
              this.ActivateOutputLinks(113);
              return;
            case ESaleType.Coin_P:
              if (!this.CheckCanBuy(shopitem, buyNum, player.PaidCoin, 114))
                return;
              break;
          }
          this.mShopType = GlobalVars.ShopType;
          int shopBuyIndex = GlobalVars.ShopBuyIndex;
          if (Network.Mode == Network.EConnectMode.Offline)
          {
            if (itemParam == null)
              return;
            player.DEBUG_BUY_ITEM(this.mShopType, shopBuyIndex);
            ShopParam shopParam = MonoSingleton<GameManager>.Instance.MasterParam.GetShopParam(this.mShopType);
            player.OnBuyAtShop(shopParam.iname, itemParam.iname, shopitem.num);
            this.Success();
          }
          else
          {
            this.ExecRequest((WebAPI) new ReqItemShopBuypaid(this.mShopType.ToString(), shopBuyIndex, 1, new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
            ((Behaviour) this).set_enabled(true);
          }
        }
      }
    }

    private void Success()
    {
      this.TrackSpendingEvent();
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
              string iname = shopItem.iname;
              if (shopItem.isSetSaleValue)
              {
                MyMetaps.TrackSpendShop(shopItem.saleType, this.mShopType, shopItem.saleValue);
              }
              else
              {
                int num = !shopItem.IsArtifact ? MonoSingleton<GameManager>.Instance.GetItemParam(shopItem.iname).GetBuyNum(shopItem.saleType) * shopItem.num : MonoSingleton<GameManager>.Instance.MasterParam.GetArtifactParam(shopItem.iname).GetBuyNum(shopItem.saleType) * shopItem.num;
                if (num > 0)
                  MyMetaps.TrackSpendShop(shopItem.saleType, this.mShopType, num);
              }
              player.OnBuyAtShop(shopParam.iname, iname, shopItem.num);
            }
            this.Success();
          }
        }
      }
    }

    public bool CheckCanBuy(ShopItem shopitem, int buy, int check, int pin)
    {
      int num = !shopitem.isSetSaleValue ? buy * shopitem.num : shopitem.saleValue;
      if (check >= num)
        return true;
      this.ActivateOutputLinks(pin);
      return false;
    }
  }
}
