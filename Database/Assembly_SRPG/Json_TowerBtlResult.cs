namespace SRPG
{
    using System;

    public class Json_TowerBtlResult : Json_PlayerDataAll
    {
        public JSON_ReqTowerResuponse.Json_TowerPlayerUnit[] pdeck;
        public Json_Artifact[] artis;
        public int arrived_num;
        public int clear;
        public Json_TowerBtlEndRank ranking;

        public Json_TowerBtlResult()
        {
            base..ctor();
            return;
        }
    }
}

