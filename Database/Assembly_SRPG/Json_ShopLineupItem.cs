namespace SRPG
{
    using System;

    [Serializable]
    public class Json_ShopLineupItem
    {
        public string gtid;
        public Json_ShopLineupItemDetail[] items;

        public Json_ShopLineupItem()
        {
            base..ctor();
            return;
        }
    }
}

