namespace SRPG
{
    using System;

    public class TimeParser
    {
        private string str_time;
        private DateTime date_time;

        public TimeParser()
        {
            base..ctor();
            return;
        }

        public void Set(string str_time_at, DateTime base_time)
        {
            this.str_time = str_time_at;
            this.date_time = base_time;
            if (string.IsNullOrEmpty(this.str_time) != null)
            {
                goto Label_0054;
            }
        Label_001E:
            try
            {
                this.date_time = DateTime.Parse(this.str_time);
                goto Label_0054;
            }
            catch (Exception)
            {
            Label_0034:
                DebugUtility.LogWarning("Failed to parse date! [" + this.str_time + "]");
                goto Label_0054;
            }
        Label_0054:
            return;
        }

        public string StrTime
        {
            get
            {
                return this.str_time;
            }
        }

        public DateTime DateTimes
        {
            get
            {
                return this.date_time;
            }
        }
    }
}

