namespace SRPG
{
    using System;
    using System.Text;
    using UnityEngine;

    [NodeType("System/ReqSupportSet", 0x7fe5), Pin(100, "傭兵設定", 0, 100), Pin(110, "傭兵設定成功", 1, 110), Pin(120, "傭兵設定失敗", 1, 120)]
    public class FlowNode_ReqSupportSet : FlowNode_Network
    {
        public const int INPUT_SUPPORT_SET = 100;
        public const int OUTPUT_SUPPORT_SET_SUCCESS = 110;
        public const int OUTPUT_SUPPORT_SET_FAILED = 120;
        [SerializeField]
        private SupportElementListRootWindow m_TargetWindow;
        private ApiBase m_Api;

        public FlowNode_ReqSupportSet()
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
            this.m_Api = new Api_SupportSet(this, this.m_TargetWindow.GetSupportUnitData());
            if (this.m_Api == null)
            {
                goto Label_004A;
            }
            this.m_Api.Start();
            base.set_enabled(1);
        Label_004A:
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

        public class Api_SupportSet : FlowNode_ReqSupportSet.ApiBase
        {
            private FlowNode_ReqSupportSet.OwnSupportData[] m_SupportData;

            public Api_SupportSet(FlowNode_ReqSupportSet node, FlowNode_ReqSupportSet.OwnSupportData[] ownSupportData)
            {
                base..ctor(node);
                this.m_SupportData = ownSupportData;
                return;
            }

            public override void Complete(WWWResult www)
            {
                int num;
                if (Network.IsError == null)
                {
                    goto Label_0011;
                }
                this.Failed();
                return;
            Label_0011:
                num = 0;
                goto Label_0057;
            Label_0018:
                if (this.m_SupportData[num] == null)
                {
                    goto Label_0053;
                }
                if (this.m_SupportData[num].m_Element != null)
                {
                    goto Label_0053;
                }
                GlobalVars.SelectedSupportUnitUniqueID.Set(this.m_SupportData[num].m_UniqueID);
                goto Label_0065;
            Label_0053:
                num += 1;
            Label_0057:
                if (num < ((int) this.m_SupportData.Length))
                {
                    goto Label_0018;
                }
            Label_0065:
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
                    return "support/set";
                }
            }

            public override string req
            {
                get
                {
                    StringBuilder builder;
                    int num;
                    builder = new StringBuilder(0x80);
                    builder.Append("\"units\":[");
                    num = 0;
                    goto Label_009E;
                Label_001E:
                    if (this.m_SupportData[num] != null)
                    {
                        goto Label_0030;
                    }
                    goto Label_009A;
                Label_0030:
                    if (num == null)
                    {
                        goto Label_0042;
                    }
                    builder.Append(",");
                Label_0042:
                    builder.Append("{");
                    builder.Append("\"id\":");
                    builder.Append(this.m_SupportData[num].m_UniqueID);
                    builder.Append(",\"elem\":");
                    builder.Append(this.m_SupportData[num].m_Element);
                    builder.Append("}");
                Label_009A:
                    num += 1;
                Label_009E:
                    if (num < ((int) this.m_SupportData.Length))
                    {
                        goto Label_001E;
                    }
                    builder.Append("]");
                    return builder.ToString();
                }
            }

            [Serializable]
            public class Json
            {
                public FlowNode_ReqSupportSet.Api_SupportSet.Json_OwnSupportData[] units;

                public Json()
                {
                    base..ctor();
                    return;
                }
            }

            [Serializable]
            public class Json_OwnSupportData
            {
                public long id;
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
            protected FlowNode_ReqSupportSet m_Node;
            protected RequestAPI m_Request;

            public ApiBase(FlowNode_ReqSupportSet node)
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

        public class OwnSupportData
        {
            public long m_UniqueID;
            public EElement m_Element;

            public OwnSupportData()
            {
                base..ctor();
                return;
            }
        }
    }
}

