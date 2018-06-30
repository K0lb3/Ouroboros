namespace SRPG
{
    using System;

    public class ReqGetDeviceID : WebAPI
    {
        public ReqGetDeviceID(string secretkey, string udid, Network.ResponseCallback response)
        {
            object[] objArray1;
            string str;
            base..ctor();
            base.name = "gauth/register";
            base.body = "{";
            str = base.body;
            objArray1 = new object[] { str, "\"ticket\":", (int) Network.TicketID, "," };
            base.body = string.Concat(objArray1);
            base.body = base.body + "\"access_token\":\"\",";
            base.body = base.body + "\"param\":{";
            base.body = base.body + "\"secret_key\":\"" + secretkey + "\",";
            base.body = base.body + "\"udid\":\"" + udid + "\"";
            base.body = base.body + "}";
            base.body = base.body + "}";
            base.callback = response;
            return;
        }
    }
}

