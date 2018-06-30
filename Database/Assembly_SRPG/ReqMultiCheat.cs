namespace SRPG
{
    using System;

    public class ReqMultiCheat : WebAPI
    {
        public ReqMultiCheat(int type, string val, Network.ResponseCallback response)
        {
            object[] objArray1;
            string str;
            base..ctor();
            base.name = "btl/multi/cheat";
            base.body = string.Empty;
            str = base.body;
            objArray1 = new object[] { str, "\"type\":", (int) type, "," };
            base.body = string.Concat(objArray1);
            base.body = base.body + "\"val\":\"" + JsonEscape.Escape(val) + "\"";
            base.body = WebAPI.GetRequestString(base.body);
            base.callback = response;
            return;
        }
    }
}

