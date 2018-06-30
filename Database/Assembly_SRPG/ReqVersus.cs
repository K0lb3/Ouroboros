namespace SRPG
{
    using System;
    using System.Runtime.InteropServices;
    using System.Text;

    public class ReqVersus : WebAPI
    {
        public ReqVersus(string iname, int plid, int seat, string uid, VersusStatusData param, int num, Network.ResponseCallback response, VERSUS_TYPE type, int draft_id, int enemy_draft_id)
        {
            StringBuilder builder;
            base..ctor();
            builder = WebAPI.GetStringBuilder();
            base.name = "vs/" + ((VERSUS_TYPE) type).ToString().ToLower() + "match/req";
            builder.Append("\"iname\":\"");
            builder.Append(JsonEscape.Escape(iname));
            builder.Append("\",");
            builder.Append("\"token\":\"");
            builder.Append(JsonEscape.Escape(GlobalVars.SelectedMultiPlayRoomName));
            builder.Append("\",");
            builder.Append("\"plid\":\"");
            builder.Append(plid);
            builder.Append("\",");
            builder.Append("\"seat\":\"");
            builder.Append(seat);
            builder.Append("\",");
            builder.Append("\"uid\":\"");
            builder.Append(uid);
            builder.Append("\"");
            builder.Append(",");
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
            if (draft_id <= 0)
            {
                goto Label_02E8;
            }
            builder.Append(",\"draft_id\":" + ((int) draft_id));
            builder.Append(",\"enemy_draft_id\":" + ((int) enemy_draft_id));
        Label_02E8:
            base.body = WebAPI.GetRequestString(builder.ToString());
            base.callback = response;
            return;
        }
    }
}

