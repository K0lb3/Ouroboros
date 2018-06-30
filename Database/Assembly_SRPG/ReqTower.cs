namespace SRPG
{
    using System;
    using System.Text;

    public class ReqTower : WebAPI
    {
        public ReqTower(string questID, Network.ResponseCallback response)
        {
            StringBuilder builder;
            base..ctor();
            builder = WebAPI.GetStringBuilder();
            base.name = "tower";
            builder.Append("\"qid\":\"");
            builder.Append(questID);
            builder.Append("\"");
            base.body = WebAPI.GetRequestString(builder.ToString());
            base.callback = response;
            return;
        }
    }
}

