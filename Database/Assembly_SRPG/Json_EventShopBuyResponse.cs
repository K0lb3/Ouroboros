namespace SRPG
{
    using System;

    public class Json_EventShopBuyResponse
    {
        public Json_Currencies currencies;
        public Json_Item[] items;
        public JSON_EventShopItemListSet[] shopitems;
        public Json_MailInfo mail_info;
        public Json_ShopBuyConceptCard[] cards;
        public Json_Unit[] units;
        public int concept_count;

        public Json_EventShopBuyResponse()
        {
            base..ctor();
            return;
        }
    }
}

