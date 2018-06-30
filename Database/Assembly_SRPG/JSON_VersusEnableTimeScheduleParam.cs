namespace SRPG
{
    using System;

    [Serializable]
    public class JSON_VersusEnableTimeScheduleParam
    {
        public string begin_time;
        public string open_time;
        public string quest_iname;
        public string[] add_date;

        public JSON_VersusEnableTimeScheduleParam()
        {
            base..ctor();
            return;
        }
    }
}

