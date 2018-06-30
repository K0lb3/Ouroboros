namespace SRPG
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using UnityEngine;

    [EventActionInfo("New/アクター/表示切替", "シーン上のアクターの表示状態を切り替えます", 0x555555, 0x444488)]
    public class EventAction_ToggleActor2 : EventAction
    {
        [StringIsActorList]
        public string ActorID;
        public ToggleTypes Type;
        private GameObject mSummonEffect;

        public EventAction_ToggleActor2()
        {
            base..ctor();
            return;
        }

        public override void OnActivate()
        {
            TacticsUnitController controller;
            GameObject obj2;
            ToggleTypes types;
            controller = TacticsUnitController.FindByUniqueName(this.ActorID);
            switch (this.Type)
            {
                case 0:
                    goto Label_002A;

                case 1:
                    goto Label_0042;

                case 2:
                    goto Label_0042;
            }
            goto Label_009E;
        Label_002A:
            if ((controller != null) == null)
            {
                goto Label_009E;
            }
            controller.SetVisible(0);
            goto Label_009E;
        Label_0042:
            if ((controller != null) == null)
            {
                goto Label_009E;
            }
            controller.SetVisible(1);
            if ((this.mSummonEffect != null) == null)
            {
                goto Label_009E;
            }
            obj2 = Object.Instantiate(this.mSummonEffect, controller.get_transform().get_position(), controller.get_transform().get_rotation()) as GameObject;
            GameUtility.RequireComponent<OneShotParticle>(obj2.get_gameObject());
        Label_009E:
            base.ActivateNext();
            return;
        }

        [DebuggerHidden]
        public override IEnumerator PreloadAssets()
        {
            <PreloadAssets>c__Iterator9C iteratorc;
            iteratorc = new <PreloadAssets>c__Iterator9C();
            iteratorc.<>f__this = this;
            return iteratorc;
        }

        public override bool IsPreloadAssets
        {
            get
            {
                return 1;
            }
        }

        [CompilerGenerated]
        private sealed class <PreloadAssets>c__Iterator9C : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal LoadRequest <req>__0;
            internal int $PC;
            internal object $current;
            internal EventAction_ToggleActor2 <>f__this;

            public <PreloadAssets>c__Iterator9C()
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
                        goto Label_006F;
                }
                goto Label_0091;
            Label_0021:
                if (this.<>f__this.Type != 2)
                {
                    goto Label_008A;
                }
                this.<req>__0 = GameUtility.LoadResourceAsyncChecked<GameObject>("Effects/eff_p_charsummon0");
                if (this.<req>__0.isDone != null)
                {
                    goto Label_006F;
                }
                this.$current = this.<req>__0.StartCoroutine();
                this.$PC = 1;
                goto Label_0093;
            Label_006F:
                this.<>f__this.mSummonEffect = this.<req>__0.asset as GameObject;
            Label_008A:
                this.$PC = -1;
            Label_0091:
                return 0;
            Label_0093:
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

        public enum ToggleTypes
        {
            Hide,
            Show,
            Summon
        }
    }
}

