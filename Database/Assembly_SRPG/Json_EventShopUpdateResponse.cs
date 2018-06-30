namespace SRPG
{
    using System;

    public class Json_EventShopUpdateResponse
    {
        public Json_Currencies currencies;
        public Json_Item[] items;
        public JSON_EventShopItemListSet[] shopitems;
        public int relcnt;
        public int concept_count;

        public Json_EventShopUpdateResponse()
        {
            base..ctor();
            return;
        }
    }
}

