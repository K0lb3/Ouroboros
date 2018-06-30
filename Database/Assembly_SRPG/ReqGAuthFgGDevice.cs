namespace SRPG
{
    using System;
    using System.Text;

    public class ReqGAuthFgGDevice : WebAPI
    {
        public ReqGAuthFgGDevice(string deviceID, string mail, string password, Network.ResponseCallback response)
        {
            StringBuilder builder;
            base..ctor();
            base.name = "gauth/fggid/device";
            builder = WebAPI.GetStringBuilder();
            builder.Append("\"idfv\":\"");
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

        public class Json_FggDevice
        {
            public string device_id;
            public string secret_key;

            public Json_FggDevice()
            {
                base..ctor();
                return;
            }
        }
    }
}

