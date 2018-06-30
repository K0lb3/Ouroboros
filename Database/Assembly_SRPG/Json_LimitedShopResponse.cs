namespace SRPG
{
    using System;

    public class Json_LimitedShopResponse
    {
        public JSON_LimitedShopItemListSet[] shopitems;
        public int relcnt;
        public int concept_count;

        public Json_LimitedShopResponse()
        {
            base..ctor();
            return;
        }
    }
}

