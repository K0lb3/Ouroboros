namespace SRPG
{
    using System;

    public class ReqMasterParam : WebAPI
    {
        public ReqMasterParam(Network.ResponseCallback response)
        {
            base..ctor();
            base.name = "mst/10/master";
            base.callback = response;
            return;
        }
    }
}

