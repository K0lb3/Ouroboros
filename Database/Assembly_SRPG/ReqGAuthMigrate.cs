namespace SRPG
{
    using System;
    using System.Text;

    public class ReqGAuthMigrate : WebAPI
    {
        public ReqGAuthMigrate(string secretKey, string deviceID, string passcode, Network.ResponseCallback response)
        {
            StringBuilder builder;
            base..ctor();
            base.name = "gauth/migrate";
            builder = WebAPI.GetStringBuilder();
            builder.Append("\"secret_key\":\"");
            builder.Append(secretKey);
            builder.Append("\",\"device_id\":\"");
            builder.Append(deviceID);
            builder.Append("\",\"passcode\":\"");
            builder.Append(passcode);
            builder.Append("\"");
            base.body = WebAPI.GetRequestString(builder.ToString());
            base.callback = response;
            return;
        }
    }
}

