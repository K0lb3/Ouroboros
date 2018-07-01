// Decompiled with JetBrains decompiler
// Type: SRPG.UnitSubSetting
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

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
