namespace SRPG
{
    using System;
    using System.Text;

    public class ReqJobEquipV2 : WebAPI
    {
        public ReqJobEquipV2(long iid_unit, string iname_jobset, long slot, bool is_cmn, Network.ResponseCallback response)
        {
            StringBuilder builder;
            base..ctor();
            base.name = "unit/job/equip/set2";
            builder = WebAPI.GetStringBuilder();
            builder.Append("\"iid\":");
            builder.Append(iid_unit);
            builder.Append(",\"iname\":\"");
            builder.Append(iname_jobset);
            builder.Append("\"");
            builder.Append(",\"slot\":");
            builder.Append(slot);
            builder.Append(",\"is_cmn\":");
            builder.Append((is_cmn == null) ? 0 : 1);
            base.body = WebAPI.GetRequestString(builder.ToString());
            base.callback = response;
            return;
        }
    }
}

