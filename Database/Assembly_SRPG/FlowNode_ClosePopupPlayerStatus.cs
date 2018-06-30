namespace SRPG
{
    using System;

    [Pin(1, "Success", 1, 1), Pin(0, "Request", 0, 0), NodeType("System/ClosePopup", 0x7fe5)]
    public class FlowNode_ClosePopupPlayerStatus : FlowNode
    {
        public FlowNode_ClosePopupPlayerStatus()
        {
            base..ctor();
            return;
        }

        public override void OnActivate(int pinID)
        {
            Win_Btn_DecideCancel_FL_C l_fl_c;
            Win_Btn_DecideCancel_FL_C l_fl_c2;
            if (pinID != null)
            {
                goto Label_0075;
            }
            if ((FlowNode_BuyGold.ConfirmBoxObj != null) == null)
            {
                goto Label_0039;
            }
            l_fl_c = FlowNode_BuyGold.ConfirmBoxObj.GetComponent<Win_Btn_DecideCancel_FL_C>();
            if ((l_fl_c != null) == null)
            {
                goto Label_0033;
            }
            l_fl_c.BeginClose();
        Label_0033:
            FlowNode_BuyGold.ConfirmBoxObj = null;
        Label_0039:
            if ((FlowNode_BuyStamina.ConfirmBoxObj != null) == null)
            {
                goto Label_006C;
            }
            l_fl_c2 = FlowNode_BuyStamina.ConfirmBoxObj.GetComponent<Win_Btn_DecideCancel_FL_C>();
            if ((l_fl_c2 != null) == null)
            {
                goto Label_0066;
            }
            l_fl_c2.BeginClose();
        Label_0066:
            FlowNode_BuyStamina.ConfirmBoxObj = null;
        Label_006C:
            base.ActivateOutputLinks(1);
            return;
        Label_0075:
            return;
        }
    }
}

