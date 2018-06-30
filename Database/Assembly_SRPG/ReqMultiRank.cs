namespace SRPG
{
    using System;

    public class ReqMultiRank : WebAPI
    {
        public ReqMultiRank(string iname, Network.ResponseCallback response)
        {
            base..ctor();
            base.name = "btl/usedunit";
            base.body = "\"iname\":\"" + JsonEscape.Escape(iname) + "\"";
            base.body = WebAPI.GetRequestString(base.body);
            base.callback = response;
            return;
        }

        public class Json_MultiRank
        {
            public ReqMultiRank.Json_MultiRankParam[] ranking;
            public int isReady;

            public Json_MultiRank()
            {
                base..ctor();
                return;
            }
        }

        public class Json_MultiRankParam
        {
            public string unit_iname;
            public string job_iname;

            public Json_MultiRankParam()
            {
                base..ctor();
                return;
            }
        }
    }
}

