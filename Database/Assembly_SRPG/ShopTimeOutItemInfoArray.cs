namespace SRPG
{
    using System;

    [Serializable]
    public class ShopTimeOutItemInfoArray
    {
        public ShopTimeOutItemInfo[] Infos;

        public ShopTimeOutItemInfoArray()
        {
            base..ctor();
            return;
        }

        public ShopTimeOutItemInfoArray(ShopTimeOutItemInfo[] infos)
        {
            base..ctor();
            this.Infos = infos;
            return;
        }
    }
}

