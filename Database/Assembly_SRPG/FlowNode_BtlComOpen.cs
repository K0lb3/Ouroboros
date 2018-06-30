namespace SRPG
{
    using GR;
    using System;

    [NodeType("System/BtlComOpen", 0x7fe5), Pin(100, "Success", 1, 10), Pin(1, "Request", 0, 0), Pin(0x65, "Failed", 1, 11)]
    public class FlowNode_BtlComOpen : FlowNode_Network
    {
        public FlowNode_BtlComOpen()
        {
            base..ctor();
            return;
        }

        private void Failure()
        {
            base.set_enabled(0);
            base.ActivateOutputLinks(0x65);
            return;
        }

        public override void OnActivate(int pinID)
        {
            string str;
            if (pinID != 1)
            {
                goto Label_004E;
            }
            base.set_enabled(0);
            str = GlobalVars.SelectedChapter;
            if (Network.Mode != 1)
            {
                goto Label_002F;
            }
            this.Success();
            goto Label_004E;
        Label_002F:
            base.ExecRequest(new ReqBtlComOpen(str, new Network.ResponseCallback(this.ResponseCallback)));
            base.set_enabled(1);
        Label_004E:
            return;
        }

        public override unsafe void OnSuccess(WWWResult www)
        {
            WebAPI.JSON_BodyResponse<JSON_BtlComOpenResponse> response;
            GameManager manager;
            Network.EErrCode code;
            if (Network.IsError == null)
            {
                goto Label_0017;
            }
            code = Network.ErrCode;
            this.OnRetry();
            return;
        Label_0017:
            response = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<JSON_BtlComOpenResponse>>(&www.text);
            DebugUtility.Assert((response == null) == 0, "res == null");
            Network.RemoveAPI();
            manager = MonoSingleton<GameManager>.Instance;
        Label_0040:
            try
            {
                manager.Deserialize(response.body.items);
                manager.Deserialize(response.body.quests);
                goto Label_0079;
            }
            catch (Exception)
            {
            Label_0068:
                this.Failure();
                goto Label_007F;
            }
        Label_0079:
            this.Success();
        Label_007F:
            return;
        }

        private void Success()
        {
            base.set_enabled(0);
            base.ActivateOutputLinks(100);
            return;
        }

        public class JSON_BtlComOpenResponse
        {
            public Json_Item[] items;
            public JSON_QuestProgress[] quests;

            public JSON_BtlComOpenResponse()
            {
                base..ctor();
                return;
            }
        }
    }
}

