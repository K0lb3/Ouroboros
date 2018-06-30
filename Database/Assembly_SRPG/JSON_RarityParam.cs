namespace SRPG
{
    using System;

    [Serializable]
    public class JSON_RarityParam
    {
        public int unitcap;
        public int jobcap;
        public int awakecap;
        public int piece;
        public int ch_piece;
        public int ch_piece_select;
        public int rareup_cost;
        public int gain_pp;
        public int eq_enhcap;
        public int eq_costscale;
        public int[] eq_points;
        public string eq_item1;
        public string eq_item2;
        public string eq_item3;
        public int[] eq_num1;
        public int[] eq_num2;
        public int[] eq_num3;
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
        public string drop;
        public int af_lvcap;
        public int af_unlock;
        public int af_gousei;
        public int af_change;
        public int af_unlock_cost;
        public int af_gousei_cost;
        public int af_change_cost;
        public int af_upcost;
        public int card_lvcap;
        public int card_awake_count;

        public JSON_RarityParam()
        {
            base..ctor();
            return;
        }
    }
}

