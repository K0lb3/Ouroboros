namespace SRPG
{
    using System;
    using System.Collections.Generic;

    public class VersusEnableTimeParam
    {
        private int mScheduleId;
        private VERSUS_TYPE mVersusType;
        private DateTime mBeginAt;
        private DateTime mEndAt;
        private List<VersusEnableTimeScheduleParam> mSchedule;
        private VersusDraftType mDraftType;

        public VersusEnableTimeParam()
        {
            base..ctor();
            return;
        }

        public bool Deserialize(JSON_VersusEnableTimeParam json)
        {
            Exception exception;
            int num;
            VersusEnableTimeScheduleParam param;
            bool flag;
            if (json != null)
            {
                goto Label_0008;
            }
            return 0;
        Label_0008:
            this.mScheduleId = json.id;
            this.mVersusType = json.mode;
        Label_0020:
            try
            {
                if (string.IsNullOrEmpty(json.begin_at) != null)
                {
                    goto Label_0041;
                }
                this.mBeginAt = DateTime.Parse(json.begin_at);
            Label_0041:
                if (string.IsNullOrEmpty(json.end_at) != null)
                {
                    goto Label_0062;
                }
                this.mEndAt = DateTime.Parse(json.end_at);
            Label_0062:
                goto Label_007F;
            }
            catch (Exception exception1)
            {
            Label_0067:
                exception = exception1;
                DebugUtility.LogError(exception.Message);
                flag = 0;
                goto Label_00DB;
            }
        Label_007F:
            this.mSchedule = new List<VersusEnableTimeScheduleParam>();
            num = 0;
            goto Label_00BF;
        Label_0091:
            param = new VersusEnableTimeScheduleParam();
            if (param.Deserialize(json.schedule[num]) != null)
            {
                goto Label_00AF;
            }
            goto Label_00BB;
        Label_00AF:
            this.mSchedule.Add(param);
        Label_00BB:
            num += 1;
        Label_00BF:
            if (num < ((int) json.schedule.Length))
            {
                goto Label_0091;
            }
            this.mDraftType = json.draft_type;
            return 1;
        Label_00DB:
            return flag;
        }

        public int ScheduleId
        {
            get
            {
                return this.mScheduleId;
            }
        }

        public VERSUS_TYPE VersusType
        {
            get
            {
                return this.mVersusType;
            }
        }

        public DateTime BeginAt
        {
            get
            {
                return this.mBeginAt;
            }
        }

        public DateTime EndAt
        {
            get
            {
                return this.mEndAt;
            }
        }

        public List<VersusEnableTimeScheduleParam> Schedule
        {
            get
            {
                return this.mSchedule;
            }
        }

        public VersusDraftType DraftType
        {
            get
            {
                return this.mDraftType;
            }
        }
    }
}

