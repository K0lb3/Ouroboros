namespace SRPG
{
    using System;

    public class JSON_EventShopItemListSet
    {
        public int id;
        public int sold;
        public Json_ShopItemDesc item;
        public Cost cost;
        public Json_ShopItemDesc[] children;
        public int isreset;
        public long start;
        public long end;

        public JSON_EventShopItemListSet()
        {
            base..ctor();
            return;
        }

        public class Cost : Json_ShopItemCost
        {
            public string iname;

            public Cost()
            {
                base..ctor();
                return;
            }
        }
    }
}

