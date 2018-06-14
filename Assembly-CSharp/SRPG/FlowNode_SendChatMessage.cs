// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_SendChatMessage
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using System.Collections.Generic;
using UnityEngine;

namespace SRPG
{
  [FlowNode.NodeType("System/SendChatMessage", 32741)]
  [FlowNode.Pin(0, "メッセージ送信", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(1, "Success", FlowNode.PinTypes.Output, 1)]
  [FlowNode.Pin(2, "Failure", FlowNode.PinTypes.Output, 2)]
  [FlowNode.Pin(3, "Interval", FlowNode.PinTypes.Output, 3)]
  [FlowNode.Pin(4, "スタンプ送信", FlowNode.PinTypes.Input, 4)]
  public class FlowNode_SendChatMessage : FlowNode_Network
  {
    private int mStampId = -1;
    private int mChannel;
    private string mRoomToken;
    private string mMessage;
    private ChatWindow.eChatType mTargetChatType;

    public override void OnActivate(int pinID)
    {
      if (pinID == 0)
      {
        this.ReqestSendMessage();
      }
      else
      {
        if (pinID != 4)
          return;
        this.ReqestSendStamp();
      }
    }

    public void SetMessageData(int channle, string message)
    {
      this.ResetParam();
      this.mChannel = channle;
      this.mMessage = message;
      this.mTargetChatType = ChatWindow.eChatType.World;
    }

    public void SetMessageData(string room_token, string message)
    {
      this.ResetParam();
      this.mRoomToken = room_token;
      this.mMessage = message;
      this.mTargetChatType = ChatWindow.eChatType.Room;
    }

    public void SetStampData(int channle, int stamp_id)
    {
      this.ResetParam();
      this.mChannel = channle;
      this.mStampId = stamp_id;
      this.mTargetChatType = ChatWindow.eChatType.World;
    }

    public void SetStampData(string room_token, int stamp_id)
    {
      this.ResetParam();
      this.mRoomToken = room_token;
      this.mStampId = stamp_id;
      this.mTargetChatType = ChatWindow.eChatType.Room;
    }

    public void ResetParam()
    {
      this.mChannel = 0;
      this.mRoomToken = (string) null;
      this.mMessage = (string) null;
      this.mStampId = -1;
      this.mTargetChatType = ChatWindow.eChatType.None;
    }

    public void ReqestSendMessage()
    {
      switch (this.mTargetChatType)
      {
        case ChatWindow.eChatType.World:
          this.RequestSendMessageToWorld(new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback));
          break;
        case ChatWindow.eChatType.Room:
          this.RequestSendMessageToRoom(new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback));
          break;
      }
    }

    public void ReqestSendStamp()
    {
      switch (this.mTargetChatType)
      {
        case ChatWindow.eChatType.World:
          this.RequestSendStampToWorld(new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback));
          break;
        case ChatWindow.eChatType.Room:
          this.RequestSendStampToRoom(new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback));
          break;
      }
    }

    private void Success()
    {
      ((Behaviour) this).set_enabled(false);
      this.mChannel = 0;
      this.mMessage = string.Empty;
      this.ActivateOutputLinks(1);
    }

    private void Failure()
    {
      ((Behaviour) this).set_enabled(false);
      this.mChannel = 0;
      this.mMessage = string.Empty;
      this.ActivateOutputLinks(2);
    }

    private void Interval()
    {
      ((Behaviour) this).set_enabled(false);
      this.ActivateOutputLinks(3);
    }

    public override void OnSuccess(WWWResult www)
    {
      if (Network.IsError)
      {
        if (Network.ErrCode == Network.EErrCode.SendChatInterval)
        {
          Network.IsIndicator = true;
          FlowNode_Variable.Set("MESSAGE_CAUTION_SEND_MESSAGE", Network.ErrMsg);
          Network.RemoveAPI();
          Network.ResetError();
          this.Interval();
        }
        else
        {
          Network.IsIndicator = true;
          Network.RemoveAPI();
          Network.ResetError();
        }
      }
      else
      {
        FlowNode_Variable.Set("MESSAGE_CAUTION_SEND_MESSAGE", string.Empty);
        WebAPI.JSON_BodyResponse<JSON_ChatSendRes> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<JSON_ChatSendRes>>(www.text);
        DebugUtility.Assert(jsonObject != null, "res == null");
        Network.RemoveAPI();
        Network.IsIndicator = true;
        if (jsonObject.body != null)
        {
          ChatSendRes chatSendRes = new ChatSendRes();
          chatSendRes.Deserialize(jsonObject.body);
          if (chatSendRes.IsSuccess)
          {
            this.Success();
            return;
          }
        }
        this.Failure();
      }
    }

    private void RequestSendMessageToWorld(Network.ResponseCallback callback)
    {
      if (string.IsNullOrEmpty(this.mMessage) || this.mChannel < 0)
        return;
      ((Behaviour) this).set_enabled(true);
      Network.IsIndicator = false;
      this.ExecRequest((WebAPI) new ReqSendChatMessageWorld(this.mChannel, WebAPI.EscapeString(this.mMessage), callback));
    }

    private void RequestSendStampToWorld(Network.ResponseCallback callback)
    {
      if (this.mStampId < 0)
        return;
      ((Behaviour) this).set_enabled(true);
      Network.IsIndicator = false;
      this.ExecRequest((WebAPI) new ReqSendChatStampWorld(this.mChannel, this.mStampId, callback));
    }

    private void RequestSendMessageToRoom(Network.ResponseCallback callback)
    {
      if (string.IsNullOrEmpty(this.mMessage) || this.mChannel < 0)
        return;
      string[] roomMemberUiDs = this.GetRoomMemberUIDs(true);
      if (roomMemberUiDs == null)
        return;
      ((Behaviour) this).set_enabled(true);
      Network.IsIndicator = false;
      this.ExecRequest((WebAPI) new ReqSendChatMessageRoom(this.mRoomToken, WebAPI.EscapeString(this.mMessage), roomMemberUiDs, callback));
    }

    private void RequestSendStampToRoom(Network.ResponseCallback callback)
    {
      if (this.mStampId < 0)
        return;
      string[] roomMemberUiDs = this.GetRoomMemberUIDs(true);
      if (roomMemberUiDs == null)
        return;
      ((Behaviour) this).set_enabled(true);
      Network.IsIndicator = false;
      this.ExecRequest((WebAPI) new ReqSendChatStampRoom(this.mRoomToken, this.mStampId, roomMemberUiDs, callback));
    }

    private string[] GetRoomMemberUIDs(bool is_ignore_self = false)
    {
      if (ChatWindow.room_member_manager == null)
        return (string[]) null;
      List<string> stringList = new List<string>();
      for (int index = 0; index < ChatWindow.room_member_manager.RoomMembers.Count; ++index)
      {
        if ((!is_ignore_self || !(MonoSingleton<GameManager>.Instance.DeviceId == ChatWindow.room_member_manager.RoomMembers[index].UID)) && !stringList.Contains(ChatWindow.room_member_manager.RoomMembers[index].UID))
          stringList.Add(ChatWindow.room_member_manager.RoomMembers[index].UID);
      }
      return stringList.ToArray();
    }
  }
}
