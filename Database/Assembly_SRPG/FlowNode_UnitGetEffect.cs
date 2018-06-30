namespace SRPG
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;

    [NodeType("UI/UnitGetEffect", 0x7fe5), Pin(1, "開始", 0, 1), Pin(10, "終了", 1, 10)]
    public class FlowNode_UnitGetEffect : FlowNode
    {
        private UnitGetWindowController mWindow;

        public FlowNode_UnitGetEffect()
        {
            base..ctor();
            return;
        }

        public override void OnActivate(int pinID)
        {
            if (pinID != 1)
            {
                goto Label_0031;
            }
            this.mWindow = base.get_gameObject().AddComponent<UnitGetWindowController>();
            this.mWindow.Init(null);
            base.StartCoroutine(this.ShowEffect());
        Label_0031:
            return;
        }

        [DebuggerHidden]
        private IEnumerator ShowEffect()
        {
            <ShowEffect>c__IteratorCE rce;
            rce = new <ShowEffect>c__IteratorCE();
            rce.<>f__this = this;
            return rce;
        }

        [CompilerGenerated]
        private sealed class <ShowEffect>c__IteratorCE : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal int $PC;
            internal object $current;
            internal FlowNode_UnitGetEffect <>f__this;

            public <ShowEffect>c__IteratorCE()
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
                        goto Label_0039;
                }
                goto Label_0063;
            Label_0021:
                goto Label_0039;
            Label_0026:
                this.$current = null;
                this.$PC = 1;
                goto Label_0065;
            Label_0039:
                if (this.<>f__this.mWindow.IsEnd == null)
                {
                    goto Label_0026;
                }
                this.<>f__this.ActivateOutputLinks(10);
                this.$PC = -1;
            Label_0063:
                return 0;
            Label_0065:
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

