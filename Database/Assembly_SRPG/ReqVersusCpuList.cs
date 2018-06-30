namespace SRPG
{
    using System;
    using System.Text;

    public class ReqVersusCpuList : WebAPI
    {
        public ReqVersusCpuList(VersusStatusData param, int num, string quest_iname, Network.ResponseCallback response)
        {
            StringBuilder builder;
            base..ctor();
            base.name = "vs/com";
            builder = WebAPI.GetStringBuilder();
            builder.Append("\"status\":{");
            builder.Append("\"hp\":" + ((int) param.Hp) + ",");
            builder.Append("\"atk\":" + ((int) param.Atk) + ",");
            builder.Append("\"def\":" + ((int) param.Def) + ",");
            builder.Append("\"matk\":" + ((int) param.Matk) + ",");
            builder.Append("\"mdef\":" + ((int) param.Mdef) + ",");
            builder.Append("\"dex\":" + ((int) param.Dex) + ",");
            builder.Append("\"spd\":" + ((int) param.Spd) + ",");
            builder.Append("\"cri\":" + ((int) param.Cri) + ",");
            builder.Append("\"luck\":" + ((int) param.Luck) + ",");
            builder.Append("\"cmb\":" + ((int) param.Cmb) + ",");
            builder.Append("\"move\":" + ((int) param.Move) + ",");
            builder.Append("\"jmp\":" + ((int) param.Jmp));
            builder.Append("}");
            builder.Append(",\"member_count\":" + ((int) num));
            builder.Append(",\"iname\":\"");
            builder.Append(JsonEscape.Escape(quest_iname));
            builder.Append("\"");
            base.body = WebAPI.GetRequestString(builder.ToString());
            base.callback = response;
            return;
        }
    }
}

