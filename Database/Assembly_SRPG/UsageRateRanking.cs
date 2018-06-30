namespace SRPG
{
    using GR;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using UnityEngine;
    using UnityEngine.Events;
    using UnityEngine.UI;

    [Pin(1, "Refresh", 0, 1)]
    public class UsageRateRanking : MonoBehaviour, IFlowInterface
    {
        public GameObject ItemBase;
        public GameObject Parent;
        public GameObject Aggregating;
        private List<UsageRateRankingItem> Items;
        public Scrollbar ItemScrollBar;
        private ViewInfoType mNowViewInfoType;
        public Toggle[] RankingToggle;
        public static readonly string[] ViewInfo;

        static UsageRateRanking()
        {
            string[] textArray1;
            textArray1 = new string[] { "quest", "arena", "tower_match" };
            ViewInfo = textArray1;
            return;
        }

        public UsageRateRanking()
        {
            this.Items = new List<UsageRateRankingItem>();
            base..ctor();
            return;
        }

        public void Activated(int pinID)
        {
            if (pinID != 1)
            {
                goto Label_000D;
            }
            this.Refresh();
        Label_000D:
            return;
        }

        public void OnChangedToggle(ViewInfoType index)
        {
            int num;
            this.mNowViewInfoType = index;
            if ((this.ItemScrollBar != null) == null)
            {
                goto Label_0028;
            }
            this.ItemScrollBar.set_value(1f);
        Label_0028:
            num = 0;
            goto Label_0049;
        Label_002F:
            this.RankingToggle[num].set_isOn(this.NowViewInfoIndex == num);
            num += 1;
        Label_0049:
            if (num < ((int) this.RankingToggle.Length))
            {
                goto Label_002F;
            }
            this.Refresh();
            return;
        }

        private void OnChangedToggle(int index)
        {
            this.OnChangedToggle((byte) index);
            return;
        }

        private void Refresh()
        {
            Dictionary<string, RankingData> dictionary;
            RankingData data;
            int num;
            GameObject obj2;
            UsageRateRankingItem item;
            int num2;
            dictionary = MonoSingleton<GameManager>.Instance.UnitRanking;
            if (dictionary.ContainsKey(this.NowViewInfo) != null)
            {
                goto Label_0029;
            }
            this.Aggregating.SetActive(1);
            return;
        Label_0029:
            data = dictionary[this.NowViewInfo];
            this.Aggregating.SetActive((data.isReady == 1) == 0);
            if (data.isReady == 1)
            {
                goto Label_005A;
            }
            return;
        Label_005A:
            num = 0;
            goto Label_00E4;
        Label_0061:
            if (this.Items.Count > num)
            {
                goto Label_00C4;
            }
            obj2 = Object.Instantiate<GameObject>(this.ItemBase);
            obj2.get_transform().SetParent(this.Parent.get_transform(), 0);
            item = obj2.GetComponent<UsageRateRankingItem>();
            if ((item != null) == null)
            {
                goto Label_00C4;
            }
            item.get_gameObject().SetActive(1);
            this.Items.Add(item);
        Label_00C4:
            this.Items[num].Refresh(num + 1, data.ranking[num]);
            num += 1;
        Label_00E4:
            if (num < ((int) data.ranking.Length))
            {
                goto Label_0061;
            }
            GameParameter.UpdateAll(base.get_gameObject());
            if (((int) data.ranking.Length) >= this.Items.Count)
            {
                goto Label_0154;
            }
            num2 = (int) data.ranking.Length;
            goto Label_0142;
        Label_0124:
            this.Items[num2].get_gameObject().SetActive(0);
            num2 += 1;
        Label_0142:
            if (num2 < this.Items.Count)
            {
                goto Label_0124;
            }
        Label_0154:
            return;
        }

        public void Start()
        {
            int num;
            <Start>c__AnonStorey3E6 storeye;
            if (this.RankingToggle == null)
            {
                goto Label_006E;
            }
            num = 0;
            goto Label_0060;
        Label_0012:
            storeye = new <Start>c__AnonStorey3E6();
            storeye.<>f__this = this;
            if ((this.RankingToggle[num] == null) == null)
            {
                goto Label_0037;
            }
            goto Label_005C;
        Label_0037:
            storeye.index = num;
            this.RankingToggle[num].onValueChanged.AddListener(new UnityAction<bool>(storeye, this.<>m__491));
        Label_005C:
            num += 1;
        Label_0060:
            if (num < ((int) this.RankingToggle.Length))
            {
                goto Label_0012;
            }
        Label_006E:
            return;
        }

        public byte NowViewInfoIndex
        {
            get
            {
                return this.mNowViewInfoType;
            }
        }

        public string NowViewInfo
        {
            get
            {
                return ViewInfo[this.NowViewInfoIndex];
            }
        }

        [CompilerGenerated]
        private sealed class <Start>c__AnonStorey3E6
        {
            internal int index;
            internal UsageRateRanking <>f__this;

            public <Start>c__AnonStorey3E6()
            {
                base..ctor();
                return;
            }

            internal void <>m__491(bool value)
            {
                if (value == null)
                {
                    goto Label_0017;
                }
                this.<>f__this.OnChangedToggle(this.index);
            Label_0017:
                return;
            }
        }

        public enum ViewInfoType : byte
        {
            Quest = 0,
            Arena = 1,
            TowerMatch = 2,
            Num = 3
        }
    }
}

