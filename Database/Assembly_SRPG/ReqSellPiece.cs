namespace SRPG
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class ReqSellPiece : WebAPI
    {
        public unsafe ReqSellPiece(Dictionary<long, int> sells, Network.ResponseCallback response)
        {
            object[] objArray1;
            StringBuilder builder;
            string str;
            KeyValuePair<long, int> pair;
            Dictionary<long, int>.Enumerator enumerator;
            string str2;
            base..ctor();
            builder = WebAPI.GetStringBuilder();
            builder.Append("\"sells\":[");
            str = string.Empty;
            enumerator = sells.GetEnumerator();
        Label_0025:
            try
            {
                goto Label_0095;
            Label_002A:
                pair = &enumerator.Current;
                str = str + "{";
                str2 = str;
                objArray1 = new object[] { str2, "\"iid\":", (long) &pair.Key, "," };
                str = string.Concat(objArray1);
                str = str + "\"num\":" + ((int) &pair.Value);
                str = str + "},";
            Label_0095:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_002A;
                }
                goto Label_00B2;
            }
            finally
            {
            Label_00A6:
                ((Dictionary<long, int>.Enumerator) enumerator).Dispose();
            }
        Label_00B2:
            if (str.Length <= 0)
            {
                goto Label_00CE;
            }
            str = str.Substring(0, str.Length - 1);
        Label_00CE:
            builder.Append(str);
            builder.Append("]");
            base.name = "shop/piece/sell";
            base.body = WebAPI.GetRequestString(builder.ToString());
            base.callback = response;
            return;
        }
    }
}

