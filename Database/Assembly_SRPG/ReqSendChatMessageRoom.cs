namespace SRPG
{
    using System;
    using System.Text;

    public class ReqSendChatMessageRoom : WebAPI
    {
        public ReqSendChatMessageRoom(string room_token, string message, string[] uids, Network.ResponseCallback response)
        {
            StringBuilder builder;
            int num;
            base..ctor();
            builder = WebAPI.GetStringBuilder();
            base.name = "chat/room/send";
            builder.Append("\"room_token\":\"" + room_token + "\",");
            builder.Append("\"message\":\"" + message + "\",");
            builder.Append("\"uids\":[");
            num = 0;
            goto Label_008C;
        Label_0058:
            builder.Append("\"" + uids[num] + "\"");
            if (num == (((int) uids.Length) - 1))
            {
                goto Label_0088;
            }
            builder.Append(",");
        Label_0088:
            num += 1;
        Label_008C:
            if (num < ((int) uids.Length))
            {
                goto Label_0058;
            }
            builder.Append("]");
            base.body = WebAPI.GetRequestString(builder.ToString());
            base.callback = response;
            return;
        }
    }
}

