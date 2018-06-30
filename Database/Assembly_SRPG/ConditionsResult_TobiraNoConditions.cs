namespace SRPG
{
    using System;

    public class ConditionsResult_TobiraNoConditions : ConditionsResult
    {
        public ConditionsResult_TobiraNoConditions()
        {
            base..ctor();
            base.mIsClear = 1;
            return;
        }

        public override string text
        {
            get
            {
                return LocalizedText.Get("sys.TOBIRA_CONDITIONS_NOTHING");
            }
        }

        public override string errorText
        {
            get
            {
                return LocalizedText.Get("sys.ITEM_NOT_ENOUGH");
            }
        }
    }
}

