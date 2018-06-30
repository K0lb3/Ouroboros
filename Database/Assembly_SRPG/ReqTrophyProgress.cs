namespace SRPG
{
    using System;

    public class ReqTrophyProgress : WebAPI
    {
        public ReqTrophyProgress(Network.ResponseCallback response)
        {
            base..ctor();
            base.name = "trophy";
            base.body = WebAPI.GetRequestString(null);
            base.callback = response;
            return;
        }
    }
}

