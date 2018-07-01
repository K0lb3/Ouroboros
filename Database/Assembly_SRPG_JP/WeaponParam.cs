// Decompiled with JetBrains decompiler
// Type: SRPG.WeaponParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  public class WeaponParam
  {
    public string iname;
    public OInt atk;
    public OInt formula;

    public WeaponFormulaTypes FormulaType
    {
      get
      {
        return (WeaponFormulaTypes) (int) this.formula;
      }
    }

    public bool Deserialize(JSON_WeaponParam json)
    {
      if (json == null)
        return false;
      this.iname = json.iname;
      this.atk = (OInt) json.atk;
      this.formula = (OInt) json.formula;
      return true;
    }
  }
}
