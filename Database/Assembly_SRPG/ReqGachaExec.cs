namespace SRPG
{
    using System;
    using System.Runtime.InteropServices;

    public class ReqGachaExec : WebAPI
    {
        public ReqGachaExec(string gachaid, Network.ResponseCallback response)
        {
            base..ctor();
            base.name = "gacha/exec";
            base.body = WebAPI.GetRequestString("\"gachaid\":\"" + gachaid + "\"");
            base.callback = response;
            return;
        }

        public unsafe ReqGachaExec(string iname, Network.ResponseCallback response, int free, int num, int is_decision)
        {
            base..ctor();
            base.name = "gacha/exec";
            base.body = "\"gachaid\":\"" + iname + "\",";
            base.body = base.body + "\"free\":" + &free.ToString();
            if (num <= 0)
            {
                goto Label_0069;
            }
            base.body = base.body + ",\"ticketnum\":" + &num.ToString();
        Label_0069:
            if (is_decision == null)
            {
                goto Label_008D;
            }
            base.body = base.body + ",\"is_decision\":" + &is_decision.ToString();
        Label_008D:
            base.body = WebAPI.GetRequestString(base.body);
            base.callback = response;
            return;
        }
    }
}

