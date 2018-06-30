namespace SRPG
{
    using System;

    public class TowerScoreParam
    {
        public string Rank;
        public OInt Score;
        public OInt TurnCnt;
        public OInt DiedCnt;
        public OInt RetireCnt;
        public OInt RecoverCnt;

        public TowerScoreParam()
        {
            base..ctor();
            return;
        }

        public bool Deserialize(JSON_TowerScoreThreshold json)
        {
            if (json != null)
            {
                goto Label_0008;
            }
            return 0;
        Label_0008:
            this.Rank = json.rank;
            this.Score = json.score;
            this.TurnCnt = json.turn;
            this.DiedCnt = json.died;
            this.RetireCnt = json.retire;
            this.RecoverCnt = json.recover;
            return 1;
        }
    }
}

