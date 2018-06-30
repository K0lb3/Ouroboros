namespace SRPG
{
    using System;
    using System.Collections.Generic;

    public class ReqEquipExpAdd : WebAPI
    {
        public unsafe ReqEquipExpAdd(long iid, int slot, Dictionary<string, int> usedItems, Network.ResponseCallback response)
        {
            object[] objArray1;
            string str;
            KeyValuePair<string, int> pair;
            Dictionary<string, int>.Enumerator enumerator;
            string str2;
            base..ctor();
            base.name = "unit/job/equip/enforce";
            base.body = "\"iid\":" + ((long) iid) + ",";
            str2 = base.body;
            objArray1 = new object[] { str2, "\"id_equip\":", (int) slot, "," };
            base.body = string.Concat(objArray1);
            base.body = base.body + "\"mats\":[";
            str = string.Empty;
            enumerator = usedItems.GetEnumerator();
        Label_0084:
            try
            {
                goto Label_00D9;
            Label_0089:
                pair = &enumerator.Current;
                str = str + "{";
                str = str + "\"iname\":\"" + &pair.Key + "\",";
                str = str + "\"num\":" + ((int) &pair.Value);
                str = str + "},";
            Label_00D9:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_0089;
                }
                goto Label_00F6;
            }
            finally
            {
            Label_00EA:
                ((Dictionary<string, int>.Enumerator) enumerator).Dispose();
            }
        Label_00F6:
            if (str.Length <= 0)
            {
                goto Label_0112;
            }
            str = str.Substring(0, str.Length - 1);
        Label_0112:
            base.body = base.body + str;
            base.body = base.body + "]";
            base.body = WebAPI.GetRequestString(base.body);
            base.callback = response;
            return;
        }
    }
}

