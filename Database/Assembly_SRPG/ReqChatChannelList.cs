namespace SRPG
{
    using System;
    using System.Text;

    public class ReqChatChannelList : WebAPI
    {
        public ReqChatChannelList(int[] channel_ids, Network.ResponseCallback response)
        {
            StringBuilder builder;
            int num;
            base..ctor();
            builder = WebAPI.GetStringBuilder();
            base.name = "chat/channel";
            builder.Append("\"channel_ids\":[");
            num = 0;
            goto Label_004F;
        Label_002A:
            builder.Append(channel_ids[num]);
            if (num == (((int) channel_ids.Length) - 1))
            {
                goto Label_004B;
            }
            builder.Append(",");
        Label_004B:
            num += 1;
        Label_004F:
            if (num < ((int) channel_ids.Length))
            {
                goto Label_002A;
            }
            builder.Append("]");
            base.body = WebAPI.GetRequestString(builder.ToString());
            base.callback = response;
            return;
        }

        public unsafe ReqChatChannelList(int start_id, int limit, int exclude_id, Network.ResponseCallback response)
        {
            StringBuilder builder;
            base..ctor();
            builder = WebAPI.GetStringBuilder();
            base.name = "chat/channel";
            builder.Append("\"start_id\":" + &start_id.ToString() + ",");
            builder.Append("\"limit\":" + &limit.ToString() + ",");
            builder.Append("\"exclude_id\":" + &exclude_id.ToString());
            base.body = WebAPI.GetRequestString(builder.ToString());
            base.callback = response;
            return;
        }
    }
}

