namespace SRPG
{
    using System;

    public class ReqProductParam : WebAPI
    {
        public ReqProductParam(Network.ResponseCallback response)
        {
            base..ctor();
            base.name = "product";
            base.body = WebAPI.GetRequestString(null);
            base.callback = response;
            return;
        }
    }
}

