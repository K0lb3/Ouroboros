namespace SRPG
{
    using System;

    public class ReqJobEquip : WebAPI
    {
        public ReqJobEquip(long iid_job, long id_equip, Network.ResponseCallback response)
        {
            base..ctor();
            base.name = "unit/job/equip/set";
            base.body = "\"iid\":" + ((long) iid_job) + ",";
            base.body = base.body + "\"id_equip\":" + ((long) id_equip);
            base.body = WebAPI.GetRequestString(base.body);
            base.callback = response;
            return;
        }
    }
}

