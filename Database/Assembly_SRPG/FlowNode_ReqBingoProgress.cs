namespace SRPG
{
    using GR;
    using System;

    [Pin(1, "Success", 1, 1), NodeType("System/ReqBingoProgress", 0x7fe5), Pin(0, "Request", 0, 0)]
    public class FlowNode_ReqBingoProgress : FlowNode_Network
    {
        public FlowNode_ReqBingoProgress()
        {
            base..ctor();
            return;
        }

        public override void OnActivate(int pinID)
        {
            if (pinID == null)
            {
                goto Label_0007;
            }
            return;
        Label_0007:
            if (Network.Mode != 1)
            {
                goto Label_0024;
            }
            base.set_enabled(0);
            this.Success();
            goto Label_0042;
        Label_0024:
            base.ExecRequest(new ReqBingoProgress(new Network.ResponseCallback(this.ResponseCallback)));
            base.set_enabled(1);
        Label_0042:
            return;
        }

        public override unsafe void OnSuccess(WWWResult www)
        {
            WebAPI.JSON_BodyResponse<JSON_BingoResponse> response;
            GameManager manager;
            int num;
            JSON_TrophyProgress progress;
            TrophyParam param;
            TrophyState state;
            int num2;
            Network.EErrCode code;
            if (Network.IsError == null)
            {
                goto Label_0018;
            }
            code = Network.ErrCode;
            this.OnRetry();
            return;
        Label_0018:
            response = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<JSON_BingoResponse>>(&www.text);
            DebugUtility.Assert((response == null) == 0, "res == null");
            if (response.body != null)
            {
                goto Label_0048;
            }
            this.OnRetry();
            return;
        Label_0048:
            if (response.body.bingoprogs != null)
            {
                goto Label_0064;
            }
            Network.RemoveAPI();
            this.Success();
            return;
        Label_0064:
            manager = MonoSingleton<GameManager>.Instance;
            num = 0;
            goto Label_0141;
        Label_0071:
            progress = response.body.bingoprogs[num];
            if (progress != null)
            {
                goto Label_008A;
            }
            goto Label_013D;
        Label_008A:
            if (manager.MasterParam.GetTrophy(progress.iname) != null)
            {
                goto Label_00BE;
            }
            DebugUtility.LogError("存在しないミッション:" + progress.iname);
            goto Label_013D;
        Label_00BE:
            state = manager.Player.GetTrophyCounter(manager.MasterParam.GetTrophy(progress.iname), 0);
            num2 = 0;
            goto Label_00FE;
        Label_00E5:
            state.Count[num2] = progress.pts[num2];
            num2 += 1;
        Label_00FE:
            if (num2 >= ((int) progress.pts.Length))
            {
                goto Label_011D;
            }
            if (num2 < ((int) state.Count.Length))
            {
                goto Label_00E5;
            }
        Label_011D:
            state.StartYMD = progress.ymd;
            state.IsEnded = (progress.rewarded_at == 0) == 0;
        Label_013D:
            num += 1;
        Label_0141:
            if (num < ((int) response.body.bingoprogs.Length))
            {
                goto Label_0071;
            }
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

        public class JSON_BingoResponse
        {
            public JSON_TrophyProgress[] bingoprogs;

            public JSON_BingoResponse()
            {
                base..ctor();
                return;
            }
        }
    }
}

