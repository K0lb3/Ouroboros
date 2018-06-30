namespace SRPG
{
    using System;

    public class ReqAuthEmailRegister : WebAPI
    {
        public ReqAuthEmailRegister(string email, string password, string device_id, string secret_key, string udid, Network.ResponseCallback response)
        {
            object[] objArray1;
            string str;
            base..ctor();
            base.name = "auth/email/register";
            base.body = "{";
            str = base.body;
            objArray1 = new object[] { str, "\"ticket\":", (int) Network.TicketID, "," };
            base.body = string.Concat(objArray1);
            base.body = base.body + "\"access_token\":\"\",";
            base.body = base.body + "\"email\":\"" + email + "\",";
            base.body = base.body + "\"password\":\"" + password + "\",";
            base.body = base.body + "\"disable_validation_email\":true,";
            base.body = base.body + "\"device_id\":\"" + device_id + "\",";
            base.body = base.body + "\"secret_key\":\"" + secret_key + "\",";
            base.body = base.body + "\"udid\":\"" + udid + "\"";
            base.body = base.body + "}";
            base.callback = response;
            return;
        }
    }
}

