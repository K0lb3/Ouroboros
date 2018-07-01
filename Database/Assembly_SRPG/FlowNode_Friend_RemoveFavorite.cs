// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_Friend_RemoveFavorite
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using System;
using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(0, "Request", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(1, "Success", FlowNode.PinTypes.Output, 1)]
  [FlowNode.NodeType("System/Friend/RemoveFavorite", 32741)]
  public class FlowNode_Friend_RemoveFavorite : FlowNode_Network
  {
    private string mTargetFuid;

    public override void OnActivate(int pinID)
    {
      if (pinID != 0)
        return;
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      FlowNode_Friend_RemoveFavorite.\u003COnActivate\u003Ec__AnonStorey2D8 activateCAnonStorey2D8 = new FlowNode_Friend_RemoveFavorite.\u003COnActivate\u003Ec__AnonStorey2D8();
      if (Network.Mode == Network.EConnectMode.Offline)
      {
        this.Success();
      }
      else
      {
        // ISSUE: reference to a compiler-generated field
        activateCAnonStorey2D8.fuid = (string) null;
        if (!string.IsNullOrEmpty(GlobalVars.SelectedFriendID))
        {
          // ISSUE: reference to a compiler-generated field
          activateCAnonStorey2D8.fuid = GlobalVars.SelectedFriendID;
        }
        else if (GlobalVars.FoundFriend != null && !string.IsNullOrEmpty(GlobalVars.FoundFriend.FUID))
        {
          // ISSUE: reference to a compiler-generated field
          activateCAnonStorey2D8.fuid = GlobalVars.FoundFriend.FUID;
        }
        // ISSUE: reference to a compiler-generated field
        if (activateCAnonStorey2D8.fuid == null)
        {
          this.Success();
        }
        else
        {
          // ISSUE: reference to a compiler-generated method
          FriendData friendData = MonoSingleton<GameManager>.Instance.Player.Friends.Find(new Predicate<FriendData>(activateCAnonStorey2D8.\u003C\u003Em__2CD));
          if (friendData != null)
          {
            string empty = string.Empty;
            if (!friendData.IsFavorite)
            {
              UIUtility.SystemMessage((string) null, LocalizedText.Get("sys.FRIEND_NO_FAVORITE"), (UIUtility.DialogResultEvent) (go => {}), (GameObject) null, false, -1);
              return;
            }
          }
          // ISSUE: reference to a compiler-generated field
          this.mTargetFuid = activateCAnonStorey2D8.fuid;
          // ISSUE: reference to a compiler-generated field
          this.ExecRequest((WebAPI) new ReqFriendFavoriteRemove(activateCAnonStorey2D8.fuid, new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
          ((Behaviour) this).set_enabled(true);
        }
      }
    }

    private void Success()
    {
      ((Behaviour) this).set_enabled(false);
      this.ActivateOutputLinks(1);
    }

    public override void OnSuccess(WWWResult www)
    {
      WebAPI.JSON_BodyResponse<Json_FriendArray> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json_FriendArray>>(www.text);
      DebugUtility.Assert(jsonObject != null, "res == null");
      if (Network.IsError)
        this.OnRetry();
      else if (jsonObject.body == null)
      {
        this.OnRetry();
      }
      else
      {
        if (!string.IsNullOrEmpty(this.mTargetFuid))
        {
          try
          {
            MonoSingleton<GameManager>.Instance.Deserialize(jsonObject.body.friends, FriendStates.Friend);
            FriendData friendData = MonoSingleton<GameManager>.Instance.Player.Friends.Find((Predicate<FriendData>) (f => f.FUID == this.mTargetFuid));
            if (friendData != null)
              GlobalVars.SelectedFriend = friendData;
          }
          catch (Exception ex)
          {
            this.OnRetry(ex);
            return;
          }
        }
        Network.RemoveAPI();
        this.Success();
      }
    }
  }
}
