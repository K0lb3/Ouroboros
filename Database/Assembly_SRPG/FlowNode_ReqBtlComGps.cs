namespace SRPG
{
    using GR;
    using System;

    [Pin(1, "Success", 1, 10), Pin(3, "エリアクエスト無し", 1, 12), NodeType("System/ReqBtlComGps", 0x7fe5), Pin(100, "Start", 0, 0), Pin(200, "StartMulti", 0, 0), Pin(2, "Reset to Title", 1, 11)]
    public class FlowNode_ReqBtlComGps : FlowNode_Network
    {
        public FlowNode_ReqBtlComGps()
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
                goto Label_0046;
            }
            if (Network.Mode != null)
            {
                goto Label_003B;
            }
            base.ExecRequest(new ReqBtlComGps(new Network.ResponseCallback(this.ResponseCallback), GlobalVars.Location, 0));
            base.set_enabled(1);
            goto Label_0041;
        Label_003B:
            this.SuccessNotQuest();
        Label_0041:
            goto Label_008A;
        Label_0046:
            if (pinID != 200)
            {
                goto Label_008A;
            }
            if (Network.Mode != null)
            {
                goto Label_0084;
            }
            base.ExecRequest(new ReqBtlComGps(new Network.ResponseCallback(this.ResponseCallback), GlobalVars.Location, 1));
            base.set_enabled(1);
            goto Label_008A;
        Label_0084:
            this.SuccessNotQuest();
        Label_008A:
            return;
        }

        public override unsafe void OnSuccess(WWWResult www)
        {
            WebAPI.JSON_BodyResponse<JSON_ReqBtlComGpsResponse> response;
            GameManager manager;
            Network.EErrCode code;
            if (Network.IsError == null)
            {
                goto Label_0038;
            }
            if (Network.ErrCode == 0xcec)
            {
                goto Label_0020;
            }
            goto Label_0031;
        Label_0020:
            Network.RemoveAPI();
            Network.ResetError();
            this.SuccessNotQuest();
            return;
        Label_0031:
            this.OnRetry();
            return;
        Label_0038:
            response = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<JSON_ReqBtlComGpsResponse>>(&www.text);
            DebugUtility.Assert((response == null) == 0, "res == null");
            Network.RemoveAPI();
            manager = MonoSingleton<GameManager>.Instance;
            manager.ResetGpsQuests();
            if (response.body.quests == null)
            {
                goto Label_0089;
            }
            if (((int) response.body.quests.Length) != null)
            {
                goto Label_0090;
            }
        Label_0089:
            this.SuccessNotQuest();
            return;
        Label_0090:
            if (manager.DeserializeGps(response.body.quests) != null)
            {
                goto Label_00AD;
            }
            this.Failure();
            return;
        Label_00AD:
            this.Success();
            return;
        }

        private void Success()
        {
            base.set_enabled(0);
            base.ActivateOutputLinks(1);
            return;
        }

        private void SuccessNotQuest()
        {
            base.set_enabled(0);
            base.ActivateOutputLinks(3);
            return;
        }

        public class JSON_ReqBtlComGpsResponse
        {
            public JSON_QuestProgress[] quests;

            public JSON_ReqBtlComGpsResponse()
            {
                base..ctor();
                return;
            }
        }
    }
}

