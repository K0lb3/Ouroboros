namespace SRPG
{
    using System;
    using UnityEngine;
    using UnityEngine.UI;

    public class ChallengeMissionCategoryButton : MonoBehaviour
    {
        public UnityEngine.UI.Button Button;
        public Image Badge;
        public Image SelectionFrame;
        public GameObject TimerBase;
        public Text Timer;
        private float mRefreshInterval;
        private DateTime mEndTime;
        private ChallengeCategoryParam mCategoryParam;

        public ChallengeMissionCategoryButton()
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
            time = TimeManager.ServerTime;
            time2 = this.mEndTime;
            span = time2 - time;
            str = null;
            if ((time2 < DateTime.MaxValue) == null)
            {
                goto Label_00C1;
            }
            if (&span.TotalDays < 1.0)
            {
                goto Label_0061;
            }
            objArray1 = new object[] { (int) &span.Days };
            str = LocalizedText.Get("sys.QUEST_TIMELIMIT_D", objArray1);
            goto Label_00C1;
        Label_0061:
            if (&span.TotalHours < 1.0)
            {
                goto Label_009B;
            }
            objArray2 = new object[] { (int) &span.Hours };
            str = LocalizedText.Get("sys.QUEST_TIMELIMIT_H", objArray2);
            goto Label_00C1;
        Label_009B:
            objArray3 = new object[] { (int) Mathf.Max(&span.Minutes, 0) };
            str = LocalizedText.Get("sys.QUEST_TIMELIMIT_M", objArray3);
        Label_00C1:
            if ((this.Timer != null) == null)
            {
                goto Label_00F4;
            }
            if ((this.Timer.get_text() != str) == null)
            {
                goto Label_00F4;
            }
            this.Timer.set_text(str);
        Label_00F4:
            return;
        }

        public void SetChallengeCategory(ChallengeCategoryParam category)
        {
            if ((category.end_at.DateTimes < DateTime.MaxValue) == null)
            {
                goto Label_004E;
            }
            this.TimerBase.SetActive(1);
            this.mCategoryParam = category;
            this.mEndTime = this.mCategoryParam.end_at.DateTimes;
            this.Refresh();
            goto Label_005A;
        Label_004E:
            this.TimerBase.SetActive(0);
        Label_005A:
            return;
        }

        private void Update()
        {
            if (this.mCategoryParam != null)
            {
                goto Label_000C;
            }
            return;
        Label_000C:
            this.mRefreshInterval -= Time.get_unscaledDeltaTime();
            if (this.mRefreshInterval > 0f)
            {
                goto Label_003F;
            }
            this.Refresh();
            this.mRefreshInterval = 1f;
        Label_003F:
            return;
        }
    }
}

