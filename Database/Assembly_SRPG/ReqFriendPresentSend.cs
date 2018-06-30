namespace SRPG
{
    using System;
    using System.Runtime.InteropServices;
    using System.Text;

    public class ReqFriendPresentSend : WebAPI
    {
        public ReqFriendPresentSend(string url, Network.ResponseCallback response, string text, string trophyprog, string bingoprog)
        {
            StringBuilder builder;
            base..ctor();
            builder = WebAPI.GetStringBuilder();
            builder.Append(text);
            if (string.IsNullOrEmpty(trophyprog) != null)
            {
                goto Label_0029;
            }
            builder.Append(trophyprog);
        Label_0029:
            if (string.IsNullOrEmpty(bingoprog) != null)
            {
                goto Label_0056;
            }
            if (string.IsNullOrEmpty(trophyprog) != null)
            {
                goto Label_004D;
            }
            builder.Append(",");
        Label_004D:
            builder.Append(bingoprog);
        Label_0056:
            base.name = url;
            base.body = WebAPI.GetRequestString(builder.ToString());
            base.callback = response;
            return;
        }
    }
}

