namespace SRPG
{
    using System;
    using System.Text;

    public class ReqTowerReward : WebAPI
    {
        public ReqTowerReward(short mid, short nid, Network.ResponseCallback response)
        {
            StringBuilder builder;
            base..ctor();
            builder = WebAPI.GetStringBuilder();
            base.name = "expedition/reward";
            builder.Append("\"mid\":");
            builder.Append(mid);
            builder.Append(",");
            builder.Append("\"nid\":");
            builder.Append(nid);
            base.body = WebAPI.GetRequestString(builder.ToString());
            base.callback = response;
            return;
        }
    }
}

