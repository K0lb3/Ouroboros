namespace SRPG
{
    using GR;
    using System;
    using UnityEngine;
    using UnityEngine.UI;

    public class EventShopTimeLimit : MonoBehaviour, IGameParameter
    {
        public GameObject Body;
        public Text Timer;
        private long mEndTime;
        private float mRefreshInterval;

        public EventShopTimeLimit()
        {
            this.mRefreshInterval = 1f;
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
            EventShopData data;
            this.mEndTime = 0L;
            if (MonoSingleton<GameManager>.Instance.Player.GetEventShopData() == null)
            {
                goto Label_003A;
            }
            this.mEndTime = GlobalVars.EventShopItem.shops.end;
            this.Refresh();
            return;
        Label_003A:
            return;
        }
    }
}

