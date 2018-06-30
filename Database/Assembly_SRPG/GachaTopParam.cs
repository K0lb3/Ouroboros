namespace SRPG
{
    using System;
    using System.Collections.Generic;

    public class GachaTopParam
    {
        public string[] iname;
        public string[] category;
        public int[] coin;
        public int[] gold;
        public int[] coin_p;
        public List<UnitParam> units;
        public int[] num;
        public string[] ticket;
        public int[] ticket_num;
        public bool[] step;
        public int[] step_num;
        public int[] step_index;
        public bool[] limit;
        public int[] limit_num;
        public int[] limit_stock;
        public string type;
        public string asset_title;
        public string asset_bg;
        public string group;
        public string[] btext;
        public string[] confirm;
        public List<int> sort;

        public GachaTopParam()
        {
            this.iname = new string[4];
            this.category = new string[4];
            this.coin = new int[4];
            this.gold = new int[4];
            this.coin_p = new int[4];
            this.num = new int[4];
            this.ticket = new string[4];
            this.ticket_num = new int[4];
            this.step = new bool[4];
            this.step_num = new int[4];
            this.step_index = new int[4];
            this.limit = new bool[4];
            this.limit_num = new int[4];
            this.limit_stock = new int[4];
            this.btext = new string[4];
            this.confirm = new string[4];
            this.sort = new List<int>();
            base..ctor();
            return;
        }
    }
}

