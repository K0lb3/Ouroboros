namespace SRPG
{
    using System;

    public class Json_ShopItem
    {
        public int id;
        public int sold;
        public Json_ShopItemDesc item;
        public Json_ShopItemCost cost;
        public Json_ShopItemDesc[] children;
        public int isreset;
        public long start;
        public long end;
        public int discount;

        public Json_ShopItem()
        {
            base..ctor();
            return;
        }
    }
}

