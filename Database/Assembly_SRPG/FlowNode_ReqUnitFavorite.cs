namespace SRPG
{
    using GR;
    using System;
    using System.Collections.Generic;
    using System.Text;

    [Pin(120, "お気に入り設定ON完了", 1, 120), Pin(200, "お気に入り設定失敗", 1, 200), Pin(130, "お気に入り設定OFF完了", 1, 130), Pin(110, "お気に入りOFF", 0, 110), Pin(100, "お気に入りON", 0, 100), NodeType("System/WebApi/ReqUnitFavorite", 0x7fe5)]
    public class FlowNode_ReqUnitFavorite : FlowNode_Network
    {
        private ApiBase m_Api;

        public FlowNode_ReqUnitFavorite()
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
                goto Label_0030;
            }
            this.m_Api = new Api_SetUnitFavorite(this, 1);
            goto Label_0045;
        Label_0030:
            if (pinID != 110)
            {
                goto Label_0045;
            }
            this.m_Api = new Api_SetUnitFavorite(this, 0);
        Label_0045:
            if (this.m_Api == null)
            {
                goto Label_0062;
            }
            this.m_Api.Start();
            base.set_enabled(1);
        Label_0062:
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

        public class Api_SetUnitFavorite : FlowNode_ReqUnitFavorite.ApiBase
        {
            private List<long> m_OnUniqId;
            private List<long> m_OffUniqId;

            public Api_SetUnitFavorite(FlowNode_ReqUnitFavorite node, bool value)
            {
                this.m_OnUniqId = new List<long>();
                this.m_OffUniqId = new List<long>();
                base..ctor(node);
                if ((UnitEnhanceV3.Instance != null) == null)
                {
                    goto Label_007B;
                }
                if (UnitEnhanceV3.Instance.CurrentUnit == null)
                {
                    goto Label_007B;
                }
                if (value == null)
                {
                    goto Label_0061;
                }
                this.m_OnUniqId.Add(UnitEnhanceV3.Instance.CurrentUnit.UniqueID);
                goto Label_007B;
            Label_0061:
                this.m_OffUniqId.Add(UnitEnhanceV3.Instance.CurrentUnit.UniqueID);
            Label_007B:
                return;
            }

            public override unsafe void Complete(WWWResult www)
            {
                WebAPI.JSON_BodyResponse<Json> response;
                PlayerData data;
                int num;
                UnitData data2;
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
                if ((response.body == null) || (response.body.units == null))
                {
                    goto Label_00E3;
                }
                data = MonoSingleton<GameManager>.Instance.Player;
                if (data == null)
                {
                    goto Label_00E3;
                }
                num = 0;
                goto Label_00D0;
            Label_0088:
                data2 = data.GetUnitData(response.body.units[num].iid);
                if (data2 == null)
                {
                    goto Label_00CC;
                }
                data2.IsFavorite = (response.body.units[num].fav != 1) ? 0 : 1;
            Label_00CC:
                num += 1;
            Label_00D0:
                if (num < ((int) response.body.units.Length))
                {
                    goto Label_0088;
                }
            Label_00E3:
                Network.RemoveAPI();
                this.Success();
                return;
            }

            public override void Failed()
            {
                base.m_Node.ActivateOutputLinks(200);
                Network.RemoveAPI();
                Network.ResetError();
                base.m_Node.set_enabled(0);
                return;
            }

            public override void Success()
            {
                if (this.m_OnUniqId.Count <= 0)
                {
                    goto Label_0024;
                }
                base.m_Node.ActivateOutputLinks(120);
                goto Label_005C;
            Label_0024:
                if (this.m_OffUniqId.Count <= 0)
                {
                    goto Label_004B;
                }
                base.m_Node.ActivateOutputLinks(130);
                goto Label_005C;
            Label_004B:
                base.m_Node.ActivateOutputLinks(200);
            Label_005C:
                base.m_Node.set_enabled(0);
                return;
            }

            public override string url
            {
                get
                {
                    return "unit/favorite";
                }
            }

            public override string req
            {
                get
                {
                    StringBuilder builder;
                    int num;
                    int num2;
                    long num3;
                    long num4;
                    builder = new StringBuilder(0x80);
                    builder.Append("\"favids\":[");
                    num = 0;
                    goto Label_0058;
                Label_001E:
                    num3 = this.m_OnUniqId[num];
                    builder.Append(((num <= 0) ? string.Empty : ",") + &num3.ToString());
                    num += 1;
                Label_0058:
                    if (num < this.m_OnUniqId.Count)
                    {
                        goto Label_001E;
                    }
                    builder.Append("]");
                    builder.Append(",\"unfavids\":[");
                    num2 = 0;
                    goto Label_00C3;
                Label_0088:
                    num4 = this.m_OffUniqId[num2];
                    builder.Append(((num2 <= 0) ? string.Empty : ",") + &num4.ToString());
                    num2 += 1;
                Label_00C3:
                    if (num2 < this.m_OffUniqId.Count)
                    {
                        goto Label_0088;
                    }
                    builder.Append("]");
                    DebugMenu.Log("API", builder.ToString());
                    return builder.ToString();
                }
            }

            [Serializable]
            public class Json
            {
                public Json_Unit[] units;

                public Json()
                {
                    base..ctor();
                    return;
                }
            }
        }

        public class ApiBase
        {
            protected FlowNode_ReqUnitFavorite m_Node;
            protected RequestAPI m_Request;

            public ApiBase(FlowNode_ReqUnitFavorite node)
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

