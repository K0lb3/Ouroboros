namespace SRPG
{
    using System;

    public class ReqRankMatch : WebAPI
    {
        public ReqRankMatch(string iname, int plid, int seat, string uid, Network.ResponseCallback response)
        {
            RequestParam param;
            base..ctor();
            base.name = "vs/rankmatch/req";
            param = new RequestParam();
            param.iname = iname;
            param.token = GlobalVars.SelectedMultiPlayRoomName;
            param.plid = plid;
            param.seat = seat;
            param.uid = uid;
            base.body = WebAPI.GetRequestString<RequestParam>(param);
            base.callback = response;
            return;
        }

        [Serializable]
        private class RequestParam
        {
            public string iname;
            public string token;
            public int plid;
            public int seat;
            public string uid;

            public RequestParam()
            {
                base..ctor();
                return;
            }
        }
    }
}

