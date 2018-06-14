// Decompiled with JetBrains decompiler
// Type: SRPG.TrophyParam
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using System;
using System.Collections.Generic;

namespace SRPG
{
  public class TrophyParam
  {
    public bool[] DaysRepeat = new bool[7]{ true, true, true, true, true, true, true };
    private string localizedNameID;
    public string iname;
    public string Name;
    public string Expr;
    public string begin_at;
    public string end_at;
    public int Days;
    public TrophyCategorys Category;
    public TrophyDispType DispType;
    public string[] RequiredTrophies;
    public TrophyObjective[] Objectives;
    public int Gold;
    public int Coin;
    public int Exp;
    public int Stamina;
    public int Beginner;
    public TrophyParam.RewardItem[] Items;
    public int Challenge;
    public string ParentTrophy;
    public int help;

    protected void localizeFields(string language)
    {
      this.init();
      this.Name = LocalizedText.SGGet(language, GameUtility.LocalizedMasterParamFileName, this.localizedNameID);
    }

    protected void init()
    {
      this.localizedNameID = this.GetType().GenerateLocalizedID(this.iname, "NAME");
    }

    public bool Deserialize(string language, JSON_TrophyParam json)
    {
      bool flag = this.Deserialize(json);
      this.localizeFields(language);
      return flag;
    }

    protected void ConvertDayRepeat(int days)
    {
      if (days <= 1)
        return;
      for (int index = 0; index < this.DaysRepeat.Length; ++index)
        this.DaysRepeat[index] = (days & 1 << index + 1) > 0;
    }

    public bool IsBeginner
    {
      get
      {
        return this.Beginner == 1;
      }
    }

    public bool IsChallengeMission
    {
      get
      {
        return this.Challenge == 1;
      }
    }

    public bool IsChallengeMissionRoot
    {
      get
      {
        if (this.IsChallengeMission)
          return string.IsNullOrEmpty(this.ParentTrophy);
        return false;
      }
    }

    public bool ContainsCondition(TrophyConditionTypes c)
    {
      if (this.Objectives == null)
        return false;
      for (int index = 0; index < this.Objectives.Length; ++index)
      {
        if (this.Objectives[index].type == c)
          return true;
      }
      return false;
    }

    public bool Deserialize(JSON_TrophyParam json)
    {
      if (json == null || json.objective == null)
        return false;
      if (json.flg_quests == null)
      {
        this.RequiredTrophies = new string[0];
      }
      else
      {
        this.RequiredTrophies = new string[json.flg_quests.Length];
        for (int index = 0; index < json.flg_quests.Length; ++index)
          this.RequiredTrophies[index] = json.flg_quests[index];
      }
      this.Objectives = new TrophyObjective[json.objective.Length];
      for (int index = 0; index < json.objective.Length; ++index)
      {
        this.Objectives[index] = new TrophyObjective();
        this.Objectives[index].Param = this;
        this.Objectives[index].index = index;
        if (!this.Objectives[index].Deserialize(json.objective[index]))
          return false;
      }
      this.iname = json.iname;
      this.begin_at = json.begin_at;
      this.end_at = json.end_at;
      this.Name = json.name;
      this.Expr = json.expr;
      this.Gold = json.reward_gold;
      this.Coin = json.reward_coin;
      this.Exp = json.reward_exp;
      this.Stamina = json.reward_stamina;
      this.Days = json.day_reset & 1;
      this.ConvertDayRepeat(json.day_reset);
      this.Beginner = json.bgnr;
      this.ParentTrophy = json.parent_iname;
      this.help = json.help;
      this.Category = json.category == 0 ? TrophyCategorys.Other : (TrophyCategorys) json.category;
      this.DispType = (TrophyDispType) json.disp;
      int length = 0;
      if (!string.IsNullOrEmpty(json.reward_item1_iname) && json.reward_item1_num > 0)
        ++length;
      if (!string.IsNullOrEmpty(json.reward_item2_iname) && json.reward_item2_num > 0)
        ++length;
      if (!string.IsNullOrEmpty(json.reward_item3_iname) && json.reward_item3_num > 0)
        ++length;
      this.Items = new TrophyParam.RewardItem[length];
      int index1 = 0;
      if (!string.IsNullOrEmpty(json.reward_item1_iname) && json.reward_item1_num > 0)
      {
        this.Items[index1].iname = json.reward_item1_iname;
        this.Items[index1].Num = json.reward_item1_num;
        ++index1;
      }
      if (!string.IsNullOrEmpty(json.reward_item2_iname) && json.reward_item2_num > 0)
      {
        this.Items[index1].iname = json.reward_item2_iname;
        this.Items[index1].Num = json.reward_item2_num;
        ++index1;
      }
      if (!string.IsNullOrEmpty(json.reward_item3_iname) && json.reward_item3_num > 0)
      {
        this.Items[index1].iname = json.reward_item3_iname;
        this.Items[index1].Num = json.reward_item3_num;
        int num = index1 + 1;
      }
      return true;
    }

