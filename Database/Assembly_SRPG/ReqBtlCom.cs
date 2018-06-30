namespace SRPG
{
    using System;
    using System.Runtime.InteropServices;
    using System.Text;

    public class ReqBtlCom : WebAPI
    {
        public ReqBtlCom(Network.ResponseCallback response, bool refresh, bool tower_progress)
        {
            StringBuilder builder;
            string str;
            base..ctor();
            base.name = "btl/com";
            builder = WebAPI.GetStringBuilder();
            if (refresh == null)
            {
                goto Label_0029;
            }
            builder.Append("\"event\":1,");
        Label_0029:
            if (tower_progress == null)
            {
                goto Label_003B;
            }
            builder.Append("\"is_tower\":1,");
        Label_003B:
            str = builder.ToString();
            if (string.IsNullOrEmpty(str) != null)
            {
                goto Label_005C;
            }
            str = str.Remove(str.Length - 1);
        Label_005C:
            base.body = WebAPI.GetRequestString(str);
            base.callback = response;
            return;
        }
    }
}

