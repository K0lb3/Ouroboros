namespace SRPG
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class ReqArtifactEnforce : WebAPI
    {
        public unsafe ReqArtifactEnforce(long iid, Dictionary<string, int> usedItems, Network.ResponseCallback response)
        {
            StringBuilder builder;
            string str;
            KeyValuePair<string, int> pair;
            Dictionary<string, int>.Enumerator enumerator;
            base..ctor();
            builder = WebAPI.GetStringBuilder();
            builder.Append("\"iid\":");
            builder.Append(iid);
            builder.Append(",\"mats\":[");
            str = string.Empty;
            enumerator = usedItems.GetEnumerator();
        Label_0039:
            try
            {
                goto Label_008E;
            Label_003E:
                pair = &enumerator.Current;
                str = str + "{";
                str = str + "\"iname\":\"" + &pair.Key + "\",";
                str = str + "\"num\":" + ((int) &pair.Value);
                str = str + "},";
            Label_008E:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_003E;
                }
                goto Label_00AB;
            }
            finally
            {
            Label_009F:
                ((Dictionary<string, int>.Enumerator) enumerator).Dispose();
            }
        Label_00AB:
            if (str.Length <= 0)
            {
                goto Label_00C7;
            }
            str = str.Substring(0, str.Length - 1);
        Label_00C7:
            builder.Append(str);
            builder.Append("]");
            base.name = "unit/job/artifact/enforce";
            base.body = WebAPI.GetRequestString(builder.ToString());
            base.callback = response;
            return;
        }
    }
}

