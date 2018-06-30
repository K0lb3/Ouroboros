namespace SRPG
{
    using GR;
    using System;
    using System.Text;
    using UnityEngine;

    [Pin(110, "ウィッシュリスト取得完了", 1, 110), Pin(200, "Set", 0, 200), Pin(210, "ウィッシュリスト設定完了", 1, 210), Pin(220, "ウィッシュリスト設定失敗", 1, 220), NodeType("System/WebApi/WishList", 0x7fe5), Pin(100, "Get", 0, 100), Pin(120, "ウィッシュリスト取得失敗", 1, 120)]
    public class FlowNode_ReqWishList : FlowNode_Network
    {
        private ApiBase m_Api;

        public FlowNode_ReqWishList()
        {
            base..ctor();
            return;
        }

        public override void OnActivate(int pinID)
        {
            FriendPresentRootWindow.WantContent.ItemAccessor accessor;
            FriendPresentWantWindow.Content.ItemAccessor accessor2;
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
            this.m_Api = new Api_WishList(this);
            goto Label_006A;
        Label_002F:
            if (pinID != 200)
            {
                goto Label_006A;
            }
            accessor = FriendPresentRootWindow.WantContent.clickItem;
            accessor2 = FriendPresentWantWindow.Content.clickItem;
            if (accessor == null)
            {
                goto Label_006A;
            }
            if (accessor2 == null)
            {
                goto Label_006A;
            }
            this.m_Api = new Api_WishListSet(this, accessor2.presentId, accessor.priority);
        Label_006A:
            if (this.m_Api == null)
            {
                goto Label_0087;
            }
            this.m_Api.Start();
            base.set_enabled(1);
        Label_0087:
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

        public class Api_WishList : FlowNode_ReqWishList.ApiBase
        {
            public Api_WishList(FlowNode_ReqWishList node)
            {
                base..ctor(node);
                return;
            }

            public override unsafe void Complete(WWWResult www)
            {
                WebAPI.JSON_BodyResponse<FriendPresentWishList.Json[]> response;
                if (Network.IsError == null)
                {
                    goto Label_0016;
                }
                base.m_Node.OnFailed();
                return;
            Label_0016:
                DebugMenu.Log("API", this.url + ":" + &www.text);
                response = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<FriendPresentWishList.Json[]>>(&www.text);
                DebugUtility.Assert((response == null) == 0, "res == null");
                if (response.body == null)
                {
                    goto Label_0070;
                }
                MonoSingleton<GameManager>.Instance.Deserialize(response.body);
            Label_0070:
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
                    return "wishlist";
                }
            }
        }

        public class Api_WishListSet : FlowNode_ReqWishList.ApiBase
        {
            private string m_Id;
            private int m_Priority;

            public Api_WishListSet(FlowNode_ReqWishList node, string iname, int priority)
            {
                base..ctor(node);
                this.m_Id = iname;
                this.m_Priority = priority;
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
                    goto Label_008B;
                }
                if (response.body.result == null)
                {
                    goto Label_008B;
                }
                MonoSingleton<GameManager>.Instance.Player.SetWishList(this.m_Id, this.m_Priority);
            Label_008B:
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
                    return "wishlist/set";
                }
            }

            public override string req
            {
                get
                {
                    StringBuilder builder;
                    builder = new StringBuilder(0x80);
                    builder.Append("\"iname\":\"" + this.m_Id + "\"");
                    builder.Append(",\"priority\":" + ((int) (this.m_Priority + 1)));
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
            protected FlowNode_ReqWishList m_Node;
            protected RequestAPI m_Request;

            public ApiBase(FlowNode_ReqWishList node)
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

