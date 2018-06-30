namespace SRPG
{
    using System;
    using System.Runtime.InteropServices;

    public class ReqVersusMake : WebAPI
    {
        public ReqVersusMake(VERSUS_TYPE type, string comment, string iname, bool isLine, Network.ResponseCallback response)
        {
            base..ctor();
            base.name = "vs/" + ((VERSUS_TYPE) type).ToString().ToLower() + "match/make";
            base.body = string.Empty;
            base.body = base.body + "\"comment\":\"" + JsonEscape.Escape(comment) + "\",";
            base.body = base.body + "\"iname\":\"" + JsonEscape.Escape(iname) + "\",";
            base.body = base.body + "\"Line\":" + ((int) ((isLine == null) ? 0 : 1));
            base.body = base.body + ",\"is_draft\":" + ((int) ((GlobalVars.IsVersusDraftMode == null) ? 0 : 1));
            base.body = WebAPI.GetRequestString(base.body);
            base.callback = response;
            return;
        }

        public class Response
        {
            public string token;
            public string owner_name;
            public int roomid;

            public Response()
            {
                base..ctor();
                return;
            }
        }
    }
}

