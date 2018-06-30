namespace SRPG
{
    using System;

    public class ReqHikkoshiToken : WebAPI
    {
        public ReqHikkoshiToken(Network.ResponseCallback response)
        {
            base..ctor();
            base.name = "hikkoshi/token";
            base.body = WebAPI.GetRequestString(null);
            base.callback = response;
            return;
        }
    }
}

