namespace SRPG
{
    using System;

    [Serializable]
    public class JSON_TowerRoundRewardParam
    {
        public string iname;
        public JSON_TowerRoundRewardItem[] rewards;

        public JSON_TowerRoundRewardParam()
        {
            base..ctor();
            return;
        }
    }
}

