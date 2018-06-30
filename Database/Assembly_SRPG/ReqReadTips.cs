namespace SRPG
{
    using System;
    using System.Text;

    public class ReqReadTips : WebAPI
    {
        public ReqReadTips(string tips, string trophyprog, string bingoprog, Network.ResponseCallback response)
        {
            StringBuilder builder;
            base..ctor();
            base.name = "tips/end";
            builder = new StringBuilder();
            builder.Append("\"iname\":");
            builder.Append("\"" + tips + "\"");
            if (string.IsNullOrEmpty(trophyprog) != null)
            {
                goto Label_0059;
            }
            builder.Append(",");
            builder.Append(trophyprog);
        Label_0059:
            if (string.IsNullOrEmpty(bingoprog) != null)
            {
                goto Label_0078;
            }
            builder.Append(",");
            builder.Append(bingoprog);
        Label_0078:
            base.body = WebAPI.GetRequestString(builder.ToString());
            base.callback = response;
            return;
        }
    }
}

