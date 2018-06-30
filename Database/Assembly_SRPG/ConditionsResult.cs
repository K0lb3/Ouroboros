namespace SRPG
{
    using System;

    public abstract class ConditionsResult
    {
        protected bool mIsClear;
        protected int mTargetValue;
        protected int mCurrentValue;

        protected ConditionsResult()
        {
            base..ctor();
            return;
        }

        public bool isClear
        {
            get
            {
                return this.mIsClear;
            }
        }

        public int targetValue
        {
            get
            {
                return this.mTargetValue;
            }
        }

        public int currentValue
        {
            get
            {
                return this.mCurrentValue;
            }
        }

        public abstract string text { get; }

        public abstract string errorText { get; }

        public bool isConditionsUnitLv
        {
            get
            {
                return (base.GetType() == typeof(ConditionsResult_UnitLv));
            }
        }

        public bool isConditionsAwake
        {
            get
            {
                return (base.GetType() == typeof(ConditionsResult_AwakeLv));
            }
        }

        public bool isConditionsJobLv
        {
            get
            {
                return (base.GetType() == typeof(ConditionsResult_JobLv));
            }
        }

        public bool isConditionsTobiraLv
        {
            get
            {
                return (base.GetType() == typeof(ConditionsResult_TobiraLv));
            }
        }

        public bool isConditionsQuestClear
        {
            get
            {
                return (base.GetType() == typeof(ConditionsResult_QuestClear));
            }
        }

        public bool isConditionsTobiraNoConditions
        {
            get
            {
                return (base.GetType() == typeof(ConditionsResult_TobiraNoConditions));
            }
        }
    }
}

