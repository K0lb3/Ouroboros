namespace SRPG
{
    using System;
    using System.Collections.Generic;

    public class VersusRankParam
    {
        private int mId;
        private VS_MODE mVSMode;
        private string mName;
        private int mLimit;
        private DateTime mBeginAt;
        private DateTime mEndAt;
        private int mWinPointBase;
        private int mLosePointBase;
        private List<DateTime> mDisableDateList;
        private string mHUrl;

        public VersusRankParam()
        {
            base..ctor();
            return;
        }

        public bool Deserialize(JSON_VersusRankParam json)
        {
            int num;
            Exception exception;
            bool flag;
            if (json != null)
            {
                goto Label_0008;
            }
            return 0;
        Label_0008:
            this.mId = json.id;
            this.mVSMode = json.btl_mode;
            this.mName = json.name;
            this.mLimit = json.limit;
            this.mWinPointBase = json.win_pt_base;
            this.mLosePointBase = json.lose_pt_base;
            this.mHUrl = json.hurl;
            this.mDisableDateList = new List<DateTime>();
        Label_0067:
            try
            {
                if (string.IsNullOrEmpty(json.begin_at) != null)
                {
                    goto Label_0088;
                }
                this.mBeginAt = DateTime.Parse(json.begin_at);
            Label_0088:
                if (string.IsNullOrEmpty(json.end_at) != null)
                {
                    goto Label_00A9;
                }
                this.mEndAt = DateTime.Parse(json.end_at);
            Label_00A9:
                if (json.disabledate == null)
                {
                    goto Label_00F7;
                }
                num = 0;
                goto Label_00E9;
            Label_00BB:
                if (string.IsNullOrEmpty(json.disabledate[num]) != null)
                {
                    goto Label_00E5;
                }
                this.mDisableDateList.Add(DateTime.Parse(json.disabledate[num]));
            Label_00E5:
                num += 1;
            Label_00E9:
                if (num < ((int) json.disabledate.Length))
                {
                    goto Label_00BB;
                }
            Label_00F7:
                goto Label_0114;
            }
            catch (Exception exception1)
            {
            Label_00FC:
                exception = exception1;
                DebugUtility.LogError(exception.Message);
                flag = 0;
                goto Label_0116;
            }
        Label_0114:
            return 1;
        Label_0116:
            return flag;
        }

        public int Id
        {
            get
            {
                return this.mId;
            }
        }

        public VS_MODE VSMode
        {
            get
            {
                return this.mVSMode;
            }
        }

        public string Name
        {
            get
            {
                return this.mName;
            }
        }

        public int Limit
        {
            get
            {
                return this.mLimit;
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

        public IList<DateTime> DisableDateList
        {
            get
            {
                return this.mDisableDateList.AsReadOnly();
            }
        }

        public int WinPointBase
        {
            get
            {
                return this.mWinPointBase;
            }
        }

        public int LosePointBase
        {
            get
            {
                return this.mLosePointBase;
            }
        }

        public string HelpURL
        {
            get
            {
                return this.mHUrl;
            }
        }
    }
}

