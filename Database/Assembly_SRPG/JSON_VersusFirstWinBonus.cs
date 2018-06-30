namespace SRPG
{
    using System;

    [Serializable]
    public class JSON_VersusFirstWinBonus
    {
        public int id;
        public string begin_at;
        public string end_at;
        public JSON_VersusWinBonusReward[] rewards;

        public JSON_VersusFirstWinBonus()
        {
            base..ctor();
            return;
        }
    }
}

