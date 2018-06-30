namespace SRPG
{
    using System;

    public class Json_ArenaPlayer
    {
        public string result;
        public string fuid;
        public string name;
        public int lv;
        public int rank;
        public Json_Unit[] units;
        public long btl_at;
        public string award;

        public Json_ArenaPlayer()
        {
            base..ctor();
            return;
        }
    }
}

