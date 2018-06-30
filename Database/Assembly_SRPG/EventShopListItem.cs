namespace SRPG
{
    using GR;
    using System;
    using System.Runtime.CompilerServices;
    using UnityEngine;
    using UnityEngine.UI;

    public class EventShopListItem : MonoBehaviour
    {
        public LText l_text;
        public GameObject Body;
        public Text Timer;
        private long mEndTime;
        private float mRefreshInterval;
        public Image banner;
        public string EventShopSpritePath;
        public GameObject mPaidCoinIcon;
        public GameObject mPaidCoinNum;
        public GameObject mLockObject;
        public Text mLockText;
        public SRPG.EventShopInfo EventShopInfo;

        public EventShopListItem()
        {
            this.mRefreshInterval = 1f;
            this.EventShopSpritePath = "EventShopBanner/EventShopSprites";
            this.EventShopInfo = new SRPG.EventShopInfo();
            base..ctor();
            return;
        }

        [CompilerGenerated]
        private bool <SetShopList>m__313(EventCoinData f)
        {
            return f.iname.Equals(this.EventShopInfo.shop_cost_iname);
        }

        [CompilerGenerated]
        private bool <SetShopList>m__314(ItemParam f)
        {
            return f.iname.Equals(this.EventShopInfo.shop_cost_iname);
        }

        private unsafe void Refresh()
        {
            object[] objArray3;
            object[] objArray2;
            object[] objArray1;
            DateTime time;
            DateTime time2;
            TimeSpan span;
            string str;
            if (this.mEndTime > 0L)
            {
                goto Label_002B;
            }
            if ((this.Body != null) == null)
            {
                goto Label_002A;
            }
            this.Body.SetActive(0);
        Label_002A:
            return;
        Label_002B:
            if ((this.Body != null) == null)
            {
                goto Label_0048;
            }
            this.Body.SetActive(1);
        Label_0048:
            time = TimeManager.ServerTime;
            span = TimeManager.FromUnixTime(this.mEndTime) - time;
            str = null;
            if (&span.TotalDays < 1.0)
            {
                goto Label_009E;
            }
            objArray1 = new object[] { (int) &span.Days };
            str = LocalizedText.Get("sys.QUEST_TIMELIMIT_D", objArray1);
            goto Label_00FE;
        Label_009E:
            if (&span.TotalHours < 1.0)
            {
                goto Label_00D8;
            }
            objArray2 = new object[] { (int) &span.Hours };
            str = LocalizedText.Get("sys.QUEST_TIMELIMIT_H", objArray2);
            goto Label_00FE;
        Label_00D8:
            objArray3 = new object[] { (int) Mathf.Max(&span.Minutes, 0) };
            str = LocalizedText.Get("sys.QUEST_TIMELIMIT_M", objArray3);
        Label_00FE:
            if ((this.Timer != null) == null)
            {
                goto Label_0131;
            }
            if ((this.Timer.get_text() != str) == null)
            {
                goto Label_0131;
            }
            this.Timer.set_text(str);
        Label_0131:
            return;
        }

        public void SetShopList(JSON_ShopListArray.Shops shops, Json_ShopMsgResponse msg)
        {
            GachaTabSprites sprites;
            Sprite[] spriteArray;
            int num;
            EventCoinData data;
            ItemParam param;
            bool flag;
            Button button;
            this.EventShopInfo.shops = shops;
            if (msg == null)
            {
                goto Label_0066;
            }
            this.EventShopInfo.banner_sprite = msg.banner;
            this.EventShopInfo.shop_cost_iname = msg.costiname;
            if (msg.update == null)
            {
                goto Label_0066;
            }
            this.EventShopInfo.btn_update = (msg.update.Equals("on") == null) ? 0 : 1;
        Label_0066:
            sprites = AssetManager.Load<GachaTabSprites>(this.EventShopSpritePath);
            if ((((sprites != null) == null) || (sprites.Sprites == null)) || (((int) sprites.Sprites.Length) <= 0))
            {
                goto Label_00EB;
            }
            spriteArray = sprites.Sprites;
            num = 0;
            goto Label_00E2;
        Label_00A5:
            if (((spriteArray[num] != null) == null) || ((spriteArray[num].get_name() == this.EventShopInfo.banner_sprite) == null))
            {
                goto Label_00DE;
            }
            this.banner.set_sprite(spriteArray[num]);
        Label_00DE:
            num += 1;
        Label_00E2:
            if (num < ((int) spriteArray.Length))
            {
                goto Label_00A5;
            }
        Label_00EB:
            data = MonoSingleton<GameManager>.Instance.Player.EventCoinList.Find(new Predicate<EventCoinData>(this.<SetShopList>m__313));
            if (data != null)
            {
                goto Label_0142;
            }
            data = new EventCoinData();
            param = MonoSingleton<GameManager>.Instance.MasterParam.Items.Find(new Predicate<ItemParam>(this.<SetShopList>m__314));
            data.param = param;
        Label_0142:
            DataSource.Bind<ItemParam>(this.mPaidCoinIcon, data.param);
            DataSource.Bind<EventCoinData>(this.mPaidCoinNum, data);
            if (((shops.unlock == null) || ((this.mLockObject != null) == null)) || ((this.mLockText != null) == null))
            {
                goto Label_022E;
            }
            flag = (shops.unlock.flg != 1) ? 0 : 1;
            button = base.GetComponent<Button>();
            if (button == null)
            {
                goto Label_022E;
            }
            if (flag == null)
            {
                goto Label_01EA;
            }
            button.set_interactable(1);
            this.mLockObject.SetActive(0);
            this.mLockText.set_text(string.Empty);
            goto Label_022E;
        Label_01EA:
            button.set_interactable(0);
            this.mLockObject.SetActive(1);
            this.mLockText.set_text((shops.unlock.message != null) ? shops.unlock.message : string.Empty);
        Label_022E:
            return;
        }

        private void Start()
        {
            this.UpdateValue();
            this.Refresh();
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

        public void UpdateValue()
        {
            this.mEndTime = 0L;
            if (this.EventShopInfo.shops == null)
            {
                goto Label_0035;
            }
            this.mEndTime = this.EventShopInfo.shops.end;
            this.Refresh();
            return;
        Label_0035:
            return;
        }
    }
}

