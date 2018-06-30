namespace SRPG
{
    using System;

    public class TobiraCondsUnitParam
    {
        private string mId;
        private string mUnitIname;
        private int mLevel;
        private int mAwakeLevel;
        private string mJobIname;
        private int mJobLevel;
        private TobiraParam.Category mCategory;
        private int mTobiraLv;
        private ConditionsDetail mConditionsDetail;

        public TobiraCondsUnitParam()
        {
            base..ctor();
            return;
        }

        public void Deserialize(JSON_TobiraCondsUnitParam json)
        {
            if (json != null)
            {
                goto Label_0007;
            }
            return;
        Label_0007:
            this.mId = json.id;
            this.mUnitIname = json.unit_iname;
            this.mLevel = json.lv;
            this.mAwakeLevel = json.awake_lv;
            this.mJobIname = json.job_iname;
            this.mJobLevel = json.job_lv;
            this.mCategory = json.category;
            this.mTobiraLv = json.tobira_lv;
            this.UpdateConditionsFlag();
            return;
        }

        public bool HasFlag(ConditionsDetail flag)
        {
            return (((this.mConditionsDetail & flag) == 0L) == 0);
        }

        private void UpdateConditionsFlag()
        {
            if (this.IsSelfUnit == null)
            {
                goto Label_001A;
            }
            this.mConditionsDetail |= 1L;
        Label_001A:
            if (this.Level <= 0)
            {
                goto Label_0035;
            }
            this.mConditionsDetail |= 2L;
        Label_0035:
            if (string.IsNullOrEmpty(this.JobIname) != null)
            {
                goto Label_0060;
            }
            if (this.mJobLevel <= 0)
            {
                goto Label_0060;
            }
            this.mConditionsDetail |= 8L;
        Label_0060:
            if (this.mAwakeLevel <= 0)
            {
                goto Label_007B;
            }
            this.mConditionsDetail |= 4L;
        Label_007B:
            if (this.mTobiraLv <= 0)
            {
                goto Label_0097;
            }
            this.mConditionsDetail |= 0x10L;
        Label_0097:
            return;
        }

        public string Id
        {
            get
            {
                return this.mId;
            }
        }

        public string UnitIname
        {
            get
            {
                return this.mUnitIname;
            }
        }

        public int Level
        {
            get
            {
                return this.mLevel;
            }
        }

        public int AwakeLevel
        {
            get
            {
                return this.mAwakeLevel;
            }
        }

        public string JobIname
        {
            get
            {
                return this.mJobIname;
            }
        }

        public int JobLevel
        {
            get
            {
                return this.mJobLevel;
            }
        }

        public TobiraParam.Category TobiraCategory
        {
            get
            {
                return this.mCategory;
            }
        }

        public int TobiraLv
        {
            get
            {
                return this.mTobiraLv;
            }
        }

        public bool IsSelfUnit
        {
            get
            {
                return string.IsNullOrEmpty(this.mUnitIname);
            }
        }

        [Flags]
        public enum ConditionsDetail : long
        {
            IsSelf = 1L,
            IsUnitLv = 2L,
            IsAwake = 4L,
            IsJobLv = 8L,
            IsTobiraLv = 0x10L
        }
    }
}

