// Decompiled with JetBrains decompiler
// Type: SRPG.UnitSubSetting
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  public class UnitSubSetting
  {
    public eMapUnitCtCalcType startCtCalc;
    public OInt startCtVal;

    public UnitSubSetting()
    {
    }

    public UnitSubSetting(JSON_MapPartySubCT json)
    {
      this.startCtCalc = (eMapUnitCtCalcType) json.ct_calc;
      this.startCtVal = (OInt) json.ct_val;
    }
  }
}
