namespace SRPG
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using UnityEngine;

    [EventActionInfo("New/アクター/アニメーション再生", "ユニットにアニメーションを再生させます。", 0x664444, 0xaa4444)]
    public class EventAction_PlayAnimation2 : EventAction
    {
        [StringIsActorList]
        public string ActorID;
        [HideInInspector]
        public string AnimationName;
        public AnimationTypes AnimationType;
        public float Delay;
        public float Interp;
        [HideInInspector]
        public bool Loop;
        public bool Async;
        [HideInInspector]
        public bool ApplyRootBoneAtEnd;
        private string mAnimationID;
        private TacticsUnitController mController;

        public EventAction_PlayAnimation2()
        {
            this.Interp = 0.1f;
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
                goto Label_00A1;
            }
            this.mController.RootMotionMode = 1;
            this.mController.PlayAnimation(this.mAnimationID, this.Loop, this.Interp, 0f);
        Label_00A1:
            if (this.Async == null)
            {
                goto Label_00B2;
            }
            base.ActivateNext();
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
            <PreloadAssets>c__Iterator8E iteratore;
            iteratore = new <PreloadAssets>c__Iterator8E();
            iteratore.<>f__this = this;
            return iteratore;
        }

        public override void Update()
        {
            Transform transform;
            if (this.Delay <= 0f)
            {
                goto Label_00CE;
            }
            this.Delay -= Time.get_deltaTime();
            if (this.Delay > 0f)
            {
                goto Label_01C2;
            }
            if (string.IsNullOrEmpty(this.mAnimationID) != null)
            {
                goto Label_0090;
            }
            if (this.AnimationType != null)
            {
                goto Label_0090;
            }
            if (string.IsNullOrEmpty(this.AnimationName) != null)
            {
                goto Label_0090;
            }
            this.mController.RootMotionMode = 1;
            this.mController.PlayAnimation(this.mAnimationID, this.Loop, this.Interp, 0f);
            goto Label_00C9;
        Label_0090:
            if (this.AnimationType != 1)
            {
                goto Label_01C2;
            }
            this.mController.PlayIdle(0f);
            if (this.Async != null)
            {
                goto Label_00C2;
            }
            base.ActivateNext();
            goto Label_00C9;
        Label_00C2:
            base.enabled = 0;
        Label_00C9:
            goto Label_01C2;
        Label_00CE:
            if (this.mController.GetRemainingTime(this.mAnimationID) > 0f)
            {
                goto Label_01C2;
            }
            if (this.ApplyRootBoneAtEnd == null)
            {
                goto Label_01A5;
            }
            if (this.Loop != null)
            {
                goto Label_01A5;
            }
            this.mController.StopAnimation(this.mAnimationID);
            transform = GameUtility.findChildRecursively(this.mController.get_transform(), this.mController.RootMotionBoneName);
            this.mController.get_transform().set_position(transform.get_position());
            transform.set_localPosition(new Vector3(0f, 0f, 0f));
            this.mController.get_transform().set_rotation(transform.get_rotation() * Quaternion.Euler(90f, 0f, 0f));
            transform.set_localRotation(Quaternion.Euler(270f, 0f, 0f));
        Label_01A5:
            if (this.Async != null)
            {
                goto Label_01BB;
            }
            base.ActivateNext();
            goto Label_01C2;
        Label_01BB:
            base.enabled = 0;
        Label_01C2:
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
        private sealed class <PreloadAssets>c__Iterator8E : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal GameObject <go>__0;
            internal string <path>__1;
            internal int $PC;
            internal object $current;
            internal EventAction_PlayAnimation2 <>f__this;

            public <PreloadAssets>c__Iterator8E()
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

