namespace SRPG
{
    using System;
    using System.Text;

    public class ReqChatBlackList : WebAPI
    {
        public unsafe ReqChatBlackList(int offset, int limit, Network.ResponseCallback response)
        {
            StringBuilder builder;
            base..ctor();
            builder = WebAPI.GetStringBuilder();
            base.name = "chat/blacklist";
            builder.Append("\"offset\":" + &offset.ToString() + ",");
            builder.Append("\"limit\":" + &limit.ToString());
            base.body = WebAPI.GetRequestString(builder.ToString());
            base.callback = response;
            return;
        }

        public unsafe ReqChatBlackList(int start_id, int limit, int exclude_id, Network.ResponseCallback response)
        {
            StringBuilder builder;
            base..ctor();
            builder = WebAPI.GetStringBuilder();
            base.name = "chat/blacklist";
            builder.Append("\"start_id\":" + &start_id.ToString() + ",");
            builder.Append("\"limit\":" + &limit.ToString() + ",");
            builder.Append("\"exclude_id\":" + &exclude_id.ToString());
            base.body = WebAPI.GetRequestString(builder.ToString());
            base.callback = response;
            return;
        }
    }
}

