namespace SRPG
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using UnityEngine;

    public class ArenaActionCount : MonoBehaviour
    {
        private const uint VALUE_OF_DISPLAY_IN_YELLOW_FONT = 20;
        private const uint VALUE_OF_DISPLAY_IN_RED_FONT = 5;
        public GameObject GoWhiteFont;
        public GameObject GoYellowFont;
        public GameObject GoRedFont;
        private AnmCtrl mAnmCtrl;
        private uint mActionCount;
        private uint mOldActionCount;
        private bool mIsInitialized;

        public ArenaActionCount()
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
            obj2 = this.GoWhiteFont;
            if (((long) count) > 5L)
            {
                goto Label_0056;
            }
            obj2 = this.GoRedFont;
            goto Label_0067;
        Label_0056:
            if (((long) count) > 20L)
            {
                goto Label_0067;
            }
            obj2 = this.GoYellowFont;
        Label_0067:
            obj2.SetActive(1);
            text = obj2.GetComponentInChildren<BitmapText>(1);
            if (text != null)
            {
                goto Label_0082;
            }
            return;
        Label_0082:
            text.text = &count.ToString();
            return;
        }

        [DebuggerHidden]
        private IEnumerator playEffect()
        {
            <playEffect>c__Iterator29 iterator;
            iterator = new <playEffect>c__Iterator29();
            iterator.<>f__this = this;
            return iterator;
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
        private sealed class <playEffect>c__Iterator29 : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal GameObject <go_active>__0;
            internal GameObject <go_digit>__1;
            internal GameObject <go>__2;
            internal RectTransform <od_rtr>__3;
            internal RectTransform <rtr>__4;
            internal int $PC;
            internal object $current;
            internal ArenaActionCount <>f__this;

            public <playEffect>c__Iterator29()
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
                ArenaActionCount.eAnmState state;
                bool flag;
                num = this.$PC;
                this.$PC = -1;
                switch (num)
                {
                    case 0:
                        goto Label_0021;

                    case 1:
                        goto Label_015C;
                }
                goto Label_05BB;
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
                this.<go_active>__0 = this.<>f__this.GoWhiteFont;
                if (this.<>f__this.mActionCount > 5)
                {
                    goto Label_00C9;
                }
                this.<go_active>__0 = this.<>f__this.GoRedFont;
                goto Label_00EC;
            Label_00C9:
                if (this.<>f__this.mActionCount > 20)
                {
                    goto Label_00EC;
                }
                this.<go_active>__0 = this.<>f__this.GoYellowFont;
            Label_00EC:
                if (this.<go_active>__0 == null)
                {
                    goto Label_059F;
                }
                this.<>f__this.mAnmCtrl.mGoSelf = this.<go_active>__0.get_gameObject();
                this.<>f__this.mAnmCtrl.mAnmSelf = this.<go_active>__0.GetComponentInChildren<Animator>(1);
                this.<>f__this.mAnmCtrl.mAnmState = 1;
                goto Label_059F;
            Label_0149:
                this.$current = null;
                this.$PC = 1;
                goto Label_05BD;
            Label_015C:
                switch ((this.<>f__this.mAnmCtrl.mAnmState - 1))
                {
                    case 0:
                        goto Label_0192;

                    case 1:
                        goto Label_0332;

                    case 2:
                        goto Label_038F;

                    case 3:
                        goto Label_03F0;

                    case 4:
                        goto Label_04D1;

                    case 5:
                        goto Label_0501;
                }
                goto Label_059F;
            Label_0192:
                this.<go_digit>__1 = this.<>f__this.mAnmCtrl.mGoSelf;
                this.<go>__2 = Object.Instantiate<GameObject>(this.<go_digit>__1);
                if (this.<go>__2 != null)
                {
                    goto Label_01CE;
                }
                goto Label_059F;
            Label_01CE:
                this.<od_rtr>__3 = this.<go_digit>__1.GetComponent<RectTransform>();
                this.<rtr>__4 = this.<go>__2.GetComponent<RectTransform>();
                if (this.<od_rtr>__3 == null)
                {
                    goto Label_02D9;
                }
                if (this.<rtr>__4 == null)
                {
                    goto Label_02D9;
                }
                this.<go>__2.get_transform().SetParent(this.<>f__this.get_transform());
                this.<go>__2.get_transform().set_localRotation(this.<go_digit>__1.get_transform().get_localRotation());
                this.<go>__2.get_transform().set_localScale(this.<go_digit>__1.get_transform().get_localScale());
                this.<rtr>__4.set_anchorMin(this.<od_rtr>__3.get_anchorMin());
                this.<rtr>__4.set_anchorMax(this.<od_rtr>__3.get_anchorMax());
                this.<rtr>__4.set_pivot(this.<od_rtr>__3.get_pivot());
                this.<rtr>__4.set_sizeDelta(this.<od_rtr>__3.get_sizeDelta());
                this.<rtr>__4.set_position(this.<od_rtr>__3.get_position());
            Label_02D9:
                this.<>f__this.mAnmCtrl.mGoEffect = this.<go>__2;
                this.<>f__this.mAnmCtrl.mAnmEffect = this.<go>__2.GetComponentInChildren<Animator>(1);
                this.<>f__this.mAnmCtrl.mWaitFrameCtr = 0;
                this.<>f__this.mAnmCtrl.mAnmState = 2;
            Label_0332:
                if (this.<>f__this.mAnmCtrl.mWaitFrameCtr == null)
                {
                    goto Label_035F;
                }
                this.<>f__this.mAnmCtrl.mWaitFrameCtr -= 1;
            Label_035F:
                if (this.<>f__this.mAnmCtrl.mWaitFrameCtr != null)
                {
                    goto Label_059F;
                }
                this.<>f__this.mAnmCtrl.mAnmState = 3;
                goto Label_038F;
                goto Label_059F;
            Label_038F:
                if (this.<>f__this.mActionCount > 20)
                {
                    goto Label_03C0;
                }
                GameUtility.SetAnimatorTrigger(this.<>f__this.mAnmCtrl.mAnmEffect, "drop_red");
                goto Label_03DA;
            Label_03C0:
                GameUtility.SetAnimatorTrigger(this.<>f__this.mAnmCtrl.mAnmEffect, "drop");
            Label_03DA:
                this.<>f__this.mAnmCtrl.mAnmState = 4;
                goto Label_059F;
            Label_03F0:
                if (GameUtility.IsAnimatorRunning(this.<>f__this.mAnmCtrl.mAnmEffect) != null)
                {
                    goto Label_059F;
                }
                this.<>f__this.mAnmCtrl.mGoEffect.SetActive(0);
                Object.Destroy(this.<>f__this.mAnmCtrl.mGoEffect.get_gameObject());
                this.<>f__this.mAnmCtrl.mAnmEffect = null;
                this.<>f__this.mAnmCtrl.mGoEffect = null;
                this.<>f__this.mAnmCtrl.mPlayBeatCtr = 0;
                if (this.<>f__this.mActionCount > 5)
                {
                    goto Label_048F;
                }
                this.<>f__this.mAnmCtrl.mPlayBeatCtr = 2;
            Label_048F:
                if (this.<>f__this.mAnmCtrl.mPlayBeatCtr <= 0)
                {
                    goto Label_04BB;
                }
                this.<>f__this.mAnmCtrl.mAnmState = 5;
                goto Label_04D1;
            Label_04BB:
                this.<>f__this.mAnmCtrl.mAnmState = 0;
                goto Label_059F;
            Label_04D1:
                GameUtility.SetAnimatorTrigger(this.<>f__this.mAnmCtrl.mAnmSelf, "beat");
                this.<>f__this.mAnmCtrl.mAnmState = 6;
                goto Label_059F;
            Label_0501:
                if (GameUtility.IsAnimatorRunning(this.<>f__this.mAnmCtrl.mAnmSelf) != null)
                {
                    goto Label_059F;
                }
                if (this.<>f__this.mAnmCtrl.mPlayBeatCtr == null)
                {
                    goto Label_0548;
                }
                this.<>f__this.mAnmCtrl.mPlayBeatCtr -= 1;
            Label_0548:
                if (this.<>f__this.mAnmCtrl.mPlayBeatCtr != null)
                {
                    goto Label_0584;
                }
                this.<>f__this.mAnmCtrl.mAnmSelf = null;
                this.<>f__this.mAnmCtrl.mAnmState = 0;
                goto Label_059A;
            Label_0584:
                this.<>f__this.mAnmCtrl.mAnmState = 5;
                goto Label_04D1;
            Label_059A:;
            Label_059F:
                if (this.<>f__this.mAnmCtrl.mAnmState != null)
                {
                    goto Label_0149;
                }
                this.$PC = -1;
            Label_05BB:
                return 0;
            Label_05BD:
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
            public ArenaActionCount.eAnmState mAnmState;
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

