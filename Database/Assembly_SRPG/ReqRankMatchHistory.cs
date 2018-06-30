namespace SRPG
{
    using System;

    public class ReqRankMatchHistory : WebAPI
    {
        public ReqRankMatchHistory(Network.ResponseCallback response)
        {
            base..ctor();
            base.name = "vs/rankmatch/history";
            base.body = WebAPI.GetRequestString(string.Empty);
            base.callback = response;
            return;
        }

        [Serializable]
        public class ResponceHistory
        {
            public ReqRankMatchHistory.ResponceHistoryList[] list;
            public ReqRankMatchHistory.ResponceHistoryOption option;

            public ResponceHistory()
            {
                base..ctor();
                return;
            }
        }

        [Serializable]
        public class ResponceHistoryList
        {
            public int id;
            public int score;
            public int enemyscore;
            public int value;
            public long time_start;
            public long time_end;
            public ReqRankMatchHistory.ResponceHistoryResult result;
            public Json_Friend enemy;
            public int type;

            public ResponceHistoryList()
            {
                base..ctor();
                return;
            }
        }

        [Serializable]
        public class ResponceHistoryOption
        {
            public int totalPage;

            public ResponceHistoryOption()
            {
                base..ctor();
                return;
            }
        }

        [Serializable]
        public class ResponceHistoryResult
        {
            public long time;
            public string result;
            public int[] beats;
            public string token;
            public int beatcnt;

            public ResponceHistoryResult()
            {
                base..ctor();
                return;
            }
        }

        [Serializable]
        public class Response
        {
            public ReqRankMatchHistory.ResponceHistory histories;

            public Response()
            {
                base..ctor();
                return;
            }
        }
    }
}

