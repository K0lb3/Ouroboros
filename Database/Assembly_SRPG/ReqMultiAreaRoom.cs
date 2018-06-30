namespace SRPG
{
    using System;
    using UnityEngine;

    public class ReqMultiAreaRoom : WebAPI
    {
        public unsafe ReqMultiAreaRoom(string fuid, string[] iname, Vector2 location, Network.ResponseCallback response)
        {
            object[] objArray2;
            object[] objArray1;
            int num;
            string str;
            base..ctor();
            base.name = "btl/room/areaquest";
            base.body = string.Empty;
            if (string.IsNullOrEmpty(fuid) != null)
            {
                goto Label_0048;
            }
            base.body = base.body + "\"fuid\":\"" + JsonEscape.Escape(fuid) + "\"";
        Label_0048:
            if (iname == null)
            {
                goto Label_00FC;
            }
            if (((int) iname.Length) <= 0)
            {
                goto Label_00FC;
            }
            if (string.IsNullOrEmpty(base.body) != null)
            {
                goto Label_007D;
            }
            base.body = base.body + ",";
        Label_007D:
            base.body = base.body + "\"iname\":[";
            num = 0;
            goto Label_00DD;
        Label_009A:
            if (num == null)
            {
                goto Label_00B6;
            }
            base.body = base.body + ",";
        Label_00B6:
            base.body = base.body + "\"" + JsonEscape.Escape(iname[num]) + "\"";
            num += 1;
        Label_00DD:
            if (num < ((int) iname.Length))
            {
                goto Label_009A;
            }
            base.body = base.body + "]";
        Label_00FC:
            if (string.IsNullOrEmpty(base.body) != null)
            {
                goto Label_0122;
            }
            base.body = base.body + ",";
        Label_0122:
            base.body = base.body + "\"location\":{";
            str = base.body;
            objArray1 = new object[] { str, "\"lat\":", (float) &location.x, "," };
            base.body = string.Concat(objArray1);
            str = base.body;
            objArray2 = new object[] { str, "\"lng\":", (float) &location.y, "}" };
            base.body = string.Concat(objArray2);
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

