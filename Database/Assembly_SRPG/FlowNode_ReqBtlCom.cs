namespace SRPG
{
    using GR;
    using System;

    [Pin(100, "Start", 0, 0), Pin(2, "Reset to Title", 1, 11), NodeType("System/ReqBtlCom", 0x7fe5), Pin(1, "Success", 1, 10)]
    public class FlowNode_ReqBtlCom : FlowNode_Network
    {
        public bool FastRefresh;
        public bool GetTowerProgress;

        public FlowNode_ReqBtlCom()
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
            if (pinID != 100)
            {
                goto Label_0047;
            }
            if (Network.Mode != null)
            {
                goto Label_0041;
            }
            base.ExecRequest(new ReqBtlCom(new Network.ResponseCallback(this.ResponseCallback), this.FastRefresh, this.GetTowerProgress));
            base.set_enabled(1);
            goto Label_0047;
        Label_0041:
            this.Success();
        Label_0047:
            return;
        }

        public override unsafe void OnSuccess(WWWResult www)
        {
            WebAPI.JSON_BodyResponse<JSON_ReqBtlComResponse> response;
            GameManager manager;
            int num;
            JSON_ReqTowerResuponse.Json_TowerProg prog;
            TowerParam param;
            Network.EErrCode code;
            if (Network.IsError == null)
            {
                goto Label_0018;
            }
            code = Network.ErrCode;
            this.OnRetry();
            return;
        Label_0018:
            response = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<JSON_ReqBtlComResponse>>(&www.text);
            DebugUtility.Assert((response == null) == 0, "res == null");
            Network.RemoveAPI();
            manager = MonoSingleton<GameManager>.Instance;
            manager.Player.SetQuestListDirty();
            manager.ResetJigenQuests();
            if (manager.Deserialize(response.body.quests) != null)
            {
                goto Label_006F;
            }
            this.Failure();
            return;
        Label_006F:
            if (response.body.towers == null)
            {
                goto Label_00D5;
            }
            num = 0;
            goto Label_00C2;
        Label_0086:
            prog = response.body.towers[num];
            param = manager.FindTower(prog.iname);
            if (param != null)
            {
                goto Label_00AE;
            }
            goto Label_00BE;
        Label_00AE:
            param.is_unlock = prog.is_open == 1;
        Label_00BE:
            num += 1;
        Label_00C2:
            if (num < ((int) response.body.towers.Length))
            {
                goto Label_0086;
            }
        Label_00D5:
            this.Success();
            return;
        }

        private void Success()
        {
            base.set_enabled(0);
            base.ActivateOutputLinks(1);
            return;
        }

        public class JSON_ReqBtlComResponse
        {
            public JSON_QuestProgress[] quests;
            public JSON_ReqTowerResuponse.Json_TowerProg[] towers;

            public JSON_ReqBtlComResponse()
            {
                base..ctor();
                return;
            }
        }
    }
}

