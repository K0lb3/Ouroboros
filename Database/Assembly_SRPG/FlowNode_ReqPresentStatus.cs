namespace SRPG
{
    using GR;
    using System;
    using UnityEngine;

    [Pin(110, "送付ステータス取得完了", 1, 110), Pin(100, "送付ステータス取得", 0, 100), NodeType("System/WebApi/PresentStatus", 0x7fe5), Pin(120, "送付ステータス取得失敗", 1, 120)]
    public class FlowNode_ReqPresentStatus : FlowNode_Network
    {
        private ApiBase m_Api;

        public FlowNode_ReqPresentStatus()
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
                goto Label_002A;
            }
            this.m_Api = new Api_PresentListStatus(this);
        Label_002A:
            if (this.m_Api == null)
            {
                goto Label_0047;
            }
            this.m_Api.Start();
            base.set_enabled(1);
        Label_0047:
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

        public class Api_PresentListStatus : FlowNode_ReqPresentStatus.ApiBase
        {
            public Api_PresentListStatus(FlowNode_ReqPresentStatus node)
            {
                base..ctor(node);
                return;
            }

            public override unsafe void Complete(WWWResult www)
            {
                WebAPI.JSON_BodyResponse<Json> response;
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
                    goto Label_00FA;
                }
                if (string.IsNullOrEmpty(response.body.result) == null)
                {
                    goto Label_0080;
                }
                FriendPresentRootWindow.SetSendStatus(1);
                goto Label_00F5;
            Label_0080:
                if ((response.body.result == "0") == null)
                {
                    goto Label_00A5;
                }
                FriendPresentRootWindow.SetSendStatus(2);
                goto Label_00F5;
            Label_00A5:
                if ((response.body.result == "1") == null)
                {
                    goto Label_00CA;
                }
                FriendPresentRootWindow.SetSendStatus(4);
                goto Label_00F5;
            Label_00CA:
                if ((response.body.result == "9") == null)
                {
                    goto Label_00EF;
                }
                FriendPresentRootWindow.SetSendStatus(3);
                goto Label_00F5;
            Label_00EF:
                FriendPresentRootWindow.SetSendStatus(0);
            Label_00F5:
                goto Label_0100;
            Label_00FA:
                FriendPresentRootWindow.SetSendStatus(0);
            Label_0100:
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
                    return "present";
                }
            }

            [Serializable]
            public class Json
            {
                public string result;

                public Json()
                {
                    base..ctor();
                    return;
                }
            }
        }

        public class ApiBase
        {
            protected FlowNode_ReqPresentStatus m_Node;
            protected RequestAPI m_Request;

            public ApiBase(FlowNode_ReqPresentStatus node)
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
                    goto Label_005B;
                }
                if (MonoSingleton<GameManager>.Instance.MasterParam.IsFriendPresentItemParamValid() == null)
                {
                    goto Label_0050;
                }
                this.m_Node.ExecRequest(new RequestAPI(this.url, new Network.ResponseCallback(this.m_Node.ResponseCallback), this.req));
                goto Label_0056;
            Label_0050:
                this.Success();
            Label_0056:
                goto Label_0061;
            Label_005B:
                this.Failed();
            Label_0061:
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

