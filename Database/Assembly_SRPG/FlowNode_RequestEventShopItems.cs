// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_RequestEventShopItems
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using System.Collections.Generic;
using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(1, "Success", FlowNode.PinTypes.Output, 1)]
  [FlowNode.Pin(0, "Request", FlowNode.PinTypes.Input, 0)]
  [FlowNode.NodeType("System/RequestEventShopItems", 32741)]
  [FlowNode.Pin(11, "Period", FlowNode.PinTypes.Output, 11)]
  public class FlowNode_RequestEventShopItems : FlowNode_Network
  {
    public const string ErrorWindowPrefabPath = "e/UI/NetworkErrorWindowEx";

    public override void OnActivate(int pinID)
    {
      if (pinID != 0 || ((Behaviour) this).get_enabled())
        return;
      if (Network.Mode == Network.EConnectMode.Online)
      {
        this.ExecRequest((WebAPI) new ReqEventShopItemList(GlobalVars.EventShopItem.shops.gname, new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
        ((Behaviour) this).set_enabled(true);
      }
      else
        this.Success();
    }

    private void Success()
    {
      ((Behaviour) this).set_enabled(false);
      this.ActivateOutputLinks(1);
    }

    private void Period()
    {
      if (Network.IsImmediateMode)
        return;
      ((NetworkErrorWindowEx) Object.Instantiate<NetworkErrorWindowEx>(Resources.Load<NetworkErrorWindowEx>("e/UI/NetworkErrorWindowEx"))).Body = Network.ErrMsg;
    }

    private void OnPeriod()
    {
      this.Period();
      Network.RemoveAPI();
      Network.ResetError();
      ((Behaviour) this).set_enabled(false);
      this.ActivateOutputLinks(11);
    }

    public override void OnSuccess(WWWResult www)
    {
      if (Network.IsError)
      {
        if (Network.ErrCode == Network.EErrCode.LimitedShopOutOfPeriod)
          this.OnPeriod();
        else
          this.OnRetry();
      }
      else
      {
        WebAPI.JSON_BodyResponse<Json_EventShopResponse> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json_EventShopResponse>>(www.text);
        DebugUtility.Assert(jsonObject != null, "res == null");
        if (jsonObject.body == null)
        {
          this.OnRetry();
        }
        else
        {
          List<JSON_EventShopItemListSet> eventShopItemListSetList = new List<JSON_EventShopItemListSet>((IEnumerable<JSON_EventShopItemListSet>) jsonObject.body.shopitems);
          jsonObject.body.shopitems = eventShopItemListSetList.ToArray();
          Network.RemoveAPI();
          EventShopData shop = MonoSingleton<GameManager>.Instance.Player.GetEventShopData() ?? new EventShopData();
          if (!shop.Deserialize(jsonObject.body))
          {
            this.OnFailed();
          }
          else
          {
            MonoSingleton<GameManager>.Instance.Player.SetEventShopData(shop);
            this.Success();
          }
        }
      }
    }
  }
}
