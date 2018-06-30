namespace SRPG
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;
    using System.Text;

    public class ReqAbilityRankUp : WebAPI
    {
        public unsafe ReqAbilityRankUp(Dictionary<long, int> abilities, Network.ResponseCallback response, string trophyprog, string bingoprog)
        {
            object[] objArray1;
            StringBuilder builder;
            string str;
            KeyValuePair<long, int> pair;
            Dictionary<long, int>.Enumerator enumerator;
            string str2;
            base..ctor();
            base.name = "unit/job/abil/lvup";
            builder = WebAPI.GetStringBuilder();
            builder.Append("\"aps\":[");
            str = string.Empty;
            enumerator = abilities.GetEnumerator();
        Label_0030:
            try
            {
                goto Label_00A0;
            Label_0035:
                pair = &enumerator.Current;
                str = str + "{";
                str2 = str;
                objArray1 = new object[] { str2, "\"iid\":", (long) &pair.Key, "," };
                str = string.Concat(objArray1);
                str = str + "\"ap\":" + ((int) &pair.Value);
                str = str + "},";
            Label_00A0:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_0035;
                }
                goto Label_00BD;
            }
            finally
            {
            Label_00B1:
                ((Dictionary<long, int>.Enumerator) enumerator).Dispose();
            }
        Label_00BD:
            if (str.Length <= 0)
            {
                goto Label_00DF;
            }
            builder.Append(str.Substring(0, str.Length - 1));
        Label_00DF:
            builder.Append("]");
            if (string.IsNullOrEmpty(trophyprog) != null)
            {
                goto Label_010A;
            }
            builder.Append(",");
            builder.Append(trophyprog);
        Label_010A:
            if (string.IsNullOrEmpty(bingoprog) != null)
            {
                goto Label_012B;
            }
            builder.Append(",");
            builder.Append(bingoprog);
        Label_012B:
            base.body = WebAPI.GetRequestString(builder.ToString());
            base.callback = response;
            return;
        }
    }
}

