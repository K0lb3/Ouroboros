namespace SRPG
{
    using System;
    using System.Text;

    public class ReqGetConceptCard : WebAPI
    {
        public ReqGetConceptCard(long last_card_iid, Network.ResponseCallback response)
        {
            StringBuilder builder;
            base..ctor();
            base.name = "unit/concept";
            builder = WebAPI.GetStringBuilder();
            builder.Append("\"last_iid\":");
            builder.Append(last_card_iid);
            base.body = WebAPI.GetRequestString(builder.ToString());
            base.callback = response;
            return;
        }
    }
}

