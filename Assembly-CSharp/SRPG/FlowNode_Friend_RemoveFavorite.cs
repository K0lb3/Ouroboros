// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_Friend_RemoveFavorite
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using System;
using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(0, "Request", FlowNode.PinTypes.Input, 0)]
  [FlowNode.NodeType("System/Friend/RemoveFavorite", 32741)]
  [FlowNode.Pin(1, "Success", FlowNode.PinTypes.Output, 1)]
  public class FlowNode_Friend_RemoveFavorite : FlowNode_Network
  {
    private string mTargetFuid;

    public override void OnActivate(int pinID)
    {
      if (pinID != 0)
        return;
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      FlowNode_Friend_RemoveFavorite.\u003COnActivate\u003Ec__AnonStorey21A activateCAnonStorey21A = new FlowNode_Friend_RemoveFavorite.\u003COnActivate\u003Ec__AnonStorey21A();
      if (Network.Mode == Network.EConnectMode.Offline)
      {
        this.Success();
      }
      else
      {
        // ISSUE: reference to a compiler-generated field
        activateCAnonStorey21A.fuid = (string) null;
        if (!string.IsNullOrEmpty(GlobalVars.SelectedFriendID))
        {
          // ISSUE: reference to a compiler-generated field
          activateCAnonStorey21A.fuid = GlobalVars.SelectedFriendID;
        }
        else if (GlobalVars.FoundFriend != null && !string.IsNullOrEmpty(GlobalVars.FoundFriend.FUID))
        {
          // ISSUE: reference to a compiler-generated field
          activateCAnonStorey21A.fuid = GlobalVars.FoundFriend.FUID;
        }
        // ISSUE: reference to a compiler-generated field
        if (activateCAnonStorey21A.fuid == null)
        {
          this.Success();
        }
        else
        {
          // ISSUE: reference to a compiler-generated method
          FriendData friendData = MonoSingleton<GameManager>.Instance.Player.Friends.Find(new Predicate<FriendData>(activateCAnonStorey21A.\u003C\u003Em__217));
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
          this.mTargetFuid = activateCAnonStorey21A.fuid;
          // ISSUE: reference to a compiler-generated field
          this.ExecRequest((WebAPI) new ReqFriendFavoriteRemove(activateCAnonStorey21A.fuid, new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
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
