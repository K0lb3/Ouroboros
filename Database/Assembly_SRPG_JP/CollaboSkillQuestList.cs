// Decompiled with JetBrains decompiler
// Type: SRPG.CollaboSkillQuestList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  [FlowNode.Pin(10, "リスト切り替え", FlowNode.PinTypes.Input, 10)]
  [FlowNode.Pin(100, "クエストが選択された", FlowNode.PinTypes.Output, 100)]
  public class CollaboSkillQuestList : MonoBehaviour, IFlowInterface
  {
    public UnitData CurrentUnit1;
    public UnitData CurrentUnit2;
    public Transform QuestList;
    public GameObject StoryQuestItemTemplate;
    public GameObject StoryQuestDisableItemTemplate;
    public GameObject QuestDetailTemplate;
    public string DisableFlagName;
    public GameObject CharacterImage1;
    public GameObject CharacterImage2;
    private List<GameObject> mStoryQuestListItems;
    private GameObject mQuestDetail;
    public Image ListToggleButton;
    public Sprite StoryListSprite;
    private bool mIsStoryList;
    private bool mListRefreshing;
    private bool mIsRestore;

    public CollaboSkillQuestList()
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

    protected virtual void Start()
    {
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.StoryQuestItemTemplate, (UnityEngine.Object) null))
        this.StoryQuestItemTemplate.get_gameObject().SetActive(false);
      this.UpdateToggleButton();
      this.RefreshQuestList();
    }

    public static List<QuestParam> GetCollaboSkillQuests(UnitData unitData1, UnitData unitData2)
    {
      List<QuestParam> questParamList = new List<QuestParam>();
      GameManager instanceDirect = MonoSingleton<GameManager>.GetInstanceDirect();
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) instanceDirect, (UnityEngine.Object) null))
        return questParamList;
      QuestParam collaboSkillQuest = CollaboSkillQuestList.GetCollaboSkillQuest(unitData1, unitData2);
      if (collaboSkillQuest != null)
      {
        QuestParam[] quests = instanceDirect.Quests;
        for (int index = 0; index < quests.Length; ++index)
        {
          if (quests[index].ChapterID == collaboSkillQuest.ChapterID)
            questParamList.Add(quests[index]);
        }
      }
      return questParamList;
    }

    public static QuestParam GetCollaboSkillQuest(UnitData unitData1, UnitData unitData2)
    {
      GameManager instanceDirect = MonoSingleton<GameManager>.GetInstanceDirect();
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) instanceDirect, (UnityEngine.Object) null))
        return (QuestParam) null;
      return CollaboSkillQuestList.GetLearnSkillQuest(instanceDirect.MasterParam.GetCollaboSkillData(unitData1.UnitParam.iname), unitData2);
    }

    private static QuestParam GetLearnSkillQuest(CollaboSkillParam csp, UnitData partner)
    {
      GameManager instanceDirect = MonoSingleton<GameManager>.GetInstanceDirect();
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) instanceDirect, (UnityEngine.Object) null))
        return (QuestParam) null;
      if (csp == null || partner == null)
        return (QuestParam) null;
      CollaboSkillParam.LearnSkill learnSkill = csp.LearnSkillLists.Find((Predicate<CollaboSkillParam.LearnSkill>) (ls => ls.PartnerUnitIname == partner.UnitParam.iname));
      if (learnSkill != null)
        return instanceDirect.FindQuest(learnSkill.QuestIname);
      DebugUtility.LogError("learnSkill がnull");
      return (QuestParam) null;
    }

    private void CreateStoryList()
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) MonoSingleton<GameManager>.GetInstanceDirect(), (UnityEngine.Object) null))
        return;
      List<QuestParam> collaboSkillQuests = CollaboSkillQuestList.GetCollaboSkillQuests(this.CurrentUnit1, this.CurrentUnit2);
      if (collaboSkillQuests == null)
      {
        DebugUtility.LogError(string.Format("連携スキルクエストが見つかりません:{0} × {1}", (object) this.CurrentUnit1.UnitParam.iname, (object) this.CurrentUnit2.UnitParam.iname));
      }
      else
      {
        QuestParam[] availableQuests = MonoSingleton<GameManager>.Instance.Player.AvailableQuests;
        for (int index = 0; index < collaboSkillQuests.Count; ++index)
        {
          QuestParam questParam = collaboSkillQuests[index];
          bool flag1 = questParam.IsDateUnlock(-1L);
          bool flag2 = Array.Find<QuestParam>(availableQuests, (Predicate<QuestParam>) (p => p == questParam)) != null;
          bool flag3 = questParam.state == QuestStates.Cleared;
          bool flag4 = flag1 && flag2 && !flag3;
          GameObject gameObject;
          if (flag2 || flag3)
          {
            gameObject = (GameObject) UnityEngine.Object.Instantiate<GameObject>((M0) this.StoryQuestItemTemplate);
            ((Selectable) gameObject.GetComponent<Button>()).set_interactable(flag4);
          }
          else
            gameObject = (GameObject) UnityEngine.Object.Instantiate<GameObject>((M0) this.StoryQuestDisableItemTemplate);
          gameObject.SetActive(true);
          gameObject.get_transform().SetParent(this.QuestList, false);
          DataSource.Bind<QuestParam>(gameObject, questParam);
          CharacterQuestListItem component1 = (CharacterQuestListItem) gameObject.GetComponent<CharacterQuestListItem>();
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component1, (UnityEngine.Object) null))
            component1.SetUp(this.CurrentUnit1, this.CurrentUnit2, questParam);
          ListItemEvents component2 = (ListItemEvents) gameObject.GetComponent<ListItemEvents>();
          component2.OnSelect = new ListItemEvents.ListItemEvent(this.OnQuestSelect);
          component2.OnOpenDetail = new ListItemEvents.ListItemEvent(this.OnOpenItemDetail);
          component2.OnCloseDetail = new ListItemEvents.ListItemEvent(this.OnCloseItemDetail);
          this.mStoryQuestListItems.Add(gameObject);
        }
      }
    }

    private void RefreshQuestList()
    {
      if (this.mListRefreshing || UnityEngine.Object.op_Equality((UnityEngine.Object) this.StoryQuestItemTemplate, (UnityEngine.Object) null) || (UnityEngine.Object.op_Equality((UnityEngine.Object) this.StoryQuestDisableItemTemplate, (UnityEngine.Object) null) || UnityEngine.Object.op_Equality((UnityEngine.Object) this.QuestList, (UnityEngine.Object) null)))
        return;
      this.mListRefreshing = true;
      if (this.mStoryQuestListItems.Count <= 0)
        this.CreateStoryList();
      for (int index = 0; index < this.mStoryQuestListItems.Count; ++index)
        this.mStoryQuestListItems[index].SetActive(this.mIsStoryList);
      DataSource.Bind<UnitData>(this.CharacterImage1, this.CurrentUnit1);
      DataSource.Bind<UnitData>(this.CharacterImage2, this.CurrentUnit2);
      this.mListRefreshing = false;
    }

    private void OnQuestSelect(GameObject button)
    {
      List<GameObject> storyQuestListItems = this.mStoryQuestListItems;
      int index = storyQuestListItems.IndexOf(button.get_gameObject());
      QuestParam quest = DataSource.FindDataOfClass<QuestParam>(storyQuestListItems[index], (QuestParam) null);
      if (quest == null)
        return;
      if (!quest.IsDateUnlock(-1L))
        UIUtility.NegativeSystemMessage((string) null, LocalizedText.Get("sys.DISABLE_QUEST_DATE_UNLOCK"), (UIUtility.DialogResultEvent) null, (GameObject) null, false, -1);
      else if (Array.Find<QuestParam>(MonoSingleton<GameManager>.Instance.Player.AvailableQuests, (Predicate<QuestParam>) (p => p == quest)) == null)
      {
        UIUtility.NegativeSystemMessage((string) null, LocalizedText.Get("sys.DISABLE_QUEST_CHALLENGE"), (UIUtility.DialogResultEvent) null, (GameObject) null, false, -1);
      }
      else
      {
        GlobalVars.SelectedQuestID = quest.iname;
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 100);
      }
    }

    private void OnCloseItemDetail(GameObject go)
    {
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mQuestDetail, (UnityEngine.Object) null))
        return;
      UnityEngine.Object.DestroyImmediate((UnityEngine.Object) this.mQuestDetail.get_gameObject());
      this.mQuestDetail = (GameObject) null;
    }

    private void OnOpenItemDetail(GameObject go)
    {
      QuestParam dataOfClass = DataSource.FindDataOfClass<QuestParam>(go, (QuestParam) null);
      if (!UnityEngine.Object.op_Equality((UnityEngine.Object) this.mQuestDetail, (UnityEngine.Object) null) || dataOfClass == null)
        return;
      this.mQuestDetail = (GameObject) UnityEngine.Object.Instantiate<GameObject>((M0) this.QuestDetailTemplate);
      DataSource.Bind<QuestParam>(this.mQuestDetail, dataOfClass);
      DataSource.Bind<UnitData>(this.mQuestDetail, this.CurrentUnit1);
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
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ListToggleButton, (UnityEngine.Object) null))
        return;
      this.ListToggleButton.set_sprite(this.StoryListSprite);
    }

    public void Activated(int pinID)
    {
      if (pinID != 10)
        return;
      this.OnToggleButton();
    }
  }
}
