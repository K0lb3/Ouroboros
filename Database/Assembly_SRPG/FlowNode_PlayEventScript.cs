namespace SRPG
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using UnityEngine;

    [NodeType("SRPG/Play Event Script", 0x7fe5), Pin(10, "Finished", 1, 10), Pin(1, "Start", 0, 1)]
    public class FlowNode_PlayEventScript : FlowNode
    {
        public string ScriptID;

        public FlowNode_PlayEventScript()
        {
            base..ctor();
            return;
        }

        [DebuggerHidden]
        private IEnumerator LoadAndPlayAsync(string path)
        {
            <LoadAndPlayAsync>c__IteratorC2 rc;
            rc = new <LoadAndPlayAsync>c__IteratorC2();
            rc.path = path;
            rc.<$>path = path;
            rc.<>f__this = this;
            return rc;
        }

        public override void OnActivate(int pinID)
        {
            if (pinID != 1)
            {
                goto Label_0037;
            }
            if (base.get_enabled() == null)
            {
                goto Label_0013;
            }
            return;
        Label_0013:
            base.set_enabled(1);
            base.StartCoroutine(this.LoadAndPlayAsync("Events/" + this.ScriptID));
        Label_0037:
            return;
        }

        [CompilerGenerated]
        private sealed class <LoadAndPlayAsync>c__IteratorC2 : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal string path;
            internal LoadRequest <req>__0;
            internal EventScript <script>__1;
            internal EventScript.Sequence <seq>__2;
            internal int $PC;
            internal object $current;
            internal string <$>path;
            internal FlowNode_PlayEventScript <>f__this;

            public <LoadAndPlayAsync>c__IteratorC2()
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
                        goto Label_0063;

                    case 2:
                        goto Label_00EC;
                }
                goto Label_013E;
            Label_0025:
                this.<req>__0 = GameUtility.LoadResourceAsyncChecked<EventScript>(this.path);
                if (this.<req>__0.isDone != null)
                {
                    goto Label_0063;
                }
                this.$current = this.<req>__0.StartCoroutine();
                this.$PC = 1;
                goto Label_0140;
            Label_0063:
                if ((this.<req>__0.asset == null) == null)
                {
                    goto Label_0098;
                }
                this.<>f__this.set_enabled(0);
                this.<>f__this.ActivateOutputLinks(10);
                goto Label_013E;
            Label_0098:
                FadeController.Instance.ResetSceneFade(0f);
                this.<script>__1 = this.<req>__0.asset as EventScript;
                this.<seq>__2 = this.<script>__1.OnStart(0, 0);
                goto Label_00EC;
            Label_00D5:
                this.$current = new WaitForEndOfFrame();
                this.$PC = 2;
                goto Label_0140;
            Label_00EC:
                if ((this.<seq>__2 != null) == null)
                {
                    goto Label_010D;
                }
                if (this.<seq>__2.IsPlaying != null)
                {
                    goto Label_00D5;
                }
            Label_010D:
                Object.Destroy(this.<seq>__2.get_gameObject());
                this.<>f__this.set_enabled(0);
                this.<>f__this.ActivateOutputLinks(10);
                this.$PC = -1;
            Label_013E:
                return 0;
            Label_0140:
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

