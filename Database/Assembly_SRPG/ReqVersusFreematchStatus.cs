namespace SRPG
{
    using System;

    public class ReqVersusFreematchStatus : WebAPI
    {
        public ReqVersusFreematchStatus(Network.ResponseCallback response)
        {
            base..ctor();
            base.name = "vs/freematch/status";
            base.body = string.Empty;
            base.body = WebAPI.GetRequestString(base.body);
            base.callback = response;
            return;
        }

        public class EnableTimeSchedule
        {
            public long expired;
            public long next;

            public EnableTimeSchedule()
            {
                base..ctor();
                return;
            }
        }

        public class Response
        {
            public ReqVersusFreematchStatus.EnableTimeSchedule enabletime;

            public Response()
            {
                base..ctor();
                return;
            }
        }
    }
}

