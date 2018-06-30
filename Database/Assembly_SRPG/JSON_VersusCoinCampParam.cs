namespace SRPG
{
    using System;

    [Serializable]
    public class JSON_VersusCoinCampParam
    {
        public string iname;
        public string begin_at;
        public string end_at;
        public int rate;

        public JSON_VersusCoinCampParam()
        {
            base..ctor();
            return;
        }
    }
}

