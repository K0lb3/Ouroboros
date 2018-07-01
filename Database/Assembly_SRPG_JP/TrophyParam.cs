// Decompiled with JetBrains decompiler
// Type: SRPG.TrophyParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;
using System;
using System.Collections.Generic;

namespace SRPG
{
  public class TrophyParam
  {
    public bool is_none_category_hash = true;
    public int category_hash_code;
    public string iname;
    public string Name;
    public string Expr;
    public string Category;
    public TrophyCategoryParam CategoryParam;
    public ChallengeCategoryParam ChallengeCategoryParam;
    public TrophyDispType DispType;
    public string[] RequiredTrophies;
    public TrophyObjective[] Objectives;
    public int Gold;
    public int Coin;
    public int Exp;
    public int Stamina;
    public TrophyParam.RewardItem[] Items;
    public TrophyParam.RewardItem[] Artifacts;
    public int Challenge;
    public string ParentTrophy;
    public int help;
    public TrophyParam.RewardItem[] ConceptCards;

    public bool IsBeginner
    {
      get
      {
        if (this.CategoryParam != null)
          return this.CategoryParam.IsBeginner;
        return false;
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

    public bool IsDaily
    {
      get
      {
        if (this.CategoryParam != null)
          return this.CategoryParam.IsDaily;
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
      if (json == null)
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
      this.Objectives = new TrophyObjective[1];
      for (int index = 0; index < 1; ++index)
      {
        this.Objectives[index] = new TrophyObjective();
        this.Objectives[index].Param = this;
        this.Objectives[index].index = index;
        this.Objectives[index].type = (TrophyConditionTypes) json.type;
        this.Objectives[index].ival = json.ival;
        if (json.sval != null)
          this.Objectives[index].sval = new List<string>((IEnumerable<string>) json.sval);
      }
      this.iname = json.iname;
      this.Name = json.name;
      this.Expr = json.expr;
      this.Gold = json.reward_gold;
      this.Coin = json.reward_coin;
      this.Exp = json.reward_exp;
      this.Stamina = json.reward_stamina;
      this.ParentTrophy = json.parent_iname;
      this.help = json.help;
      if (!string.IsNullOrEmpty(json.category))
      {
        this.category_hash_code = json.category.GetHashCode();
        this.is_none_category_hash = false;
      }
      this.Category = json.category;
      this.DispType = (TrophyDispType) json.disp;
      this.Items = TrophyParam.InitializeItems(json);
      this.Artifacts = TrophyParam.InitializeArtifacts(json);
      this.ConceptCards = TrophyParam.InitializeConceptCards(json);
      return true;
    }

    private static TrophyParam.RewardItem[] InitializeItems(JSON_TrophyParam json)
    {
      List<TrophyParam.RewardItem> rewardItemList = new List<TrophyParam.RewardItem>();
      if (!string.IsNullOrEmpty(json.reward_item1_iname) && json.reward_item1_num > 0)
        rewardItemList.Add(new TrophyParam.RewardItem()
        {
          iname = json.reward_item1_iname,
          Num = json.reward_item1_num
        });
      if (!string.IsNullOrEmpty(json.reward_item2_iname) && json.reward_item2_num > 0)
        rewardItemList.Add(new TrophyParam.RewardItem()
        {
          iname = json.reward_item2_iname,
          Num = json.reward_item2_num
        });
      if (!string.IsNullOrEmpty(json.reward_item3_iname) && json.reward_item3_num > 0)
        rewardItemList.Add(new TrophyParam.RewardItem()
        {
          iname = json.reward_item3_iname,
          Num = json.reward_item3_num
        });
      return rewardItemList.ToArray();
    }

    private static TrophyParam.RewardItem[] InitializeArtifacts(JSON_TrophyParam json)
    {
      List<TrophyParam.RewardItem> rewardItemList = new List<TrophyParam.RewardItem>();
      if (!string.IsNullOrEmpty(json.reward_artifact1_iname) && json.reward_artifact1_num > 0)
        rewardItemList.Add(new TrophyParam.RewardItem()
        {
          iname = json.reward_artifact1_iname,
          Num = json.reward_artifact1_num
        });
      if (!string.IsNullOrEmpty(json.reward_artifact2_iname) && json.reward_artifact2_num > 0)
        rewardItemList.Add(new TrophyParam.RewardItem()
        {
          iname = json.reward_artifact2_iname,
          Num = json.reward_artifact2_num
        });
      if (!string.IsNullOrEmpty(json.reward_artifact3_iname) && json.reward_artifact3_num > 0)
        rewardItemList.Add(new TrophyParam.RewardItem()
        {
          iname = json.reward_artifact3_iname,
          Num = json.reward_artifact3_num
        });
      return rewardItemList.ToArray();
    }

    private static TrophyParam.RewardItem[] InitializeConceptCards(JSON_TrophyParam json)
    {
      List<TrophyParam.RewardItem> result = new List<TrophyParam.RewardItem>();
      Action<string, int> action = (Action<string, int>) ((iname, num) =>
      {
        if (string.IsNullOrEmpty(iname) || num <= 0)
          return;
        result.Add(new TrophyParam.RewardItem()
        {
          iname = iname,
          Num = num
        });
      });
      action(json.reward_cc_1_iname, json.reward_cc_1_num);
      action(json.reward_cc_2_iname, json.reward_cc_2_num);
      return result.ToArray();
    }

    public static bool CheckRequiredTrophies(GameManager gm, TrophyParam tp, bool is_end_check, bool is_beginner_check = true)
    {
      if (is_beginner_check)
      {
        TrophyState trophyCounter = gm.Player.GetTrophyCounter(tp, false);
        if (tp.IsBeginner && !MonoSingleton<GameManager>.Instance.Player.IsBeginner() && (trophyCounter == null || !trophyCounter.IsCompleted))
          return false;
      }
      bool flag = true;
      string[] requiredTrophies = tp.RequiredTrophies;
      for (int index = 0; index < requiredTrophies.Length; ++index)
      {
        if (!string.IsNullOrEmpty(requiredTrophies[index]))
        {
          TrophyParam trophy = gm.MasterParam.GetTrophy(requiredTrophies[index]);
          if (trophy != null && is_end_check)
          {
            TrophyState trophyCounter = gm.Player.GetTrophyCounter(trophy, false);
            if (trophyCounter == null || !trophyCounter.IsEnded)
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
      return state != null && !state.IsEnded && state.IsCompleted && ((!this.IsBeginner || MonoSingleton<GameManager>.Instance.Player.IsBeginner()) && (!this.IsInvisibleVip() && !this.IsInvisibleCard())) && (!this.IsInvisibleStamina() && !this.IsChallengeMission && (state.Param.DispType != TrophyDispType.Award && state.Param.DispType != TrophyDispType.Hide) && ((state.Param.RequiredTrophies == null || TrophyParam.CheckRequiredTrophies(MonoSingleton<GameManager>.Instance, state.Param, true, true)) && state.Param.IsAvailablePeriod(TimeManager.ServerTime, true)));
    }

    public bool IsInvisibleVip()
    {
      PlayerData player = MonoSingleton<GameManager>.Instance.Player;
      for (int index = this.Objectives.Length - 1; index >= 0 && (this.Objectives[index].type == TrophyConditionTypes.vip && !(this.Objectives[index].sval_base != "lv")); --index)
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
        int num1 = int.Parse(this.Objectives[index].sval_base.Substring(0, 2));
        int num2 = int.Parse(this.Objectives[index].sval_base.Substring(3, 2));
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
      if (this.IsChallengeMission)
        return this.ChallengeCategoryParam.IsAvailablePeriod(now);
      if (this.CategoryParam == null)
        return false;
      return this.CategoryParam.IsAvailablePeriod(now, is_grace);
    }

    public bool IsPlanningToUse()
    {
      if (this.IsChallengeMission)
        return true;
      if (this.CategoryParam == null)
        return false;
      DateTime serverTime = TimeManager.ServerTime;
      DateTime dateTimes1 = this.CategoryParam.begin_at.DateTimes;
      DateTime dateTimes2 = this.CategoryParam.end_at.DateTimes;
      DateTime dateTime1 = this.SubTimeSpan(dateTimes1, this.GetAvailableSpan());
      DateTime dateTime2 = this.AddTimeSpan(dateTimes2, this.GetGraceRewardSpan() + this.GetAvailableSpan());
      return !(serverTime < dateTime1) && !(dateTime2 < serverTime);
    }

    public DateTime GetGraceRewardTime()
    {
      if (this.CategoryParam == null)
        return new DateTime();
      DateTime dateTime = this.CategoryParam.end_at.DateTimes;
      if (this.IsBeginner)
      {
        DateTime beginnerEndTime = MonoSingleton<GameManager>.Instance.Player.GetBeginnerEndTime();
        dateTime = !(dateTime <= beginnerEndTime) ? beginnerEndTime : dateTime;
      }
      if (!this.IsBeginner)
        dateTime = this.CategoryParam.GetQuestTime(dateTime, false);
      return this.AddTimeSpan(dateTime, this.GetGraceRewardSpan());
    }

    public struct RewardItem
    {
      public string iname;
      public int Num;
    }
  }
}
