namespace SRPG
{
    using System;

    public class ReqVersusStart : WebAPI
    {
        public ReqVersusStart(Network.ResponseCallback response)
        {
            base..ctor();
            base.name = "vs/start";
            base.body = WebAPI.GetRequestString(string.Empty);
            base.callback = response;
            return;
        }

        public class JSON_VersusMap
        {
            public string free;
            public string tower;
            public string friend;

            public JSON_VersusMap()
            {
                base..ctor();
                return;
            }
        }

        public class Response
        {
            public string app_id;
            public ReqVersusStart.JSON_VersusMap maps;

            public Response()
            {
                base..ctor();
                return;
            }
        }
    }
}

