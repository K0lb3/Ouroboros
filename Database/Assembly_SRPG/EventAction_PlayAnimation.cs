namespace SRPG
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using UnityEngine;

    [EventActionInfo("アクター/アニメーション再生", "ユニットにアニメーションを再生させます。", 0x664444, 0xaa4444)]
    public class EventAction_PlayAnimation : EventAction
    {
        [StringIsActorID]
        public string ActorID;
        [HideInInspector]
        public string AnimationName;
        public AnimationTypes AnimationType;
        [HideInInspector]
        public bool Loop;
        public bool Async;
        private string mAnimationID;
        private TacticsUnitController mController;

        public EventAction_PlayAnimation()
        {
            base..ctor();
            return;
        }

        public override void GoToEndState()
        {
            if ((this.mController != null) == null)
            {
                goto Label_0089;
            }
            if (string.IsNullOrEmpty(this.mAnimationID) != null)
            {
                goto Label_006D;
            }
            if (this.AnimationType != null)
            {
                goto Label_006D;
            }
            if (string.IsNullOrEmpty(this.AnimationName) != null)
            {
                goto Label_006D;
            }
            this.mController.PlayAnimation(this.mAnimationID, this.Loop, -0.1f, 0f);
            this.mController.SkipToAnimationEnd();
            goto Label_0089;
        Label_006D:
            if (this.AnimationType != 1)
            {
                goto Label_0089;
            }
            this.mController.PlayIdle(-1f);
        Label_0089:
            return;
        }

        public override void OnActivate()
        {
            if ((this.mController != null) == null)
            {
                goto Label_0097;
            }
            if (string.IsNullOrEmpty(this.mAnimationID) != null)
            {
                goto Label_007B;
            }
            if (this.AnimationType != null)
            {
                goto Label_007B;
            }
            if (string.IsNullOrEmpty(this.AnimationName) != null)
            {
                goto Label_007B;
            }
            this.mController.RootMotionMode = 1;
            this.mController.PlayAnimation(this.mAnimationID, this.Loop, 0.1f, 0f);
            if (this.Async == null)
            {
                goto Label_007A;
            }
            base.ActivateNext();
        Label_007A:
            return;
        Label_007B:
            if (this.AnimationType != 1)
            {
                goto Label_0097;
            }
            this.mController.PlayIdle(0f);
        Label_0097:
            base.ActivateNext();
            return;
        }

        protected override void OnDestroy()
        {
            if ((this.mController != null) == null)
            {
                goto Label_0032;
            }
            if (string.IsNullOrEmpty(this.mAnimationID) != null)
            {
                goto Label_0032;
            }
            this.mController.UnloadAnimation(this.mAnimationID);
        Label_0032:
            return;
        }

        [DebuggerHidden]
        public override IEnumerator PreloadAssets()
        {
            <PreloadAssets>c__Iterator8D iteratord;
            iteratord = new <PreloadAssets>c__Iterator8D();
            iteratord.<>f__this = this;
            return iteratord;
        }

        public override void Update()
        {
            if (this.mController.GetRemainingTime(this.mAnimationID) > 0f)
            {
                goto Label_0021;
            }
            base.ActivateNext();
        Label_0021:
            return;
        }

        public override bool IsPreloadAssets
        {
            get
            {
                return 1;
            }
        }

        [CompilerGenerated]
        private sealed class <PreloadAssets>c__Iterator8D : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal GameObject <go>__0;
            internal string <path>__1;
            internal int $PC;
            internal object $current;
            internal EventAction_PlayAnimation <>f__this;

            public <PreloadAssets>c__Iterator8D()
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
                        goto Label_011C;

                    case 2:
                        goto Label_0168;
                }
                goto Label_016F;
            Label_0025:
                this.<go>__0 = EventAction.FindActor(this.<>f__this.ActorID);
                if ((this.<go>__0 != null) == null)
                {
                    goto Label_0136;
                }
                this.<>f__this.mController = this.<go>__0.GetComponent<TacticsUnitController>();
                if ((this.<>f__this.mController != null) == null)
                {
                    goto Label_0155;
                }
                if (this.<>f__this.AnimationType != null)
                {
                    goto Label_0155;
                }
                if (string.IsNullOrEmpty(this.<>f__this.AnimationName) != null)
                {
                    goto Label_0155;
                }
                this.<>f__this.mAnimationID = "tmp_" + Convert.ToString(this.<>f__this.GetInstanceID(), 0x10);
                this.<path>__1 = "CHM/" + this.<>f__this.AnimationName;
                this.<>f__this.mController.LoadAnimationAsync(this.<>f__this.mAnimationID, this.<path>__1);
                goto Label_011C;
            Label_0105:
                this.$current = new WaitForEndOfFrame();
                this.$PC = 1;
                goto Label_0171;
            Label_011C:
                if (this.<>f__this.mController.IsLoading != null)
                {
                    goto Label_0105;
                }
                goto Label_0155;
            Label_0136:
                Debug.LogError("アクター'" + this.<>f__this.ActorID + "'は存在しません。");
            Label_0155:
                this.$current = null;
                this.$PC = 2;
                goto Label_0171;
            Label_0168:
                this.$PC = -1;
            Label_016F:
                return 0;
            Label_0171:
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

        public enum AnimationTypes
        {
            Custom,
            Idle
        }
    }
}

