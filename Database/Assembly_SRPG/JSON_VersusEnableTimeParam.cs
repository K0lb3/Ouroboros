namespace SRPG
{
    using System;

    [Serializable]
    public class JSON_VersusEnableTimeParam
    {
        public int id;
        public int mode;
        public string begin_at;
        public string end_at;
        public JSON_VersusEnableTimeScheduleParam[] schedule;
        public int draft_type;

        public JSON_VersusEnableTimeParam()
        {
            base..ctor();
            return;
        }
    }
}

