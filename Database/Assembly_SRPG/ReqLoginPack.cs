namespace SRPG
{
    using System;
    using System.Runtime.InteropServices;

    public class ReqLoginPack : WebAPI
    {
        public ReqLoginPack(Network.ResponseCallback response, bool relogin)
        {
            base..ctor();
            base.name = "login/param";
            base.body = "\"relogin\":";
            base.body = base.body + ((int) ((relogin == null) ? 0 : 1));
            base.body = base.body + ",\"req_uid\":1";
            base.body = WebAPI.GetRequestString(base.body);
            base.callback = response;
            return;
        }
    }
}

