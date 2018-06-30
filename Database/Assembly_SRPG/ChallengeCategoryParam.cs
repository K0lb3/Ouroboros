namespace SRPG
{
    using System;

    public class ChallengeCategoryParam
    {
        public string iname;
        public TimeParser begin_at;
        public TimeParser end_at;
        public int prio;

        public ChallengeCategoryParam()
        {
            this.begin_at = new TimeParser();
            this.end_at = new TimeParser();
            base..ctor();
            return;
        }

        public bool Deserialize(JSON_ChallengeCategoryParam json)
        {
            if (json != null)
            {
                goto Label_0008;
            }
            return 0;
        Label_0008:
            this.iname = json.iname;
            this.begin_at.Set(json.begin_at, DateTime.MinValue);
            this.end_at.Set(json.end_at, DateTime.MaxValue);
            this.prio = json.prio;
            return 1;
        }

        public bool IsAvailablePeriod(DateTime now)
        {
            DateTime time;
            DateTime time2;
            time = this.begin_at.DateTimes;
            time2 = this.end_at.DateTimes;
            return (((now >= time) == null) ? 0 : (time2 >= now));
        }

        public int Priority
        {
            get
            {
                return this.prio;
            }
        }
    }
}

