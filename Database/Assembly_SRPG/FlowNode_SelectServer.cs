namespace SRPG
{
    using System;

    [NodeType("System/SelectServer", 0x7fe5), Pin(1, "開発用", 0, 1), Pin(2, "Output", 1, 2), Pin(0, "安定版", 0, 0)]
    public class FlowNode_SelectServer : FlowNode
    {
        public FlowNode_SelectServer()
        {
            base..ctor();
            return;
        }

        public override void OnActivate(int pinID)
        {
            string str;
            int num;
            str = "http://localhost:5000/";
            num = pinID;
            if (num == 1)
            {
                goto Label_0014;
            }
            goto Label_001F;
        Label_0014:
            Network.SetDefaultHostConfigured(str);
            goto Label_0024;
        Label_001F:;
        Label_0024:
            base.set_enabled(0);
            base.ActivateOutputLinks(2);
            return;
        }
    }
}

