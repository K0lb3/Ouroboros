namespace SRPG
{
    using System;
    using System.Collections.Generic;

    public class GachaTopParam2
    {
        public string iname;
        public string category;
        public int coin;
        public int coin_p;
        public int gold;
        public int gold_p;
        public List<UnitParam> units;
        public int num;
        public string ticket;
        public int ticket_num;
        public bool step;
        public int step_num;
        public int step_index;
        public bool limit;
        public int limit_num;
        public int limit_stock;
        public string type;
        public string asset_title;
        public string asset_bg;
        public string group;
        public string btext;
        public string confirm;

        public GachaTopParam2()
        {
            base..ctor();
            return;
        }
    }
}

