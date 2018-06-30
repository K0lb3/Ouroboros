namespace SRPG
{
    using GR;
    using System;

    public class ConditionsResult_HasItem : ConditionsResult
    {
        public ConditionsResult_HasItem(string iname, int condsItemNum)
        {
            ItemData data;
            base..ctor();
            data = MonoSingleton<GameManager>.Instance.Player.FindItemDataByItemID(iname);
            if (data == null)
            {
                goto Label_0029;
            }
            base.mCurrentValue = data.Num;
        Label_0029:
            base.mTargetValue = condsItemNum;
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
                return LocalizedText.Get("sys.ITEM_NOT_ENOUGH");
            }
        }
    }
}

