namespace SRPG
{
    using System;

    [Serializable]
    public class JSON_UnitJobOverwriteParam
    {
        public string unit_iname;
        public string job_iname;
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
        public int avoid;
        public int inimp;

        public JSON_UnitJobOverwriteParam()
        {
            base..ctor();
            return;
        }
    }
}

