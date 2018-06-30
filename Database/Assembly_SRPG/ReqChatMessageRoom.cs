namespace SRPG
{
    using System;
    using System.Text;

    public class ReqChatMessageRoom : WebAPI
    {
        public unsafe ReqChatMessageRoom(long start_id, string room_token, int limit, long exclude_id, bool isMultiPush, Network.ResponseCallback response)
        {
            StringBuilder builder;
            base..ctor();
            builder = WebAPI.GetStringBuilder();
            base.name = "chat/room/message";
            builder.Append("\"start_id\":" + &start_id.ToString() + ",");
            if (string.IsNullOrEmpty(room_token) != null)
            {
                goto Label_0056;
            }
            builder.Append("\"room_token\":\"" + room_token + "\",");
        Label_0056:
            builder.Append("\"limit\":" + &limit.ToString() + ",");
            builder.Append("\"exclude_id\":" + &exclude_id.ToString());
            if (isMultiPush == null)
            {
                goto Label_009E;
            }
            builder.Append(",\"is_multi_push\":1");
        Label_009E:
            base.body = WebAPI.GetRequestString(builder.ToString());
            base.callback = response;
            return;
        }
    }
}

