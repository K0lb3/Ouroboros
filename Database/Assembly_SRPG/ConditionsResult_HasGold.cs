namespace SRPG
{
    using GR;
    using System;

    public class ConditionsResult_HasGold : ConditionsResult
    {
        public ConditionsResult_HasGold(int condsNum)
        {
            base..ctor();
            base.mCurrentValue = MonoSingleton<GameManager>.Instance.Player.Gold;
            base.mTargetValue = condsNum;
            base.mIsClear = (base.mCurrentValue < base.mTargetValue) == 0;
            return;
        }

        public override string text
        {
            get
            {
                return string.Empty;
            }
        }

        public override string errorText
        {
            get
            {
                return LocalizedText.Get("sys.GOLD_NOT_ENOUGH");
            }
        }
    }
}

