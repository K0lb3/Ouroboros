namespace SRPG
{
    using GR;
    using System;

    [Pin(0x66, "WaitingForSetup", 1, 0x66), NodeType("Payment/GetProducts", 0x7fe5), Pin(0x67, "Empty", 1, 0x67), Pin(0, "Start", 0, 0), Pin(1, "ClearList", 0, 1), Pin(100, "Success", 1, 100), Pin(0x65, "Failure", 1, 0x65)]
    public class FlowNode_PaymentGetProducts : FlowNode
    {
        private bool mSetDelegate;
        public static PaymentManager.Product[] Products;

        public FlowNode_PaymentGetProducts()
        {
            base..ctor();
            return;
        }

        private void Empty()
        {
            DebugUtility.LogWarning("GetProducts empty");
            base.set_enabled(0);
            this.RemoveDelegate();
            base.ActivateOutputLinks(0x67);
            return;
        }

        private void Failure()
        {
            DebugUtility.LogWarning("GetProducts failure");
            base.set_enabled(0);
            this.RemoveDelegate();
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
                goto Label_0060;
            }
            if (this.mSetDelegate != null)
            {
                goto Label_003E;
            }
            local1 = MonoSingleton<PaymentManager>.Instance;
            local1.OnShowItems = (PaymentManager.ShowItemsDelegate) Delegate.Combine(local1.OnShowItems, new PaymentManager.ShowItemsDelegate(this.OnShowItems));
            this.mSetDelegate = 1;
        Label_003E:
            base.set_enabled(1);
            if (MonoSingleton<PaymentManager>.Instance.ShowItems() != null)
            {
                goto Label_007D;
            }
            this.Failure();
            return;
            goto Label_007D;
        Label_0060:
            if (pinID != 1)
            {
                goto Label_007D;
            }
            base.set_enabled(0);
            Products = null;
            base.ActivateOutputLinks(100);
        Label_007D:
            return;
        }

        protected override void OnDestroy()
        {
            this.RemoveDelegate();
            base.OnDestroy();
            return;
        }

        public void OnShowItems(PaymentManager.EShowItemsResult result, PaymentManager.Product[] products)
        {
            if (MonoSingleton<PaymentManager>.Instance.IsAvailable != null)
            {
                goto Label_0016;
            }
            this.WaitingForSetup();
            return;
        Label_0016:
            if (products == null)
            {
                goto Label_0025;
            }
            if (((int) products.Length) > 0)
            {
                goto Label_002C;
            }
        Label_0025:
            this.Empty();
            return;
        Label_002C:
            if (result == null)
            {
                goto Label_0039;
            }
            this.Failure();
            return;
        Label_0039:
            Products = products;
            this.Success();
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
            local1.OnShowItems = (PaymentManager.ShowItemsDelegate) Delegate.Remove(local1.OnShowItems, new PaymentManager.ShowItemsDelegate(this.OnShowItems));
            this.mSetDelegate = 0;
        Label_0038:
            return;
        }

        private void Success()
        {
            DebugUtility.Log("GetProducts success");
            base.set_enabled(0);
            this.RemoveDelegate();
            base.ActivateOutputLinks(100);
            return;
        }

        private void WaitingForSetup()
        {
            DebugUtility.LogWarning("GetProducts waiting for setup");
            base.set_enabled(0);
            this.RemoveDelegate();
            base.ActivateOutputLinks(0x66);
            return;
        }
    }
}

