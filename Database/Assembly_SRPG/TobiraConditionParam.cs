namespace SRPG
{
    using System;

    public class TobiraConditionParam
    {
        private ConditionType mCondType;
        private string mCondIname;
        private TobiraCondsUnitParam mCondUnit;

        public TobiraConditionParam()
        {
            base..ctor();
            return;
        }

        public void Deserialize(JSON_TobiraConditionParam json)
        {
            if (json != null)
            {
                goto Label_0007;
            }
            return;
        Label_0007:
            this.mCondType = json.conds_type;
            this.mCondIname = json.conds_iname;
            return;
        }

        public void SetCondUnit(TobiraCondsUnitParam cond_unit)
        {
            this.mCondUnit = cond_unit;
            return;
        }

        public ConditionType CondType
        {
            get
            {
                return this.mCondType;
            }
        }

        public string CondIname
        {
            get
            {
                return this.mCondIname;
            }
        }

        public TobiraCondsUnitParam CondUnit
        {
            get
            {
                return this.mCondUnit;
            }
        }

        public enum ConditionType
        {
            None,
            Unit,
            Quest
        }
    }
}

