namespace SRPG
{
    using System;

    public class ReqMultiPlayResume : WebAPI
    {
        public ReqMultiPlayResume(long btlID, Network.ResponseCallback response)
        {
            base..ctor();
            base.name = "btl/multi/resume";
            base.body = string.Empty;
            base.body = base.body + "\"btlid\":" + ((long) btlID);
            base.body = WebAPI.GetRequestString(base.body);
            base.callback = response;
            return;
        }

        public class Quest
        {
            public string iname;

            public Quest()
            {
                base..ctor();
                return;
            }
        }

        public class Response
        {
            public ReqMultiPlayResume.Quest quest;
            public int btlid;
            public string app_id;
            public string token;
            public Json_BtlInfo_Multi btlinfo;

            public Response()
            {
                base..ctor();
                return;
            }
        }
    }
}

