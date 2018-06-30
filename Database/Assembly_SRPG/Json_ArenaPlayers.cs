namespace SRPG
{
    using System;

    public class Json_ArenaPlayers
    {
        public Json_ArenaPlayer[] coloenemies;
        public int rank_myself;
        public int best_myself;
        public long btl_at;
        public string quest_iname;
        public int seed;
        public int maxActionNum;
        public long end_at;

        public Json_ArenaPlayers()
        {
            base..ctor();
            return;
        }
    }
}

