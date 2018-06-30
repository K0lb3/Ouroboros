namespace SRPG
{
    using GR;
    using System;
    using UnityEngine;
    using UnityEngine.UI;

    public class LimitedShopListItem : MonoBehaviour
    {
        public JSON_ShopListArray.Shops shops;
        public LText l_text;
        public GameObject Body;
        public Text Timer;
        private long mEndTime;
        private float mRefreshInterval;
        public Image banner;
        public string banner_sprite;
        public bool btn_update;
        public string LimitedShopSpritePath;

        public LimitedShopListItem()
        {
            this.mRefreshInterval = 1f;
            this.LimitedShopSpritePath = "LimitedShopBanner/LimitedShopSprites";
            base..ctor();
            return;
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

        public void SetShopList(JSON_ShopListArray.Shops shops)
        {
            Json_ShopMsgResponse response;
            GachaTabSprites sprites;
            Sprite[] spriteArray;
            int num;
            this.shops = shops;
            if ((shops.info == null) || (shops.info.msg == null))
            {
                goto Label_0099;
            }
        Label_0022:
            try
            {
                response = JSONParser.parseJSONObject<Json_ShopMsgResponse>(shops.info.msg);
                goto Label_005A;
            }
            catch (Exception)
            {
            Label_0038:
                Debug.LogError("Parse failed: " + shops.info.msg);
                response = null;
                goto Label_005A;
            }
        Label_005A:
            if (response == null)
            {
                goto Label_0099;
            }
            this.banner_sprite = response.banner;
            if (response.update == null)
            {
                goto Label_0099;
            }
            this.btn_update = (response.update.Equals("on") == null) ? 0 : 1;
        Label_0099:
            sprites = AssetManager.Load<GachaTabSprites>(this.LimitedShopSpritePath);
            if ((sprites != null) == null)
            {
                goto Label_0119;
            }
            if (sprites.Sprites == null)
            {
                goto Label_0119;
            }
            if (((int) sprites.Sprites.Length) <= 0)
            {
                goto Label_0119;
            }
            spriteArray = sprites.Sprites;
            num = 0;
            goto Label_0110;
        Label_00D8:
            if ((spriteArray[num] != null) == null)
            {
                goto Label_010C;
            }
            if ((spriteArray[num].get_name() == this.banner_sprite) == null)
            {
                goto Label_010C;
            }
            this.banner.set_sprite(spriteArray[num]);
        Label_010C:
            num += 1;
        Label_0110:
            if (num < ((int) spriteArray.Length))
            {
                goto Label_00D8;
            }
        Label_0119:
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
            if (this.shops == null)
            {
                goto Label_002B;
            }
            this.mEndTime = this.shops.end;
            this.Refresh();
            return;
        Label_002B:
            return;
        }
    }
}

