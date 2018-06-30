namespace SRPG
{
    using System;
    using System.Text;

    public class ReqHealAp : WebAPI
    {
        public ReqHealAp(long iid, int num, Network.ResponseCallback response)
        {
            StringBuilder builder;
            base..ctor();
            base.name = "item/addstm";
            builder = WebAPI.GetStringBuilder();
            builder.Append("\"iid\" : ");
            builder.Append(iid);
            builder.Append(",");
            builder.Append("\"num\" : ");
            builder.Append(num);
            base.body = WebAPI.GetRequestString(builder.ToString());
            base.callback = response;
            return;
        }
    }
}

