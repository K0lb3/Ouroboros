namespace SRPG
{
    using GR;
    using System;

    [Pin(0, "Request", 0, 0), NodeType("System/ReqRemoveChatBlackList", 0x7fe5), Pin(1, "Success", 1, 1)]
    public class FlowNode_ReqRemoveChatBlackList : FlowNode_Network
    {
        public FlowNode_ReqRemoveChatBlackList()
        {
            base..ctor();
            return;
        }

        public override void OnActivate(int pinID)
        {
            string str;
            if (pinID != null)
            {
                goto Label_0030;
            }
            str = FlowNode_Variable.Get("SelectUserID");
            base.ExecRequest(new ReqRemoveChatBlackList(str, new Network.ResponseCallback(this.ResponseCallback)));
            base.set_enabled(1);
        Label_0030:
            return;
        }

        public override unsafe void OnSuccess(WWWResult www)
        {
            WebAPI.JSON_BodyResponse<JSON_ChatBlackListRes> response;
            Network.EErrCode code;
            if (Network.IsError == null)
            {
                goto Label_0011;
            }
            code = Network.ErrCode;
            return;
        Label_0011:
            response = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<JSON_ChatBlackListRes>>(&www.text);
            DebugUtility.Assert((response == null) == 0, "res == null");
            Network.RemoveAPI();
            if (response.body.is_success != 1)
            {
                goto Label_004B;
            }
            this.Success();
        Label_004B:
            return;
        }

        private void Success()
        {
            base.set_enabled(0);
            base.ActivateOutputLinks(1);
            return;
        }
    }
}

