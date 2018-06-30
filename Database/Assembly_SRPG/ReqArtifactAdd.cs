namespace SRPG
{
    using System;
    using System.Runtime.InteropServices;
    using System.Text;

    public class ReqArtifactAdd : WebAPI
    {
        public ReqArtifactAdd(string iname, Network.ResponseCallback response, string trophyprog, string bingoprog)
        {
            StringBuilder builder;
            base..ctor();
            builder = WebAPI.GetStringBuilder();
            builder.Append("\"iname\":\"");
            builder.Append(iname);
            builder.Append(0x22);
            if (string.IsNullOrEmpty(trophyprog) != null)
            {
                goto Label_0048;
            }
            builder.Append(",");
            builder.Append(trophyprog);
        Label_0048:
            if (string.IsNullOrEmpty(bingoprog) != null)
            {
                goto Label_0069;
            }
            builder.Append(",");
            builder.Append(bingoprog);
        Label_0069:
            base.name = "unit/job/artifact/add";
            base.body = WebAPI.GetRequestString(builder.ToString());
            base.callback = response;
            return;
        }
    }
}

