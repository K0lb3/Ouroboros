namespace SRPG
{
    using GR;
    using System;

    [Pin(0, "Request", 0, 0), Pin(1, "Success", 1, 1), Pin(10, "石が足りない", 1, 10), NodeType("System/アビリティポイント回復", 0x7fe5)]
    public class FlowNode_AbilityPoint : FlowNode_Network
    {
        public FlowNode_AbilityPoint()
        {
            base..ctor();
            return;
        }

        private int getRequiredCoin()
        {
            return MonoSingleton<GameManager>.GetInstanceDirect().MasterParam.FixParam.AbilityRankUpCountCoin;
        }

        public override void OnActivate(int pinID)
        {
            PlayerData data;
            if (pinID == null)
            {
                goto Label_0007;
            }
            return;
        Label_0007:
            if (MonoSingleton<GameManager>.Instance.Player.Coin >= this.getRequiredCoin())
            {
                goto Label_002B;
            }
            base.ActivateOutputLinks(10);
            return;
        Label_002B:
            if (Network.Mode != 1)
            {
                goto Label_0066;
            }
            base.set_enabled(0);
            data = MonoSingleton<GameManager>.Instance.Player;
            data.DEBUG_CONSUME_COIN(this.getRequiredCoin());
            data.RestoreAbilityRankUpCount();
            this.Success();
            goto Label_0084;
        Label_0066:
            base.set_enabled(1);
            base.ExecRequest(new ReqItemAbilPointPaid(new Network.ResponseCallback(this.ResponseCallback)));
        Label_0084:
            return;
        }

        public override unsafe void OnSuccess(WWWResult www)
        {
            WebAPI.JSON_BodyResponse<Json_PlayerDataAll> response;
            Exception exception;
            Network.EErrCode code;
            if (Network.IsError == null)
            {
                goto Label_002E;
            }
            if (Network.ErrCode == 0xbb8)
            {
                goto Label_0020;
            }
            goto Label_0027;
        Label_0020:
            this.OnBack();
            return;
        Label_0027:
            this.OnRetry();
            return;
        Label_002E:
            response = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json_PlayerDataAll>>(&www.text);
            DebugUtility.Assert((response == null) == 0, "res == null");
            if (response.body != null)
            {
                goto Label_005E;
            }
            this.OnRetry();
            return;
        Label_005E:
            try
            {
                MonoSingleton<GameManager>.Instance.Deserialize(response.body.player);
                goto Label_008F;
            }
            catch (Exception exception1)
            {
            Label_0078:
                exception = exception1;
                DebugUtility.LogException(exception);
                this.OnRetry();
                goto Label_00AB;
            }
        Label_008F:
            Network.RemoveAPI();
            MyMetaps.TrackSpendCoin("AbilityPoint", this.getRequiredCoin());
            this.Success();
        Label_00AB:
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

