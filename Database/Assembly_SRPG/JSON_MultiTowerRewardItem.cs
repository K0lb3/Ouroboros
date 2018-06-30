namespace SRPG
{
    using System;

    [Serializable]
    public class JSON_MultiTowerRewardItem
    {
        public int round_st;
        public int round_ed;
        public int type;
        public string itemname;
        public int num;

        public JSON_MultiTowerRewardItem()
        {
            base..ctor();
            return;
        }
    }
}

