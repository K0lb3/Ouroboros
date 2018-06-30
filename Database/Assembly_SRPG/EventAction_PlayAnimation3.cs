namespace SRPG
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using UnityEngine;

    [EventActionInfo("New/アクター/アニメーション再生2", "ユニットにアニメーションを再生させます。", 0x664444, 0xaa4444)]
    public class EventAction_PlayAnimation3 : EventAction
    {
        private const string MOVIE_PATH = "Movies/";
        private const string DEMO_PATH = "Demo/";
        [StringIsActorList]
        public string ActorID;
        [HideInInspector]
        public PREFIX_PATH Path;
        [HideInInspector]
        public string AnimationName;
        public AnimationTypes AnimationType;
        public float Delay;
        [HideInInspector]
        public bool Loop;
        public bool Async;
        private string mAnimationID;
        private TacticsUnitController mController;

        public EventAction_PlayAnimation3()
        {
            base..ctor();
            return;
        }

        public override void OnActivate()
        {
            if ((this.mController != null) == null)
            {
                goto Label_0118;
            }
            if (string.IsNullOrEmpty(this.mAnimationID) != null)
            {
                goto Label_00B3;
            }
            if (this.AnimationType != null)
            {
                goto Label_00B3;
            }
            if (string.IsNullOrEmpty(this.AnimationName) != null)
            {
                goto Label_00B3;
            }
            if (this.Delay <= 0f)
            {
                goto Label_0063;
            }
            if (this.Async == null)
            {
                goto Label_0063;
            }
            base.ActivateNext(1);
            goto Label_00B2;
        Label_0063:
            if (this.Delay > 0f)
            {
                goto Label_00A0;
            }
            this.mController.RootMotionMode = 1;
            this.mController.PlayAnimation(this.mAnimationID, this.Loop, 0.1f, 0f);
        Label_00A0:
            if (this.Async == null)
            {
                goto Label_00B2;
            }
            base.ActivateNext(1);
        Label_00B2:
            return;
        Label_00B3:
            if (this.AnimationType != 1)
            {
                goto Label_0118;
            }
            if (this.Delay <= 0f)
            {
                goto Label_00E6;
            }
            if (this.Async == null)
            {
                goto Label_00E6;
            }
            base.ActivateNext(1);
            goto Label_0117;
        Label_00E6:
            if (this.Delay > 0f)
            {
                goto Label_0106;
            }
            this.mController.PlayIdle(0f);
        Label_0106:
            if (this.Async == null)
            {
                goto Label_0117;
            }
            base.ActivateNext();
        Label_0117:
            return;
        Label_0118:
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
            <PreloadAssets>c__Iterator8F iteratorf;
            iteratorf = new <PreloadAssets>c__Iterator8F();
            iteratorf.<>f__this = this;
            return iteratorf;
        }

        public override void Update()
        {
            if (this.Delay <= 0f)
            {
                goto Label_00CD;
            }
            this.Delay -= Time.get_deltaTime();
            if (this.Delay > 0f)
            {
                goto Label_0105;
            }
            if (string.IsNullOrEmpty(this.mAnimationID) != null)
            {
                goto Label_008F;
            }
            if (this.AnimationType != null)
            {
                goto Label_008F;
            }
            if (string.IsNullOrEmpty(this.AnimationName) != null)
            {
                goto Label_008F;
            }
            this.mController.RootMotionMode = 1;
            this.mController.PlayAnimation(this.mAnimationID, this.Loop, 0.1f, 0f);
            goto Label_00C8;
        Label_008F:
            if (this.AnimationType != 1)
            {
                goto Label_0105;
            }
            this.mController.PlayIdle(0f);
            if (this.Async != null)
            {
                goto Label_00C1;
            }
            base.ActivateNext();
            goto Label_00C8;
        Label_00C1:
            base.enabled = 0;
        Label_00C8:
            goto Label_0105;
        Label_00CD:
            if (this.mController.GetRemainingTime(this.mAnimationID) > 0f)
            {
                goto Label_0105;
            }
            if (this.Async != null)
            {
                goto Label_00FE;
            }
            base.ActivateNext();
            goto Label_0105;
        Label_00FE:
            base.enabled = 0;
        Label_0105:
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
        private sealed class <PreloadAssets>c__Iterator8F : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal GameObject <go>__0;
            internal string <path>__1;
            internal int $PC;
            internal object $current;
            internal EventAction_PlayAnimation3 <>f__this;

            public <PreloadAssets>c__Iterator8F()
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
                        goto Label_017F;

                    case 2:
                        goto Label_01CB;
                }
                goto Label_01D2;
            Label_0025:
                this.<go>__0 = EventAction.FindActor(this.<>f__this.ActorID);
                if ((this.<go>__0 != null) == null)
                {
                    goto Label_0199;
                }
                this.<>f__this.mController = this.<go>__0.GetComponent<TacticsUnitController>();
                if ((this.<>f__this.mController != null) == null)
                {
                    goto Label_01B8;
                }
                if (this.<>f__this.AnimationType != null)
                {
                    goto Label_01B8;
                }
                if (string.IsNullOrEmpty(this.<>f__this.AnimationName) != null)
                {
                    goto Label_01B8;
                }
                this.<>f__this.mAnimationID = "tmp_" + Convert.ToString(this.<>f__this.GetInstanceID(), 0x10);
                this.<path>__1 = string.Empty;
                if (this.<>f__this.Path != 1)
                {
                    goto Label_00FB;
                }
                this.<path>__1 = this.<path>__1 + "Movies/";
                goto Label_0121;
            Label_00FB:
                if (this.<>f__this.Path != null)
                {
                    goto Label_0121;
                }
                this.<path>__1 = this.<path>__1 + "Demo/";
            Label_0121:
                this.<path>__1 = this.<path>__1 + "CHM/" + this.<>f__this.AnimationName;
                this.<>f__this.mController.LoadAnimationAsync(this.<>f__this.mAnimationID, this.<path>__1);
                goto Label_017F;
            Label_0168:
                this.$current = new WaitForEndOfFrame();
                this.$PC = 1;
                goto Label_01D4;
            Label_017F:
                if (this.<>f__this.mController.IsLoading != null)
                {
                    goto Label_0168;
                }
                goto Label_01B8;
            Label_0199:
                Debug.LogError("アクター'" + this.<>f__this.ActorID + "'は存在しません。");
            Label_01B8:
                this.$current = null;
                this.$PC = 2;
                goto Label_01D4;
            Label_01CB:
                this.$PC = -1;
            Label_01D2:
                return 0;
            Label_01D4:
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

        public enum PREFIX_PATH
        {
            Demo,
            Movie,
            Default
        }
    }
}

