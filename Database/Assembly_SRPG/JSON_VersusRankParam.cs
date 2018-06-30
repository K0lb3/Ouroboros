namespace SRPG
{
    using System;

    [Serializable]
    public class JSON_VersusRankParam
    {
        public int id;
        public int btl_mode;
        public string name;
        public int limit;
        public string begin_at;
        public string end_at;
        public int win_pt_base;
        public int lose_pt_base;
        public string[] disabledate;
        public string hurl;

        public JSON_VersusRankParam()
        {
            base..ctor();
            return;
        }
    }
}

