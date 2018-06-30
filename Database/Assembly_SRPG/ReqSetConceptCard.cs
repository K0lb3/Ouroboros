namespace SRPG
{
    using System;
    using System.Text;

    public class ReqSetConceptCard : WebAPI
    {
        private ReqSetConceptCard(long card_iid, long unit_iid, Network.ResponseCallback response)
        {
            StringBuilder builder;
            base..ctor();
            base.name = "unit/concept/set";
            builder = WebAPI.GetStringBuilder();
            builder.Append("\"concept_iid\":");
            builder.Append(card_iid);
            builder.Append(",");
            builder.Append("\"unit_iid\":");
            builder.Append(unit_iid);
            base.body = WebAPI.GetRequestString(builder.ToString());
            base.callback = response;
            return;
        }

        public static ReqSetConceptCard CreateSet(long card_iid, long unit_iid, Network.ResponseCallback response)
        {
            return new ReqSetConceptCard(card_iid, unit_iid, response);
        }

        public static ReqSetConceptCard CreateUnset(long card_iid, Network.ResponseCallback response)
        {
            return new ReqSetConceptCard(card_iid, 0L, response);
        }
    }
}

