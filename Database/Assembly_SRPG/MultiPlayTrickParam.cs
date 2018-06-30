namespace SRPG
{
    using System;

    [Serializable]
    public class MultiPlayTrickParam
    {
        public string tid;
        public bool val;
        public int cun;
        public int rnk;
        public int rcp;
        public int grx;
        public int gry;
        public int rac;
        public int ccl;
        public string tag;

        public MultiPlayTrickParam()
        {
            base..ctor();
            return;
        }
    }
}

