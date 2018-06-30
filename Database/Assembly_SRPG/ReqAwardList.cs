namespace SRPG
{
    using System;

    public class ReqAwardList : WebAPI
    {
        public ReqAwardList(Network.ResponseCallback response)
        {
            base..ctor();
            base.name = "award";
            base.body = WebAPI.GetRequestString(null);
            base.callback = response;
            return;
        }
    }
}

