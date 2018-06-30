namespace SRPG
{
    using System;

    [Serializable]
    public class JSON_ChallengeCategoryParam
    {
        public string iname;
        public string begin_at;
        public string end_at;
        public int prio;

        public JSON_ChallengeCategoryParam()
        {
            base..ctor();
            return;
        }
    }
}

