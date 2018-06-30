namespace SRPG
{
    using System;

    public class ReqRankMatchMissionExec : WebAPI
    {
        public ReqRankMatchMissionExec(string iname, Network.ResponseCallback response)
        {
            RequestParam param;
            base..ctor();
            base.name = "vs/rankmatch/mission/exec";
            param = new RequestParam();
            param.iname = iname;
            base.body = WebAPI.GetRequestString<RequestParam>(param);
            base.callback = response;
            return;
        }

        [Serializable]
        private class RequestParam
        {
            public string iname;

            public RequestParam()
            {
                base..ctor();
                return;
            }
        }
    }
}

