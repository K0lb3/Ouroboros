// Decompiled with JetBrains decompiler
// Type: SRPG.RankingQuestScheduleParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SRPG
{
  public class RankingQuestScheduleParam
  {
    public int id;
    public DateTime beginAt;
    public DateTime endAt;
    public DateTime reward_begin_at;
    public DateTime reward_end_at;
    public DateTime visible_begin_at;
    public DateTime visible_end_at;

    public bool Deserialize(JSON_RankingQuestScheduleParam json)
    {
      this.id = json.id;
      this.beginAt = DateTime.MinValue;
      this.endAt = DateTime.MaxValue;
      if (!string.IsNullOrEmpty(json.begin_at))
        DateTime.TryParse(json.begin_at, out this.beginAt);
      if (!string.IsNullOrEmpty(json.end_at))
        DateTime.TryParse(json.end_at, out this.endAt);
      this.reward_begin_at = DateTime.MinValue;
      this.reward_end_at = DateTime.MaxValue;
      if (!string.IsNullOrEmpty(json.reward_begin_at))
        DateTime.TryParse(json.reward_begin_at, out this.reward_begin_at);
      if (!string.IsNullOrEmpty(json.reward_end_at))
        DateTime.TryParse(json.reward_end_at, out this.reward_end_at);
      this.visible_begin_at = DateTime.MinValue;
      this.visible_end_at = DateTime.MaxValue;
      if (!string.IsNullOrEmpty(json.visible_begin_at))
        DateTime.TryParse(json.visible_begin_at, out this.visible_begin_at);
      if (!string.IsNullOrEmpty(json.visible_end_at))
        DateTime.TryParse(json.visible_end_at, out this.visible_end_at);
      return true;
    }

    public bool IsAvailablePeriod(DateTime now)
    {
      return !(now < this.beginAt) && !(this.endAt < now);
    }

    public bool IsAvailableRewardPeriod(DateTime now)
    {
      return !(now < this.reward_begin_at) && !(this.reward_end_at < now);
    }

    public bool IsAvailableVisiblePeriod(DateTime now)
    {
      return !(now < this.visible_begin_at) && !(this.visible_end_at < now);
    }

    public static List<RankingQuestScheduleParam> GetRankingQuestScheduleParam(RankingQuestScheduleParam.RakingQuestScheduleGetFlags flag)
    {
      List<RankingQuestScheduleParam> questScheduleParamList = new List<RankingQuestScheduleParam>();
      GameManager instanceDirect = MonoSingleton<GameManager>.GetInstanceDirect();
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) instanceDirect, (UnityEngine.Object) null))
        return questScheduleParamList;
      if (instanceDirect.RankingQuestScheduleParams == null)
      {
        DebugUtility.LogError("GameManager.Instance.RankingQuestScheduleParamsがnullです");
        return questScheduleParamList;
      }
      if (instanceDirect.RankingQuestParams == null)
      {
        DebugUtility.LogError("GameManager.Instance.RankingQuestParamsがnullです");
        return questScheduleParamList;
      }
      DateTime now = TimeManager.ServerTime;
      if (flag == RankingQuestScheduleParam.RakingQuestScheduleGetFlags.All)
      {
        questScheduleParamList.AddRange((IEnumerable<RankingQuestScheduleParam>) instanceDirect.RankingQuestScheduleParams);
      }
      else
      {
        bool period = false;
        questScheduleParamList = instanceDirect.RankingQuestScheduleParams.Where<RankingQuestScheduleParam>((Func<RankingQuestScheduleParam, bool>) (param =>
        {
          period = false;
          if ((flag & RankingQuestScheduleParam.RakingQuestScheduleGetFlags.Open) != RankingQuestScheduleParam.RakingQuestScheduleGetFlags.All)
            period |= param.IsAvailablePeriod(now);
          if ((flag & RankingQuestScheduleParam.RakingQuestScheduleGetFlags.Reward) != RankingQuestScheduleParam.RakingQuestScheduleGetFlags.All)
            period |= param.IsAvailableRewardPeriod(now);
          if ((flag & RankingQuestScheduleParam.RakingQuestScheduleGetFlags.Visible) != RankingQuestScheduleParam.RakingQuestScheduleGetFlags.All)
            period |= param.IsAvailableVisiblePeriod(now);
          return period;
        })).ToList<RankingQuestScheduleParam>();
      }
      return questScheduleParamList;
    }

    public static List<RankingQuestParam> FindRankingQuestParamBySchedule(RankingQuestScheduleParam.RakingQuestScheduleGetFlags flag)
    {
      List<RankingQuestParam> rankingQuestParamList = new List<RankingQuestParam>();
      GameManager instanceDirect = MonoSingleton<GameManager>.GetInstanceDirect();
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) instanceDirect, (UnityEngine.Object) null))
        return rankingQuestParamList;
      if (instanceDirect.RankingQuestParams == null)
      {
        DebugUtility.LogError("GameManager.Instance.RankingQuestParamsがnullです");
        return rankingQuestParamList;
      }
      List<RankingQuestScheduleParam> questScheduleParam = RankingQuestScheduleParam.GetRankingQuestScheduleParam(flag);
      List<RankingQuestParam> rankingQuestParams = instanceDirect.RankingQuestParams;
      for (int index1 = 0; index1 < rankingQuestParams.Count; ++index1)
      {
        for (int index2 = 0; index2 < questScheduleParam.Count; ++index2)
        {
          if (rankingQuestParams[index1].schedule_id == questScheduleParam[index2].id)
          {
            rankingQuestParamList.Add(rankingQuestParams[index1]);
            break;
          }
        }
      }
      return rankingQuestParamList;
    }

    public static RankingQuestScheduleParam FindByID(int id)
    {
      GameManager instanceDirect = MonoSingleton<GameManager>.GetInstanceDirect();
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) instanceDirect, (UnityEngine.Object) null))
        return (RankingQuestScheduleParam) null;
      if (instanceDirect.RankingQuestScheduleParams == null)
        return (RankingQuestScheduleParam) null;
      return instanceDirect.RankingQuestScheduleParams.Find((Predicate<RankingQuestScheduleParam>) (param => param.id == id));
    }

    public static RankingQuestParam CompareOpenOrLatest(ref DateTime now, RankingQuestParam param1, RankingQuestParam param2)
    {
      long num1 = param1.scheduleParam.endAt.Ticks - now.Ticks;
      long num2 = param2.scheduleParam.endAt.Ticks - now.Ticks;
      return !param1.scheduleParam.IsAvailablePeriod(now) ? (!param1.scheduleParam.IsAvailableVisiblePeriod(now) ? (!param2.scheduleParam.IsAvailablePeriod(now) ? (!param2.scheduleParam.IsAvailableVisiblePeriod(now) ? (num1 <= num2 ? param2 : param1) : param2) : param2) : (!param2.scheduleParam.IsAvailablePeriod(now) ? (!param2.scheduleParam.IsAvailableVisiblePeriod(now) ? param1 : (num1 <= num2 ? param2 : param1)) : param2)) : (!param2.scheduleParam.IsAvailablePeriod(now) ? param1 : (num1 >= num2 ? param2 : param1));
    }

    public static List<RankingQuestParam> FilterDuplicateRankingQuestIDs(List<RankingQuestParam> list)
    {
      DateTime serverTime = TimeManager.ServerTime;
      List<RankingQuestParam> rankingQuestParamList = new List<RankingQuestParam>();
      for (int i = 0; i < list.Count; ++i)
      {
        int index = rankingQuestParamList.FindIndex((Predicate<RankingQuestParam>) (p => p.iname == list[i].iname));
        RankingQuestParam rankingQuestParam = index == -1 ? (RankingQuestParam) null : rankingQuestParamList[index];
        if (rankingQuestParam != null)
          rankingQuestParamList[index] = RankingQuestScheduleParam.CompareOpenOrLatest(ref serverTime, rankingQuestParam, list[i]);
        else
          rankingQuestParamList.Add(list[i]);
      }
      return rankingQuestParamList;
    }

    [Flags]
    public enum RakingQuestScheduleGetFlags
    {
      All = 0,
      Open = 1,
      Reward = 2,
      Visible = 4,
    }
  }
}
