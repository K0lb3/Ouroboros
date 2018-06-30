namespace SRPG
{
    using GR;
    using System;

    [NodeType("Multi/ReqRankMatchReward", 0x7fe5), Pin(0x1388, "NoMatchVersion", 1, 0x1388), Pin(0x1770, "MultiMaintenance", 1, 0x1770), Pin(1, "Yes", 1, 1), Pin(0, "Req", 0, 0), Pin(2, "No", 1, 2)]
    public class FlowNode_ReqRankMatchReward : FlowNode_Network
    {
        private bool isChecked;
        private ReqType mReqType;
        private int mToDay;

        public FlowNode_ReqRankMatchReward()
        {
            base..ctor();
            return;
        }

        public override unsafe void OnActivate(int pinID)
        {
            int num;
            DateTime time;
            DateTime time2;
            DateTime time3;
            if (pinID != null)
            {
                goto Label_0090;
            }
            if (this.isChecked == null)
            {
                goto Label_0012;
            }
            return;
        Label_0012:
            this.isChecked = 1;
            this.mToDay = ((&TimeManager.ServerTime.Year * 0x2710) + (&TimeManager.ServerTime.Month * 100)) + &TimeManager.ServerTime.Day;
            if (PlayerPrefsUtility.GetInt(PlayerPrefsUtility.RANKMATCH_SEASON_REWARD_RECEIVE_DATE, 0) < this.mToDay)
            {
                goto Label_0072;
            }
            base.ActivateOutputLinks(2);
            return;
        Label_0072:
            this.mReqType = 1;
            base.ExecRequest(new ReqRankMatchReward(new Network.ResponseCallback(this.ResponseCallback)));
        Label_0090:
            return;
        }

        public override unsafe void OnSuccess(WWWResult www)
        {
            WebAPI.JSON_BodyResponse<ReqRankMatchStatus.Response> response;
            WebAPI.JSON_BodyResponse<ReqRankMatchReward.Response> response2;
            GameManager manager;
            DebugUtility.Log("OnSuccess");
            if (Network.IsError == null)
            {
                goto Label_00AC;
            }
            if (Network.ErrCode == 0xe78)
            {
                goto Label_0032;
            }
            if (Network.ErrCode != 0x2718)
            {
                goto Label_0050;
            }
        Label_0032:
            Network.RemoveAPI();
            Network.ResetError();
            base.set_enabled(0);
            base.ActivateOutputLinks(0x1388);
            return;
        Label_0050:
            if (Network.ErrCode == 0xca)
            {
                goto Label_008C;
            }
            if (Network.ErrCode == 0xcb)
            {
                goto Label_008C;
            }
            if (Network.ErrCode == 0xce)
            {
                goto Label_008C;
            }
            if (Network.ErrCode != 0xcd)
            {
                goto Label_00A5;
            }
        Label_008C:
            Network.RemoveAPI();
            base.set_enabled(0);
            base.ActivateOutputLinks(0x1770);
            return;
        Label_00A5:
            this.OnFailed();
            return;
        Label_00AC:
            if (this.mReqType != null)
            {
                goto Label_0134;
            }
            response = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<ReqRankMatchStatus.Response>>(&www.text);
            DebugUtility.Assert((response == null) == 0, "res == null");
            if (response.body != null)
            {
                goto Label_00E7;
            }
            this.OnFailed();
            return;
        Label_00E7:
            Network.RemoveAPI();
            base.set_enabled(0);
            if (response.body.RankingStatus != 2)
            {
                goto Label_0127;
            }
            this.mReqType = 1;
            base.ExecRequest(new ReqRankMatchReward(new Network.ResponseCallback(this.ResponseCallback)));
            goto Label_012F;
        Label_0127:
            base.ActivateOutputLinks(2);
        Label_012F:
            goto Label_0219;
        Label_0134:
            if (this.mReqType != 1)
            {
                goto Label_0219;
            }
            response2 = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<ReqRankMatchReward.Response>>(&www.text);
            DebugUtility.Assert((response2 == null) == 0, "res == null");
            if (response2.body != null)
            {
                goto Label_0170;
            }
            this.OnFailed();
            return;
        Label_0170:
            Network.RemoveAPI();
            base.set_enabled(0);
            PlayerPrefsUtility.SetInt(PlayerPrefsUtility.RANKMATCH_SEASON_REWARD_RECEIVE_DATE, this.mToDay, 0);
            if (response2.body.rank != null)
            {
                goto Label_01AB;
            }
            base.ActivateOutputLinks(2);
            goto Label_0219;
        Label_01AB:
            manager = MonoSingleton<GameManager>.Instance;
            manager.Player.mRankMatchSeasonResult.Deserialize(response2.body);
            GlobalVars.RankMatchSeasonReward.Clear();
            GlobalVars.RankMatchSeasonReward.Add(manager.GetVersusRankClassRewardList(response2.body.reward.ranking));
            GlobalVars.RankMatchSeasonReward.Add(manager.GetVersusRankClassRewardList(response2.body.reward.type));
            base.ActivateOutputLinks(1);
        Label_0219:
            return;
        }

        private enum ReqType
        {
            Status,
            Reward
        }
    }
}

