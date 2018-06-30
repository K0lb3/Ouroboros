namespace SRPG
{
    using System;

    [Serializable]
    public class JSON_TowerScore
    {
        public string iname;
        public JSON_TowerScoreThreshold[] threshold_vals;

        public JSON_TowerScore()
        {
            base..ctor();
            return;
        }
    }
}

