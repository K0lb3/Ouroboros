// Decompiled with JetBrains decompiler
// Type: SRPG.UIQuestSectionData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  public class UIQuestSectionData
  {
    private SectionParam mParam;

    public UIQuestSectionData(SectionParam param)
    {
      this.mParam = param;
    }

    public string Name
    {
      get
      {
        return this.mParam.name;
      }
    }

    public string SectionID
    {
      get
      {
        return this.mParam.iname;
      }
    }
  }
}
