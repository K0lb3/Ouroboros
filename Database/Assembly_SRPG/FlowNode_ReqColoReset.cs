namespace SRPG
{
    using GR;
    using System;

    [Pin(1, "Success", 1, 1), Pin(0, "Request", 0, 0), NodeType("Network/btl_colo_reset", 0x7fe5)]
    public class FlowNode_ReqColoReset : FlowNode_Network
    {
        public ColoResetTypes ResetType;

        public FlowNode_ReqColoReset()
        {
            base..ctor();
            return;
        }

        private int getRequiredCoin()
        {
            return 1;
        }

        public override void OnActivate(int pinID)
        {
            if (pinID != null)
            {
                goto Label_003C;
            }
            if (Network.Mode != 1)
            {
                goto Label_0018;
            }
            this.Success();
            return;
        Label_0018:
            base.ExecRequest(new ReqBtlColoReset(this.ResetType, new Network.ResponseCallback(this.ResponseCallback)));
            base.set_enabled(1);
        Label_003C:
            return;
        }

        public override unsafe void OnSuccess(WWWResult www)
        {
            WebAPI.JSON_BodyResponse<Json_PlayerDataAll> response;
            Exception exception;
            Network.EErrCode code;
            if (Network.IsError == null)
            {
                goto Label_0017;
            }
            code = Network.ErrCode;
            this.OnFailed();
            return;
        Label_0017:
            response = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json_PlayerDataAll>>(&www.text);
            DebugUtility.Assert((response == null) == 0, "res == null");
            if (response.body != null)
            {
                goto Label_0047;
            }
            this.OnFailed();
            return;
        Label_0047:
            try
            {
                MonoSingleton<GameManager>.Instance.Player.Deserialize(response.body.player);
                goto Label_007D;
            }
            catch (Exception exception1)
            {
            Label_0066:
                exception = exception1;
                DebugUtility.LogException(exception);
                this.OnFailed();
                goto Label_00A4;
            }
        Label_007D:
            Network.RemoveAPI();
            MyMetaps.TrackSpendCoin(((ColoResetTypes) this.ResetType).ToString(), this.getRequiredCoin());
            this.Success();
        Label_00A4:
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

