namespace SRPG
{
    using System;
    using System.Text;

    public class ReqTutUpdate : WebAPI
    {
        public ReqTutUpdate(long flags, Network.ResponseCallback response)
        {
            StringBuilder builder;
            base..ctor();
            base.name = "tut/update";
            builder = WebAPI.GetStringBuilder();
            builder.Append("\"tut\":");
            builder.Append(flags);
            base.body = WebAPI.GetRequestString(builder.ToString());
            base.callback = response;
            return;
        }
    }
}

