// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqFriendFollower
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using System;
using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(0, "Request", FlowNode.PinTypes.Input, 0)]
  [FlowNode.NodeType("Network/ReqFriendFollower", 32741)]
  [FlowNode.Pin(1, "Success", FlowNode.PinTypes.Output, 1)]
  public class FlowNode_ReqFriendFollower : FlowNode_Network
  {
    public override void OnActivate(int pinID)
    {
      if (pinID != 0 || ((Behaviour) this).get_enabled())
        return;
      if (Network.Mode == Network.EConnectMode.Offline)
      {
        this.Success();
      }
      else
      {
        this.ExecRequest((WebAPI) new ReqFriendFollower(new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
        ((Behaviour) this).set_enabled(true);
      }
    }

    private void Success()
    {
      this.ActivateOutputLinks(1);
    }

    public override void OnSuccess(WWWResult www)
    {
      if (Network.IsError)
      {
        this.OnFailed();
      }
      else
      {
        WebAPI.JSON_BodyResponse<Json_PlayerDataAll> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json_PlayerDataAll>>(www.text);
        DebugUtility.Assert(jsonObject != null, "res == null");
        Network.RemoveAPI();
        GameManager instance = MonoSingleton<GameManager>.Instance;
        try
        {
          instance.Deserialize(jsonObject.body.friends, FriendStates.Follwer);
          this.Success();
        }
        catch (Exception ex)
        {
          DebugUtility.LogException(ex);
          return;
        }
        if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this, (UnityEngine.Object) null))
          return;
        ((Behaviour) this).set_enabled(false);
      }
    }
  }
}
