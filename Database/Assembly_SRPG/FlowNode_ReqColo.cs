namespace SRPG
{
    using GR;
    using System;

    [NodeType("Network/btl_colo", 0x7fe5), Pin(1, "Success", 1, 1), Pin(0, "Request", 0, 0)]
    public class FlowNode_ReqColo : FlowNode_Network
    {
        public FlowNode_ReqColo()
        {
            base..ctor();
            return;
        }

        public override void OnActivate(int pinID)
        {
            if (pinID != null)
            {
                goto Label_0042;
            }
            if (base.get_enabled() == null)
            {
                goto Label_0012;
            }
            return;
        Label_0012:
            if (Network.Mode != 1)
            {
                goto Label_0024;
            }
            this.Success();
            return;
        Label_0024:
            base.ExecRequest(new ReqBtlColo(new Network.ResponseCallback(this.ResponseCallback)));
            base.set_enabled(1);
        Label_0042:
            return;
        }

        public override unsafe void OnSuccess(WWWResult www)
        {
            WebAPI.JSON_BodyResponse<Json_ArenaPlayers> response;
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
            response = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json_ArenaPlayers>>(&www.text);
            DebugUtility.Assert((response == null) == 0, "res == null");
            if (response.body != null)
            {
                goto Label_0047;
            }
            this.OnRetry();
            return;
        Label_0047:
            base.set_enabled(0);
            manager = MonoSingleton<GameManager>.Instance;
            if (manager.Deserialize(response.body) != null)
            {
                goto Label_006C;
            }
            this.OnFailed();
            return;
        Label_006C:
            Network.RemoveAPI();
            manager.Player.UpdateArenaRankTrophyStates(-1, -1);
            this.Success();
            return;
        }

        private void Success()
        {
            base.ActivateOutputLinks(1);
            return;
        }
    }
}

