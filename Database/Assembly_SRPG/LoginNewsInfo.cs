namespace SRPG
{
    using GR;
    using System;
    using UnityEngine;
    using UnityEngine.UI;

    [Pin(2, "OpenNews", 1, 2), Pin(1, "SetUrl", 0, 1), NodeType("System/LoginNewsInfo", 0x7fe5)]
    public class LoginNewsInfo : FlowNode
    {
        private const int TEXT_LIMIT_LENGTH = 0x34;
        private const int PIN_SET_URL = 1;
        private const int PIN_OPEN_NEWS = 2;
        private const string URL_KEY = "LOGIN_NEWS_URL";
        [SerializeField]
        public GameObject mRootNewsInfoObj;
        [SerializeField]
        public Text mTitleText;
        private static JSON_PubInfo mPubinfo;
        private JSON_PubInfo mCurrentPubInfo;
        private static bool isShowNews;

        public LoginNewsInfo()
        {
            base..ctor();
            return;
        }

        protected override void Awake()
        {
            LoginInfoParam[] paramArray;
            paramArray = MonoSingleton<GameManager>.GetInstanceDirect().MasterParam.GetActiveLoginInfos();
            if (paramArray == null)
            {
                goto Label_001F;
            }
            if (((int) paramArray.Length) > 0)
            {
                goto Label_002C;
            }
        Label_001F:
            this.mRootNewsInfoObj.SetActive(0);
            return;
        Label_002C:
            if (mPubinfo != null)
            {
                goto Label_0043;
            }
            this.mRootNewsInfoObj.SetActive(0);
            return;
        Label_0043:
            this.mCurrentPubInfo = mPubinfo;
            isShowNews = 1;
            this.mRootNewsInfoObj.SetActive(1);
            this.Refresh();
            return;
        }

        public static bool IsChangePubInfo()
        {
            string str;
            if (mPubinfo != null)
            {
                goto Label_000C;
            }
            return 0;
        Label_000C:
            if (isShowNews == null)
            {
                goto Label_0018;
            }
            return 0;
        Label_0018:
            str = PlayerPrefsUtility.GetString(GameUtility.BEFORE_LOGIN_NEWS_INFO_TOKEN, string.Empty);
            if (string.IsNullOrEmpty(str) != null)
            {
                goto Label_0047;
            }
            if (string.IsNullOrEmpty(mPubinfo.token) == null)
            {
                goto Label_0049;
            }
        Label_0047:
            return 0;
        Label_0049:
            if ((str != Pubinfo.token) == null)
            {
                goto Label_0060;
            }
            return 1;
        Label_0060:
            return 0;
        }

        public override void OnActivate(int pinID)
        {
            if (pinID != 1)
            {
                goto Label_003A;
            }
            if (this.mCurrentPubInfo != null)
            {
                goto Label_0022;
            }
            FlowNode_Variable.Set("LOGIN_NEWS_URL", Network.NewsHost);
            return;
        Label_0022:
            FlowNode_Variable.Set("LOGIN_NEWS_URL", this.PageID);
            base.ActivateOutputLinks(2);
        Label_003A:
            return;
        }

        protected override void OnDestroy()
        {
            isShowNews = 0;
            mPubinfo = null;
            return;
        }

        private unsafe void Refresh()
        {
            float num;
            bool flag;
            Vector2 vector;
            Vector2 vector2;
            Vector2 vector3;
            if (this.mCurrentPubInfo != null)
            {
                goto Label_000C;
            }
            return;
        Label_000C:
            this.mRootNewsInfoObj.SetActive(1);
            if (string.IsNullOrEmpty(this.mCurrentPubInfo.message) != null)
            {
                goto Label_01B5;
            }
            this.mTitleText.set_text(this.mCurrentPubInfo.message);
            num = this.mTitleText.get_preferredHeight() - &this.mTitleText.get_rectTransform().get_sizeDelta().y;
            if (num <= 0f)
            {
                goto Label_01B5;
            }
            flag = 1;
            if (this.mTitleText.get_text()[this.mTitleText.get_text().Length - 1] != 10)
            {
                goto Label_014C;
            }
            flag = 0;
            this.mTitleText.set_text(this.mTitleText.get_text().Remove(this.mTitleText.get_text().Length - 1));
            num = this.mTitleText.get_preferredHeight() - &this.mTitleText.get_rectTransform().get_sizeDelta().y;
            goto Label_014C;
        Label_00F7:
            this.mTitleText.set_text(this.mTitleText.get_text().Remove(this.mTitleText.get_text().Length - 1));
            num = this.mTitleText.get_preferredHeight() - &this.mTitleText.get_rectTransform().get_sizeDelta().y;
            flag = 1;
        Label_014C:
            if (num > 0f)
            {
                goto Label_00F7;
            }
            if (flag == null)
            {
                goto Label_01B5;
            }
            this.mTitleText.set_text(this.mTitleText.get_text().Remove(this.mTitleText.get_text().Length - 1));
            this.mTitleText.set_text(this.mTitleText.get_text() + string.Format(LocalizedText.Get("sys.TEXT_OVER_SUBSTITUTION"), new object[0]));
        Label_01B5:
            return;
        }

        public static void SetPubInfo(JSON_PubInfo pubinfo)
        {
            if (pubinfo != null)
            {
                goto Label_0007;
            }
            return;
        Label_0007:
            if (mPubinfo == null)
            {
                goto Label_0027;
            }
            PlayerPrefsUtility.SetString(GameUtility.BEFORE_LOGIN_NEWS_INFO_TOKEN, mPubinfo.token, 0);
        Label_0027:
            mPubinfo = pubinfo;
            return;
        }

        public static void UpdateBeforePubInfo()
        {
            if (mPubinfo != null)
            {
                goto Label_000B;
            }
            return;
        Label_000B:
            PlayerPrefsUtility.SetString(GameUtility.BEFORE_LOGIN_NEWS_INFO_TOKEN, mPubinfo.token, 0);
            return;
        }

        public static JSON_PubInfo Pubinfo
        {
            get
            {
                return mPubinfo;
            }
        }

        public string PageID
        {
            get
            {
                return ("?id=" + this.mCurrentPubInfo.id);
            }
        }

        public class JSON_PubInfo
        {
            public string id;
            public string token;
            public string category;
            public string message;

            public JSON_PubInfo()
            {
                base..ctor();
                return;
            }
        }
    }
}

