namespace SRPG
{
    using System;
    using System.Text;

    public class ReqLimitedShopItemList : WebAPI
    {
        public ReqLimitedShopItemList(string shop_name, Network.ResponseCallback response)
        {
            StringBuilder builder;
            base..ctor();
            base.name = "shop/limited";
            builder = WebAPI.GetStringBuilder();
            builder.Append("\"shopName\":\"");
            builder.Append(shop_name);
            builder.Append("\"");
            base.body = WebAPI.GetRequestString(builder.ToString());
            base.callback = response;
            return;
        }
    }
}

