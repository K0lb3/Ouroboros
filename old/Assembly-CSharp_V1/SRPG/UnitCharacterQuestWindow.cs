// Decompiled with JetBrains decompiler
// Type: SRPG.UnitCharacterQuestWindow
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
  [FlowNode.Pin(10, "リスト切り替え", FlowNode.PinTypes.Input, 10)]
  [FlowNode.Pin(100, "クエストが選択された", FlowNode.PinTypes.Output, 100)]
  public class UnitCharacterQuestWindow : MonoBehaviour, IFlowInterface
  {
    public UnitData CurrentUnit;
    public Transform QuestList;
    public GameObject StoryQuestItemTemplate;
    public GameObject StoryQuestDisableItemTemplate;
    public GameObject PieceQuestItemTemplate;
    public GameObject PieceQuestDisableItemTemplate;
    public GameObject QuestDetailTemplate;
    public string DisableFlagName;
    public GameObject CharacterImage;
    private List<QuestParam> mQuestList;
    private List<GameObject> mStoryQuestListItems;
    private List<GameObject> mPieceQuestListItems;
    private GameObject mQuestDetail;
    public string PieceQuestWorldId;
    public Image ListToggleButton;
    public Sprite StoryListSprite;
    public Sprite PieceListSprite;
    private bool mIsStoryList;
    private bool mListRefreshing;
    private bool mIsRestore;

    public UnitCharacterQuestWindow()
    {
      base.\u002Ector();
    }

    public bool IsRestore
    {
      get
      {
        return this.mIsRestore;
      }
      set
      {
        this.mIsRestore = value;
      }
    }

    private void Start()
    {
      if (Object.op_Inequality((Object) this.StoryQuestItemTemplate, (Object) null))
        this.StoryQuestItemTemplate.get_gameObject().SetActive(false);
      if (this.IsRestore)
      {
        // ISSUE: object of a compiler-generated type is created
        // ISSUE: variable of a compiler-generated type
        UnitCharacterQuestWindow.\u003CStart\u003Ec__AnonStorey27D startCAnonStorey27D = new UnitCharacterQuestWindow.\u003CStart\u003Ec__AnonStorey27D();
        // ISSUE: reference to a compiler-generated field
        startCAnonStorey27D.lastQuestName = GlobalVars.LastPlayedQuest.Get();
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        if (startCAnonStorey27D.lastQuestName != null && !string.IsNullOrEmpty(startCAnonStorey27D.lastQuestName))
        {
          // ISSUE: reference to a compiler-generated method
          QuestParam questParam = Array.Find<QuestParam>(MonoSingleton<GameManager>.Instance.Quests, new Predicate<QuestParam>(startCAnonStorey27D.\u003C\u003Em__305));
          if (questParam != null && !string.IsNullOrEmpty(questParam.ChapterID))
            this.mIsStoryList = !(questParam.world == this.PieceQuestWorldId);
        }
      }
      this.UpdateToggleButton();
      this.RefreshQuestList();
    }

    private void CreateStoryList()
    {
      this.mQuestList.Clear();
      this.mQuestList = this.CurrentUnit.FindCondQuests();
      UnitData.CharacterQuestParam[] charaEpisodeList = this.CurrentUnit.GetCharaEpisodeList();
      QuestParam[] availableQuests = MonoSingleton<GameManager>.Instance.Player.AvailableQuests;
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      UnitCharacterQuestWindow.\u003CCreateStoryList\u003Ec__AnonStorey27E listCAnonStorey27E = new UnitCharacterQuestWindow.\u003CCreateStoryList\u003Ec__AnonStorey27E();
      // ISSUE: reference to a compiler-generated field
      listCAnonStorey27E.\u003C\u003Ef__this = this;
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      for (listCAnonStorey27E.i = 0; listCAnonStorey27E.i < this.mQuestList.Count; ++listCAnonStorey27E.i)
      {
        // ISSUE: reference to a compiler-generated field
        bool flag1 = this.mQuestList[listCAnonStorey27E.i].IsDateUnlock(-1L);
        // ISSUE: reference to a compiler-generated method
        bool flag2 = Array.Find<QuestParam>(availableQuests, new Predicate<QuestParam>(listCAnonStorey27E.\u003C\u003Em__306)) != null;
        // ISSUE: reference to a compiler-generated field
        bool flag3 = this.mQuestList[listCAnonStorey27E.i].state == QuestStates.Cleared;
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        bool flag4 = charaEpisodeList[listCAnonStorey27E.i] != null && charaEpisodeList[listCAnonStorey27E.i].IsAvailable && this.CurrentUnit.IsChQuestParentUnlocked(this.mQuestList[listCAnonStorey27E.i]);
        bool flag5 = flag1 && flag2 && !flag3;
        GameObject gameObject;
        if (flag4 || flag3)
        {
          gameObject = (GameObject) Object.Instantiate<GameObject>((M0) this.StoryQuestItemTemplate);
          Button component = (Button) gameObject.GetComponent<Button>();
          if (!Object.op_Equality((Object) component, (Object) null))
            ((Selectable) component).set_interactable(flag5);
          else
            continue;
        }
        else
          gameObject = (GameObject) Object.Instantiate<GameObject>((M0) this.StoryQuestDisableItemTemplate);
        if (!Object.op_Equality((Object) gameObject, (Object) null))
        {
          gameObject.SetActive(true);
          gameObject.get_transform().SetParent(this.QuestList, false);
          // ISSUE: reference to a compiler-generated field
          DataSource.Bind<QuestParam>(gameObject, this.mQuestList[listCAnonStorey27E.i]);
          DataSource.Bind<UnitData>(gameObject, this.CurrentUnit);
          ListItemEvents component = (ListItemEvents) gameObject.GetComponent<ListItemEvents>();
          component.OnSelect = new ListItemEvents.ListItemEvent(this.OnQuestSelect);
          component.OnOpenDetail = new ListItemEvents.ListItemEvent(this.OnOpenItemDetail);
          component.OnCloseDetail = new ListItemEvents.ListItemEvent(this.OnCloseItemDetail);
          this.mStoryQuestListItems.Add(gameObject);
        }
      }
    }

    private void CreatePieceQuest()
    {
      if (Object.op_Equality((Object) this.PieceQuestItemTemplate, (Object) null))
        return;
      GameManager instance = MonoSingleton<GameManager>.Instance;
      List<QuestParam> questParamList = new List<QuestParam>();
      QuestParam[] quests = instance.Quests;
      for (int index = 0; index < quests.Length; ++index)
      {
        if (!string.IsNullOrEmpty(quests[index].world) && quests[index].world == this.PieceQuestWorldId && (!string.IsNullOrEmpty(quests[index].ChapterID) && quests[index].ChapterID == this.CurrentUnit.UnitID))
          questParamList.Add(quests[index]);
      }
      if (questParamList.Count <= 1)
        return;
      for (int index = 0; index < questParamList.Count; ++index)
      {
        GameObject gameObject = (GameObject) Object.Instantiate<GameObject>((M0) this.PieceQuestItemTemplate);
        gameObject.SetActive(true);
        gameObject.get_transform().SetParent(this.QuestList, false);
        DataSource.Bind<QuestParam>(gameObject, questParamList[index]);
        DataSource.Bind<UnitData>(gameObject, this.CurrentUnit);
        ListItemEvents component = (ListItemEvents) gameObject.GetComponent<ListItemEvents>();
        component.OnSelect = new ListItemEvents.ListItemEvent(this.OnQuestSelect);
        component.OnOpenDetail = new ListItemEvents.ListItemEvent(this.OnOpenItemDetail);
        component.OnCloseDetail = new ListItemEvents.ListItemEvent(this.OnCloseItemDetail);
        this.mPieceQuestListItems.Add(gameObject);
      }
    }

    private void RefreshQuestList()
    {
      if (this.mListRefreshing || Object.op_Equality((Object) this.StoryQuestItemTemplate, (Object) null) || (Object.op_Equality((Object) this.StoryQuestDisableItemTemplate, (Object) null) || Object.op_Equality((Object) this.QuestList, (Object) null)))
        return;
      this.mListRefreshing = true;
      if (this.mStoryQuestListItems.Count <= 0)
        this.CreateStoryList();
      if (this.mPieceQuestListItems.Count <= 0)
        this.CreatePieceQuest();
      for (int index = 0; index < this.mStoryQuestListItems.Count; ++index)
        this.mStoryQuestListItems[index].SetActive(this.mIsStoryList);
      for (int index = 0; index < this.mPieceQuestListItems.Count; ++index)
        this.mPieceQuestListItems[index].SetActive(!this.mIsStoryList);
      DataSource.Bind<UnitData>(this.CharacterImage, this.CurrentUnit);
      this.mListRefreshing = false;
    }

    private void OnQuestSelect(GameObject button)
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      UnitCharacterQuestWindow.\u003COnQuestSelect\u003Ec__AnonStorey27F selectCAnonStorey27F = new UnitCharacterQuestWindow.\u003COnQuestSelect\u003Ec__AnonStorey27F();
      List<GameObject> gameObjectList = !this.mIsStoryList ? this.mPieceQuestListItems : this.mStoryQuestListItems;
      int index = gameObjectList.IndexOf(button.get_gameObject());
      // ISSUE: reference to a compiler-generated field
      selectCAnonStorey27F.quest = DataSource.FindDataOfClass<QuestParam>(gameObjectList[index], (QuestParam) null);
      // ISSUE: reference to a compiler-generated field
      if (selectCAnonStorey27F.quest == null)
        return;
      // ISSUE: reference to a compiler-generated field
      if (!selectCAnonStorey27F.quest.IsDateUnlock(-1L))
      {
        UIUtility.NegativeSystemMessage((string) null, LocalizedText.Get("sys.DISABLE_QUEST_DATE_UNLOCK"), (UIUtility.DialogResultEvent) null, (GameObject) null, false, -1);
      }
      else
      {
        // ISSUE: reference to a compiler-generated method
        if (Array.Find<QuestParam>(MonoSingleton<GameManager>.Instance.Player.AvailableQuests, new Predicate<QuestParam>(selectCAnonStorey27F.\u003C\u003Em__307)) == null)
        {
          UIUtility.NegativeSystemMessage((string) null, LocalizedText.Get("sys.DISABLE_QUEST_CHALLENGE"), (UIUtility.DialogResultEvent) null, (GameObject) null, false, -1);
        }
        else
        {
          // ISSUE: reference to a compiler-generated field
          GlobalVars.SelectedQuestID = selectCAnonStorey27F.quest.iname;
          FlowNode_GameObject.ActivateOutputLinks((Component) this, 100);
        }
      }
    }

    private void OnCloseItemDetail(GameObject go)
    {
      if (!Object.op_Inequality((Object) this.mQuestDetail, (Object) null))
        return;
      Object.DestroyImmediate((Object) this.mQuestDetail.get_gameObject());
      this.mQuestDetail = (GameObject) null;
    }

    private void OnOpenItemDetail(GameObject go)
    {
      QuestParam dataOfClass = DataSource.FindDataOfClass<QuestParam>(go, (QuestParam) null);
      if (!Object.op_Equality((Object) this.mQuestDetail, (Object) null) || dataOfClass == null)
        return;
      this.mQuestDetail = (GameObject) Object.Instantiate<GameObject>((M0) this.QuestDetailTemplate);
      DataSource.Bind<QuestParam>(this.mQuestDetail, dataOfClass);
      DataSource.Bind<UnitData>(this.mQuestDetail, this.CurrentUnit);
      this.mQuestDetail.SetActive(true);
    }

    private void OnToggleButton()
    {
      if (this.mListRefreshing)
        return;
      this.mIsStoryList = !this.mIsStoryList;
      this.UpdateToggleButton();
      this.RefreshQuestList();
    }

    private void UpdateToggleButton()
    {
      if (!Object.op_Inequality((Object) this.ListToggleButton, (Object) null))
        return;
      this.ListToggleButton.set_sprite(!this.mIsStoryList ? this.PieceListSprite : this.StoryListSprite);
    }

    public void Activated(int pinID)
    {
      if (pinID != 10)
        return;
      this.OnToggleButton();
    }
  }
}
