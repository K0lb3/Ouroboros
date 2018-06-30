namespace SRPG
{
    using System;
    using System.Text;

    public class ReqTrustMasterConceptCard : WebAPI
    {
        public ReqTrustMasterConceptCard(long card_iid, bool is_favorite, Network.ResponseCallback response)
        {
            StringBuilder builder;
            base..ctor();
            base.name = "unit/concept/trust/reward";
            builder = WebAPI.GetStringBuilder();
            builder.Append("\"concept_iid\":");
            builder.Append(card_iid);
            base.body = WebAPI.GetRequestString(builder.ToString());
            base.callback = response;
            return;
        }
    }
}

