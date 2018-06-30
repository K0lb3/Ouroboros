namespace SRPG
{
    using GR;
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;

    [Pin(2, "Failure", 1, 2), Pin(3, "Interval", 1, 3), Pin(4, "スタンプ送信", 0, 4), Pin(0, "メッセージ送信", 0, 0), NodeType("System/SendChatMessage", 0x7fe5), Pin(1, "Success", 1, 1)]
    public class FlowNode_SendChatMessage : FlowNode_Network
    {
        private int mChannel;
        private string mRoomToken;
        private string mMessage;
        private int mStampId;
        private ChatWindow.eChatType mTargetChatType;

        public FlowNode_SendChatMessage()
        {
            this.mStampId = -1;
            base..ctor();
            return;
        }

        private void Failure()
        {
            base.set_enabled(0);
            this.mChannel = 0;
            this.mMessage = string.Empty;
            base.ActivateOutputLinks(2);
            return;
        }

        private string[] GetRoomMemberUIDs(bool is_ignore_self)
        {
            List<string> list;
            int num;
            if (ChatWindow.room_member_manager != null)
            {
                goto Label_000C;
            }
            return null;
        Label_000C:
            list = new List<string>();
            num = 0;
            goto Label_008C;
        Label_0019:
            if (is_ignore_self == null)
            {
                goto Label_004D;
            }
            if ((MonoSingleton<GameManager>.Instance.DeviceId == ChatWindow.room_member_manager.RoomMembers[num].UID) == null)
            {
                goto Label_004D;
            }
            goto Label_0088;
        Label_004D:
            if (list.Contains(ChatWindow.room_member_manager.RoomMembers[num].UID) != null)
            {
                goto Label_0088;
            }
            list.Add(ChatWindow.room_member_manager.RoomMembers[num].UID);
        Label_0088:
            num += 1;
        Label_008C:
            if (num < ChatWindow.room_member_manager.RoomMembers.Count)
            {
                goto Label_0019;
            }
            return list.ToArray();
        }

        private void Interval()
        {
            base.set_enabled(0);
            base.ActivateOutputLinks(3);
            return;
        }

        public override void OnActivate(int pinID)
        {
            if (pinID != null)
            {
                goto Label_0011;
            }
            this.ReqestSendMessage();
            goto Label_001E;
        Label_0011:
            if (pinID != 4)
            {
                goto Label_001E;
            }
            this.ReqestSendStamp();
        Label_001E:
            return;
        }

        public override unsafe void OnSuccess(WWWResult www)
        {
            WebAPI.JSON_BodyResponse<JSON_ChatSendRes> response;
            ChatSendRes res;
            Network.EErrCode code;
            if (Network.IsError == null)
            {
                goto Label_0057;
            }
            if (Network.ErrCode == 0x2136)
            {
                goto Label_0020;
            }
            goto Label_0046;
        Label_0020:
            Network.IsIndicator = 1;
            FlowNode_Variable.Set("MESSAGE_CAUTION_SEND_MESSAGE", Network.ErrMsg);
            Network.RemoveAPI();
            Network.ResetError();
            this.Interval();
            return;
        Label_0046:
            Network.IsIndicator = 1;
            Network.RemoveAPI();
            Network.ResetError();
            return;
        Label_0057:
            FlowNode_Variable.Set("MESSAGE_CAUTION_SEND_MESSAGE", string.Empty);
            response = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<JSON_ChatSendRes>>(&www.text);
            DebugUtility.Assert((response == null) == 0, "res == null");
            Network.RemoveAPI();
            Network.IsIndicator = 1;
            if (response.body == null)
            {
                goto Label_00BE;
            }
            res = new ChatSendRes();
            res.Deserialize(response.body);
            if (res.IsSuccess == null)
            {
                goto Label_00BE;
            }
            this.Success();
            return;
        Label_00BE:
            this.Failure();
            return;
        }

        public void ReqestSendMessage()
        {
            ChatWindow.eChatType type;
            type = this.mTargetChatType;
            if (type == 1)
            {
                goto Label_001A;
            }
            if (type == 2)
            {
                goto Label_0031;
            }
            goto Label_0048;
        Label_001A:
            this.RequestSendMessageToWorld(new Network.ResponseCallback(this.ResponseCallback));
            goto Label_0048;
        Label_0031:
            this.RequestSendMessageToRoom(new Network.ResponseCallback(this.ResponseCallback));
        Label_0048:
            return;
        }

        public void ReqestSendStamp()
        {
            ChatWindow.eChatType type;
            type = this.mTargetChatType;
            if (type == 1)
            {
                goto Label_001A;
            }
            if (type == 2)
            {
                goto Label_0031;
            }
            goto Label_0048;
        Label_001A:
            this.RequestSendStampToWorld(new Network.ResponseCallback(this.ResponseCallback));
            goto Label_0048;
        Label_0031:
            this.RequestSendStampToRoom(new Network.ResponseCallback(this.ResponseCallback));
        Label_0048:
            return;
        }

        private void RequestSendMessageToRoom(Network.ResponseCallback callback)
        {
            string[] strArray;
            if (string.IsNullOrEmpty(this.mMessage) != null)
            {
                goto Label_001C;
            }
            if (this.mChannel >= 0)
            {
                goto Label_001D;
            }
        Label_001C:
            return;
        Label_001D:
            strArray = this.GetRoomMemberUIDs(1);
            if (strArray == null)
            {
                goto Label_0056;
            }
            base.set_enabled(1);
            Network.IsIndicator = 0;
            base.ExecRequest(new ReqSendChatMessageRoom(this.mRoomToken, WebAPI.EscapeString(this.mMessage), strArray, callback));
        Label_0056:
            return;
        }

        private void RequestSendMessageToWorld(Network.ResponseCallback callback)
        {
            if (string.IsNullOrEmpty(this.mMessage) != null)
            {
                goto Label_001C;
            }
            if (this.mChannel >= 0)
            {
                goto Label_001D;
            }
        Label_001C:
            return;
        Label_001D:
            base.set_enabled(1);
            Network.IsIndicator = 0;
            base.ExecRequest(new ReqSendChatMessageWorld(this.mChannel, WebAPI.EscapeString(this.mMessage), callback));
            return;
        }

        private void RequestSendStampToRoom(Network.ResponseCallback callback)
        {
            string[] strArray;
            if (this.mStampId >= 0)
            {
                goto Label_000D;
            }
            return;
        Label_000D:
            strArray = this.GetRoomMemberUIDs(1);
            if (strArray == null)
            {
                goto Label_0041;
            }
            base.set_enabled(1);
            Network.IsIndicator = 0;
            base.ExecRequest(new ReqSendChatStampRoom(this.mRoomToken, this.mStampId, strArray, callback));
        Label_0041:
            return;
        }

        private void RequestSendStampToWorld(Network.ResponseCallback callback)
        {
            if (this.mStampId >= 0)
            {
                goto Label_000D;
            }
            return;
        Label_000D:
            base.set_enabled(1);
            Network.IsIndicator = 0;
            base.ExecRequest(new ReqSendChatStampWorld(this.mChannel, this.mStampId, callback));
            return;
        }

        public void ResetParam()
        {
            this.mChannel = 0;
            this.mRoomToken = null;
            this.mMessage = null;
            this.mStampId = -1;
            this.mTargetChatType = 0;
            return;
        }

        public void SetMessageData(int channle, string message)
        {
            this.ResetParam();
            this.mChannel = channle;
            this.mMessage = message;
            this.mTargetChatType = 1;
            return;
        }

        public void SetMessageData(string room_token, string message)
        {
            this.ResetParam();
            this.mRoomToken = room_token;
            this.mMessage = message;
            this.mTargetChatType = 2;
            return;
        }

        public void SetStampData(int channle, int stamp_id)
        {
            this.ResetParam();
            this.mChannel = channle;
            this.mStampId = stamp_id;
            this.mTargetChatType = 1;
            return;
        }

        public void SetStampData(string room_token, int stamp_id)
        {
            this.ResetParam();
            this.mRoomToken = room_token;
            this.mStampId = stamp_id;
            this.mTargetChatType = 2;
            return;
        }

        private void Success()
        {
            base.set_enabled(0);
            this.mChannel = 0;
            this.mMessage = string.Empty;
            base.ActivateOutputLinks(1);
            return;
        }
    }
}

