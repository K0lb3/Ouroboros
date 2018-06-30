namespace SRPG
{
    using System;

    public class ReqMPVersion : WebAPI
    {
        public ReqMPVersion(Network.ResponseCallback response)
        {
            base..ctor();
            base.name = "btl/multi/check";
            base.body = string.Empty;
            base.body = WebAPI.GetRequestString(base.body);
            base.callback = response;
            return;
        }

        public class Response
        {
            public string device_id;

            public Response()
            {
                base..ctor();
                return;
            }
        }
    }
}

