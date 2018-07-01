// Decompiled with JetBrains decompiler
// Type: SRPG.AbilityDeriveParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  public class AbilityDeriveParam : BaseDeriveParam<AbilityParam>
  {
    public string BaseAbilityIname
    {
      get
      {
        return this.m_BaseParam.iname;
      }
    }

    public string DeriveAbilityIname
    {
      get
      {
        return this.m_DeriveParam.iname;
      }
    }

    public string BaseAbilityName
    {
      get
      {
        return this.m_BaseParam.name;
      }
    }

    public string DeriveAbilityName
    {
      get
      {
        return this.m_DeriveParam.name;
      }
    }
  }
}
