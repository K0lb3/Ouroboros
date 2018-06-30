namespace SRPG
{
    using System;

    public class ReqGetTipsAlreadyRead : WebAPI
    {
        public ReqGetTipsAlreadyRead(Network.ResponseCallback response)
        {
            base..ctor();
            base.name = "tips";
            base.body = WebAPI.GetRequestString(null);
            base.callback = response;
            return;
        }
    }
}

