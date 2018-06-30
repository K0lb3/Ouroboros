namespace SRPG
{
    using System;
    using System.Text;

    public class ReqAddChatBlackList : WebAPI
    {
        public ReqAddChatBlackList(string target_uid, Network.ResponseCallback response)
        {
            StringBuilder builder;
            base..ctor();
            builder = WebAPI.GetStringBuilder();
            base.name = "chat/blacklist/add";
            builder.Append("\"target_uid\":\"" + target_uid + "\"");
            base.body = WebAPI.GetRequestString(builder.ToString());
            base.callback = response;
            return;
        }
    }
}

