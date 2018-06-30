namespace SRPG
{
    using GR;
    using System;

    [Pin(1, "Success", 1, 1), NodeType("Network/GachaHistory", 0x7fe5), Pin(0, "Request", 0, 0)]
    public class FlowNode_ReqGachaHistory : FlowNode_Network
    {
        public FlowNode_ReqGachaHistory()
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
            base.ExecRequest(new ReqGachaHistory(new Network.ResponseCallback(this.ResponseCallback)));
            base.set_enabled(1);
        Label_0024:
            return;
        }

        public override unsafe void OnSuccess(WWWResult www)
        {
            WebAPI.JSON_BodyResponse<Json_GachaHistory> response;
            GachaHistoryWindow window;
            Network.EErrCode code;
            if (Network.IsError == null)
            {
                goto Label_0011;
            }
            code = Network.ErrCode;
            return;
        Label_0011:
            response = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json_GachaHistory>>(&www.text);
            DebugUtility.Assert((response == null) == 0, "res == null");
            Network.RemoveAPI();
            window = base.GetComponent<GachaHistoryWindow>();
            if ((window != null) == null)
            {
                goto Label_0059;
            }
            window.SetGachaHistoryData(response.body.log);
        Label_0059:
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

