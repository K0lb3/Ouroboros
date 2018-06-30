namespace SRPG
{
    using System;

    [Serializable]
    public class JSON_ArtifactParam
    {
        public string iname;
        public string name;
        public string expr;
        public string flavor;
        public string spec;
        public string asset;
        public string voice;
        public string icon;
        public int type;
        public int rini;
        public int rmax;
        public string kakera;
        public int notsmn;
        public int maxnum;
        public string[] skills;
        public string equip1;
        public string equip2;
        public string equip3;
        public string equip4;
        public string equip5;
        public string attack1;
        public string attack2;
        public string attack3;
        public string attack4;
        public string attack5;
        public string[] abils;
        public int[] abshows;
        public int[] ablvs;
        public int[] abrares;
        public string[] abconds;
        public int kc;
        public int tc;
        public int ac;
        public int mc;
        public int pp;
        public int buy;
        public int sell;
        public int ecost;
        public string tag;
        public string eqcond;
        public string[] units;
        public string[] jobs;
        public string birth;
        public int sex;
        public int elem;
        public int eqlv;
        public int eqrmin;
        public int eqrmax;

        public JSON_ArtifactParam()
        {
            base..ctor();
            return;
        }
    }
}

