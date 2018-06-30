namespace SRPG
{
    using GR;
    using System;

    public class ReqPlayNew : WebAPI
    {
        public ReqPlayNew(bool isDebug, Network.ResponseCallback response)
        {
            string str;
            base..ctor();
            base.name = "playnew";
            base.body = string.Empty;
            str = string.Empty;
            if (isDebug == null)
            {
                goto Label_002E;
            }
            str = "\"debug\":1,";
        Label_002E:
            str = str + "\"permanent_id\":\"" + MonoSingleton<GameManager>.Instance.UdId + "\"";
            base.body = base.body + WebAPI.GetRequestString(str);
            base.callback = response;
            return;
        }
    }
}

