namespace SRPG
{
    using System;
    using System.Collections.Generic;

    public class ReqUnitExpAdd : WebAPI
    {
        public unsafe ReqUnitExpAdd(long iid, Dictionary<string, int> usedItems, Network.ResponseCallback response)
        {
            string str;
            KeyValuePair<string, int> pair;
            Dictionary<string, int>.Enumerator enumerator;
            base..ctor();
            base.name = "unit/exp/add";
            base.body = "\"iid\":" + ((long) iid) + ",";
            base.body = base.body + "\"mats\":[";
            str = string.Empty;
            enumerator = usedItems.GetEnumerator();
        Label_004F:
            try
            {
                goto Label_00A4;
            Label_0054:
                pair = &enumerator.Current;
                str = str + "{";
                str = str + "\"iname\":\"" + &pair.Key + "\",";
                str = str + "\"num\":" + ((int) &pair.Value);
                str = str + "},";
            Label_00A4:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_0054;
                }
                goto Label_00C1;
            }
            finally
            {
            Label_00B5:
                ((Dictionary<string, int>.Enumerator) enumerator).Dispose();
            }
        Label_00C1:
            if (str.Length <= 0)
            {
                goto Label_00DD;
            }
            str = str.Substring(0, str.Length - 1);
        Label_00DD:
            base.body = base.body + str;
            base.body = base.body + "]";
            base.body = WebAPI.GetRequestString(base.body);
            base.callback = response;
            return;
        }
    }
}

