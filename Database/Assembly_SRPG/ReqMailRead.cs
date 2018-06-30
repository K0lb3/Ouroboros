namespace SRPG
{
    using System;

    public class ReqMailRead : WebAPI
    {
        public unsafe ReqMailRead(long[] mailids, bool period, int page, Network.ResponseCallback response)
        {
            int num;
            base..ctor();
            base.name = "mail/read";
            base.body = "\"mailids\":[";
            num = 0;
            goto Label_0065;
        Label_0023:
            base.body = base.body + &(mailids[num]).ToString();
            if (num == (((int) mailids.Length) - 1))
            {
                goto Label_0061;
            }
            base.body = base.body + ",";
        Label_0061:
            num += 1;
        Label_0065:
            if (num < ((int) mailids.Length))
            {
                goto Label_0023;
            }
            base.body = base.body + "],";
            base.body = base.body + "\"page\":";
            base.body = base.body + ((int) page);
            base.body = base.body + ",";
            base.body = base.body + "\"period\":";
            base.body = base.body + ((int) ((period == null) ? 0 : 1));
            base.body = WebAPI.GetRequestString(base.body);
            base.callback = response;
            return;
        }

        public unsafe ReqMailRead(long[] mailids, int[] periods, int page, Network.ResponseCallback response)
        {
            int num;
            base..ctor();
            base.name = "mail/read";
            base.body = "\"mailids\":[";
            num = 0;
            goto Label_0065;
        Label_0023:
            base.body = base.body + &(mailids[num]).ToString();
            if (num == (((int) mailids.Length) - 1))
            {
                goto Label_0061;
            }
            base.body = base.body + ",";
        Label_0061:
            num += 1;
        Label_0065:
            if (num < ((int) mailids.Length))
            {
                goto Label_0023;
            }
            base.body = base.body + "],";
            base.body = base.body + "\"page\":";
            base.body = base.body + ((int) page);
            base.body = base.body + ",";
            base.body = base.body + "\"period\":";
            base.body = base.body + ((int) periods[0]);
            base.body = WebAPI.GetRequestString(base.body);
            base.callback = response;
            return;
        }

        public unsafe ReqMailRead(long mailid, bool period, int page, string iname, Network.ResponseCallback response)
        {
            base..ctor();
            base.name = "mail/read";
            base.body = "\"mailids\":[";
            base.body = base.body + &mailid.ToString();
            base.body = base.body + "],";
            base.body = base.body + "\"page\":";
            base.body = base.body + ((int) page);
            base.body = base.body + ",";
            base.body = base.body + "\"period\":";
            base.body = base.body + ((int) ((period == null) ? 0 : 1));
            base.body = base.body + ",";
            base.body = base.body + "\"selected\":\"";
            base.body = base.body + iname;
            base.body = base.body + "\"";
            base.body = WebAPI.GetRequestString(base.body);
            base.callback = response;
            return;
        }
    }
}

