namespace SRPG
{
    using System;

    public class VersusScheduleParam
    {
        public string tower_iname;
        public string iname;
        public string begin_at;
        public string end_at;
        public string gift_begin_at;
        public string gift_end_at;
        private DateTime BeginDate;
        private DateTime EndDate;
        private DateTime GiftBeginDate;
        private DateTime GiftEndDate;

        public VersusScheduleParam()
        {
            base..ctor();
            return;
        }

        public void Deserialize(JSON_VersusSchedule json)
        {
            Exception exception;
            if (json != null)
            {
                goto Label_0007;
            }
            return;
        Label_0007:
            this.tower_iname = json.tower_iname;
            this.iname = json.iname;
            this.begin_at = json.begin_at;
            this.end_at = json.end_at;
            this.gift_begin_at = json.gift_begin_at;
            this.gift_end_at = json.gift_end_at;
        Label_004F:
            try
            {
                this.BeginDate = DateTime.Parse(this.begin_at);
                this.EndDate = DateTime.Parse(this.end_at);
                this.GiftBeginDate = DateTime.Parse(this.gift_begin_at);
                this.GiftEndDate = DateTime.Parse(this.gift_end_at);
                goto Label_00A9;
            }
            catch (Exception exception1)
            {
            Label_0098:
                exception = exception1;
                DebugUtility.Log(exception.ToString());
                goto Label_00A9;
            }
        Label_00A9:
            return;
        }

        public bool IsOpen
        {
            get
            {
                DateTime time;
                time = TimeManager.ServerTime;
                return (((this.BeginDate < time) == null) ? 0 : (time < this.EndDate));
            }
        }

        public bool IsGift
        {
            get
            {
                DateTime time;
                time = TimeManager.ServerTime;
                return (((this.GiftBeginDate < time) == null) ? 0 : (time < this.GiftEndDate));
            }
        }
    }
}

