namespace SRPG
{
    using GR;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;

    [Pin(1, "Success", 1, 1), Pin(0, "Request", 0, 0), NodeType("System/ReqProductParam", 0x7fe5), Pin(2, "Failed", 1, 2)]
    public class FlowNode_ReqProductParam : FlowNode_Network
    {
        public bool IsLoginBefore;

        public FlowNode_ReqProductParam()
        {
            this.IsLoginBefore = 1;
            base..ctor();
            return;
        }

        [DebuggerHidden]
        private IEnumerator CheckPaymentInit(ProductParamResponse param)
        {
            <CheckPaymentInit>c__IteratorC5 rc;
            rc = new <CheckPaymentInit>c__IteratorC5();
            rc.param = param;
            rc.<$>param = param;
            rc.<>f__this = this;
            return rc;
        }

        private void Failure()
        {
            base.set_enabled(0);
            base.ActivateOutputLinks(2);
            return;
        }

        public override void OnActivate(int pinID)
        {
            if (pinID != null)
            {
                goto Label_0039;
            }
            if (Network.Mode != null)
            {
                goto Label_0033;
            }
            base.ExecRequest(new ReqProductParam(new Network.ResponseCallback(this.ResponseCallback)));
            base.set_enabled(1);
            goto Label_0039;
        Label_0033:
            this.Success();
        Label_0039:
            return;
        }

        public override unsafe void OnSuccess(WWWResult www)
        {
            WebAPI.JSON_BodyResponse<JSON_ProductParamResponse> response;
            ProductParamResponse response2;
            Network.EErrCode code;
            if ((this == null) == null)
            {
                goto Label_0012;
            }
            Network.RemoveAPI();
            return;
        Label_0012:
            if (Network.IsError == null)
            {
                goto Label_0029;
            }
            code = Network.ErrCode;
            this.OnFailed();
            return;
        Label_0029:
            response = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<JSON_ProductParamResponse>>(&www.text);
            DebugUtility.Assert((response == null) == 0, "res == null");
            Network.RemoveAPI();
            response2 = new ProductParamResponse();
            if (response2.Deserialize(response.body) != null)
            {
                goto Label_006A;
            }
            this.Failure();
            return;
        Label_006A:
            base.StartCoroutine(this.CheckPaymentInit(response2));
            return;
        }

        private void Success()
        {
            base.set_enabled(0);
            base.ActivateOutputLinks(1);
            return;
        }

        [CompilerGenerated]
        private sealed class <CheckPaymentInit>c__IteratorC5 : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal ProductParamResponse param;
            internal int $PC;
            internal object $current;
            internal ProductParamResponse <$>param;
            internal FlowNode_ReqProductParam <>f__this;

            public <CheckPaymentInit>c__IteratorC5()
            {
                base..ctor();
                return;
            }

            [DebuggerHidden]
            public void Dispose()
            {
                this.$PC = -1;
                return;
            }

            public bool MoveNext()
            {
                uint num;
                bool flag;
                num = this.$PC;
                this.$PC = -1;
                switch (num)
                {
                    case 0:
                        goto Label_0025;

                    case 1:
                        goto Label_0038;

                    case 2:
                        goto Label_0077;
                }
                goto Label_0098;
            Label_0025:
                this.$current = null;
                this.$PC = 1;
                goto Label_009A;
            Label_0038:
                if (this.<>f__this.IsLoginBefore == null)
                {
                    goto Label_0077;
                }
                MonoSingleton<PaymentManager>.Instance.Init(1, this.param);
                goto Label_0086;
                goto Label_0077;
            Label_0064:
                this.$current = null;
                this.$PC = 2;
                goto Label_009A;
            Label_0077:
                if (MonoSingleton<PaymentManager>.Instance.IsAvailable == null)
                {
                    goto Label_0064;
                }
            Label_0086:
                this.<>f__this.Success();
                this.$PC = -1;
            Label_0098:
                return 0;
            Label_009A:
                return 1;
                return flag;
            }

            [DebuggerHidden]
            public void Reset()
            {
                throw new NotSupportedException();
            }

            object IEnumerator<object>.Current
            {
                [DebuggerHidden]
                get
                {
                    return this.$current;
                }
            }

            object IEnumerator.Current
            {
                [DebuggerHidden]
                get
                {
                    return this.$current;
                }
            }
        }
    }
}

