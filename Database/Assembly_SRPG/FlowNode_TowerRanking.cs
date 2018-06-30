namespace SRPG
{
    using GR;
    using System;

    [Pin(0xc9, "Error", 1, 0xc9), Pin(200, "Finish", 1, 200), Pin(100, "Request", 0, 100), NodeType("System/TowerRank", 0x7fe5)]
    public class FlowNode_TowerRanking : FlowNode_Network
    {
        public FlowNode_TowerRanking()
        {
            base..ctor();
            return;
        }

        private void Failure()
        {
            base.set_enabled(0);
            base.ActivateOutputLinks(0xc9);
            return;
        }

        public override void OnActivate(int pinID)
        {
            if (pinID != 100)
            {
                goto Label_002B;
            }
            base.set_enabled(1);
            base.ExecRequest(new ReqTowerRank(GlobalVars.SelectedTowerID, new Network.ResponseCallback(this.ResponseCallback)));
        Label_002B:
            return;
        }

        public override unsafe void OnSuccess(WWWResult www)
        {
            TowerResuponse resuponse;
            WebAPI.JSON_BodyResponse<ReqTowerRank.JSON_TowerRankResponse> response;
            if (TowerErrorHandle.Error(this) == null)
            {
                goto Label_000C;
            }
            return;
        Label_000C:
            resuponse = MonoSingleton<GameManager>.Instance.TowerResuponse;
            response = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<ReqTowerRank.JSON_TowerRankResponse>>(&www.text);
            DebugUtility.Assert((response == null) == 0, "res == null");
            Network.RemoveAPI();
            resuponse.Deserialize(response.body);
            this.Success();
            return;
        }

        private void Success()
        {
            base.set_enabled(0);
            base.ActivateOutputLinks(200);
            return;
        }
    }
}

