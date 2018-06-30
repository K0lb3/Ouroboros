namespace SRPG
{
    using System;
    using System.Text;

    public class ReqArtifactConvert : WebAPI
    {
        public unsafe ReqArtifactConvert(long[] artifact_iids, Network.ResponseCallback response)
        {
            StringBuilder builder;
            string str;
            int num;
            base..ctor();
            builder = WebAPI.GetStringBuilder();
            builder.Append("\"iids\":[");
            str = string.Empty;
            num = 0;
            goto Label_0041;
        Label_0025:
            str = str + &(artifact_iids[num]).ToString() + ",";
            num += 1;
        Label_0041:
            if (num < ((int) artifact_iids.Length))
            {
                goto Label_0025;
            }
            if (str.Length <= 0)
            {
                goto Label_0066;
            }
            str = str.Substring(0, str.Length - 1);
        Label_0066:
            builder.Append(str);
            builder.Append("]");
            base.name = "unit/job/artifact/convert";
            base.body = WebAPI.GetRequestString(builder.ToString());
            base.callback = response;
            return;
        }
    }
}

