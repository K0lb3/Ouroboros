namespace SRPG
{
    using System;

    public class ReqGallery : WebAPI
    {
        public ReqGallery(Network.ResponseCallback response)
        {
            base..ctor();
            base.name = "gallery";
            base.body = WebAPI.GetRequestString(null);
            base.callback = response;
            return;
        }
    }
}

