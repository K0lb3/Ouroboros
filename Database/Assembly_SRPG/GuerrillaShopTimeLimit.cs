namespace SRPG
{
    using GR;
    using System;
    using UnityEngine;
    using UnityEngine.UI;

    public class GuerrillaShopTimeLimit : MonoBehaviour, IGameParameter
    {
        public Text Hour;
        public Text Minute;
        public Text Second;
        private long mEndTime;
        private float mRefreshInterval;

        public GuerrillaShopTimeLimit()
        {
            this.mRefreshInterval = 1f;
            base..ctor();
            return;
        }

        private unsafe void Refresh()
        {
            DateTime time;
            DateTime time2;
            TimeSpan span;
            int num;
            int num2;
            int num3;
            if (this.mEndTime > 0L)
            {
                goto Label_0071;
            }
            if ((this.Hour != null) == null)
            {
                goto Label_002E;
            }
            this.Hour.set_text("00");
        Label_002E:
            if ((this.Minute != null) == null)
            {
                goto Label_004F;
            }
            this.Minute.set_text("00");
        Label_004F:
            if ((this.Second != null) == null)
            {
                goto Label_0070;
            }
            this.Second.set_text("00");
        Label_0070:
            return;
        Label_0071:
            time = TimeManager.ServerTime;
            span = TimeManager.FromUnixTime(this.mEndTime) - time;
            num = (int) &span.TotalHours;
            num2 = (int) &span.TotalMinutes;
            num3 = (int) &span.TotalSeconds;
            if ((this.Hour != null) == null)
            {
                goto Label_00D4;
            }
            this.Hour.set_text(string.Format("{0:D2}", (int) num));
        Label_00D4:
            if ((this.Minute != null) == null)
            {
                goto Label_012E;
            }
            if (num <= 0)
            {
                goto Label_0112;
            }
            this.Minute.set_text(string.Format("{0:D2}", (int) (num2 % (num * 60))));
            goto Label_012E;
        Label_0112:
            this.Minute.set_text(string.Format("{0:D2}", (int) num2));
        Label_012E:
            if ((this.Second != null) == null)
            {
                goto Label_018A;
            }
            if (num2 <= 0)
            {
                goto Label_016E;
            }
            this.Second.set_text(string.Format("{0:D2}", (int) (num3 % (num2 * 60))));
            goto Label_018A;
        Label_016E:
            this.Second.set_text(string.Format("{0:D2}", (int) num3));
        Label_018A:
            if ((span <= TimeSpan.Zero) == null)
            {
                goto Label_01A6;
            }
            GlobalEvent.Invoke("FINISH_GUERRILLA_SHOP_SHOW", null);
            return;
        Label_01A6:
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
            long num;
            this.mEndTime = 0L;
            num = MonoSingleton<GameManager>.Instance.Player.GuerrillaShopEnd;
            if (num <= 0L)
            {
                goto Label_002E;
            }
            this.mEndTime = num;
            this.Refresh();
            return;
        Label_002E:
            return;
        }
    }
}

