// Decompiled with JetBrains decompiler
// Type: SRPG.ConditionsResult_AwakeLv
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  public class ConditionsResult_AwakeLv : ConditionsResult_Unit
  {
    public int mCondsAwakeLv;

    public ConditionsResult_AwakeLv(UnitData unitData, UnitParam unitParam, int condsAwakeLv)
      : base(unitData, unitParam)
    {
      this.mCondsAwakeLv = condsAwakeLv;
      this.mTargetValue = condsAwakeLv;
      if (unitData != null)
      {
        this.mIsClear = unitData.AwakeLv >= condsAwakeLv;
        this.mCurrentValue = unitData.AwakeLv;
      }
      else
        this.mIsClear = false;
    }

    public override string text
    {
      get
      {
        return LocalizedText.Get("sys.TOBIRA_CONDITIONS_UNIT_AWAKE", (object) this.unitName, (object) this.mCondsAwakeLv);
      }
    }

    public override string errorText
    {
      get
      {
        return string.Format("ユニット「{0}」を所持していません", (object) this.unitName);
      }
    }
  }
}
