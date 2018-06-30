namespace SRPG
{
    using GR;
    using System;
    using UnityEngine;

    public class RankMatchMapInfo : MonoBehaviour
    {
        [SerializeField]
        private GameParameter NameParam;
        [SerializeField]
        private GameParameter DescriptionParam;
        [SerializeField]
        private GameParameter ThumbnailParam;
        [SerializeField]
        private LText TimeText;

        public RankMatchMapInfo()
        {
            base..ctor();
            return;
        }

        private void OnEnable()
        {
            this.UpdateValue();
            return;
        }

        public unsafe void UpdateValue()
        {
            object[] objArray1;
            VersusEnableTimeScheduleParam param;
            QuestParam param2;
            DateTime time;
            TimeSpan span;
            DateTime time2;
            DateTime time3;
            param = DataSource.FindDataOfClass<VersusEnableTimeScheduleParam>(base.get_gameObject(), null);
            if (param != null)
            {
                goto Label_0014;
            }
            return;
        Label_0014:
            param2 = MonoSingleton<GameManager>.Instance.FindQuest(param.QuestIname);
            DataSource.Bind<QuestParam>(base.get_gameObject(), param2);
            this.NameParam.UpdateValue();
            this.DescriptionParam.UpdateValue();
            this.ThumbnailParam.UpdateValue();
            time = DateTime.Parse(&TimeManager.ServerTime.ToShortDateString() + " " + param.Begin + ":00");
            span = TimeSpan.Parse(param.Open);
            time2 = time + span;
            objArray1 = new object[] { (int) &time.Hour, (int) &time.Minute, (int) &time2.Hour, (int) &time2.Minute };
            this.TimeText.set_text(string.Format(LocalizedText.Get("sys.RANK_MATCH_ENABLE_TIME"), objArray1));
            return;
        }
    }
}

