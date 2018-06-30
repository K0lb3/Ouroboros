namespace SRPG
{
    using System;

    [Pin(0x65, "Off", 0, 1), NodeType("System/SetSleep", 0x7fe5), Pin(100, "On", 0, 0), Pin(1, "Out", 1, 2)]
    public class FlowNode_SetSleep : FlowNode
    {
        public FlowNode_SetSleep()
        {
            base..ctor();
            return;
        }

        public override void OnActivate(int pinID)
        {
            if (pinID != this.On)
            {
                goto Label_0016;
            }
            GameUtility.SetDefaultSleepSetting();
            goto Label_0027;
        Label_0016:
            if (pinID != this.Off)
            {
                goto Label_0027;
            }
            GameUtility.SetNeverSleep();
        Label_0027:
            base.ActivateOutputLinks(1);
            return;
        }

        private int On
        {
            get
            {
                return 100;
            }
        }

        private int Off
        {
            get
            {
                return 0x65;
            }
        }
    }
}

