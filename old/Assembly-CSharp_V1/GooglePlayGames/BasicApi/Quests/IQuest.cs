// Decompiled with JetBrains decompiler
// Type: GooglePlayGames.BasicApi.Quests.IQuest
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System;

namespace GooglePlayGames.BasicApi.Quests
{
  public interface IQuest
  {
    string Id { get; }

    string Name { get; }

    string Description { get; }

    string BannerUrl { get; }

    string IconUrl { get; }

    DateTime StartTime { get; }

    DateTime ExpirationTime { get; }

    DateTime? AcceptedTime { get; }

    IQuestMilestone Milestone { get; }

    QuestState State { get; }
  }
}
