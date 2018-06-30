namespace SRPG
{
    using System;

    public class ReqHikkoshiExec : WebAPI
    {
        public ReqHikkoshiExec(string token, Network.ResponseCallback response)
        {
            base..ctor();
            base.name = "hikkoshi/exec";
            base.body = WebAPI.GetRequestString("\"token\":\"" + token + "\"");
            base.callback = response;
            return;
        }
    }
}

