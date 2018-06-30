namespace SRPG
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using UnityEngine;
    using UnityEngine.UI;

    public class SortMenuButton_Unitlist : SortMenuButton
    {
        public Image FilterButton;
        public Sprite Active;
        public Sprite NonActive;
        public Text Msg;
        private Coroutine mCoroutine;
        private bool mRequest;
        private bool mFlag;

        public SortMenuButton_Unitlist()
        {
            base..ctor();
            return;
        }

        protected override void OnEnable()
        {
        }

        protected override void Start()
        {
            base.Start();
            return;
        }

        private void Update()
        {
            if (this.mRequest == null)
            {
                goto Label_0035;
            }
            if (this.mCoroutine != null)
            {
                goto Label_0035;
            }
            this.mCoroutine = base.StartCoroutine(this.UpdateState(this.mFlag));
            this.mRequest = 0;
        Label_0035:
            return;
        }

        protected override void UpdateFilterState(bool active)
        {
            this.mFlag = active;
            this.mRequest = 1;
            return;
        }

        [DebuggerHidden]
        private IEnumerator UpdateState(bool active)
        {
            <UpdateState>c__Iterator13D iteratord;
            iteratord = new <UpdateState>c__Iterator13D();
            iteratord.active = active;
            iteratord.<$>active = active;
            iteratord.<>f__this = this;
            return iteratord;
        }

        [CompilerGenerated]
        private sealed class <UpdateState>c__Iterator13D : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal bool active;
            internal Exception <e>__0;
            internal int $PC;
            internal object $current;
            internal bool <$>active;
            internal SortMenuButton_Unitlist <>f__this;

            public <UpdateState>c__Iterator13D()
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
                Exception exception;
                bool flag;
                num = this.$PC;
                this.$PC = -1;
                switch (num)
                {
                    case 0:
                        goto Label_0025;

                    case 1:
                        goto Label_0041;

                    case 2:
                        goto Label_011D;
                }
                goto Label_0124;
            Label_0025:
                this.$current = new WaitForSeconds(0.5f);
                this.$PC = 1;
                goto Label_0126;
            Label_0041:
                try
                {
                    if ((this.<>f__this.FilterButton != null) == null)
                    {
                        goto Label_008D;
                    }
                    this.<>f__this.FilterButton.set_sprite((this.active == null) ? this.<>f__this.NonActive : this.<>f__this.Active);
                Label_008D:
                    if ((this.<>f__this.Msg != null) == null)
                    {
                        goto Label_00D2;
                    }
                    this.<>f__this.Msg.set_text(LocalizedText.Get((this.active == null) ? "sys.BTN_SORTFILTER" : "sys.BTN_SORTFILTER2"));
                Label_00D2:
                    goto Label_00FE;
                }
                catch (Exception exception1)
                {
                Label_00D7:
                    exception = exception1;
                    this.<e>__0 = exception;
                    Debug.LogError("UnitList:" + this.<e>__0.Message);
                    goto Label_00FE;
                }
            Label_00FE:
                this.<>f__this.mCoroutine = null;
                this.$current = null;
                this.$PC = 2;
                goto Label_0126;
            Label_011D:
                this.$PC = -1;
            Label_0124:
                return 0;
            Label_0126:
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

