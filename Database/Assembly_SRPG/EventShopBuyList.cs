namespace SRPG
{
    using System;
    using UnityEngine;
    using UnityEngine.UI;

    public class EventShopBuyList : MonoBehaviour
    {
        public GameObject amount;
        public GameObject day_reset;
        public GameObject limit;
        public GameObject icon_set;
        public GameObject sold_count;
        public GameObject no_limited_price;
        public Text amount_text;
        public Button self_button;
        [HeaderBar("▼アイテム表示用")]
        public GameObject item_name;
        public GameObject item_icon;
        [HeaderBar("▼武具表示用")]
        public GameObject artifact_name;
        public GameObject artifact_icon;
        [HeaderBar("▼真理念装表示用")]
        public GameObject conceptCard_name;
        public ConceptCardIcon conceptCard_icon;
        [Space(10f)]
        public GameObject TimeLimitBase;
        public GameObject TimeLimitPopup;
        public Text TimeLimitText;
        private float mRefreshInterval;
        private long mEndTime;
        private string mDayLimit;
        private string mHourLimit;
        private string mMinuteLimit;
        private EventShopItem mEventShopItem;

        public EventShopBuyList()
        {
            this.mRefreshInterval = 1f;
            base..ctor();
            return;
        }

        private void Awake()
        {
            this.mDayLimit = LocalizedText.Get("sys.SHOP_TIMELIMIT_D");
            this.mHourLimit = LocalizedText.Get("sys.SHOP_TIMELIMIT_H");
            this.mMinuteLimit = LocalizedText.Get("sys.SHOP_TIMELIMIT_M");
            this.Refresh();
            return;
        }

        private unsafe void Refresh()
        {
            DateTime time;
            DateTime time2;
            DateTime time3;
            TimeSpan span;
            Color color;
            string str;
            if (this.mEventShopItem.is_soldout == null)
            {
                goto Label_002E;
            }
            if ((this.TimeLimitBase != null) == null)
            {
                goto Label_002D;
            }
            this.TimeLimitBase.SetActive(0);
        Label_002D:
            return;
        Label_002E:
            if (this.mEndTime > 0L)
            {
                goto Label_0059;
            }
            if ((this.TimeLimitBase != null) == null)
            {
                goto Label_0058;
            }
            this.TimeLimitBase.SetActive(0);
        Label_0058:
            return;
        Label_0059:
            time = TimeManager.ServerTime;
            time2 = TimeManager.FromUnixTime(this.mEndTime);
            time3 = TimeManager.FromUnixTime(GlobalVars.EventShopItem.shops.end);
            if ((time2 > time3) == null)
            {
                goto Label_00AA;
            }
            if ((this.TimeLimitBase != null) == null)
            {
                goto Label_00A9;
            }
            this.TimeLimitBase.SetActive(0);
        Label_00A9:
            return;
        Label_00AA:
            span = time2 - time;
            if (&span.TotalDays < 8.0)
            {
                goto Label_00C8;
            }
            return;
        Label_00C8:
            color = (&span.TotalDays >= 1.0) ? Color.get_yellow() : Color.get_red();
            if ((this.TimeLimitBase != null) == null)
            {
                goto Label_010B;
            }
            this.TimeLimitBase.SetActive(1);
        Label_010B:
            str = null;
            if (&span.TotalDays < 1.0)
            {
                goto Label_0141;
            }
            str = string.Format(this.mDayLimit, (int) &span.Days);
            goto Label_01E1;
        Label_0141:
            if (&span.TotalHours < 1.0)
            {
                goto Label_0174;
            }
            str = string.Format(this.mHourLimit, (int) &span.Hours);
            goto Label_01E1;
        Label_0174:
            if (&span.TotalSeconds <= 0.0)
            {
                goto Label_01C4;
            }
            str = string.Format(this.mMinuteLimit, (int) &span.Minutes);
            if ((this.TimeLimitPopup != null) == null)
            {
                goto Label_01E1;
            }
            this.TimeLimitPopup.SetActive(1);
            goto Label_01E1;
        Label_01C4:
            if ((this.self_button != null) == null)
            {
                goto Label_01E1;
            }
            this.self_button.set_interactable(0);
        Label_01E1:
            if ((this.TimeLimitText != null) == null)
            {
                goto Label_0223;
            }
            if ((this.TimeLimitText.get_text() != str) == null)
            {
                goto Label_0223;
            }
            this.TimeLimitText.set_color(color);
            this.TimeLimitText.set_text(str);
        Label_0223:
            return;
        }

        public void SetupConceptCard(ConceptCardData conceptCardData)
        {
            if ((this.conceptCard_icon == null) == null)
            {
                goto Label_0012;
            }
            return;
        Label_0012:
            this.conceptCard_icon.Setup(conceptCardData);
            return;
        }

        private void Update()
        {
            this.mRefreshInterval -= Time.get_unscaledDeltaTime();
            if (this.mRefreshInterval > 0f)
            {
                goto Label_0033;
            }
            this.Refresh();
            this.mRefreshInterval = 1f;
        Label_0033:
            return;
        }

        public EventShopItem eventShopItem
        {
            get
            {
                return this.mEventShopItem;
            }
            set
            {
                int num;
                this.mEventShopItem = value;
                GameUtility.SetGameObjectActive(this.day_reset, this.mEventShopItem.is_reset);
                GameUtility.SetGameObjectActive(this.limit, this.mEventShopItem.is_reset == 0);
                GameUtility.SetGameObjectActive(this.sold_count, this.mEventShopItem.IsNotLimited == 0);
                GameUtility.SetGameObjectActive(this.no_limited_price, this.mEventShopItem.IsNotLimited);
                GameUtility.SetGameObjectActive(this.amount, this.mEventShopItem.IsSet == 0);
                if (this.mEventShopItem.IsItem != null)
                {
                    goto Label_009E;
                }
                if (this.mEventShopItem.IsSet == null)
                {
                    goto Label_00EB;
                }
            Label_009E:
                GameUtility.SetGameObjectActive(this.item_name, 1);
                GameUtility.SetGameObjectActive(this.item_icon, 1);
                GameUtility.SetGameObjectActive(this.artifact_name, 0);
                GameUtility.SetGameObjectActive(this.artifact_icon, 0);
                GameUtility.SetGameObjectActive(this.conceptCard_name, 0);
                GameUtility.SetGameObjectActive(this.conceptCard_icon, 0);
                goto Label_01A0;
            Label_00EB:
                if (this.mEventShopItem.IsArtifact == null)
                {
                    goto Label_0148;
                }
                GameUtility.SetGameObjectActive(this.item_name, 0);
                GameUtility.SetGameObjectActive(this.item_icon, 0);
                GameUtility.SetGameObjectActive(this.artifact_name, 1);
                GameUtility.SetGameObjectActive(this.artifact_icon, 1);
                GameUtility.SetGameObjectActive(this.conceptCard_name, 0);
                GameUtility.SetGameObjectActive(this.conceptCard_icon, 0);
                goto Label_01A0;
            Label_0148:
                if (this.mEventShopItem.IsConceptCard == null)
                {
                    goto Label_01A0;
                }
                GameUtility.SetGameObjectActive(this.item_name, 0);
                GameUtility.SetGameObjectActive(this.item_icon, 0);
                GameUtility.SetGameObjectActive(this.artifact_name, 0);
                GameUtility.SetGameObjectActive(this.artifact_icon, 0);
                GameUtility.SetGameObjectActive(this.conceptCard_name, 1);
                GameUtility.SetGameObjectActive(this.conceptCard_icon, 1);
            Label_01A0:
                if ((this.amount_text != null) == null)
                {
                    goto Label_01CF;
                }
                this.amount_text.set_text(&this.mEventShopItem.remaining_num.ToString());
            Label_01CF:
                this.mEndTime = this.mEventShopItem.end;
                return;
            }
        }
    }
}

