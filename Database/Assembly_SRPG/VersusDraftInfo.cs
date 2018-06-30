namespace SRPG
{
    using GR;
    using System;
    using UnityEngine;
    using UnityEngine.UI;

    public class VersusDraftInfo : MonoBehaviour
    {
        [SerializeField]
        private Text mDateText;

        public VersusDraftInfo()
        {
            base..ctor();
            return;
        }

        private unsafe void Start()
        {
            object[] objArray1;
            GameManager manager;
            VersusEnableTimeParam param;
            DateTime time;
            DateTime time2;
            DateTime time3;
            VersusEnableTimeScheduleParam param2;
            DateTime time4;
            TimeSpan span;
            string str;
            DateTime time5;
            if ((this.mDateText == null) == null)
            {
                goto Label_0012;
            }
            return;
        Label_0012:
            manager = MonoSingleton<GameManager>.Instance;
            if (manager.VSDraftType == 1)
            {
                goto Label_0025;
            }
            return;
        Label_0025:
            param = manager.GetVersusEnableTime(manager.mVersusEnableId);
            if (param != null)
            {
                goto Label_0039;
            }
            return;
        Label_0039:
            time = param.BeginAt;
            time2 = param.EndAt;
            time3 = DateTime.Today;
            if (param.Schedule == null)
            {
                goto Label_00C3;
            }
            if (param.Schedule.Count <= 0)
            {
                goto Label_00C3;
            }
            param2 = param.Schedule[param.Schedule.Count - 1];
            time4 = DateTime.Parse(&TimeManager.ServerTime.ToShortDateString() + " " + param2.Begin);
            span = TimeSpan.Parse(param2.Open);
            time3 = time4 + span;
        Label_00C3:
            str = LocalizedText.Get("sys.DRAFT_PERIOD");
            objArray1 = new object[] { (int) &time.Month, (int) &time.Day, (int) &time2.Month, (int) &time2.Day, (int) &time3.Hour, (int) &time3.Minute };
            this.mDateText.set_text(string.Format(str, objArray1));
            return;
        }
    }
}

