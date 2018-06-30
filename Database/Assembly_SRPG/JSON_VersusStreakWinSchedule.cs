namespace SRPG
{
    using System;

    [Serializable]
    public class JSON_VersusStreakWinSchedule
    {
        public int id;
        public int judge;
        public string begin_at;
        public string end_at;

        public JSON_VersusStreakWinSchedule()
        {
            base..ctor();
            return;
        }
    }
}

