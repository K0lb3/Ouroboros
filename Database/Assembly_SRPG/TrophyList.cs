// Decompiled with JetBrains decompiler
// Type: SRPG.TrophyList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class TrophyList : SRPG_ListBase, IFlowInterface
  {
    private List<TrophyRecordPullView> child_pull_viewes = new List<TrophyRecordPullView>();
    private List<TrophyParam> child_plates = new List<TrophyParam>();
    private float FOCUS_EFFECT_SECOND = 0.2f;
    private List<TrophyRecordPullView> click_target = new List<TrophyRecordPullView>();
    private const int CREATE_ENDED_PLATE_COUNT = 50;
    private const int CREATE_PULL_VIEW_COUNT_MAX = 50;
    public TrophyWindow trophy_window;
    public TrophyList.TrophyTypes TrophyType;
    public TrophyCategorys TrophyCategory;
    public TrophyRecordPullView OriginalPullView;
    public ListItemEvents Item_Normal;
    public ListItemEvents Item_Completed;
    public ListItemEvents Item_Ended;
    public GameObject DetailWindow;
    public ListItemEvents Item_Review;
    public ListItemEvents Item_FollowTwitter;
    public RectTransform parent_panel_rect;
    [SerializeField]
    private LayoutElement view_port_handle;
    private RectTransform view_port_handle_rect;
    private RectTransform scroll_trans_rect;
    public bool RefreshOnStart;
    private bool mStarted;
    private CanvasGroup mCanvasGroup;
    private QuestTypes g_quest_type;
    private bool is_busy;
    private Vector2 start_pos;
    private Vector2 target_pos;
    private ScrollRect scroll_rect;
    private float total_close_item_size;
    private float focus_delay_time;
    private float elapsed_time;
    private TrophyList.eState state;

    public TrophyWindow Trophy_Window
    {
      get
      {
        if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.trophy_window, (UnityEngine.Object) null))
          this.trophy_window = (TrophyWindow) ((Component) this).GetComponentInParent<TrophyWindow>();
        return this.trophy_window;
      }
    }

    private RectTransform ScrollTransRect
    {
      get
      {
        if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.scroll_trans_rect, (UnityEngine.Object) null))
          this.scroll_trans_rect = this.GetRectTransform();
        return this.scroll_trans_rect;
      }
    }

    public void Activated(int pinID)
    {
    }

    private void Awake()
    {
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.Item_Normal, (UnityEngine.Object) null) && ((Component) this.Item_Normal).get_gameObject().get_activeInHierarchy())
        ((Component) this.Item_Normal).get_gameObject().SetActive(false);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.Item_Review, (UnityEngine.Object) null) && ((Component) this.Item_Review).get_gameObject().get_activeInHierarchy())
        ((Component) this.Item_Review).get_gameObject().SetActive(false);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.Item_FollowTwitter, (UnityEngine.Object) null) && ((Component) this.Item_FollowTwitter).get_gameObject().get_activeInHierarchy())
        ((Component) this.Item_FollowTwitter).get_gameObject().SetActive(false);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.Item_Completed, (UnityEngine.Object) null) && ((Component) this.Item_Completed).get_gameObject().get_activeInHierarchy())
        ((Component) this.Item_Completed).get_gameObject().SetActive(false);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.Item_Ended, (UnityEngine.Object) null) && ((Component) this.Item_Ended).get_gameObject().get_activeInHierarchy())
        ((Component) this.Item_Ended).get_gameObject().SetActive(false);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.DetailWindow, (UnityEngine.Object) null) && this.DetailWindow.get_activeInHierarchy())
        this.DetailWindow.SetActive(false);
      this.mCanvasGroup = (CanvasGroup) ((Component) this).GetComponent<CanvasGroup>();
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.mCanvasGroup, (UnityEngine.Object) null))
        this.mCanvasGroup = (CanvasGroup) ((Component) this).get_gameObject().AddComponent<CanvasGroup>();
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.scroll_rect, (UnityEngine.Object) null))
        this.scroll_rect = (ScrollRect) ((Component) this).GetComponentInParent<ScrollRect>();
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.view_port_handle, (UnityEngine.Object) null))
        return;
      this.view_port_handle_rect = (RectTransform) ((Component) this.view_port_handle).GetComponent<RectTransform>();
    }

    protected override void OnDestroy()
    {
      base.OnDestroy();
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) HomeWindow.Current, (UnityEngine.Object) null))
        return;
      HomeWindow.Current.UnlockContents();
    }

    protected override void Start()
    {
      base.Start();
      this.mStarted = true;
      if (!this.RefreshOnStart)
        return;
      this.Refresh();
    }

    private void OnTrophyReset()
    {
      this.Refresh();
    }

    private void OnEnable()
    {
      this.ScrollTransRect.set_anchoredPosition(Vector2.get_zero());
      this.Refresh();
      MonoSingleton<GameManager>.Instance.OnDayChange += new GameManager.DayChangeEvent(this.OnTrophyReset);
    }

    private void OnDisable()
    {
      this.ClearAllItems();
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) MonoSingleton<GameManager>.GetInstanceDirect(), (UnityEngine.Object) null))
        return;
      MonoSingleton<GameManager>.Instance.OnDayChange -= new GameManager.DayChangeEvent(this.OnTrophyReset);
    }

    private void Update()
    {
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mCanvasGroup, (UnityEngine.Object) null) || (double) this.mCanvasGroup.get_alpha() >= 1.0)
        return;
      this.mCanvasGroup.set_alpha(Mathf.Clamp01(this.mCanvasGroup.get_alpha() + Time.get_unscaledDeltaTime() * 3.333333f));
    }

    protected override void LateUpdate()
    {
      base.LateUpdate();
      this.Run();
    }

    private void ClearAllItems()
    {
      for (int index = 0; index < this.child_pull_viewes.Count; ++index)
        this.child_pull_viewes[index].ClearItems();
      this.ClearItems();
    }

    private void RefreshTrophyRecordLight()
    {
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mCanvasGroup, (UnityEngine.Object) null))
        this.mCanvasGroup.set_alpha(0.0f);
      if (!this.Trophy_Window.TrophyRecordDatas.ContainsKey(this.TrophyCategory))
      {
        this.ClearAllItems();
      }
      else
      {
        List<TrophyCategoryData> trophyRecordData = this.Trophy_Window.TrophyRecordDatas[this.TrophyCategory];
        if (trophyRecordData == null || trophyRecordData.Count <= 0)
        {
          this.ClearAllItems();
        }
        else
        {
          int count1 = this.child_pull_viewes.Count;
          for (int index1 = 0; index1 < trophyRecordData.Count; ++index1)
          {
            if (!trophyRecordData[index1].Param.IsNotPull)
            {
              for (int index2 = 0; index2 < this.child_pull_viewes.Count; ++index2)
              {
                if (trophyRecordData[index1].Param.hash_code == this.child_pull_viewes[index2].HashCode)
                {
                  this.child_pull_viewes[index2].SetCategoryData(trophyRecordData[index1]);
                  --count1;
                }
              }
            }
          }
          if (count1 != 0)
          {
            this.Refresh();
          }
          else
          {
            int count2 = this.child_plates.Count;
            for (int index1 = 0; index1 < trophyRecordData.Count; ++index1)
            {
              if (trophyRecordData[index1].Param.IsNotPull)
              {
                for (int index2 = 0; index2 < trophyRecordData[index1].Trophies.Count; ++index2)
                {
                  for (int index3 = 0; index3 < this.child_plates.Count; ++index3)
                  {
                    if (!(trophyRecordData[index1].Trophies[index2].Param.iname != this.child_plates[index3].iname))
                      --count2;
                  }
                }
              }
            }
            if (count2 != 0)
            {
              this.Refresh();
            }
            else
            {
              for (int index = 0; index < this.child_pull_viewes.Count; ++index)
                this.child_pull_viewes[index].Refresh(this.ScrollTransRect);
            }
          }
        }
      }
    }

    public void RefreshLight()
    {
      if (this.TrophyType == TrophyList.TrophyTypes.Normal)
        this.RefreshTrophyRecordLight();
      else
        this.Refresh();
    }

    private void Refresh()
    {
      if (!this.mStarted)
        return;
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mCanvasGroup, (UnityEngine.Object) null))
        this.mCanvasGroup.set_alpha(0.0f);
      this.ClearAllItems();
      switch (this.TrophyType)
      {
        case TrophyList.TrophyTypes.Normal:
          this.RefreshTrophyRecord();
          break;
        case TrophyList.TrophyTypes.Daily:
          this.RefreshTrophySimple(this.Trophy_Window.TrophyDailyDatas, -1, false, true);
          break;
        case TrophyList.TrophyTypes.All:
          this.RefreshTrophySimple(this.Trophy_Window.TrophyEndedDatas, 50, true, false);
          break;
      }
      this.ScrollTransRect.set_anchoredPosition(Vector2.get_zero());
    }

    private void RefreshTrophyRecord()
    {
      if (!this.Trophy_Window.TrophyRecordDatas.ContainsKey(this.TrophyCategory))
        return;
      List<TrophyCategoryData> trophyRecordData = this.Trophy_Window.TrophyRecordDatas[this.TrophyCategory];
      if (trophyRecordData == null || trophyRecordData.Count <= 0)
        return;
      int num = 50;
      Dictionary<int, TrophyRecordPullView> dictionary = new Dictionary<int, TrophyRecordPullView>();
      this.child_pull_viewes.Clear();
      for (int index = 0; index < trophyRecordData.Count && num != 0; ++index)
      {
        if (!dictionary.ContainsKey(trophyRecordData[index].Param.hash_code))
        {
          if (!trophyRecordData[index].Param.IsNotPull)
          {
            TrophyRecordPullView trophyRecordPullView = this.MakeTrophyCategory(trophyRecordData[index].Param.name);
            if (UnityEngine.Object.op_Inequality((UnityEngine.Object) trophyRecordPullView, (UnityEngine.Object) null))
            {
              --num;
              trophyRecordPullView.Setup(dictionary.Count, this);
              dictionary.Add(trophyRecordData[index].Param.hash_code, trophyRecordPullView);
              this.child_pull_viewes.Add(trophyRecordPullView);
              this.AddItem((ListItemEvents) ((Component) trophyRecordPullView).GetComponent<ListItemEvents>());
              if (MonoSingleton<GameManager>.Instance.IsTutorial() && MonoSingleton<GameManager>.Instance.GetNextTutorialStep() == "ShowMissionFilter")
              {
                if (trophyRecordData[index].Param.iname == "BEGINNER_MISSIONS")
                {
                  SGHighlightObject.Instance().highlightedObject = ((Component) trophyRecordPullView).get_gameObject();
                  SGHighlightObject.Instance().Highlight(string.Empty, "sg_tut_1.003b", (SGHighlightObject.OnActivateCallback) null, EventDialogBubble.Anchors.BottomLeft, true, false, false);
                }
                else if (trophyRecordData[index].Param.iname == "PLAYER_LEVEL")
                {
                  SGHighlightObject.Instance().highlightedObject = ((Component) this.trophy_window.TrophyTab[5]).get_gameObject();
                  SGHighlightObject.Instance().Highlight(string.Empty, "sg_tut_1.003c", (SGHighlightObject.OnActivateCallback) null, EventDialogBubble.Anchors.BottomLeft, true, false, false);
                }
              }
            }
          }
          else
            continue;
        }
        dictionary[trophyRecordData[index].Param.hash_code].SetCategoryData(trophyRecordData[index]);
        if (dictionary.Count >= 50)
          break;
      }
      this.child_plates.Clear();
      for (int index1 = 0; index1 < trophyRecordData.Count && num != 0; ++index1)
      {
        if (trophyRecordData[index1].Param.IsNotPull)
        {
          for (int index2 = 0; index2 < trophyRecordData[index1].Trophies.Count; ++index2)
          {
            ListItemEvents listItemEvents = this.MakeTrophyPlate(trophyRecordData[index1].Trophies[index2], trophyRecordData[index1].Trophies[index2].IsCompleted);
            if (UnityEngine.Object.op_Inequality((UnityEngine.Object) listItemEvents, (UnityEngine.Object) null))
            {
              this.child_plates.Add(DataSource.FindDataOfClass<TrophyParam>(((Component) listItemEvents).get_gameObject(), (TrophyParam) null));
              this.AddItem(listItemEvents);
              --num;
            }
          }
        }
      }
      using (Dictionary<int, TrophyRecordPullView>.KeyCollection.Enumerator enumerator = dictionary.Keys.GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          int current = enumerator.Current;
          dictionary[current].RefreshDisplayParam();
        }
      }
      ((Component) this.view_port_handle).get_transform().SetAsLastSibling();
    }

    public void RefreshTrophySimple(List<TrophyState> _trophies, int _create_count, bool _need_sort, bool _daily_comp_check)
    {
      if (_trophies == null || _trophies.Count <= 0)
        return;
      int num = _create_count;
      int[] numArray = (int[]) null;
      if (_need_sort)
        numArray = this.GetSortedIndex(_trophies);
      if (_daily_comp_check)
        MonoSingleton<GameManager>.Instance.Player.DailyAllCompleteCheck();
      this.child_plates.Clear();
      for (int index1 = 0; index1 < _trophies.Count && num != 0; ++index1)
      {
        int index2 = !_need_sort ? index1 : numArray[index1];
        ListItemEvents listItemEvents = this.MakeTrophyPlate(_trophies[index2], _trophies[index2].IsCompleted);
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) listItemEvents, (UnityEngine.Object) null))
        {
          this.child_plates.Add(DataSource.FindDataOfClass<TrophyParam>(((Component) listItemEvents).get_gameObject(), (TrophyParam) null));
          this.AddItem(listItemEvents);
          --num;
        }
      }
    }

    private int[] GetSortedIndex(List<TrophyState> _trophies)
    {
      int[] numArray1 = new int[_trophies.Count];
      for (int index = 0; index < _trophies.Count; ++index)
        numArray1[index] = index;
      ulong[] numArray2 = new ulong[_trophies.Count];
      for (int index = 0; index < _trophies.Count; ++index)
      {
        DateTime dateTime = _trophies[index] == null ? DateTime.MinValue : _trophies[index].RewardedAt;
        numArray2[index] = (ulong) ((long) ((ulong) dateTime.Year % 100UL) * 10000000000L + (long) ((ulong) dateTime.Month % 100UL) * 100000000L + (long) ((ulong) dateTime.Day % 100UL) * 1000000L + (long) ((ulong) dateTime.Hour % 100UL) * 10000L + (long) ((ulong) dateTime.Minute % 100UL) * 100L) + (ulong) dateTime.Second % 100UL;
      }
      Dictionary<int, ulong> dictionary = new Dictionary<int, ulong>();
      dictionary.Keys.CopyTo(numArray1, 0);
      dictionary.Values.CopyTo(numArray2, 0);
      Array.Sort<ulong, int>(numArray2, numArray1);
      Array.Reverse((Array) numArray1);
      return numArray1;
    }

    private TrophyRecordPullView MakeTrophyCategory(string _title_str)
    {
      TrophyRecordPullView trophyRecordPullView = (TrophyRecordPullView) UnityEngine.Object.Instantiate<TrophyRecordPullView>((M0) this.OriginalPullView);
      trophyRecordPullView.Init(_title_str);
      ((Component) trophyRecordPullView).get_transform().SetParent(((Component) this).get_transform(), false);
      return trophyRecordPullView;
    }

    public ListItemEvents MakeTrophyPlate(TrophyState st, bool is_achievement)
    {
      ListItemEvents listItemEvents1 = !st.IsEnded ? (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.Item_FollowTwitter, (UnityEngine.Object) null) || !st.Param.ContainsCondition(TrophyConditionTypes.followtwitter) ? (!st.IsCompleted ? this.Item_Normal : this.Item_Completed) : this.Item_FollowTwitter) : this.Item_Ended;
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) listItemEvents1, (UnityEngine.Object) null))
        return (ListItemEvents) null;
      ListItemEvents listItemEvents2 = (ListItemEvents) UnityEngine.Object.Instantiate<ListItemEvents>((M0) listItemEvents1);
      DataSource.Bind<TrophyParam>(((Component) listItemEvents2).get_gameObject(), st.Param);
      ((Component) listItemEvents2).get_transform().SetParent(((Component) this).get_transform(), false);
      listItemEvents2.OnOpenDetail = new ListItemEvents.ListItemEvent(this.OnItemDetail);
      listItemEvents2.OnSelect = new ListItemEvents.ListItemEvent(this.OnItemSelect);
      SRPG_Button componentInChildren = (SRPG_Button) ((Component) listItemEvents2).GetComponentInChildren<SRPG_Button>();
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) componentInChildren, (UnityEngine.Object) null) && !st.IsEnded)
      {
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.Item_FollowTwitter, (UnityEngine.Object) null) && st.Param.ContainsCondition(TrophyConditionTypes.followtwitter))
          this.FollowBtnSetting(componentInChildren);
        else if (st.IsCompleted)
          this.AchievementBtnSetting(componentInChildren);
        else
          this.ChallengeBtnSetting(componentInChildren, st.Param);
      }
      RewardData data = new RewardData(st.Param);
      DataSource.Bind<RewardData>(((Component) listItemEvents2).get_gameObject(), data);
      ((Component) listItemEvents2).get_gameObject().SetActive(true);
      return listItemEvents2;
    }

    private void OnItemSelect(GameObject go)
    {
      TrophyParam dataOfClass = DataSource.FindDataOfClass<TrophyParam>(go, (TrophyParam) null);
      if (dataOfClass == null)
        return;
      TrophyState trophyCounter = MonoSingleton<GameManager>.Instance.Player.GetTrophyCounter(dataOfClass, true);
      if (!trophyCounter.IsEnded && trophyCounter.IsCompleted)
      {
        if (dataOfClass.IsInvisibleStamina() || !dataOfClass.IsAvailablePeriod(TimeManager.ServerTime, true))
        {
          UIUtility.NegativeSystemMessage((string) null, LocalizedText.Get("sys.TROPHY_OUTDATED"), (UIUtility.DialogResultEvent) null, (GameObject) null, false, -1);
          this.Refresh();
        }
        else
        {
          GlobalVars.SelectedTrophy.Set(dataOfClass.iname);
          RewardData rewardData = new RewardData(dataOfClass);
          GlobalVars.LastReward.Set(rewardData);
          GlobalVars.UnitGetReward = new UnitGetParam(rewardData.Items.ToArray());
          this.trophy_window.ActivateOutputLinks(2000);
        }
      }
      else
      {
        QuestParam questParam = new QuestParam();
        switch (dataOfClass.Objectives[0].type)
        {
          case TrophyConditionTypes.winquest:
          case TrophyConditionTypes.playerlv:
          case TrophyConditionTypes.winquestsoldier:
          case TrophyConditionTypes.losequest:
          case TrophyConditionTypes.damage_over:
          case TrophyConditionTypes.complete_all_quest_mission:
          case TrophyConditionTypes.has_gold_over:
            QuestTypes quest_type1 = QuestTypes.Story;
            if (!questParam.TransSectionGotoQuest(dataOfClass.Objectives[0].sval_base, out quest_type1, new UIUtility.DialogResultEvent(this.MsgBoxJumpToQuest)))
            {
              this.g_quest_type = quest_type1;
              break;
            }
            QuestTypes questTypes1 = quest_type1;
            switch (questTypes1)
            {
              case QuestTypes.Event:
              case QuestTypes.Gps:
                this.trophy_window.ActivateOutputLinks(2006);
                return;
              case QuestTypes.Tower:
                this.trophy_window.ActivateOutputLinks(2026);
                return;
              default:
                if (questTypes1 != QuestTypes.Multi)
                {
                  if (questTypes1 != QuestTypes.Beginner)
                  {
                    this.trophy_window.ActivateOutputLinks(2002);
                    return;
                  }
                  goto case QuestTypes.Event;
                }
                else
                {
                  this.trophy_window.ActivateOutputLinks(2005);
                  return;
                }
            }
          case TrophyConditionTypes.winelite:
          case TrophyConditionTypes.loseelite:
            if (!questParam.TransSectionGotoElite(new UIUtility.DialogResultEvent(this.MsgBoxJumpToQuest)))
            {
              this.g_quest_type = QuestTypes.Story;
              break;
            }
            this.trophy_window.ActivateOutputLinks(2002);
            break;
          case TrophyConditionTypes.winevent:
          case TrophyConditionTypes.loseevent:
          case TrophyConditionTypes.exclear_fire:
          case TrophyConditionTypes.exclear_water:
          case TrophyConditionTypes.exclear_wind:
          case TrophyConditionTypes.exclear_thunder:
          case TrophyConditionTypes.exclear_light:
          case TrophyConditionTypes.exclear_dark:
          case TrophyConditionTypes.exclear_fire_nocon:
          case TrophyConditionTypes.exclear_water_nocon:
          case TrophyConditionTypes.exclear_wind_nocon:
          case TrophyConditionTypes.exclear_thunder_nocon:
          case TrophyConditionTypes.exclear_light_nocon:
          case TrophyConditionTypes.exclear_dark_nocon:
            questParam.GotoEventListQuest((string) null, (string) null);
            this.trophy_window.ActivateOutputLinks(2006);
            break;
          case TrophyConditionTypes.gacha:
          case TrophyConditionTypes.collectunits:
            this.trophy_window.ActivateOutputLinks(2001);
            break;
          case TrophyConditionTypes.multiplay:
          case TrophyConditionTypes.winmulti:
          case TrophyConditionTypes.winmultimore:
          case TrophyConditionTypes.winmultiless:
            this.trophy_window.ActivateOutputLinks(2005);
            break;
          case TrophyConditionTypes.ability:
          case TrophyConditionTypes.changeability:
          case TrophyConditionTypes.makeabilitylevel:
            this.trophy_window.ActivateOutputLinks(2025);
            break;
          case TrophyConditionTypes.soubi:
            this.GotoEquip();
            break;
          case TrophyConditionTypes.buygold:
            this.trophy_window.ActivateOutputLinks(2003);
            break;
          case TrophyConditionTypes.arena:
          case TrophyConditionTypes.winarena:
          case TrophyConditionTypes.losearena:
          case TrophyConditionTypes.becomearenarank:
          case TrophyConditionTypes.overarenarank:
            this.GotoArena();
            break;
          case TrophyConditionTypes.review:
            this.trophy_window.ActivateOutputLinks(2029);
            break;
          case TrophyConditionTypes.fggid:
            this.trophy_window.ActivateOutputLinks(2008);
            break;
          case TrophyConditionTypes.unitlevel:
          case TrophyConditionTypes.evolutionnum:
          case TrophyConditionTypes.joblevel:
          case TrophyConditionTypes.upunitlevel:
          case TrophyConditionTypes.makeunitlevel:
            this.trophy_window.ActivateOutputLinks(2025);
            break;
          case TrophyConditionTypes.unitequip:
          case TrophyConditionTypes.upjoblevel:
          case TrophyConditionTypes.makejoblevel:
          case TrophyConditionTypes.totaljoblv11:
            this.trophy_window.ActivateOutputLinks(2025);
            break;
          case TrophyConditionTypes.limitbreak:
          case TrophyConditionTypes.evoltiontimes:
          case TrophyConditionTypes.changejob:
          case TrophyConditionTypes.totalunitlvs:
            this.trophy_window.ActivateOutputLinks(2025);
            break;
          case TrophyConditionTypes.buyatshop:
            this.GotoShop(dataOfClass);
            break;
          case TrophyConditionTypes.artifacttransmute:
          case TrophyConditionTypes.artifactstrength:
          case TrophyConditionTypes.artifactevolution:
          case TrophyConditionTypes.upartifactlevel:
          case TrophyConditionTypes.makeartifactlevel:
            this.trophy_window.ActivateOutputLinks(2021);
            break;
          case TrophyConditionTypes.wintower:
          case TrophyConditionTypes.losetower:
          case TrophyConditionTypes.tower:
          case TrophyConditionTypes.overtowerscore:
            QuestTypes quest_type2 = QuestTypes.Tower;
            if (!questParam.TransSectionGotoTower(dataOfClass.Objectives[0].sval_base, out quest_type2))
              break;
            QuestTypes questTypes2 = quest_type2;
            switch (questTypes2)
            {
              case QuestTypes.Gps:
              case QuestTypes.Beginner:
                this.trophy_window.ActivateOutputLinks(2006);
                return;
              default:
                if (questTypes2 != QuestTypes.Event)
                {
                  this.trophy_window.ActivateOutputLinks(2026);
                  return;
                }
                goto case QuestTypes.Gps;
            }
          case TrophyConditionTypes.vs:
          case TrophyConditionTypes.vswin:
          case TrophyConditionTypes.overvsrankfloor:
            this.GotoVersus();
            break;
          case TrophyConditionTypes.winstory_extra:
            if (!questParam.TransSectionGotoStoryExtra(new UIUtility.DialogResultEvent(this.MsgBoxJumpToQuest)))
            {
              this.g_quest_type = QuestTypes.Story;
              break;
            }
            this.trophy_window.ActivateOutputLinks(2002);
            break;
          case TrophyConditionTypes.multitower_help:
          case TrophyConditionTypes.multitower:
            this.GotoMultiTower();
            break;
          case TrophyConditionTypes.fblogin:
            this.trophy_window.ActivateOutputLinks(9040);
            break;
        }
      }
    }

    private void OnItemDetail(GameObject go)
    {
      TrophyParam dataOfClass = DataSource.FindDataOfClass<TrophyParam>(go, (TrophyParam) null);
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.DetailWindow, (UnityEngine.Object) null) || dataOfClass == null)
        return;
      GameObject gameObject = (GameObject) UnityEngine.Object.Instantiate<GameObject>((M0) this.DetailWindow);
      DataSource.Bind<TrophyParam>(gameObject, dataOfClass);
      RewardData data = new RewardData(dataOfClass);
      DataSource.Bind<RewardData>(gameObject, data);
      gameObject.SetActive(true);
    }

    private void GotoArena()
    {
      PlayerData player = MonoSingleton<GameManager>.Instance.Player;
      if (player.CheckUnlock(UnlockTargets.Arena))
        this.trophy_window.ActivateOutputLinks(2007);
      else
        LevelLock.ShowLockMessage(player.Lv, player.VipRank, UnlockTargets.Arena);
    }

    private void GotoVersus()
    {
      PlayerData player = MonoSingleton<GameManager>.Instance.Player;
      if (player.CheckUnlock(UnlockTargets.MultiVS))
        this.trophy_window.ActivateOutputLinks(2030);
      else
        LevelLock.ShowLockMessage(player.Lv, player.VipRank, UnlockTargets.MultiVS);
    }

    private void GotoMultiTower()
    {
      GlobalVars.ReqEventPageListType = GlobalVars.EventQuestListType.Tower;
      this.trophy_window.ActivateOutputLinks(2031);
    }

    private void GotoEquip()
    {
      PlayerData player = MonoSingleton<GameManager>.Instance.Player;
      if (player.CheckUnlock(UnlockTargets.EnhanceEquip))
        this.trophy_window.ActivateOutputLinks(2004);
      else
        LevelLock.ShowLockMessage(player.Lv, player.VipRank, UnlockTargets.EnhanceEquip);
    }

    private void GotoShop(TrophyParam param)
    {
      PlayerData player = MonoSingleton<GameManager>.Instance.Player;
      EShopType type;
      if (string.IsNullOrEmpty(param.Objectives[0].sval_base))
      {
        type = EShopType.Normal;
      }
      else
      {
        char[] chArray = new char[1]{ ',' };
        string[] strArray = param.Objectives[0].sval_base.Split(chArray);
        type = !string.IsNullOrEmpty(strArray[0]) ? (EShopType) MonoSingleton<GameManager>.Instance.MasterParam.GetShopType(strArray[0]) : EShopType.Normal;
      }
      if (type >= EShopType.Normal && player.CheckUnlockShopType(type))
      {
        switch (type)
        {
          case EShopType.Normal:
          case EShopType.Tabi:
          case EShopType.Kimagure:
            this.trophy_window.ActivateOutputLinks(2013);
            break;
          case EShopType.Arena:
            this.GotoArena();
            break;
          case EShopType.Multi:
            this.trophy_window.ActivateOutputLinks(2005);
            break;
          case EShopType.AwakePiece:
            this.trophy_window.ActivateOutputLinks(2020);
            break;
          case EShopType.Artifact:
            this.trophy_window.ActivateOutputLinks(2021);
            break;
          case EShopType.Event:
            this.trophy_window.ActivateOutputLinks(2028);
            break;
          case EShopType.Limited:
            this.trophy_window.ActivateOutputLinks(2027);
            break;
        }
      }
      if (type < EShopType.Normal)
        return;
      if (MonoSingleton<GameManager>.Instance.Player.CheckUnlockShopType(type))
        return;
      try
      {
        UnlockTargets unlockTargets = type.ToUnlockTargets();
        LevelLock.ShowLockMessage(player.Lv, player.VipRank, unlockTargets);
      }
      catch (Exception ex)
      {
      }
    }

    private void ChallengeBtnSetting(SRPG_Button btn, TrophyParam trophy)
    {
      bool flag = true;
      switch (trophy.Objectives[0].type)
      {
        case TrophyConditionTypes.killenemy:
        case TrophyConditionTypes.getitem:
        case TrophyConditionTypes.vip:
        case TrophyConditionTypes.stamina:
        case TrophyConditionTypes.card:
        case TrophyConditionTypes.logincount:
        case TrophyConditionTypes.childrencomp:
        case TrophyConditionTypes.dailyall:
          flag = false;
          break;
      }
      if (trophy.DispType == TrophyDispType.HideChallenge)
        flag = false;
      ((Component) btn).get_gameObject().SetActive(flag);
      if (!flag)
      {
        VerticalLayoutGroup component = (VerticalLayoutGroup) ((Component) ((Component) btn).get_transform().get_parent()).GetComponent<VerticalLayoutGroup>();
        if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
          return;
        ((LayoutGroup) component).set_childAlignment((TextAnchor) 7);
      }
      else
      {
        Text componentInChildren = (Text) ((Component) ((Component) btn).get_transform()).GetComponentInChildren<Text>();
        if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) componentInChildren, (UnityEngine.Object) null))
          return;
        componentInChildren.set_text(LocalizedText.Get("sys.TROPHY_BTN_GO"));
      }
    }

    private void AchievementBtnSetting(SRPG_Button btn)
    {
      Text componentInChildren = (Text) ((Component) ((Component) btn).get_transform()).GetComponentInChildren<Text>();
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) componentInChildren, (UnityEngine.Object) null))
        return;
      componentInChildren.set_text(LocalizedText.Get("sys.TROPHY_BTN_CLEAR"));
    }

    private void GlobalVideoAdsBtnSetting(SRPG_Button btn)
    {
      Text componentInChildren = (Text) ((Component) ((Component) btn).get_transform()).GetComponentInChildren<Text>();
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) componentInChildren, (UnityEngine.Object) null))
        return;
      componentInChildren.set_text(LocalizedText.Get("sys.TROPHY_BTN_GO"));
    }

    private void FollowBtnSetting(SRPG_Button btn)
    {
      Text componentInChildren = (Text) ((Component) ((Component) btn).get_transform()).GetComponentInChildren<Text>();
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) componentInChildren, (UnityEngine.Object) null))
        return;
      componentInChildren.set_text(LocalizedText.Get("sys.TROPHY_BTN_FOLLOWTWITTER"));
    }

    private void MsgBoxJumpToQuest(GameObject go)
    {
      QuestTypes gQuestType = this.g_quest_type;
      switch (gQuestType)
      {
        case QuestTypes.Event:
        case QuestTypes.Gps:
          this.trophy_window.ActivateOutputLinks(2006);
          break;
        case QuestTypes.Tower:
          this.trophy_window.ActivateOutputLinks(2026);
          break;
        default:
          if (gQuestType != QuestTypes.Multi)
          {
            if (gQuestType != QuestTypes.Beginner)
            {
              this.trophy_window.ActivateOutputLinks(2002);
              break;
            }
            goto case QuestTypes.Event;
          }
          else
          {
            this.trophy_window.ActivateOutputLinks(2005);
            break;
          }
      }
    }

    private void Run()
    {
      switch (this.state)
      {
        case TrophyList.eState.IDLE:
          this.UpdateIdle();
          break;
        case TrophyList.eState.CLOSE_OTHER_WAIT:
          this.UpdateCloseOtherWait();
          break;
        case TrophyList.eState.FOCUS:
          this.UpdateFocus();
          break;
        case TrophyList.eState.FOCUS_INTERVAL:
          this.UpdateFocusInterval();
          break;
        case TrophyList.eState.CREATE_PULL_ITEM:
          this.UpdateCreatePullItems();
          break;
        case TrophyList.eState.OPEN:
          this.UpdateOpen();
          break;
        case TrophyList.eState.CLOSE:
          this.UpdateClose();
          break;
      }
    }

    private void ChangeState(TrophyList.eState _new_state)
    {
      if (this.state == _new_state)
        return;
      switch (this.state)
      {
        case TrophyList.eState.IDLE:
          this.EndIdle();
          break;
        case TrophyList.eState.CLOSE_OTHER_WAIT:
          this.EndCloseOtherWait();
          break;
        case TrophyList.eState.FOCUS:
          this.EndFocus();
          break;
        case TrophyList.eState.OPEN:
          this.EndOpen();
          break;
        case TrophyList.eState.CLOSE:
          this.EndClose();
          break;
      }
      this.state = _new_state;
      switch (this.state)
      {
        case TrophyList.eState.IDLE:
          this.StartIdle();
          break;
        case TrophyList.eState.CLOSE_OTHER_WAIT:
          this.StartCloseOtherWait();
          break;
        case TrophyList.eState.FOCUS:
          this.StartFocus();
          break;
        case TrophyList.eState.CREATE_PULL_ITEM:
          this.StartCreatePullItems();
          break;
        case TrophyList.eState.OPEN:
          this.StartOpen();
          break;
        case TrophyList.eState.CLOSE:
          this.StartClose();
          break;
      }
    }

    public void SetClickTarget(TrophyRecordPullView _pull_view)
    {
      if (this.is_busy)
        return;
      this.is_busy = true;
      this.click_target.Add(_pull_view);
    }

    private void CheckTarget()
    {
      if (this.click_target.Count <= 0)
        return;
      if (this.click_target[0].IsStateClosed)
      {
        if (!this.OpenPullView())
          return;
        this.scroll_rect.StopMovement();
      }
      else
      {
        if (!this.click_target[0].IsStateOpened || !this.ClosePullView())
          return;
        this.scroll_rect.StopMovement();
      }
    }

    private bool OpenPullView()
    {
      if (this.child_pull_viewes == null || this.child_pull_viewes.Count <= 0)
      {
        this.ChangeState(TrophyList.eState.FOCUS);
        return true;
      }
      this.ChangeState(TrophyList.eState.CLOSE_OTHER_WAIT);
      return true;
    }

    private bool ClosePullView()
    {
      this.ChangeState(TrophyList.eState.CLOSE);
      return true;
    }

    private void StartCloseOtherWait()
    {
      this.total_close_item_size = 0.0f;
      for (int index = 0; index < this.child_pull_viewes.Count; ++index)
      {
        if (!UnityEngine.Object.op_Equality((UnityEngine.Object) this.child_pull_viewes[index], (UnityEngine.Object) this.click_target[0]) && this.child_pull_viewes[index].IsStateOpen)
        {
          this.child_pull_viewes[index].ChangeState(TrophyRecordPullView.eState.CLOSE_IMMEDIATE);
          if (this.child_pull_viewes[index].Index < this.click_target[0].Index)
            this.total_close_item_size += this.child_pull_viewes[index].RootLayoutElementMinHeightDef;
        }
      }
    }

    private void EndCloseOtherWait()
    {
      this.ScrollTransRect.set_anchoredPosition(new Vector2((float) this.ScrollTransRect.get_anchoredPosition().x, Mathf.Max(0.0f, (float) this.ScrollTransRect.get_anchoredPosition().y - this.total_close_item_size)));
    }

    private void UpdateCloseOtherWait()
    {
      for (int index = 0; index < this.child_pull_viewes.Count; ++index)
      {
        if (!UnityEngine.Object.op_Equality((UnityEngine.Object) this.child_pull_viewes[index], (UnityEngine.Object) this.click_target[0]) && !this.child_pull_viewes[index].IsStateClosed)
          return;
      }
      this.ChangeState(TrophyList.eState.FOCUS);
    }

    private void StartFocus()
    {
      this.focus_delay_time = 0.2f;
      this.elapsed_time = 0.0f;
      this.start_pos = this.ScrollTransRect.get_anchoredPosition();
      this.target_pos = new Vector2((float) this.ScrollTransRect.get_anchoredPosition().x, (float) this.click_target[0].Index * this.click_target[0].ItemDistance);
    }

    private void EndFocus()
    {
      this.ScrollTransRect.set_anchoredPosition(new Vector2(0.0f, (float) this.target_pos.y));
    }

    private void UpdateFocus()
    {
      this.focus_delay_time -= Time.get_deltaTime();
      if ((double) this.focus_delay_time > 0.0)
      {
        this.ScrollTransRect.set_anchoredPosition(this.start_pos);
      }
      else
      {
        this.elapsed_time += Time.get_deltaTime();
        this.elapsed_time = Mathf.Min(this.elapsed_time, this.FOCUS_EFFECT_SECOND);
        this.ScrollTransRect.set_anchoredPosition(new Vector2(0.0f, (float) Vector2.Lerp(this.start_pos, this.target_pos, this.elapsed_time / this.FOCUS_EFFECT_SECOND).y));
        if ((double) this.elapsed_time < (double) this.FOCUS_EFFECT_SECOND)
          return;
        this.ChangeState(TrophyList.eState.FOCUS_INTERVAL);
      }
    }

    private void UpdateFocusInterval()
    {
      this.ChangeState(TrophyList.eState.CREATE_PULL_ITEM);
    }

    private void StartCreatePullItems()
    {
      this.click_target[0].CreateContents();
      float num1 = (float) (this.ScrollTransRect.get_anchoredPosition().y + this.view_port_handle_rect.get_anchoredPosition().y) - this.click_target[0].TargetViewPortSize;
      Rect rect = this.parent_panel_rect.get_rect();
      // ISSUE: explicit reference operation
      float num2 = Mathf.Max(0.0f, ((Rect) @rect).get_height() + num1 - this.click_target[0].VerticalLayoutSpacing);
      this.view_port_handle.set_minHeight((double) this.view_port_handle.get_minHeight() < (double) num2 ? num2 : this.view_port_handle.get_minHeight());
    }

    private void UpdateCreatePullItems()
    {
      this.ChangeState(TrophyList.eState.OPEN);
    }

    private void StartOpen()
    {
      this.click_target[0].StartOpen();
    }

    private void EndOpen()
    {
    }

    private void UpdateOpen()
    {
      if (!this.click_target[0].IsStateOpened)
        return;
      this.ChangeState(TrophyList.eState.IDLE);
      this.click_target.Clear();
    }

    private void StartClose()
    {
      LayoutElement viewPortHandle = this.view_port_handle;
      viewPortHandle.set_minHeight(viewPortHandle.get_minHeight() + this.click_target[0].TargetViewPortSize);
      this.click_target[0].StartClose();
    }

    protected void EndClose()
    {
      this.scroll_rect.set_verticalNormalizedPosition(Mathf.Clamp(this.scroll_rect.get_verticalNormalizedPosition(), 0.0f, 1f));
    }

    protected void UpdateClose()
    {
      this.scroll_rect.set_verticalNormalizedPosition(Mathf.Clamp(this.scroll_rect.get_verticalNormalizedPosition(), 0.0f, 1f));
      if (!this.click_target[0].IsStateClosed)
        return;
      this.ChangeState(TrophyList.eState.IDLE);
      this.click_target.Clear();
    }

    private void UpdateHandleHeight()
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.view_port_handle, (UnityEngine.Object) null) || (double) this.view_port_handle.get_minHeight() <= 0.0)
        return;
      float num = (float) (this.ScrollTransRect.get_anchoredPosition().y + this.view_port_handle_rect.get_anchoredPosition().y);
      Rect rect = this.parent_panel_rect.get_rect();
      // ISSUE: explicit reference operation
      this.view_port_handle.set_minHeight(Mathf.Clamp(((Rect) @rect).get_height() + num, 0.0f, this.view_port_handle.get_minHeight()));
    }

    private void StartIdle()
    {
      this.is_busy = false;
      SRPG_TouchInputModule.UnlockInput(false);
    }

    private void EndIdle()
    {
      SRPG_TouchInputModule.LockInput();
    }

    private void UpdateIdle()
    {
      this.UpdateHandleHeight();
      this.CheckTarget();
    }

    private enum eState
    {
      IDLE,
      CLOSE_OTHER_WAIT,
      FOCUS,
      FOCUS_INTERVAL,
      CREATE_PULL_ITEM,
      OPEN,
      CLOSE,
    }

    public enum TrophyTypes
    {
      Normal,
      Daily,
      All,
    }
  }
}
