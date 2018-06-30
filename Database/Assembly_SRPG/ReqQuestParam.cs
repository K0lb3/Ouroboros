namespace SRPG
{
    using System;

    public class ReqQuestParam : WebAPI
    {
        public ReqQuestParam(Network.ResponseCallback response)
        {
            base..ctor();
            base.name = "mst/10/quest";
            base.callback = response;
            return;
        }
    }
}

