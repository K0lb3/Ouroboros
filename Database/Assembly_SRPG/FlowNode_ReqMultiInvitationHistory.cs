namespace SRPG
{
    using System;
    using System.Text;
    using UnityEngine;

    [Pin(100, "マルチ招待ログ一覧", 0, 100), Pin(120, "マルチ招待ログ一覧取得失敗", 1, 120), NodeType("System/WebApi/MultiInvitationHistory", 0x7fe5), Pin(110, "マルチ招待ログ一覧取得完了", 1, 110)]
    public class FlowNode_ReqMultiInvitationHistory : FlowNode_Network
    {
        public const int INPUT_MULTIINVITATION = 100;
        public const int OUTPUT_MULTIINVITATION_SUCCESS = 110;
        public const int OUTPUT_MULTIINVITATION_FAILED = 120;
        private ApiBase m_Api;

        public FlowNode_ReqMultiInvitationHistory()
        {
            base..ctor();
            return;
        }

        public override void OnActivate(int pinID)
        {
            MultiInvitationReceiveWindow window;
            int num;
            if (this.m_Api == null)
            {
                goto Label_0016;
            }
            DebugUtility.LogError("同時に複数の通信が入ると駄目！");
            return;
        Label_0016:
            if (pinID != 100)
            {
                goto Label_0069;
            }
            window = MultiInvitationReceiveWindow.instance;
            if (window == null)
            {
                goto Label_0069;
            }
            num = 1;
            if (window.GetLogPage() == num)
            {
                goto Label_004A;
            }
            this.m_Api = new Api_MultiInvitationHistory(this, num);
            goto Label_0069;
        Label_004A:
            this.m_Api = new Api_MultiInvitationHistory(this, num);
            this.m_Api.Success();
            this.m_Api = null;
        Label_0069:
            if (this.m_Api == null)
            {
                goto Label_0086;
            }
            this.m_Api.Start();
            base.set_enabled(1);
        Label_0086:
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

        public class Api_MultiInvitationHistory : FlowNode_ReqMultiInvitationHistory.ApiBase
        {
            private int m_Page;

            public Api_MultiInvitationHistory(FlowNode_ReqMultiInvitationHistory node, int page)
            {
                this.m_Page = 1;
                base..ctor(node);
                this.m_Page = page;
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
                response = JsonUtility.FromJson<WebAPI.JSON_BodyResponse<Json>>(&www.text);
                DebugUtility.Assert((response == null) == 0, "res == null");
                if (response.body == null)
                {
                    goto Label_007F;
                }
                window = MultiInvitationReceiveWindow.instance;
                if (window == null)
                {
                    goto Label_007F;
                }
                window.DeserializeLogList(this.m_Page, response.body);
            Label_007F:
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
                    return "btl/room/invitation/history";
                }
            }

            public override string req
            {
                get
                {
                    StringBuilder builder;
                    builder = new StringBuilder(0x80);
                    builder.Append("\"page\":" + ((int) this.m_Page));
                    builder.Append(",\"id\":0");
                    return builder.ToString();
                }
            }

            [Serializable]
            public class Json
            {
                public FlowNode_ReqMultiInvitationHistory.Api_MultiInvitationHistory.JsonList[] list;

                public Json()
                {
                    base..ctor();
                    return;
                }
            }

            [Serializable]
            public class JsonList
            {
                public int id;
                public int roomid;
                public string iname;
                public string btype;
                public string created_at;
                public FlowNode_ReqMultiInvitationHistory.Api_MultiInvitationHistory.JsonPlayer player;

                public JsonList()
                {
                    base..ctor();
                    return;
                }
            }

            [Serializable]
            public class JsonPlayer
            {
                public string uid;
                public string fuid;
                public string name;
                public int lv;
                public string lastlogin;
                public Json_Unit unit;

                public JsonPlayer()
                {
                    base..ctor();
                    return;
                }
            }
        }

        public class ApiBase
        {
            protected FlowNode_ReqMultiInvitationHistory m_Node;
            protected RequestAPI m_Request;

            public ApiBase(FlowNode_ReqMultiInvitationHistory node)
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

            public virtual void Start()
            {
                if (Network.Mode != null)
                {
                    goto Label_003C;
                }
                this.m_Node.ExecRequest(new RequestAPI(this.url, new Network.ResponseCallback(this.m_Node.ResponseCallback), this.req));
                goto Label_0042;
            Label_003C:
                this.Failed();
            Label_0042:
                return;
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

