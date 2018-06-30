namespace SRPG
{
    using System;
    using System.Text;

    public class ReqBtlMultiTwReq : WebAPI
    {
        public ReqBtlMultiTwReq(string iname, int partyIndex, int plid, int seat, string[] uid, Network.ResponseCallback response)
        {
            StringBuilder builder;
            int num;
            base..ctor();
            builder = WebAPI.GetStringBuilder();
            base.name = "btl/multi/tower/req";
            builder.Append("\"iname\":\"");
            builder.Append(iname);
            builder.Append("\",");
            if (partyIndex < 0)
            {
                goto Label_005E;
            }
            builder.Append("\"partyid\":");
            builder.Append(partyIndex);
            builder.Append(",");
        Label_005E:
            builder.Append("\"token\":\"");
            builder.Append(JsonEscape.Escape(GlobalVars.SelectedMultiPlayRoomName));
            builder.Append("\",");
            builder.Append("\"plid\":\"");
            builder.Append(plid);
            builder.Append("\",");
            builder.Append("\"seat\":\"");
            builder.Append(seat);
            builder.Append("\",");
            builder.Append("\"uids\":[");
            num = 0;
            goto Label_0111;
        Label_00DB:
            builder.Append("\"" + uid[num] + "\"");
            if (num == (((int) uid.Length) - 1))
            {
                goto Label_010D;
            }
            builder.Append(",");
        Label_010D:
            num += 1;
        Label_0111:
            if (num < ((int) uid.Length))
            {
                goto Label_00DB;
            }
            builder.Append("]");
            base.body = WebAPI.GetRequestString(builder.ToString());
            base.callback = response;
            return;
        }
    }
}

