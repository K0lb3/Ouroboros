namespace SRPG
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using UnityEngine;

    [EventActionInfo("New/アクター/走りアニメーション変更2", "ユニットの走りアニメーションを変更します。", 0x555555, 0x444488)]
    public class EventAction_SetRunMode3 : EventAction
    {
        private const string MOVIE_PATH = "Movies/";
        private const string DEMO_PATH = "Demo/";
        public PREFIX_PATH Path;
        [StringIsActorList]
        public string ActorID;
        public string AnimationName;

        public EventAction_SetRunMode3()
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
            <PreloadAssets>c__Iterator93 iterator;
            iterator = new <PreloadAssets>c__Iterator93();
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
        private sealed class <PreloadAssets>c__Iterator93 : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal GameObject <go>__0;
            internal string <path>__1;
            internal TacticsUnitController <controller>__2;
            internal int $PC;
            internal object $current;
            internal EventAction_SetRunMode3 <>f__this;

            public <PreloadAssets>c__Iterator93()
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
                        goto Label_0101;

                    case 2:
                        goto Label_0124;
                }
                goto Label_012B;
            Label_0025:
                if (string.IsNullOrEmpty(this.<>f__this.AnimationName) != null)
                {
                    goto Label_0111;
                }
                this.<go>__0 = EventAction.FindActor(this.<>f__this.ActorID);
                if ((this.<go>__0 != null) == null)
                {
                    goto Label_0111;
                }
                this.<path>__1 = (this.<>f__this.Path != null) ? "Movies/" : "Demo/";
                this.<path>__1 = this.<path>__1 + "CHM/" + this.<>f__this.AnimationName;
                this.<controller>__2 = this.<go>__0.GetComponent<TacticsUnitController>();
                if ((this.<controller>__2 != null) == null)
                {
                    goto Label_0111;
                }
                this.<controller>__2.LoadRunAnimation(this.<>f__this.AnimationName, this.<path>__1);
                goto Label_0101;
            Label_00EA:
                this.$current = new WaitForEndOfFrame();
                this.$PC = 1;
                goto Label_012D;
            Label_0101:
                if (this.<controller>__2.IsLoading != null)
                {
                    goto Label_00EA;
                }
            Label_0111:
                this.$current = null;
                this.$PC = 2;
                goto Label_012D;
            Label_0124:
                this.$PC = -1;
            Label_012B:
                return 0;
            Label_012D:
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

        public enum PREFIX_PATH
        {
            Demo,
            Movie
        }
    }
}

