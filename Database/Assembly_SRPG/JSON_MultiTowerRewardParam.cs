namespace SRPG
{
    using System;

    [Serializable]
    public class JSON_MultiTowerRewardParam
    {
        public string iname;
        public JSON_MultiTowerRewardItem[] rewards;

        public JSON_MultiTowerRewardParam()
        {
            base..ctor();
            return;
        }
    }
}

