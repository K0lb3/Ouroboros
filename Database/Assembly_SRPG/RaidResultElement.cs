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

    public class RaidResultElement : MonoBehaviour, IFlowInterface
    {
        public Text TxtTitle;
        public Text TxtExp;
        public Text TxtGold;
        public Transform ItemParent;
        public GameObject ItemTemplate;
        [Description("入手アイテムを可視状態に切り替えるトリガー")]
        public string Treasure_TurnOnTrigger;
        [Description("入手アイテムを可視状態に切り替える間隔 (秒数)")]
        public float Treasure_TriggerInterval;
        private List<GameObject> mItems;
        private bool mRequest;
        private bool mFinished;
        private float mTimeScale;

        public RaidResultElement()
        {
            this.Treasure_TurnOnTrigger = "on";
            this.Treasure_TriggerInterval = 1f;
            this.mTimeScale = 1f;
            base..ctor();
            return;
        }

        public void Activated(int pinID)
        {
        }

        public bool IsFinished()
        {
            return this.mFinished;
        }

        public bool IsRequest()
        {
            return this.mRequest;
        }

        public unsafe void RequestAnimation()
        {
            RaidQuestResult result;
            int num;
            GameObject obj2;
            result = DataSource.FindDataOfClass<RaidQuestResult>(base.get_gameObject(), null);
            if (result != null)
            {
                goto Label_001B;
            }
            this.mFinished = 1;
            return;
        Label_001B:
            if (this.IsRequest() != null)
            {
                goto Label_0031;
            }
            if (this.IsFinished() == null)
            {
                goto Label_0032;
            }
        Label_0031:
            return;
        Label_0032:
            this.mRequest = 1;
            if ((this.TxtTitle != null) == null)
            {
                goto Label_0071;
            }
            this.TxtTitle.set_text(string.Format(LocalizedText.Get("sys.RAID_RESULT_INDEX"), (int) (result.index + 1)));
        Label_0071:
            if ((this.TxtExp != null) == null)
            {
                goto Label_0098;
            }
            this.TxtExp.set_text(&result.uexp.ToString());
        Label_0098:
            if ((this.TxtGold != null) == null)
            {
                goto Label_00BF;
            }
            this.TxtGold.set_text(&result.gold.ToString());
        Label_00BF:
            if (result.drops == null)
            {
                goto Label_0140;
            }
            this.mItems = new List<GameObject>((int) result.drops.Length);
            num = 0;
            goto Label_0132;
        Label_00E4:
            if (result.drops[num] != null)
            {
                goto Label_00F6;
            }
            goto Label_012E;
        Label_00F6:
            obj2 = Object.Instantiate<GameObject>(this.ItemTemplate);
            obj2.get_transform().SetParent(this.ItemParent, 0);
            DataSource.Bind<QuestResult.DropItemData>(obj2, result.drops[num]);
            this.mItems.Add(obj2);
        Label_012E:
            num += 1;
        Label_0132:
            if (num < ((int) result.drops.Length))
            {
                goto Label_00E4;
            }
        Label_0140:
            base.get_gameObject().SetActive(1);
            GameParameter.UpdateAll(base.get_gameObject());
            base.StartCoroutine(this.TreasureAnimation());
            return;
        }

        public void Start()
        {
            if ((this.ItemTemplate != null) == null)
            {
                goto Label_001D;
            }
            this.ItemTemplate.SetActive(0);
        Label_001D:
            return;
        }

        [DebuggerHidden]
        private IEnumerator TreasureAnimation()
        {
            <TreasureAnimation>c__Iterator136 iterator;
            iterator = new <TreasureAnimation>c__Iterator136();
            iterator.<>f__this = this;
            return iterator;
        }

        public float TimeScale
        {
            get
            {
                return this.mTimeScale;
            }
            set
            {
                this.mTimeScale = Mathf.Clamp(value, 0.1f, 10f);
                return;
            }
        }

        [CompilerGenerated]
        private sealed class <TreasureAnimation>c__Iterator136 : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal int <index>__0;
            internal float <time>__1;
            internal int $PC;
            internal object $current;
            internal RaidResultElement <>f__this;

            public <TreasureAnimation>c__Iterator136()
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
                        goto Label_0132;
                }
                goto Label_0160;
            Label_0021:
                if (this.<>f__this.mItems == null)
                {
                    goto Label_014D;
                }
                this.<index>__0 = 0;
                this.<time>__1 = 0f;
                goto Label_0132;
            Label_0048:
                this.<time>__1 += Time.get_deltaTime() * this.<>f__this.mTimeScale;
                goto Label_0100;
            Label_006B:
                if (this.<time>__1 < this.<>f__this.Treasure_TriggerInterval)
                {
                    goto Label_011B;
                }
                if (string.IsNullOrEmpty(this.<>f__this.Treasure_TurnOnTrigger) != null)
                {
                    goto Label_00D0;
                }
                MonoSingleton<MySound>.Instance.PlaySEOneShot("SE_0508", 0f);
                GameUtility.SetAnimatorTrigger(this.<>f__this.mItems[this.<index>__0], this.<>f__this.Treasure_TurnOnTrigger);
            Label_00D0:
                this.<time>__1 -= this.<>f__this.Treasure_TriggerInterval;
                this.<index>__0 += 1;
                goto Label_0100;
                goto Label_011B;
            Label_0100:
                if (this.<index>__0 < this.<>f__this.mItems.Count)
                {
                    goto Label_006B;
                }
            Label_011B:
                this.$current = new WaitForEndOfFrame();
                this.$PC = 1;
                goto Label_0162;
            Label_0132:
                if (this.<index>__0 < this.<>f__this.mItems.Count)
                {
                    goto Label_0048;
                }
            Label_014D:
                this.<>f__this.mFinished = 1;
                this.$PC = -1;
            Label_0160:
                return 0;
            Label_0162:
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

