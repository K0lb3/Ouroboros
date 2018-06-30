namespace SRPG
{
    using GR;
    using System;

    [Pin(100, "ミッション進捗を取得完了", 1, 100), Pin(1, "ミッション進捗を取得", 0, 1), NodeType("Request/Tower/Floor/Mission", 0x7fe5)]
    public class FlowNode_ReqTowerFloorMission : FlowNode_Network
    {
        private const int INPUT_REQUEST_MISSION = 1;
        private const int OUTPUT_REQUEST_MISSION = 100;

        public FlowNode_ReqTowerFloorMission()
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
            base.ExecRequest(new ReqTowerFloorMission(GlobalVars.SelectedTowerID, GlobalVars.SelectedQuestID, new Network.ResponseCallback(this.ResponseCallback)));
        Label_002F:
            return;
        }

        public override unsafe void OnSuccess(WWWResult www)
        {
            WebAPI.JSON_BodyResponse<ReqTowerFloorMission.Json_Response> response;
            QuestParam param;
            ReqTowerFloorMission.Json_Response response2;
            int num;
            int num2;
            int num3;
            bool flag;
            Exception exception;
            if (TowerErrorHandle.Error(this) == null)
            {
                goto Label_000C;
            }
            return;
        Label_000C:
            response = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<ReqTowerFloorMission.Json_Response>>(&www.text);
            DebugUtility.Assert((response == null) == 0, "res == null");
            Network.RemoveAPI();
        Label_002F:
            try
            {
                param = MonoSingleton<GameManager>.Instance.FindQuest(GlobalVars.SelectedQuestID);
                if (param == null)
                {
                    goto Label_00CE;
                }
                response2 = response.body;
                num = 0;
                goto Label_0067;
            Label_0053:
                param.SetMissionFlag(num, 0);
                param.SetMissionValue(num, 0);
                num += 1;
            Label_0067:
                if (num < param.MissionNum)
                {
                    goto Label_0053;
                }
                if (response2 == null)
                {
                    goto Label_00CE;
                }
                if (response2.missions == null)
                {
                    goto Label_00CE;
                }
                num2 = 0;
                goto Label_00BF;
            Label_008C:
                num3 = response2.missions_val[num2];
                flag = response2.missions[num2] > 0;
                param.SetMissionFlag(num2, flag);
                param.SetMissionValue(num2, num3);
                num2 += 1;
            Label_00BF:
                if (num2 < ((int) response2.missions.Length))
                {
                    goto Label_008C;
                }
            Label_00CE:
                base.ActivateOutputLinks(100);
                goto Label_00EF;
            }
            catch (Exception exception1)
            {
            Label_00DC:
                exception = exception1;
                DebugUtility.LogException(exception);
                goto Label_00F6;
            }
        Label_00EF:
            base.set_enabled(0);
        Label_00F6:
            return;
        }
    }
}

