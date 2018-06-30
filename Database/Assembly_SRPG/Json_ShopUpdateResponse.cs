namespace SRPG
{
    using System;

    public class Json_ShopUpdateResponse
    {
        public Json_Currencies currencies;
        public Json_ShopItem[] shopitems;
        public int relcnt;
        public int concept_count;

        public Json_ShopUpdateResponse()
        {
            base..ctor();
            return;
        }
    }
}

