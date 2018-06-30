namespace SRPG
{
    using System;

    [Serializable]
    public class ShopTimeOutItemInfo
    {
        public string ShopId;
        public int ItemId;
        public long End;

        public ShopTimeOutItemInfo(string shopId, int itemId, long end)
        {
            base..ctor();
            this.ShopId = shopId;
            this.ItemId = itemId;
            this.End = end;
            return;
        }
    }
}