    public static bool CheckRequiredTrophies(GameManager gm, TrophyParam tp, bool is_end_check)
    {
      TrophyState trophyCounter1 = gm.Player.GetTrophyCounter(tp);
      if (tp.IsBeginner && !MonoSingleton<GameManager>.Instance.Player.IsBeginner() && (trophyCounter1 == null || !trophyCounter1.IsCompleted))
        return false;
      bool flag = true;
      string[] requiredTrophies = tp.RequiredTrophies;
      for (int index = 0; index < requiredTrophies.Length; ++index)
      {
        if (!string.IsNullOrEmpty(requiredTrophies[index]))
        {
          TrophyParam trophy = gm.MasterParam.GetTrophy(requiredTrophies[index]);
          if (trophy != null && is_end_check)
          {
            TrophyState trophyCounter2 = gm.Player.GetTrophyCounter(trophy);
            if (trophyCounter2 == null || !trophyCounter2.IsEnded)
            {
              flag = false;
              break;
            }
          }
        }
      }
      return flag;
    }

    public bool IsShowBadge(TrophyState state)
    {
      return state != null && !state.IsEnded && state.IsCompleted && ((!this.IsBeginner || MonoSingleton<GameManager>.Instance.Player.IsBeginner()) && (!this.IsInvisibleVip() && !this.IsInvisibleCard())) && (!this.IsInvisibleStamina() && !this.IsChallengeMission && (state.Param.DispType != TrophyDispType.Award && state.Param.DispType != TrophyDispType.Hide) && ((state.Param.RequiredTrophies == null || TrophyParam.CheckRequiredTrophies(MonoSingleton<GameManager>.Instance, state.Param, true)) && state.Param.IsAvailablePeriod(TimeManager.ServerTime, true)));
    }

    public bool IsInvisibleVip()
    {
      PlayerData player = MonoSingleton<GameManager>.Instance.Player;
      for (int index = this.Objectives.Length - 1; index >= 0 && (this.Objectives[index].type == TrophyConditionTypes.vip && !(this.Objectives[index].sval != "lv")); --index)
      {
        if (player.VipRank > 0)
        {
          if (player.VipRank != this.Objectives[index].ival)
            return true;
        }
        else if (this.Objectives[index].ival != 1)
          return true;
      }
      return false;
    }

    public bool IsInvisibleCard()
    {
      TrophyState[] trophyStates = MonoSingleton<GameManager>.Instance.Player.TrophyStates;
      bool flag = false;
      for (int index1 = this.Objectives.Length - 1; index1 >= 0; --index1)
      {
        if (this.Objectives[index1].type != TrophyConditionTypes.card)
          return false;
        flag = true;
        for (int index2 = trophyStates.Length - 1; index2 >= 0; --index2)
        {
          if (!(trophyStates[index2].iname != this.iname))
            return !trophyStates[index2].IsCompleted;
        }
      }
      return flag;
    }

