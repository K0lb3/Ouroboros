namespace SRPG
{
    using GR;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using UnityEngine;
    using UnityEngine.UI;

    public class NotifyList : MonoBehaviour
    {
        private static NotifyList mInstance;
        public RectTransform ListParent;
        public NotifyListItem Item_Generic;
        public NotifyListItem Item_LoginBonus;
        public NotifyListItem Item_Mission;
        public NotifyListItem Item_DailyMission;
        public NotifyListItem Item_ContentsUnlock;
        public NotifyListItem Item_QuestSupport;
        public NotifyListItem Item_Award;
        public NotifyListItem Item_MultiInvitation;
        public string FadeTrigger;
        public float FadeTime;
        private List<NotifyListItem> mItems;
        private List<NotifyListItem> mQueue;
        public float Lifetime;
        public float Spacing;
        public float MaxHeight;
        public float Interval;
        public float FadeInterval;
        public float GroupSpan;
        private float mStackHeight;
        private float mGroupTime;
        public string[] DebugItems;
        [CompilerGenerated]
        private static bool <mNotifyEnable>k__BackingField;

        public NotifyList()
        {
            this.FadeTrigger = "KILL";
            this.FadeTime = 1f;
            this.mItems = new List<NotifyListItem>();
            this.mQueue = new List<NotifyListItem>();
            this.Lifetime = 2f;
            this.Spacing = 10f;
            this.MaxHeight = 400f;
            this.Interval = 0.1f;
            this.FadeInterval = 0.1f;
            this.GroupSpan = 0.8f;
            base..ctor();
            return;
        }

        private void Awake()
        {
            if ((mInstance != null) == null)
            {
                goto Label_001C;
            }
            Object.Destroy(base.get_gameObject());
            return;
        Label_001C:
            mInstance = this;
            if ((this.Item_Generic != null) == null)
            {
                goto Label_0059;
            }
            if (this.Item_Generic.get_gameObject().get_activeInHierarchy() == null)
            {
                goto Label_0059;
            }
            this.Item_Generic.get_gameObject().SetActive(0);
        Label_0059:
            if ((this.Item_LoginBonus != null) == null)
            {
                goto Label_0090;
            }
            if (this.Item_LoginBonus.get_gameObject().get_activeInHierarchy() == null)
            {
                goto Label_0090;
            }
            this.Item_LoginBonus.get_gameObject().SetActive(0);
        Label_0090:
            if ((this.Item_Mission != null) == null)
            {
                goto Label_00C7;
            }
            if (this.Item_Mission.get_gameObject().get_activeInHierarchy() == null)
            {
                goto Label_00C7;
            }
            this.Item_Mission.get_gameObject().SetActive(0);
        Label_00C7:
            if ((this.Item_DailyMission != null) == null)
            {
                goto Label_00FE;
            }
            if (this.Item_DailyMission.get_gameObject().get_activeInHierarchy() == null)
            {
                goto Label_00FE;
            }
            this.Item_DailyMission.get_gameObject().SetActive(0);
        Label_00FE:
            if ((this.Item_ContentsUnlock != null) == null)
            {
                goto Label_0135;
            }
            if (this.Item_ContentsUnlock.get_gameObject().get_activeInHierarchy() == null)
            {
                goto Label_0135;
            }
            this.Item_ContentsUnlock.get_gameObject().SetActive(0);
        Label_0135:
            if ((this.Item_QuestSupport != null) == null)
            {
                goto Label_016C;
            }
            if (this.Item_QuestSupport.get_gameObject().get_activeInHierarchy() == null)
            {
                goto Label_016C;
            }
            this.Item_QuestSupport.get_gameObject().SetActive(0);
        Label_016C:
            mNotifyEnable = 1;
            Object.DontDestroyOnLoad(base.get_gameObject());
            return;
        }

        private void OnDestroy()
        {
            if ((mInstance == this) == null)
            {
                goto Label_0016;
            }
            mInstance = null;
        Label_0016:
            return;
        }

        private bool Push(NotifyListItem item)
        {
            RectTransform transform;
            float num;
            if ((item == null) == null)
            {
                goto Label_000E;
            }
            return 0;
        Label_000E:
            transform = item.get_transform() as RectTransform;
            item.get_gameObject().SetActive(1);
            num = LayoutUtility.GetPreferredHeight(transform);
            item.get_gameObject().SetActive(0);
            item.Lifetime = this.Interval;
            item.Height = num;
            Object.DontDestroyOnLoad(item.get_gameObject());
            this.mQueue.Add(item);
            return 1;
        }

        public static void Push(string msg)
        {
            NotifyListItem item;
            if ((mInstance != null) == null)
            {
                goto Label_004D;
            }
            if ((mInstance.Item_Generic != null) == null)
            {
                goto Label_004D;
            }
            item = Object.Instantiate<NotifyListItem>(mInstance.Item_Generic);
            item.Message.set_text(msg);
            mInstance.Push(item);
        Label_004D:
            return;
        }

        public static unsafe void PushAward(TrophyParam trophy)
        {
            object[] objArray1;
            int num;
            AwardParam param;
            NotifyListItem item;
            if (trophy != null)
            {
                goto Label_0007;
            }
            return;
        Label_0007:
            if ((mInstance != null) == null)
            {
                goto Label_00D1;
            }
            if ((mInstance.Item_Award != null) == null)
            {
                goto Label_00D1;
            }
            num = 0;
            goto Label_00C3;
        Label_0033:
            param = MonoSingleton<GameManager>.Instance.GetAwardParam(&(trophy.Items[num]).iname);
            if (param == null)
            {
                goto Label_009A;
            }
            item = Object.Instantiate<NotifyListItem>(mInstance.Item_Award);
            objArray1 = new object[] { param.name };
            item.Message.set_text(LocalizedText.Get("sys.AWARD_GET", objArray1));
            mInstance.Push(item);
            goto Label_00BF;
        Label_009A:
            DebugUtility.LogError("Not found trophy award. iname is [ " + &(trophy.Items[num]).iname + " ]");
        Label_00BF:
            num += 1;
        Label_00C3:
            if (num < ((int) trophy.Items.Length))
            {
                goto Label_0033;
            }
        Label_00D1:
            return;
        }

        public static void PushContentsUnlock(UnlockParam unlock)
        {
            object[] objArray1;
            string str;
            NotifyListItem item;
            if (unlock.UnlockTarget != 8)
            {
                goto Label_000D;
            }
            return;
        Label_000D:
            if ((mInstance != null) == null)
            {
                goto Label_008E;
            }
            if ((mInstance.Item_ContentsUnlock != null) == null)
            {
                goto Label_008E;
            }
            if (unlock == null)
            {
                goto Label_008E;
            }
            str = LocalizedText.Get("sys.UNLOCK_" + unlock.iname.ToUpper());
            item = Object.Instantiate<NotifyListItem>(mInstance.Item_ContentsUnlock);
            objArray1 = new object[] { str };
            item.Message.set_text(LocalizedText.Get("sys.NOTIFY_CONTENTSUNLOCK", objArray1));
            mInstance.Push(item);
        Label_008E:
            return;
        }

        public static void PushDailyTrophy(TrophyParam trophy)
        {
            object[] objArray1;
            NotifyListItem item;
            if ((mInstance != null) == null)
            {
                goto Label_0065;
            }
            if ((mInstance.Item_DailyMission != null) == null)
            {
                goto Label_0065;
            }
            item = Object.Instantiate<NotifyListItem>(mInstance.Item_DailyMission);
            objArray1 = new object[] { trophy.Name };
            item.Message.set_text(LocalizedText.Get("sys.TRPYCOMP", objArray1));
            mInstance.Push(item);
        Label_0065:
            return;
        }

        public static void PushLoginBonus(ItemData data)
        {
            object[] objArray1;
            NotifyListItem item;
            if ((mInstance != null) == null)
            {
                goto Label_006A;
            }
            if ((mInstance.Item_LoginBonus != null) == null)
            {
                goto Label_006A;
            }
            item = Object.Instantiate<NotifyListItem>(mInstance.Item_LoginBonus);
            objArray1 = new object[] { data.Param.name };
            item.Message.set_text(LocalizedText.Get("sys.LOGBO_TODAY", objArray1));
            mInstance.Push(item);
        Label_006A:
            return;
        }

        public static void PushMultiInvitation()
        {
            NotifyListItem item;
            if ((mInstance != null) == null)
            {
                goto Label_0056;
            }
            if ((mInstance.Item_MultiInvitation != null) == null)
            {
                goto Label_0056;
            }
            item = Object.Instantiate<NotifyListItem>(mInstance.Item_MultiInvitation);
            item.Message.set_text(LocalizedText.Get("sys.MULTIINVITATION_NOTIFY"));
            mInstance.Push(item);
        Label_0056:
            return;
        }

        public static void PushQuestSupport(int count, int gold)
        {
            object[] objArray1;
            NotifyListItem item;
            if ((mInstance != null) == null)
            {
                goto Label_006E;
            }
            if ((mInstance.Item_QuestSupport != null) == null)
            {
                goto Label_006E;
            }
            item = Object.Instantiate<NotifyListItem>(mInstance.Item_QuestSupport);
            objArray1 = new object[] { (int) count, (int) gold };
            item.Message.set_text(LocalizedText.Get("sys.NOTIFY_SUPPORT", objArray1));
            mInstance.Push(item);
        Label_006E:
            return;
        }

        public static void PushTrophy(TrophyParam trophy)
        {
            object[] objArray1;
            NotifyListItem item;
            if (trophy != null)
            {
                goto Label_0007;
            }
            return;
        Label_0007:
            if ((mInstance != null) == null)
            {
                goto Label_006C;
            }
            if ((mInstance.Item_Mission != null) == null)
            {
                goto Label_006C;
            }
            item = Object.Instantiate<NotifyListItem>(mInstance.Item_Mission);
            objArray1 = new object[] { trophy.Name };
            item.Message.set_text(LocalizedText.Get("sys.TRPYCOMP", objArray1));
            mInstance.Push(item);
        Label_006C:
            return;
        }

        [DebuggerHidden]
        private IEnumerator Start()
        {
            <Start>c__Iterator129 iterator;
            iterator = new <Start>c__Iterator129();
            iterator.<>f__this = this;
            return iterator;
        }

        private void Update()
        {
            NotifyListItem local2;
            NotifyListItem local1;
            float num;
            int num2;
            Animator animator;
            NotifyListItem item;
            RectTransform transform;
            int num3;
            num = Time.get_unscaledDeltaTime();
            if (this.mItems.Count > 0)
            {
                goto Label_0028;
            }
            if (this.mQueue.Count <= 0)
            {
                goto Label_0036;
            }
        Label_0028:
            this.mGroupTime += num;
        Label_0036:
            if (this.mItems.Count <= 0)
            {
                goto Label_0124;
            }
            num2 = 0;
            goto Label_00EC;
        Label_004E:
            local1 = this.mItems[num2];
            local1.Lifetime -= num;
            if (this.mItems[num2].Lifetime > 0f)
            {
                goto Label_00E8;
            }
            if (string.IsNullOrEmpty(this.FadeTrigger) != null)
            {
                goto Label_00BC;
            }
            animator = this.mItems[num2].GetComponent<Animator>();
            if ((animator != null) == null)
            {
                goto Label_00BC;
            }
            animator.SetTrigger(this.FadeTrigger);
        Label_00BC:
            Object.Destroy(this.mItems[num2].get_gameObject(), this.FadeTime);
            this.mItems.RemoveAt(num2);
            num2 -= 1;
        Label_00E8:
            num2 += 1;
        Label_00EC:
            if (num2 < this.mItems.Count)
            {
                goto Label_004E;
            }
            if (this.mItems.Count > 0)
            {
                goto Label_0124;
            }
            this.mGroupTime = 0f;
            this.mStackHeight = 0f;
        Label_0124:
            if (this.mQueue.Count <= 0)
            {
                goto Label_028F;
            }
            if (this.mItems.Count != null)
            {
                goto Label_0150;
            }
            this.mGroupTime = 0f;
        Label_0150:
            item = this.mQueue[0];
            if (((this.mStackHeight + this.mQueue[0].Height) + this.Spacing) > this.MaxHeight)
            {
                goto Label_028F;
            }
            if (this.mGroupTime >= this.GroupSpan)
            {
                goto Label_028F;
            }
            if (this.mQueue[0].Lifetime <= 0f)
            {
                goto Label_01D1;
            }
            local2 = this.mQueue[0];
            local2.Lifetime -= num;
            goto Label_028F;
        Label_01D1:
            if (mNotifyEnable == null)
            {
                goto Label_028F;
            }
            transform = item.get_transform() as RectTransform;
            transform.SetParent(this.ListParent, 0);
            transform.set_anchoredPosition(new Vector2(0f, -this.mStackHeight));
            this.mStackHeight += item.Height + this.Spacing;
            item.get_gameObject().SetActive(1);
            this.mItems.Add(item);
            this.mQueue.RemoveAt(0);
            num3 = 0;
            goto Label_027D;
        Label_0254:
            this.mItems[num3].Lifetime = this.Lifetime + (((float) num3) * this.FadeInterval);
            num3 += 1;
        Label_027D:
            if (num3 < this.mItems.Count)
            {
                goto Label_0254;
            }
        Label_028F:
            return;
        }

        public static bool hasInstance
        {
            get
            {
                return (mInstance != null);
            }
        }

        public static bool mNotifyEnable
        {
            [CompilerGenerated]
            get
            {
                return <mNotifyEnable>k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                <mNotifyEnable>k__BackingField = value;
                return;
            }
        }

        [CompilerGenerated]
        private sealed class <Start>c__Iterator129 : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal int <i>__0;
            internal int $PC;
            internal object $current;
            internal NotifyList <>f__this;

            public <Start>c__Iterator129()
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
                        goto Label_0075;
                }
                goto Label_00BE;
            Label_0021:
                if (this.<>f__this.DebugItems == null)
                {
                    goto Label_00B7;
                }
                this.<i>__0 = 0;
                goto Label_009F;
            Label_003D:
                if (string.IsNullOrEmpty(this.<>f__this.DebugItems[this.<i>__0]) == null)
                {
                    goto Label_007A;
                }
                this.$current = new WaitForSeconds(1f);
                this.$PC = 1;
                goto Label_00C0;
            Label_0075:
                goto Label_0091;
            Label_007A:
                NotifyList.Push(this.<>f__this.DebugItems[this.<i>__0]);
            Label_0091:
                this.<i>__0 += 1;
            Label_009F:
                if (this.<i>__0 < ((int) this.<>f__this.DebugItems.Length))
                {
                    goto Label_003D;
                }
            Label_00B7:
                this.$PC = -1;
            Label_00BE:
                return 0;
            Label_00C0:
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

