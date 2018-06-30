namespace SRPG
{
    using GR;
    using System;

    [Pin(0, "Request", 0, 0), NodeType("System/ReqChatChannelMaster", 0x7fe5), Pin(1, "Success", 1, 1), Pin(2, "Failed", 1, 2)]
    public class FlowNode_ReqChatChannelMaster : FlowNode_Network
    {
        public FlowNode_ReqChatChannelMaster()
        {
            base..ctor();
            return;
        }

        private void Failure()
        {
            base.set_enabled(0);
            base.ActivateOutputLinks(2);
            return;
        }

        public override void OnActivate(int pinID)
        {
            if (pinID != null)
            {
                goto Label_0024;
            }
            base.ExecRequest(new ReqChatChannelMaster(new Network.ResponseCallback(this.ResponseCallback)));
            base.set_enabled(1);
        Label_0024:
            return;
        }

        public override unsafe void OnSuccess(WWWResult www)
        {
            WebAPI.JSON_BodyResponse<JSON_ChatChannelMaster> response;
            Network.EErrCode code;
            if (Network.IsError == null)
            {
                goto Label_0015;
            }
            code = Network.ErrCode;
        Label_0015:
            response = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<JSON_ChatChannelMaster>>(&www.text);
            DebugUtility.Assert((response == null) == 0, "res == null");
            Network.RemoveAPI();
            if (MonoSingleton<GameManager>.Instance.Deserialize(response.body) != null)
            {
                goto Label_0054;
            }
            this.Failure();
            return;
        Label_0054:
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

