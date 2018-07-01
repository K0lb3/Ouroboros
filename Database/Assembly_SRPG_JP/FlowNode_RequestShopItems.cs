// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_RequestShopItems
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;
using UnityEngine;

namespace SRPG
{
  [FlowNode.NodeType("System/RequestShopItems", 32741)]
  [FlowNode.Pin(0, "Request", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(1, "Success", FlowNode.PinTypes.Output, 1)]
  public class FlowNode_RequestShopItems : FlowNode_Network
  {
    private EShopType mShopType;

    public override void OnActivate(int pinID)
    {
      if (pinID != 0 || ((Behaviour) this).get_enabled())
        return;
      if (Network.Mode == Network.EConnectMode.Online)
      {
        this.mShopType = GlobalVars.ShopType;
        string str = this.mShopType.ToString();
        if (this.mShopType == EShopType.Guerrilla)
          this.ExecRequest((WebAPI) new ReqItemGuerrillaShop(str, new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
        else
          this.ExecRequest((WebAPI) new ReqItemShop(str, new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
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

    public override void OnSuccess(WWWResult www)
    {
      if (Network.IsError)
      {
        Network.EErrCode errCode = Network.ErrCode;
        this.OnRetry();
      }
      else
      {
        WebAPI.JSON_BodyResponse<Json_ShopResponse> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json_ShopResponse>>(www.text);
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
            this.Success();
          }
        }
      }
    }
  }
}
