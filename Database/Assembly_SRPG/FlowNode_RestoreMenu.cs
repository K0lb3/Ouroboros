namespace SRPG
{
    using System;

    [Pin(1, "Set", 0, 1), Pin(10, "Out", 1, 10), NodeType("System/Set Restore Menu", 0x7fe5)]
    public class FlowNode_RestoreMenu : FlowNode
    {
        [ShowInInfo]
        public RestorePoints RestorePoint;

        public FlowNode_RestoreMenu()
        {
            base..ctor();
            return;
        }

        public override void OnActivate(int pinID)
        {
            if (pinID != 1)
            {
                goto Label_001C;
            }
            HomeWindow.SetRestorePoint(this.RestorePoint);
            base.ActivateOutputLinks(10);
            return;
        Label_001C:
            return;
        }
    }
}

