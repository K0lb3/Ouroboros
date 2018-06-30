namespace SRPG
{
    using System;
    using System.Text;

    public class ReqQRCodeAccess : WebAPI
    {
        public ReqQRCodeAccess(int campaign, string serial, Network.ResponseCallback response)
        {
            StringBuilder builder;
            base..ctor();
            base.name = "qr/serial";
            builder = WebAPI.GetStringBuilder();
            builder.Append("\"campaign_id\":");
            builder.Append(campaign);
            builder.Append(",");
            builder.Append("\"code\":\"");
            builder.Append(serial);
            builder.Append("\"");
            base.body = WebAPI.GetRequestString(builder.ToString());
            base.callback = response;
            return;
        }
    }
}

