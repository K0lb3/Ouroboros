namespace SRPG
{
    using System;

    public class Json_TrophyPlayerData
    {
        public int exp;
        public int gold;
        public Json_Coin coin;
        public Json_Stamina stamina;

        public Json_TrophyPlayerData()
        {
            base..ctor();
            return;
        }
    }
}

