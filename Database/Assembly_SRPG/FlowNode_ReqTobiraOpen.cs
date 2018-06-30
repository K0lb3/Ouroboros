namespace SRPG
{
    using GR;
    using System;

    [Pin(0, "扉を解放する", 0, 0), NodeType("System/ReqTobiraOpen"), Pin(100, "扉を解放した", 1, 100)]
    public class FlowNode_ReqTobiraOpen : FlowNode_Network
    {
        public const int INPUT_REQUEST = 0;
        public const int OUTPUT_REQUEST = 100;

        public FlowNode_ReqTobiraOpen()
        {
            base..ctor();
            return;
        }

        public override void OnActivate(int pinID)
        {
            if (pinID != null)
            {
                goto Label_0038;
            }
            base.set_enabled(1);
            base.ExecRequest(new ReqTobiraOpen(GlobalVars.SelectedUnitUniqueID, GlobalVars.PreBattleUnitTobiraCategory, new Network.ResponseCallback(this.TobiraOpenCallback)));
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
                goto Label_00AF;
            }
        Label_008F:
            base.set_enabled(0);
            MonoSingleton<GameManager>.Instance.Player.OnOpenTobiraTrophy(GlobalVars.SelectedUnitUniqueID);
        Label_00AF:
            return;
        }

        private void TobiraOpenCallback(WWWResult www)
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

