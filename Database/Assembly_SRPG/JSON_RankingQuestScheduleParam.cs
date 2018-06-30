namespace SRPG
{
    using System;

    [Serializable]
    public class JSON_RankingQuestScheduleParam
    {
        public int id;
        public string begin_at;
        public string end_at;
        public string reward_begin_at;
        public string reward_end_at;
        public string visible_begin_at;
        public string visible_end_at;

        public JSON_RankingQuestScheduleParam()
        {
            base..ctor();
            return;
        }
    }
}

