namespace SRPG
{
    using GR;
    using System;

    [Pin(0, "Request", 0, 0), Pin(11, "Failed", 1, 11), Pin(10, "Success", 1, 10), NodeType("Network/ReqFirstChargeState", 0x7fe5)]
    public class FlowNode_ReqFirstChargeState : FlowNode_Network
    {
        private const int INPUT_REQUEST = 0;
        private const int OUTPUT_SUCCESS = 10;
        private const int OUTPUT_FAILED = 11;

        public FlowNode_ReqFirstChargeState()
        {
            base..ctor();
            return;
        }

        private void _Failed()
        {
            Network.RemoveAPI();
            Network.ResetError();
            base.set_enabled(0);
            base.ActivateOutputLinks(11);
            return;
        }

        private void _Success()
        {
            base.set_enabled(0);
            base.ActivateOutputLinks(0);
            return;
        }

        public override void OnActivate(int pinID)
        {
            if (pinID != null)
            {
                goto Label_0024;
            }
            base.ExecRequest(new ReqFirstChargeState(new Network.ResponseCallback(this.ResponseCallback)));
            base.set_enabled(1);
        Label_0024:
            return;
        }

        public override unsafe void OnSuccess(WWWResult www)
        {
            WebAPI.JSON_BodyResponse<JSON_FirstChargeState> response;
            if (Network.IsError == null)
            {
                goto Label_0011;
            }
            this._Failed();
            return;
        Label_0011:
            response = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<JSON_FirstChargeState>>(&www.text);
            DebugUtility.Assert((response == null) == 0, "res == null");
            Network.RemoveAPI();
            if (response.body == null)
            {
                goto Label_005E;
            }
            MonoSingleton<GameManager>.Instance.Player.FirstChargeStatus = response.body.charge_bonus;
            goto Label_006E;
        Label_005E:
            MonoSingleton<GameManager>.Instance.Player.FirstChargeStatus = 0;
        Label_006E:
            this._Success();
            return;
        }

        public class JSON_FirstChargeState
        {
            public int charge_bonus;

            public JSON_FirstChargeState()
            {
                base..ctor();
                return;
            }
        }
    }
}

