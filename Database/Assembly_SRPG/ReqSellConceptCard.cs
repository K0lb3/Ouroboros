namespace SRPG
{
    using System;
    using System.Text;

    public class ReqSellConceptCard : WebAPI
    {
        public ReqSellConceptCard(long[] sell_ids, Network.ResponseCallback response)
        {
            StringBuilder builder;
            int num;
            base..ctor();
            base.name = "unit/concept/sell";
            builder = WebAPI.GetStringBuilder();
            builder.Append("\"sell_ids\":[");
            num = 0;
            goto Label_004F;
        Label_002A:
            builder.Append(sell_ids[num]);
            if (num == (((int) sell_ids.Length) - 1))
            {
                goto Label_004B;
            }
            builder.Append(",");
        Label_004B:
            num += 1;
        Label_004F:
            if (num < ((int) sell_ids.Length))
            {
                goto Label_002A;
            }
            builder.Append("]");
            base.body = WebAPI.GetRequestString(builder.ToString());
            base.callback = response;
            return;
        }
    }
}

