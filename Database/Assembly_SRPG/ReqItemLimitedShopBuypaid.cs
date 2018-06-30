namespace SRPG
{
    using System;
    using System.Text;

    public class ReqItemLimitedShopBuypaid : WebAPI
    {
        public unsafe ReqItemLimitedShopBuypaid(string shopname, int id, int buynum, Network.ResponseCallback response)
        {
            StringBuilder builder;
            base..ctor();
            builder = WebAPI.GetStringBuilder();
            base.name = "shop/limited/buy";
            builder.Append("\"shopName\":\"" + shopname + "\",");
            builder.Append("\"id\":" + &id.ToString() + ",");
            builder.Append("\"buynum\":" + &buynum.ToString());
            base.body = WebAPI.GetRequestString(builder.ToString());
            base.callback = response;
            return;
        }
    }
}

