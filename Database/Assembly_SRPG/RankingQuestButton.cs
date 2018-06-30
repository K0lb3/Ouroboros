namespace SRPG
{
    using System;
    using UnityEngine;
    using UnityEngine.UI;

    [Pin(100, "Success", 1, 100), Pin(0, "Set ranking quest", 0, 0)]
    public class RankingQuestButton : MonoBehaviour, IGameParameter, IFlowInterface
    {
        private const int PERIOD_STATE_ICON_INDEX_OPEN = 0;
        private const int PERIOD_STATE_ICON_INDEX_WAIT = 1;
        private const int PERIOD_STATE_ICON_INDEX_VISIBLE = 2;
        [SerializeField]
        private ImageArray m_PeriodStateIcon;
        [SerializeField]
        private Text m_Time;

        public RankingQuestButton()
        {
            base..ctor();
            return;
        }

        public void Activated(int pinID)
        {
            RankingQuestParam param;
            if (pinID != null)
            {
                goto Label_0032;
            }
            param = DataSource.FindDataOfClass<RankingQuestParam>(base.get_gameObject(), null);
            if (param == null)
            {
                goto Label_0032;
            }
            GlobalVars.SelectedQuestID = param.iname;
            GlobalVars.SelectedRankingQuestParam = param;
            FlowNode_GameObject.ActivateOutputLinks(this, 100);
        Label_0032:
            return;
        }

        private void Start()
        {
            this.UpdateValue();
            return;
        }

        public unsafe void UpdateValue()
        {
            object[] objArray3;
            object[] objArray2;
            object[] objArray1;
            RankingQuestParam param;
            DateTime time;
            TimeSpan span;
            param = DataSource.FindDataOfClass<RankingQuestParam>(base.get_gameObject(), null);
            if (param != null)
            {
                goto Label_0024;
            }
            base.get_gameObject().SetActive(0);
            goto Label_01B6;
        Label_0024:
            time = TimeManager.ServerTime;
            base.get_gameObject().SetActive(1);
            if (param.scheduleParam.IsAvailablePeriod(time) == null)
            {
                goto Label_0155;
            }
            if ((this.m_PeriodStateIcon != null) == null)
            {
                goto Label_0064;
            }
            this.m_PeriodStateIcon.ImageIndex = 0;
        Label_0064:
            span = param.scheduleParam.endAt - time;
            if ((this.m_Time != null) == null)
            {
                goto Label_01B6;
            }
            if (&span.TotalDays < 1.0)
            {
                goto Label_00CB;
            }
            objArray1 = new object[] { (int) &span.Days };
            this.m_Time.set_text(LocalizedText.Get("sys.RANKING_QUEST_QUEST_BANNER_DAY", objArray1));
            goto Label_013F;
        Label_00CB:
            if (&span.TotalHours < 1.0)
            {
                goto Label_010F;
            }
            objArray2 = new object[] { (int) &span.Hours };
            this.m_Time.set_text(LocalizedText.Get("sys.RANKING_QUEST_QUEST_BANNER_HOUR", objArray2));
            goto Label_013F;
        Label_010F:
            objArray3 = new object[] { (int) Mathf.Max(&span.Minutes, 0) };
            this.m_Time.set_text(LocalizedText.Get("sys.RANKING_QUEST_QUEST_BANNER_MINUTE", objArray3));
        Label_013F:
            this.m_Time.get_gameObject().SetActive(1);
            goto Label_01B6;
        Label_0155:
            if (param.scheduleParam.IsAvailableVisiblePeriod(time) == null)
            {
                goto Label_01AA;
            }
            if ((this.m_PeriodStateIcon != null) == null)
            {
                goto Label_0183;
            }
            this.m_PeriodStateIcon.ImageIndex = 2;
        Label_0183:
            if ((this.m_Time != null) == null)
            {
                goto Label_01B6;
            }
            this.m_Time.get_gameObject().SetActive(0);
            goto Label_01B6;
        Label_01AA:
            base.get_gameObject().SetActive(0);
        Label_01B6:
            return;
        }
    }
}

