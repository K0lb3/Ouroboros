// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_FacebookInvite
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using Facebook.Unity;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(2, "FacebookNotConnected", FlowNode.PinTypes.Output, 2)]
  [FlowNode.Pin(0, "Request", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(1, "Success", FlowNode.PinTypes.Output, 1)]
  [FlowNode.NodeType("System/FacebookInvite", 32741)]
  public class FlowNode_FacebookInvite : FlowNode
  {
    private string lastResponse = string.Empty;
    private string ApiQuery = string.Empty;
    private string applink_url = "https://fb.me/1669343083078137";
    private string image_url = "http://prod-dlc-alcww-gumi-sg.akamaized.net/social/friend_invite_image.png";

    public override void OnActivate(int pinID)
    {
      if (pinID != 0)
        return;
      if (AccessToken.get_CurrentAccessToken() != null)
      {
        this.InviteFriend();
      }
      else
      {
        // ISSUE: method pointer
        FB.LogInWithReadPermissions((IEnumerable<string>) new List<string>()
        {
          "public_profile",
          "email",
          "user_friends"
        }, new FacebookDelegate<ILoginResult>((object) this, __methodptr(HandleResult)));
      }
    }

    private void InviteFriend()
    {
      Uri uri1 = new Uri(this.applink_url);
      Uri uri2 = new Uri(this.image_url);
      // ISSUE: reference to a compiler-generated field
      if (FlowNode_FacebookInvite.\u003C\u003Ef__am\u0024cache4 == null)
      {
        // ISSUE: reference to a compiler-generated field
        // ISSUE: method pointer
        FlowNode_FacebookInvite.\u003C\u003Ef__am\u0024cache4 = new FacebookDelegate<IAppInviteResult>((object) null, __methodptr(\u003CInviteFriend\u003Em__14C));
      }
      // ISSUE: reference to a compiler-generated field
      FacebookDelegate<IAppInviteResult> fAmCache4 = FlowNode_FacebookInvite.\u003C\u003Ef__am\u0024cache4;
      FB.Mobile.AppInvite(uri1, uri2, fAmCache4);
      this.ActivateOutputLinks(1);
    }

    protected void HandleResult(IResult result)
    {
      if (result == null)
      {
        Debug.Log((object) "FlowNode_FacebookShare | Null Response.");
        this.ActivateOutputLinks(2);
      }
      else if (!string.IsNullOrEmpty(result.get_Error()))
      {
        Debug.Log((object) ("FlowNode_FacebookShare | Error: " + result.get_Error()));
        this.ActivateOutputLinks(2);
      }
      else if (result.get_Cancelled())
      {
        Debug.Log((object) ("FlowNode_FacebookShare | Error: " + result.get_RawResult()));
        this.ActivateOutputLinks(2);
      }
      else if (!string.IsNullOrEmpty(result.get_RawResult()))
      {
        Debug.Log((object) ("FlowNode_FacebookShare | Success: " + result.get_RawResult()));
        this.InviteFriend();
      }
      else
      {
        Debug.Log((object) "FlowNode_FacebookShare | Empty Response.");
        this.ActivateOutputLinks(2);
      }
    }
  }
}
