namespace SRPG
{
    using System;
    using System.Text;

    public class ReqTowerReset : WebAPI
    {
        public ReqTowerReset(string qid, Network.ResponseCallback response)
        {
            StringBuilder builder;
            base..ctor();
            builder = WebAPI.GetStringBuilder();
            builder.Append("\"qid\":\"");
            builder.Append(qid);
            builder.Append("\"");
            base.name = "tower/reset";
            base.body = WebAPI.GetRequestString(builder.ToString());
            base.callback = response;
            return;
        }
    }
}

