// Decompiled with JetBrains decompiler
// Type: SRPG.ConditionsResult_Unit
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  public abstract class ConditionsResult_Unit : ConditionsResult
  {
    private UnitData mUnitData;
    private UnitParam mUnitParam;

    public ConditionsResult_Unit(UnitData unitData, UnitParam unitParam)
    {
      this.mUnitData = unitData;
      this.mUnitParam = unitParam;
    }

    public UnitData unitData
    {
      get
      {
        return this.mUnitData;
      }
    }

    public bool hasUnitData
    {
      get
      {
        return this.mUnitData != null;
      }
    }

    public string unitName
    {
      get
      {
        if (this.mUnitData != null)
          return this.mUnitData.UnitParam.name;
        if (this.mUnitParam != null)
          return this.mUnitParam.name;
        return string.Empty;
      }
    }
  }
}
