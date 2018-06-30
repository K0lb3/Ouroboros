namespace SRPG
{
    using GR;
    using System;

    [Pin(0, "Request", 0, 0), NodeType("System/ReqMasterParam", 0x7fe5), Pin(2, "Failed", 1, 2), Pin(1, "Success", 1, 1)]
    public class FlowNode_ReqMasterParam : FlowNode_Network
    {
        public FlowNode_ReqMasterParam()
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
                goto Label_006F;
            }
            if (GameUtility.Config_UseLocalData.Value == null)
            {
                goto Label_002D;
            }
            MonoSingleton<GameManager>.Instance.ReloadMasterData(null, null);
            this.Success();
            goto Label_006F;
        Label_002D:
            if (Network.Mode != null)
            {
                goto Label_0069;
            }
            if (GameUtility.Config_UseAssetBundles.Value != null)
            {
                goto Label_0069;
            }
            base.ExecRequest(new ReqMasterParam(new Network.ResponseCallback(this.ResponseCallback)));
            base.set_enabled(1);
            goto Label_006F;
        Label_0069:
            this.Success();
        Label_006F:
            return;
        }

        public override unsafe void OnSuccess(WWWResult www)
        {
            WebAPI.JSON_BodyResponse<JSON_MasterParam> response;
            Network.EErrCode code;
            if (Network.IsError == null)
            {
                goto Label_0017;
            }
            code = Network.ErrCode;
            this.OnFailed();
            return;
        Label_0017:
            response = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<JSON_MasterParam>>(&www.text);
            DebugUtility.Assert((response == null) == 0, "res == null");
            Network.RemoveAPI();
            if (MonoSingleton<GameManager>.Instance.Deserialize(response.body) != null)
            {
                goto Label_0056;
            }
            this.Failure();
            return;
        Label_0056:
            MonoSingleton<GameManager>.Instance.MasterParam.DumpLoadedLog();
            this.Success();
            response = null;
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

