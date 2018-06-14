// Decompiled with JetBrains decompiler
// Type: SRPG.Json_BtlQuestRanking
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

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
