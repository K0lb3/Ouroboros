// Decompiled with JetBrains decompiler
// Type: SRPG.Json_BtlQuestRanking
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  public class Json_BtlQuestRanking
  {
    public int is_new_score;
    public int is_join_reward;
    public int rank;

    public bool IsNewScore
    {
      get
      {
        return this.is_new_score == 1;
      }
    }

    public bool IsJoinReward
    {
      get
      {
        return this.is_join_reward == 1;
      }
    }
  }
}
