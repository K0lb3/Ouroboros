namespace SRPG
{
    using System;

    [Serializable]
    public class MultiPlayResumeAbilChg
    {
        public Data[] acd;

        public MultiPlayResumeAbilChg()
        {
            base..ctor();
            return;
        }

        [Serializable]
        public class Data
        {
            public string fid;
            public string tid;
            public int tur;
            public int irs;
            public int exp;
            public int iif;

            public Data()
            {
                base..ctor();
                return;
            }
        }
    }
}

