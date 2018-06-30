namespace SRPG
{
    using System;

    [Serializable]
    public class JSON_MultiTowerFloorParam
    {
        public int id;
        public string title;
        public string name;
        public string expr;
        public string cond;
        public string tower_id;
        public int cond_floor;
        public string reward_id;
        public JSON_MapParam[] map;
        public short pt;
        public short lv;
        public short joblv;
        public short floor;
        public short unitnum;
        public short notcon;
        public string me_id;
        public int is_wth_no_chg;
        public string wth_set_id;

        public JSON_MultiTowerFloorParam()
        {
            base..ctor();
            return;
        }
    }
}

