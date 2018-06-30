namespace SRPG
{
    using System;

    public class ConditionsResult_UnitLv : ConditionsResult_Unit
    {
        public int mCondsLv;

        public ConditionsResult_UnitLv(UnitData unitData, UnitParam unitParam, int condsLv)
        {
            base..ctor(unitData, unitParam);
            this.mCondsLv = condsLv;
            base.mTargetValue = condsLv;
            if (unitData == null)
            {
                goto Label_003F;
            }
            base.mIsClear = (unitData.Lv < condsLv) == 0;
            base.mCurrentValue = unitData.Lv;
            goto Label_0046;
        Label_003F:
            base.mIsClear = 0;
        Label_0046:
            return;
        }

        public override string text
        {
            get
            {
                object[] objArray1;
                objArray1 = new object[] { base.unitName, (int) this.mCondsLv };
                return LocalizedText.Get("sys.TOBIRA_CONDITIONS_UNIT_LEVEL", objArray1);
            }
        }

        public override string errorText
        {
            get
            {
                if (base.unitData == null)
                {
                    goto Label_001C;
                }
                return string.Format("ユニット「{0}」のレベルが条件を満たしていない", base.unitName);
            Label_001C:
                return string.Format("ユニット「{0}」を所持していません", base.unitName);
            }
        }
    }
}

