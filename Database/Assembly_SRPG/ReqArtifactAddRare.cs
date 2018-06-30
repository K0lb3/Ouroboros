namespace SRPG
{
    using System;
    using System.Runtime.InteropServices;
    using System.Text;

    public class ReqArtifactAddRare : WebAPI
    {
        public ReqArtifactAddRare(long iid, Network.ResponseCallback response, string trophyprog, string bingoprog)
        {
            StringBuilder builder;
            base..ctor();
            builder = WebAPI.GetStringBuilder();
            builder.Append("\"iid\":");
            builder.Append(iid);
            if (string.IsNullOrEmpty(trophyprog) != null)
            {
                goto Label_003F;
            }
            builder.Append(",");
            builder.Append(trophyprog);
        Label_003F:
            if (string.IsNullOrEmpty(bingoprog) != null)
            {
                goto Label_0060;
            }
            builder.Append(",");
            builder.Append(bingoprog);
        Label_0060:
            base.name = "unit/job/artifact/rare/add";
            base.body = WebAPI.GetRequestString(builder.ToString());
            base.callback = response;
            return;
        }
    }
}

