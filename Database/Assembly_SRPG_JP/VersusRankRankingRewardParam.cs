// Decompiled with JetBrains decompiler
// Type: SRPG.VersusRankRankingRewardParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  public class VersusRankRankingRewardParam
  {
    private int mScheduleId;
    private int mRankBegin;
    private int mRankEnd;
    private string mRewardId;

    public int ScheduleId
    {
      get
      {
        return this.mScheduleId;
      }
    }

    public int RankBegin
    {
      get
      {
        return this.mRankBegin;
      }
    }

    public int RankEnd
    {
      get
      {
        return this.mRankEnd;
      }
    }

    public string RewardId
    {
      get
      {
        return this.mRewardId;
      }
    }

    public bool Deserialize(JSON_VersusRankRankingRewardParam json)
    {
      if (json == null)
        return false;
      this.mScheduleId = json.schedule_id;
      this.mRankBegin = json.rank_begin;
      this.mRankEnd = json.rank_end;
      this.mRewardId = json.reward_id;
      return true;
    }
  }
}
