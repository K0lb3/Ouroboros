namespace SRPG
{
    using System;

    [Pin(2, "Wait", 0, 2), Pin(10, "Out", 1, 10), Pin(11, "Finished", 1, 11), NodeType("System/クリティカルセクション", 0xff0000), Pin(0, "Enter", 0, 0), Pin(1, "Leave", 0, 1)]
    public class FlowNode_CriticalSection : FlowNode
    {
        private const int PINID_ENTER = 0;
        private const int PINID_LEAVE = 1;
        private const int PINID_WAIT = 2;
        private const int PINID_OUT = 10;
        private const int PINID_FINISHED = 11;
        [BitMask]
        public CriticalSections Mask;

        public FlowNode_CriticalSection()
        {
            this.Mask = 1;
            base..ctor();
            return;
        }

        public override void OnActivate(int pinID)
        {
            int num;
            num = pinID;
            switch (num)
            {
                case 0:
                    goto Label_0019;

                case 1:
                    goto Label_0032;

                case 2:
                    goto Label_004B;
            }
            goto Label_0057;
        Label_0019:
            CriticalSection.Enter(this.Mask);
            base.ActivateOutputLinks(10);
            goto Label_0057;
        Label_0032:
            CriticalSection.Leave(this.Mask);
            base.ActivateOutputLinks(10);
            goto Label_0057;
        Label_004B:
            base.set_enabled(1);
        Label_0057:
            return;
        }

        private void Update()
        {
            if (CriticalSection.IsActive != null)
            {
                goto Label_001A;
            }
            base.set_enabled(0);
            base.ActivateOutputLinks(11);
        Label_001A:
            return;
        }
    }
}

