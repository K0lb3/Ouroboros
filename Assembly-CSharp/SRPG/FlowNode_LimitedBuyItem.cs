// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_LimitedBuyItem
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(110, "アリーナコイン不足", FlowNode.PinTypes.Output, 20)]
  [FlowNode.NodeType("System/LimitedBuyItem", 32741)]
  [FlowNode.Pin(1, "Request", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(100, "Success", FlowNode.PinTypes.Output, 10)]
  [FlowNode.Pin(104, "ショップ情報がない", FlowNode.PinTypes.Output, 14)]
  [FlowNode.Pin(105, "購入済み", FlowNode.PinTypes.Output, 15)]
  [FlowNode.Pin(106, "アイテム所持上限", FlowNode.PinTypes.Output, 16)]
  [FlowNode.Pin(107, "ゴールド不足", FlowNode.PinTypes.Output, 17)]
  [FlowNode.Pin(108, "課金コイン不足", FlowNode.PinTypes.Output, 18)]
  [FlowNode.Pin(109, "遠征コイン不足", FlowNode.PinTypes.Output, 19)]
  [FlowNode.Pin(111, "カケラポイント不足", FlowNode.PinTypes.Output, 21)]
  [FlowNode.Pin(112, "マルチコイン不足", FlowNode.PinTypes.Output, 22)]
  [FlowNode.Pin(113, "有償石不足", FlowNode.PinTypes.Output, 23)]
  [FlowNode.Pin(114, "イベントコイン不足", FlowNode.PinTypes.Output, 24)]
  public class FlowNode_LimitedBuyItem : FlowNode_Network
  {
    private EShopType mShopType;

    public override void OnActivate(int pinID)
    {
      if (pinID != 1)
        return;
      PlayerData player = MonoSingleton<GameManager>.Instance.Player;
      ((Behaviour) this).set_enabled(false);
      LimitedShopData limitedShopData = player.GetLimitedShopData();
      if (limitedShopData == null)
      {
        this.ActivateOutputLinks(104);
      }
      else
      {
        LimitedShopItem shopitem = limitedShopData.items[GlobalVars.ShopBuyIndex];
        if (shopitem.is_soldout)
        {
          this.ActivateOutputLinks(105);
        }
        else
        {
          int buyNum;
          if (shopitem.IsArtifact)
          {
            buyNum = MonoSingleton<GameManager>.Instance.MasterParam.GetArtifactParam(shopitem.iname).GetBuyNum(shopitem.saleType);
          }
          else
          {
            ItemParam itemParam = MonoSingleton<GameManager>.Instance.GetItemParam(shopitem.iname);
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
              this.ActivateOutputLinks(113);
              return;
            case ESaleType.Coin_P:
              if (!this.CheckCanBuy(shopitem, buyNum, player.PaidCoin, 113))
                return;
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
            this.ExecRequest((WebAPI) new ReqItemLimitedShopBuypaid(GlobalVars.LimitedShopItem.shops.gname, GlobalVars.ShopBuyIndex, 1, new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
            ((Behaviour) this).set_enabled(true);
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
        WebAPI.JSON_BodyResponse<Json_LimitedShopBuyResponse> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json_LimitedShopBuyResponse>>(www.text);
        DebugUtility.Assert(jsonObject != null, "res == null");
        if (jsonObject.body == null)
        {
          this.OnRetry();
        }
        else
        {
          Network.RemoveAPI();
          LimitedShopData shop = MonoSingleton<GameManager>.Instance.Player.GetLimitedShopData() ?? new LimitedShopData();
          if (!shop.Deserialize(jsonObject.body))
          {
            this.OnFailed();
          }
          else
          {
            MonoSingleton<GameManager>.Instance.Player.SetLimitedShopData(shop);
            LimitedShopItem limitedShopItem = shop.items[GlobalVars.ShopBuyIndex];
            ItemParam itemParam = MonoSingleton<GameManager>.Instance.GetItemParam(limitedShopItem.iname);
            if (limitedShopItem.isSetSaleValue)
              AnalyticsManager.TrackOriginalCurrencyUse(limitedShopItem.saleType, MonoSingleton<GameManager>.Instance.Player.GetShopUpdateCost(this.mShopType, true), "ShopBuy." + (object) this.mShopType);
            else if ((!limitedShopItem.IsArtifact ? itemParam.GetBuyNum(limitedShopItem.saleType) * limitedShopItem.num : MonoSingleton<GameManager>.Instance.MasterParam.GetArtifactParam(limitedShopItem.iname).GetBuyNum(limitedShopItem.saleType) * limitedShopItem.num) > 0)
              AnalyticsManager.TrackOriginalCurrencyUse(limitedShopItem.saleType, MonoSingleton<GameManager>.Instance.Player.GetShopUpdateCost(this.mShopType, true), "ShopBuy." + (object) this.mShopType);
            this.Success();
          }
        }
      }
    }

    public bool CheckCanBuy(LimitedShopItem shopitem, int buy, int check, int pin)
    {
      int num = !shopitem.isSetSaleValue ? buy * shopitem.num : shopitem.saleValue;
      if (check >= num)
        return true;
      this.ActivateOutputLinks(pin);
      return false;
    }
  }
}
