namespace SRPG
{
    using System;

    public class ReqFriendRemove : WebAPI
    {
        public ReqFriendRemove(string[] fuids, Network.ResponseCallback response)
        {
            int num;
            base..ctor();
            base.name = "friend/remove";
            base.body = "\"fuids\":[";
            num = 0;
            goto Label_0066;
        Label_0023:
            base.body = base.body + "\"" + fuids[num] + "\"";
            if (num == (((int) fuids.Length) - 1))
            {
                goto Label_0062;
            }
            base.body = base.body + ",";
        Label_0062:
            num += 1;
        Label_0066:
            if (num < ((int) fuids.Length))
            {
                goto Label_0023;
            }
            base.body = base.body + "]";
            base.body = WebAPI.GetRequestString(base.body);
            base.callback = response;
            return;
        }
    }
}

