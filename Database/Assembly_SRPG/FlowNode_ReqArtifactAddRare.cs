namespace SRPG
{
    using GR;
    using System;

    [NodeType("System/ReqArtifactAddRare", 0x7fe5), Pin(0, "Request", 0, 0), Pin(1, "Success", 1, 1)]
    public class FlowNode_ReqArtifactAddRare : FlowNode_Network
    {
        public FlowNode_ReqArtifactAddRare()
        {
            base..ctor();
            return;
        }

        public override void OnActivate(int pinID)
        {
            long num;
            if (pinID != null)
            {
                goto Label_0061;
            }
            if (Network.Mode != null)
            {
                goto Label_005B;
            }
            num = GlobalVars.SelectedArtifactUniqueID;
            if (num >= 1L)
            {
                goto Label_0035;
            }
            base.set_enabled(0);
            this.Success();
            goto Label_0056;
        Label_0035:
            base.set_enabled(1);
            base.ExecRequest(new ReqArtifactAddRare(num, new Network.ResponseCallback(this.ResponseCallback), null, null));
        Label_0056:
            goto Label_0061;
        Label_005B:
            this.Success();
        Label_0061:
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
                MonoSingleton<GameManager>.Instance.Deserialize(response.body.items);
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

