namespace SRPG
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using UnityEngine;
    using UnityEngine.UI;

    [Pin(0, "Input", 0, 0), Pin(10, "Output", 1, 10), NodeType("UI/ResetScrollPosition", 0x7fe5)]
    public class FlowNode_ResetScrollPosition : FlowNode
    {
        [SerializeField]
        private ScrollRect ScrollParent;
        [SerializeField]
        private Transform ResetTransform;
        private float mDecelerationRate;

        public FlowNode_ResetScrollPosition()
        {
            base..ctor();
            return;
        }

        public override void OnActivate(int pinID)
        {
            if (pinID != null)
            {
                goto Label_0014;
            }
            this.ResetScrollPosition();
            FlowNode_GameObject.ActivateOutputLinks(this, 10);
        Label_0014:
            return;
        }

        [DebuggerHidden]
        private IEnumerator RefreshScrollRect()
        {
            <RefreshScrollRect>c__IteratorC7 rc;
            rc = new <RefreshScrollRect>c__IteratorC7();
            rc.<>f__this = this;
            return rc;
        }

        private unsafe void ResetScrollPosition()
        {
            RectTransform transform;
            Vector2 vector;
            if ((this.ScrollParent == null) == null)
            {
                goto Label_0012;
            }
            return;
        Label_0012:
            this.mDecelerationRate = this.ScrollParent.get_decelerationRate();
            this.ScrollParent.set_decelerationRate(0f);
            transform = this.ResetTransform as RectTransform;
            transform.set_anchoredPosition(new Vector2(&transform.get_anchoredPosition().x, 0f));
            base.StartCoroutine(this.RefreshScrollRect());
            return;
        }

        [CompilerGenerated]
        private sealed class <RefreshScrollRect>c__IteratorC7 : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal int $PC;
            internal object $current;
            internal FlowNode_ResetScrollPosition <>f__this;

            public <RefreshScrollRect>c__IteratorC7()
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
                        goto Label_0034;
                }
                goto Label_006C;
            Label_0021:
                this.$current = null;
                this.$PC = 1;
                goto Label_006E;
            Label_0034:
                if ((this.<>f__this.ScrollParent != null) == null)
                {
                    goto Label_0065;
                }
                this.<>f__this.ScrollParent.set_decelerationRate(this.<>f__this.mDecelerationRate);
            Label_0065:
                this.$PC = -1;
            Label_006C:
                return 0;
            Label_006E:
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

