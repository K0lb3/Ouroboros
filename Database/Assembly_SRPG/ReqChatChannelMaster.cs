namespace SRPG
{
    using System;

    public class ReqChatChannelMaster : WebAPI
    {
        public ReqChatChannelMaster(Network.ResponseCallback response)
        {
            base..ctor();
            base.name = "chat/channel/master";
            base.body = WebAPI.GetRequestString(null);
            base.callback = response;
            return;
        }
    }
}

