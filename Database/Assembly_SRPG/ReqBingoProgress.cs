namespace SRPG
{
    using System;

    public class ReqBingoProgress : WebAPI
    {
        public ReqBingoProgress(Network.ResponseCallback response)
        {
            base..ctor();
            base.name = "bingo";
            base.body = WebAPI.GetRequestString(null);
            base.callback = response;
            return;
        }
    }
}

