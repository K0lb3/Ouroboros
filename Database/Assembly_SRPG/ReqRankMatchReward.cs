namespace SRPG
{
    using System;

    public class ReqRankMatchReward : WebAPI
    {
        public ReqRankMatchReward(Network.ResponseCallback response)
        {
            base..ctor();
            base.name = "vs/rankmatch/reward";
            base.body = WebAPI.GetRequestString(string.Empty);
            base.callback = response;
            return;
        }

        [Serializable]
        public class Response
        {
            public int schedule_id;
            public int score;
            public int rank;
            public int type;
            public ReqRankMatchReward.RwardResponse reward;

            public Response()
            {
                base..ctor();
                return;
            }
        }

        [Serializable]
        public class RwardResponse
        {
            public string ranking;
            public string type;

            public RwardResponse()
            {
                base..ctor();
                return;
            }
        }
    }
}

