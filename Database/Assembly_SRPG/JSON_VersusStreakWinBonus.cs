namespace SRPG
{
    using System;

    [Serializable]
    public class JSON_VersusStreakWinBonus
    {
        public int id;
        public int wincnt;
        public JSON_VersusWinBonusReward[] rewards;

        public JSON_VersusStreakWinBonus()
        {
            base..ctor();
            return;
        }
    }
}

