namespace SRPG
{
    using System;
    using System.Text;

    public class ReqUnitJob : WebAPI
    {
        public ReqUnitJob(long iid_unit, long iid_job, Network.ResponseCallback response)
        {
            base..ctor();
            this.Setup(iid_unit, iid_job, null, response);
            return;
        }

        public ReqUnitJob(long iid_unit, long iid_job, string ptype, Network.ResponseCallback response)
        {
            base..ctor();
            this.Setup(iid_unit, iid_job, ptype, response);
            return;
        }

        private void Setup(long iid_unit, long iid_job, string ptype, Network.ResponseCallback response)
        {
            StringBuilder builder;
            base.name = "unit/job/set";
            builder = WebAPI.GetStringBuilder();
            builder.Append("\"iid\":");
            builder.Append(iid_unit);
            builder.Append(",\"iid_job\":");
            builder.Append(iid_job);
            if (string.IsNullOrEmpty(ptype) != null)
            {
                goto Label_0061;
            }
            builder.Append(",\"type\":\"");
            builder.Append(ptype);
            builder.Append(0x22);
        Label_0061:
            base.body = WebAPI.GetRequestString(builder.ToString());
            base.callback = response;
            return;
        }
    }
}

