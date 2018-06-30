namespace SRPG
{
    using System;

    public class Json_EventShopResponse
    {
        public JSON_EventShopItemListSet[] shopitems;
        public int relcnt;
        public int concept_count;

        public Json_EventShopResponse()
        {
            base..ctor();
            return;
        }
    }
}

