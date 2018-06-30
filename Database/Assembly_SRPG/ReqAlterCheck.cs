namespace SRPG
{
    using System;
    using System.Text;

    public class ReqAlterCheck : WebAPI
    {
        public ReqAlterCheck(string hash, Network.ResponseCallback response)
        {
            StringBuilder builder;
            base..ctor();
            base.name = "master/md5";
            builder = WebAPI.GetStringBuilder();
            builder.Append("\"md5\":\"");
            builder.Append(hash);
            builder.Append("\"");
            base.body = WebAPI.GetRequestString(builder.ToString());
            base.callback = response;
            return;
        }
    }
}

