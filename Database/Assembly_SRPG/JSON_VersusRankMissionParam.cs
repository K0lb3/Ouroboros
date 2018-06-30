namespace SRPG
{
    using System;

    [Serializable]
    public class JSON_VersusRankMissionParam
    {
        public string iname;
        public string name;
        public string expr;
        public int type;
        public string sval;
        public int ival;
        public string reward_id;

        public JSON_VersusRankMissionParam()
        {
            base..ctor();
            return;
        }
    }
}

