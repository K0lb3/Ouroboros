namespace SRPG
{
    using System;

    public class EventShopInfo
    {
        public JSON_ShopListArray.Shops shops;
        public string shop_cost_iname;
        public bool btn_update;
        public string banner_sprite;

        public EventShopInfo()
        {
            base..ctor();
            return;
        }
    }
}

