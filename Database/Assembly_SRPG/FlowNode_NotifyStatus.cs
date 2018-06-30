namespace SRPG
{
    using System;

    [Pin(0, "Enable", 0, 0), NodeType("System/Notify/Status", 0x7fe5), Pin(10, "output", 1, 10), Pin(1, "Disable", 0, 1)]
    public class FlowNode_NotifyStatus : FlowNode
    {
        private const int PIN_ENABLE = 0;
        private const int PIN_DISABLE = 1;
        private const int PIN_OUTPUT = 10;

        public FlowNode_NotifyStatus()
        {
            base..ctor();
            return;
        }

        public void NotifyDisable()
        {
            NotifyList.mNotifyEnable = 0;
            return;
        }

        public void NotifyEnable()
        {
            NotifyList.mNotifyEnable = 1;
            return;
        }

        public override void OnActivate(int pinID)
        {
            int num;
            num = pinID;
            if (num == null)
            {
                goto Label_0014;
            }
            if (num == 1)
            {
                goto Label_001F;
            }
            goto Label_002A;
        Label_0014:
            this.NotifyEnable();
            goto Label_002A;
        Label_001F:
            this.NotifyDisable();
        Label_002A:
            base.ActivateOutputLinks(10);
            return;
        }
    }
}

