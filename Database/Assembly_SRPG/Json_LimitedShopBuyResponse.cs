namespace SRPG
{
    using System;

    public class Json_LimitedShopBuyResponse
    {
        public Json_Currencies currencies;
        public JSON_LimitedShopItemListSet[] shopitems;
        public Json_Item[] items;
        public Json_MailInfo mail_info;
        public Json_ShopBuyConceptCard[] cards;
        public Json_Unit[] units;
        public int concept_count;

        public Json_LimitedShopBuyResponse()
        {
            base..ctor();
            return;
        }
    }
}

