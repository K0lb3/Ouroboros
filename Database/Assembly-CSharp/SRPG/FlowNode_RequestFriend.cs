// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_RequestFriend
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace SRPG
{
  [FlowNode.NodeType("System/RequestFriend", 32741)]
  [FlowNode.Pin(5, "一括申請Request", FlowNode.PinTypes.Input, 5)]
  [FlowNode.Pin(4, "存在しないプレイヤーを指定した", FlowNode.PinTypes.Output, 4)]
  [FlowNode.Pin(3, "相手のフレンドリスト上限", FlowNode.PinTypes.Output, 3)]
  [FlowNode.Pin(2, "自分のフレンドリスト上限", FlowNode.PinTypes.Output, 2)]
  [FlowNode.Pin(1, "Success", FlowNode.PinTypes.Output, 1)]
  [FlowNode.Pin(0, "Request", FlowNode.PinTypes.Input, 0)]
  public class FlowNode_RequestFriend : FlowNode_Network
  {
    private List<FriendWindowItem> FriendItemList = new List<FriendWindowItem>();
    private bool is_all;
    private int index;

    public override void OnActivate(int pinID)
    {
      if (pinID != 0 && pinID != 5)
        return;
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      FlowNode_RequestFriend.\u003COnActivate\u003Ec__AnonStorey2D4 activateCAnonStorey2D4 = new FlowNode_RequestFriend.\u003COnActivate\u003Ec__AnonStorey2D4();
      if (pinID == 5)
        this.is_all = true;
      if (Network.Mode == Network.EConnectMode.Offline)
      {
        this.Success();
      }
      else
      {
        // ISSUE: reference to a compiler-generated field
        activateCAnonStorey2D4.fuid = (string) null;
        if (!string.IsNullOrEmpty(GlobalVars.SelectedFriendID))
        {
          // ISSUE: reference to a compiler-generated field
          activateCAnonStorey2D4.fuid = GlobalVars.SelectedFriendID;
        }
        else if (GlobalVars.FoundFriend != null && !string.IsNullOrEmpty(GlobalVars.FoundFriend.FUID))
        {
          // ISSUE: reference to a compiler-generated field
          activateCAnonStorey2D4.fuid = GlobalVars.FoundFriend.FUID;
        }
        if (pinID == 5)
        {
          this.index = 0;
          FriendWindowItem[] componentsInChildren = (FriendWindowItem[]) ((Component) this).GetComponentsInChildren<FriendWindowItem>();
          if (componentsInChildren == null)
          {
            this.Success();
            return;
          }
          this.FriendItemList.Clear();
          for (int index = 0; index < componentsInChildren.Length; ++index)
          {
            if (componentsInChildren[index].IsOn)
              this.FriendItemList.Add(componentsInChildren[index]);
          }
          if (this.FriendItemList.Count <= 0)
          {
            this.Success();
            return;
          }
        }
        if (this.is_all && this.FriendItemList != null && this.FriendItemList.Count > this.index)
        {
          FriendWindowItem friendItem = this.FriendItemList[this.index];
          // ISSUE: reference to a compiler-generated field
          activateCAnonStorey2D4.fuid = friendItem.PlayerParam.FUID;
        }
        // ISSUE: reference to a compiler-generated field
        if (activateCAnonStorey2D4.fuid == null)
        {
          this.Success();
        }
        else
        {
          // ISSUE: reference to a compiler-generated method
          FriendData friendData = MonoSingleton<GameManager>.Instance.Player.Friends.Find(new Predicate<FriendData>(activateCAnonStorey2D4.\u003C\u003Em__2C3));
          if (friendData != null)
          {
            string empty = string.Empty;
            if (friendData.State == FriendStates.Friend)
            {
              UIUtility.SystemMessage((string) null, LocalizedText.Get("sys.FRIEND_ALREADY_FRIEND"), (UIUtility.DialogResultEvent) (go => {}), (GameObject) null, false, -1);
              return;
            }
            if (friendData.State == FriendStates.Follow)
            {
              UIUtility.SystemMessage((string) null, LocalizedText.Get("sys.FRIEND_ALREADY_FOLLOW"), (UIUtility.DialogResultEvent) (go => {}), (GameObject) null, false, -1);
              return;
            }
          }
          // ISSUE: reference to a compiler-generated field
          this.ExecRequest((WebAPI) new ReqFriendReq(activateCAnonStorey2D4.fuid, new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
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
      WebAPI.JSON_BodyResponse<Json_PlayerDataAll> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json_PlayerDataAll>>(www.text);
      DebugUtility.Assert(jsonObject != null, "res == null");
      if (Network.IsError)
      {
        switch (Network.ErrCode)
        {
          case Network.EErrCode.ReqFriendRequestMax:
          case Network.EErrCode.ReqFriendIsFull:
          case Network.EErrCode.ReqFriendRegistered:
          case Network.EErrCode.ReqFriendRequesting:
            this.OnBack();
            break;
          default:
            this.OnRetry();
            break;
        }
      }
      else if (jsonObject.body == null)
      {
        this.OnRetry();
      }
      else
      {
        try
        {
          MonoSingleton<GameManager>.Instance.Deserialize(jsonObject.body.player);
        }
        catch (Exception ex)
        {
          this.OnRetry(ex);
          return;
        }
        MonoSingleton<GameManager>.GetInstanceDirect().RequestUpdateBadges(GameManager.BadgeTypes.DailyMission);
        Network.RemoveAPI();
        if (this.is_all)
        {
          ++this.index;
          if (this.index < this.FriendItemList.Count)
          {
            this.OnActivate(0);
            return;
          }
        }
        this.is_all = false;
        this.Success();
      }
    }
  }
}
