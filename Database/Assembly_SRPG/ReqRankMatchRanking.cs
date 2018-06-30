namespace SRPG
{
    using System;

    public class ReqRankMatchRanking : WebAPI
    {
        public ReqRankMatchRanking(Network.ResponseCallback response)
        {
            base..ctor();
            base.name = "vs/rankmatch/ranking";
            base.body = WebAPI.GetRequestString(string.Empty);
            base.callback = response;
            return;
        }

        [Serializable]
        public class ResponceRanking
        {
            public int type;
            public int score;
            public int rank;
            public Json_Friend enemy;

            public ResponceRanking()
            {
                base..ctor();
                return;
            }
        }

        [Serializable]
        public class Response
        {
            public ReqRankMatchRanking.ResponceRanking[] rankings;

            public Response()
            {
                base..ctor();
                return;
            }
        }
    }
}

