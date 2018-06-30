namespace SRPG
{
    using System;

    public class ReqChatMessageOffical : WebAPI
    {
        public ReqChatMessageOffical(Network.ResponseCallback response)
        {
            base..ctor();
            base.name = "chat/message/offical";
            base.body = WebAPI.GetRequestString(null);
            base.callback = response;
            return;
        }
    }
}

