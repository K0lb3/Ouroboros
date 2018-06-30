namespace SRPG
{
    using System;

    [Serializable]
    public class JSON_VersusRule
    {
        public int id;
        public string begin_at;
        public string end_at;
        public int vsmode;
        public int getcoin;
        public int rate;

        public JSON_VersusRule()
        {
            base..ctor();
            return;
        }
    }
}

