namespace SRPG
{
    using System;

    public class ReqItemAddStmPaid : WebAPI
    {
        public ReqItemAddStmPaid(Network.ResponseCallback response)
        {
            base..ctor();
            base.name = "item/addstmpaid";
            base.body = WebAPI.GetRequestString(null);
            base.callback = response;
            return;
        }
    }
}

