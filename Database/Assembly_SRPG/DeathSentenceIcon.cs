namespace SRPG
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using UnityEngine;
    using UnityEngine.UI;

    public class DeathSentenceIcon : MonoBehaviour
    {
        public GameObject DeathIconPrefab;
        private Text mCountDownText;
        private GameObject mDeathIcon;
        private bool mIsDeathSentenceCountDownPlaying;

        public DeathSentenceIcon()
        {
            base..ctor();
            return;
        }

        public void Close()
        {
            if ((this.mDeathIcon != null) == null)
            {
                goto Label_002D;
            }
            if (this.mDeathIcon.get_activeSelf() == null)
            {
                goto Label_002D;
            }
            this.mDeathIcon.SetActive(0);
        Label_002D:
            return;
        }

        public void Countdown(int currentCount, float LifeSeconds)
        {
            if (base.get_gameObject().get_activeInHierarchy() == null)
            {
                goto Label_002F;
            }
            if (LifeSeconds <= 0f)
            {
                goto Label_002F;
            }
            base.StartCoroutine(this.CountdownInternal(currentCount, LifeSeconds));
            goto Label_003D;
        Label_002F:
            this.mIsDeathSentenceCountDownPlaying = 0;
            this.UpdateCountDown(currentCount);
        Label_003D:
            return;
        }

        [DebuggerHidden]
        private IEnumerator CountdownInternal(int currentCount, float LifeSeconds)
        {
            <CountdownInternal>c__Iterator104 iterator;
            iterator = new <CountdownInternal>c__Iterator104();
            iterator.currentCount = currentCount;
            iterator.LifeSeconds = LifeSeconds;
            iterator.<$>currentCount = currentCount;
            iterator.<$>LifeSeconds = LifeSeconds;
            iterator.<>f__this = this;
            return iterator;
        }

        public void Init(Unit parent)
        {
            if (this.DeathIconPrefab == null)
            {
                goto Label_008C;
            }
            this.mDeathIcon = Object.Instantiate(this.DeathIconPrefab, base.get_transform().get_position(), base.get_transform().get_rotation()) as GameObject;
            if ((this.mDeathIcon != null) == null)
            {
                goto Label_008C;
            }
            this.mCountDownText = this.mDeathIcon.GetComponentInChildren<Text>();
            this.mDeathIcon.get_transform().SetParent(base.get_transform());
            this.mDeathIcon.SetActive(0);
            DataSource.Bind<Unit>(base.get_gameObject(), parent);
        Label_008C:
            return;
        }

        public void Open()
        {
            if ((this.mDeathIcon != null) == null)
            {
                goto Label_002D;
            }
            if (this.mDeathIcon.get_activeSelf() != null)
            {
                goto Label_002D;
            }
            this.mDeathIcon.SetActive(1);
        Label_002D:
            return;
        }

        public unsafe void UpdateCountDown(int currentCount)
        {
            if ((this.mDeathIcon == null) == null)
            {
                goto Label_0012;
            }
            return;
        Label_0012:
            if ((this.mCountDownText != null) == null)
            {
                goto Label_0035;
            }
            this.mCountDownText.set_text(&currentCount.ToString());
        Label_0035:
            return;
        }

        public bool IsDeathSentenceCountDownPlaying
        {
            get
            {
                return this.mIsDeathSentenceCountDownPlaying;
            }
            set
            {
            }
        }

        [CompilerGenerated]
        private sealed class <CountdownInternal>c__Iterator104 : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal int currentCount;
            internal float LifeSeconds;
            internal int $PC;
            internal object $current;
            internal int <$>currentCount;
            internal float <$>LifeSeconds;
            internal DeathSentenceIcon <>f__this;

            public <CountdownInternal>c__Iterator104()
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
                        goto Label_0067;

                    case 2:
                        goto Label_009B;
                }
                goto Label_00AE;
            Label_0025:
                this.<>f__this.mIsDeathSentenceCountDownPlaying = 1;
                this.<>f__this.UpdateCountDown(this.currentCount + 1);
                this.$current = new WaitForSeconds(this.LifeSeconds / 2f);
                this.$PC = 1;
                goto Label_00B0;
            Label_0067:
                this.<>f__this.UpdateCountDown(this.currentCount);
                this.$current = new WaitForSeconds(this.LifeSeconds / 2f);
                this.$PC = 2;
                goto Label_00B0;
            Label_009B:
                this.<>f__this.mIsDeathSentenceCountDownPlaying = 0;
                this.$PC = -1;
            Label_00AE:
                return 0;
            Label_00B0:
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

