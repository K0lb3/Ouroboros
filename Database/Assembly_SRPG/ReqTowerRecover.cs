namespace SRPG
{
    using System;
    using System.Text;

    public class ReqTowerRecover : WebAPI
    {
        public ReqTowerRecover(string qid, int coin, int round, byte floor, Network.ResponseCallback response)
        {
            StringBuilder builder;
            base..ctor();
            builder = WebAPI.GetStringBuilder();
            base.name = "tower/recover";
            builder.Append("\"qid\":\"");
            builder.Append(qid);
            builder.Append("\",");
            builder.Append("\"coin\":");
            builder.Append(coin);
            builder.Append(",");
            builder.Append("\"round\":");
            builder.Append(round);
            builder.Append(",");
            builder.Append("\"floor\":");
            builder.Append(floor);
            base.body = WebAPI.GetRequestString(builder.ToString());
            base.callback = response;
            return;
        }
    }
}

