namespace SRPG
{
    using System;
    using System.Text;

    public class ReqItemGuerrillaShop : WebAPI
    {
        public ReqItemGuerrillaShop(string shop_name, Network.ResponseCallback response)
        {
            StringBuilder builder;
            base..ctor();
            base.name = "shop/guerrilla";
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

