namespace SRPG
{
    using System;
    using System.Text;

    public class ReqFavoriteConceptCard : WebAPI
    {
        public ReqFavoriteConceptCard(long card_iid, bool is_favorite, Network.ResponseCallback response)
        {
            StringBuilder builder;
            base..ctor();
            base.name = "unit/concept/favorite";
            builder = WebAPI.GetStringBuilder();
            builder.Append("\"concept_iid\":");
            builder.Append(card_iid);
            builder.Append(",");
            builder.Append("\"is_favorite\":");
            builder.Append((is_favorite == null) ? 0 : 1);
            base.body = WebAPI.GetRequestString(builder.ToString());
            base.callback = response;
            return;
        }
    }
}

