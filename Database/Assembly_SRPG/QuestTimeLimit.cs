namespace SRPG
{
    using System;
    using UnityEngine;
    using UnityEngine.UI;

    public class QuestTimeLimit : MonoBehaviour, IGameParameter
    {
        public GameObject Body;
        public Text Timer;
        public bool IsTTMMSS;
        private long mEndTime;
        private float mRefreshInterval;

        public QuestTimeLimit()
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
            int num;
            int num2;
            int num3;
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
            if (this.IsTTMMSS == null)
            {
                goto Label_0130;
            }
            num = Math.Max(Math.Min((&span.Days * 0x18) + &span.Hours, 0x63), 0);
            num2 = Math.Max(Math.Min(&span.Minutes, 0x3b), 0);
            num3 = Math.Max(Math.Min(&span.Seconds, 0x3b), 0);
            str = ((((str + string.Format("{0:D2}", (int) num).ToString()) + ":") + string.Format("{0:D2}", (int) num2).ToString()) + ":") + string.Format("{0:D2}", (int) num3).ToString();
            goto Label_01CA;
        Label_0130:
            if (&span.TotalDays < 1.0)
            {
                goto Label_016A;
            }
            objArray1 = new object[] { (int) &span.Days };
            str = LocalizedText.Get("sys.QUEST_TIMELIMIT_D", objArray1);
            goto Label_01CA;
        Label_016A:
            if (&span.TotalHours < 1.0)
            {
                goto Label_01A4;
            }
            objArray2 = new object[] { (int) &span.Hours };
            str = LocalizedText.Get("sys.QUEST_TIMELIMIT_H", objArray2);
            goto Label_01CA;
        Label_01A4:
            objArray3 = new object[] { (int) Mathf.Max(&span.Minutes, 0) };
            str = LocalizedText.Get("sys.QUEST_TIMELIMIT_M", objArray3);
        Label_01CA:
            if ((this.Timer != null) == null)
            {
                goto Label_01FD;
            }
            if ((this.Timer.get_text() != str) == null)
            {
                goto Label_01FD;
            }
            this.Timer.set_text(str);
        Label_01FD:
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
            QuestParam param;
            KeyQuestTypes types;
            ChapterParam param2;
            KeyQuestTypes types2;
            KeyQuestTypes types3;
            this.mEndTime = 0L;
            param = DataSource.FindDataOfClass<QuestParam>(base.get_gameObject(), null);
            if (param == null)
            {
                goto Label_008A;
            }
            if (param.Chapter == null)
            {
                goto Label_008A;
            }
            types3 = param.Chapter.GetKeyQuestType();
            if (types3 == 1)
            {
                goto Label_004A;
            }
            if (types3 == 2)
            {
                goto Label_0060;
            }
            goto Label_006D;
        Label_004A:
            this.mEndTime = param.Chapter.key_end;
            goto Label_0083;
        Label_0060:
            this.mEndTime = 0L;
            goto Label_0083;
        Label_006D:
            this.mEndTime = param.Chapter.end;
        Label_0083:
            this.Refresh();
            return;
        Label_008A:
            param2 = DataSource.FindDataOfClass<ChapterParam>(base.get_gameObject(), null);
            if (param2 == null)
            {
                goto Label_00F2;
            }
            types3 = param2.GetKeyQuestType();
            if (types3 == 1)
            {
                goto Label_00BC;
            }
            if (types3 == 2)
            {
                goto Label_00CD;
            }
            goto Label_00DA;
        Label_00BC:
            this.mEndTime = param2.key_end;
            goto Label_00EB;
        Label_00CD:
            this.mEndTime = 0L;
            goto Label_00EB;
        Label_00DA:
            this.mEndTime = param2.end;
        Label_00EB:
            this.Refresh();
            return;
        Label_00F2:
            if (param == null)
            {
                goto Label_0117;
            }
            if (param.type != 7)
            {
                goto Label_0117;
            }
            this.mEndTime = param.end;
            this.Refresh();
            return;
        Label_0117:
            return;
        }
    }
}

