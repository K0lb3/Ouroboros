namespace SRPG
{
    using System;

    public class VersusRankMissionScheduleParam
    {
        private int mScheduleId;
        private string mIName;

        public VersusRankMissionScheduleParam()
        {
            base..ctor();
            return;
        }

        public bool Deserialize(JSON_VersusRankMissionScheduleParam json)
        {
            if (json != null)
            {
                goto Label_0008;
            }
            return 0;
        Label_0008:
            this.mScheduleId = json.schedule_id;
            this.mIName = json.iname;
            return 1;
        }

        public int ScheduleId
        {
            get
            {
                return this.mScheduleId;
            }
        }

        public string IName
        {
            get
            {
                return this.mIName;
            }
        }
    }
}

