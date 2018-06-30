namespace SRPG
{
    using System;

    public class ReqJobAbility : WebAPI
    {
        public unsafe ReqJobAbility(long iid_job, long[] iid_abils, Network.ResponseCallback response)
        {
            int num;
            base..ctor();
            base.name = "unit/job/abil/set";
            base.body = "\"iid\":" + ((long) iid_job) + ",";
            base.body = base.body + "\"iid_abils\":";
            base.body = base.body + "[";
            num = 0;
            goto Label_00A1;
        Label_005F:
            base.body = base.body + &(iid_abils[num]).ToString();
            if (num == (((int) iid_abils.Length) - 1))
            {
                goto Label_009D;
            }
            base.body = base.body + ",";
        Label_009D:
            num += 1;
        Label_00A1:
            if (num < ((int) iid_abils.Length))
            {
                goto Label_005F;
            }
            base.body = base.body + "]";
            base.body = WebAPI.GetRequestString(base.body);
            base.callback = response;
            return;
        }
    }
}

