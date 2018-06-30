namespace SRPG
{
    using System;
    using System.Collections.Generic;

    public class ReqItemSell : WebAPI
    {
        public unsafe ReqItemSell(Dictionary<long, int> sells, bool is_item_convert, Network.ResponseCallback response)
        {
            string str;
            KeyValuePair<long, int> pair;
            Dictionary<long, int>.Enumerator enumerator;
            long num;
            int num2;
            base..ctor();
            base.name = (is_item_convert == null) ? "item/sell" : "item/auto/sell";
            base.body = "\"sells\":[";
            str = string.Empty;
            enumerator = sells.GetEnumerator();
        Label_0039:
            try
            {
                goto Label_009A;
            Label_003E:
                pair = &enumerator.Current;
                str = str + "{";
                str = str + "\"iid\":" + &&pair.Key.ToString() + ",";
                str = str + "\"num\":" + &&pair.Value.ToString();
                str = str + "},";
            Label_009A:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_003E;
                }
                goto Label_00B7;
            }
            finally
            {
            Label_00AB:
                ((Dictionary<long, int>.Enumerator) enumerator).Dispose();
            }
        Label_00B7:
            if (str.Length <= 0)
            {
                goto Label_00D3;
            }
            str = str.Substring(0, str.Length - 1);
        Label_00D3:
            base.body = base.body + str;
            base.body = base.body + "]";
            base.body = WebAPI.GetRequestString(base.body);
            base.callback = response;
            return;
        }
    }
}

