namespace SRPG
{
    using System;

    public class Json_ArenaPlayerHistory
    {
        public string type;
        public string result;
        public JSON_ArenaRankInfo ranking;
        public long at;
        public Json_ArenaPlayer my;
        public Json_ArenaPlayer enemy;

        public Json_ArenaPlayerHistory()
        {
            base..ctor();
            return;
        }
    }
}

