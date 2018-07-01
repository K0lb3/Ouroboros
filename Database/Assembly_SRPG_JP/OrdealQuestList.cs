// Decompiled with JetBrains decompiler
// Type: SRPG.OrdealQuestList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SRPG
{
  [FlowNode.Pin(0, "チーム情報更新", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(1100, "チーム編成開始", FlowNode.PinTypes.Output, 1100)]
  [FlowNode.Pin(1, "クエスト開始要求", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(1000, "クエスト開始", FlowNode.PinTypes.Output, 1000)]
  public class OrdealQuestList : MonoBehaviour, IFlowInterface, IWebHelp
  {
    [SerializeField]
    private GameObject ItemContainer;
    [SerializeField]
    private UnityEngine.UI.Text QuestTypeText;
    [SerializeField]
    private GameObject ChapterScrollRect;
    [SerializeField]
    private GameObject DetailTemplate;
    [Space(10f)]
    [SerializeField]
    private GameObject TeamPanelContainer;
    [SerializeField]
    private OrdealTeamPanel TeamPanelTemplate;
    [SerializeField]
    private Button StartButton;
    [SerializeField]
    private ListItemEvents MissionButton;
    [SerializeField]
    private Image BossImage;
    [SerializeField]
    private UnityEngine.UI.Text BossText;
    private List<ListItemEvents> mItems;
    private GameObject mDetailInfo;
    private ChapterParam mCurrentChapter;
    private QuestParam mCurrentQuest;
    private List<GameObject> mTeamPanels;

    public OrdealQuestList()
    {
      base.\u002Ector();
    }

    private void Awake()
    {
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.TeamPanelTemplate, (UnityEngine.Object) null))
        ((Component) this.TeamPanelTemplate).get_gameObject().SetActive(false);
      GlobalVars.OrdealParties = new List<PartyEditData>();
      GlobalVars.OrdealSupports = new List<SupportData>();
      this.Refresh();
      this.RefreshQuestTypeText();
    }

    private void ResetScroll()
    {
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ItemContainer, (UnityEngine.Object) null))
        return;
      ScrollRect[] componentsInParent = (ScrollRect[]) this.ItemContainer.GetComponentsInParent<ScrollRect>(true);
      if (componentsInParent.Length <= 0)
        return;
      componentsInParent[0].set_verticalNormalizedPosition(1f);
    }

    public void Activated(int pinID)
    {
      switch (pinID)
      {
        case 0:
          this.LoadTeam();
          break;
        case 1:
          this.StartQuest();
          break;
      }
    }

    private bool ChapterContainsPlayableQuest(ChapterParam chapter, ChapterParam[] allChapters, QuestParam[] availableQuests, long currentTime)
    {
      bool flag = false;
      for (int index = 0; index < allChapters.Length; ++index)
      {
        if (allChapters[index].parent == chapter)
        {
          if (this.ChapterContainsPlayableQuest(allChapters[index], allChapters, availableQuests, currentTime))
            return true;
          flag = true;
        }
      }
      if (!flag)
      {
        for (int index = 0; index < availableQuests.Length; ++index)
        {
          if (availableQuests[index].ChapterID == chapter.iname && !availableQuests[index].IsMulti && availableQuests[index].IsDateUnlock(currentTime))
            return true;
        }
      }
      return false;
    }

    private List<ChapterParam> GetAvailableChapters(ChapterParam[] allChapters, QuestParam[] questsAvailable, long currentTime, out ChapterParam currentChapter)
    {
      List<ChapterParam> chapterParamList = new List<ChapterParam>();
      currentChapter = (ChapterParam) null;
      foreach (ChapterParam allChapter in allChapters)
      {
        if (allChapter.IsOrdealQuest())
        {
          chapterParamList.Add(allChapter);
          if (allChapter.quests[0].state != QuestStates.Cleared)
            currentChapter = allChapter;
        }
      }
      if (currentChapter == null && chapterParamList.Count > 0)
        currentChapter = chapterParamList[0];
      for (int index = chapterParamList.Count - 1; index >= 0; --index)
      {
        if (!this.ChapterContainsPlayableQuest(chapterParamList[index], allChapters, questsAvailable, currentTime))
          chapterParamList.RemoveAt(index);
      }
      return chapterParamList;
    }

    private void Refresh()
    {
      GameUtility.DestroyGameObjects<ListItemEvents>(this.mItems);
      this.mItems.Clear();
      GameManager instance = MonoSingleton<GameManager>.Instance;
      ChapterParam currentChapter;
      List<ChapterParam> availableChapters = this.GetAvailableChapters(instance.Chapters, instance.Player.AvailableQuests, Network.GetServerTime(), out currentChapter);
      this.mCurrentChapter = currentChapter;
      for (int index = 0; index < availableChapters.Count; ++index)
      {
        ChapterParam data = availableChapters[index];
        if (!string.IsNullOrEmpty(data.prefabPath))
        {
          StringBuilder stringBuilder = GameUtility.GetStringBuilder();
          stringBuilder.Append("QuestChapters/");
          stringBuilder.Append(data.prefabPath);
          ListItemEvents listItemEvents1 = AssetManager.Load<ListItemEvents>(stringBuilder.ToString());
          if (!UnityEngine.Object.op_Equality((UnityEngine.Object) listItemEvents1, (UnityEngine.Object) null))
          {
            ListItemEvents listItemEvents2 = (ListItemEvents) UnityEngine.Object.Instantiate<ListItemEvents>((M0) listItemEvents1);
            foreach (ButtonEvent componentsInChild in (ButtonEvent[]) ((Component) listItemEvents2).GetComponentsInChildren<ButtonEvent>(true))
              componentsInChild.syncEvent = this.ChapterScrollRect;
            DataSource.Bind<ChapterParam>(((Component) listItemEvents2).get_gameObject(), data);
            if (data.quests != null && data.quests.Count > 0)
              DataSource.Bind<QuestParam>(((Component) listItemEvents2).get_gameObject(), data.quests[0]);
            KeyQuestBanner component = (KeyQuestBanner) ((Component) listItemEvents2).get_gameObject().GetComponent<KeyQuestBanner>();
            if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
              component.UpdateValue();
            ((Component) listItemEvents2).get_transform().SetParent(this.ItemContainer.get_transform(), false);
            ((Component) listItemEvents2).get_gameObject().SetActive(true);
            listItemEvents2.OnSelect = new ListItemEvents.ListItemEvent(this.OnItemSelect);
            listItemEvents2.OnOpenDetail = new ListItemEvents.ListItemEvent(this.OnOpenItemDetail);
            listItemEvents2.OnCloseDetail = new ListItemEvents.ListItemEvent(this.OnCloseItemDetail);
            this.mItems.Add(listItemEvents2);
          }
        }
      }
      for (int index = 0; index < this.mItems.Count; ++index)
      {
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mItems[index], (UnityEngine.Object) null))
          ((Component) this.mItems[index]).get_gameObject().get_transform().SetSiblingIndex(index);
      }
      this.ResetScroll();
    }

    private void RefreshQuestTypeText()
    {
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.QuestTypeText, (UnityEngine.Object) null))
        return;
      this.QuestTypeText.set_text(LocalizedText.Get("sys.QUESTTYPE_ORDEAL"));
    }

    private void StartQuest()
    {
      List<PartyEditData> ordealParties = GlobalVars.OrdealParties;
      List<SupportData> ordealSupports = GlobalVars.OrdealSupports;
      if (!PartyUtility.ValidateOrdealTeams(this.mCurrentQuest, ordealParties, ordealSupports, false) || PartyUtility.CheckWarningForOrdealTeams(ordealParties, (Action) (() => FlowNode_GameObject.ActivateOutputLinks((Component) this, 1000))))
        return;
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 1000);
    }

    private void ResetMissionButton()
    {
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.MissionButton, (UnityEngine.Object) null))
        return;
      this.MissionButton.OnOpenDetail = new ListItemEvents.ListItemEvent(this.OnOpenItemDetail);
      DataSource.Bind<QuestParam>(((Component) this.MissionButton).get_gameObject(), this.mCurrentQuest);
    }

    private void LoadBossData(QuestParam quest)
    {
      SpriteSheet spriteSheet = AssetManager.Load<SpriteSheet>("OrdealQuestList/OrdealQuestList_Images");
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) spriteSheet, (UnityEngine.Object) null))
        this.BossImage.set_sprite(spriteSheet.GetSprite(quest.iname));
      this.BossText.set_text(LocalizedText.Get("sys.ORDEAL_QUEST_BOSS_MESSAGE_" + quest.iname));
    }

    private void LoadTeam()
    {
      GameUtility.DestroyGameObjects(this.mTeamPanels);
      this.mTeamPanels.Clear();
      GlobalVars.OrdealParties = this.LoadTeamFromPlayerPrefs();
      List<PartyEditData> ordealParties = GlobalVars.OrdealParties;
      List<SupportData> ordealSupports = GlobalVars.OrdealSupports;
      for (int index = 0; index < ordealParties.Count; ++index)
      {
        // ISSUE: object of a compiler-generated type is created
        // ISSUE: variable of a compiler-generated type
        OrdealQuestList.\u003CLoadTeam\u003Ec__AnonStorey368 teamCAnonStorey368 = new OrdealQuestList.\u003CLoadTeam\u003Ec__AnonStorey368();
        // ISSUE: reference to a compiler-generated field
        teamCAnonStorey368.\u003C\u003Ef__this = this;
        OrdealTeamPanel component = (OrdealTeamPanel) ((GameObject) UnityEngine.Object.Instantiate<GameObject>((M0) ((Component) this.TeamPanelTemplate).get_gameObject())).GetComponent<OrdealTeamPanel>();
        ((Component) component).get_gameObject().SetActive(true);
        foreach (UnitData unit in ordealParties[index].Units)
        {
          if (unit != null)
            component.Add(unit);
        }
        // ISSUE: reference to a compiler-generated field
        teamCAnonStorey368.index = index;
        // ISSUE: method pointer
        ((UnityEvent) component.Button.get_onClick()).AddListener(new UnityAction((object) teamCAnonStorey368, __methodptr(\u003C\u003Em__37A)));
        component.TeamName.set_text(ordealParties[index].Name);
        SupportData supportData = (SupportData) null;
        if (ordealSupports != null && index < ordealSupports.Count)
        {
          supportData = ordealSupports[index];
          component.SetSupport(supportData);
        }
        int num = PartyUtility.CalcTotalAttack(ordealParties[index], MonoSingleton<GameManager>.Instance.Player.Units, supportData, (List<UnitData>) null);
        component.TotalAtack.set_text(num.ToString());
        this.mTeamPanels.Add(((Component) component).get_gameObject());
        ((Component) component).get_transform().SetParent(this.TeamPanelContainer.get_transform(), false);
      }
      this.CheckPlayableTeams(this.mCurrentQuest, ordealParties, ordealSupports);
    }

    private void OnClickTeamPanel(int index)
    {
      GlobalVars.SelectedTeamIndex = index;
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 1100);
    }

    private void CheckPlayableTeams(QuestParam quest, List<PartyEditData> teams, List<SupportData> supports = null)
    {
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.StartButton, (UnityEngine.Object) null))
        return;
      ((Selectable) this.StartButton).set_interactable(PartyUtility.ValidateOrdealTeams(quest, teams, supports, true));
    }

    private List<PartyEditData> LoadTeamFromPlayerPrefs()
    {
      int maxTeamCount = PartyWindow2.EditPartyTypes.Ordeal.GetMaxTeamCount();
      int lastSelectionIndex;
      List<PartyEditData> teams = PartyUtility.LoadTeamPresets(PartyWindow2.EditPartyTypes.Ordeal, out lastSelectionIndex, false) ?? new List<PartyEditData>();
      this.ValidateTeam(this.mCurrentQuest, teams, maxTeamCount);
      return teams;
    }

    private void ValidateTeam(QuestParam quest, List<PartyEditData> teams, int maxTeamCount)
    {
      bool flag = false;
      if (teams.Count > maxTeamCount)
      {
        teams = teams.Take<PartyEditData>(maxTeamCount).ToList<PartyEditData>();
        flag = true;
      }
      else if (teams.Count < maxTeamCount)
      {
        for (int count = teams.Count; count < maxTeamCount; ++count)
        {
          PartyData party = new PartyData(PlayerPartyTypes.Ordeal);
          teams.Add(new PartyEditData(PartyUtility.CreateOrdealPartyNameFromIndex(count), party));
        }
        flag = true;
      }
      if (!(flag | !PartyUtility.ResetToDefaultTeamIfNeededForOrdealQuest(quest, teams)))
        return;
      PartyUtility.SaveTeamPresets(PartyWindow2.EditPartyTypes.Ordeal, 0, teams, false);
    }

    private void OnItemSelect(GameObject go)
    {
      ChapterParam dataOfClass = DataSource.FindDataOfClass<ChapterParam>(go, (ChapterParam) null);
      if (dataOfClass == null)
        return;
      QuestParam[] availableQuests = MonoSingleton<GameManager>.Instance.Player.AvailableQuests;
      long serverTime = Network.GetServerTime();
      int num1 = 0;
      int num2 = 0;
      for (int index = 0; index < availableQuests.Length; ++index)
      {
        if (availableQuests[index].ChapterID == dataOfClass.iname && !availableQuests[index].IsMulti)
        {
          ++num1;
          if (availableQuests[index].IsJigen && !availableQuests[index].IsDateUnlock(serverTime))
            ++num2;
        }
      }
      if (num1 > 0 && num1 == num2)
      {
        UIUtility.NegativeSystemMessage((string) null, LocalizedText.Get("sys.QUEST_OUT_OF_DATE"), (UIUtility.DialogResultEvent) null, (GameObject) null, false, -1);
      }
      else
      {
        if (dataOfClass.quests == null || dataOfClass.quests.Count <= 0)
          return;
        this.mCurrentQuest = dataOfClass.quests[0];
        GlobalVars.SelectedQuestID = this.mCurrentQuest.iname;
        DataSource.Bind<QuestParam>(((Component) this).get_gameObject(), this.mCurrentQuest);
        this.ResetMissionButton();
        this.LoadBossData(this.mCurrentQuest);
        this.LoadTeam();
      }
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
