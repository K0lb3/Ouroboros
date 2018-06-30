namespace SRPG
{
    using System;
    using System.Text;

    public class ReqRanking : WebAPI
    {
        public ReqRanking(string[] inames, Network.ResponseCallback response)
        {
            StringBuilder builder;
            int num;
            base..ctor();
            base.name = "btl/usedunit/multiple";
            builder = WebAPI.GetStringBuilder();
            builder.Append("\"inames\":[");
            num = 0;
            goto Label_0071;
        Label_002A:
            builder.Append("\"");
            builder.Append(JsonEscape.Escape(inames[num]));
            if (num != (((int) inames.Length) - 1))
            {
                goto Label_0061;
            }
            builder.Append("\"]");
            goto Label_006D;
        Label_0061:
            builder.Append("\",");
        Label_006D:
            num += 1;
        Label_0071:
            if (num < ((int) inames.Length))
            {
                goto Label_002A;
            }
            base.body = builder.ToString();
            base.body = WebAPI.GetRequestString(base.body);
            base.callback = response;
            return;
        }
    }
}

