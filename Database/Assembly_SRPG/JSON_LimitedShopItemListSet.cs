namespace SRPG
{
    using System;

    public class JSON_LimitedShopItemListSet
    {
        public int id;
        public int sold;
        public Json_ShopItemDesc item;
        public Json_ShopItemCost cost;
        public Json_ShopItemDesc[] children;
        public int isreset;
        public long start;
        public long end;

        public JSON_LimitedShopItemListSet()
        {
            base..ctor();
            return;
        }
    }
}

