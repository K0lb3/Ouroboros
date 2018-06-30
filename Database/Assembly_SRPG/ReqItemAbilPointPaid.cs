namespace SRPG
{
    using System;
    using System.Text;

    public class ReqItemAbilPointPaid : WebAPI
    {
        public ReqItemAbilPointPaid(Network.ResponseCallback response)
        {
            base..ctor();
            base.name = "item/addappaid";
            base.body = WebAPI.GetRequestString(null);
            base.callback = response;
            return;
        }

        public ReqItemAbilPointPaid(int value, Network.ResponseCallback response)
        {
            StringBuilder builder;
            base..ctor();
            builder = WebAPI.GetStringBuilder();
            base.name = "item/addappaid";
            builder.Append("\"val\" : ");
            builder.Append(value);
            base.body = WebAPI.GetRequestString(builder.ToString());
            base.callback = response;
            return;
        }
    }
}

