namespace SRPG
{
    using System;

    public class Json_ArenaBattleResponse
    {
        public Json_ArenaRewardInfo reward_info;
        public int new_rank;
        public int def_rank;
        public int got_pexp;
        public int got_uexp;
        public int got_gold;

        public Json_ArenaBattleResponse()
        {
            base..ctor();
            return;
        }
    }
}

