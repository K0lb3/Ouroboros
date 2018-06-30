namespace SRPG
{
    using GR;
    using System;

    [NodeType("Network/btl_colo_history"), Pin(1, "Success", 1, 1), Pin(0, "Request", 0, 0)]
    public class FlowNode_ReqArenaHistory : FlowNode_Network
    {
        public FlowNode_ReqArenaHistory()
        {
            base..ctor();
            return;
        }

        public override void OnActivate(int pinID)
        {
            if (pinID != null)
            {
                goto Label_0036;
            }
            if (Network.Mode != 1)
            {
                goto Label_0018;
            }
            this.Success();
            return;
        Label_0018:
            base.ExecRequest(new ReqBtlColoHistory(new Network.ResponseCallback(this.ResponseCallback)));
            base.set_enabled(1);
        Label_0036:
            return;
        }

        public override unsafe void OnSuccess(WWWResult www)
        {
            WebAPI.JSON_BodyResponse<JSON_ArenaHistory> response;
            GameManager manager;
            Network.EErrCode code;
            if (Network.IsError == null)
            {
                goto Label_0017;
            }
            code = Network.ErrCode;
            this.OnFailed();
            return;
        Label_0017:
            response = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<JSON_ArenaHistory>>(&www.text);
            DebugUtility.Assert((response == null) == 0, "res == null");
            if (response.body != null)
            {
                goto Label_0047;
            }
            this.OnFailed();
            return;
        Label_0047:
            if (MonoSingleton<GameManager>.Instance.Deserialize(response.body) != null)
            {
                goto Label_0065;
            }
            this.OnFailed();
            return;
        Label_0065:
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
    }
}

