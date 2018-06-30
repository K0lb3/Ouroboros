namespace SRPG
{
    using GR;
    using System;

    [NodeType("Network/btl_colo_ranking"), Pin(0, "Request", 0, 0), Pin(1, "Success", 1, 1)]
    public class FlowNode_ReqArenaRanking : FlowNode_Network
    {
        public ReqBtlColoRanking.RankingTypes RankingType;

        public FlowNode_ReqArenaRanking()
        {
            base..ctor();
            return;
        }

        public override void OnActivate(int pinID)
        {
            if (pinID != null)
            {
                goto Label_003C;
            }
            if (Network.Mode != 1)
            {
                goto Label_0018;
            }
            this.Success();
            return;
        Label_0018:
            base.ExecRequest(new ReqBtlColoRanking(this.RankingType, new Network.ResponseCallback(this.ResponseCallback)));
            base.set_enabled(1);
        Label_003C:
            return;
        }

        public override unsafe void OnSuccess(WWWResult www)
        {
            WebAPI.JSON_BodyResponse<JSON_ArenaRanking> response;
            GameManager manager;
            Network.EErrCode code;
            if (Network.IsError == null)
            {
                goto Label_0017;
            }
            code = Network.ErrCode;
            this.OnFailed();
            return;
        Label_0017:
            response = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<JSON_ArenaRanking>>(&www.text);
            DebugUtility.Assert((response == null) == 0, "res == null");
            if (response.body != null)
            {
                goto Label_0047;
            }
            this.OnFailed();
            return;
        Label_0047:
            if (MonoSingleton<GameManager>.Instance.Deserialize(response.body, this.RankingType) != null)
            {
                goto Label_006B;
            }
            this.OnFailed();
            return;
        Label_006B:
            Network.RemoveAPI();
            this.Success();
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

