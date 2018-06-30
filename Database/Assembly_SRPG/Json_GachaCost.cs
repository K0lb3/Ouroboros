namespace SRPG
{
    using System;

    public class Json_GachaCost
    {
        public int gold;
        public int coin;
        public int coin_p;
        public Json_GachaCostTicket ticket;

        public Json_GachaCost()
        {
            this.gold = -1;
            this.coin = -1;
            this.coin_p = -1;
            base..ctor();
            return;
        }
    }
}

