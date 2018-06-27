// Decompiled with JetBrains decompiler
// Type: GooglePlayGames.BasicApi.Quests.QuestFetchFlags
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System;

namespace GooglePlayGames.BasicApi.Quests
{
  [Flags]
  public enum QuestFetchFlags
  {
    Upcoming = 1,
    Open = 2,
    Accepted = 4,
    Completed = 8,
    CompletedNotClaimed = 16, // 0x00000010
    Expired = 32, // 0x00000020
    EndingSoon = 64, // 0x00000040
    Failed = 128, // 0x00000080
    All = -1,
  }
}
