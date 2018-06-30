namespace SRPG
{
    using System;

    public class ReqVersusDraft : WebAPI
    {
        public ReqVersusDraft(string token, Network.ResponseCallback response)
        {
            RequestParam param;
            base..ctor();
            base.name = "vs/draft";
            param = new RequestParam();
            param.token = token;
            base.body = WebAPI.GetRequestString<RequestParam>(param);
            base.callback = response;
            return;
        }

        [Serializable]
        public class RequestParam
        {
            public string token;

            public RequestParam()
            {
                base..ctor();
                return;
            }
        }

        public class Response
        {
            public int turn_own;
            public ReqVersusDraft.ResponseUnit[] draft_units;

            public Response()
            {
                base..ctor();
                return;
            }
        }

        public class ResponseUnit
        {
            public long id;
            public int secret;

            public ResponseUnit()
            {
                base..ctor();
                return;
            }
        }
    }
}

