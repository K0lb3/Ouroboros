namespace SRPG
{
    using System;
    using System.Runtime.InteropServices;
    using System.Text;

    public class ReqUnitRare : WebAPI
    {
        public ReqUnitRare(long iid, Network.ResponseCallback response, string trophyprog, string bingoprog)
        {
            StringBuilder builder;
            base..ctor();
            base.name = "unit/rare/add";
            builder = WebAPI.GetStringBuilder();
            builder.Append("\"iid\":" + ((long) iid));
            if (string.IsNullOrEmpty(trophyprog) != null)
            {
                goto Label_004D;
            }
            builder.Append(",");
            builder.Append(trophyprog);
        Label_004D:
            if (string.IsNullOrEmpty(bingoprog) != null)
            {
                goto Label_006E;
            }
            builder.Append(",");
            builder.Append(bingoprog);
        Label_006E:
            base.body = WebAPI.GetRequestString(builder.ToString());
            base.callback = response;
            return;
        }
    }
}

