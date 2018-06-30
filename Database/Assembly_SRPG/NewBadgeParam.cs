namespace SRPG
{
    using System;

    public class NewBadgeParam
    {
        private bool mIsUseNewFlag;
        private bool mIsNew;
        private NewBadgeType mType;

        public NewBadgeParam(bool use, bool isnew, NewBadgeType type)
        {
            base..ctor();
            this.mIsUseNewFlag = use;
            this.mIsNew = isnew;
            this.mType = type;
            return;
        }

        public bool use_newflag
        {
            get
            {
                return this.mIsUseNewFlag;
            }
        }

        public bool is_new
        {
            get
            {
                return this.mIsNew;
            }
        }

        public NewBadgeType type
        {
            get
            {
                return this.mType;
            }
        }
    }
}

