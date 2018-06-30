namespace SRPG
{
    using System;

    public class GiftRecieveItemData
    {
        public string iname;
        public int rarity;
        public int num;
        public string name;
        public GiftTypes type;

        public GiftRecieveItemData()
        {
            base..ctor();
            return;
        }

        public void Set(string iname, GiftTypes giftTipe, int rarity, int num)
        {
            this.iname = iname;
            this.type = giftTipe;
            this.rarity = rarity;
            this.num = num;
            return;
        }
    }
}

