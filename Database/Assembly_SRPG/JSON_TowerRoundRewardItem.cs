namespace SRPG
{
    using System;

    [Serializable]
    public class JSON_TowerRoundRewardItem : JSON_TowerRewardItem
    {
        public byte round_num;

        public JSON_TowerRoundRewardItem()
        {
            base..ctor();
            return;
        }
    }
}

