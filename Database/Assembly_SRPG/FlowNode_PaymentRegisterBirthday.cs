namespace SRPG
{
    using GR;
    using System;

    [Pin(0, "Start", 0, 0), Pin(100, "Success", 1, 100), Pin(200, "Error", 1, 200), NodeType("Payment/RegisterBirthday", 0x7fe5)]
    public class FlowNode_PaymentRegisterBirthday : FlowNode
    {
        private bool mSetDelegate;

        public FlowNode_PaymentRegisterBirthday()
        {
            base..ctor();
            return;
        }

        private void Failure()
        {
            DebugUtility.Log("RegisterBirthday failure");
            base.set_enabled(0);
            this.RemoveDelegate();
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

        public override void OnActivate(int pinID)
        {
            PaymentManager local1;
            if (pinID != null)
            {
                goto Label_006A;
            }
            if (this.mSetDelegate != null)
            {
                goto Label_003E;
            }
            local1 = MonoSingleton<PaymentManager>.Instance;
            local1.OnRegisterBirthday = (PaymentManager.RegisterBirthdayDelegate) Delegate.Combine(local1.OnRegisterBirthday, new PaymentManager.RegisterBirthdayDelegate(this.OnRegisterBirthday));
            this.mSetDelegate = 1;
        Label_003E:
            base.set_enabled(1);
            if (MonoSingleton<PaymentManager>.Instance.RegisterBirthday(GlobalVars.EditedYear, GlobalVars.EditedMonth, GlobalVars.EditedDay) != null)
            {
                goto Label_006A;
            }
            this.Failure();
            return;
        Label_006A:
            return;
        }

        protected override void OnDestroy()
        {
            this.RemoveDelegate();
            base.OnDestroy();
            return;
        }

        public void OnRegisterBirthday(PaymentManager.ERegisterBirthdayResult result)
        {
            if (result != null)
            {
                goto Label_0011;
            }
            this.Success();
            goto Label_0017;
        Label_0011:
            this.Failure();
        Label_0017:
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
            local1.OnRegisterBirthday = (PaymentManager.RegisterBirthdayDelegate) Delegate.Remove(local1.OnRegisterBirthday, new PaymentManager.RegisterBirthdayDelegate(this.OnRegisterBirthday));
            this.mSetDelegate = 0;
        Label_0038:
            return;
        }

        private void Success()
        {
            DebugUtility.Log("RegisterBirthday");
            base.set_enabled(0);
            this.RemoveDelegate();
            base.ActivateOutputLinks(100);
            return;
        }
    }
}

