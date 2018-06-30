namespace SRPG
{
    using GR;
    using System;

    [NodeType("System/ReqTowerReset", 0x7fe5), Pin(1, "Success", 1, 10), Pin(0, "Request", 0, 0)]
    public class FlowNode_ReqTowerReset : FlowNode_Network
    {
        private long rtime;
        private byte round;

        public FlowNode_ReqTowerReset()
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
                goto Label_0048;
            }
            if (Network.Mode != null)
            {
                goto Label_0038;
            }
            base.ExecRequest(new ReqTowerReset(GlobalVars.SelectedTowerID, new Network.ResponseCallback(this.ResponseCallback)));
            base.set_enabled(1);
            goto Label_0048;
        Label_0038:
            GlobalVars.SelectedTowerID = "QE_TW_BABEL";
            this.Success();
        Label_0048:
            return;
        }

        public override unsafe void OnSuccess(WWWResult www)
        {
            TowerResuponse resuponse;
            WebAPI.JSON_BodyResponse<Json_ReqTowerReset> response;
            Json_PlayerData data;
            if (TowerErrorHandle.Error(this) == null)
            {
                goto Label_000C;
            }
            return;
        Label_000C:
            resuponse = MonoSingleton<GameManager>.Instance.TowerResuponse;
            response = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json_ReqTowerReset>>(&www.text);
            DebugUtility.Assert((response == null) == 0, "res == null");
            Network.RemoveAPI();
            this.rtime = response.body.rtime;
            data = new Json_PlayerData();
            data.coin = new Json_Coin();
            data.coin.free = response.body.coin.free;
            data.coin.paid = response.body.coin.paid;
            data.coin.com = response.body.coin.com;
            this.round = response.body.round;
            if (response.body.rank == null)
            {
                goto Label_0152;
            }
            resuponse.speedRank = response.body.rank.spd_rank;
            resuponse.techRank = response.body.rank.tec_rank;
            resuponse.spd_score = response.body.rank.spd_score;
            resuponse.tec_score = response.body.rank.tec_score;
            resuponse.ret_score = response.body.rank.ret_score;
            resuponse.rcv_score = response.body.rank.rcv_score;
        Label_0152:
            MonoSingleton<GameManager>.Instance.Player.Deserialize(data, 2);
            GameParameter.UpdateValuesOfType(7);
            GlobalVars.SelectedQuestID = MonoSingleton<GameManager>.Instance.FindFirstTowerFloor(MonoSingleton<GameManager>.Instance.TowerResuponse.TowerID).iname;
            this.Success();
            return;
        }

        private void Success()
        {
            TowerResuponse resuponse;
            JSON_ReqTowerResuponse resuponse2;
            resuponse = MonoSingleton<GameManager>.Instance.TowerResuponse;
            base.set_enabled(0);
            resuponse2 = new JSON_ReqTowerResuponse();
            resuponse2.is_reset = 0;
            resuponse2.round = this.round;
            resuponse2.rank = new JSON_ReqTowerResuponse.Json_RankStatus();
            if (resuponse2.rank == null)
            {
                goto Label_00A7;
            }
            resuponse2.rank.spd_rank = resuponse.speedRank;
            resuponse2.rank.tec_rank = resuponse.techRank;
            resuponse2.rank.spd_score = resuponse.spd_score;
            resuponse2.rank.tec_score = resuponse.tec_score;
            resuponse2.rank.ret_score = resuponse.ret_score;
            resuponse2.rank.rcv_score = resuponse.rcv_score;
        Label_00A7:
            MonoSingleton<GameManager>.Instance.Deserialize(resuponse2);
            MonoSingleton<GameManager>.Instance.TowerResuponse.rtime = this.rtime;
            base.ActivateOutputLinks(1);
            return;
        }

        public class Json_ReqTowerReset
        {
            public long rtime;
            public byte round;
            public JSON_ReqTowerResuponse.Json_RankStatus rank;
            public JSON_ReqTowerResuponse.Json_UserCoin coin;

            public Json_ReqTowerReset()
            {
                base..ctor();
                return;
            }
        }
    }
}

