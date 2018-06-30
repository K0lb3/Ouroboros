namespace SRPG
{
    using System;
    using System.Text;

    public class ReqGAuthMigrateFgG : WebAPI
    {
        public ReqGAuthMigrateFgG(string secretKey, string deviceID, string mail, string password, Network.ResponseCallback response)
        {
            StringBuilder builder;
            base..ctor();
            base.name = "gauth/achievement/mgrate";
            builder = WebAPI.GetStringBuilder();
            builder.Append("\"secret_key\":\"");
            builder.Append(secretKey);
            builder.Append("\",\"device_id\":\"");
            builder.Append(deviceID);
            builder.Append("\",\"email\":\"");
            builder.Append(mail);
            builder.Append("\",\"password\":\"");
            builder.Append(password);
            builder.Append("\"");
            base.body = WebAPI.GetRequestString(builder.ToString());
            base.callback = response;
            return;
        }
    }
}

