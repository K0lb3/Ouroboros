namespace SRPG
{
    using GR;
    using System;

    [NodeType("System/ReqArtifactSell", 0x7fe5), Pin(1, "Success", 1, 1), Pin(0, "Request", 0, 0)]
    public class FlowNode_ReqArtifactSell : FlowNode_Network
    {
        public FlowNode_ReqArtifactSell()
        {
            base..ctor();
            return;
        }

        public override void OnActivate(int pinID)
        {
            long[] numArray;
            if (pinID != null)
            {
                goto Label_0060;
            }
            if (Network.Mode != null)
            {
                goto Label_005A;
            }
            numArray = GlobalVars.SellArtifactsList.ToArray();
            if (((int) numArray.Length) >= 1)
            {
                goto Label_0036;
            }
            base.set_enabled(0);
            this.Success();
            goto Label_0055;
        Label_0036:
            base.set_enabled(1);
            base.ExecRequest(new ReqArtifactSell(numArray, new Network.ResponseCallback(this.ResponseCallback)));
        Label_0055:
            goto Label_0060;
        Label_005A:
            this.Success();
        Label_0060:
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
            this.OnRetry();
            return;
        Label_0017:
            response = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json_PlayerDataAll>>(&www.text);
            DebugUtility.Assert((response == null) == 0, "res == null");
            if (response.body != null)
            {
                goto Label_0047;
            }
            this.OnRetry();
            return;
        Label_0047:
            try
            {
                MonoSingleton<GameManager>.Instance.Deserialize(response.body.player);
                MonoSingleton<GameManager>.Instance.Deserialize(response.body.units);
                MonoSingleton<GameManager>.Instance.Deserialize(response.body.artifacts, 0);
                goto Label_00A3;
            }
            catch (Exception exception1)
            {
            Label_008C:
                exception = exception1;
                DebugUtility.LogException(exception);
                this.OnRetry();
                goto Label_00AE;
            }
        Label_00A3:
            Network.RemoveAPI();
            this.Success();
        Label_00AE:
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

