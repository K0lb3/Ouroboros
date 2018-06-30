namespace SRPG
{
    using System;

    [Serializable]
    public class JSON_QuestCondParam
    {
        public string iname;
        public int plvmax;
        public int plvmin;
        public int ulvmax;
        public int ulvmin;
        public int el_none;
        public int el_fire;
        public int el_watr;
        public int el_wind;
        public int el_thdr;
        public int el_lit;
        public int el_drk;
        public string[] job;
        public int party_type;
        public string[] unit;
        public int sex;
        public int rmax;
        public int rmin;
        public int hmax;
        public int hmin;
        public int wmax;
        public int wmin;
        public int jobset1;
        public int jobset2;
        public int jobset3;
        public string[] birth;

        public JSON_QuestCondParam()
        {
            base..ctor();
            return;
        }
    }
}

