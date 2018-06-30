namespace SRPG
{
    using System;

    internal class TrophyObjectiveData
    {
        public TrophyObjective Objective;
        public string Description;
        public int Count;
        public int CountMax;

        public TrophyObjectiveData()
        {
            base..ctor();
            return;
        }
    }
}

