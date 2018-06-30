namespace SRPG
{
    using System;

    public class ReqBtlComCont : WebAPI
    {
        public ReqBtlComCont(long btlid, BattleCore.Record record, Network.ResponseCallback response, bool multi, bool isMT)
        {
            string str;
            base..ctor();
            if (isMT == null)
            {
                goto Label_001D;
            }
            base.name = "btl/multi/tower/cont";
            goto Label_0039;
        Label_001D:
            base.name = (multi == null) ? "btl/com/cont" : "btl/multi/cont";
        Label_0039:
            if (record == null)
            {
                goto Label_009D;
            }
            base.body = "\"btlid\":" + ((long) btlid) + ",";
            if (string.IsNullOrEmpty(WebAPI.GetBtlEndParamString(record, multi)) != null)
            {
                goto Label_0087;
            }
            base.body = base.body + WebAPI.GetBtlEndParamString(record, multi);
        Label_0087:
            base.body = WebAPI.GetRequestString(base.body);
            goto Label_00BD;
        Label_009D:
            base.body = WebAPI.GetRequestString("\"btlid\":\"" + ((long) btlid) + "\"");
        Label_00BD:
            base.callback = response;
            return;
        }
    }
}

