namespace SRPG
{
    using System;

    [Serializable]
    public class JSON_TowerRewardParam
    {
        public string iname;
        public JSON_TowerRewardItem[] rewards;

        public JSON_TowerRewardParam()
        {
            base..ctor();
            return;
        }
    }
}

