namespace SRPG
{
    using GR;
    using System;

    [Pin(100, "リセット完了", 1, 100), NodeType("Request/Tower/Floor/Reset", 0x7fe5), Pin(1, "敵の引継ぎ情報リセット", 0, 1)]
    public class FlowNode_ReqTowerFloorReset : FlowNode_Network
    {
        private const int INPUT_REQUEST_RESET = 1;
        private const int OUTPUT_REQUEST_RESET = 100;

        public FlowNode_ReqTowerFloorReset()
        {
            base..ctor();
            return;
        }

        public override void OnActivate(int pinID)
        {
            TowerResuponse resuponse;
            TowerFloorParam param;
            if (pinID != 1)
            {
                goto Label_0050;
            }
            resuponse = MonoSingleton<GameManager>.Instance.TowerResuponse;
            if (resuponse != null)
            {
                goto Label_0019;
            }
            return;
        Label_0019:
            param = resuponse.GetCurrentFloor();
            if (param != null)
            {
                goto Label_0027;
            }
            return;
        Label_0027:
            base.set_enabled(1);
            base.ExecRequest(new ReqTowerFloorReset(GlobalVars.SelectedTowerID, param.iname, new Network.ResponseCallback(this.ResponseCallback)));
        Label_0050:
            return;
        }

        public override unsafe void OnSuccess(WWWResult www)
        {
            WebAPI.JSON_BodyResponse<ReqTowerFloorReset.Json_Response> response;
            Exception exception;
            if (TowerErrorHandle.Error(this) == null)
            {
                goto Label_000C;
            }
            return;
        Label_000C:
            response = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<ReqTowerFloorReset.Json_Response>>(&www.text);
            DebugUtility.Assert((response == null) == 0, "res == null");
            Network.RemoveAPI();
        Label_002F:
            try
            {
                MonoSingleton<GameManager>.Instance.Player.SetTowerFloorResetCoin(response.body);
                MonoSingleton<GameManager>.Instance.TowerResuponse.OnFloorReset();
                base.ActivateOutputLinks(100);
                goto Label_0072;
            }
            catch (Exception exception1)
            {
            Label_0061:
                exception = exception1;
                DebugUtility.LogException(exception);
                goto Label_0079;
            }
        Label_0072:
            base.set_enabled(0);
        Label_0079:
            return;
        }
    }
}

