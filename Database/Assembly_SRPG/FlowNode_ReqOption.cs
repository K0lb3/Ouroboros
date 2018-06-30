namespace SRPG
{
    using GR;
    using System;
    using System.Text;

    [Pin(220, "オプション設定失敗", 1, 220), NodeType("System/WebApi/Option", 0x7fe5), Pin(200, "Set", 0, 200), Pin(210, "オプション設定完了", 1, 210)]
    public class FlowNode_ReqOption : FlowNode_Network
    {
        private ApiBase m_Api;

        public FlowNode_ReqOption()
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
            if (pinID != 200)
            {
                goto Label_002D;
            }
            this.m_Api = new Api_OptionSet(this);
        Label_002D:
            if (this.m_Api == null)
            {
                goto Label_005B;
            }
            if (this.m_Api.Start() == null)
            {
                goto Label_0054;
            }
            base.set_enabled(1);
            goto Label_005B;
        Label_0054:
            this.m_Api = null;
        Label_005B:
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

        public class Api_OptionSet : FlowNode_ReqOption.ApiBase
        {
            private bool m_Flag;
            private string m_Comment;

            public Api_OptionSet(FlowNode_ReqOption node)
            {
                base..ctor(node);
                this.m_Flag = GlobalVars.MultiInvitaionFlag;
                this.m_Comment = GlobalVars.MultiInvitaionComment;
                return;
            }

            public override unsafe void Complete(WWWResult www)
            {
                WebAPI.JSON_BodyResponse<Json> response;
                if (Network.IsError == null)
                {
                    goto Label_0011;
                }
                this.Failed();
                return;
            Label_0011:
                DebugMenu.Log("API", this.url + ":" + &www.text);
                response = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json>>(&www.text);
                DebugUtility.Assert((response == null) == 0, "res == null");
                if (response.body == null)
                {
                    goto Label_0075;
                }
                MonoSingleton<GameManager>.Instance.Player.Deserialize(response.body.player);
            Label_0075:
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

            public override bool Start()
            {
                PlayerData data;
                if ((MonoSingleton<GameManager>.Instance == null) != null)
                {
                    goto Label_001F;
                }
                if (MonoSingleton<GameManager>.Instance.Player != null)
                {
                    goto Label_0027;
                }
            Label_001F:
                this.Failed();
                return 0;
            Label_0027:
                data = MonoSingleton<GameManager>.Instance.Player;
                if (this.m_Flag != data.MultiInvitaionFlag)
                {
                    goto Label_0059;
                }
                if ((this.m_Comment != data.MultiInvitaionComment) == null)
                {
                    goto Label_0060;
                }
            Label_0059:
                return base.Start();
            Label_0060:
                this.Success();
                return 0;
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
                    return "setoption";
                }
            }

            public override string req
            {
                get
                {
                    StringBuilder builder;
                    builder = new StringBuilder(0x80);
                    builder.Append("\"is_multi_push\":" + ((this.m_Flag == null) ? "0" : "1"));
                    builder.Append(",\"multi_comment\":\"" + ((this.m_Comment != null) ? this.m_Comment : string.Empty) + "\"");
                    return builder.ToString();
                }
            }

            [Serializable]
            public class Json
            {
                public Json_PlayerData player;

                public Json()
                {
                    base..ctor();
                    return;
                }
            }
        }

        public class ApiBase
        {
            protected FlowNode_ReqOption m_Node;
            protected RequestAPI m_Request;

            public ApiBase(FlowNode_ReqOption node)
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

