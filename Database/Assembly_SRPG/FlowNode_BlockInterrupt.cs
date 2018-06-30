namespace SRPG
{
    using System;

    [NodeType("BlockInterrupt", 0x7fe5), Pin(20, "OnCreate", 1, 0), Pin(10, "OnDestroy", 1, 0), Pin(5, "Create", 0, 0), Pin(0, "Destroy", 0, 0)]
    public class FlowNode_BlockInterrupt : FlowNode
    {
        public BlockInterrupt.EType Type;
        private BlockInterrupt mBlock;

        public FlowNode_BlockInterrupt()
        {
            base..ctor();
            return;
        }

        public override void OnActivate(int pinID)
        {
            if (pinID != null)
            {
                goto Label_0031;
            }
            if (this.mBlock == null)
            {
                goto Label_0023;
            }
            this.mBlock.Destroy();
            this.mBlock = null;
        Label_0023:
            base.ActivateOutputLinks(10);
            goto Label_005D;
        Label_0031:
            if (pinID != 5)
            {
                goto Label_005D;
            }
            if (this.mBlock != null)
            {
                goto Label_0054;
            }
            this.mBlock = BlockInterrupt.Create(this.Type);
        Label_0054:
            base.ActivateOutputLinks(20);
        Label_005D:
            return;
        }

        protected override void OnDestroy()
        {
            if (this.mBlock == null)
            {
                goto Label_001D;
            }
            this.mBlock.Destroy();
            this.mBlock = null;
        Label_001D:
            base.OnDestroy();
            return;
        }
    }
}

