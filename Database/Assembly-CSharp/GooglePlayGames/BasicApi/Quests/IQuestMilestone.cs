// Decompiled with JetBrains decompiler
// Type: GooglePlayGames.BasicApi.Quests.IQuestMilestone
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

namespace GooglePlayGames.BasicApi.Quests
{
  public interface IQuestMilestone
  {
    string Id { get; }

    string EventId { get; }

    string QuestId { get; }

    ulong CurrentCount { get; }

    ulong TargetCount { get; }

    byte[] CompletionRewardData { get; }

    MilestoneState State { get; }
  }
}
