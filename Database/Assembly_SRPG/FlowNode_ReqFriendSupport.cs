namespace SRPG
{
    using System;
    using System.Text;
    using UnityEngine;

    [Pin(110, "傭兵取得成功", 1, 110), NodeType("System/ReqFriendSupport", 0x7fe5), Pin(100, "傭兵取得", 0, 100), Pin(120, "傭兵取得失敗", 1, 120)]
    public class FlowNode_ReqFriendSupport : FlowNode_Network
    {
        public const int INPUT_FRIEND_SUPPORT = 100;
        public const int OUTPUT_FRIEND_SUPPORT_SUCCESS = 110;
        public const int OUTPUT_FRIEND_SUPPORT_FAILED = 120;
        [SerializeField]
        private SerializeValueBehaviour m_SerializeValue;
        private ApiBase m_Api;

        public FlowNode_ReqFriendSupport()
        {
            base..ctor();
            return;
        }

        public override void OnActivate(int pinID)
        {
            SerializeValueList list;
            if (this.m_Api == null)
            {
                goto Label_0016;
            }
            DebugUtility.LogError("同時に複数の通信が入ると駄目！");
            return;
        Label_0016:
            list = ((this.m_SerializeValue != null) == null) ? new SerializeValueList() : this.m_SerializeValue.list;
            this.m_Api = new Api_FriendSupport(this, list);
            if (this.m_Api == null)
            {
                goto Label_0067;
            }
            this.m_Api.Start();
            base.set_enabled(1);
        Label_0067:
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

        public class Api_FriendSupport : FlowNode_ReqFriendSupport.ApiBase
        {
            private SerializeValueList m_ValueList;

            public Api_FriendSupport(FlowNode_ReqFriendSupport node, SerializeValueList valueList)
            {
                base..ctor(node);
                this.m_ValueList = valueList;
                return;
            }

            public override unsafe void Complete(WWWResult www)
            {
                WebAPI.JSON_BodyResponse<Json> response;
                UnitData[] dataArray;
                int num;
                Json_Unit unit;
                int num2;
                UnitData data;
                if (Network.IsError == null)
                {
                    goto Label_0016;
                }
                base.m_Node.OnBack();
                return;
            Label_0016:
                DebugMenu.Log("API", this.url + ":" + &www.text);
                response = JsonUtility.FromJson<WebAPI.JSON_BodyResponse<Json>>(&www.text);
                DebugUtility.Assert((response == null) == 0, "res == null");
                if (response.body == null)
                {
                    goto Label_014D;
                }
                if (response.body.unit == null)
                {
                    goto Label_014D;
                }
                dataArray = new UnitData[Enum.GetValues(typeof(EElement)).Length];
                if (response.body.units == null)
                {
                    goto Label_010E;
                }
                num = 0;
                goto Label_00FB;
            Label_00A1:
                unit = response.body.units[num];
                num2 = unit.elem;
                if (num2 >= ((int) dataArray.Length))
                {
                    goto Label_00C9;
                }
                if (num2 >= 0)
                {
                    goto Label_00E4;
                }
            Label_00C9:
                DebugUtility.LogError(string.Format("不正なインデックスが属性値として指定されています。 elem = {0}", (int) num2));
                goto Label_00F7;
            Label_00E4:
                dataArray[num2] = new UnitData();
                dataArray[num2].Deserialize(unit);
            Label_00F7:
                num += 1;
            Label_00FB:
                if (num < ((int) response.body.units.Length))
                {
                    goto Label_00A1;
                }
            Label_010E:
                if (response.body.unit == null)
                {
                    goto Label_013C;
                }
                data = new UnitData();
                data.Deserialize(response.body.unit);
                dataArray[0] = data;
            Label_013C:
                this.m_ValueList.SetObject("data_units", dataArray);
            Label_014D:
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
                    goto Label_0035;
                }
                base.m_Node.ActivateOutputLinks(120);
                base.m_Node.set_enabled(0);
            Label_0035:
                return;
            }

            public override void Success()
            {
                if ((base.m_Node != null) == null)
                {
                    goto Label_002B;
                }
                base.m_Node.ActivateOutputLinks(110);
                base.m_Node.set_enabled(0);
            Label_002B:
                return;
            }

            public override string url
            {
                get
                {
                    return "friend/support";
                }
            }

            public override string req
            {
                get
                {
                    StringBuilder builder;
                    builder = new StringBuilder(0x80);
                    builder.Append("\"fuid\":\"");
                    builder.Append(this.m_ValueList.GetString("fuid"));
                    builder.Append("\"");
                    return builder.ToString();
                }
            }

            [Serializable]
            public class Json
            {
                public Json_Unit unit;
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
            protected FlowNode_ReqFriendSupport m_Node;
            protected RequestAPI m_Request;

            public ApiBase(FlowNode_ReqFriendSupport node)
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

