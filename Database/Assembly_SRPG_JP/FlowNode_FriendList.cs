// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_FriendList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;
using System;
using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(1, "Success", FlowNode.PinTypes.Output, 1)]
  [FlowNode.NodeType("Network/FriendList", 32741)]
  [FlowNode.Pin(2, "Request(フレンド申請も含む)", FlowNode.PinTypes.Input, 2)]
  [FlowNode.Pin(0, "Request", FlowNode.PinTypes.Input, 0)]
  public class FlowNode_FriendList : FlowNode_Network
  {
    private bool IsFollower;

    public override void OnActivate(int pinID)
    {
      if (pinID != 0 && pinID != 2)
        return;
      this.IsFollower = pinID == 2;
      if (((Behaviour) this).get_enabled())
        return;
      if (Network.Mode == Network.EConnectMode.Offline)
      {
        this.Success();
      }
      else
      {
        this.ExecRequest((WebAPI) new ReqFriendList(this.IsFollower, new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
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
        DebugMenu.Log("API", "friend:" + www.text);
        WebAPI.JSON_BodyResponse<Json_FriendList> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json_FriendList>>(www.text);
        DebugUtility.Assert(jsonObject != null, "res == null");
        Network.RemoveAPI();
        GameManager instance = MonoSingleton<GameManager>.Instance;
        try
        {
          instance.Deserialize(jsonObject.body.friends, FriendStates.Friend);
          if (this.IsFollower)
          {
            instance.Player.FollowerNum = jsonObject.body.follower_count;
            InnWindow componentInChildren = (InnWindow) ((Component) this).get_gameObject().GetComponentInChildren<InnWindow>();
            if (UnityEngine.Object.op_Inequality((UnityEngine.Object) componentInChildren, (UnityEngine.Object) null))
              componentInChildren.Refresh();
          }
          instance.Player.FirstFriendCount = jsonObject.body.first_count;
          this.Success();
        }
        catch (Exception ex)
        {
          DebugUtility.LogException(ex);
          return;
        }
        ((Behaviour) this).set_enabled(false);
      }
    }
  }
}
