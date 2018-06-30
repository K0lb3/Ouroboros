namespace SRPG
{
    using System;

    public class Json_ArenaRewardInfo
    {
        public int gold;
        public int coin;
        public int arenacoin;
        public Json_Item[] items;

        public Json_ArenaRewardInfo()
        {
            base..ctor();
            return;
        }

        public bool IsReward()
        {
            return ((((this.gold > 0) || (this.coin > 0)) || (this.arenacoin > 0)) ? 1 : ((this.items == null) ? 0 : (((int) this.items.Length) > 0)));
        }
    }
}

