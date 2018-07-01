// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqCoinShop
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;

namespace SRPG
{
  [FlowNode.Pin(2, "Failed", FlowNode.PinTypes.Output, 2)]
  [FlowNode.Pin(1, "Success", FlowNode.PinTypes.Output, 1)]
  [FlowNode.Pin(0, "Request", FlowNode.PinTypes.Input, 0)]
  [FlowNode.NodeType("System/ReqCoinShop", 32741)]
  public class FlowNode_ReqCoinShop : FlowNode_Network
  {
    private FlowNode_ReqCoinShop.Mode mode;

    public override void OnActivate(int pinID)
    {
      if (pinID != 0)
        return;
      this.mode = FlowNode_ReqCoinShop.Mode.GetShopList;
      this.ExecRequest((WebAPI) new ReqEventShopList(new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
    }

    public override void OnSuccess(WWWResult www)
    {
      if (Network.IsError)
      {
        this.OnRetry();
      }
      else
      {
        if (this.mode == FlowNode_ReqCoinShop.Mode.GetShopList)
        {
          WebAPI.JSON_BodyResponse<JSON_ShopListArray> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<JSON_ShopListArray>>(www.text);
          Network.RemoveAPI();
          if (jsonObject.body.shops != null && jsonObject.body.shops.Length > 0)
          {
            for (int index = 0; index < jsonObject.body.shops.Length; ++index)
            {
              if (jsonObject.body.shops[index] == null)
              {
                this.OnRetry();
                return;
              }
            }
            foreach (JSON_ShopListArray.Shops shop in jsonObject.body.shops)
            {
              if (shop.gname.Split('-')[0] == "EventSummon2")
              {
                MonoSingleton<GameManager>.Instance.Player.UpdateEventCoin();
                EventShopInfo eventShopInfo = new EventShopInfo();
                eventShopInfo.shops = shop;
                Json_ShopMsgResponse msg = EventShopList.ParseMsg(shop);
                if (msg != null)
                {
                  eventShopInfo.banner_sprite = msg.banner;
                  eventShopInfo.shop_cost_iname = msg.costiname;
                  if (msg.update != null)
                    eventShopInfo.btn_update = msg.update.Equals("on");
                  GlobalVars.EventShopItem = eventShopInfo;
                  GlobalVars.ShopType = EShopType.Event;
                  this.mode = FlowNode_ReqCoinShop.Mode.GetCoinNum;
                  this.ExecRequest((WebAPI) new ReqGetCoinNum(new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
                  return;
                }
              }
            }
          }
        }
        else if (this.mode == FlowNode_ReqCoinShop.Mode.GetCoinNum)
        {
          WebAPI.JSON_BodyResponse<FlowNode_ReqCoinShop.JSON_CoinNum> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<FlowNode_ReqCoinShop.JSON_CoinNum>>(www.text);
          Network.RemoveAPI();
          if (jsonObject.body != null && jsonObject.body.item != null && jsonObject.body.item.Length > 0)
            MonoSingleton<GameManager>.Instance.Player.Deserialize(jsonObject.body.item);
          if (jsonObject.body != null && jsonObject.body.newcoin != null)
            GlobalVars.NewSummonCoinInfo = new GlobalVars.SummonCoinInfo()
            {
              Period = jsonObject.body.newcoin.period
            };
          this.ActivateOutputLinks(1);
          return;
        }
        this.ActivateOutputLinks(2);
      }
    }

    private enum Mode
    {
      GetShopList,
      GetCoinNum,
    }

    private class JSON_CoinNum
    {
      public Json_Item[] item;
      public FlowNode_ReqCoinShop.JSON_NewCoin newcoin;
    }

    private class JSON_NewCoin
    {
      public long period;
    }
  }
}
