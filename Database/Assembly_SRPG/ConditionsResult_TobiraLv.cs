namespace SRPG
{
    using GR;
    using System;

    public class ConditionsResult_TobiraLv : ConditionsResult_Unit
    {
        public int mCondsTobiraLv;
        public TobiraParam.Category mCondsTobiraCategory;
        public TobiraData mTobiraData;
        private bool mTargetIsMaxLevel;

        public ConditionsResult_TobiraLv(UnitData unitData, UnitParam unitParam, TobiraParam.Category condsTobiraCategory, int condsTobiraLv)
        {
            TobiraData data;
            base..ctor(unitData, unitParam);
            this.mCondsTobiraCategory = condsTobiraCategory;
            this.mCondsTobiraLv = condsTobiraLv;
            base.mTargetValue = condsTobiraLv;
            this.mTargetIsMaxLevel = condsTobiraLv == MonoSingleton<GameManager>.Instance.MasterParam.FixParam.TobiraLvCap;
            if (unitData == null)
            {
                goto Label_008A;
            }
            data = unitData.GetTobiraData(this.mCondsTobiraCategory);
            if (data == null)
            {
                goto Label_0091;
            }
            this.mTobiraData = data;
            base.mIsClear = (data.Lv < this.mCondsTobiraLv) == 0;
            base.mCurrentValue = data.Lv;
            goto Label_0091;
        Label_008A:
            base.mIsClear = 0;
        Label_0091:
            return;
        }

        public override string text
        {
            get
            {
                object[] objArray2;
                object[] objArray1;
                if (this.mTargetIsMaxLevel == null)
                {
                    goto Label_0043;
                }
                objArray1 = new object[] { base.unitName, TobiraParam.GetCategoryName(this.mCondsTobiraCategory), (int) (this.mCondsTobiraLv - 1) };
                return LocalizedText.Get("sys.TOBIRA_CONDITIONS_TOBIRA_LEVEL_MAX", objArray1);
            Label_0043:
                objArray2 = new object[] { base.unitName, TobiraParam.GetCategoryName(this.mCondsTobiraCategory), (int) (this.mCondsTobiraLv - 1) };
                return LocalizedText.Get("sys.TOBIRA_CONDITIONS_TOBIRA_LEVEL", objArray2);
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
                return string.Format("ユニット「{0}」を所持していません", base.unitName);
            Label_001C:
                if (this.mTobiraData == null)
                {
                    goto Label_0043;
                }
                return string.Format("ユニット「{0}」の「{1}」のレベルが足りません", base.unitName, TobiraParam.GetCategoryName(this.mCondsTobiraCategory));
            Label_0043:
                return string.Format("ユニット「{0}」の「{1}」が解放されていません", base.unitName, TobiraParam.GetCategoryName(this.mCondsTobiraCategory));
            }
        }
    }
}

