namespace SRPG
{
    using GR;
    using System;

    [Pin(0, "扉を強化する", 0, 0), Pin(100, "扉を強化した", 1, 100), NodeType("System/ReqTobiraEnhance", 0x7fe5)]
    public class FlowNode_ReqTobiraEnhance : FlowNode_Network
    {
        private const int INPUT_REQUEST = 0;
        private const int OUTPUT_REQUEST = 100;

        public FlowNode_ReqTobiraEnhance()
        {
            base..ctor();
            return;
        }

        private void EnhanceTobiraCallback(WWWResult www)
        {
            if (FlowNode_Network.HasCommonError(www) != null)
            {
                goto Label_001B;
            }
            this.OnSuccess(www);
            base.ActivateOutputLinks(100);
        Label_001B:
            return;
        }

        public override void OnActivate(int pinID)
        {
            if (pinID != null)
            {
                goto Label_0038;
            }
            base.set_enabled(1);
            base.ExecRequest(new ReqTobiraEnhance(GlobalVars.SelectedUnitUniqueID, GlobalVars.PreBattleUnitTobiraCategory, new Network.ResponseCallback(this.EnhanceTobiraCallback)));
        Label_0038:
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
            Network.RemoveAPI();
        Label_003A:
            try
            {
                MonoSingleton<GameManager>.Instance.Deserialize(response.body.player);
                MonoSingleton<GameManager>.Instance.Deserialize(response.body.units);
                MonoSingleton<GameManager>.Instance.Deserialize(response.body.items);
                goto Label_008F;
            }
            catch (Exception exception1)
            {
            Label_007E:
                exception = exception1;
                DebugUtility.LogException(exception);
                goto Label_009C;
            }
        Label_008F:
            GameParameter.UpdateValuesOfType(6);
            base.set_enabled(0);
        Label_009C:
            return;
        }
    }
}

