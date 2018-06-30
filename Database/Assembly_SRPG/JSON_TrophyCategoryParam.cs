namespace SRPG
{
    using System;

    [Serializable]
    public class JSON_TrophyCategoryParam
    {
        public string iname;
        public int hash_code;
        public int category;
        public int is_not_pull;
        public int day_reset;
        public int bgnr;
        public string begin_at;
        public string end_at;
        public string linked_quest;

        public JSON_TrophyCategoryParam()
        {
            base..ctor();
            return;
        }
    }
}

