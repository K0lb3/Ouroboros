// Decompiled with JetBrains decompiler
// Type: SRPG.CharacterQuestDataChunk
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System.Collections.Generic;

namespace SRPG
{
  public class CharacterQuestDataChunk
  {
    public List<QuestParam> questParams = new List<QuestParam>();
    public string areaName;
    public string unitName;
    public UnitParam unitParam;

    public void SetUnitNameFromChapterID(string chapterID)
    {
      this.unitName = chapterID;
    }
  }
}
