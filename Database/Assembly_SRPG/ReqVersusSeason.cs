namespace SRPG
{
    using System;

    public class ReqVersusSeason : WebAPI
    {
        public ReqVersusSeason(Network.ResponseCallback response)
        {
            base..ctor();
            base.name = "vs/towermatch/season";
            base.body = string.Empty;
            base.body = WebAPI.GetRequestString(base.body);
            base.callback = response;
            return;
        }

        public class Response
        {
            public int unreadmail;

            public Response()
            {
                base..ctor();
                return;
            }
        }
    }
}

