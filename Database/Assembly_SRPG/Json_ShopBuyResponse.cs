namespace SRPG
{
    using System;

    public class Json_ShopBuyResponse
    {
        public Json_Currencies currencies;
        public Json_Item[] items;
        public Json_ShopItem[] shopitems;
        public Json_ShopBuyConceptCard[] cards;
        public Json_Unit[] units;
        public int concept_count;

        public Json_ShopBuyResponse()
        {
            base..ctor();
            return;
        }
    }
}

