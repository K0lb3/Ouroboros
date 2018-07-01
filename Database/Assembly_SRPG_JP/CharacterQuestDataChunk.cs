// Decompiled with JetBrains decompiler
// Type: SRPG.CharacterQuestDataChunk
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

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
