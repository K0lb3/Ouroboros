namespace SRPG
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using UnityEngine;

    [Pin(820, "傭兵リスト取得失敗", 1, 820), NodeType("System/ReqSupportList"), Pin(0, "傭兵リスト取得", 0, 0), Pin(10, "傭兵リスト取得(強制)", 0, 10), Pin(810, "傭兵リスト取得成功", 1, 810)]
    public class FlowNode_ReqSupportList : FlowNode_Network
    {
        public const int INPUT_GETLIST = 0;
        public const int INPUT_GETLIST_FORCE = 10;
        public const int OUTPUT_SUCCESS = 810;
        public const int OUTPUT_FAILED = 820;
        public UnitListWindow m_Window;
        private ApiBase m_Api;

        public FlowNode_ReqSupportList()
        {
            base..ctor();
            return;
        }

        public override void OnActivate(int pinID)
        {
            int num;
            if (this.m_Api == null)
            {
                goto Label_0016;
            }
            DebugUtility.LogError("同時に複数の通信が入ると駄目！");
            return;
        Label_0016:
            num = pinID;
            if (num == null)
            {
                goto Label_002B;
            }
            if (num == 10)
            {
                goto Label_004D;
            }
            goto Label_006F;
        Label_002B:
            this.m_Api = new Api_SupportList(this, this.m_Window.rootWindow.GetElement(), 0);
            goto Label_006F;
        Label_004D:
            this.m_Api = new Api_SupportList(this, this.m_Window.rootWindow.GetElement(), 1);
        Label_006F:
            if (this.m_Api == null)
            {
                goto Label_008C;
            }
            this.m_Api.Start();
            base.set_enabled(1);
        Label_008C:
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

        public class Api_SupportList : FlowNode_ReqSupportList.ApiBase
        {
            private EElement m_Element;
            private bool m_IsForce;
            private List<SupportData> m_Select;

            public Api_SupportList(FlowNode_ReqSupportList node, EElement element)
            {
                this.m_Select = new List<SupportData>();
                base..ctor(node);
                this.m_Element = element;
                return;
            }

            public Api_SupportList(FlowNode_ReqSupportList node, EElement element, bool isForce)
            {
                QuestParam param;
                SupportData[] dataArray;
                SupportData data;
                SupportData[] dataArray2;
                int num;
                int num2;
                this.m_Select = new List<SupportData>();
                base..ctor(node);
                this.m_Element = element;
                this.m_IsForce = isForce;
                if ((node.m_Window != null) == null)
                {
                    goto Label_00C7;
                }
                param = node.m_Window.GetData<QuestParam>("data_quest");
                dataArray = node.m_Window.GetData<SupportData[]>("data_support");
                if (param == null)
                {
                    goto Label_009C;
                }
                if (param.type != 15)
                {
                    goto Label_009C;
                }
                dataArray2 = dataArray;
                num = 0;
                goto Label_008D;
            Label_0070:
                data = dataArray2[num];
                if (data == null)
                {
                    goto Label_0087;
                }
                this.m_Select.Add(data);
            Label_0087:
                num += 1;
            Label_008D:
                if (num < ((int) dataArray2.Length))
                {
                    goto Label_0070;
                }
                goto Label_00C7;
            Label_009C:
                num2 = node.m_Window.GetData<int>("data_party_index", -1);
                if (dataArray[num2] == null)
                {
                    goto Label_00C7;
                }
                this.m_Select.Add(dataArray[num2]);
            Label_00C7:
                return;
            }

            public override unsafe void Complete(WWWResult www)
            {
                WebAPI.JSON_BodyResponse<Json> response;
                FlowNode_ReqSupportList.SupportList list;
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
                    goto Label_00C3;
                }
                list = new FlowNode_ReqSupportList.SupportList(this.m_Element);
                list.Deserialize(response.body.supports);
                if ((base.m_Node.m_Window != null) == null)
                {
                    goto Label_00C3;
                }
                if (base.m_Node.m_Window.rootWindow == null)
                {
                    goto Label_00C3;
                }
                base.m_Node.m_Window.rootWindow.AddData("data_supportres", list);
            Label_00C3:
                Network.RemoveAPI();
                this.Success();
                return;
            }

            public override void Failed()
            {
                base.m_Node.ActivateOutputLinks(820);
                Network.RemoveAPI();
                Network.ResetError();
                base.m_Node.set_enabled(0);
                return;
            }

            public override void Success()
            {
                base.m_Node.ActivateOutputLinks(810);
                base.m_Node.set_enabled(0);
                return;
            }

            public override string url
            {
                get
                {
                    if (this.m_Element != null)
                    {
                        goto Label_0011;
                    }
                    return "btl/com/supportlist";
                Label_0011:
                    return "btl/com/support_elem";
                }
            }

            public override string req
            {
                get
                {
                    StringBuilder builder;
                    int num;
                    builder = new StringBuilder(0x80);
                    builder.Append("\"elem\":" + ((int) this.m_Element));
                    builder.Append(",\"is_update\":" + ((this.m_IsForce == null) ? "1" : "0"));
                    if (this.m_Select == null)
                    {
                        goto Label_014C;
                    }
                    if (this.m_Select.Count <= 0)
                    {
                        goto Label_014C;
                    }
                    builder.Append(",\"helps\":[");
                    num = 0;
                    goto Label_012F;
                Label_0081:
                    if (num <= 0)
                    {
                        goto Label_0094;
                    }
                    builder.Append(",");
                Label_0094:
                    builder.Append("{");
                    builder.Append("\"fuid\":\"" + this.m_Select[num].FUID + "\"");
                    builder.Append(",\"elem\":" + ((int) this.m_Select[num].Unit.SupportElement));
                    builder.Append(",\"iname\":\"" + this.m_Select[num].Unit.UnitID + "\"");
                    builder.Append("}");
                    num += 1;
                Label_012F:
                    if (num < this.m_Select.Count)
                    {
                        goto Label_0081;
                    }
                    builder.Append("]");
                Label_014C:
                    return builder.ToString();
                }
            }

            [Serializable]
            public class Json
            {
                public Json_Support[] supports;

                public Json()
                {
                    base..ctor();
                    return;
                }
            }
        }

        public class ApiBase
        {
            protected FlowNode_ReqSupportList m_Node;
            protected RequestAPI m_Request;

            public ApiBase(FlowNode_ReqSupportList node)
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

        public class SupportList
        {
            public EElement m_Element;
            public SupportData[] m_SupportData;

            public SupportList(EElement element)
            {
                base..ctor();
                this.m_Element = element;
                return;
            }

            public void Deserialize(Json_Support[] json)
            {
                int num;
                SupportData data;
                Exception exception;
                this.m_SupportData = new SupportData[(int) json.Length];
                num = 0;
                goto Label_0042;
            Label_0015:
                data = new SupportData();
            Label_001B:
                try
                {
                    data.Deserialize(json[num]);
                    this.m_SupportData[num] = data;
                    goto Label_003E;
                }
                catch (Exception exception1)
                {
                Label_0032:
                    exception = exception1;
                    DebugUtility.LogException(exception);
                    goto Label_003E;
                }
            Label_003E:
                num += 1;
            Label_0042:
                if (num < ((int) json.Length))
                {
                    goto Label_0015;
                }
                return;
            }
        }
    }
}

