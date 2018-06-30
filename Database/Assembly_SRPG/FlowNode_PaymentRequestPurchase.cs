namespace SRPG
{
    using GR;
    using System;
    using System.Runtime.InteropServices;

    [NodeType("Payment/RequestPurchase", 0x7fe5), Pin(0x12d, "ConnectingDialogClose", 1, 0x12d), Pin(300, "ConnectingDialogOpen", 1, 300), Pin(0xce, "NeedBirthday", 1, 0xce), Pin(0xcd, "OverLimited", 1, 0xcd), Pin(0xcc, "InsufficientBalances", 1, 0xcc), Pin(0xcb, "Cancel", 1, 0xcb), Pin(0xca, "Deferred", 1, 0xca), Pin(0xc9, "AlreadyOwn", 1, 0xc9), Pin(200, "Error", 1, 200), Pin(100, "Success", 1, 100), Pin(0, "Start", 0, 0)]
    public class FlowNode_PaymentRequestPurchase : FlowNode
    {
        private bool mSetDelegate;

        public FlowNode_PaymentRequestPurchase()
        {
            base..ctor();
            return;
        }

        private void AlreadyOwn()
        {
            DebugUtility.LogWarning("RequestPurchase alreadyown");
            base.set_enabled(0);
            this.RemoveDelegate();
            this.CloseProcessingDialog();
            base.ActivateOutputLinks(0xc9);
            return;
        }

        private void Cancel()
        {
            DebugUtility.LogWarning("RequestPurchase cancel");
            base.set_enabled(0);
            this.RemoveDelegate();
            this.CloseProcessingDialog();
            base.ActivateOutputLinks(0xcb);
            return;
        }

        public void CloseProcessingDialog()
        {
            base.ActivateOutputLinks(0x12d);
            return;
        }

        private void Deferred()
        {
            DebugUtility.LogWarning("RequestPurchase deferred");
            base.set_enabled(0);
            this.RemoveDelegate();
            this.CloseProcessingDialog();
            base.ActivateOutputLinks(0xca);
            return;
        }

        private void Error()
        {
            DebugUtility.LogWarning("RequestPurchase error");
            base.set_enabled(0);
            this.RemoveDelegate();
            this.CloseProcessingDialog();
            base.ActivateOutputLinks(200);
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

        private void InsufficientBalances()
        {
            DebugUtility.LogWarning("RequestPurchase insufficient balances");
            base.set_enabled(0);
            this.RemoveDelegate();
            this.CloseProcessingDialog();
            base.ActivateOutputLinks(0xcc);
            return;
        }

        private void NeedBirthday()
        {
            DebugUtility.LogWarning("RequestPurchase need birthday");
            base.set_enabled(0);
            this.RemoveDelegate();
            this.CloseProcessingDialog();
            base.ActivateOutputLinks(0xce);
            return;
        }

        public override void OnActivate(int pinID)
        {
            PaymentManager local2;
            PaymentManager local1;
            if (pinID != null)
            {
                goto Label_009A;
            }
            if (this.mSetDelegate != null)
            {
                goto Label_006E;
            }
            local1 = MonoSingleton<PaymentManager>.Instance;
            local1.OnRequestPurchase = (PaymentManager.RequestPurchaseDelegate) Delegate.Combine(local1.OnRequestPurchase, new PaymentManager.RequestPurchaseDelegate(this.OnRequestPurchase));
            local2 = MonoSingleton<PaymentManager>.Instance;
            local2.OnRequestProcessing = (PaymentManager.RequestProcessingDelegate) Delegate.Combine(local2.OnRequestProcessing, new PaymentManager.RequestProcessingDelegate(this.OnRequestProcessing));
            this.mSetDelegate = 1;
            DebugUtility.Log("PaymentRequestPurchase SetDelegate");
        Label_006E:
            base.set_enabled(1);
            DebugUtility.LogWarning("PaymentRequestPurchase start");
            if (MonoSingleton<PaymentManager>.Instance.RequestPurchase(GlobalVars.SelectedProductID) != null)
            {
                goto Label_009A;
            }
            this.Error();
            return;
        Label_009A:
            return;
        }

        protected override void OnDestroy()
        {
            this.RemoveDelegate();
            base.OnDestroy();
            return;
        }

        public void OnRequestProcessing()
        {
            base.ActivateOutputLinks(300);
            return;
        }

        public void OnRequestPurchase(PaymentManager.ERequestPurchaseResult result, PaymentManager.CoinRecord record)
        {
            if (result == 1)
            {
                goto Label_000E;
            }
            if (result != -1)
            {
                goto Label_0019;
            }
        Label_000E:
            this.Cancel();
            goto Label_00B5;
        Label_0019:
            if (result != 2)
            {
                goto Label_002B;
            }
            this.AlreadyOwn();
            goto Label_00B5;
        Label_002B:
            if (result != 3)
            {
                goto Label_003D;
            }
            this.Deferred();
            goto Label_00B5;
        Label_003D:
            if (result != 4)
            {
                goto Label_004F;
            }
            this.InsufficientBalances();
            goto Label_00B5;
        Label_004F:
            if (result != 5)
            {
                goto Label_0061;
            }
            this.OverLimited();
            goto Label_00B5;
        Label_0061:
            if (result != 6)
            {
                goto Label_0073;
            }
            this.NeedBirthday();
            goto Label_00B5;
        Label_0073:
            if (result != null)
            {
                goto Label_00AF;
            }
            if (record == null)
            {
                goto Label_00A4;
            }
            MonoSingleton<GameManager>.Instance.Player.SetCoinPurchaseResult(record);
            MonoSingleton<GameManager>.Instance.Player.GainVipPoint(record.additionalPaidCoin);
        Label_00A4:
            this.Success();
            goto Label_00B5;
        Label_00AF:
            this.Error();
        Label_00B5:
            return;
        }

        private void OverLimited()
        {
            DebugUtility.LogWarning("RequestPurchase over limited");
            base.set_enabled(0);
            this.RemoveDelegate();
            this.CloseProcessingDialog();
            base.ActivateOutputLinks(0xcd);
            return;
        }

        private void RemoveDelegate()
        {
            PaymentManager local2;
            PaymentManager local1;
            if (this.mSetDelegate == null)
            {
                goto Label_0068;
            }
            local1 = MonoSingleton<PaymentManager>.Instance;
            local1.OnRequestPurchase = (PaymentManager.RequestPurchaseDelegate) Delegate.Remove(local1.OnRequestPurchase, new PaymentManager.RequestPurchaseDelegate(this.OnRequestPurchase));
            local2 = MonoSingleton<PaymentManager>.Instance;
            local2.OnRequestProcessing = (PaymentManager.RequestProcessingDelegate) Delegate.Remove(local2.OnRequestProcessing, new PaymentManager.RequestProcessingDelegate(this.OnRequestProcessing));
            this.mSetDelegate = 0;
            DebugUtility.Log("PaymentRequestPurchase.RemoveDelegate");
        Label_0068:
            return;
        }

        private void Success()
        {
            DebugUtility.LogWarning("RequestPurchase success");
            base.set_enabled(0);
            this.RemoveDelegate();
            this.CloseProcessingDialog();
            base.ActivateOutputLinks(100);
            return;
        }
    }
}

