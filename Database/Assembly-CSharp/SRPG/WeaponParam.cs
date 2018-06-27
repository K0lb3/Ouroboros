// Decompiled with JetBrains decompiler
// Type: SRPG.WeaponParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

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
