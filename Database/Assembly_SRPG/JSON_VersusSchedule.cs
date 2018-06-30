namespace SRPG
{
    using System;

    [Serializable]
    public class JSON_VersusSchedule
    {
        public string tower_iname;
        public string iname;
        public string begin_at;
        public string end_at;
        public string gift_begin_at;
        public string gift_end_at;

        public JSON_VersusSchedule()
        {
            base..ctor();
            return;
        }
    }
}

