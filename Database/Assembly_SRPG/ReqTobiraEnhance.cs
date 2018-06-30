namespace SRPG
{
    using System;
    using System.Text;

    public class ReqTobiraEnhance : WebAPI
    {
        public ReqTobiraEnhance(long unit_iid, TobiraParam.Category category, Network.ResponseCallback response)
        {
            StringBuilder builder;
            base..ctor();
            base.name = "unit/door/lvup";
            builder = WebAPI.GetStringBuilder();
            builder.Append("\"unit_iid\":");
            builder.Append(unit_iid);
            builder.Append(",");
            builder.Append("\"category\":");
            builder.Append(category);
            base.body = WebAPI.GetRequestString(builder.ToString());
            base.callback = response;
            return;
        }
    }
}

