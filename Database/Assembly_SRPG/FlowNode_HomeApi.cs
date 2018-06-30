namespace SRPG
{
    using GR;
    using System;

    [Pin(0x15, "Failed", 1, 0x15), NodeType("System/HomeApi", 0x7fe5), Pin(100, "Start", 0, 0), Pin(20, "Success", 1, 20)]
    public class FlowNode_HomeApi : FlowNode_Network
    {
        public FlowNode_HomeApi()
        {
            base..ctor();
            return;
        }

        private void _Failed()
        {
            Network.RemoveAPI();
            Network.ResetError();
            base.ActivateOutputLinks(20);
            base.set_enabled(0);
            return;
        }

        private void _Success()
        {
            base.ActivateOutputLinks(20);
            base.set_enabled(0);
            return;
        }

        public override void OnActivate(int pinID)
        {
            bool flag;
            if (pinID != 100)
            {
                goto Label_0073;
            }
            if (Network.Mode != null)
            {
                goto Label_006D;
            }
            flag = 0;
            if ((MonoSingleton<GameManager>.Instance != null) == null)
            {
                goto Label_0043;
            }
            if (MonoSingleton<GameManager>.Instance.Player == null)
            {
                goto Label_0043;
            }
            flag = MonoSingleton<GameManager>.Instance.Player.MultiInvitaionFlag;
        Label_0043:
            MultiInvitationBadge.isValid = 0;
            base.ExecRequest(new HomeApi(flag, new Network.ResponseCallback(this.ResponseCallback)));
            base.set_enabled(1);
            goto Label_0073;
        Label_006D:
            this._Success();
        Label_0073:
            return;
        }

        public override unsafe void OnSuccess(WWWResult www)
        {
            WebAPI.JSON_BodyResponse<JSON_HomeApiResponse> response;
            if (Network.IsError == null)
            {
                goto Label_0011;
            }
            this._Failed();
            return;
        Label_0011:
            DebugMenu.Log("API", "homeapi:{" + &www.text + "}");
            response = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<JSON_HomeApiResponse>>(&www.text);
            DebugUtility.Assert((response == null) == 0, "res == null");
            Network.RemoveAPI();
            if ((response.body == null) || (response.body.player == null))
            {
                goto Label_0104;
            }
            MonoSingleton<GameManager>.Instance.Player.ValidGpsGift = (response.body.player.areamail_enabled == null) ? 0 : 1;
            MonoSingleton<GameManager>.Instance.Player.ValidFriendPresent = (response.body.player.present_granted == null) ? 0 : 1;
            MultiInvitationReceiveWindow.SetBadge((response.body.player.multi_inv == 0) == 0);
            MonoSingleton<GameManager>.Instance.Player.FirstChargeStatus = response.body.player.charge_bonus;
            goto Label_013A;
        Label_0104:
            MonoSingleton<GameManager>.Instance.Player.ValidGpsGift = 0;
            MonoSingleton<GameManager>.Instance.Player.ValidFriendPresent = 0;
            MultiInvitationReceiveWindow.SetBadge(0);
            MonoSingleton<GameManager>.Instance.Player.FirstChargeStatus = 0;
        Label_013A:
            if (response.body == null)
            {
                goto Label_0165;
            }
            if (response.body.pubinfo == null)
            {
                goto Label_0165;
            }
            LoginNewsInfo.SetPubInfo(response.body.pubinfo);
        Label_0165:
            this._Success();
            return;
        }

        public class JSON_HomeApiResponse
        {
            public Player player;
            public LoginNewsInfo.JSON_PubInfo pubinfo;

            public JSON_HomeApiResponse()
            {
                base..ctor();
                return;
            }

            public class Player
            {
                public int areamail_enabled;
                public int present_granted;
                public int multi_inv;
                public int charge_bonus;

                public Player()
                {
                    base..ctor();
                    return;
                }
            }
        }
    }
}

