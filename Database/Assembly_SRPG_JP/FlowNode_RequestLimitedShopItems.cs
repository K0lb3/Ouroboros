// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_RequestLimitedShopItems
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;
using UnityEngine;

namespace SRPG
{
  [FlowNode.NodeType("System/RequestLimitedShopItems", 32741)]
  [FlowNode.Pin(0, "Request", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(1, "Success", FlowNode.PinTypes.Output, 1)]
  [FlowNode.Pin(11, "Period", FlowNode.PinTypes.Output, 11)]
  public class FlowNode_RequestLimitedShopItems : FlowNode_Network
  {
    public const string ErrorWindowPrefabPath = "e/UI/NetworkErrorWindowEx";

    public override void OnActivate(int pinID)
    {
      if (pinID != 0 || ((Behaviour) this).get_enabled())
        return;
      if (Network.Mode == Network.EConnectMode.Online)
      {
        this.ExecRequest((WebAPI) new ReqLimitedShopItemList(GlobalVars.LimitedShopItem.shops.gname, new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
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
        if (Network.ErrCode == Network.EErrCode.ShopBuyOutofPeriod)
          this.OnPeriod();
        else
          this.OnRetry();
      }
      else
      {
        WebAPI.JSON_BodyResponse<Json_LimitedShopResponse> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json_LimitedShopResponse>>(www.text);
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
            this.Success();
          }
        }
      }
    }
  }
}
