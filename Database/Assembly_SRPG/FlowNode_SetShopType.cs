namespace SRPG
{
    using System;

    [Pin(0x69, "Tour", 0, 5), Pin(0x6a, "Multi", 0, 6), Pin(0x6b, "AwakePiece", 0, 7), Pin(0x6c, "Artifact", 0, 8), Pin(0x6d, "Artifact", 0, 9), Pin(110, "Gurrilla", 0, 10), Pin(100, "Normal", 0, 0), Pin(1, "Success", 1, 0x3e8), NodeType("System/SetShopType", 0x7fe5), Pin(0x65, "Tabi", 0, 1), Pin(0x66, "Kimagure", 0, 2), Pin(0x67, "Monozuki", 0, 3), Pin(0x68, "Arena", 0, 4)]
    public class FlowNode_SetShopType : FlowNode
    {
        public FlowNode_SetShopType()
        {
            base..ctor();
            return;
        }

        public override void OnActivate(int pinID)
        {
            int num;
            num = pinID;
            switch ((num - 0x65))
            {
                case 0:
                    goto Label_0038;

                case 1:
                    goto Label_0043;

                case 2:
                    goto Label_004E;

                case 3:
                    goto Label_0059;

                case 4:
                    goto Label_0064;

                case 5:
                    goto Label_006F;

                case 6:
                    goto Label_007A;

                case 7:
                    goto Label_0085;

                case 8:
                    goto Label_0090;

                case 9:
                    goto Label_009C;
            }
            goto Label_00A8;
        Label_0038:
            GlobalVars.ShopType = 1;
            goto Label_00B3;
        Label_0043:
            GlobalVars.ShopType = 2;
            goto Label_00B3;
        Label_004E:
            GlobalVars.ShopType = 3;
            goto Label_00B3;
        Label_0059:
            GlobalVars.ShopType = 5;
            goto Label_00B3;
        Label_0064:
            GlobalVars.ShopType = 4;
            goto Label_00B3;
        Label_006F:
            GlobalVars.ShopType = 6;
            goto Label_00B3;
        Label_007A:
            GlobalVars.ShopType = 7;
            goto Label_00B3;
        Label_0085:
            GlobalVars.ShopType = 8;
            goto Label_00B3;
        Label_0090:
            GlobalVars.ShopType = 10;
            goto Label_00B3;
        Label_009C:
            GlobalVars.ShopType = 11;
            goto Label_00B3;
        Label_00A8:
            GlobalVars.ShopType = 0;
        Label_00B3:
            base.set_enabled(0);
            base.ActivateOutputLinks(1);
            return;
        }
    }
}

