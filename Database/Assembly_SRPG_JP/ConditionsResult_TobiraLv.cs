// Decompiled with JetBrains decompiler
// Type: SRPG.ConditionsResult_TobiraLv
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;

namespace SRPG
{
  public class ConditionsResult_TobiraLv : ConditionsResult_Unit
  {
    public int mCondsTobiraLv;
    public TobiraParam.Category mCondsTobiraCategory;
    public TobiraData mTobiraData;
    private bool mTargetIsMaxLevel;

    public ConditionsResult_TobiraLv(UnitData unitData, UnitParam unitParam, TobiraParam.Category condsTobiraCategory, int condsTobiraLv)
      : base(unitData, unitParam)
    {
      this.mCondsTobiraCategory = condsTobiraCategory;
      this.mCondsTobiraLv = condsTobiraLv;
      this.mTargetValue = condsTobiraLv;
      this.mTargetIsMaxLevel = condsTobiraLv == (int) MonoSingleton<GameManager>.Instance.MasterParam.FixParam.TobiraLvCap;
      if (unitData != null)
      {
        TobiraData tobiraData = unitData.GetTobiraData(this.mCondsTobiraCategory);
        if (tobiraData == null)
          return;
        this.mTobiraData = tobiraData;
        this.mIsClear = tobiraData.Lv >= this.mCondsTobiraLv;
        this.mCurrentValue = tobiraData.Lv;
      }
      else
        this.mIsClear = false;
    }

    public override string text
    {
      get
      {
        if (this.mTargetIsMaxLevel)
          return LocalizedText.Get("sys.TOBIRA_CONDITIONS_TOBIRA_LEVEL_MAX", (object) this.unitName, (object) TobiraParam.GetCategoryName(this.mCondsTobiraCategory), (object) (this.mCondsTobiraLv - 1));
        return LocalizedText.Get("sys.TOBIRA_CONDITIONS_TOBIRA_LEVEL", (object) this.unitName, (object) TobiraParam.GetCategoryName(this.mCondsTobiraCategory), (object) (this.mCondsTobiraLv - 1));
      }
    }

    public override string errorText
    {
      get
      {
        if (this.unitData != null)
          return string.Format("ユニット「{0}」を所持していません", (object) this.unitName);
        if (this.mTobiraData != null)
          return string.Format("ユニット「{0}」の「{1}」のレベルが足りません", (object) this.unitName, (object) TobiraParam.GetCategoryName(this.mCondsTobiraCategory));
        return string.Format("ユニット「{0}」の「{1}」が解放されていません", (object) this.unitName, (object) TobiraParam.GetCategoryName(this.mCondsTobiraCategory));
      }
    }
  }
}
