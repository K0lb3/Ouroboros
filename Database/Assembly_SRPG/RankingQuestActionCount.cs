namespace SRPG
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using UnityEngine;

    public class RankingQuestActionCount : MonoBehaviour
    {
        public GameObject GoWhiteFont;
        public GameObject GoYellowFont;
        public GameObject GoRedFont;
        private AnmCtrl mAnmCtrl;
        private uint mActionCount;
        private uint mOldActionCount;
        private bool mIsInitialized;

        public RankingQuestActionCount()
        {
            this.mAnmCtrl = new AnmCtrl();
            this.mOldActionCount = -1;
            base..ctor();
            return;
        }

        private unsafe void dispActionCount(int count)
        {
            GameObject obj2;
            BitmapText text;
            if (this.mIsInitialized != null)
            {
                goto Label_000C;
            }
            return;
        Label_000C:
            if (count >= 0)
            {
                goto Label_0016;
            }
            count = 0;
        Label_0016:
            this.GoWhiteFont.SetActive(0);
            this.GoYellowFont.SetActive(0);
            this.GoRedFont.SetActive(0);
            obj2 = this.GoYellowFont;
            obj2.SetActive(1);
            text = obj2.GetComponentInChildren<BitmapText>(1);
            if (text != null)
            {
                goto Label_005C;
            }
            return;
        Label_005C:
            text.text = &count.ToString();
            return;
        }

        [DebuggerHidden]
        private IEnumerator playEffect()
        {
            <playEffect>c__Iterator4F iteratorf;
            iteratorf = new <playEffect>c__Iterator4F();
            iteratorf.<>f__this = this;
            return iteratorf;
        }

        public void PlayEffect()
        {
            base.StartCoroutine(this.playEffect());
            return;
        }

        private void Start()
        {
            this.mIsInitialized = 0;
            if (this.GoWhiteFont == null)
            {
                goto Label_003E;
            }
            if (this.GoYellowFont == null)
            {
                goto Label_003E;
            }
            if (this.GoRedFont == null)
            {
                goto Label_003E;
            }
            this.mIsInitialized = 1;
        Label_003E:
            this.ActionCount = 0;
            return;
        }

        public uint ActionCount
        {
            get
            {
                return this.mActionCount;
            }
            set
            {
                this.mActionCount = value;
                if (this.mOldActionCount == this.mActionCount)
                {
                    goto Label_0030;
                }
                this.dispActionCount(this.mActionCount);
                this.mOldActionCount = this.mActionCount;
            Label_0030:
                return;
            }
        }

        [CompilerGenerated]
        private sealed class <playEffect>c__Iterator4F : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal GameObject <go_active>__0;
            internal GameObject <go_digit>__1;
            internal GameObject <go>__2;
            internal RectTransform <od_rtr>__3;
            internal RectTransform <rtr>__4;
            internal int $PC;
            internal object $current;
            internal RankingQuestActionCount <>f__this;

            public <playEffect>c__Iterator4F()
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
                RankingQuestActionCount.eAnmState state;
                bool flag;
                num = this.$PC;
                this.$PC = -1;
                switch (num)
                {
                    case 0:
                        goto Label_0021;

                    case 1:
                        goto Label_0112;
                }
                goto Label_051E;
            Label_0021:
                if (this.<>f__this.mAnmCtrl.mAnmState == null)
                {
                    goto Label_0091;
                }
                if (this.<>f__this.mAnmCtrl.mGoEffect == null)
                {
                    goto Label_0080;
                }
                this.<>f__this.mAnmCtrl.mGoEffect.SetActive(0);
                Object.Destroy(this.<>f__this.mAnmCtrl.mGoEffect.get_gameObject());
            Label_0080:
                this.<>f__this.mAnmCtrl.mAnmState = 0;
            Label_0091:
                this.<go_active>__0 = this.<>f__this.GoYellowFont;
                if (this.<go_active>__0 == null)
                {
                    goto Label_0502;
                }
                this.<>f__this.mAnmCtrl.mGoSelf = this.<go_active>__0.get_gameObject();
                this.<>f__this.mAnmCtrl.mAnmSelf = this.<go_active>__0.GetComponentInChildren<Animator>(1);
                this.<>f__this.mAnmCtrl.mAnmState = 1;
                goto Label_0502;
            Label_00FF:
                this.$current = null;
                this.$PC = 1;
                goto Label_0520;
            Label_0112:
                switch ((this.<>f__this.mAnmCtrl.mAnmState - 1))
                {
                    case 0:
                        goto Label_0148;

                    case 1:
                        goto Label_02E8;

                    case 2:
                        goto Label_0345;

                    case 3:
                        goto Label_0375;

                    case 4:
                        goto Label_0434;

                    case 5:
                        goto Label_0464;
                }
                goto Label_0502;
            Label_0148:
                this.<go_digit>__1 = this.<>f__this.mAnmCtrl.mGoSelf;
                this.<go>__2 = Object.Instantiate<GameObject>(this.<go_digit>__1);
                if (this.<go>__2 != null)
                {
                    goto Label_0184;
                }
                goto Label_0502;
            Label_0184:
                this.<od_rtr>__3 = this.<go_digit>__1.GetComponent<RectTransform>();
                this.<rtr>__4 = this.<go>__2.GetComponent<RectTransform>();
                if (this.<od_rtr>__3 == null)
                {
                    goto Label_028F;
                }
                if (this.<rtr>__4 == null)
                {
                    goto Label_028F;
                }
                this.<go>__2.get_transform().SetParent(this.<>f__this.get_transform());
                this.<go>__2.get_transform().set_localRotation(this.<go_digit>__1.get_transform().get_localRotation());
                this.<go>__2.get_transform().set_localScale(this.<go_digit>__1.get_transform().get_localScale());
                this.<rtr>__4.set_anchorMin(this.<od_rtr>__3.get_anchorMin());
                this.<rtr>__4.set_anchorMax(this.<od_rtr>__3.get_anchorMax());
                this.<rtr>__4.set_pivot(this.<od_rtr>__3.get_pivot());
                this.<rtr>__4.set_sizeDelta(this.<od_rtr>__3.get_sizeDelta());
                this.<rtr>__4.set_position(this.<od_rtr>__3.get_position());
            Label_028F:
                this.<>f__this.mAnmCtrl.mGoEffect = this.<go>__2;
                this.<>f__this.mAnmCtrl.mAnmEffect = this.<go>__2.GetComponentInChildren<Animator>(1);
                this.<>f__this.mAnmCtrl.mWaitFrameCtr = 0;
                this.<>f__this.mAnmCtrl.mAnmState = 2;
            Label_02E8:
                if (this.<>f__this.mAnmCtrl.mWaitFrameCtr == null)
                {
                    goto Label_0315;
                }
                this.<>f__this.mAnmCtrl.mWaitFrameCtr -= 1;
            Label_0315:
                if (this.<>f__this.mAnmCtrl.mWaitFrameCtr != null)
                {
                    goto Label_0502;
                }
                this.<>f__this.mAnmCtrl.mAnmState = 3;
                goto Label_0345;
                goto Label_0502;
            Label_0345:
                GameUtility.SetAnimatorTrigger(this.<>f__this.mAnmCtrl.mAnmEffect, "drop_red");
                this.<>f__this.mAnmCtrl.mAnmState = 4;
                goto Label_0502;
            Label_0375:
                if (GameUtility.IsAnimatorRunning(this.<>f__this.mAnmCtrl.mAnmEffect) != null)
                {
                    goto Label_0502;
                }
                this.<>f__this.mAnmCtrl.mGoEffect.SetActive(0);
                Object.Destroy(this.<>f__this.mAnmCtrl.mGoEffect.get_gameObject());
                this.<>f__this.mAnmCtrl.mAnmEffect = null;
                this.<>f__this.mAnmCtrl.mGoEffect = null;
                this.<>f__this.mAnmCtrl.mPlayBeatCtr = 0;
                if (this.<>f__this.mAnmCtrl.mPlayBeatCtr <= 0)
                {
                    goto Label_041E;
                }
                this.<>f__this.mAnmCtrl.mAnmState = 5;
                goto Label_0434;
            Label_041E:
                this.<>f__this.mAnmCtrl.mAnmState = 0;
                goto Label_0502;
            Label_0434:
                GameUtility.SetAnimatorTrigger(this.<>f__this.mAnmCtrl.mAnmSelf, "beat");
                this.<>f__this.mAnmCtrl.mAnmState = 6;
                goto Label_0502;
            Label_0464:
                if (GameUtility.IsAnimatorRunning(this.<>f__this.mAnmCtrl.mAnmSelf) != null)
                {
                    goto Label_0502;
                }
                if (this.<>f__this.mAnmCtrl.mPlayBeatCtr == null)
                {
                    goto Label_04AB;
                }
                this.<>f__this.mAnmCtrl.mPlayBeatCtr -= 1;
            Label_04AB:
                if (this.<>f__this.mAnmCtrl.mPlayBeatCtr != null)
                {
                    goto Label_04E7;
                }
                this.<>f__this.mAnmCtrl.mAnmSelf = null;
                this.<>f__this.mAnmCtrl.mAnmState = 0;
                goto Label_04FD;
            Label_04E7:
                this.<>f__this.mAnmCtrl.mAnmState = 5;
                goto Label_0434;
            Label_04FD:;
            Label_0502:
                if (this.<>f__this.mAnmCtrl.mAnmState != null)
                {
                    goto Label_00FF;
                }
                this.$PC = -1;
            Label_051E:
                return 0;
            Label_0520:
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

        private class AnmCtrl
        {
            public RankingQuestActionCount.eAnmState mAnmState;
            public uint mWaitFrameCtr;
            public uint mPlayBeatCtr;
            public GameObject mGoSelf;
            public Animator mAnmSelf;
            public GameObject mGoEffect;
            public Animator mAnmEffect;

            public AnmCtrl()
            {
                this.mPlayBeatCtr = 1;
                base..ctor();
                return;
            }
        }

        private enum eAnmState
        {
            IDLE,
            INIT,
            WAIT_FRAME,
            PLAY_DROP,
            WAIT_DROP,
            PLAY_BEAT,
            WAIT_BEAT
        }
    }
}

