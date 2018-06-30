namespace SRPG
{
    using System;
    using System.Text;

    public class ReqJobRankupAll : WebAPI
    {
        public ReqJobRankupAll(long iid_unit, string iname_jobset, bool is_cmn, Network.ResponseCallback response)
        {
            StringBuilder builder;
            base..ctor();
            base.name = "unit/job/equip/lvupall";
            builder = WebAPI.GetStringBuilder();
            builder.Append("\"uiid\":");
            builder.Append(iid_unit);
            builder.Append(",\"jobset\":\"");
            builder.Append(iname_jobset);
            builder.Append("\"");
            builder.Append(",\"is_cmn\":");
            builder.Append((is_cmn == null) ? 0 : 1);
            base.body = WebAPI.GetRequestString(builder.ToString());
            base.callback = response;
            return;
        }

        public ReqJobRankupAll(long iid_unit, string iname_jobset, bool is_cmn, int current_rank, int target_rank, int isEquips, Network.ResponseCallback response)
        {
            StringBuilder builder;
            base..ctor();
            base.name = "unit/job/equip/lvupall";
            builder = WebAPI.GetStringBuilder();
            builder.Append("\"uiid\":");
            builder.Append(iid_unit);
            builder.Append(",\"jobset\":\"");
            builder.Append(iname_jobset);
            builder.Append("\"");
            builder.Append(",");
            builder.Append("\"is_cmn\":");
            builder.Append((is_cmn == null) ? 0 : 1);
            builder.Append(",");
            builder.Append("\"current_rank\":");
            builder.Append(current_rank);
            builder.Append(",");
            builder.Append("\"target_rank\":");
            builder.Append(target_rank);
            if (isEquips != 1)
            {
                goto Label_00E2;
            }
            builder.Append(",");
            builder.Append("\"isEquips\":");
            builder.Append(isEquips);
        Label_00E2:
            base.body = WebAPI.GetRequestString(builder.ToString());
            base.callback = response;
            return;
        }
    }
}

