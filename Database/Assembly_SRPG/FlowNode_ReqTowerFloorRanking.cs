namespace SRPG
{
    using GR;
    using System;

    [Pin(100, "ランキングを取得完了", 1, 100), NodeType("Request/Tower/Floor/Ranking", 0x7fe5), Pin(1, "この階層のランキングを取得", 0, 1)]
    public class FlowNode_ReqTowerFloorRanking : FlowNode_Network
    {
        private const int INPUT_REQUEST_RANKING = 1;
        private const int OUTPUT_REQUEST_RANKING = 100;

        public FlowNode_ReqTowerFloorRanking()
        {
            base..ctor();
            return;
        }

        public override void OnActivate(int pinID)
        {
            if (pinID != 1)
            {
                goto Label_002F;
            }
            base.set_enabled(1);
            base.ExecRequest(new ReqTowerFloorRanking(GlobalVars.SelectedTowerID, GlobalVars.SelectedQuestID, new Network.ResponseCallback(this.ResponseCallback)));
        Label_002F:
            return;
        }

        public override unsafe void OnSuccess(WWWResult www)
        {
            WebAPI.JSON_BodyResponse<ReqTowerFloorRanking.Json_Response> response;
            ReqTowerFloorRanking.Json_Response response2;
            Exception exception;
            if (TowerErrorHandle.Error(this) == null)
            {
                goto Label_000C;
            }
            return;
        Label_000C:
            response = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<ReqTowerFloorRanking.Json_Response>>(&www.text);
            DebugUtility.Assert((response == null) == 0, "res == null");
            Network.RemoveAPI();
        Label_002F:
            try
            {
                response2 = response.body;
                MonoSingleton<GameManager>.Instance.TowerResuponse.OnFloorRanking(response2);
                base.ActivateOutputLinks(100);
                goto Label_0065;
            }
            catch (Exception exception1)
            {
            Label_0054:
                exception = exception1;
                DebugUtility.LogException(exception);
                goto Label_006C;
            }
        Label_0065:
            base.set_enabled(0);
        Label_006C:
            return;
        }
    }
}

