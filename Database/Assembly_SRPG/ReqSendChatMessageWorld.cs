namespace SRPG
{
    using System;
    using System.Text;

    public class ReqSendChatMessageWorld : WebAPI
    {
        public unsafe ReqSendChatMessageWorld(int channel, string message, Network.ResponseCallback response)
        {
            StringBuilder builder;
            base..ctor();
            builder = WebAPI.GetStringBuilder();
            base.name = "chat/send";
            builder.Append("\"channel\":" + &channel.ToString() + ",");
            builder.Append("\"message\":\"" + message + "\"");
            base.body = WebAPI.GetRequestString(builder.ToString());
            base.callback = response;
            return;
        }
    }
}

