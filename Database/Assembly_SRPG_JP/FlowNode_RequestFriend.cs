// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_RequestFriend
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(3, "相手のフレンドリスト上限", FlowNode.PinTypes.Output, 3)]
  [FlowNode.NodeType("System/RequestFriend", 32741)]
  [FlowNode.Pin(0, "Request", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(1, "Success", FlowNode.PinTypes.Output, 1)]
  [FlowNode.Pin(2, "自分のフレンドリスト上限", FlowNode.PinTypes.Output, 2)]
  [FlowNode.Pin(4, "存在しないプレイヤーを指定した", FlowNode.PinTypes.Output, 4)]
  [FlowNode.Pin(5, "一括申請Request", FlowNode.PinTypes.Input, 5)]
  [FlowNode.Pin(6, "すでにフレンド", FlowNode.PinTypes.Output, 6)]
  [FlowNode.Pin(7, "すでに申請を出している", FlowNode.PinTypes.Output, 7)]
  [FlowNode.Pin(10, "Request無し", FlowNode.PinTypes.Output, 10)]
  [FlowNode.Pin(11, "Success(ブロックリスト申請のみ)", FlowNode.PinTypes.Output, 11)]
  [FlowNode.Pin(12, "Success(フレンド申請&ブロックリスト申請)", FlowNode.PinTypes.Output, 12)]
  [FlowNode.Pin(13, "Failed(フレンドorブロック)", FlowNode.PinTypes.Output, 13)]
  public class FlowNode_RequestFriend : FlowNode_Network
  {
    private const int PIN_IN_REQUEST = 0;
    private const int PIN_OT_SUCCESS_FRIEND_ADD = 1;
    private const int PIN_OT_ERR_REQ_FRIEND_REQUEST_MAX = 2;
    private const int PIN_OT_ERR_REQ_FRIEND_IS_FULL = 3;
    private const int PIN_OT_ERR_NOT_PLAYER = 4;
    private const int PIN_IN_REQUEST_ALL = 5;
    private const int PIN_OT_ERR_REQ_FRIEND_REGISTERED = 6;
    private const int PIN_OT_ERR_REQ_FRIEND_REQUESTING = 7;
    private const int PIN_OT_SUCCESS_NOT_REQUEST = 10;
    private const int PIN_OT_SUCCESS_BLOCKLIST_ADD = 11;
    private const int PIN_OT_SUCCESS_FRIEND_BLOCK_ADD = 12;
    private const int PIN_OT_FAILED_FRIEND_BLOCK_ADD = 13;
    private int apiType;

    public override void OnActivate(int pinID)
    {
      if (((Behaviour) this).get_enabled())
        return;
      List<string> friendApplyList = new List<string>();
      List<string> stringList = new List<string>();
      List<FriendData> friends = MonoSingleton<GameManager>.Instance.Player.Friends;
      if (pinID == 0)
      {
        if (!string.IsNullOrEmpty(GlobalVars.SelectedFriendID))
          friendApplyList.Add(GlobalVars.SelectedFriendID);
        else if (GlobalVars.FoundFriend != null && !string.IsNullOrEmpty(GlobalVars.FoundFriend.FUID))
          friendApplyList.Add(GlobalVars.FoundFriend.FUID);
      }
      else
      {
        FriendWindowItem[] componentsInChildren = (FriendWindowItem[]) ((Component) this).GetComponentsInChildren<FriendWindowItem>();
        if (componentsInChildren != null && componentsInChildren.Length > 0)
        {
          for (int index = 0; index < componentsInChildren.Length; ++index)
          {
            FriendWindowItem friendWindowItem = componentsInChildren[index];
            if (friendWindowItem.IsOn)
              friendApplyList.Add(friendWindowItem.Support == null ? friendWindowItem.PlayerParam.FUID : friendWindowItem.Support.FUID);
            else if (friendWindowItem.IsBlockOn)
              stringList.Add(friendWindowItem.Support == null ? friendWindowItem.PlayerParam.FUID : friendWindowItem.Support.FUID);
          }
        }
      }
      if (friendApplyList.Count > 0 || stringList.Count > 0)
      {
        for (int i = 0; i < friendApplyList.Count; ++i)
        {
          FriendData friendData = friends.Find((Predicate<FriendData>) (f => f.FUID == friendApplyList[i]));
          if (friendData != null && friendData.State == FriendStates.Friend)
          {
            UIUtility.SystemMessage((string) null, LocalizedText.Get("sys.FRIEND_ALREADY_FRIEND"), (UIUtility.DialogResultEvent) (go => this.ActivateOutputLinks(6)), (GameObject) null, false, -1);
            return;
          }
          this.apiType |= 2;
        }
        if (stringList != null && stringList.Count > 0)
          this.apiType |= 4;
        this.ExecRequest((WebAPI) new ReqFriendBlockApply(friendApplyList.ToArray(), stringList.ToArray(), new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
        ((Behaviour) this).set_enabled(true);
      }
      else
      {
        ((Behaviour) this).set_enabled(false);
        this.ActivateOutputLinks(10);
      }
    }

    private void Success()
    {
      ((Behaviour) this).set_enabled(false);
      int pinID = 1;
      if ((this.apiType & 4) != 0)
        pinID = (this.apiType & 2) == 0 ? 11 : 12;
      this.ActivateOutputLinks(pinID);
    }

    private void Failure()
    {
      ((Behaviour) this).set_enabled(false);
      this.ActivateOutputLinks(13);
    }

    public override void OnSuccess(WWWResult www)
    {
      WebAPI.JSON_BodyResponse<Json_RequestFriendResponse> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json_RequestFriendResponse>>(www.text);
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
        if (jsonObject.body.errors != null)
        {
          StringBuilder stringBuilder = GameUtility.GetStringBuilder();
          if (jsonObject.body.errors.friends != null && jsonObject.body.errors.friends.Length > 0)
            stringBuilder.Append(LocalizedText.Get("sys.FRIEND_REQ_ERROR_MESSAGE"));
          if (jsonObject.body.errors.blocks != null && jsonObject.body.errors.blocks.Length > 0)
            stringBuilder.Append(LocalizedText.Get("sys.BLOCK_REQ_ERROR_MESSAGE"));
          UIUtility.SystemMessage(stringBuilder.ToString(), (UIUtility.DialogResultEvent) (go => this.Failure()), (GameObject) null, false, -1);
        }
        else
          this.Success();
      }
    }

    private enum APIType
    {
      None,
      Friend,
      Block,
    }
  }
}
