namespace SRPG
{
    using GR;
    using System;
    using UnityEngine;

    [Pin(11, "Failed", 1, 11), Pin(0, "Request", 0, 0), Pin(0x65, "ConnectingDialogClose", 1, 0x65), Pin(100, "ConnectingDialogOpen", 1, 100), NodeType("Payment/PurchaseResume", 0x7fe5), Pin(10, "Success", 1, 10)]
    public class FlowNode_PurchaseResume : FlowNode
    {
        private bool mSetDelegate;
        private float wait_time;
        private bool succeeded;

        public FlowNode_PurchaseResume()
        {
            base..ctor();
            return;
        }

        public void ClosePurchaseMsg()
        {
            base.ActivateOutputLinks(0x65);
            return;
        }

        protected override void Finalize()
        {
        Label_0000:
            try
            {
                this.RemoveDelegate();
                goto Label_0012;
            }
            finally
            {
            Label_000B:
                base.Finalize();
            }
        Label_0012:
            return;
        }

        public override void OnActivate(int pinID)
        {
            PaymentManager local1;
            if (pinID != null)
            {
                goto Label_0067;
            }
            this.wait_time = 5f;
            this.succeeded = 0;
            if (this.mSetDelegate != null)
            {
                goto Label_0050;
            }
            local1 = MonoSingleton<PaymentManager>.Instance;
            local1.OnRequestSucceeded = (PaymentManager.RequestSucceededDelegate) Delegate.Combine(local1.OnRequestSucceeded, new PaymentManager.RequestSucceededDelegate(this.OnRequestSucceeded));
            this.mSetDelegate = 1;
        Label_0050:
            base.set_enabled(1);
            MonoSingleton<PurchaseManager>.Instance.Resume();
            this.OpenPurchaseMsg();
        Label_0067:
            return;
        }

        protected override void OnDestroy()
        {
            this.RemoveDelegate();
            base.OnDestroy();
            return;
        }

        public void OnRequestSucceeded()
        {
            this.succeeded = 1;
            return;
        }

        public void OpenPurchaseMsg()
        {
            base.ActivateOutputLinks(100);
            return;
        }

        private void RemoveDelegate()
        {
            PaymentManager local1;
            if (this.mSetDelegate == null)
            {
                goto Label_0038;
            }
            local1 = MonoSingleton<PaymentManager>.Instance;
            local1.OnRequestSucceeded = (PaymentManager.RequestSucceededDelegate) Delegate.Remove(local1.OnRequestSucceeded, new PaymentManager.RequestSucceededDelegate(this.OnRequestSucceeded));
            this.mSetDelegate = 0;
        Label_0038:
            return;
        }

        public void Update()
        {
            if (this.succeeded == null)
            {
                goto Label_002C;
            }
            base.set_enabled(0);
            this.ClosePurchaseMsg();
            base.ActivateOutputLinks(10);
            this.RemoveDelegate();
            goto Label_006A;
        Label_002C:
            this.wait_time -= Time.get_unscaledDeltaTime();
            if (this.wait_time > 0f)
            {
                goto Label_006A;
            }
            base.set_enabled(0);
            this.ClosePurchaseMsg();
            base.ActivateOutputLinks(11);
            this.RemoveDelegate();
        Label_006A:
            return;
        }
    }
}

