namespace SRPG
{
    using System;

    public class ReqProductBuy : WebAPI
    {
        public ReqProductBuy(string productID, string receipt, string transactionID, Network.ResponseCallback response)
        {
            base..ctor();
            base.name = "product/buy";
            base.body = string.Empty;
            base.body = base.body + "\"productid\":\"" + productID + "\",";
            base.body = base.body + "\"receipt\":\"" + receipt + "\",";
            base.body = base.body + "\"transactionid\":\"" + transactionID + "\"";
            base.body = WebAPI.GetRequestString(base.body);
            base.callback = response;
            return;
        }
    }
}

