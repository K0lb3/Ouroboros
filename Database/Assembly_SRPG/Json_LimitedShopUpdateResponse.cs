namespace SRPG
{
    using System;

    public class Json_LimitedShopUpdateResponse
    {
        public Json_Currencies currencies;
        public JSON_LimitedShopItemListSet[] shopitems;
        public int relcnt;
        public int concept_count;

        public Json_LimitedShopUpdateResponse()
        {
            base..ctor();
            return;
        }
    }
}

