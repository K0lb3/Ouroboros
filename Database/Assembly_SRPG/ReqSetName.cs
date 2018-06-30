namespace SRPG
{
    using System;

    public class ReqSetName : WebAPI
    {
        public ReqSetName(string username, Network.ResponseCallback response)
        {
            base..ctor();
            username = WebAPI.EscapeString(username);
            base.name = "setname";
            base.body = WebAPI.GetRequestString("\"name\":\"" + username + "\"");
            base.callback = response;
            return;
        }
    }
}

