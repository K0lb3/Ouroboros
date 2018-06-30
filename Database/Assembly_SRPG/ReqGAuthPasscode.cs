namespace SRPG
{
    using System;
    using System.Text;

    public class ReqGAuthPasscode : WebAPI
    {
        public ReqGAuthPasscode(string secretKey, string deviceID, Network.ResponseCallback response)
        {
            StringBuilder builder;
            base..ctor();
            base.name = "gauth/passcode";
            builder = WebAPI.GetStringBuilder();
            builder.Append("\"secret_key\":\"");
            builder.Append(secretKey);
            builder.Append("\",\"device_id\":\"");
            builder.Append(deviceID);
            builder.Append("\"");
            base.body = WebAPI.GetRequestString(builder.ToString());
            base.callback = response;
            return;
        }
    }
}

