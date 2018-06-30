namespace SRPG
{
    using System;
    using System.Collections.Generic;

    public class VersusEnableTimeScheduleParam
    {
        private string mBegin;
        private string mOpen;
        private string mQuestIname;
        private List<DateTime> mAddDateList;

        public VersusEnableTimeScheduleParam()
        {
            base..ctor();
            return;
        }

        public bool Deserialize(JSON_VersusEnableTimeScheduleParam json)
        {
            int num;
            Exception exception;
            bool flag;
            this.mBegin = json.begin_time;
            this.mOpen = json.open_time;
            this.mQuestIname = json.quest_iname;
        Label_0024:
            try
            {
                if (json.add_date == null)
                {
                    goto Label_007D;
                }
                this.mAddDateList = new List<DateTime>();
                num = 0;
                goto Label_006F;
            Label_0041:
                if (string.IsNullOrEmpty(json.add_date[num]) != null)
                {
                    goto Label_006B;
                }
                this.mAddDateList.Add(DateTime.Parse(json.add_date[num]));
            Label_006B:
                num += 1;
            Label_006F:
                if (num < ((int) json.add_date.Length))
                {
                    goto Label_0041;
                }
            Label_007D:
                goto Label_009A;
            }
            catch (Exception exception1)
            {
            Label_0082:
                exception = exception1;
                DebugUtility.LogError(exception.Message);
                flag = 0;
                goto Label_009C;
            }
        Label_009A:
            return 1;
        Label_009C:
            return flag;
        }

        public string Begin
        {
            get
            {
                return this.mBegin;
            }
        }

        public string Open
        {
            get
            {
                return this.mOpen;
            }
        }

        public string QuestIname
        {
            get
            {
                return this.mQuestIname;
            }
        }

        public List<DateTime> AddDateList
        {
            get
            {
                return this.mAddDateList;
            }
        }
    }
}

