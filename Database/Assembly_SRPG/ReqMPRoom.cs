namespace SRPG
{
    using System;

    public class ReqMPRoom : WebAPI
    {
        public ReqMPRoom(string fuid, string iname, Network.ResponseCallback response)
        {
            base..ctor();
            base.name = "btl/room";
            base.body = string.Empty;
            if (string.IsNullOrEmpty(fuid) != null)
            {
                goto Label_0048;
            }
            base.body = base.body + "\"fuid\":\"" + JsonEscape.Escape(fuid) + "\"";
        Label_0048:
            if (string.IsNullOrEmpty(iname) != null)
            {
                goto Label_009A;
            }
            if (string.IsNullOrEmpty(base.body) != null)
            {
                goto Label_0079;
            }
            base.body = base.body + ",";
        Label_0079:
            base.body = base.body + "\"iname\":\"" + JsonEscape.Escape(iname) + "\"";
        Label_009A:
            base.body = WebAPI.GetRequestString(base.body);
            base.callback = response;
            return;
        }

        public class Response
        {
            public MultiPlayAPIRoom[] rooms;

            public Response()
            {
                base..ctor();
                return;
            }
        }
    }
}

