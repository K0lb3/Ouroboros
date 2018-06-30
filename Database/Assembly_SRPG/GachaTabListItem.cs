namespace SRPG
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using UnityEngine;
    using UnityEngine.UI;

    public class GachaTabListItem : MonoBehaviour
    {
        public Text Value;
        public Text Fotter;
        private long mEndAt;
        private bool mDisabled;
        private Coroutine mUpdateCoroutine;
        private float mNextUpdateTime;
        private string mFormatkey;
        private long mGachaStartAt;
        private long mGachaEndAt;
        private int mListIndex;

        public GachaTabListItem()
        {
            this.mFormatkey = "sys.QUEST_TIMELIMIT_";
            this.mListIndex = -1;
            base..ctor();
            return;
        }

        private void OnEnable()
        {
            if (this.mUpdateCoroutine == null)
            {
                goto Label_001E;
            }
            base.StopCoroutine(this.mUpdateCoroutine);
            this.mUpdateCoroutine = null;
        Label_001E:
            this.RefreshTimer();
            return;
        }

        private unsafe void RefreshTimer()
        {
            object[] objArray3;
            object[] objArray2;
            object[] objArray1;
            DateTime time;
            DateTime time2;
            TimeSpan span;
            SRPG_Button button;
            string str;
            time = TimeManager.ServerTime;
            time2 = TimeManager.FromUnixTime(this.mEndAt);
            span = time2 - time;
            if (this.Disabled == null)
            {
                goto Label_009A;
            }
            if (&span.TotalSeconds >= 0.0)
            {
                goto Label_009A;
            }
            if (this.mGachaEndAt < Network.GetServerTime())
            {
                goto Label_009A;
            }
            this.mEndAt = TimeManager.FromDateTime(&time2.AddDays(1.0));
            span = TimeManager.FromUnixTime(this.mEndAt) - time;
            button = base.GetComponent<SRPG_Button>();
            if ((button != null) == null)
            {
                goto Label_009A;
            }
            button.set_interactable(1);
            this.Disabled = 0;
        Label_009A:
            str = string.Empty;
            if (&span.TotalDays < 1.0)
            {
                goto Label_00E7;
            }
            objArray1 = new object[] { (int) &span.Days };
            str = LocalizedText.Get(this.FormatKey + "D", objArray1);
            goto Label_015F;
        Label_00E7:
            if (&span.TotalHours < 1.0)
            {
                goto Label_012D;
            }
            objArray2 = new object[] { (int) &span.Hours };
            str = LocalizedText.Get(this.FormatKey + "H", objArray2);
            goto Label_015F;
        Label_012D:
            objArray3 = new object[] { (int) Mathf.Max(&span.Minutes, 0) };
            str = LocalizedText.Get(this.FormatKey + "M", objArray3);
        Label_015F:
            if ((this.Value != null) == null)
            {
                goto Label_0194;
            }
            if ((this.Value.get_text() != str) == null)
            {
                goto Label_0194;
            }
            this.Value.set_text(str);
        Label_0194:
            this.SetUpdateTimer(1f);
            return;
        }

        private void SetUpdateTimer(float interval)
        {
            if (base.get_gameObject().get_activeInHierarchy() != null)
            {
                goto Label_0011;
            }
            return;
        Label_0011:
            if (interval > 0f)
            {
                goto Label_0034;
            }
            if (this.mUpdateCoroutine == null)
            {
                goto Label_0033;
            }
            base.StopCoroutine(this.mUpdateCoroutine);
        Label_0033:
            return;
        Label_0034:
            this.mNextUpdateTime = Time.get_time() + interval;
            if (this.mUpdateCoroutine == null)
            {
                goto Label_004D;
            }
            return;
        Label_004D:
            this.mUpdateCoroutine = base.StartCoroutine(this.UpdateTimer());
            return;
        }

        private void Start()
        {
        }

        [DebuggerHidden]
        private IEnumerator UpdateTimer()
        {
            <UpdateTimer>c__Iterator112 iterator;
            iterator = new <UpdateTimer>c__Iterator112();
            iterator.<>f__this = this;
            return iterator;
        }

        public long EndAt
        {
            get
            {
                return this.mEndAt;
            }
            set
            {
                this.mEndAt = value;
                return;
            }
        }

        public bool Disabled
        {
            get
            {
                return this.mDisabled;
            }
            set
            {
                this.mDisabled = value;
                return;
            }
        }

        public string FormatKey
        {
            get
            {
                return this.mFormatkey;
            }
            set
            {
                this.mFormatkey = value;
                return;
            }
        }

        public long GachaStartAt
        {
            get
            {
                return this.mGachaStartAt;
            }
            set
            {
                this.mGachaStartAt = value;
                return;
            }
        }

        public long GachaEndtAt
        {
            get
            {
                return this.mGachaEndAt;
            }
            set
            {
                this.mGachaEndAt = value;
                return;
            }
        }

        public int ListIndex
        {
            get
            {
                return this.mListIndex;
            }
            set
            {
                this.mListIndex = value;
                return;
            }
        }

        [CompilerGenerated]
        private sealed class <UpdateTimer>c__Iterator112 : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal int $PC;
            internal object $current;
            internal GachaTabListItem <>f__this;

            public <UpdateTimer>c__Iterator112()
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
                        goto Label_003D;

                    case 2:
                        goto Label_0091;
                }
                goto Label_0098;
            Label_0025:
                goto Label_003D;
            Label_002A:
                this.$current = null;
                this.$PC = 1;
                goto Label_009A;
            Label_003D:
                if (Time.get_time() < this.<>f__this.mNextUpdateTime)
                {
                    goto Label_002A;
                }
                this.<>f__this.RefreshTimer();
                if (Time.get_time() < this.<>f__this.mNextUpdateTime)
                {
                    goto Label_003D;
                }
                this.<>f__this.mUpdateCoroutine = null;
                this.$current = null;
                this.$PC = 2;
                goto Label_009A;
            Label_0091:
                this.$PC = -1;
            Label_0098:
                return 0;
            Label_009A:
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

