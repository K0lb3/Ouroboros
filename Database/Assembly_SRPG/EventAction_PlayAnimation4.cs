namespace SRPG
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using UnityEngine;

    [EventActionInfo("New/アクター/アニメーション再生(複数)", "ユニットにアニメーションを再生させます。", 0x664444, 0xaa4444)]
    public class EventAction_PlayAnimation4 : EventAction
    {
        private const string MOVIE_PATH = "Movies/";
        private const string DEMO_PATH = "Demo/";
        [StringIsActorList]
        public string ActorID;
        public bool Async;
        [HideInInspector]
        public AnimationData[] AnimationDataArray;
        private bool foldout;
        private List<string> mAnimationIDList;
        private TacticsUnitController mController;
        private int idx;
        private float mDelay;
        private bool isPlay;

        public EventAction_PlayAnimation4()
        {
            AnimationData[] dataArray1;
            dataArray1 = new AnimationData[] { new AnimationData() };
            this.AnimationDataArray = dataArray1;
            this.foldout = 1;
            this.mAnimationIDList = new List<string>();
            base..ctor();
            return;
        }

        public override void OnActivate()
        {
            if ((this.mController != null) == null)
            {
                goto Label_0047;
            }
            this.idx = 0;
            this.mDelay = this.AnimationDataArray[this.idx].Delay;
            if (this.Async == null)
            {
                goto Label_004D;
            }
            base.ActivateNext(1);
            goto Label_004D;
        Label_0047:
            base.ActivateNext();
        Label_004D:
            return;
        }

        protected override void OnDestroy()
        {
            int num;
            if ((this.mController == null) == null)
            {
                goto Label_0012;
            }
            return;
        Label_0012:
            num = 0;
            goto Label_0034;
        Label_0019:
            this.mController.UnloadAnimation(this.mAnimationIDList[num]);
            num += 1;
        Label_0034:
            if (num < this.mAnimationIDList.Count)
            {
                goto Label_0019;
            }
            return;
        }

        [DebuggerHidden]
        public override IEnumerator PreloadAssets()
        {
            <PreloadAssets>c__Iterator90 iterator;
            iterator = new <PreloadAssets>c__Iterator90();
            iterator.<>f__this = this;
            return iterator;
        }

        public override void Update()
        {
            Transform transform;
            if (this.mDelay <= 0f)
            {
                goto Label_0027;
            }
            this.mDelay -= Time.get_deltaTime();
            goto Label_025A;
        Label_0027:
            if (this.isPlay == null)
            {
                goto Label_019E;
            }
            if (this.mController.GetRemainingTime(this.mAnimationIDList[this.idx]) > 0f)
            {
                goto Label_025A;
            }
            if (this.AnimationDataArray[this.idx].ApplyRootBoneAtEnd == null)
            {
                goto Label_0137;
            }
            if (this.AnimationDataArray[this.idx].Loop != null)
            {
                goto Label_0137;
            }
            this.mController.StopAnimation(this.mAnimationIDList[this.idx]);
            transform = GameUtility.findChildRecursively(this.mController.get_transform(), this.mController.RootMotionBoneName);
            this.mController.get_transform().set_position(transform.get_position());
            transform.set_localPosition(new Vector3(0f, 0f, 0f));
            this.mController.get_transform().set_rotation(transform.get_rotation() * Quaternion.Euler(90f, 0f, 0f));
            transform.set_localRotation(Quaternion.Euler(270f, 0f, 0f));
        Label_0137:
            this.isPlay = 0;
            this.idx += 1;
            if (this.idx >= ((int) this.AnimationDataArray.Length))
            {
                goto Label_017C;
            }
            this.mDelay = this.AnimationDataArray[this.idx].Delay;
            goto Label_0199;
        Label_017C:
            if (this.Async == null)
            {
                goto Label_0193;
            }
            base.enabled = 0;
            goto Label_0199;
        Label_0193:
            base.ActivateNext();
        Label_0199:
            goto Label_025A;
        Label_019E:
            this.isPlay = 1;
            if (this.AnimationDataArray[this.idx].Type != null)
            {
                goto Label_022D;
            }
            if (string.IsNullOrEmpty(this.mAnimationIDList[this.idx]) != null)
            {
                goto Label_025A;
            }
            this.mController.RootMotionMode = 1;
            this.mController.PlayAnimation(this.mAnimationIDList[this.idx], this.AnimationDataArray[this.idx].Loop, this.AnimationDataArray[this.idx].Interp, 0f);
            goto Label_025A;
        Label_022D:
            this.mController.PlayIdle(0f);
            if (this.Async == null)
            {
                goto Label_0254;
            }
            base.enabled = 0;
            goto Label_025A;
        Label_0254:
            base.ActivateNext();
        Label_025A:
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
        private sealed class <PreloadAssets>c__Iterator90 : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal GameObject <go>__0;
            internal string <instanceID>__1;
            internal int <i>__2;
            internal EventAction_PlayAnimation4.AnimationTypes <AnimationType>__3;
            internal string <AnimationName>__4;
            internal EventAction_PlayAnimation4.AnimationData.PREFIX_PATH <Path>__5;
            internal string <mAnimationID>__6;
            internal string <path>__7;
            internal int $PC;
            internal object $current;
            internal EventAction_PlayAnimation4 <>f__this;

            public <PreloadAssets>c__Iterator90()
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

            public unsafe bool MoveNext()
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
                        goto Label_01F1;

                    case 2:
                        goto Label_027D;
                }
                goto Label_0284;
            Label_0025:
                this.<go>__0 = EventAction.FindActor(this.<>f__this.ActorID);
                if ((this.<go>__0 != null) == null)
                {
                    goto Label_024B;
                }
                this.<>f__this.mController = this.<go>__0.GetComponent<TacticsUnitController>();
                if ((this.<>f__this.mController != null) == null)
                {
                    goto Label_026A;
                }
                this.<instanceID>__1 = Convert.ToString(this.<>f__this.GetInstanceID(), 0x10);
                this.<i>__2 = 0;
                goto Label_022E;
            Label_009C:
                this.<AnimationType>__3 = this.<>f__this.AnimationDataArray[this.<i>__2].Type;
                this.<AnimationName>__4 = this.<>f__this.AnimationDataArray[this.<i>__2].Name;
                this.<Path>__5 = this.<>f__this.AnimationDataArray[this.<i>__2].Path;
                if (this.<AnimationType>__3 != null)
                {
                    goto Label_020B;
                }
                if (string.IsNullOrEmpty(this.<AnimationName>__4) != null)
                {
                    goto Label_020B;
                }
                this.<mAnimationID>__6 = "tmp" + &this.<i>__2.ToString() + "_" + this.<instanceID>__1;
                this.<>f__this.mAnimationIDList.Add(this.<mAnimationID>__6);
                this.<path>__7 = string.Empty;
                if (this.<Path>__5 != 1)
                {
                    goto Label_017C;
                }
                this.<path>__7 = this.<path>__7 + "Movies/";
                goto Label_019D;
            Label_017C:
                if (this.<Path>__5 != null)
                {
                    goto Label_019D;
                }
                this.<path>__7 = this.<path>__7 + "Demo/";
            Label_019D:
                this.<path>__7 = this.<path>__7 + "CHM/" + this.<AnimationName>__4;
                this.<>f__this.mController.LoadAnimationAsync(this.<mAnimationID>__6, this.<path>__7);
                goto Label_01F1;
            Label_01DA:
                this.$current = new WaitForEndOfFrame();
                this.$PC = 1;
                goto Label_0286;
            Label_01F1:
                if (this.<>f__this.mController.IsLoading != null)
                {
                    goto Label_01DA;
                }
                goto Label_0220;
            Label_020B:
                this.<>f__this.mAnimationIDList.Add(string.Empty);
            Label_0220:
                this.<i>__2 += 1;
            Label_022E:
                if (this.<i>__2 < ((int) this.<>f__this.AnimationDataArray.Length))
                {
                    goto Label_009C;
                }
                goto Label_026A;
            Label_024B:
                Debug.LogError("アクター'" + this.<>f__this.ActorID + "'は存在しません。");
            Label_026A:
                this.$current = null;
                this.$PC = 2;
                goto Label_0286;
            Label_027D:
                this.$PC = -1;
            Label_0284:
                return 0;
            Label_0286:
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

        [Serializable]
        public class AnimationData
        {
            public EventAction_PlayAnimation4.AnimationTypes Type;
            public string Name;
            public float Delay;
            public float Interp;
            public bool ApplyRootBoneAtEnd;
            public bool Loop;
            public PREFIX_PATH Path;

            public AnimationData()
            {
                this.Interp = 0.1f;
                base..ctor();
                return;
            }

            public enum PREFIX_PATH
            {
                Demo,
                Movie,
                Default
            }
        }

        public enum AnimationTypes
        {
            Custom,
            Idle
        }
    }
}

