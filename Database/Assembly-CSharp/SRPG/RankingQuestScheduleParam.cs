// Decompiled with JetBrains decompiler
// Type: SRPG.RankingQuestScheduleParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

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
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      RankingQuestScheduleParam.\u003CGetRankingQuestScheduleParam\u003Ec__AnonStorey2F1 paramCAnonStorey2F1 = new RankingQuestScheduleParam.\u003CGetRankingQuestScheduleParam\u003Ec__AnonStorey2F1();
      // ISSUE: reference to a compiler-generated field
      paramCAnonStorey2F1.flag = flag;
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
      // ISSUE: reference to a compiler-generated field
      paramCAnonStorey2F1.now = TimeManager.ServerTime;
      // ISSUE: reference to a compiler-generated field
      if (paramCAnonStorey2F1.flag == RankingQuestScheduleParam.RakingQuestScheduleGetFlags.All)
      {
        questScheduleParamList.AddRange((IEnumerable<RankingQuestScheduleParam>) instanceDirect.RankingQuestScheduleParams);
      }
      else
      {
        // ISSUE: object of a compiler-generated type is created
        // ISSUE: reference to a compiler-generated method
        questScheduleParamList = instanceDirect.RankingQuestScheduleParams.Where<RankingQuestScheduleParam>(new Func<RankingQuestScheduleParam, bool>(new RankingQuestScheduleParam.\u003CGetRankingQuestScheduleParam\u003Ec__AnonStorey2F0()
        {
          \u003C\u003Ef__ref\u0024753 = paramCAnonStorey2F1,
          period = false
        }.\u003C\u003Em__2F3)).ToList<RankingQuestScheduleParam>();
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
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      RankingQuestScheduleParam.\u003CFilterDuplicateRankingQuestIDs\u003Ec__AnonStorey2F3 idsCAnonStorey2F3 = new RankingQuestScheduleParam.\u003CFilterDuplicateRankingQuestIDs\u003Ec__AnonStorey2F3();
      // ISSUE: reference to a compiler-generated field
      idsCAnonStorey2F3.list = list;
      DateTime serverTime = TimeManager.ServerTime;
      List<RankingQuestParam> rankingQuestParamList = new List<RankingQuestParam>();
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      RankingQuestScheduleParam.\u003CFilterDuplicateRankingQuestIDs\u003Ec__AnonStorey2F4 idsCAnonStorey2F4 = new RankingQuestScheduleParam.\u003CFilterDuplicateRankingQuestIDs\u003Ec__AnonStorey2F4();
      // ISSUE: reference to a compiler-generated field
      idsCAnonStorey2F4.\u003C\u003Ef__ref\u0024755 = idsCAnonStorey2F3;
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      for (idsCAnonStorey2F4.i = 0; idsCAnonStorey2F4.i < idsCAnonStorey2F3.list.Count; ++idsCAnonStorey2F4.i)
      {
        // ISSUE: reference to a compiler-generated method
        int index = rankingQuestParamList.FindIndex(new Predicate<RankingQuestParam>(idsCAnonStorey2F4.\u003C\u003Em__2F5));
        RankingQuestParam rankingQuestParam = index == -1 ? (RankingQuestParam) null : rankingQuestParamList[index];
        if (rankingQuestParam != null)
        {
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          rankingQuestParamList[index] = RankingQuestScheduleParam.CompareOpenOrLatest(ref serverTime, rankingQuestParam, idsCAnonStorey2F3.list[idsCAnonStorey2F4.i]);
        }
        else
        {
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          rankingQuestParamList.Add(idsCAnonStorey2F3.list[idsCAnonStorey2F4.i]);
        }
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
