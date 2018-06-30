namespace SRPG
{
    using GR;
    using System;

    [NodeType("Check/RewardApCheck", 0x7fe5), Pin(3, "Overflow", 1, 2), Pin(2, "AlreadyCapped", 1, 2), Pin(1, "Success", 1, 1), Pin(0, "Check", 0, 0)]
    public class FlowNode_RewardApCheck : FlowNode
    {
        public FlowNode_RewardApCheck()
        {
            base..ctor();
            return;
        }

        public override void OnActivate(int pinID)
        {
            RewardData data;
            PlayerData data2;
            if (pinID != null)
            {
                goto Label_00F7;
            }
            data = GlobalVars.LastReward.Get();
            if (data == null)
            {
                goto Label_0023;
            }
            if (data.Stamina >= 1)
            {
                goto Label_002C;
            }
        Label_0023:
            base.ActivateOutputLinks(1);
            return;
        Label_002C:
            data2 = MonoSingleton<GameManager>.GetInstanceDirect().Player;
            if (data.Stamina <= data2.StaminaStockCap)
            {
                goto Label_0051;
            }
            base.ActivateOutputLinks(3);
            return;
        Label_0051:
            if (data2.Stamina < data2.StaminaStockCap)
            {
                goto Label_00CD;
            }
            if (data.Exp > 0)
            {
                goto Label_00BB;
            }
            if (data.Gold > 0)
            {
                goto Label_00BB;
            }
            if (data.Coin > 0)
            {
                goto Label_00BB;
            }
            if (data.ArenaMedal > 0)
            {
                goto Label_00BB;
            }
            if (data.MultiCoin > 0)
            {
                goto Label_00BB;
            }
            if (data.KakeraCoin > 0)
            {
                goto Label_00BB;
            }
            if (data.Items.Count <= 0)
            {
                goto Label_00C4;
            }
        Label_00BB:
            base.ActivateOutputLinks(3);
            return;
        Label_00C4:
            base.ActivateOutputLinks(2);
            return;
        Label_00CD:
            if ((data2.Stamina + data.Stamina) > data2.StaminaStockCap)
            {
                goto Label_00EE;
            }
            base.ActivateOutputLinks(1);
            return;
        Label_00EE:
            base.ActivateOutputLinks(3);
            return;
        Label_00F7:
            return;
        }
    }
}

