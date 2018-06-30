namespace SRPG
{
    using System;
    using System.Text;

    public class ReqTobiraUnlock : WebAPI
    {
        public ReqTobiraUnlock(long unit_iid, Network.ResponseCallback response)
        {
            StringBuilder builder;
            base..ctor();
            base.name = "unit/door/release";
            builder = WebAPI.GetStringBuilder();
            builder.Append("\"unit_iid\":");
            builder.Append(unit_iid);
            base.body = WebAPI.GetRequestString(builder.ToString());
            base.callback = response;
            return;
        }
    }
}

