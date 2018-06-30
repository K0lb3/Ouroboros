namespace SRPG
{
    using System;

    [Serializable]
    public class JSON_RankingQuestRewardParam
    {
        public int id;
        public string type;
        public string iname;
        public int num;

        public JSON_RankingQuestRewardParam()
        {
            base..ctor();
            return;
        }
    }
}

