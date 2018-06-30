namespace SRPG
{
    using System;

    [Serializable]
    public class JSON_EvaluationParam
    {
        public string iname;
        public int val;
        public int hp;
        public int mp;
        public int atk;
        public int def;
        public int mag;
        public int mnd;
        public int dex;
        public int spd;
        public int cri;
        public int luk;

        public JSON_EvaluationParam()
        {
            base..ctor();
            return;
        }
    }
}

