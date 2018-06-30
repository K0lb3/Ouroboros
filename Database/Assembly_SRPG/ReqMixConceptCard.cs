namespace SRPG
{
    using System;
    using System.Runtime.InteropServices;
    using System.Text;

    public class ReqMixConceptCard : WebAPI
    {
        public ReqMixConceptCard(long base_id, long[] mix_ids, Network.ResponseCallback response, string trophyProgs, string bingoProgs)
        {
            StringBuilder builder;
            int num;
            base..ctor();
            base.name = "unit/concept/mix";
            builder = WebAPI.GetStringBuilder();
            builder.Append("\"base_id\":");
            builder.Append(base_id);
            builder.Append(",");
            builder.Append("\"mix_ids\":[");
            num = 0;
            goto Label_006F;
        Label_004A:
            builder.Append(mix_ids[num]);
            if (num == (((int) mix_ids.Length) - 1))
            {
                goto Label_006B;
            }
            builder.Append(",");
        Label_006B:
            num += 1;
        Label_006F:
            if (num < ((int) mix_ids.Length))
            {
                goto Label_004A;
            }
            builder.Append("]");
            if (string.IsNullOrEmpty(trophyProgs) != null)
            {
                goto Label_00A5;
            }
            builder.Append(",");
            builder.Append(trophyProgs);
        Label_00A5:
            if (string.IsNullOrEmpty(bingoProgs) != null)
            {
                goto Label_00C6;
            }
            builder.Append(",");
            builder.Append(bingoProgs);
        Label_00C6:
            base.body = WebAPI.GetRequestString(builder.ToString());
            base.callback = response;
            return;
        }
    }
}

