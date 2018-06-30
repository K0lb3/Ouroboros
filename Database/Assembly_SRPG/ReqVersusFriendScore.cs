namespace SRPG
{
    using System;

    public class ReqVersusFriendScore : WebAPI
    {
        public ReqVersusFriendScore(Network.ResponseCallback response)
        {
            base..ctor();
            base.name = "vs/towermatch/friend_score";
            base.body = string.Empty;
            base.body = WebAPI.GetRequestString(base.body);
            base.callback = response;
            return;
        }

        public class Response
        {
            public Json_VersusFriendScore[] friends;

            public Response()
            {
                base..ctor();
                return;
            }
        }
    }
}

