namespace SRPG
{
    using GR;
    using System;

    [Pin(0, "扉機能を解放する", 0, 0), NodeType("System/ReqTobiraUnlock"), Pin(100, "扉機能を解放した", 1, 100)]
    public class FlowNode_ReqTobiraUnlock : FlowNode_Network
    {
        public const int INPUT_REQUEST = 0;
        public const int OUTPUT_REQUEST = 100;

        public FlowNode_ReqTobiraUnlock()
        {
            base..ctor();
            return;
        }

        public override void OnActivate(int pinID)
        {
            if (pinID != null)
            {
                goto Label_002E;
            }
            base.set_enabled(1);
            base.ExecRequest(new ReqTobiraUnlock(GlobalVars.SelectedUnitUniqueID, new Network.ResponseCallback(this.TobiraUnlockCallback)));
        Label_002E:
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
                goto Label_0096;
            }
        Label_008F:
            base.set_enabled(0);
        Label_0096:
            return;
        }

        private void TobiraUnlockCallback(WWWResult www)
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
    }
}

