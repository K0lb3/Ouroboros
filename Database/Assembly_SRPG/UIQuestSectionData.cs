// Decompiled with JetBrains decompiler
// Type: SRPG.UIQuestSectionData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

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
