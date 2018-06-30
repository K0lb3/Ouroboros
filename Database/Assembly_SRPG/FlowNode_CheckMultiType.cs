namespace SRPG
{
    using System;

    [Pin(100, "Coop", 1, 100), Pin(200, "Versus", 1, 200), Pin(0x270f, "Invalid", 1, 0x270f), Pin(0, "Check", 0, 0), NodeType("Multi/CheckSchemeType", 0x7fe5), Pin(300, "Tower", 1, 300)]
    public class FlowNode_CheckMultiType : FlowNode
    {
        public FlowNode_CheckMultiType()
        {
            base..ctor();
            return;
        }

        public override void OnActivate(int pinID)
        {
            if (pinID != null)
            {
                goto Label_0060;
            }
            if (FlowNode_OnUrlSchemeLaunch.LINEParam_Pending.type != null)
            {
                goto Label_0023;
            }
            base.ActivateOutputLinks(100);
            goto Label_0060;
        Label_0023:
            if (FlowNode_OnUrlSchemeLaunch.LINEParam_Pending.type != 1)
            {
                goto Label_0044;
            }
            base.ActivateOutputLinks(200);
            goto Label_0060;
        Label_0044:
            if (FlowNode_OnUrlSchemeLaunch.LINEParam_Pending.type != 2)
            {
                goto Label_0060;
            }
            base.ActivateOutputLinks(300);
        Label_0060:
            return;
        }
    }
}

