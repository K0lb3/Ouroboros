namespace SRPG
{
    using GR;
    using System;

    public class TrophyCategoryParam
    {
        public string iname;
        public int hash_code;
        public TrophyCategorys category;
        public bool is_not_pull;
        public int days;
        public int beginner;
        public TimeParser begin_at;
        public TimeParser end_at;
        public string linked_quest;

        public TrophyCategoryParam()
        {
            this.begin_at = new TimeParser();
            this.end_at = new TimeParser();
            base..ctor();
            return;
        }

        private unsafe DateTime AddTimeSpan(DateTime times, TimeSpan span)
        {
            if (&times.Equals(DateTime.MaxValue) == null)
            {
                goto Label_0013;
            }
            return times;
        Label_0013:
            try
            {
                times = &times.Add(span);
                goto Label_002F;
            }
            catch (Exception)
            {
            Label_0022:
                times = DateTime.MaxValue;
                goto Label_002F;
            }
        Label_002F:
            return times;
        }

        public bool Deserialize(JSON_TrophyCategoryParam json)
        {
            if (json != null)
            {
                goto Label_0008;
            }
            return 0;
        Label_0008:
            this.iname = json.iname;
            this.hash_code = this.iname.GetHashCode();
            this.is_not_pull = json.is_not_pull == 1;
            this.days = json.day_reset;
            this.beginner = json.bgnr;
            this.begin_at.Set(json.begin_at, DateTime.MinValue);
            this.end_at.Set(json.end_at, DateTime.MaxValue);
            if (json.category == null)
            {
                goto Label_0094;
            }
            this.category = json.category;
            goto Label_009B;
        Label_0094:
            this.category = 6;
        Label_009B:
            this.linked_quest = json.linked_quest;
            return 1;
        }

        private TimeSpan GetGraceRewardSpan()
        {
            return new TimeSpan(14, 0, 0, 0);
        }

        private TimeSpan GetQuestGrace()
        {
            if (this.IsDaily == null)
            {
                goto Label_0015;
            }
            return new TimeSpan(0, 0, 0, 0);
        Label_0015:
            return new TimeSpan(1, 0, 0, 0);
        }

        public DateTime GetQuestTime(DateTime base_time, bool is_quest_grace)
        {
            QuestParam param;
            DateTime time;
            if (this.IsLinekedQuest != null)
            {
                goto Label_000D;
            }
            return base_time;
        Label_000D:
            param = MonoSingleton<GameManager>.Instance.FindQuest(this.linked_quest);
            if (param != null)
            {
                goto Label_0026;
            }
            return base_time;
        Label_0026:
            time = (is_quest_grace == null) ? TimeManager.FromUnixTime(param.end) : this.AddTimeSpan(TimeManager.FromUnixTime(param.end), this.GetQuestGrace());
            return time;
        }

        public bool IsAvailablePeriod(DateTime now, bool is_grace)
        {
            DateTime time;
            DateTime time2;
            DateTime time3;
            time = this.begin_at.DateTimes;
            time2 = this.end_at.DateTimes;
            if (this.IsBeginner == null)
            {
                goto Label_0047;
            }
            time3 = MonoSingleton<GameManager>.Instance.Player.GetBeginnerEndTime();
            time2 = ((time2 <= time3) == null) ? time3 : time2;
        Label_0047:
            if (is_grace == null)
            {
                goto Label_005B;
            }
            time2 = this.AddTimeSpan(time2, this.GetGraceRewardSpan());
        Label_005B:
            return (((now >= time) == null) ? 0 : (time2 >= now));
        }

        public bool IsOpenLinekedQuest(DateTime now, bool is_grace)
        {
            QuestParam param;
            bool flag;
            DateTime time;
            DateTime time2;
            if (this.IsLinekedQuest != null)
            {
                goto Label_000D;
            }
            return 1;
        Label_000D:
            param = MonoSingleton<GameManager>.Instance.FindQuest(this.linked_quest);
            if (param != null)
            {
                goto Label_0026;
            }
            return 0;
        Label_0026:
            if ((MonoSingleton<GameManager>.Instance.Player.IsBeginner() != null) || (param.IsBeginner == null))
            {
                goto Label_0047;
            }
            return 0;
        Label_0047:
            flag = 0;
            if (param.IsJigen == null)
            {
                goto Label_00A7;
            }
            time = TimeManager.FromUnixTime(param.start);
            time2 = TimeManager.FromUnixTime(param.end);
            time2 = this.AddTimeSpan(time2, (is_grace == null) ? this.GetQuestGrace() : this.GetGraceRewardSpan());
            flag = ((time <= now) == null) ? 0 : (now < time2);
            goto Label_00B1;
        Label_00A7:
            flag = param.hidden == 0;
        Label_00B1:
            return flag;
        }

        public bool IsNotPull
        {
            get
            {
                return this.is_not_pull;
            }
        }

        public bool IsBeginner
        {
            get
            {
                return (this.beginner == 1);
            }
        }

        public bool IsDaily
        {
            get
            {
                return (this.days == 1);
            }
        }

        public bool IsLinekedQuest
        {
            get
            {
                return (string.IsNullOrEmpty(this.linked_quest) == 0);
            }
        }
    }
}

