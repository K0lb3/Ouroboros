namespace SRPG
{
    using System;

    [Pin(1, "Out", 1, 1), NodeType("Battle/RefreshQueue", 0x7fe5), Pin(0, "行動順更新", 0, 0)]
    public class FlowNode_BattleRefreshQueue : FlowNode
    {
        public FlowNode_BattleRefreshQueue()
        {
            base..ctor();
            return;
        }

        public override void OnActivate(int pinID)
        {
            if (pinID != null)
            {
                goto Label_0029;
            }
            if ((UnitQueue.Instance != null) == null)
            {
                goto Label_0029;
            }
            UnitQueue.Instance.Refresh(0);
            base.ActivateOutputLinks(1);
        Label_0029:
            return;
        }
    }
}

