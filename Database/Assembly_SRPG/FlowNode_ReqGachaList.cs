namespace SRPG
{
    using GR;
    using System;

    [NodeType("Network/gacha", 0x7fe5), Pin(0, "Request", 0, 0), Pin(1, "Success", 1, 1), Pin(2, "Failure", 1, 2), Pin(3, "ToCheckPending(引き直し召喚チェック)", 1, 3)]
    public class FlowNode_ReqGachaList : FlowNode_Network
    {
        private const int PIN_IN_REQUEST = 0;
        private const int PIN_OT_REQUEST_SUCCESS = 1;
        private const int PIN_OT_REQUEST_FAILURE = 2;
        private const int PIN_OT_TO_GACHA_PENDING = 3;

        public FlowNode_ReqGachaList()
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
                goto Label_0036;
            }
            if (Network.Mode != 1)
            {
                goto Label_0018;
            }
            this.Success();
            return;
        Label_0018:
            base.ExecRequest(new ReqGacha(new Network.ResponseCallback(this.ResponseCallback)));
            base.set_enabled(1);
        Label_0036:
            return;
        }

        public override unsafe void OnSuccess(WWWResult www)
        {
            WebAPI.JSON_BodyResponse<Json_GachaList> response;
            GameManager manager;
            Exception exception;
            Network.EErrCode code;
            if (Network.IsError == null)
            {
                goto Label_0017;
            }
            code = Network.ErrCode;
            this.OnRetry();
            return;
        Label_0017:
            response = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json_GachaList>>(&www.text);
            DebugUtility.Assert((response == null) == 0, "res == null");
            Network.RemoveAPI();
            if (response.body != null)
            {
                goto Label_004C;
            }
            this.Failure();
            return;
        Label_004C:
            manager = MonoSingleton<GameManager>.Instance;
        Label_0052:
            try
            {
                if (manager.Deserialize(response.body) != null)
                {
                    goto Label_006E;
                }
                this.Failure();
                goto Label_00B9;
            Label_006E:
                goto Label_008A;
            }
            catch (Exception exception1)
            {
            Label_0073:
                exception = exception1;
                DebugUtility.LogException(exception);
                this.Failure();
                goto Label_00B9;
            }
        Label_008A:
            GachaResultData.Reset();
            if ((FlowNode_Variable.Get("REDRAW_GACHA_PENDING") == "1") == null)
            {
                goto Label_00B3;
            }
            this.ToCheckPending();
            goto Label_00B9;
        Label_00B3:
            this.Success();
        Label_00B9:
            return;
        }

        private void Success()
        {
            base.set_enabled(0);
            base.ActivateOutputLinks(1);
            return;
        }

        private void ToCheckPending()
        {
            base.set_enabled(0);
            base.ActivateOutputLinks(3);
            return;
        }
    }
}

