// Decompiled with JetBrains decompiler
// Type: SRPG.SkillDeriveParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  public class SkillDeriveParam : BaseDeriveParam<SkillParam>
  {
    public string BaseSkillIname
    {
      get
      {
        return this.m_BaseParam.iname;
      }
    }

    public string DeriveSkillIname
    {
      get
      {
        return this.m_DeriveParam.iname;
      }
    }

    public string BaseSkillName
    {
      get
      {
        return this.m_BaseParam.name;
      }
    }

    public string DeriveSkillName
    {
      get
      {
        return this.m_DeriveParam.name;
      }
    }
  }
}
