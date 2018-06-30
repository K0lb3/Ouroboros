namespace SRPG
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class ReqVersusDraftParty : WebAPI
    {
        public ReqVersusDraftParty(string token, int draft_id, Network.ResponseCallback response)
        {
            RequestParam param;
            List<int> list;
            int num;
            <ReqVersusDraftParty>c__AnonStorey40F storeyf;
            base..ctor();
            base.name = "vs/draft/party";
            param = new RequestParam();
            param.token = token;
            param.draft_id = draft_id;
            list = new List<int>();
            storeyf = new <ReqVersusDraftParty>c__AnonStorey40F();
            storeyf.i = 0;
            goto Label_0069;
        Label_003D:
            num = VersusDraftList.VersusDraftUnitDataListPlayer.FindIndex(new Predicate<UnitData>(storeyf.<>m__4D5));
            list.Add(num);
            storeyf.i += 1;
        Label_0069:
            if (storeyf.i < VersusDraftList.VersusDraftPartyUnits.Count)
            {
                goto Label_003D;
            }
            param.party_indexes = list.ToArray();
            base.body = WebAPI.GetRequestString<RequestParam>(param);
            base.callback = response;
            return;
        }

        [CompilerGenerated]
        private sealed class <ReqVersusDraftParty>c__AnonStorey40F
        {
            internal int i;

            public <ReqVersusDraftParty>c__AnonStorey40F()
            {
                base..ctor();
                return;
            }

            internal bool <>m__4D5(UnitData ud)
            {
                return (ud.UniqueID == VersusDraftList.VersusDraftPartyUnits[this.i].UniqueID);
            }
        }

        [Serializable]
        public class RequestParam
        {
            public string token;
            public int draft_id;
            public int[] party_indexes;

            public RequestParam()
            {
                base..ctor();
                return;
            }
        }
    }
}

