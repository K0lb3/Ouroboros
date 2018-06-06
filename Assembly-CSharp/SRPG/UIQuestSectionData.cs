// Decompiled with JetBrains decompiler
// Type: SRPG.UIQuestSectionData
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
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
