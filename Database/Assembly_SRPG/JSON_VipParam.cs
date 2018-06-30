namespace SRPG
{
    using System;

    [Serializable]
    public class JSON_VipParam
    {
        public int exp;
        public int ticket;
        public int buy_coin_bonus;
        public int buy_coin_num;
        public int buy_stmn_num;
        public int reset_elite;
        public int reset_arena;

        public JSON_VipParam()
        {
            base..ctor();
            return;
        }
    }
}

