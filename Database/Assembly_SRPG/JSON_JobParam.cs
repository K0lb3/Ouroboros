namespace SRPG
{
    using System;

    [Serializable]
    public class JSON_JobParam
    {
        public string iname;
        public string name;
        public string expr;
        public string mdl;
        public string ac2d;
        public string mdlp;
        public string pet;
        public string buki;
        public string origin;
        public int type;
        public int role;
        public int jmov;
        public int jjmp;
        public string wepmdl;
        public string atkskl;
        public string atkfi;
        public string atkwa;
        public string atkwi;
        public string atkth;
        public string atksh;
        public string atkda;
        public string fixabl;
        public string artifact;
        public string ai;
        public string master;
        public string me_abl;
        public int is_me_rr;
        public string desc_ch;
        public string desc_ot;
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
        public JSON_JobRankParam[] ranks;
        public string unit_image;

        public JSON_JobParam()
        {
            base..ctor();
            return;
        }
    }
}

