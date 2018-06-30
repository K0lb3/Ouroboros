﻿namespace SRPG
{
    using System;

    public class ReqItemComposit : WebAPI
    {
        public ReqItemComposit(string iname, bool is_cmn, Network.ResponseCallback response)
        {
            object[] objArray1;
            int num;
            base..ctor();
            base.name = "item/gousei";
            num = (is_cmn == null) ? 0 : 1;
            objArray1 = new object[] { "\"iname\":\"", iname, "\",\"is_cmn\":", (int) num };
            base.body = WebAPI.GetRequestString(string.Concat(objArray1));
            base.callback = response;
            return;
        }
    }
}

