namespace SRPG
{
    using System;

    public class ReqReLogin : WebAPI
    {
        public ReqReLogin(Network.ResponseCallback response)
        {
            base..ctor();
            base.name = "login/span";
            base.body = string.Empty;
            base.body = WebAPI.GetRequestString(base.body);
            base.callback = response;
            return;
        }
    }
}

