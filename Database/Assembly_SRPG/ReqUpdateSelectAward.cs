namespace SRPG
{
    using System;
    using System.Text;

    public class ReqUpdateSelectAward : WebAPI
    {
        public ReqUpdateSelectAward(string iname, Network.ResponseCallback response)
        {
            StringBuilder builder;
            base..ctor();
            base.name = "award/select";
            builder = WebAPI.GetStringBuilder();
            builder.Append("\"selected_award\":\"");
            builder.Append(iname);
            builder.Append("\"");
            base.body = WebAPI.GetRequestString(builder.ToString());
            base.callback = response;
            return;
        }
    }
}

