namespace SRPG
{
    using System;

    [Pin(0x67, "DMM", 1, 0x67), Pin(0x68, "EDITOR", 1, 0x68), Pin(0, "Start", 0, 0), Pin(0x65, "iOS", 1, 0x65), Pin(0x66, "Android", 1, 0x66), NodeType("Platform/Switch", 0x7fe5)]
    public class FlowNode_Platform : FlowNode
    {
        private const int PIN_PLATFORM_IOS = 0x65;
        private const int PIN_PLATFORM_ANDROID = 0x66;
        private const int PIN_PLATFORM_DMM = 0x67;
        private const int PIN_PLATFORM_EDITOR = 0x68;

        public FlowNode_Platform()
        {
            base..ctor();
            return;
        }

        public override void OnActivate(int pinID)
        {
            if (pinID != null)
            {
                goto Label_0010;
            }
            base.ActivateOutputLinks(0x67);
            return;
        Label_0010:
            return;
        }
    }
}

