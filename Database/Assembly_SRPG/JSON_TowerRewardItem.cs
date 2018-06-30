namespace SRPG
{
    using System;

    [Serializable]
    public class JSON_TowerRewardItem
    {
        public string iname;
        public int num;
        public byte type;
        public byte visible;

        public JSON_TowerRewardItem()
        {
            base..ctor();
            return;
        }
    }
}

