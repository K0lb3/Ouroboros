namespace SRPG
{
    using System;
    using System.Text;

    public class ReqChatMessage : WebAPI
    {
        public unsafe ReqChatMessage(long start_id, int channel, int limit, long exclude_id, bool isMultiPush, Network.ResponseCallback response)
        {
            StringBuilder builder;
            base..ctor();
            builder = WebAPI.GetStringBuilder();
            base.name = "chat/message";
            builder.Append("\"start_id\":" + &start_id.ToString() + ",");
            builder.Append("\"channel\":" + &channel.ToString() + ",");
            builder.Append("\"limit\":" + &limit.ToString() + ",");
            builder.Append("\"exclude_id\":" + &exclude_id.ToString());
            if (isMultiPush == null)
            {
                goto Label_0099;
            }
            builder.Append(",\"is_multi_push\":1");
        Label_0099:
            base.body = WebAPI.GetRequestString(builder.ToString());
            base.callback = response;
            return;
        }
    }
}

