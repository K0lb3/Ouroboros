namespace SRPG
{
    using System;

    [Serializable]
    public class JSON_VersusCoin
    {
        public string iname;
        public string coin_iname;
        public int win_cnt;
        public int lose_cnt;
        public int draw_cnt;

        public JSON_VersusCoin()
        {
            base..ctor();
            return;
        }
    }
}

