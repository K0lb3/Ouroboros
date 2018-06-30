namespace SRPG
{
    using GR;
    using System;

    [NodeType("System/ReqChatAutoAssign", 0x7fe5), Pin(1, "Success", 1, 1), Pin(0, "Request", 0, 0)]
    public class FlowNode_ReqChatAutoAssign : FlowNode_Network
    {
        public FlowNode_ReqChatAutoAssign()
        {
            base..ctor();
            return;
        }

        public override void OnActivate(int pinID)
        {
            if (pinID != null)
            {
                goto Label_0024;
            }
            base.ExecRequest(new ReqChatChannelAutoAssign(new Network.ResponseCallback(this.ResponseCallback)));
            base.set_enabled(1);
        Label_0024:
            return;
        }

        public override unsafe void OnSuccess(WWWResult www)
        {
            WebAPI.JSON_BodyResponse<JSON_ChatChannelAutoAssign> response;
            Network.EErrCode code;
            if (Network.IsError == null)
            {
                goto Label_0011;
            }
            code = Network.ErrCode;
            return;
        Label_0011:
            response = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<JSON_ChatChannelAutoAssign>>(&www.text);
            DebugUtility.Assert((response == null) == 0, "res == null");
            Network.RemoveAPI();
            GlobalVars.CurrentChatChannel.Set(response.body.channel);
            GlobalVars.ChatChannelViewNum = response.body.channel_one_page_num;
            GlobalVars.ChatChannelMax = response.body.max_channel_size;
            this.Success();
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

