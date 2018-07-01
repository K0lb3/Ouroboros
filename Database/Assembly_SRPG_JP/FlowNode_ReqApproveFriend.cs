// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqApproveFriend
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;
using System;
using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(0, "Request", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(1, "Success", FlowNode.PinTypes.Output, 1)]
  [FlowNode.NodeType("System/ApproveFriend", 32741)]
  public class FlowNode_ReqApproveFriend : FlowNode_Network
  {
    private string req_fuid;

    public override void OnActivate(int pinID)
    {
      this.req_fuid = (string) null;
      if (pinID != 0)
        return;
      if (Network.Mode == Network.EConnectMode.Offline)
      {
        this.Success();
      }
      else
      {
        string fuid = (string) null;
        if (!string.IsNullOrEmpty(GlobalVars.SelectedFriendID))
          fuid = GlobalVars.SelectedFriendID;
        else if (GlobalVars.FoundFriend != null && !string.IsNullOrEmpty(GlobalVars.FoundFriend.FUID))
          fuid = GlobalVars.FoundFriend.FUID;
        if (fuid == null)
        {
          this.Success();
        }
        else
        {
          this.ExecRequest((WebAPI) new ReqFriendApprove(fuid, new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
          this.req_fuid = fuid;
          ((Behaviour) this).set_enabled(true);
        }
      }
    }

    private void Success()
    {
      ((Behaviour) this).set_enabled(false);
      GameParameter.UpdateValuesOfType(GameParameter.ParameterTypes.PLAYER_FRIENDNUM);
      this.ActivateOutputLinks(1);
    }

    public override void OnSuccess(WWWResult www)
    {
      if (Network.IsError)
      {
        switch (Network.ErrCode)
        {
          case Network.EErrCode.ApprRequestMax:
          case Network.EErrCode.ApprFriendIsFull:
            this.OnBack();
            break;
          default:
            this.OnRetry();
            break;
        }
      }
      else
      {
        WebAPI.JSON_BodyResponse<Json_ApproveFriend> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json_ApproveFriend>>(www.text);
        DebugUtility.Assert(jsonObject != null, "res == null");
        if (jsonObject.body == null)
        {
          this.OnRetry();
        }
        else
        {
          try
          {
            MonoSingleton<GameManager>.Instance.Player.RemoveFriendFollower(this.req_fuid);
            MonoSingleton<GameManager>.Instance.Deserialize(jsonObject.body.player);
            MonoSingleton<GameManager>.Instance.Deserialize(jsonObject.body.friends, FriendStates.Friend);
            MonoSingleton<GameManager>.Instance.Player.FirstFriendCount = jsonObject.body.first_count;
          }
          catch (Exception ex)
          {
            this.OnRetry(ex);
            return;
          }
          Network.RemoveAPI();
          this.Success();
        }
      }
    }
  }
}
