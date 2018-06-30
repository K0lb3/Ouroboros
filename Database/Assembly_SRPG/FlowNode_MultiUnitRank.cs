namespace SRPG
{
    using GR;
    using System;

    [Pin(100, "Request", 0, 100), NodeType("Multi/Ranking", 0x7fe5), Pin(200, "Finish", 1, 200), Pin(0xc9, "Error", 1, 0xc9)]
    public class FlowNode_MultiUnitRank : FlowNode_Network
    {
        public FlowNode_MultiUnitRank()
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
            base.ExecRequest(new ReqMultiRank(GlobalVars.SelectedQuestID, new Network.ResponseCallback(this.ResponseCallback)));
        Label_002B:
            return;
        }

        public override unsafe void OnSuccess(WWWResult www)
        {
            WebAPI.JSON_BodyResponse<ReqMultiRank.Json_MultiRank> response;
            GameManager manager;
            Network.EErrCode code;
            if (Network.IsError == null)
            {
                goto Label_001C;
            }
            code = Network.ErrCode;
            Network.RemoveAPI();
            this.Failure();
            return;
        Label_001C:
            response = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<ReqMultiRank.Json_MultiRank>>(&www.text);
            DebugUtility.Assert((response == null) == 0, "res is null");
            Network.RemoveAPI();
            MonoSingleton<GameManager>.Instance.Deserialize(response.body);
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

