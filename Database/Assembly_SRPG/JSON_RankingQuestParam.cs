namespace SRPG
{
    using System;

    [Serializable]
    public class JSON_RankingQuestParam
    {
        public int schedule_id;
        public int type;
        public string iname;
        public int reward_id;

        public JSON_RankingQuestParam()
        {
            base..ctor();
            return;
        }
    }
}

