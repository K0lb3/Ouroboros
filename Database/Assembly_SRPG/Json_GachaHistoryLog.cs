namespace SRPG
{
    using System;

    public class Json_GachaHistoryLog
    {
        public string title;
        public Json_GachaHistoryItem[] drops;
        public long drop_at;

        public Json_GachaHistoryLog()
        {
            base..ctor();
            return;
        }
    }
}

