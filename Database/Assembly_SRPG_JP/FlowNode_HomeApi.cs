// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_HomeApi
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;
using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(21, "Failed", FlowNode.PinTypes.Output, 21)]
  [FlowNode.NodeType("System/HomeApi", 32741)]
  [FlowNode.Pin(100, "Start", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(20, "Success", FlowNode.PinTypes.Output, 20)]
  public class FlowNode_HomeApi : FlowNode_Network
  {
    public override void OnActivate(int pinID)
    {
      if (pinID != 100)
        return;
      if (Network.Mode == Network.EConnectMode.Online)
      {
        bool isMultiPush = false;
        if (Object.op_Inequality((Object) MonoSingleton<GameManager>.Instance, (Object) null) && MonoSingleton<GameManager>.Instance.Player != null)
          isMultiPush = MonoSingleton<GameManager>.Instance.Player.MultiInvitaionFlag;
        MultiInvitationBadge.isValid = false;
        this.ExecRequest((WebAPI) new HomeApi(isMultiPush, new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
        ((Behaviour) this).set_enabled(true);
      }
      else
        this._Success();
    }

    private void _Success()
    {
      this.ActivateOutputLinks(20);
      ((Behaviour) this).set_enabled(false);
    }

    private void _Failed()
    {
      Network.RemoveAPI();
      Network.ResetError();
      this.ActivateOutputLinks(20);
      ((Behaviour) this).set_enabled(false);
    }

    public override void OnSuccess(WWWResult www)
    {
      if (Network.IsError)
      {
        this._Failed();
      }
      else
      {
        DebugMenu.Log("API", "homeapi:{" + www.text + "}");
        WebAPI.JSON_BodyResponse<FlowNode_HomeApi.JSON_HomeApiResponse> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<FlowNode_HomeApi.JSON_HomeApiResponse>>(www.text);
        DebugUtility.Assert(jsonObject != null, "res == null");
        Network.RemoveAPI();
        if (jsonObject.body != null && jsonObject.body.player != null)
        {
          MonoSingleton<GameManager>.Instance.Player.ValidGpsGift = jsonObject.body.player.areamail_enabled != 0;
          MonoSingleton<GameManager>.Instance.Player.ValidFriendPresent = jsonObject.body.player.present_granted != 0;
          MultiInvitationReceiveWindow.SetBadge(jsonObject.body.player.multi_inv != 0);
          MonoSingleton<GameManager>.Instance.Player.FirstChargeStatus = jsonObject.body.player.charge_bonus;
        }
        else
        {
          MonoSingleton<GameManager>.Instance.Player.ValidGpsGift = false;
          MonoSingleton<GameManager>.Instance.Player.ValidFriendPresent = false;
          MultiInvitationReceiveWindow.SetBadge(false);
          MonoSingleton<GameManager>.Instance.Player.FirstChargeStatus = 0;
        }
        if (jsonObject.body != null && jsonObject.body.pubinfo != null)
          LoginNewsInfo.SetPubInfo(jsonObject.body.pubinfo);
        this._Success();
      }
    }

    public class JSON_HomeApiResponse
    {
      public FlowNode_HomeApi.JSON_HomeApiResponse.Player player;
      public LoginNewsInfo.JSON_PubInfo pubinfo;

      public class Player
      {
        public int areamail_enabled;
        public int present_granted;
        public int multi_inv;
        public int charge_bonus;
      }
    }
  }
}
