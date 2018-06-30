namespace SRPG
{
    using GR;
    using System;

    [NodeType("Multi/MultiPlayContinueCoin", 0x7fe5), Pin(0, "Revive", 0, 0), Pin(1, "Success", 1, 1), Pin(2, "コインが足りない", 1, 2), Pin(3, "Continue", 0, 3)]
    public class FlowNode_MultiPlayContinueCoin : FlowNode_Network
    {
        public FlowNode_MultiPlayContinueCoin()
        {
            base..ctor();
            return;
        }

        private void Failure()
        {
            base.set_enabled(0);
            base.ActivateOutputLinks(3);
            return;
        }

        public override void OnActivate(int pinID)
        {
            SceneBattle battle;
            int num;
            int num2;
            PlayerData data;
            BattleCore.Record record;
            if ((pinID != null) && (pinID != 3))
            {
                goto Label_0141;
            }
            battle = SceneBattle.Instance;
            num = (pinID != null) ? MonoSingleton<GameManager>.Instance.MasterParam.FixParam.ContinueCoinCost : MonoSingleton<GameManager>.Instance.MasterParam.FixParam.ContinueCoinCostMulti;
            if ((battle != null) == null)
            {
                goto Label_008D;
            }
            if (battle.Battle == null)
            {
                goto Label_008D;
            }
            if (battle.Battle.IsMultiTower == null)
            {
                goto Label_008D;
            }
            num = MonoSingleton<GameManager>.Instance.MasterParam.FixParam.ContinueCoinCostMultiTower;
        Label_008D:
            if (MonoSingleton<GameManager>.Instance.Player.Coin >= num)
            {
                goto Label_00B4;
            }
            base.set_enabled(0);
            base.ActivateOutputLinks(2);
            return;
        Label_00B4:
            if (Network.Mode != 1)
            {
                goto Label_00F1;
            }
            if (MonoSingleton<GameManager>.Instance.Player.DEBUG_CONSUME_COIN(num) != null)
            {
                goto Label_00E6;
            }
            base.set_enabled(0);
            base.ActivateOutputLinks(2);
            return;
        Label_00E6:
            this.Success();
            goto Label_0141;
        Label_00F1:
            record = SceneBattle.Instance.Battle.GetQuestRecord();
            base.ExecRequest(new ReqBtlComCont(SceneBattle.Instance.Battle.BtlID, record, new Network.ResponseCallback(this.ResponseCallback), 1, SceneBattle.Instance.Battle.IsMultiTower));
            base.set_enabled(1);
        Label_0141:
            return;
        }

        public override unsafe void OnSuccess(WWWResult www)
        {
            WebAPI.JSON_BodyResponse<BattleCore.Json_BattleCont> response;
            PlayerData.EDeserializeFlags flags;
            SceneBattle battle;
            int num;
            Network.EErrCode code;
            if (Network.IsError == null)
            {
                goto Label_0018;
            }
            code = Network.ErrCode;
            this.OnFailed();
            return;
        Label_0018:
            response = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<BattleCore.Json_BattleCont>>(&www.text);
            DebugUtility.Assert((response == null) == 0, "res == null");
            if (response.body != null)
            {
                goto Label_0048;
            }
            this.OnFailed();
            return;
        Label_0048:
            GlobalVars.MultiPlayBattleCont = response.body;
            flags = 0;
            flags |= 2;
            if (MonoSingleton<GameManager>.Instance.Player.Deserialize(response.body.player, flags) != null)
            {
                goto Label_0080;
            }
            this.OnFailed();
            return;
        Label_0080:
            Network.RemoveAPI();
            battle = SceneBattle.Instance;
            num = MonoSingleton<GameManager>.Instance.MasterParam.FixParam.ContinueCoinCostMulti;
            if ((battle != null) == null)
            {
                goto Label_00E6;
            }
            if (battle.Battle == null)
            {
                goto Label_00E6;
            }
            if (battle.Battle.IsMultiTower == null)
            {
                goto Label_00E6;
            }
            num = MonoSingleton<GameManager>.Instance.MasterParam.FixParam.ContinueCoinCostMultiTower;
        Label_00E6:
            MyMetaps.TrackSpendCoin("ContinueMultiQuest", num);
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

