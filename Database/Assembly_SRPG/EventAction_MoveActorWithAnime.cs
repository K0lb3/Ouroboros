namespace SRPG
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using UnityEngine;

    [EventActionInfo("New/アクター/移動(アニメーション再生付)", "アクターを指定パスに沿って移動させます。", 0x664444, 0xaa4444)]
    public class EventAction_MoveActorWithAnime : EventAction_MoveActor2
    {
        [HideInInspector]
        public string m_AnimationName;
        [HideInInspector]
        public bool m_Loop;
        public AnimeType m_AnimeType;
        private string m_AnimationID;

        public EventAction_MoveActorWithAnime()
        {
            base..ctor();
            return;
        }

        protected override void OnDestroy()
        {
            if ((base.mController != null) == null)
            {
                goto Label_0032;
            }
            if (string.IsNullOrEmpty(this.m_AnimationID) != null)
            {
                goto Label_0032;
            }
            base.mController.UnloadAnimation(this.m_AnimationID);
        Label_0032:
            return;
        }

        [DebuggerHidden]
        public override IEnumerator PreloadAssets()
        {
            <PreloadAssets>c__Iterator8B iteratorb;
            iteratorb = new <PreloadAssets>c__Iterator8B();
            iteratorb.<>f__this = this;
            return iteratorb;
        }

        public override void Update()
        {
            Debug.Log("mawa.update()");
            if (base.mReady != null)
            {
                goto Label_0050;
            }
            if ((base.mController != null) == null)
            {
                goto Label_0037;
            }
            if (base.mController.IsLoading == null)
            {
                goto Label_0037;
            }
            return;
        Label_0037:
            if (base.Async == null)
            {
                goto Label_0049;
            }
            base.ActivateNext(1);
        Label_0049:
            base.mReady = 1;
        Label_0050:
            if (base.mMoving != null)
            {
                goto Label_009D;
            }
            if (base.mController.IsLoading == null)
            {
                goto Label_006C;
            }
            return;
        Label_006C:
            if (base.Delay <= 0f)
            {
                goto Label_008F;
            }
            base.Delay -= Time.get_deltaTime();
            return;
        Label_008F:
            base.StartMove();
            base.mMoving = 1;
            return;
        Label_009D:
            if (base.LockRotation != null)
            {
                goto Label_00BE;
            }
            if (base.LockMotion != null)
            {
                goto Label_00BE;
            }
            if (base.GroundSnap != null)
            {
                goto Label_00CF;
            }
        Label_00BE:
            if (base.UpdateMove() == null)
            {
                goto Label_00E0;
            }
            return;
            goto Label_00E0;
        Label_00CF:
            if (base.mController.isMoving == null)
            {
                goto Label_00E0;
            }
            return;
        Label_00E0:
            if (base.GotoRealPosition == null)
            {
                goto Label_00F7;
            }
            base.mController.AutoUpdateRotation = 1;
        Label_00F7:
            if (this.m_AnimeType != null)
            {
                goto Label_0144;
            }
            if (string.IsNullOrEmpty(this.m_AnimationName) != null)
            {
                goto Label_0144;
            }
            base.mController.RootMotionMode = 1;
            base.mController.PlayAnimation(this.m_AnimationID, this.m_Loop, 0.1f, 0f);
            goto Label_0160;
        Label_0144:
            if (this.m_AnimeType != 1)
            {
                goto Label_0160;
            }
            base.mController.PlayIdle(0f);
        Label_0160:
            if (base.Async != null)
            {
                goto Label_0198;
            }
            base.ActivateNext();
            base.mController.SetRunningSpeed(base.BackupRunSpeed);
            base.mController.CollideGround = base.mActorCollideGround;
            goto Label_01C1;
        Label_0198:
            base.enabled = 0;
            base.mController.SetRunningSpeed(base.BackupRunSpeed);
            base.mController.CollideGround = base.mActorCollideGround;
        Label_01C1:
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
        private sealed class <PreloadAssets>c__Iterator8B : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal GameObject <obj>__0;
            internal string <path>__1;
            internal int $PC;
            internal object $current;
            internal EventAction_MoveActorWithAnime <>f__this;

            public <PreloadAssets>c__Iterator8B()
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
                this.<obj>__0 = EventAction.FindActor(this.<>f__this.ActorID);
                if ((this.<obj>__0 != null) == null)
                {
                    goto Label_0136;
                }
                this.<>f__this.mController = this.<obj>__0.GetComponent<TacticsUnitController>();
                if ((this.<>f__this.mController != null) == null)
                {
                    goto Label_0155;
                }
                if (this.<>f__this.m_AnimeType != null)
                {
                    goto Label_0155;
                }
                if (string.IsNullOrEmpty(this.<>f__this.m_AnimationName) != null)
                {
                    goto Label_0155;
                }
                this.<>f__this.m_AnimationID = "tmp_" + Convert.ToString(this.<>f__this.GetInstanceID(), 0x10);
                this.<path>__1 = "CHM/" + this.<>f__this.m_AnimationName;
                this.<>f__this.mController.LoadAnimationAsync(this.<>f__this.m_AnimationID, this.<path>__1);
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
                Debug.LogError("アクター'" + this.<>f__this.ActorID + "'は存在しません");
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

        public enum AnimeType
        {
            Custom,
            Idel
        }
    }
}

