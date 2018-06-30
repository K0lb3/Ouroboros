namespace SRPG
{
    using System;

    [Serializable]
    public class JSON_VersusWinBonusReward
    {
        public int item_type;
        public string item_iname;
        public int item_num;

        public JSON_VersusWinBonusReward()
        {
            base..ctor();
            return;
        }
    }
}

