// Decompiled with JetBrains decompiler
// Type: SRPG.TrophyWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  [FlowNode.Pin(2002, "クエスト選択へ移動", FlowNode.PinTypes.Output, 2002)]
  [FlowNode.Pin(2001, "ガチャへ移動", FlowNode.PinTypes.Output, 2001)]
  [FlowNode.Pin(2000, "報酬を受け取り", FlowNode.PinTypes.Output, 2000)]
  [FlowNode.Pin(1, "更新", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(1001, "レコード", FlowNode.PinTypes.Output, 1001)]
  [FlowNode.Pin(1000, "デイリー", FlowNode.PinTypes.Output, 1000)]
  [FlowNode.Pin(0, "初期化", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(9040, "SG: FBLogin", FlowNode.PinTypes.Output, 9040)]
  [FlowNode.Pin(2031, "マルチ塔へ移動", FlowNode.PinTypes.Output, 2031)]
  [FlowNode.Pin(2030, "対戦へ移動", FlowNode.PinTypes.Output, 2030)]
  [FlowNode.Pin(2029, "レビューへ移動", FlowNode.PinTypes.Output, 2029)]
  [FlowNode.Pin(2028, "イベントショップへ移動", FlowNode.PinTypes.Output, 2028)]
  [FlowNode.Pin(2027, "限定ショップへ移動", FlowNode.PinTypes.Output, 2027)]
  [FlowNode.Pin(2026, "塔クエスト選択へ移動", FlowNode.PinTypes.Output, 2026)]
  [FlowNode.Pin(2025, "ユニット選択へ移動", FlowNode.PinTypes.Output, 2025)]
  [FlowNode.Pin(2021, "武具の店へ移動", FlowNode.PinTypes.Output, 2021)]
  [FlowNode.Pin(2020, "魂の交換所へ移動", FlowNode.PinTypes.Output, 2020)]
  [FlowNode.Pin(2013, "アンナの店へ移動", FlowNode.PinTypes.Output, 2013)]
  [FlowNode.Pin(2008, "FgGID画面へ移動", FlowNode.PinTypes.Output, 2008)]
  [FlowNode.Pin(2007, "アリーナへ移動", FlowNode.PinTypes.Output, 2007)]
  [FlowNode.Pin(2006, "イベントクエスト選択へ移動", FlowNode.PinTypes.Output, 2006)]
  [FlowNode.Pin(2005, "マルチプレイへ移動", FlowNode.PinTypes.Output, 2005)]
  [FlowNode.Pin(2004, "装備強化画面へ移動", FlowNode.PinTypes.Output, 2004)]
  [FlowNode.Pin(2003, "ゴールド購入画面へ移動", FlowNode.PinTypes.Output, 2003)]
  public class TrophyWindow : MonoBehaviour, IFlowInterface
  {
    public const int PIN_REFRESH = 1;
    public const int PIN_DAILY = 1000;
    public const int PIN_RECORD = 1001;
    public const int PIN_REWARD_CHECK = 2000;
    public const int PIN_GOTO_GACHA = 2001;
    public const int PIN_GOTO_QUEST = 2002;
    public const int PIN_GOTO_BUYGOLD = 2003;
    public const int PIN_GOTO_SOUBI = 2004;
    public const int PIN_GOTO_MULTI = 2005;
    public const int PIN_GOTO_EVENT = 2006;
    public const int PIN_GOTO_ARENA = 2007;
    public const int PIN_GOTO_FGGID = 2008;
    public const int PIN_GOTO_SHOP_NORMAL = 2013;
    public const int PIN_GOTO_SHOP_KAKERA = 2020;
    public const int PIN_GOTO_SHOP_ARTIFACT = 2021;
    public const int PIN_GOTO_UNIT = 2025;
    public const int PIN_GOTO_TOWER = 2026;
    public const int PIN_GOTO_SHOP_LIMITE = 2027;
    public const int PIN_GOTO_SHOP_EVENT = 2028;
    public const int PIN_GOTO_REVIEW = 2029;
    public const int PIN_GOTO_VERSUS = 2030;
    public const int PIN_GOTO_MULTITOWER = 2031;
    public const int PIN_GOTO_FBLOGIN = 9040;
    [SerializeField]
    private GameObject daily_badge_obj;
    [SerializeField]
    private GameObject record_badge_obj;
    [SerializeField]
    private GameObject story_badge_obj;
    [SerializeField]
    private GameObject event_badge_obj;
    [SerializeField]
    private GameObject multi_badge_obj;
    [SerializeField]
    private GameObject training_badge_obj;
    [SerializeField]
    private GameObject campaign_badge_obj;
    [SerializeField]
    private GameObject other_badge_obj;
    public Toggle[] TrophyTab;
    private Dictionary<TrophyCategorys, List<TrophyCategoryData>> trophy_record_datas;
    private Dictionary<int, TrophyCategoryData> trophy_category_datas;
    private List<TrophyState> trophy_daily_datas;
    private List<TrophyState> tmp_trophy_daily_datas;
    private List<TrophyState> trophy_ended_datas;

    public TrophyWindow()
    {
      base.\u002Ector();
    }

    public void Activated(int pinID)
    {
      switch (pinID)
      {
        case 0:
          this.Initialize();
          break;
        case 1:
          this.RefreshTrophyDatas();
          TrophyList componentInChildren = (TrophyList) ((Component) this).GetComponentInChildren<TrophyList>();
          if (!Object.op_Inequality((Object) componentInChildren, (Object) null))
            break;
          componentInChildren.RefreshLight();
          break;
      }
    }

    public void ActivateOutputLinks(int pin_id)
    {
      FlowNode_GameObject.ActivateOutputLinks((Component) this, pin_id);
    }

    private void Initialize()
    {
      this.RefreshTrophyDatas();
      bool flag = false;
      this.SetToggleIsOn(0);
      for (TrophyCategorys index1 = TrophyCategorys.Story; index1 <= TrophyCategorys.Other; ++index1)
      {
        if (this.TrophyRecordDatas.ContainsKey(index1))
        {
          if (!flag)
          {
            for (int index2 = 0; index2 < this.TrophyRecordDatas[index1].Count; ++index2)
            {
              if (this.TrophyRecordDatas[index1][index2].IsInCompletedData)
              {
                flag = true;
                this.SetToggleIsOn((int) this.TrophyCategorysToTabIndex(index1));
                break;
              }
            }
          }
          else
            break;
        }
      }
      if (GlobalVars.SelectedTrophyType == TrophyType.Daily)
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 1000);
      else
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 1001);
    }

    private bool DailyCheck()
    {
      for (int index = 0; index < this.TrophyDailyDatas.Count; ++index)
      {
        if (this.TrophyDailyDatas[index].IsCompleted)
          return true;
      }
      return false;
    }

    private TrophyWindow.TabIndex TrophyCategorysToTabIndex(TrophyCategorys category)
    {
      switch (category)
      {
        case TrophyCategorys.Story:
          return TrophyWindow.TabIndex.Story;
        case TrophyCategorys.Event:
          return TrophyWindow.TabIndex.Event;
        case TrophyCategorys.Multi:
          return TrophyWindow.TabIndex.Multi;
        case TrophyCategorys.Training:
          return TrophyWindow.TabIndex.Training;
        case TrophyCategorys.Campaign:
          return TrophyWindow.TabIndex.Campaign;
        default:
          return TrophyWindow.TabIndex.Other;
      }
    }

    private void SetToggleIsOn(int index)
    {
      for (int index1 = 0; index1 < this.TrophyTab.Length; ++index1)
      {
        if (Object.op_Implicit((Object) this.TrophyTab[index1]))
          this.TrophyTab[index1].set_isOn(index1 == index);
      }
    }

    private void RefreshBadge()
    {
      this.story_badge_obj.SetActive(false);
      this.event_badge_obj.SetActive(false);
      this.multi_badge_obj.SetActive(false);
      this.training_badge_obj.SetActive(false);
      this.campaign_badge_obj.SetActive(false);
      this.other_badge_obj.SetActive(false);
      if (Object.op_Inequality((Object) this.daily_badge_obj, (Object) null))
      {
        this.daily_badge_obj.SetActive(false);
        for (int index = 0; index < this.trophy_daily_datas.Count; ++index)
        {
          if (this.trophy_daily_datas[index].IsCompleted)
          {
            this.daily_badge_obj.SetActive(true);
            break;
          }
        }
      }
      bool flag = false;
      GameObject gameObject = (GameObject) null;
      using (Dictionary<TrophyCategorys, List<TrophyCategoryData>>.KeyCollection.Enumerator enumerator = this.trophy_record_datas.Keys.GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          TrophyCategorys current = enumerator.Current;
          for (int index = 0; index < this.trophy_record_datas[current].Count; ++index)
          {
            if (this.trophy_record_datas[current][index].IsInCompletedData)
            {
              switch (current)
              {
                case TrophyCategorys.Story:
                  gameObject = this.story_badge_obj;
                  goto label_19;
                case TrophyCategorys.Event:
                  gameObject = this.event_badge_obj;
                  goto label_19;
                case TrophyCategorys.Multi:
                  gameObject = this.multi_badge_obj;
                  goto label_19;
                case TrophyCategorys.Training:
                  gameObject = this.training_badge_obj;
                  goto label_19;
                case TrophyCategorys.Campaign:
                  gameObject = this.campaign_badge_obj;
                  goto label_19;
                case TrophyCategorys.Other:
                  gameObject = this.other_badge_obj;
                  goto label_19;
                default:
                  goto label_19;
              }
            }
          }
label_19:
          if (Object.op_Inequality((Object) gameObject, (Object) null))
          {
            flag = true;
            gameObject.SetActive(true);
            gameObject = (GameObject) null;
          }
        }
      }
      this.record_badge_obj.SetActive(flag);
    }

    public Dictionary<TrophyCategorys, List<TrophyCategoryData>> TrophyRecordDatas
    {
      get
      {
        return this.trophy_record_datas;
      }
    }

    public List<TrophyState> TrophyDailyDatas
    {
      get
      {
        return this.trophy_daily_datas;
      }
    }

    public List<TrophyState> TrophyEndedDatas
    {
      get
      {
        return this.trophy_ended_datas;
      }
    }

    private void RefreshTrophyDatas()
    {
      this.trophy_record_datas.Clear();
      this.trophy_daily_datas.Clear();
      this.trophy_ended_datas.Clear();
      this.trophy_category_datas.Clear();
      TrophyCategoryParam[] trophyCategories = MonoSingleton<GameManager>.Instance.MasterParam.TrophyCategories;
      if (trophyCategories == null)
        return;
      for (int index = 0; index < trophyCategories.Length; ++index)
      {
        if (this.IsDisplayTrophyCategory(trophyCategories[index]) && !this.trophy_category_datas.ContainsKey(trophyCategories[index].hash_code))
          this.trophy_category_datas.Add(trophyCategories[index].hash_code, new TrophyCategoryData(trophyCategories[index]));
      }
      TrophyParam[] trophies = MonoSingleton<GameManager>.Instance.Trophies;
      for (int index = 0; index < trophies.Length; ++index)
      {
        TrophyState trophyCounter = MonoSingleton<GameManager>.Instance.Player.GetTrophyCounter(trophies[index], true);
        if (!trophyCounter.Param.is_none_category_hash && this.IsDisplayTrophy(trophyCounter))
        {
          if (trophyCounter.IsEnded)
            this.trophy_ended_datas.Add(trophyCounter);
          else if (trophies[index].DispType != TrophyDispType.Hide && this.trophy_category_datas.ContainsKey(trophyCounter.Param.category_hash_code))
            this.trophy_category_datas[trophyCounter.Param.category_hash_code].AddTrophy(trophyCounter);
        }
      }
      using (Dictionary<int, TrophyCategoryData>.KeyCollection.Enumerator enumerator = this.trophy_category_datas.Keys.GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          int current = enumerator.Current;
          if (this.trophy_category_datas[current].Trophies.Count > 0)
          {
            if (!this.trophy_category_datas[current].Param.IsAvailablePeriod(TimeManager.ServerTime, false))
            {
              if (this.trophy_category_datas[current].IsInCompletedData)
              {
                for (int index = this.trophy_category_datas[current].Trophies.Count - 1; index >= 0; --index)
                {
                  if (!this.trophy_category_datas[current].Trophies[index].IsCompleted)
                    this.trophy_category_datas[current].RemoveTrophy(this.trophy_category_datas[current].Trophies[index]);
                }
              }
              else
                continue;
            }
            if (this.trophy_category_datas[current].Param.IsDaily)
            {
              for (int index = 0; index < this.trophy_category_datas[current].Trophies.Count; ++index)
              {
                if (this.trophy_category_datas[current].Trophies[index].IsCompleted)
                  this.trophy_daily_datas.Add(this.trophy_category_datas[current].Trophies[index]);
                else
                  this.tmp_trophy_daily_datas.Add(this.trophy_category_datas[current].Trophies[index]);
              }
            }
            else
            {
              if (!this.trophy_record_datas.ContainsKey(this.trophy_category_datas[current].Param.category))
                this.trophy_record_datas.Add(this.trophy_category_datas[current].Param.category, new List<TrophyCategoryData>());
              this.trophy_category_datas[current].Apply();
              this.trophy_record_datas[this.trophy_category_datas[current].Param.category].Add(this.trophy_category_datas[current]);
            }
          }
        }
      }
      this.trophy_daily_datas.AddRange((IEnumerable<TrophyState>) this.tmp_trophy_daily_datas);
      this.trophy_category_datas.Clear();
      this.tmp_trophy_daily_datas.Clear();
      this.RefreshBadge();
    }

    private bool IsDisplayTrophyCategory(TrophyCategoryParam _tcp)
    {
      return _tcp.IsAvailablePeriod(TimeManager.ServerTime, true);
    }

    private bool IsDisplayTrophy(TrophyState st)
    {
      if (st.Param.DispType == TrophyDispType.Award || st.Param.IsChallengeMission || (st.Param.IsInvisibleVip() || st.Param.IsInvisibleCard()) || (st.Param.IsInvisibleStamina() || st.Param.RequiredTrophies != null && !TrophyParam.CheckRequiredTrophies(MonoSingleton<GameManager>.Instance, st.Param, true, false)) || st.Param.IsBeginner && !MonoSingleton<GameManager>.Instance.Player.IsBeginner() && !st.IsCompleted)
        return false;
      if (!st.IsCompleted && st.Param.ContainsCondition(TrophyConditionTypes.overvsrankfloor))
      {
        if (string.IsNullOrEmpty(st.Param.Objectives[0].sval_base))
        {
          if (!MonoSingleton<GameManager>.Instance.ExistsOpenVersusTower((string) null))
            return false;
        }
        else if (!MonoSingleton<GameManager>.Instance.ExistsOpenVersusTower(st.Param.Objectives[0].sval_base))
          return false;
      }
      return (st.Param.iname.Length < 7 || !(st.Param.iname.Substring(0, 7) == "REVIEW_") || !Network.Host.Contains("eval.alchemist.gu3.jp")) && (st.Param.IsBeginner || st.Param.CategoryParam.IsOpenLinekedQuest(TimeManager.ServerTime, st.IsCompleted));
    }

    private enum TabIndex
    {
      Story,
      Event,
      Multi,
      Training,
      Campaign,
      Other,
      MAX,
    }
  }
}
