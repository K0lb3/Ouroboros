namespace SRPG
{
    using System;

    public class Json_ShopResponse
    {
        public Json_ShopItem[] shopitems;
        public int relcnt;
        public string msg;
        public int concept_count;

        public Json_ShopResponse()
        {
            base..ctor();
            return;
        }
    }
}

