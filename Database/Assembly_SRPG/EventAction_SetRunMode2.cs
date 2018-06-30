namespace SRPG
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using UnityEngine;

    [EventActionInfo("New/アクター/走りアニメーション変更", "ユニットの走りアニメーションを変更します。", 0x555555, 0x444488)]
    public class EventAction_SetRunMode2 : EventAction
    {
        [StringIsActorList]
        public string ActorID;
        public string AnimationName;

        public EventAction_SetRunMode2()
        {
            base..ctor();
            return;
        }

        public override void OnActivate()
        {
            GameObject obj2;
            TacticsUnitController controller;
            obj2 = EventAction.FindActor(this.ActorID);
            if ((obj2 != null) == null)
            {
                goto Label_0037;
            }
            controller = obj2.GetComponent<TacticsUnitController>();
            if ((controller != null) == null)
            {
                goto Label_0037;
            }
            controller.SetRunAnimation(this.AnimationName);
        Label_0037:
            base.ActivateNext();
            return;
        }

        [DebuggerHidden]
        public override IEnumerator PreloadAssets()
        {
            <PreloadAssets>c__Iterator92 iterator;
            iterator = new <PreloadAssets>c__Iterator92();
            iterator.<>f__this = this;
            return iterator;
        }

        public override bool IsPreloadAssets
        {
            get
            {
                return 1;
            }
        }

        [CompilerGenerated]
        private sealed class <PreloadAssets>c__Iterator92 : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal GameObject <go>__0;
            internal TacticsUnitController <controller>__1;
            internal int $PC;
            internal object $current;
            internal EventAction_SetRunMode2 <>f__this;

            public <PreloadAssets>c__Iterator92()
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
                        goto Label_00B5;

                    case 2:
                        goto Label_00D8;
                }
                goto Label_00DF;
            Label_0025:
                if (string.IsNullOrEmpty(this.<>f__this.AnimationName) != null)
                {
                    goto Label_00C5;
                }
                this.<go>__0 = EventAction.FindActor(this.<>f__this.ActorID);
                if ((this.<go>__0 != null) == null)
                {
                    goto Label_00C5;
                }
                this.<controller>__1 = this.<go>__0.GetComponent<TacticsUnitController>();
                if ((this.<controller>__1 != null) == null)
                {
                    goto Label_00C5;
                }
                this.<controller>__1.LoadRunAnimation(this.<>f__this.AnimationName);
                goto Label_00B5;
            Label_009E:
                this.$current = new WaitForEndOfFrame();
                this.$PC = 1;
                goto Label_00E1;
            Label_00B5:
                if (this.<controller>__1.IsLoading != null)
                {
                    goto Label_009E;
                }
            Label_00C5:
                this.$current = null;
                this.$PC = 2;
                goto Label_00E1;
            Label_00D8:
                this.$PC = -1;
            Label_00DF:
                return 0;
            Label_00E1:
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

