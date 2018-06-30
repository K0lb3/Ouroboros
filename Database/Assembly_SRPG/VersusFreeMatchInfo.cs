namespace SRPG
{
    using GR;
    using System;
    using UnityEngine;
    using UnityEngine.UI;

    public class VersusFreeMatchInfo : MonoBehaviour
    {
        private readonly float UPDATE_WAIT_TIME;
        [Description("開催中のときに表示するオブジェクトルート")]
        public GameObject OpenRoot;
        [Description("開催していないときに表示するオブジェクトルート")]
        public GameObject CloseRoot;
        [Description("連勝数オブジェクト")]
        public GameObject StreakWin;
        [Description("獲得コイン回数倍率オブジェクト")]
        public GameObject CoinRate;
        [Description("開催残り時間")]
        public Text RemainTime;
        [Description("次回開催までのテキスト")]
        public Text ScheduleTxt;
        [Description("連勝数")]
        public Text StreakWinTxt;
        [Description("コイン倍率")]
        public Text CoinRateTxt;
        [Description("フリー対戦のボタン")]
        public Button FreeBtn;
        private float mWaitTime;
        private long mEndTime;

        public VersusFreeMatchInfo()
        {
            this.UPDATE_WAIT_TIME = 1f;
            this.mWaitTime = 1f;
            base..ctor();
            return;
        }

        private unsafe void Close()
        {
            object[] objArray1;
            GameManager manager;
            DateTime time;
            if ((this.OpenRoot != null) == null)
            {
                goto Label_003A;
            }
            if ((this.CloseRoot != null) == null)
            {
                goto Label_003A;
            }
            this.OpenRoot.SetActive(0);
            this.CloseRoot.SetActive(1);
        Label_003A:
            manager = MonoSingleton<GameManager>.Instance;
            if ((this.ScheduleTxt != null) == null)
            {
                goto Label_00ED;
            }
            if (manager.VSFreeNextTime != null)
            {
                goto Label_0076;
            }
            this.ScheduleTxt.set_text(LocalizedText.Get("sys.MULTI_VERSUS_NEXT_NONE"));
            goto Label_00ED;
        Label_0076:
            time = TimeManager.FromUnixTime(manager.VSFreeNextTime);
            objArray1 = new object[] { (int) &time.Year, (int) &time.Month, (int) &time.Day, (int) &time.Hour, (int) &time.Minute };
            this.ScheduleTxt.set_text(string.Format(LocalizedText.Get("sys.MULTI_VERSUS_NEXT_AT"), objArray1));
        Label_00ED:
            if ((this.FreeBtn != null) == null)
            {
                goto Label_010A;
            }
            this.FreeBtn.set_interactable(0);
        Label_010A:
            this.mEndTime = 0L;
            return;
        }

        private unsafe void CountDown()
        {
            object[] objArray3;
            object[] objArray2;
            object[] objArray1;
            DateTime time;
            DateTime time2;
            TimeSpan span;
            if (this.mEndTime > 0L)
            {
                goto Label_000E;
            }
            return;
        Label_000E:
            time = TimeManager.FromUnixTime(this.mEndTime);
            time2 = TimeManager.ServerTime;
            if ((time2 > time) == null)
            {
                goto Label_0033;
            }
            this.Close();
            return;
        Label_0033:
            span = time - time2;
            if ((this.RemainTime != null) == null)
            {
                goto Label_0104;
            }
            if (&span.TotalDays < 1.0)
            {
                goto Label_0090;
            }
            objArray1 = new object[] { (int) &span.Days };
            this.RemainTime.set_text(LocalizedText.Get("sys.MULTI_VERSUS_REMAIN_AT_DAY", objArray1));
            goto Label_0104;
        Label_0090:
            if (&span.TotalHours < 1.0)
            {
                goto Label_00D4;
            }
            objArray2 = new object[] { (int) &span.Hours };
            this.RemainTime.set_text(LocalizedText.Get("sys.MULTI_VERSUS_REMAIN_AT_HOUR", objArray2));
            goto Label_0104;
        Label_00D4:
            objArray3 = new object[] { (int) Mathf.Max(&span.Minutes, 0) };
            this.RemainTime.set_text(LocalizedText.Get("sys.MULTI_VERSUS_REMAIN_AT_MINUTE", objArray3));
        Label_0104:
            this.mWaitTime = this.UPDATE_WAIT_TIME;
            return;
        }

        private unsafe void RefreshData()
        {
            GameManager manager;
            long num;
            long num2;
            int num3;
            int num4;
            manager = MonoSingleton<GameManager>.Instance;
            num = manager.VSFreeExpiredTime;
            num2 = TimeManager.FromDateTime(TimeManager.ServerTime);
            num3 = manager.VS_StreakWinCnt_Now;
            num4 = manager.GetVSGetCoinRate(-1L);
            if ((this.OpenRoot != null) == null)
            {
                goto Label_0082;
            }
            if ((this.CloseRoot != null) == null)
            {
                goto Label_0082;
            }
            if (num2 >= num)
            {
                goto Label_007C;
            }
            this.mEndTime = num;
            this.OpenRoot.SetActive(1);
            this.CloseRoot.SetActive(0);
            this.CountDown();
            goto Label_0082;
        Label_007C:
            this.Close();
        Label_0082:
            if ((this.StreakWin != null) == null)
            {
                goto Label_00C5;
            }
            if ((this.StreakWinTxt != null) == null)
            {
                goto Label_00C5;
            }
            this.StreakWin.SetActive(num3 > 1);
            this.StreakWinTxt.set_text(&num3.ToString());
        Label_00C5:
            if ((this.CoinRate != null) == null)
            {
                goto Label_0109;
            }
            if ((this.CoinRateTxt != null) == null)
            {
                goto Label_0109;
            }
            this.CoinRate.SetActive(num4 > 1);
            this.CoinRateTxt.set_text(&num4.ToString());
        Label_0109:
            return;
        }

        private void Start()
        {
            this.RefreshData();
            return;
        }

        private void Update()
        {
            this.mWaitTime -= Time.get_deltaTime();
            if (this.mWaitTime >= 0f)
            {
                goto Label_0028;
            }
            this.CountDown();
        Label_0028:
            return;
        }
    }
}

