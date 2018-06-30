namespace SRPG
{
    using System;

    [Serializable]
    public class JSON_TowerScoreThreshold
    {
        public int score;
        public string rank;
        public int turn;
        public int died;
        public int retire;
        public int recover;

        public JSON_TowerScoreThreshold()
        {
            base..ctor();
            return;
        }
    }
}

