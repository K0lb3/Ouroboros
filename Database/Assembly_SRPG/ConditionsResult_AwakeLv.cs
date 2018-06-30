namespace SRPG
{
    using System;

    public class ConditionsResult_AwakeLv : ConditionsResult_Unit
    {
        public int mCondsAwakeLv;

        public ConditionsResult_AwakeLv(UnitData unitData, UnitParam unitParam, int condsAwakeLv)
        {
            base..ctor(unitData, unitParam);
            this.mCondsAwakeLv = condsAwakeLv;
            base.mTargetValue = condsAwakeLv;
            if (unitData == null)
            {
                goto Label_003F;
            }
            base.mIsClear = (unitData.AwakeLv < condsAwakeLv) == 0;
            base.mCurrentValue = unitData.AwakeLv;
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
                objArray1 = new object[] { base.unitName, (int) this.mCondsAwakeLv };
                return LocalizedText.Get("sys.TOBIRA_CONDITIONS_UNIT_AWAKE", objArray1);
            }
        }

        public override string errorText
        {
            get
            {
                return string.Format("ユニット「{0}」を所持していません", base.unitName);
            }
        }
    }
}

