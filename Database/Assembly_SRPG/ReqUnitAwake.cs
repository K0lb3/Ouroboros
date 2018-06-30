namespace SRPG
{
    using System;
    using System.Runtime.InteropServices;
    using System.Text;

    public class ReqUnitAwake : WebAPI
    {
        public ReqUnitAwake(long iid, Network.ResponseCallback response, string trophyprog, string bingoprog, int awake_lv)
        {
            StringBuilder builder;
            base..ctor();
            base.name = "unit/plus/add";
            builder = WebAPI.GetStringBuilder();
            builder.Append("\"iid\":" + ((long) iid));
            if (awake_lv <= 0)
            {
                goto Label_005A;
            }
            builder.Append(",");
            builder.Append("\"target_plus\":" + ((int) awake_lv));
        Label_005A:
            if (string.IsNullOrEmpty(trophyprog) != null)
            {
                goto Label_0079;
            }
            builder.Append(",");
            builder.Append(trophyprog);
        Label_0079:
            if (string.IsNullOrEmpty(bingoprog) != null)
            {
                goto Label_009A;
            }
            builder.Append(",");
            builder.Append(bingoprog);
        Label_009A:
            base.body = WebAPI.GetRequestString(builder.ToString());
            base.callback = response;
            return;
        }
    }
}

