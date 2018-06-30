namespace SRPG
{
    using System;

    public class ReqCheckVersion2 : WebAPI
    {
        public ReqCheckVersion2(string ver, Network.ResponseCallback response)
        {
            base..ctor();
            base.name = "chkver2";
            base.body = "{\"ver\":\"";
            base.body = base.body + ver;
            base.body = base.body + "\"}";
            base.callback = response;
            return;
        }
    }
}

