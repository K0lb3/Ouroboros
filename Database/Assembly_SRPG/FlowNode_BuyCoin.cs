namespace SRPG
{
    using GR;
    using System;

    [Pin(0, "Buy", 0, 0), Pin(1, "Success", 1, 1), NodeType("System/BuyCoin", 0x7fe5)]
    public class FlowNode_BuyCoin : FlowNode_Network
    {
        public FlowNode_BuyCoin()
        {
            base..ctor();
            return;
        }

        private void Failure()
        {
            PunMonoSingleton<MyPhoton>.Instance.EnableKeepAlive(1);
            base.set_enabled(0);
            base.ActivateOutputLinks(2);
            return;
        }

        public override void OnActivate(int pinID)
        {
            string str;
            string str2;
            string str3;
            int num;
            int num2;
            int num3;
            if (pinID != null)
            {
                goto Label_007D;
            }
            if (Network.Mode != null)
            {
                goto Label_0048;
            }
            base.set_enabled(1);
            str = "p_10_coin";
            str2 = "test_receipt";
            str3 = "test_transactionid";
            base.ExecRequest(new ReqProductBuy(str, str2, str3, new Network.ResponseCallback(this.ResponseCallback)));
            goto Label_007D;
        Label_0048:
            num = 0;
            num2 = 10;
            num3 = 0;
            MonoSingleton<GameManager>.Instance.Player.DEBUG_ADD_COIN(num, num2, num3);
            PunMonoSingleton<MyPhoton>.Instance.EnableKeepAlive(1);
            base.set_enabled(0);
            this.Success();
        Label_007D:
            return;
        }

        public override unsafe void OnSuccess(WWWResult www)
        {
            WebAPI.JSON_BodyResponse<Json_PlayerDataAll> response;
            PlayerData.EDeserializeFlags flags;
            response = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json_PlayerDataAll>>(&www.text);
            DebugUtility.Assert((response == null) == 0, "res == null");
            if (response.body != null)
            {
                goto Label_0030;
            }
            this.OnFailed();
            return;
        Label_0030:
            flags = 2;
            if (MonoSingleton<GameManager>.Instance.Player.Deserialize(response.body.player, flags) != null)
            {
                goto Label_0059;
            }
            this.OnFailed();
            return;
        Label_0059:
            Network.RemoveAPI();
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

