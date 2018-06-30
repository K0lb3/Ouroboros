namespace SRPG
{
    using System;

    public class ReqQuestBookmark : WebAPI
    {
        public ReqQuestBookmark(Network.ResponseCallback response)
        {
            base..ctor();
            base.name = "quest/favorite";
            base.body = WebAPI.GetRequestString(null);
            base.callback = response;
            return;
        }
    }
}

