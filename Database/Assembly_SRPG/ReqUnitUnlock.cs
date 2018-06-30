namespace SRPG
{
    using System;

    public class ReqUnitUnlock : WebAPI
    {
        public ReqUnitUnlock(string iname, Network.ResponseCallback response)
        {
            base..ctor();
            base.name = "unit/add";
            base.body = WebAPI.GetRequestString("\"iname\":\"" + iname + "\"");
            base.callback = response;
            return;
        }
    }
}

