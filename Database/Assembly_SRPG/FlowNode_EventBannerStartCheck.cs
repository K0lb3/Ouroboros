namespace SRPG
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using UnityEngine;

    [Pin(1, "Check", 0, 1), NodeType("SRPG/EventBannerStartCheck", 0x7fe5), Pin(10, "Finished", 1, 10)]
    public class FlowNode_EventBannerStartCheck : FlowNode
    {
        [SerializeField]
        private AppealItemLimitedShop LimitedShopAppealItem;

        public FlowNode_EventBannerStartCheck()
        {
            base..ctor();
            return;
        }

        public override void OnActivate(int pinID)
        {
            if (pinID != 1)
            {
                goto Label_0014;
            }
            base.StartCoroutine(this.UpdateEventBanner());
        Label_0014:
            return;
        }

        [DebuggerHidden]
        private IEnumerator UpdateEventBanner()
        {
            <UpdateEventBanner>c__IteratorB5 rb;
            rb = new <UpdateEventBanner>c__IteratorB5();
            rb.<>f__this = this;
            return rb;
        }

        [CompilerGenerated]
        private sealed class <UpdateEventBanner>c__IteratorB5 : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal int $PC;
            internal object $current;
            internal FlowNode_EventBannerStartCheck <>f__this;

            public <UpdateEventBanner>c__IteratorB5()
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
                        goto Label_0021;

                    case 1:
                        goto Label_0077;
                }
                goto Label_00A1;
            Label_0021:
                if ((this.<>f__this.LimitedShopAppealItem == null) != null)
                {
                    goto Label_004C;
                }
                if (this.<>f__this.LimitedShopAppealItem.get_isActiveAndEnabled() != null)
                {
                    goto Label_0077;
                }
            Label_004C:
                this.<>f__this.ActivateOutputLinks(10);
                goto Label_00A1;
                goto Label_0077;
            Label_0064:
                this.$current = null;
                this.$PC = 1;
                goto Label_00A3;
            Label_0077:
                if (this.<>f__this.LimitedShopAppealItem.IsInitialized == null)
                {
                    goto Label_0064;
                }
                this.<>f__this.ActivateOutputLinks(10);
                this.$PC = -1;
            Label_00A1:
                return 0;
            Label_00A3:
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

