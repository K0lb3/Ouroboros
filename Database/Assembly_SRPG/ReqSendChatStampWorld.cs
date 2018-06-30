namespace SRPG
{
    using System;
    using System.Text;

    public class ReqSendChatStampWorld : WebAPI
    {
        public unsafe ReqSendChatStampWorld(int channel, int stamp_id, Network.ResponseCallback response)
        {
            StringBuilder builder;
            base..ctor();
            builder = WebAPI.GetStringBuilder();
            base.name = "chat/send/stamp";
            builder.Append("\"channel\":" + &channel.ToString() + ",");
            builder.Append("\"stamp_id\":" + &stamp_id.ToString());
            base.body = WebAPI.GetRequestString(builder.ToString());
            base.callback = response;
            return;
        }
    }
}

