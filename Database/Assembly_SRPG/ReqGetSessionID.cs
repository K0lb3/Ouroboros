namespace SRPG
{
    using System;

    public class ReqGetSessionID : WebAPI
    {
        public ReqGetSessionID(string udid, string udid_, string romver, Network.ResponseCallback response)
        {
            base..ctor();
            base.name = "getsid";
            base.body = "{\"ticket\":" + ((int) Network.TicketID) + ",\"param\":{";
            base.body = base.body + "\"udid\":\"" + udid + "\",";
            base.body = base.body + "\"udid_\":\"" + udid_ + "\",";
            base.body = base.body + "\"romver\":\"" + romver + "\"";
            base.body = base.body + "}}";
            base.callback = response;
            return;
        }
    }
}

