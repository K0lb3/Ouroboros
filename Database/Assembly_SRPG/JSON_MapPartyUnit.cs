namespace SRPG
{
    using System;

    public class JSON_MapPartyUnit
    {
        public string name;
        public string ai;
        public int x;
        public int y;
        public int dir;
        public int wait_e;
        public int wait_m;
        public int wait_exit;
        public int ct_calc;
        public int ct_val;
        public int fvoff;
        public int ai_type;
        public int ai_x;
        public int ai_y;
        public int ai_len;
        public string parent;
        public JSON_EventTrigger trg;
        public UnitEntryTrigger[] entries;
        public int entries_and;

        public JSON_MapPartyUnit()
        {
            base..ctor();
            return;
        }
    }
}

