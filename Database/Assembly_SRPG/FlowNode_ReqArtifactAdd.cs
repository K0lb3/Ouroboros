namespace SRPG
{
    using GR;
    using System;

    [Pin(0, "Request", 0, 0), NodeType("System/ReqArtifactAdd", 0x7fe5), Pin(1, "Success", 1, 1)]
    public class FlowNode_ReqArtifactAdd : FlowNode_Network
    {
        public FlowNode_ReqArtifactAdd()
        {
            base..ctor();
            return;
        }

        public override void OnActivate(int pinID)
        {
            string str;
            if (pinID != null)
            {
                goto Label_0064;
            }
            str = GlobalVars.SelectedArtifactIname;
            if (Network.Mode != null)
            {
                goto Label_005E;
            }
            if (string.IsNullOrEmpty(str) == null)
            {
                goto Label_0038;
            }
            base.set_enabled(0);
            this.Success();
            goto Label_0059;
        Label_0038:
            base.set_enabled(1);
            base.ExecRequest(new ReqArtifactAdd(str, new Network.ResponseCallback(this.ResponseCallback), null, null));
        Label_0059:
            goto Label_0064;
        Label_005E:
            this.Success();
        Label_0064:
            return;
        }

        public override unsafe void OnSuccess(WWWResult www)
        {
            WebAPI.JSON_BodyResponse<Json_PlayerDataAll> response;
            Exception exception;
            Network.EErrCode code;
            if (Network.IsError == null)
            {
                goto Label_0040;
            }
            code = Network.ErrCode;
            if (code == 0x2328)
            {
                goto Label_002B;
            }
            if (code == 0x2329)
            {
                goto Label_0032;
            }
            goto Label_0039;
        Label_002B:
            this.OnBack();
            return;
        Label_0032:
            this.OnBack();
            return;
        Label_0039:
            this.OnRetry();
            return;
        Label_0040:
            response = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json_PlayerDataAll>>(&www.text);
            DebugUtility.Assert((response == null) == 0, "res == null");
            if (response.body != null)
            {
                goto Label_0070;
            }
            this.OnRetry();
            return;
        Label_0070:
            try
            {
                MonoSingleton<GameManager>.Instance.Deserialize(response.body.player);
                MonoSingleton<GameManager>.Instance.Deserialize(response.body.items);
                MonoSingleton<GameManager>.Instance.Deserialize(response.body.artifacts, 0);
                goto Label_00CC;
            }
            catch (Exception exception1)
            {
            Label_00B5:
                exception = exception1;
                DebugUtility.LogException(exception);
                this.OnRetry();
                goto Label_00D7;
            }
        Label_00CC:
            Network.RemoveAPI();
            this.Success();
        Label_00D7:
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

