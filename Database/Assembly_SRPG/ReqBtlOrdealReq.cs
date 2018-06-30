namespace SRPG
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class ReqBtlOrdealReq : WebAPI
    {
        public ReqBtlOrdealReq(string iname, List<SupportData> supports, Network.ResponseCallback response)
        {
            StringBuilder builder;
            int num;
            SupportData data;
            base..ctor();
            builder = WebAPI.GetStringBuilder();
            base.name = "btl/ordeal/req";
            builder.Append("\"iname\":\"");
            builder.Append(iname);
            builder.Append("\",");
            builder.Append("\"req_at\":");
            builder.Append(Network.GetServerTime());
            builder.Append(",");
            builder.Append("\"btlparam\":{\"helps\":[");
            num = 0;
            goto Label_0131;
        Label_006E:
            if (supports != null)
            {
                goto Label_0079;
            }
            goto Label_012D;
        Label_0079:
            if (num <= 0)
            {
                goto Label_008C;
            }
            builder.Append(",");
        Label_008C:
            data = supports[num];
            if (data != null)
            {
                goto Label_00AB;
            }
            builder.Append("{}");
            goto Label_012D;
        Label_00AB:
            builder.Append("{");
            builder.Append("\"fuid\":");
            builder.Append("\"" + data.FUID + "\"");
            builder.Append(",\"elem\":" + ((int) data.Unit.SupportElement));
            builder.Append(",\"iname\":\"" + data.Unit.UnitID + "\"");
            builder.Append("}");
        Label_012D:
            num += 1;
        Label_0131:
            if (num < supports.Count)
            {
                goto Label_006E;
            }
            builder.Append("]");
            builder.Append("}");
            base.body = WebAPI.GetRequestString(builder.ToString());
            base.callback = response;
            return;
        }
    }
}

