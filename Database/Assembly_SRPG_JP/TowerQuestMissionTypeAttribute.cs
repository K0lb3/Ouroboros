// Decompiled with JetBrains decompiler
// Type: SRPG.TowerQuestMissionTypeAttribute
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using System;

namespace SRPG
{
  public class TowerQuestMissionTypeAttribute : Attribute
  {
    private QuestMissionProgressJudgeType m_ProgressJudgeType;

    public TowerQuestMissionTypeAttribute(QuestMissionProgressJudgeType progressJudgeType)
    {
      this.m_ProgressJudgeType = progressJudgeType;
    }

    public QuestMissionProgressJudgeType ProgressJudgeType
    {
      get
      {
        return this.m_ProgressJudgeType;
      }
    }
  }
}
