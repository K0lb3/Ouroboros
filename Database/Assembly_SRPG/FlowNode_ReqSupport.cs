namespace SRPG
{
    using System;
    using UnityEngine;

    [Pin(100, "傭兵取得", 0, 100), NodeType("System/ReqSupport", 0x7fe5), Pin(120, "傭兵取得失敗", 1, 120), Pin(110, "傭兵取得成功", 1, 110)]
    public class FlowNode_ReqSupport : FlowNode_Network
    {
        public const int INPUT_SUPPORT = 100;
        public const int OUTPUT_SUPPORT_SUCCESS = 110;
        public const int OUTPUT_SUPPORT_FAILED = 120;
        [SerializeField]
        private SupportElementListRootWindow m_TargetWindow;
        private ApiBase m_Api;

        public FlowNode_ReqSupport()
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
            this.m_Api = new Api_Support(this);
            if (this.m_Api == null)
            {
                goto Label_003F;
            }
            this.m_Api.Start();
            base.set_enabled(1);
        Label_003F:
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

        public class Api_Support : FlowNode_ReqSupport.ApiBase
        {
            public Api_Support(FlowNode_ReqSupport node)
            {
                base..ctor(node);
                return;
            }

            public override unsafe void Complete(WWWResult www)
            {
                WebAPI.JSON_BodyResponse<Json> response;
                long[] numArray;
                int num;
                int num2;
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
                    goto Label_0116;
                }
                if (response.body.unit == null)
                {
                    goto Label_0116;
                }
                numArray = new long[Enum.GetValues(typeof(EElement)).Length];
                if (response.body.units == null)
                {
                    goto Label_00F2;
                }
                num = 0;
                goto Label_00DF;
            Label_00A1:
                if (response.body.units[num] == null)
                {
                    goto Label_00DB;
                }
                num2 = response.body.units[num].elem;
                numArray[num2] = response.body.units[num].iid;
            Label_00DB:
                num += 1;
            Label_00DF:
                if (num < ((int) response.body.units.Length))
                {
                    goto Label_00A1;
                }
            Label_00F2:
                numArray[0] = response.body.unit.iid;
                base.m_Node.m_TargetWindow.SetSupportUnitData(numArray);
            Label_0116:
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
                    return "support";
                }
            }

            public override string req
            {
                get
                {
                    return null;
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

            [Serializable]
            public class Json_OwnSupportData
            {
                public long iid;
                public int elem;

                public Json_OwnSupportData()
                {
                    base..ctor();
                    return;
                }
            }
        }

        public class ApiBase
        {
            protected FlowNode_ReqSupport m_Node;
            protected RequestAPI m_Request;

            public ApiBase(FlowNode_ReqSupport node)
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

