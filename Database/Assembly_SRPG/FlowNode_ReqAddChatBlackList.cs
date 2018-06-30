namespace SRPG
{
    using GR;
    using System;

    [Pin(10, "CanNotAddBlackList", 1, 10), Pin(1, "Success", 1, 1), NodeType("System/ReqAddChatBlackList", 0x7fe5), Pin(0, "Request", 0, 0)]
    public class FlowNode_ReqAddChatBlackList : FlowNode_Network
    {
        public FlowNode_ReqAddChatBlackList()
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
            base.ExecRequest(new ReqAddChatBlackList(str, new Network.ResponseCallback(this.ResponseCallback)));
            base.set_enabled(0);
        Label_0030:
            return;
        }

        public override unsafe void OnSuccess(WWWResult www)
        {
            WebAPI.JSON_BodyResponse<JSON_ChatBlackListRes> response;
            Network.EErrCode code;
            if (Network.IsError == null)
            {
                goto Label_0028;
            }
            if (Network.ErrCode == 0x2137)
            {
                goto Label_0020;
            }
            goto Label_0027;
        Label_0020:
            this.OnBack();
            return;
        Label_0027:
            return;
        Label_0028:
            response = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<JSON_ChatBlackListRes>>(&www.text);
            DebugUtility.Assert((response == null) == 0, "res == null");
            Network.RemoveAPI();
            if (response.body.is_success != 1)
            {
                goto Label_0062;
            }
            this.Success();
        Label_0062:
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

