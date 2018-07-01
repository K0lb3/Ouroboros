// Decompiled with JetBrains decompiler
// Type: SRPG.QuestListV2
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  [FlowNode.Pin(1, "エリートクエストを表示", FlowNode.PinTypes.Input, 1)]
  [AddComponentMenu("UI/リスト/クエスト一覧")]
  [FlowNode.Pin(0, "通常クエストを表示", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(2, "エクストラクエストを表示", FlowNode.PinTypes.Input, 2)]
  [FlowNode.Pin(10, "前回選択したクエストを表示", FlowNode.PinTypes.Input, 10)]
  [FlowNode.Pin(101, "シナリオクエストが選択された", FlowNode.PinTypes.Output, 101)]
  [FlowNode.Pin(102, "鍵クエストを開ける", FlowNode.PinTypes.Output, 102)]
  [FlowNode.Pin(103, "鍵クエストを閉じる", FlowNode.PinTypes.Output, 103)]
  [FlowNode.Pin(104, "上の階層に戻る", FlowNode.PinTypes.Output, 104)]
  public class QuestListV2 : SRPG_ListBase, IFlowInterface, IWebHelp
  {
    private static Dictionary<int, float> mScrollPosCache = new Dictionary<int, float>();
    public Dictionary<string, GameObject> mListItemTemplates = new Dictionary<string, GameObject>();
    public bool Descending = true;
    public bool RefreshOnStart = true;
    public bool ShowAllQuests = true;
    private List<QuestParam> mQuests = new List<QuestParam>();
    [Description("リストアイテムとして使用するゲームオブジェクト")]
    public GameObject ItemTemplate;
    public GameObject SpecialQuestItem;
    public GameObject ExtraQuestItem;
    [Description("シナリオクエスト用のアイテムとして使用するゲームオブジェクト")]
    public GameObject ScenarioQuestItem;
    [Description("シナリオクエスト用のアイテムとして使用するゲームオブジェクト")]
    public GameObject EliteQuestItem;
    [Description("挑戦回数を使い果たしたエリートクエストのアイテムとして使用するゲームオブジェクト")]
    public GameObject EliteQuestDisAbleItem;
    [Description("シナリオのエクストラクエスト用のアイテムとして使用するゲームオブジェクト")]
    public GameObject StoryExtraQuestItem;
    [Description("挑戦回数を使い果たしたシナリオのエクストラクエスト用のアイテムとして使用するゲームオブジェクト")]
    public GameObject StoryExtraQuestDisableItem;
    [Description("挑戦回数ありのイベントリストアイテムとして使用するゲームオブジェクト")]
    public GameObject EventTemplateLimit;
    [Description("詳細画面として使用するゲームオブジェクト")]
    public GameObject DetailTemplate;
    [Description("難易度選択ボタン (Normal)")]
    public GameObject BtnNormal;
    [Description("難易度選択ボタン (Elite)")]
    public GameObject BtnElite;
    [Description("難易度選択ボタン (Extra)")]
    public GameObject BtnExtra;
    [Description("ハードクエストのブックマーク表示用ボタン")]
    public GameObject BtnEliteBookmark;
    [Description("ユニットランキング表示ボタン")]
    public GameObject BtnUnitRanking;
    private GameObject mDetailInfo;
    public ScrollRect ScrollRect;
    public GameObject AreaInfo;
    [FourCC]
    public int ListID;
    public GameObject ChapterTimer;
    public GameObject BackButton;
    private ChapterParam mCurrentChapter;
    private QuestDifficulties mDifficultyFilter;
    private int mSetScrollPos;
    private float mNewScrollPos;
    private bool mIsQuestsRefreshed;

    public void Activated(int pinID)
    {
      bool flag = false;
      switch (pinID)
      {
        case 0:
          flag = this.Refresh(QuestDifficulties.Normal);
          break;
        case 1:
          flag = this.Refresh(QuestDifficulties.Elite);
          break;
        case 2:
          flag = this.Refresh(QuestDifficulties.Extra);
          break;
        case 10:
          flag = this.Refresh(this.PlayingDifficultiy());
          break;
      }
      if (flag)
        return;
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 104);
    }

    protected override void Start()
    {
      base.Start();
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ItemTemplate, (UnityEngine.Object) null) && this.ItemTemplate.get_gameObject().get_activeInHierarchy())
        this.ItemTemplate.SetActive(false);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.SpecialQuestItem, (UnityEngine.Object) null) && this.SpecialQuestItem.get_gameObject().get_activeInHierarchy())
        this.SpecialQuestItem.SetActive(false);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ExtraQuestItem, (UnityEngine.Object) null) && this.ExtraQuestItem.get_gameObject().get_activeInHierarchy())
        this.ExtraQuestItem.SetActive(false);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.DetailTemplate, (UnityEngine.Object) null) && this.DetailTemplate.get_gameObject().get_activeInHierarchy())
        this.DetailTemplate.SetActive(false);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ScenarioQuestItem, (UnityEngine.Object) null) && this.ScenarioQuestItem.get_gameObject().get_activeInHierarchy())
        this.ScenarioQuestItem.SetActive(false);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.EliteQuestItem, (UnityEngine.Object) null) && this.EliteQuestItem.get_gameObject().get_activeInHierarchy())
        this.EliteQuestItem.SetActive(false);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.EliteQuestDisAbleItem, (UnityEngine.Object) null) && this.EliteQuestDisAbleItem.get_gameObject().get_activeInHierarchy())
        this.EliteQuestDisAbleItem.SetActive(false);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.StoryExtraQuestItem, (UnityEngine.Object) null) && this.StoryExtraQuestItem.get_gameObject().get_activeInHierarchy())
        this.StoryExtraQuestItem.SetActive(false);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.StoryExtraQuestDisableItem, (UnityEngine.Object) null) && this.StoryExtraQuestDisableItem.get_gameObject().get_activeInHierarchy())
        this.StoryExtraQuestDisableItem.SetActive(false);
      if (this.RefreshOnStart && !this.mIsQuestsRefreshed)
      {
        this.RefreshQuests();
        this.RefreshItems();
        ChapterParam mCurrentChapter = this.mCurrentChapter;
        if (mCurrentChapter != null && mCurrentChapter.IsKeyQuest() && !mCurrentChapter.IsKeyUnlock(Network.GetServerTime()))
        {
          if (mCurrentChapter.CheckHasKeyItem())
          {
            GlobalVars.KeyQuestTimeOver = true;
            FlowNode_GameObject.ActivateOutputLinks((Component) this, 102);
          }
          else
            UIUtility.SystemMessage(LocalizedText.Get("sys.KEYQUEST_UNLOCK"), LocalizedText.Get("sys.KEYQUEST_AVAILABLE_CAUTION"), new UIUtility.DialogResultEvent(this.OnCloseKeyQuest), (GameObject) null, true, -1);
        }
      }
      MonoSingleton<GameManager>.Instance.OnPlayerLvChange += new GameManager.PlayerLvChangeEvent(this.RefreshItems);
    }

    private GameObject LoadQuestListItem(QuestParam param)
    {
      if (string.IsNullOrEmpty(param.ItemLayout))
        return (GameObject) null;
      if (this.mListItemTemplates.ContainsKey(param.ItemLayout))
        return this.mListItemTemplates[param.ItemLayout];
      GameObject gameObject = AssetManager.Load<GameObject>("QuestListItems/" + param.ItemLayout);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) gameObject, (UnityEngine.Object) null))
        this.mListItemTemplates.Add(param.ItemLayout, gameObject);
      return gameObject;
    }

    protected override void OnDestroy()
    {
      base.OnDestroy();
      GameManager instanceDirect = MonoSingleton<GameManager>.GetInstanceDirect();
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) instanceDirect, (UnityEngine.Object) null))
        return;
      instanceDirect.OnPlayerLvChange -= new GameManager.PlayerLvChangeEvent(this.RefreshItems);
    }

    public bool ExistsQuest(QuestDifficulties difficulty)
    {
      return this.mQuests.Any<QuestParam>((Func<QuestParam, bool>) (q => q.difficulty == difficulty));
    }

    private bool Refresh(QuestDifficulties difficulty)
    {
      if (GlobalVars.RankingQuestSelected)
      {
        GlobalVars.RankingQuestSelected = false;
        this.mIsQuestsRefreshed = true;
        if (!this.RefreshRankingQuests())
          return false;
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.BackButton, (UnityEngine.Object) null))
          this.BackButton.SetActive(false);
      }
      else
      {
        if (!this.RefreshQuests())
          return false;
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.BackButton, (UnityEngine.Object) null))
          this.BackButton.SetActive(true);
      }
      if (difficulty == QuestDifficulties.Normal && !this.ExistsQuest(QuestDifficulties.Normal))
        difficulty = QuestDifficulties.Elite;
      if (difficulty == QuestDifficulties.Elite && !this.ExistsQuest(QuestDifficulties.Elite))
        difficulty = QuestDifficulties.Extra;
      if (difficulty == QuestDifficulties.Extra && !this.ExistsQuest(QuestDifficulties.Extra))
        difficulty = QuestDifficulties.Normal;
      this.mDifficultyFilter = difficulty;
      this.RefreshItems();
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ScrollRect, (UnityEngine.Object) null))
      {
        this.ScrollRect.set_normalizedPosition(Vector2.get_one());
        if (HomeWindow.GetRestorePoint() == RestorePoints.QuestList && !string.IsNullOrEmpty(GlobalVars.LastPlayedQuest.Get()) && ((QuestStates) GlobalVars.LastQuestState == QuestStates.Cleared && QuestListV2.mScrollPosCache.ContainsKey(this.ListID)))
        {
          this.mSetScrollPos = 2;
          this.mNewScrollPos = QuestListV2.mScrollPosCache[this.ListID];
          QuestListV2.mScrollPosCache.Remove(this.ListID);
          HomeWindow.SetRestorePoint(RestorePoints.Home);
        }
      }
      this.RefreshButtons(difficulty);
      switch (difficulty)
      {
        case QuestDifficulties.Normal:
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.BtnNormal, (UnityEngine.Object) null) && !this.ExistsQuest(QuestDifficulties.Elite) && !this.ExistsQuest(QuestDifficulties.Extra))
          {
            this.BtnNormal.SetActive(false);
            break;
          }
          break;
        case QuestDifficulties.Elite:
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.BtnElite, (UnityEngine.Object) null) && !this.ExistsQuest(QuestDifficulties.Extra) && !this.ExistsQuest(QuestDifficulties.Normal))
          {
            this.BtnElite.SetActive(false);
            break;
          }
          break;
        default:
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.BtnExtra, (UnityEngine.Object) null) && !this.ExistsQuest(QuestDifficulties.Normal) && !this.ExistsQuest(QuestDifficulties.Elite))
          {
            this.BtnExtra.SetActive(false);
            break;
          }
          break;
      }
      return true;
    }

    private bool HasEliteQuest(QuestParam q)
    {
      QuestParam[] quests = MonoSingleton<GameManager>.Instance.Quests;
      for (int index = 0; index < quests.Length; ++index)
      {
        if (quests[index].difficulty == QuestDifficulties.Elite && Array.IndexOf<string>(quests[index].cond_quests, q.iname) >= 0)
          return true;
      }
      return false;
    }

    private bool RefreshQuests()
    {
      this.mCurrentChapter = MonoSingleton<GameManager>.Instance.FindArea((string) GlobalVars.SelectedChapter);
      this.mQuests.Clear();
      QuestParam[] availableQuests = MonoSingleton<GameManager>.Instance.Player.AvailableQuests;
      for (int index = 0; index < availableQuests.Length; ++index)
      {
        QuestParam questParam = availableQuests[index];
        if (!availableQuests[index].IsMulti && (this.ShowAllQuests || this.mCurrentChapter != null && !(this.mCurrentChapter.iname != questParam.ChapterID)))
        {
          if (this.mCurrentChapter.IsGpsQuest())
          {
            if (!questParam.gps_enable || questParam.type != QuestTypes.Gps)
              continue;
          }
          else if (questParam.type == QuestTypes.Gps)
            continue;
          if (questParam.IsDateUnlock(-1L))
            this.mQuests.Add(questParam);
        }
      }
      this.RefreshChapterTimer();
      return this.mQuests.Count > 0;
    }

    private bool RefreshRankingQuests()
    {
      List<RankingQuestParam> rankingQuesstParams = MonoSingleton<GameManager>.Instance.AvailableRankingQuesstParams;
      List<QuestParam> questParamList = new List<QuestParam>();
      for (int index = 0; index < rankingQuesstParams.Count; ++index)
      {
        QuestParam quest = MonoSingleton<GameManager>.Instance.FindQuest(rankingQuesstParams[index].iname);
        if (quest != null && questParamList.Find((Predicate<QuestParam>) (q => q.iname == quest.iname)) == null)
          questParamList.Add(quest);
      }
      this.mQuests.Clear();
      for (int index = 0; index < questParamList.Count; ++index)
      {
        QuestParam questParam = questParamList[index];
        if (!questParamList[index].IsMulti && questParam.type != QuestTypes.Gps && (questParam.IsDateUnlock(-1L) && questParam.IsQuestCondition()))
          this.mQuests.Add(questParam);
      }
      this.RefreshChapterTimer();
      return this.mQuests.Count > 0;
    }

    private void RefreshItems()
    {
      Transform transform = ((Component) this).get_transform();
      this.ClearItems();
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.ItemTemplate, (UnityEngine.Object) null))
        return;
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.AreaInfo, (UnityEngine.Object) null) && !string.IsNullOrEmpty((string) GlobalVars.SelectedSection))
        DataSource.Bind<ChapterParam>(this.AreaInfo, MonoSingleton<GameManager>.Instance.FindArea((string) GlobalVars.SelectedSection));
      QuestParam[] array = this.mQuests.ToArray();
      if (this.Descending)
        Array.Reverse((Array) array);
      for (int index = 0; index < array.Length; ++index)
      {
        QuestParam questParam = array[index];
        if (questParam.difficulty == this.mDifficultyFilter)
        {
          GameObject gameObject1 = (GameObject) null;
          if (!string.IsNullOrEmpty(questParam.ItemLayout))
            gameObject1 = this.LoadQuestListItem(questParam);
          if (UnityEngine.Object.op_Equality((UnityEngine.Object) gameObject1, (UnityEngine.Object) null))
            gameObject1 = questParam.difficulty != QuestDifficulties.Elite ? (questParam.difficulty != QuestDifficulties.Extra ? (!questParam.IsScenario ? (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.SpecialQuestItem, (UnityEngine.Object) null) || !this.HasEliteQuest(questParam) ? (questParam.GetChallangeLimit() <= 0 ? (!questParam.IsExtra ? this.ItemTemplate : this.ExtraQuestItem) : this.EventTemplateLimit) : this.SpecialQuestItem) : this.ScenarioQuestItem) : (!questParam.CheckEnableChallange() ? this.StoryExtraQuestDisableItem : this.StoryExtraQuestItem)) : (!questParam.CheckEnableChallange() ? this.EliteQuestDisAbleItem : this.EliteQuestItem);
          if (!UnityEngine.Object.op_Equality((UnityEngine.Object) gameObject1, (UnityEngine.Object) null))
          {
            GameObject gameObject2 = (GameObject) UnityEngine.Object.Instantiate<GameObject>((M0) gameObject1);
            ((UnityEngine.Object) gameObject2).set_hideFlags((HideFlags) 52);
            DataSource.Bind<QuestParam>(gameObject2, questParam);
            RankingQuestParam availableRankingQuest = MonoSingleton<GameManager>.Instance.FindAvailableRankingQuest(questParam.iname);
            DataSource.Bind<RankingQuestParam>(gameObject2, availableRankingQuest);
            DataSource.Bind<QuestParam>(gameObject2, questParam);
            QuestCampaignData[] questCampaigns = MonoSingleton<GameManager>.Instance.FindQuestCampaigns(questParam);
            DataSource.Bind<QuestCampaignData[]>(gameObject2, questCampaigns.Length != 0 ? questCampaigns : (QuestCampaignData[]) null);
            ListItemEvents component = (ListItemEvents) gameObject2.GetComponent<ListItemEvents>();
            component.OnSelect = new ListItemEvents.ListItemEvent(this.OnSelectItem);
            component.OnOpenDetail = new ListItemEvents.ListItemEvent(this.OnOpenItemDetail);
            component.OnCloseDetail = new ListItemEvents.ListItemEvent(this.OnCloseItemDetail);
            gameObject2.get_transform().SetParent(transform, false);
            gameObject2.get_gameObject().SetActive(true);
            this.AddItem(component);
          }
        }
      }
    }

    private void RefreshChapterTimer()
    {
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ChapterTimer, (UnityEngine.Object) null))
        return;
      bool flag = false;
      if (this.mCurrentChapter != null)
      {
        ChapterParam mCurrentChapter = this.mCurrentChapter;
        if (mCurrentChapter != null)
        {
          DataSource.Bind<ChapterParam>(this.ChapterTimer, mCurrentChapter);
          switch (mCurrentChapter.GetKeyQuestType())
          {
            case KeyQuestTypes.Timer:
              flag = mCurrentChapter.key_end > 0L;
              break;
            case KeyQuestTypes.Count:
              flag = false;
              break;
            default:
              flag = mCurrentChapter.end > 0L;
              break;
          }
        }
      }
      this.ChapterTimer.SetActive(flag);
      GameParameter.UpdateAll(this.ChapterTimer);
    }

    private void RefreshButtons(QuestDifficulties difficulty)
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) null, (UnityEngine.Object) this.BtnNormal) || UnityEngine.Object.op_Equality((UnityEngine.Object) null, (UnityEngine.Object) this.BtnElite) || UnityEngine.Object.op_Equality((UnityEngine.Object) null, (UnityEngine.Object) this.BtnExtra))
        return;
      switch (difficulty)
      {
        case QuestDifficulties.Normal:
          this.BtnNormal.SetActive(true);
          this.BtnUnitRanking.SetActive(true);
          this.BtnEliteBookmark.SetActive(true);
          this.BtnElite.SetActive(false);
          this.BtnExtra.SetActive(false);
          break;
        case QuestDifficulties.Elite:
          this.BtnUnitRanking.SetActive(true);
          this.BtnEliteBookmark.SetActive(true);
          this.BtnNormal.SetActive(false);
          this.BtnElite.SetActive(true);
          this.BtnExtra.SetActive(false);
          break;
        case QuestDifficulties.Extra:
          this.BtnUnitRanking.SetActive(true);
          this.BtnEliteBookmark.SetActive(true);
          this.BtnNormal.SetActive(false);
          this.BtnElite.SetActive(false);
          this.BtnExtra.SetActive(true);
          break;
      }
    }

    private void OnSelectItem(GameObject go)
    {
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ScrollRect, (UnityEngine.Object) null) && this.ListID != 0)
        QuestListV2.mScrollPosCache[this.ListID] = this.ScrollRect.get_verticalNormalizedPosition();
      GlobalVars.SelectedRankingQuestParam = DataSource.FindDataOfClass<RankingQuestParam>(go, (RankingQuestParam) null);
      QuestParam dataOfClass = DataSource.FindDataOfClass<QuestParam>(go, (QuestParam) null);
      if (dataOfClass == null)
        return;
      if (dataOfClass.IsKeyQuest && !dataOfClass.IsKeyUnlock(-1L))
      {
        if (dataOfClass.Chapter != null && dataOfClass.Chapter.CheckHasKeyItem() && dataOfClass.IsDateUnlock(-1L))
        {
          GlobalVars.KeyQuestTimeOver = true;
          FlowNode_GameObject.ActivateOutputLinks((Component) this, 102);
        }
        else
          UIUtility.SystemMessage(LocalizedText.Get("sys.KEYQUEST_UNLOCK"), LocalizedText.Get("sys.KEYQUEST_AVAILABLE_CAUTION"), new UIUtility.DialogResultEvent(this.OnCloseKeyQuest), (GameObject) null, true, -1);
      }
      else
      {
        GlobalVars.SelectedQuestID = dataOfClass.iname;
        GlobalVars.LastQuestState.Set(dataOfClass.state);
        if (dataOfClass.IsScenario)
        {
          FlowNode_GameObject.ActivateOutputLinks((Component) this, 101);
        }
        else
        {
          if ((int) dataOfClass.aplv > MonoSingleton<GameManager>.Instance.Player.Lv)
            NotifyList.Push(LocalizedText.Get("sys.QUEST_AP_CONDITION", new object[1]
            {
              (object) dataOfClass.aplv
            }));
          FlowNode_OnQuestSelect objectOfType = (FlowNode_OnQuestSelect) UnityEngine.Object.FindObjectOfType<FlowNode_OnQuestSelect>();
          if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) objectOfType, (UnityEngine.Object) null))
            return;
          objectOfType.Selected();
        }
      }
    }

    private void OnCloseKeyQuest(GameObject go)
    {
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 103);
    }

    private void OnCloseItemDetail(GameObject go)
    {
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mDetailInfo, (UnityEngine.Object) null))
        return;
      UnityEngine.Object.DestroyImmediate((UnityEngine.Object) this.mDetailInfo.get_gameObject());
      this.mDetailInfo = (GameObject) null;
    }

    private void OnOpenItemDetail(GameObject go)
    {
      QuestParam dataOfClass = DataSource.FindDataOfClass<QuestParam>(go, (QuestParam) null);
      if (!UnityEngine.Object.op_Equality((UnityEngine.Object) this.mDetailInfo, (UnityEngine.Object) null) || dataOfClass == null)
        return;
      this.mDetailInfo = (GameObject) UnityEngine.Object.Instantiate<GameObject>((M0) this.DetailTemplate);
      DataSource.Bind<QuestParam>(this.mDetailInfo, dataOfClass);
      QuestCampaignData[] questCampaigns = MonoSingleton<GameManager>.Instance.FindQuestCampaigns(dataOfClass);
      DataSource.Bind<QuestCampaignData[]>(this.mDetailInfo, questCampaigns.Length != 0 ? questCampaigns : (QuestCampaignData[]) null);
      this.mDetailInfo.SetActive(true);
    }

    private QuestDifficulties PlayingDifficultiy()
    {
      QuestParam quest = MonoSingleton<GameManager>.Instance.FindQuest(GlobalVars.SelectedQuestID);
      if (quest != null)
        return quest.difficulty;
      return QuestDifficulties.Normal;
    }

    protected override void LateUpdate()
    {
      if (this.mSetScrollPos > 0)
      {
        --this.mSetScrollPos;
        if (this.mSetScrollPos == 0)
          this.ScrollRect.set_verticalNormalizedPosition(this.mNewScrollPos);
      }
      base.LateUpdate();
    }

    public bool GetHelpURL(out string url, out string title)
    {
      if (this.mCurrentChapter != null && !string.IsNullOrEmpty(this.mCurrentChapter.helpURL))
      {
        title = this.mCurrentChapter.name;
        url = this.mCurrentChapter.helpURL;
        return true;
      }
      title = (string) null;
      url = (string) null;
      return false;
    }
  }
}
