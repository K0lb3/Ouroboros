namespace SRPG
{
    using GR;
    using System;

    [Pin(1, "Quest", 0, 1), NodeType("System/ReqRanking", 0x7fe5), Pin(0, "Request", 0, 0), Pin(100, "Success", 1, 100), Pin(3, "TowerMuch", 0, 3), Pin(2, "Arena", 0, 2)]
    public class FlowNode_ReqRanking : FlowNode_Network
    {
        public FlowNode_ReqRanking()
        {
            base..ctor();
            return;
        }

        public override void OnActivate(int pinID)
        {
            UsageRateRanking ranking;
            int num;
            ranking = base.get_gameObject().GetComponent<UsageRateRanking>();
            num = pinID;
            switch ((num - 1))
            {
                case 0:
                    goto Label_0027;

                case 1:
                    goto Label_0033;

                case 2:
                    goto Label_003F;
            }
            goto Label_004B;
        Label_0027:
            ranking.OnChangedToggle(0);
            goto Label_004B;
        Label_0033:
            ranking.OnChangedToggle(1);
            goto Label_004B;
        Label_003F:
            ranking.OnChangedToggle(2);
        Label_004B:
            pinID = ((pinID <= 0) || (pinID > 3)) ? pinID : 0;
            if (pinID != null)
            {
                goto Label_008B;
            }
            base.set_enabled(1);
            base.ExecRequest(new ReqRanking(UsageRateRanking.ViewInfo, new Network.ResponseCallback(this.ResponseCallback)));
        Label_008B:
            return;
        }

        public override unsafe void OnSuccess(WWWResult www)
        {
            WebAPI.JSON_BodyResponse<RankingData[]> response;
            response = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<RankingData[]>>(&www.text);
            DebugUtility.Assert((response == null) == 0, "res == null");
            Network.RemoveAPI();
            MonoSingleton<GameManager>.Instance.Deserialize(response.body);
            base.ActivateOutputLinks(100);
            return;
        }
    }
}

