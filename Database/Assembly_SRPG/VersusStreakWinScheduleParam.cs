namespace SRPG
{
    using System;

    public class VersusStreakWinScheduleParam
    {
        public int id;
        public STREAK_JUDGE judge;
        public DateTime begin_at;
        public DateTime end_at;

        public VersusStreakWinScheduleParam()
        {
            base..ctor();
            return;
        }

        public bool Deserialize(JSON_VersusStreakWinSchedule json)
        {
            Exception exception;
            bool flag;
            if (json != null)
            {
                goto Label_0008;
            }
            return 0;
        Label_0008:
            this.id = json.id;
        Label_0014:
            try
            {
                this.judge = (int) Enum.ToObject(typeof(STREAK_JUDGE), json.judge);
                if (string.IsNullOrEmpty(json.begin_at) != null)
                {
                    goto Label_0055;
                }
                this.begin_at = DateTime.Parse(json.begin_at);
            Label_0055:
                if (string.IsNullOrEmpty(json.end_at) != null)
                {
                    goto Label_0076;
                }
                this.end_at = DateTime.Parse(json.end_at);
            Label_0076:
                goto Label_0093;
            }
            catch (Exception exception1)
            {
            Label_007B:
                exception = exception1;
                DebugUtility.LogError(exception.Message);
                flag = 0;
                goto Label_0095;
            }
        Label_0093:
            return 1;
        Label_0095:
            return flag;
        }
    }
}

