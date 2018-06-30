namespace SRPG
{
    using GR;
    using System;

    [NodeType("Payment/CheckChargeLimit", 0x7fe5), Pin(100, "Success", 1, 100), Pin(200, "Error", 1, 200), Pin(0xc9, "NonAge", 1, 0xc9), Pin(0xca, "NeedCheck", 1, 0xca), Pin(0xcb, "NeedBirthday", 1, 0xcb), Pin(0xcc, "LimitOver", 1, 0xcc), Pin(0xcd, "VipRemains", 1, 0xcd), Pin(0, "Start", 0, 0)]
    public class FlowNode_PaymentCheckChargeLimit : FlowNode_Network
    {
        private bool mSetDelegate;

        public FlowNode_PaymentCheckChargeLimit()
        {
            base..ctor();
            return;
        }

        private void Failure()
        {
            DebugUtility.Log("CheckChargeLimit failure");
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

        private void LimitOver()
        {
            DebugUtility.Log("CheckChargeLimit limitover");
            base.set_enabled(0);
            this.RemoveDelegate();
            base.ActivateOutputLinks(0xcc);
            return;
        }

        private void NeedBirthday()
        {
            DebugUtility.Log("CheckChargeLimit needbirthday");
            base.set_enabled(0);
            this.RemoveDelegate();
            base.ActivateOutputLinks(0xcb);
            return;
        }

        private void NeedCheck()
        {
            DebugUtility.Log("CheckChargeLimit needcheck");
            base.set_enabled(0);
            this.RemoveDelegate();
            base.ActivateOutputLinks(0xca);
            return;
        }

        private void NonAge()
        {
            DebugUtility.Log("CheckChargeLimit nonage");
            base.set_enabled(0);
            this.RemoveDelegate();
            base.ActivateOutputLinks(0xc9);
            return;
        }

        public override void OnActivate(int pinID)
        {
            PaymentManager.Product[] productArray1;
            PaymentManager.Product product;
            if (pinID != null)
            {
                goto Label_0054;
            }
            if (Network.Mode != null)
            {
                goto Label_004E;
            }
            product = MonoSingleton<PaymentManager>.Instance.GetProduct(GlobalVars.SelectedProductID);
            productArray1 = new PaymentManager.Product[] { product };
            base.ExecRequest(new ReqChargeCheck(productArray1, 1, new Network.ResponseCallback(this.ResponseCallback)));
            base.set_enabled(1);
            goto Label_0054;
        Label_004E:
            this.Success();
        Label_0054:
            return;
        }

        public void OnCheckChargeLimit(PaymentManager.ECheckChargeLimitResult result)
        {
            if (result != null)
            {
                goto Label_0011;
            }
            this.Success();
            goto Label_005F;
        Label_0011:
            if (result != 1)
            {
                goto Label_0023;
            }
            this.NonAge();
            goto Label_005F;
        Label_0023:
            if (result != 2)
            {
                goto Label_0035;
            }
            this.NeedCheck();
            goto Label_005F;
        Label_0035:
            if (result != 4)
            {
                goto Label_0047;
            }
            this.NeedBirthday();
            goto Label_005F;
        Label_0047:
            if (result != 5)
            {
                goto Label_0059;
            }
            this.LimitOver();
            goto Label_005F;
        Label_0059:
            this.Failure();
        Label_005F:
            return;
        }

        protected override void OnDestroy()
        {
            this.RemoveDelegate();
            base.OnDestroy();
            return;
        }

        public override unsafe void OnSuccess(WWWResult www)
        {
            WebAPI.JSON_BodyResponse<JSON_ChargeCheckResponse> response;
            ChargeCheckData data;
            PaymentManager manager;
            Network.EErrCode code;
            if (Network.IsError == null)
            {
                goto Label_0054;
            }
            code = Network.ErrCode;
            if (code == 0x1fa5)
            {
                goto Label_002B;
            }
            if (code == 0x1fa6)
            {
                goto Label_003C;
            }
            goto Label_004D;
        Label_002B:
            Network.RemoveAPI();
            Network.ResetError();
            this.NeedBirthday();
            return;
        Label_003C:
            Network.RemoveAPI();
            Network.ResetError();
            this.VipRemains();
            return;
        Label_004D:
            this.Failure();
            return;
        Label_0054:
            response = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<JSON_ChargeCheckResponse>>(&www.text);
            DebugUtility.Assert((response == null) == 0, "res == null");
            Network.RemoveAPI();
            data = new ChargeCheckData();
            if (data.Deserialize(response.body) != null)
            {
                goto Label_0095;
            }
            this.Failure();
            return;
        Label_0095:
            manager = MonoSingleton<PaymentManager>.Instance;
            if (0 >= ((int) data.RejectIds.Length))
            {
                goto Label_00B0;
            }
            this.LimitOver();
            return;
        Label_00B0:
            if (20 <= data.Age)
            {
                goto Label_00C4;
            }
            this.NonAge();
            return;
        Label_00C4:
            if (manager.IsAgree() != null)
            {
                goto Label_00D6;
            }
            this.NeedCheck();
            return;
        Label_00D6:
            this.Success();
            return;
        }

        private void RemoveDelegate()
        {
        }

        private void Success()
        {
            DebugUtility.Log("CheckChargeLimit success");
            base.set_enabled(0);
            this.RemoveDelegate();
            base.ActivateOutputLinks(100);
            return;
        }

        private void VipRemains()
        {
            DebugUtility.Log("CheckChargeLimit vipremains");
            base.set_enabled(0);
            this.RemoveDelegate();
            base.ActivateOutputLinks(0xcd);
            return;
        }
    }
}

