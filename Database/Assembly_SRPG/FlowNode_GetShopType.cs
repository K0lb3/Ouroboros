namespace SRPG
{
    using System;

    [Pin(100, "Normal", 1, 0), Pin(0x65, "Tabi", 1, 1), Pin(0x6a, "Multi", 1, 6), Pin(0x66, "Kimagure", 1, 2), Pin(0x67, "Monozuki", 1, 3), Pin(0x6b, "AwakePiece", 1, 7), Pin(0x6c, "Artifact", 1, 8), Pin(0x6d, "Limited", 1, 9), Pin(0x68, "Arena", 1, 4), Pin(1, "Test", 0, 10), Pin(0x69, "Tour", 1, 5), NodeType("System/GetShopType", 0x7fe5)]
    public class FlowNode_GetShopType : FlowNode
    {
        public FlowNode_GetShopType()
        {
            base..ctor();
            return;
        }

        public override void OnActivate(int pinID)
        {
            if (pinID != 1)
            {
                goto Label_00C8;
            }
            if (GlobalVars.ShopType != 1)
            {
                goto Label_001B;
            }
            pinID = 0x65;
            goto Label_00C0;
        Label_001B:
            if (GlobalVars.ShopType != 2)
            {
                goto Label_002F;
            }
            pinID = 0x66;
            goto Label_00C0;
        Label_002F:
            if (GlobalVars.ShopType != 3)
            {
                goto Label_0043;
            }
            pinID = 0x67;
            goto Label_00C0;
        Label_0043:
            if (GlobalVars.ShopType != 5)
            {
                goto Label_0057;
            }
            pinID = 0x68;
            goto Label_00C0;
        Label_0057:
            if (GlobalVars.ShopType != 4)
            {
                goto Label_006B;
            }
            pinID = 0x69;
            goto Label_00C0;
        Label_006B:
            if (GlobalVars.ShopType != 6)
            {
                goto Label_007F;
            }
            pinID = 0x6a;
            goto Label_00C0;
        Label_007F:
            if (GlobalVars.ShopType != 7)
            {
                goto Label_0093;
            }
            pinID = 0x6b;
            goto Label_00C0;
        Label_0093:
            if (GlobalVars.ShopType != 8)
            {
                goto Label_00A7;
            }
            pinID = 0x6c;
            goto Label_00C0;
        Label_00A7:
            if (GlobalVars.ShopType != 10)
            {
                goto Label_00BC;
            }
            pinID = 0x6d;
            goto Label_00C0;
        Label_00BC:
            pinID = 100;
        Label_00C0:
            base.ActivateOutputLinks(pinID);
        Label_00C8:
            return;
        }
    }
}

