// Decompiled with JetBrains decompiler
// Type: SRPG.CharacterQuestDataChunk
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
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
