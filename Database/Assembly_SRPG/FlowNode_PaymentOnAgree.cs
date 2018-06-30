namespace SRPG
{
    using GR;
    using System;

    [NodeType("Payment/OnAgree", 0x7fe5), Pin(100, "Out", 1, 100), Pin(0, "OnAgree", 0, 0)]
    public class FlowNode_PaymentOnAgree : FlowNode
    {
        public FlowNode_PaymentOnAgree()
        {
            base..ctor();
            return;
        }

        public override void OnActivate(int pinID)
        {
            if (pinID != null)
            {
                goto Label_0019;
            }
            MonoSingleton<PaymentManager>.Instance.OnAgree();
            base.ActivateOutputLinks(100);
        Label_0019:
            return;
        }
    }
}

