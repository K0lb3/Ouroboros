namespace SRPG
{
    using System;
    using UnityEngine;

    public class BannerParam
    {
        public string iname;
        public BannerType type;
        public string sval;
        public string banner;
        public string banr_sprite;
        public string begin_at;
        public string end_at;
        public int priority;
        public string message;
        private bool is_not_home;

        public BannerParam()
        {
            base..ctor();
            return;
        }

        public bool Deserialize(JSON_BannerParam json)
        {
            Exception exception;
            bool flag;
            if (json != null)
            {
                goto Label_0008;
            }
            return 0;
        Label_0008:
            try
            {
                this.iname = json.iname;
                this.type = (int) Enum.Parse(typeof(BannerType), json.type);
                this.sval = json.sval;
                this.banner = json.banr;
                this.banr_sprite = json.banr_sprite;
                this.begin_at = json.begin_at;
                this.end_at = json.end_at;
                this.priority = (json.priority > 0) ? json.priority : 0x7fffffff;
                this.message = json.message;
                this.is_not_home = json.is_not_home == 1;
                goto Label_00C5;
            }
            catch (Exception exception1)
            {
            Label_00B2:
                exception = exception1;
                Debug.LogException(exception);
                flag = 0;
                goto Label_00C7;
            }
        Label_00C5:
            return 1;
        Label_00C7:
            return flag;
        }

        public bool IsAvailablePeriod(DateTime now)
        {
            DateTime time;
            DateTime time2;
            bool flag;
            time = DateTime.MinValue;
            time2 = DateTime.MaxValue;
        Label_000C:
            try
            {
                if (string.IsNullOrEmpty(this.begin_at) != null)
                {
                    goto Label_0028;
                }
                time = DateTime.Parse(this.begin_at);
            Label_0028:
                if (string.IsNullOrEmpty(this.end_at) != null)
                {
                    goto Label_0044;
                }
                time2 = DateTime.Parse(this.end_at);
            Label_0044:
                goto Label_0056;
            }
            catch
            {
            Label_0049:
                flag = 0;
                goto Label_0072;
            }
        Label_0056:
            if ((now < time) != null)
            {
                goto Label_006E;
            }
            if ((time2 < now) == null)
            {
                goto Label_0070;
            }
        Label_006E:
            return 0;
        Label_0070:
            return 1;
        Label_0072:
            return flag;
        }

        public bool IsHomeBanner
        {
            get
            {
                return (this.is_not_home == 0);
            }
        }
    }
}

