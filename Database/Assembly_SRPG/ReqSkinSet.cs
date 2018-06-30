namespace SRPG
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class ReqSkinSet : WebAPI
    {
        public unsafe ReqSkinSet(Dictionary<long, string> sets, Network.ResponseCallback response)
        {
            StringBuilder builder;
            string str;
            KeyValuePair<long, string> pair;
            Dictionary<long, string>.Enumerator enumerator;
            long num;
            base..ctor();
            builder = WebAPI.GetStringBuilder();
            builder.Append("\"sets\":[");
            str = string.Empty;
            enumerator = sets.GetEnumerator();
        Label_0025:
            try
            {
                goto Label_0083;
            Label_002A:
                pair = &enumerator.Current;
                str = str + "{";
                str = str + "\"iid\":" + &&pair.Key.ToString() + ",";
                str = str + "\"iname\":\"" + &pair.Value + "\"";
                str = str + "},";
            Label_0083:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_002A;
                }
                goto Label_00A0;
            }
            finally
            {
            Label_0094:
                ((Dictionary<long, string>.Enumerator) enumerator).Dispose();
            }
        Label_00A0:
            if (str.Length <= 0)
            {
                goto Label_00BC;
            }
            str = str.Substring(0, str.Length - 1);
        Label_00BC:
            builder.Append(str);
            builder.Append("]");
            base.name = "unit/skin/set";
            base.body = WebAPI.GetRequestString(builder.ToString());
            base.callback = response;
            return;
        }
    }
}

