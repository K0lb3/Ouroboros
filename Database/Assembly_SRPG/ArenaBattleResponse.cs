namespace SRPG
{
    using System;

    public class ArenaBattleResponse
    {
        public Json_ArenaRewardInfo reward_info;
        public int new_rank;
        public int def_rank;
        public int got_pexp;
        public int got_uexp;
        public int got_gold;

        public ArenaBattleResponse()
        {
            base..ctor();
            return;
        }

        public void Deserialize(Json_ArenaBattleResponse json)
        {
            if (json != null)
            {
                goto Label_000C;
            }
            throw new InvalidJSONException();
        Label_000C:
            this.new_rank = json.new_rank;
            this.def_rank = json.def_rank;
            this.got_pexp = json.got_pexp;
            this.got_uexp = json.got_uexp;
            this.got_gold = json.got_gold;
            this.reward_info = new Json_ArenaRewardInfo();
            if (json.reward_info == null)
            {
                goto Label_00A0;
            }
            this.reward_info.gold = json.reward_info.gold;
            this.reward_info.coin = json.reward_info.coin;
            this.reward_info.arenacoin = json.reward_info.arenacoin;
        Label_00A0:
            return;
        }
    }
}

