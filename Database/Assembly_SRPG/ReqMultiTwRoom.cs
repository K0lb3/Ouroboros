namespace SRPG
{
    using System;

    public class ReqMultiTwRoom : WebAPI
    {
        public ReqMultiTwRoom(string fuid, string iname, int floor, Network.ResponseCallback response)
        {
            base..ctor();
            base.name = "btl/multi/tower/room";
            base.body = string.Empty;
            base.body = base.body + "\"iname\":\"" + JsonEscape.Escape(iname) + "\"";
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

