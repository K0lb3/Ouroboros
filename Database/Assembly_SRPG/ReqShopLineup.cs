namespace SRPG
{
    using System;
    using System.Text;

    public class ReqShopLineup : WebAPI
    {
        public ReqShopLineup(string shop_name, Network.ResponseCallback response)
        {
            StringBuilder builder;
            base..ctor();
            builder = WebAPI.GetStringBuilder();
            base.name = "shop/itemlist";
            builder.Append("\"shopName\":\"");
            builder.Append(shop_name);
            builder.Append("\"");
            base.body = WebAPI.GetRequestString(builder.ToString());
            base.callback = response;
            return;
        }
    }
}

