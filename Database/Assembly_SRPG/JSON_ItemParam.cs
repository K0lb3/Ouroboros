namespace SRPG
{
    using System;

    [Serializable]
    public class JSON_ItemParam
    {
        public string iname;
        public string name;
        public string expr;
        public string flavor;
        public int type;
        public int tabtype;
        public int rare;
        public int cap;
        public int invcap;
        public int eqlv;
        public int coin;
        public int tc;
        public int ac;
        public int ec;
        public int mc;
        public int pp;
        public int buy;
        public int sell;
        public int encost;
        public int enpt;
        public int val;
        public string icon;
        public string skill;
        public string recipe;
        public string[] quests;
        public int is_valuables;
        public int is_cmn;
        public byte cmn_type;

        public JSON_ItemParam()
        {
            base..ctor();
            return;
        }
    }
}

