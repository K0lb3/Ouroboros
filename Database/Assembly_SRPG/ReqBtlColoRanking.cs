namespace SRPG
{
    using System;
    using System.Text;

    public class ReqBtlColoRanking : WebAPI
    {
        public ReqBtlColoRanking(RankingTypes type, Network.ResponseCallback response)
        {
            StringBuilder builder;
            base..ctor();
            base.name = "btl/colo/ranking/" + ((RankingTypes) type).ToString();
            builder = WebAPI.GetStringBuilder();
            base.body = WebAPI.GetRequestString(builder.ToString());
            base.callback = response;
            return;
        }

        public enum RankingTypes
        {
            world,
            friend
        }
    }
}

