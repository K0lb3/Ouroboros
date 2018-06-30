namespace SRPG
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class ReqQuestBookmarkUpdate : WebAPI
    {
        public ReqQuestBookmarkUpdate(IEnumerable<string> add, IEnumerable<string> delete, Network.ResponseCallback response)
        {
            StringBuilder builder;
            string str;
            IEnumerator<string> enumerator;
            string str2;
            IEnumerator<string> enumerator2;
            base..ctor();
            base.name = "quest/favorite/set";
            builder = WebAPI.GetStringBuilder();
            builder.Append("\"inames\":{");
            if (delete == null)
            {
                goto Label_00AB;
            }
            if (Enumerable.Count<string>(delete) <= 0)
            {
                goto Label_00AB;
            }
            builder.Append("\"del\":[");
            enumerator = delete.GetEnumerator();
        Label_0048:
            try
            {
                goto Label_0074;
            Label_004D:
                str = enumerator.Current;
                builder.Append("\"");
                builder.Append(str);
                builder.Append("\",");
            Label_0074:
                if (enumerator.MoveNext() != null)
                {
                    goto Label_004D;
                }
                goto Label_008F;
            }
            finally
            {
            Label_0084:
                if (enumerator != null)
                {
                    goto Label_0088;
                }
            Label_0088:
                enumerator.Dispose();
            }
        Label_008F:
            builder.Remove(builder.Length - 1, 1);
            builder.Append("]");
        Label_00AB:
            if (add == null)
            {
                goto Label_0156;
            }
            if (Enumerable.Count<string>(add) <= 0)
            {
                goto Label_0156;
            }
            if (delete == null)
            {
                goto Label_00DB;
            }
            if (Enumerable.Count<string>(delete) <= 0)
            {
                goto Label_00DB;
            }
            builder.Append(",");
        Label_00DB:
            builder.Append("\"add\":[");
            enumerator2 = add.GetEnumerator();
        Label_00EF:
            try
            {
                goto Label_011C;
            Label_00F4:
                str2 = enumerator2.Current;
                builder.Append("\"");
                builder.Append(str2);
                builder.Append("\",");
            Label_011C:
                if (enumerator2.MoveNext() != null)
                {
                    goto Label_00F4;
                }
                goto Label_013A;
            }
            finally
            {
            Label_012D:
                if (enumerator2 != null)
                {
                    goto Label_0132;
                }
            Label_0132:
                enumerator2.Dispose();
            }
        Label_013A:
            builder.Remove(builder.Length - 1, 1);
            builder.Append("]");
        Label_0156:
            builder.Append("}");
            base.body = WebAPI.GetRequestString(builder.ToString());
            base.callback = response;
            return;
        }
    }
}

