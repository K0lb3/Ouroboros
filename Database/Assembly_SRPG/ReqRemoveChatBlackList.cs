namespace SRPG
{
    using System;
    using System.Text;

    public class ReqRemoveChatBlackList : WebAPI
    {
        public ReqRemoveChatBlackList(string target_uid, Network.ResponseCallback response)
        {
            StringBuilder builder;
            base..ctor();
            builder = WebAPI.GetStringBuilder();
            base.name = "chat/blacklist/remove";
            builder.Append("\"target_uid\":\"" + target_uid + "\"");
            base.body = WebAPI.GetRequestString(builder.ToString());
            base.callback = response;
            return;
        }
    }
}

