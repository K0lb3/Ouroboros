namespace SRPG
{
    using System;
    using System.Text;

    public class ReqTrophyAchievedQuest : WebAPI
    {
        public ReqTrophyAchievedQuest(string iname, Network.ResponseCallback response)
        {
            StringBuilder builder;
            base..ctor();
            base.name = "trophy/history";
            builder = WebAPI.GetStringBuilder();
            builder.Append("\"iname\":\"" + iname + "\"");
            base.body = WebAPI.GetRequestString(builder.ToString());
            base.callback = response;
            return;
        }
    }
}

