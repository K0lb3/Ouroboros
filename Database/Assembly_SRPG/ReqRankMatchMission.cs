namespace SRPG
{
    using System;

    public class ReqRankMatchMission : WebAPI
    {
        public ReqRankMatchMission(Network.ResponseCallback response)
        {
            base..ctor();
            base.name = "vs/rankmatch/mission";
            base.body = WebAPI.GetRequestString(string.Empty);
            base.callback = response;
            return;
        }

        [Serializable]
        public class MissionProgress
        {
            public string iname;
            public int prog;
            public string rewarded_at;

            public MissionProgress()
            {
                base..ctor();
                return;
            }
        }

        [Serializable]
        public class Response
        {
            public ReqRankMatchMission.MissionProgress[] missionprogs;

            public Response()
            {
                base..ctor();
                return;
            }
        }
    }
}

