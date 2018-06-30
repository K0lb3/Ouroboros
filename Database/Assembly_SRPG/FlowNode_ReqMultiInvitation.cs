namespace SRPG
{
    using GR;
    using System;
    using System.Text;
    using UnityEngine;

    [Pin(210, "マルチ招待完了", 1, 210), Pin(300, "マルチ招待通知取得", 0, 300), Pin(310, "マルチ招待通知完了", 1, 310), Pin(320, "マルチ招待通知失敗", 1, 320), NodeType("System/WebApi/MultiInvitation", 0x7fe5), Pin(100, "マルチ招待一覧", 0, 100), Pin(110, "マルチ招待一覧取得完了", 1, 110), Pin(120, "マルチ招待一覧取得失敗", 1, 120), Pin(200, "マルチ招待", 0, 200), Pin(220, "マルチ招待失敗", 1, 220)]
    public class FlowNode_ReqMultiInvitation : FlowNode_Network
    {
        public const int INPUT_ROOMINVITATION = 100;
        public const int OUTPUT_ROOMINVITATION_SUCCESS = 110;
        public const int OUTPUT_ROOMINVITATION_FAILED = 120;
        public const int INPUT_ROOMINVITATIONSEND = 200;
        public const int OUTPUT_ROOMINVITATIONSEND_SUCCESS = 210;
        public const int OUTPUT_ROOMINVITATIONSEND_FAILED = 220;
        public const int INPUT_NOTIFYINVITATION = 300;
        public const int OUTPUT_NOTIFYINVITATION_SUCCESS = 310;
        public const int OUTPUT_NOTIFYINVITATION_FAILED = 320;
        private ApiBase m_Api;

        public FlowNode_ReqMultiInvitation()
        {
            base..ctor();
            return;
        }

        public override void OnActivate(int pinID)
        {
            if (this.m_Api == null)
            {
                goto Label_0016;
            }
            DebugUtility.LogError("同時に複数の通信が入ると駄目！");
            return;
        Label_0016:
            if (pinID != 100)
            {
                goto Label_002F;
            }
            this.m_Api = new Api_RoomInvitation(this);
            goto Label_0062;
        Label_002F:
            if (pinID != 200)
            {
                goto Label_004B;
            }
            this.m_Api = new Api_RoomInvitationSend(this);
            goto Label_0062;
        Label_004B:
            if (pinID != 300)
            {
                goto Label_0062;
            }
            this.m_Api = new Api_NotifyInvitation(this);
        Label_0062:
            if (this.m_Api == null)
            {
                goto Label_0090;
            }
            if (this.m_Api.Start() == null)
            {
                goto Label_0089;
            }
            base.set_enabled(1);
            goto Label_0090;
        Label_0089:
            this.m_Api = null;
        Label_0090:
            return;
        }

        public override void OnSuccess(WWWResult www)
        {
            if (this.m_Api == null)
            {
                goto Label_001E;
            }
            this.m_Api.Complete(www);
            this.m_Api = null;
        Label_001E:
            return;
        }

        public class Api_NotifyInvitation : FlowNode_ReqMultiInvitation.ApiBase
        {
            public Api_NotifyInvitation(FlowNode_ReqMultiInvitation node)
            {
                base..ctor(node);
                return;
            }

            public override unsafe void Complete(WWWResult www)
            {
                WebAPI.JSON_BodyResponse<Json> response;
                if (Network.IsError == null)
                {
                    goto Label_0017;
                }
                MultiInvitationBadge.isValid = 0;
                this.Failed();
                return;
            Label_0017:
                DebugMenu.Log("API", this.url + ":" + &www.text);
                response = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json>>(&www.text);
                DebugUtility.Assert((response == null) == 0, "res == null");
                if (response.body == null)
                {
                    goto Label_0094;
                }
                MultiInvitationReceiveWindow.SetBadge((response.body.player == null) ? 0 : ((response.body.player.multi_inv == 0) == 0));
                goto Label_009A;
            Label_0094:
                MultiInvitationReceiveWindow.SetBadge(0);
            Label_009A:
                Network.RemoveAPI();
                this.Success();
                return;
            }

            public override void Failed()
            {
                Network.RemoveAPI();
                Network.ResetError();
                if ((base.m_Node != null) == null)
                {
                    goto Label_0038;
                }
                base.m_Node.ActivateOutputLinks(320);
                base.m_Node.set_enabled(0);
            Label_0038:
                return;
            }

            public override bool Start()
            {
                bool flag;
                flag = 0;
                if ((MonoSingleton<GameManager>.Instance != null) == null)
                {
                    goto Label_0037;
                }
                if (MonoSingleton<GameManager>.Instance.Player == null)
                {
                    goto Label_0037;
                }
                if (MonoSingleton<GameManager>.Instance.Player.MultiInvitaionFlag != null)
                {
                    goto Label_0037;
                }
                flag = 1;
            Label_0037:
                if (flag == null)
                {
                    goto Label_004B;
                }
                MultiInvitationBadge.isValid = 0;
                this.Success();
                return 0;
            Label_004B:
                return base.Start();
            }

            public override void Success()
            {
                if ((base.m_Node != null) == null)
                {
                    goto Label_002E;
                }
                base.m_Node.ActivateOutputLinks(310);
                base.m_Node.set_enabled(0);
            Label_002E:
                return;
            }

            public override string url
            {
                get
                {
                    return "btl/multi/invitation";
                }
            }

            public override string req
            {
                get
                {
                    StringBuilder builder;
                    builder = new StringBuilder(0x40);
                    builder.Append("\"is_multi_push\":1");
                    return builder.ToString();
                }
            }

            [Serializable]
            public class Json
            {
                public FlowNode_ReqMultiInvitation.Api_NotifyInvitation.Player player;

                public Json()
                {
                    base..ctor();
                    return;
                }
            }

            [Serializable]
            public class Player
            {
                public int multi_inv;

                public Player()
                {
                    base..ctor();
                    return;
                }
            }
        }

        public class Api_RoomInvitation : FlowNode_ReqMultiInvitation.ApiBase
        {
            public Api_RoomInvitation(FlowNode_ReqMultiInvitation node)
            {
                base..ctor(node);
                return;
            }

            public override unsafe void Complete(WWWResult www)
            {
                WebAPI.JSON_BodyResponse<Json> response;
                MultiInvitationReceiveWindow window;
                if (Network.IsError == null)
                {
                    goto Label_0016;
                }
                base.m_Node.OnFailed();
                return;
            Label_0016:
                DebugMenu.Log("API", this.url + ":" + &www.text);
                response = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json>>(&www.text);
                DebugUtility.Assert((response == null) == 0, "res == null");
                if (response.body == null)
                {
                    goto Label_0079;
                }
                window = MultiInvitationReceiveWindow.instance;
                if (window == null)
                {
                    goto Label_0079;
                }
                window.DeserializeActiveList(response.body);
            Label_0079:
                Network.RemoveAPI();
                this.Success();
                return;
            }

            public override void Failed()
            {
                base.m_Node.ActivateOutputLinks(120);
                Network.RemoveAPI();
                Network.ResetError();
                base.m_Node.set_enabled(0);
                return;
            }

            public override void Success()
            {
                base.m_Node.ActivateOutputLinks(110);
                base.m_Node.set_enabled(0);
                return;
            }

            public override string url
            {
                get
                {
                    return "btl/room/invitation";
                }
            }

            [Serializable]
            public class Json
            {
                public FlowNode_ReqMultiInvitation.Api_RoomInvitation.JsonRoom[] rooms;

                public Json()
                {
                    base..ctor();
                    return;
                }
            }

            [Serializable]
            public class JsonRoom
            {
                public int roomid;
                public string comment;
                public int num;
                public FlowNode_ReqMultiInvitation.Api_RoomInvitation.JsonRoomOwner owner;
                public FlowNode_ReqMultiInvitation.Api_RoomInvitation.JsonRoomQuest quest;
                public string pwd_hash;
                public int unitlv;
                public int clear;
                public int limit;
                public string btype;

                public JsonRoom()
                {
                    base..ctor();
                    return;
                }
            }

            [Serializable]
            public class JsonRoomOwner
            {
                public string name;
                public string fuid;
                public Json_Unit[] units;

                public JsonRoomOwner()
                {
                    base..ctor();
                    return;
                }
            }

            [Serializable]
            public class JsonRoomQuest
            {
                public string iname;

                public JsonRoomQuest()
                {
                    base..ctor();
                    return;
                }
            }
        }

        public class Api_RoomInvitationSend : FlowNode_ReqMultiInvitation.ApiBase
        {
            private int m_RoomType;
            private int m_RoomId;
            private string[] m_Sends;

            public Api_RoomInvitationSend(FlowNode_ReqMultiInvitation node)
            {
                MultiInvitationSendWindow window;
                base..ctor(node);
                this.m_RoomType = (GlobalVars.SelectedMultiPlayRoomType != 2) ? 0 : 1;
                this.m_RoomId = GlobalVars.SelectedMultiPlayRoomID;
                window = MultiInvitationSendWindow.instance;
                if (window == null)
                {
                    goto Label_0042;
                }
                this.m_Sends = window.GetSendList();
            Label_0042:
                return;
            }

            public override unsafe void Complete(WWWResult www)
            {
                WebAPI.JSON_BodyResponse<Json> response;
                int num;
                if (Network.IsError == null)
                {
                    goto Label_0016;
                }
                base.m_Node.OnFailed();
                return;
            Label_0016:
                DebugMenu.Log("API", this.url + ":" + &www.text);
                DebugUtility.Assert((JsonUtility.FromJson<WebAPI.JSON_BodyResponse<Json>>(&www.text) == null) == 0, "res == null");
                if (this.m_Sends == null)
                {
                    goto Label_0086;
                }
                num = 0;
                goto Label_0078;
            Label_0067:
                MultiInvitationSendWindow.AddInvited(this.m_Sends[num]);
                num += 1;
            Label_0078:
                if (num < ((int) this.m_Sends.Length))
                {
                    goto Label_0067;
                }
            Label_0086:
                Network.RemoveAPI();
                this.Success();
                return;
            }

            public override void Failed()
            {
                base.m_Node.ActivateOutputLinks(220);
                Network.RemoveAPI();
                Network.ResetError();
                base.m_Node.set_enabled(0);
                return;
            }

            public override void Success()
            {
                base.m_Node.ActivateOutputLinks(210);
                base.m_Node.set_enabled(0);
                return;
            }

            public override string url
            {
                get
                {
                    return "btl/room/invitation/send";
                }
            }

            public override string req
            {
                get
                {
                    StringBuilder builder;
                    int num;
                    builder = new StringBuilder(0x80);
                    builder.Append("\"roomid\":" + ((int) this.m_RoomId));
                    builder.Append(",\"btype\":" + ((this.m_RoomType != null) ? "\"multi_tower\"" : "\"multi\""));
                    builder.Append(",\"send_uids\":[");
                    if (this.m_Sends == null)
                    {
                        goto Label_00B6;
                    }
                    num = 0;
                    goto Label_00A8;
                Label_0070:
                    builder.Append(((num <= 0) ? string.Empty : ",") + "\"" + this.m_Sends[num] + "\"");
                    num += 1;
                Label_00A8:
                    if (num < ((int) this.m_Sends.Length))
                    {
                        goto Label_0070;
                    }
                Label_00B6:
                    builder.Append("]");
                    return builder.ToString();
                }
            }

            [Serializable]
            public class Json
            {
                public bool result;

                public Json()
                {
                    base..ctor();
                    return;
                }
            }
        }

        public class ApiBase
        {
            protected FlowNode_ReqMultiInvitation m_Node;
            protected RequestAPI m_Request;

            public ApiBase(FlowNode_ReqMultiInvitation node)
            {
                base..ctor();
                this.m_Node = node;
                return;
            }

            public virtual void Complete(WWWResult www)
            {
            }

            public virtual void Failed()
            {
            }

            public virtual bool Start()
            {
                if (Network.Mode != null)
                {
                    goto Label_003C;
                }
                this.m_Node.ExecRequest(new RequestAPI(this.url, new Network.ResponseCallback(this.m_Node.ResponseCallback), this.req));
                goto Label_0044;
            Label_003C:
                this.Failed();
                return 0;
            Label_0044:
                return 1;
            }

            public virtual void Success()
            {
            }

            public virtual string url
            {
                get
                {
                    return string.Empty;
                }
            }

            public virtual string req
            {
                get
                {
                    return null;
                }
            }
        }
    }
}

