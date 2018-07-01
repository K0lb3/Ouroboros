// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_SetEventShopList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(10, "Success", FlowNode.PinTypes.Output, 10)]
  [FlowNode.NodeType("System/SetEventShopList", 32741)]
  [FlowNode.Pin(2, "IsEventShop", FlowNode.PinTypes.Input, 2)]
  [FlowNode.Pin(1, "Request", FlowNode.PinTypes.Input, 0)]
  public class FlowNode_SetEventShopList : FlowNode_Network
  {
    private int inputPin = 1;
    public EventShopList event_shop_list;

    public override void OnActivate(int pinID)
    {
      if (pinID != 1 && pinID != 2 || ((Behaviour) this).get_enabled())
        return;
      this.inputPin = pinID;
      if (Network.Mode == Network.EConnectMode.Online)
      {
        this.ExecRequest((WebAPI) new ReqEventShopList(new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
        ((Behaviour) this).set_enabled(true);
      }
      else
        this.Success();
    }

    public override void OnSuccess(WWWResult www)
    {
      if (Network.IsError)
      {
        Network.EErrCode errCode = Network.ErrCode;
        this.OnRetry();
      }
      else
      {
        WebAPI.JSON_BodyResponse<JSON_ShopListArray> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<JSON_ShopListArray>>(www.text);
        DebugUtility.Assert(jsonObject != null, "res == null");
        Network.RemoveAPI();
        this.event_shop_list.DestroyItems();
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
          MonoSingleton<GameManager>.Instance.Player.UpdateEventCoin();
          if (this.inputPin != 2)
            this.event_shop_list.AddEventShopList(jsonObject.body.shops);
        }
        this.event_shop_list.AddArenaShopList();
        this.event_shop_list.AddMultiShopList();
        this.Success();
      }
    }

    public void Success()
    {
      ((Behaviour) this).set_enabled(false);
      this.ActivateOutputLinks(10);
    }

    public long GetEventShopPeriodEndAt(JSON_ShopListArray list)
    {
      long num = 0;
      if (list != null && list.shops.Length > 0)
      {
        for (int index = 0; index < list.shops.Length; ++index)
        {
          long end = list.shops[index].end;
          num = num >= end ? num : end;
        }
      }
      return num;
    }
  }
}
