namespace SRPG
{
    using System;

    public class RequestAPI : WebAPI
    {
        public RequestAPI(string url, Network.ResponseCallback response, string text)
        {
            base..ctor();
            base.name = url;
            base.body = WebAPI.GetRequestString(text);
            base.callback = response;
            return;
        }
    }
}

