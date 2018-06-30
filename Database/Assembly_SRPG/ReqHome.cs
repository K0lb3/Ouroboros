namespace SRPG
{
    using System;

    public class ReqHome : WebAPI
    {
        public ReqHome(Network.ResponseCallback response)
        {
            base..ctor();
            base.name = "home";
            base.body = WebAPI.GetRequestString(null);
            base.callback = response;
            return;
        }
    }
}

