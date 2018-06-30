namespace SRPG
{
    using GR;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using UnityEngine;

    [Pin(1, "Out", 1, 1), Pin(0, "In", 0, 0), NodeType("System/InitMySound", 0xffff)]
    public class FlowNode_InitMySound : FlowNode
    {
        public bool UseEmb;
        public bool ForceReInit;

        public FlowNode_InitMySound()
        {
            base..ctor();
            return;
        }

        private void Init()
        {
            MyCriManager.Setup(this.UseEmb);
            DebugUtility.LogWarning("[MyCriManager] Setup:" + ((bool) this.UseEmb));
            return;
        }

        public override void OnActivate(int pinID)
        {
            if (pinID != null)
            {
                goto Label_0071;
            }
            if (MyCriManager.IsInitialized() == null)
            {
                goto Label_0063;
            }
            if (this.ForceReInit != null)
            {
                goto Label_004E;
            }
            if (MyCriManager.UsingEmb != this.UseEmb)
            {
                goto Label_004E;
            }
            DebugUtility.LogWarning("[MyCriManager] NoNeed to Setup:" + ((bool) this.UseEmb));
            base.ActivateOutputLinks(1);
            return;
        Label_004E:
            base.set_enabled(1);
            base.StartCoroutine(this.Restart());
            return;
        Label_0063:
            this.Init();
            base.ActivateOutputLinks(1);
        Label_0071:
            return;
        }

        [DebuggerHidden]
        private IEnumerator Restart()
        {
            <Restart>c__IteratorB9 rb;
            rb = new <Restart>c__IteratorB9();
            rb.<>f__this = this;
            return rb;
        }

        [CompilerGenerated]
        private sealed class <Restart>c__IteratorB9 : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal int $PC;
            internal object $current;
            internal FlowNode_InitMySound <>f__this;

            public <Restart>c__IteratorB9()
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
                        goto Label_0047;
                }
                goto Label_00AA;
            Label_0021:
                MonoSingleton<MySound>.Instance.PrepareToClearCache();
                goto Label_0047;
            Label_0030:
                this.$current = new WaitForEndOfFrame();
                this.$PC = 1;
                goto Label_00AC;
            Label_0047:
                if (MonoSingleton<MySound>.Instance.IsReadyToClearCache() == null)
                {
                    goto Label_0030;
                }
                MyCriManager.ReSetup(this.<>f__this.UseEmb);
                MySound.RestoreFromClearCache();
                DebugUtility.LogWarning("[MyCriManager] ReSetup:" + ((bool) this.<>f__this.UseEmb));
                this.<>f__this.set_enabled(0);
                this.<>f__this.ActivateOutputLinks(1);
                this.$PC = -1;
            Label_00AA:
                return 0;
            Label_00AC:
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

