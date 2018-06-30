namespace SRPG
{
    using System;
    using System.Text;

    public class ReqBtlColoReq : WebAPI
    {
        public ReqBtlColoReq(string questID, string fuid, ArenaPlayer ap, Network.ResponseCallback response, int partyIndex)
        {
            StringBuilder builder;
            base..ctor();
            base.name = "btl/colo/req";
            builder = WebAPI.GetStringBuilder();
            if (partyIndex < 0)
            {
                goto Label_0040;
            }
            builder.Append("\"partyid\":");
            builder.Append(partyIndex);
            builder.Append(",");
        Label_0040:
            builder.Append("\"btlparam\":{},");
            builder.Append("\"fuid\":\"");
            builder.Append(fuid);
            builder.Append("\"");
            builder.Append(",");
            builder.Append("\"opp_rank\":");
            builder.Append(ap.ArenaRank);
            base.body = WebAPI.GetRequestString(builder.ToString());
            base.callback = response;
            return;
        }
    }
}

