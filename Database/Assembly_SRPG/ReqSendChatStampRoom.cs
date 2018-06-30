namespace SRPG
{
    using System;
    using System.Text;

    public class ReqSendChatStampRoom : WebAPI
    {
        public unsafe ReqSendChatStampRoom(string room_token, int stamp_id, string[] uids, Network.ResponseCallback response)
        {
            StringBuilder builder;
            int num;
            base..ctor();
            builder = WebAPI.GetStringBuilder();
            base.name = "chat/room/send/stamp";
            builder.Append("\"room_token\":\"" + room_token + "\",");
            builder.Append("\"stamp_id\":" + &stamp_id.ToString() + ",");
            builder.Append("\"uids\":[");
            num = 0;
            goto Label_0092;
        Label_005E:
            builder.Append("\"" + uids[num] + "\"");
            if (num == (((int) uids.Length) - 1))
            {
                goto Label_008E;
            }
            builder.Append(",");
        Label_008E:
            num += 1;
        Label_0092:
            if (num < ((int) uids.Length))
            {
                goto Label_005E;
            }
            builder.Append("]");
            base.body = WebAPI.GetRequestString(builder.ToString());
            base.callback = response;
            return;
        }
    }
}

