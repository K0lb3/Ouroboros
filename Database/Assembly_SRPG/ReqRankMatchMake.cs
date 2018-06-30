namespace SRPG
{
    using System;
    using System.Runtime.InteropServices;

    public class ReqRankMatchMake : WebAPI
    {
        public ReqRankMatchMake(Network.ResponseCallback response)
        {
            base..ctor();
            base.name = "vs/rankmatch/make";
            base.body = WebAPI.GetRequestString(string.Empty);
            base.callback = response;
            return;
        }

        public class Response
        {
            public string token;
            public string owner_name;

            public Response()
            {
                base..ctor();
                return;
            }
        }
    }
}