    public bool IsInvisibleStamina()
    {
      int hour = TimeManager.ServerTime.Hour;
      List<int> mealHours = MonoSingleton<WatchManager>.Instance.GetMealHours();
      for (int index = this.Objectives.Length - 1; index >= 0; --index)
      {
        if (this.Objectives[index].type != TrophyConditionTypes.stamina)
          return false;
        int num1 = int.Parse(this.Objectives[index].sval.Substring(0, 2));
        int num2 = int.Parse(this.Objectives[index].sval.Substring(3, 2));
        if (num1 <= hour && hour < num2)
          return false;
        using (List<int>.Enumerator enumerator = mealHours.GetEnumerator())
        {
          while (enumerator.MoveNext())
          {
            int current = enumerator.Current;
            if (num1 <= current && current < num2)
              return false;
          }
        }
      }
      return true;
    }

    private TimeSpan GetGraceRewardSpan()
    {
      return new TimeSpan(14, 0, 0, 0);
    }

    private TimeSpan GetAvailableSpan()
    {
      return new TimeSpan(7, 0, 0, 0);
    }

    private DateTime AddTimeSpan(DateTime times, TimeSpan span)
    {
      if (times.Equals(DateTime.MaxValue))
        return times;
      try
      {
        times = times.Add(span);
      }
      catch (Exception ex)
      {
        times = DateTime.MaxValue;
      }
      return times;
    }

    private DateTime SubTimeSpan(DateTime times, TimeSpan span)
    {
      if (times.Equals(DateTime.MinValue))
        return times;
      try
      {
        times = times.Subtract(span);
      }
      catch (Exception ex)
      {
        times = DateTime.MaxValue;
      }
      return times;
    }

    public bool IsAvailablePeriod(DateTime now, bool is_grace)
    {
      DateTime minValue = DateTime.MinValue;
      DateTime times = DateTime.MaxValue;
      try
      {
        if (!string.IsNullOrEmpty(this.begin_at))
          minValue = DateTime.Parse(this.begin_at);
        if (!string.IsNullOrEmpty(this.end_at))
          times = DateTime.Parse(this.end_at);
      }
      catch
      {
        DebugUtility.LogWarning("Failed to parse date! [" + this.begin_at + "] or [" + this.end_at + "]");
        return false;
      }
      if (is_grace)
        times = this.AddTimeSpan(times, this.GetGraceRewardSpan());
      if (now < minValue || times < now)
        return false;
      if (this.Days == 1)
      {
        int index = (int) (now.DayOfWeek - 1);
        if (index < 0)
          index += 7;
        if (!this.DaysRepeat[index])
          return false;
      }
      return true;
    }

    public bool IsPlanningToUse()
    {
      DateTime serverTime = TimeManager.ServerTime;
      DateTime minValue = DateTime.MinValue;
      DateTime maxValue = DateTime.MaxValue;
      try
      {
        if (!string.IsNullOrEmpty(this.begin_at))
          minValue = DateTime.Parse(this.begin_at);
        if (!string.IsNullOrEmpty(this.end_at))
          maxValue = DateTime.Parse(this.end_at);
      }
      catch
      {
        DebugUtility.LogWarning("Failed to parse date! [" + this.begin_at + "] or [" + this.end_at + "]");
        return false;
      }
      DateTime dateTime1 = this.SubTimeSpan(minValue, this.GetAvailableSpan());
      DateTime dateTime2 = this.AddTimeSpan(maxValue, this.GetGraceRewardSpan() + this.GetAvailableSpan());
      return !(serverTime < dateTime1) && !(dateTime2 < serverTime);
    }

    public struct RewardItem
    {
      public string iname;
      public int Num;
    }
  }
}
