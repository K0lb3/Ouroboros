// Decompiled with JetBrains decompiler
// Type: SRPG.QuestListV2
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  [AddComponentMenu("UI/リスト/クエスト一覧")]
  [FlowNode.Pin(102, "鍵クエストを開ける", FlowNode.PinTypes.Output, 102)]
  [FlowNode.Pin(103, "鍵クエストを閉じる", FlowNode.PinTypes.Output, 103)]
  [FlowNode.Pin(0, "通常クエストを表示", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(1, "エリートクエストを表示", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(10, "前回選択したクエストを表示", FlowNode.PinTypes.Input, 10)]
  [FlowNode.Pin(101, "シナリオクエストが選択された", FlowNode.PinTypes.Output, 101)]
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
    [Description("シナリオクエスト用のアイテムとして使用するゲームオブジェクト")]
    public GameObject ScenarioQuestItem;
    [Description("シナリオクエスト用のアイテムとして使用するゲームオブジェクト")]
    public GameObject EliteQuestItem;
    [Description("挑戦回数を使い果たしたエリートクエストのアイテムとして使用するゲームオブジェクト")]
    public GameObject EliteQuestDisAbleItem;
    [Description("挑戦回数ありのイベントリストアイテムとして使用するゲームオブジェクト")]
    public GameObject EventTemplateLimit;
    [Description("詳細画面として使用するゲームオブジェクト")]
    public GameObject DetailTemplate;
    [Description("難易度選択ボタン (Normal)")]
    public GameObject BtnNormal;
    [Description("難易度選択ボタン (Elite)")]
    public GameObject BtnElite;
    private GameObject mDetailInfo;
    public ScrollRect ScrollRect;
    public GameObject AreaInfo;
    [FourCC]
    public int ListID;
    public GameObject ChapterTimer;
    private ChapterParam mCurrentChapter;
    private QuestDifficulties mDifficultyFilter;
    private int mSetScrollPos;
    private float mNewScrollPos;
    private bool isTriggeredRefresh;

    public void Activated(int pinID)
    {
      if (!((Component) this).get_gameObject().get_activeInHierarchy())
        return;
      switch (pinID)
      {
        case 0:
          this.Refresh(QuestDifficulties.Normal);
          break;
        case 1:
          this.Refresh(QuestDifficulties.Elite);
          break;
        case 10:
          this.Refresh(this.PlayingDifficultiy());
          break;
      }
    }

    protected override void Start()
    {
      base.Start();
      if (Object.op_Inequality((Object) this.ItemTemplate, (Object) null) && this.ItemTemplate.get_gameObject().get_activeInHierarchy())
        this.ItemTemplate.SetActive(false);
      if (Object.op_Inequality((Object) this.SpecialQuestItem, (Object) null) && this.SpecialQuestItem.get_gameObject().get_activeInHierarchy())
        this.SpecialQuestItem.SetActive(false);
      if (Object.op_Inequality((Object) this.DetailTemplate, (Object) null) && this.DetailTemplate.get_gameObject().get_activeInHierarchy())
        this.DetailTemplate.SetActive(false);
      if (Object.op_Inequality((Object) this.ScenarioQuestItem, (Object) null) && this.ScenarioQuestItem.get_gameObject().get_activeInHierarchy())
        this.ScenarioQuestItem.SetActive(false);
      if (Object.op_Inequality((Object) this.EliteQuestItem, (Object) null) && this.EliteQuestItem.get_gameObject().get_activeInHierarchy())
        this.EliteQuestItem.SetActive(false);
      if (Object.op_Inequality((Object) this.EliteQuestDisAbleItem, (Object) null) && this.EliteQuestDisAbleItem.get_gameObject().get_activeInHierarchy())
        this.EliteQuestDisAbleItem.SetActive(false);
      if (this.RefreshOnStart)
      {
        this.RefreshQuests();
        this.RefreshItems();
        GlobalVars.QuestDifficulty = QuestDifficulties.Normal;
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
      if (Object.op_Inequality((Object) gameObject, (Object) null))
        this.mListItemTemplates.Add(param.ItemLayout, gameObject);
      return gameObject;
    }

    protected override void OnDestroy()
    {
      base.OnDestroy();
      GameManager instanceDirect = MonoSingleton<GameManager>.GetInstanceDirect();
      if (!Object.op_Inequality((Object) instanceDirect, (Object) null))
        return;
      instanceDirect.OnPlayerLvChange -= new GameManager.PlayerLvChangeEvent(this.RefreshItems);
    }

    public int CountQuests(QuestDifficulties difficulty)
    {
      int num = 0;
      for (int index = 0; index < this.mQuests.Count; ++index)
      {
        if (this.mQuests[index].difficulty == difficulty)
          ++num;
      }
      return num;
    }

    public void Refresh(QuestDifficulties difficulty)
    {
      this.RefreshQuests();
      if (difficulty == QuestDifficulties.Elite && this.CountQuests(QuestDifficulties.Elite) <= 0)
        difficulty = QuestDifficulties.Normal;
      this.mDifficultyFilter = difficulty;
      GlobalVars.QuestDifficulty = difficulty;
      this.isTriggeredRefresh = true;
      this.RefreshItems();
      if (Object.op_Inequality((Object) this.ScrollRect, (Object) null))
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
      if (Object.op_Inequality((Object) this.BtnElite, (Object) null))
        this.BtnElite.SetActive(true);
      if (!Object.op_Inequality((Object) this.BtnNormal, (Object) null))
        return;
      this.BtnNormal.SetActive(true);
      if (this.mQuests.Count <= 0 || this.CountQuests(QuestDifficulties.Elite) > 0)
        return;
      ((Selectable) this.BtnNormal.GetComponentInChildren<Button>()).set_interactable(false);
      ((Behaviour) this.BtnNormal.GetComponentInChildren<Button>()).set_enabled(false);
      M0 componentInChildren = this.BtnNormal.GetComponentInChildren<Image>();
      ColorBlock colors = ((Selectable) this.BtnNormal.GetComponentInChildren<Button>()).get_colors();
      // ISSUE: explicit reference operation
      Color normalColor = ((ColorBlock) @colors).get_normalColor();
      ((Graphic) componentInChildren).set_color(normalColor);
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

    private void RefreshQuests()
    {
      this.mCurrentChapter = MonoSingleton<GameManager>.Instance.FindArea((string) GlobalVars.SelectedChapter);
      this.mQuests.Clear();
      QuestParam[] availableQuests = MonoSingleton<GameManager>.Instance.Player.AvailableQuests;
      for (int index = 0; index < availableQuests.Length; ++index)
      {
        QuestParam questParam = availableQuests[index];
        if (!availableQuests[index].IsMulti && (this.ShowAllQuests || this.mCurrentChapter != null && !(this.mCurrentChapter.iname != questParam.ChapterID)) && questParam.IsDateUnlock(-1L))
          this.mQuests.Add(questParam);
      }
      this.RefreshChapterTimer();
    }

    private void RefreshItems()
    {
      Transform transform = ((Component) this).get_transform();
      this.ClearItems();
      if (Object.op_Equality((Object) this.ItemTemplate, (Object) null))
        return;
      if (Object.op_Inequality((Object) this.AreaInfo, (Object) null) && !string.IsNullOrEmpty((string) GlobalVars.SelectedSection))
        DataSource.Bind<ChapterParam>(this.AreaInfo, MonoSingleton<GameManager>.Instance.FindArea((string) GlobalVars.SelectedSection));
      QuestParam[] array = this.mQuests.ToArray();
      if (this.Descending)
        Array.Reverse((Array) array);
      bool flag = false;
      GameManager instance = MonoSingleton<GameManager>.Instance;
      if (this.isTriggeredRefresh && (instance.Player.TutorialFlags & 1L) == 0L)
        flag = instance.GetNextTutorialStep() == "ShowQuestItem";
      for (int index = 0; index < array.Length; ++index)
      {
        QuestParam questParam = array[index];
        if (questParam.difficulty == this.mDifficultyFilter)
        {
          GameObject gameObject1 = (GameObject) null;
          if (!string.IsNullOrEmpty(questParam.ItemLayout))
            gameObject1 = this.LoadQuestListItem(questParam);
          if (Object.op_Equality((Object) gameObject1, (Object) null))
            gameObject1 = questParam.difficulty != QuestDifficulties.Elite ? (!questParam.IsScenario ? (!Object.op_Inequality((Object) this.SpecialQuestItem, (Object) null) || !this.HasEliteQuest(questParam) ? (questParam.GetChallangeLimit() <= 0 ? this.ItemTemplate : this.EventTemplateLimit) : this.SpecialQuestItem) : this.ScenarioQuestItem) : (!questParam.CheckEnableChallange() ? this.EliteQuestDisAbleItem : this.EliteQuestItem);
          if (!Object.op_Equality((Object) gameObject1, (Object) null))
          {
            GameObject gameObject2 = (GameObject) Object.Instantiate<GameObject>((M0) gameObject1);
            ((Object) gameObject2).set_hideFlags((HideFlags) 52);
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
            if (flag && questParam.iname == "QE_ST_NO_010001")
            {
              SGHighlightObject.Instance().highlightedObject = ((Component) component).get_gameObject();
              SGHighlightObject.Instance().Highlight(string.Empty, "sg_tut_1.034", (SGHighlightObject.OnActivateCallback) null, EventDialogBubble.Anchors.BottomRight, true, false, false);
              component.OnOpenDetail = (ListItemEvents.ListItemEvent) null;
            }
          }
        }
      }
    }

    private void RefreshChapterTimer()
    {
      if (!Object.op_Inequality((Object) this.ChapterTimer, (Object) null))
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
      if (Object.op_Equality((Object) null, (Object) this.BtnNormal) || Object.op_Equality((Object) null, (Object) this.BtnElite))
        return;
      switch (difficulty)
      {
        case QuestDifficulties.Normal:
          ((Selectable) this.BtnNormal.GetComponentInChildren<Button>()).set_interactable(true);
          ((Selectable) this.BtnElite.GetComponentInChildren<Button>()).set_interactable(false);
          break;
        case QuestDifficulties.Elite:
          ((Selectable) this.BtnNormal.GetComponentInChildren<Button>()).set_interactable(false);
          ((Selectable) this.BtnElite.GetComponentInChildren<Button>()).set_interactable(true);
          break;
      }
    }

    private void OnSelectItem(GameObject go)
    {
      if (Object.op_Inequality((Object) this.ScrollRect, (Object) null) && this.ListID != 0)
        QuestListV2.mScrollPosCache[this.ListID] = this.ScrollRect.get_verticalNormalizedPosition();
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
          FlowNode_OnQuestSelect objectOfType = (FlowNode_OnQuestSelect) Object.FindObjectOfType<FlowNode_OnQuestSelect>();
          if (!Object.op_Inequality((Object) objectOfType, (Object) null))
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
      if (!Object.op_Inequality((Object) this.mDetailInfo, (Object) null))
        return;
      Object.DestroyImmediate((Object) this.mDetailInfo.get_gameObject());
      this.mDetailInfo = (GameObject) null;
    }

    private void OnOpenItemDetail(GameObject go)
    {
      QuestParam dataOfClass = DataSource.FindDataOfClass<QuestParam>(go, (QuestParam) null);
      if (!Object.op_Equality((Object) this.mDetailInfo, (Object) null) || dataOfClass == null)
        return;
      this.mDetailInfo = (GameObject) Object.Instantiate<GameObject>((M0) this.DetailTemplate);
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
      return GlobalVars.QuestDifficulty;
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
