namespace SRPG
{
    using System;

    public class ReqChatChannelAutoAssign : WebAPI
    {
        public ReqChatChannelAutoAssign(Network.ResponseCallback response)
        {
            base..ctor();
            base.name = "chat/channel/auto";
            base.body = WebAPI.GetRequestString(null);
            base.callback = response;
            return;
        }
    }
}

