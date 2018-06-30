namespace SRPG
{
    using GR;
    using System;

    [Pin(10, "Success", 1, 10), NodeType("System/ReqUpdateSelectAward", 0x7fe5), Pin(0, "Request", 0, 0), Pin(11, "Failure", 1, 11)]
    public class FlowNode_ReqUpdateSelectAward : FlowNode_Network
    {
        public FlowNode_ReqUpdateSelectAward()
        {
            base..ctor();
            return;
        }

        private void Failure()
        {
            base.set_enabled(0);
            return;
        }

        public override void OnActivate(int pinID)
        {
            string str;
            if (pinID != null)
            {
                goto Label_0055;
            }
            str = FlowNode_Variable.Get("CONFIRM_SELECT_AWARD");
            if ((MonoSingleton<GameManager>.GetInstanceDirect().Player.SelectedAward != str) == null)
            {
                goto Label_004F;
            }
            base.ExecRequest(new ReqUpdateSelectAward(str, new Network.ResponseCallback(this.ResponseCallback)));
            base.set_enabled(1);
            goto Label_0055;
        Label_004F:
            this.Success();
        Label_0055:
            return;
        }

        public override unsafe void OnSuccess(WWWResult www)
        {
            WebAPI.JSON_BodyResponse<Json_ResSelectAward> response;
            Network.EErrCode code;
            if (Network.IsError == null)
            {
                goto Label_0015;
            }
            code = Network.ErrCode;
        Label_0015:
            response = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json_ResSelectAward>>(&www.text);
            DebugUtility.Assert((response == null) == 0, "res == null");
            Network.RemoveAPI();
            if (response != null)
            {
                goto Label_0045;
            }
            this.Failure();
            return;
        Label_0045:
            MonoSingleton<GameManager>.Instance.Player.SelectedAward = response.body.selected_award;
            this.Success();
            return;
        }

        private void Success()
        {
            base.set_enabled(0);
            base.ActivateOutputLinks(10);
            return;
        }
    }
}

