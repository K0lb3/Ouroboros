namespace SRPG
{
    using System;

    public class ReqItemAddBoxPaid : WebAPI
    {
        public ReqItemAddBoxPaid(long iid, Network.ResponseCallback response)
        {
            base..ctor();
            base.name = "item/addboxpaid";
            base.body = WebAPI.GetRequestString(null);
            base.callback = response;
            return;
        }
    }
}

