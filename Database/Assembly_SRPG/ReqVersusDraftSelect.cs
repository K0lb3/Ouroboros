namespace SRPG
{
    using GR;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class ReqVersusDraftSelect : WebAPI
    {
        public ReqVersusDraftSelect(string token, Network.ResponseCallback response)
        {
            GameManager manager;
            List<RequestDraftUnit> list;
            List<VersusDraftUnitParam> list2;
            VersusDraftUnitParam param;
            RequestDraftUnit unit;
            RequestParam param2;
            <ReqVersusDraftSelect>c__AnonStorey40E storeye;
            base..ctor();
            manager = MonoSingleton<GameManager>.Instance;
            base.name = "vs/draft/select";
            list = new List<RequestDraftUnit>();
            list2 = manager.GetVersusDraftUnits(manager.VSDraftId);
            storeye = new <ReqVersusDraftSelect>c__AnonStorey40E();
            storeye.i = 0;
            goto Label_0097;
        Label_003E:
            param = list2.Find(new Predicate<VersusDraftUnitParam>(storeye.<>m__4D4));
            unit = new RequestDraftUnit();
            unit.id = param.DraftUnitId;
            unit.secret = (param.IsHidden == null) ? 0 : 1;
            list.Add(unit);
            storeye.i += 1;
        Label_0097:
            if (storeye.i < VersusDraftList.VersusDraftUnitDataListPlayer.Count)
            {
                goto Label_003E;
            }
            param2 = new RequestParam();
            param2.token = token;
            param2.draft_result = new RequestDraftResult();
            param2.draft_result.turn_own = (VersusDraftList.VersusDraftTurnOwn == null) ? 0 : 1;
            param2.draft_result.draft_units = list.ToArray();
            base.body = WebAPI.GetRequestString<RequestParam>(param2);
            base.callback = response;
            return;
        }

        [CompilerGenerated]
        private sealed class <ReqVersusDraftSelect>c__AnonStorey40E
        {
            internal int i;

            public <ReqVersusDraftSelect>c__AnonStorey40E()
            {
                base..ctor();
                return;
            }

            internal bool <>m__4D4(VersusDraftUnitParam vdup)
            {
                return (vdup.DraftUnitId == VersusDraftList.VersusDraftUnitDataListPlayer[this.i].UniqueID);
            }
        }

        [Serializable]
        public class RequestDraftResult
        {
            public int turn_own;
            public ReqVersusDraftSelect.RequestDraftUnit[] draft_units;

            public RequestDraftResult()
            {
                base..ctor();
                return;
            }
        }

        [Serializable]
        public class RequestDraftUnit
        {
            public long id;
            public int secret;

            public RequestDraftUnit()
            {
                base..ctor();
                return;
            }
        }

        [Serializable]
        public class RequestParam
        {
            public string token;
            public ReqVersusDraftSelect.RequestDraftResult draft_result;

            public RequestParam()
            {
                base..ctor();
                return;
            }
        }

        public class Response
        {
            public int draft_id;

            public Response()
            {
                base..ctor();
                return;
            }
        }
    }
}

