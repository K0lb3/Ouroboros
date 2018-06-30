namespace SRPG
{
    using System;

    [Serializable]
    public class Json_ShopLineupItemDetail
    {
        public string iname;
        public string itype;

        public Json_ShopLineupItemDetail()
        {
            base..ctor();
            return;
        }

        public EShopItemType GetShopItemTypeWithIType()
        {
            return ShopData.String2ShopItemType(this.itype);
        }
    }
}

