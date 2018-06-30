namespace SRPG
{
    using System;
    using System.Text;

    public class ReqChatUserProfile : WebAPI
    {
        public ReqChatUserProfile(string target_uid, Network.ResponseCallback response)
        {
            StringBuilder builder;
            base..ctor();
            builder = WebAPI.GetStringBuilder();
            base.name = "chat/profile";
            builder.Append("\"target_uid\":\"" + target_uid + "\"");
            base.body = WebAPI.GetRequestString(builder.ToString());
            base.callback = response;
            return;
        }
    }
}

