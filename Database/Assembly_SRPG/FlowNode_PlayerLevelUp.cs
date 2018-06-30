namespace SRPG
{
    using System;

    [NodeType("Battle/プレイヤーレベルアップ", 0x7fe5), Pin(0, "実行", 0, 0), Pin(1, "レベルアップした", 1, 1), Pin(2, "レベルアップしなかった", 1, 2)]
    public class FlowNode_PlayerLevelUp : FlowNode
    {
        public FlowNode_PlayerLevelUp()
        {
            base..ctor();
            return;
        }

        public override void OnActivate(int pinID)
        {
            int num;
            int num2;
            if (pinID != null)
            {
                goto Label_0042;
            }
            num = PlayerData.CalcLevelFromExp(GlobalVars.PlayerExpOld);
            if (PlayerData.CalcLevelFromExp(GlobalVars.PlayerExpNew) != num)
            {
                goto Label_003A;
            }
            base.ActivateOutputLinks(2);
            goto Label_0042;
        Label_003A:
            base.ActivateOutputLinks(1);
        Label_0042:
            return;
        }
    }
}

