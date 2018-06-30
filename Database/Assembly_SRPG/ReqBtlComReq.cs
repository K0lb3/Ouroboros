namespace SRPG
{
    using System;
    using System.Runtime.InteropServices;
    using System.Text;
    using UnityEngine;

    public class ReqBtlComReq : WebAPI
    {
        public unsafe ReqBtlComReq(string iname, string fuid, SupportData support, Network.ResponseCallback response, bool multi, int partyIndex, bool isHost, int plid, int seat, Vector2 location, RankingQuestParam rankingQuestParam)
        {
            StringBuilder builder;
            base..ctor();
            builder = WebAPI.GetStringBuilder();
            base.name = (multi == null) ? "btl/com/req" : "btl/multi/req";
            builder.Append("\"iname\":\"");
            builder.Append(iname);
            builder.Append("\",");
            if (partyIndex < 0)
            {
                goto Label_0071;
            }
            builder.Append("\"partyid\":");
            builder.Append(partyIndex);
            builder.Append(",");
        Label_0071:
            if (multi == null)
            {
                goto Label_011D;
            }
            builder.Append("\"token\":\"");
            builder.Append(JsonEscape.Escape(GlobalVars.SelectedMultiPlayRoomName));
            builder.Append("\",");
            builder.Append("\"host\":\"");
            builder.Append((isHost == null) ? "0" : "1");
            builder.Append("\",");
            builder.Append("\"plid\":\"");
            builder.Append(plid);
            builder.Append("\",");
            builder.Append("\"seat\":\"");
            builder.Append(seat);
            builder.Append("\",");
            goto Label_0141;
        Label_011D:
            builder.Append("\"req_at\":");
            builder.Append(Network.GetServerTime());
            builder.Append(",");
        Label_0141:
            builder.Append("\"btlparam\":{\"help\":{\"fuid\":");
            builder.Append("\"" + fuid + "\"");
            if (support == null)
            {
                goto Label_01B7;
            }
            if (support.Unit == null)
            {
                goto Label_01B7;
            }
            builder.Append(",\"elem\":" + ((int) support.Unit.SupportElement));
            builder.Append(",\"iname\":\"" + support.Unit.UnitID + "\"");
        Label_01B7:
            builder.Append("}");
            if (multi != null)
            {
                goto Label_0229;
            }
            if (rankingQuestParam == null)
            {
                goto Label_0229;
            }
            builder.Append(",\"quest_ranking\":{");
            builder.Append("\"schedule_id\":");
            builder.Append(rankingQuestParam.schedule_id);
            builder.Append(",");
            builder.Append("\"type\":");
            builder.Append(rankingQuestParam.type);
            builder.Append("}");
        Label_0229:
            builder.Append("},");
            builder.Append("\"location\":{");
            builder.Append("\"lat\":" + ((float) &location.x) + ",");
            builder.Append("\"lng\":" + ((float) &location.y));
            builder.Append("}");
            DebugMenu.Log("APIReq", builder.ToString());
            base.body = WebAPI.GetRequestString(builder.ToString());
            base.callback = response;
            return;
        }
    }
}

