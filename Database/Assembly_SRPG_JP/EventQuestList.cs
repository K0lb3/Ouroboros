// Decompiled with JetBrains decompiler
// Type: SRPG.EventQuestList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SRPG
{
  [FlowNode.Pin(2, "リスト階層を戻る", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(1, "リスト要素を選択", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(0, "再読み込み", FlowNode.PinTypes.Input, 40)]
  [FlowNode.Pin(100, "クエストが選択された", FlowNode.PinTypes.Output, 100)]
  [FlowNode.Pin(50, "再読み込み完了", FlowNode.PinTypes.Output, 50)]
  [FlowNode.Pin(101, "クエストのアンロック", FlowNode.PinTypes.Output, 101)]
  [FlowNode.Pin(200, "塔が選択された", FlowNode.PinTypes.Output, 200)]
  [FlowNode.Pin(201, "マルチ塔が選択された", FlowNode.PinTypes.Output, 201)]
  [FlowNode.Pin(6, "塔チャプター切り替え", FlowNode.PinTypes.Input, 5)]
  [FlowNode.Pin(5, "GPSチャプター切り替え", FlowNode.PinTypes.Input, 4)]
  [FlowNode.Pin(4, "鍵チャプター切り替え", FlowNode.PinTypes.Input, 3)]
  [FlowNode.Pin(7, "更新", FlowNode.PinTypes.Input, 6)]
  [FlowNode.Pin(8, "ランキングクエストへチャプター切り替え", FlowNode.PinTypes.Input, 7)]
  [FlowNode.Pin(9, "UIの更新", FlowNode.PinTypes.Input, 8)]
  [FlowNode.Pin(3, "通常チャプター切り替え", FlowNode.PinTypes.Input, 2)]
  public class EventQuestList : MonoBehaviour, IFlowInterface
  {
    public GameObject ItemTemplate;
    public GameObject ItemContainer;
    public bool Descending;
    public bool RefreshOnStart;
    public RectTransform SwitchParent;
    public Button EventQuestButton;
    public Button KeyQuestButton;
    public Button TowerQuestButton;
    public GameObject KeyQuestOpenEffect;
    public GameObject QuestTypeTextFrame;
    public UnityEngine.UI.Text QuestTypeText;
    public GameObject TabTriple;
    public GameObject TabDouble;
    public Toggle[] TripleTabPages;
    public Toggle[] DoubleTabPages;
    public GameObject BackButton;
    public GameObject Caution;
    public UnityEngine.UI.Text CautionText;
    public Vector2 DefaultScrollPosition;
    public GameObject[] DisabledInBeginnerQuest;
    private List<ListItemEvents> mItems;
    private int mTabIndex;
    private GameObject mCurrentTab;
    private Toggle[] mCurrentTabPages;
    private EventQuestList.EventQuestTypes mEventType;
    private bool mForceChangeTab;

    public EventQuestList()
    {
      base.\u002Ector();
    }

    private void Awake()
    {
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ItemTemplate, (UnityEngine.Object) null))
        this.ItemTemplate.SetActive(false);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.Caution, (UnityEngine.Object) null))
        this.Caution.SetActive(false);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.QuestTypeTextFrame, (UnityEngine.Object) null))
        this.QuestTypeTextFrame.SetActive(false);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.TabTriple, (UnityEngine.Object) null))
        this.TabTriple.SetActive(false);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.TabDouble, (UnityEngine.Object) null))
        this.TabDouble.SetActive(false);
      if (!string.IsNullOrEmpty((string) GlobalVars.SelectedChapter))
      {
        this.mEventType = this.GetQuestTypeFromSelectedChapter((string) GlobalVars.SelectedChapter);
      }
      else
      {
        if (GlobalVars.ReqEventPageListType == GlobalVars.EventQuestListType.KeyQuest)
        {
          ChapterParam[] chapters = MonoSingleton<GameManager>.Instance.Chapters;
          if (chapters != null)
          {
            for (int index = 0; index < chapters.Length; ++index)
            {
              if (chapters[index].IsKeyQuest())
              {
                this.mEventType = EventQuestList.EventQuestTypes.Key;
                break;
              }
            }
          }
        }
        if (GlobalVars.ReqEventPageListType == GlobalVars.EventQuestListType.BeginnerQuest)
        {
          ChapterParam[] chapters = MonoSingleton<GameManager>.Instance.Chapters;
          if (chapters != null)
          {
            for (int index = 0; index < chapters.Length; ++index)
            {
              if (chapters[index].IsBeginnerQuest())
              {
                this.mEventType = EventQuestList.EventQuestTypes.Beginner;
                break;
              }
            }
          }
        }
        if (GlobalVars.ReqEventPageListType == GlobalVars.EventQuestListType.Tower && this.IsOpendTower())
          this.mEventType = EventQuestList.EventQuestTypes.Tower;
        if (GlobalVars.ReqEventPageListType == GlobalVars.EventQuestListType.RankingQuest && MonoSingleton<GameManager>.Instance.AvailableRankingQuesstParams.Count > 0)
        {
          GlobalVars.RankingQuestSelected = true;
          this.mEventType = EventQuestList.EventQuestTypes.Ranking;
          this.RefreshQuestTypeText(this.mEventType);
        }
      }
      SubQuestTypes highestPrioritySubType = this.GetHighestPrioritySubType();
      this.InitializeTab(highestPrioritySubType);
      this.mTabIndex = (int) highestPrioritySubType;
      if (this.RefreshOnStart)
        this.Refresh(this.mEventType);
      this.RefreshQuestTypeText(this.mEventType);
    }

    private EventQuestList.EventQuestTypes GetQuestTypeFromSelectedChapter(string chapterName)
    {
      ChapterParam area = MonoSingleton<GameManager>.Instance.FindArea(chapterName);
      if (area == null)
        return EventQuestList.EventQuestTypes.Normal;
      if (area.IsGpsQuest())
        return EventQuestList.EventQuestTypes.Gps;
      if (area.IsKeyQuest())
        return EventQuestList.EventQuestTypes.Key;
      return area.IsBeginnerQuest() ? EventQuestList.EventQuestTypes.Beginner : EventQuestList.EventQuestTypes.Normal;
    }

    private SubQuestTypes GetHighestPrioritySubType()
    {
      ChapterParam[] chapters = MonoSingleton<GameManager>.Instance.Chapters;
      QuestParam[] availableQuests = MonoSingleton<GameManager>.Instance.Player.AvailableQuests;
      long serverTime = Network.GetServerTime();
      ChapterParam currentChapter;
      using (List<ChapterParam>.Enumerator enumerator = this.GetAvailableChapters(EventQuestList.EventQuestTypes.NormalAndGps, chapters, availableQuests, (string) GlobalVars.SelectedSection, (string) null, serverTime, out currentChapter).GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          ChapterParam current = enumerator.Current;
          if (current.GetSubQuestType() == SubQuestTypes.Special && this.ChapterContainsPlayableQuest(current, chapters, availableQuests, serverTime))
            return SubQuestTypes.Special;
        }
      }
      return SubQuestTypes.Limited;
    }

    private void ResetScroll()
    {
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ItemContainer, (UnityEngine.Object) null))
        return;
      ScrollRect[] componentsInParent = (ScrollRect[]) this.ItemContainer.GetComponentsInParent<ScrollRect>(true);
      if (componentsInParent.Length <= 0)
        return;
      componentsInParent[0].set_normalizedPosition(this.DefaultScrollPosition);
    }

    public void Activated(int pinID)
    {
      GlobalVars.RankingQuestSelected = false;
      switch (pinID)
      {
        case 0:
          this.Refresh(this.mEventType);
          break;
        case 1:
          this.Refresh(this.mEventType);
          break;
        case 2:
          this.RestoreHierarchy();
          this.Refresh(this.mEventType);
          break;
        case 3:
          this.RestoreHierarchyRoot();
          this.mEventType = EventQuestList.EventQuestTypes.Normal;
          GlobalVars.ReqEventPageListType = GlobalVars.EventQuestListType.EventQuest;
          this.Refresh(this.mEventType);
          break;
        case 4:
          this.RestoreHierarchyRoot();
          this.mEventType = EventQuestList.EventQuestTypes.Key;
          GlobalVars.ReqEventPageListType = GlobalVars.EventQuestListType.KeyQuest;
          this.Refresh(this.mEventType);
          break;
        case 5:
          this.mEventType = EventQuestList.EventQuestTypes.Gps;
          this.mForceChangeTab = true;
          this.Refresh(this.mEventType);
          break;
        case 6:
          this.RestoreHierarchyRoot();
          this.mEventType = EventQuestList.EventQuestTypes.Tower;
          GlobalVars.ReqEventPageListType = GlobalVars.EventQuestListType.Tower;
          this.Refresh(this.mEventType);
          break;
        case 7:
          this.RestoreHierarchyRoot();
          switch (GlobalVars.ReqEventPageListType)
          {
            case GlobalVars.EventQuestListType.EventQuest:
              this.mEventType = EventQuestList.EventQuestTypes.Normal;
              break;
            case GlobalVars.EventQuestListType.KeyQuest:
              this.mEventType = EventQuestList.EventQuestTypes.Key;
              break;
            case GlobalVars.EventQuestListType.Tower:
              this.mEventType = EventQuestList.EventQuestTypes.Tower;
              break;
            case GlobalVars.EventQuestListType.RankingQuest:
              this.mEventType = EventQuestList.EventQuestTypes.Ranking;
              break;
          }
          this.Refresh(this.mEventType);
          break;
        case 8:
          GlobalVars.RankingQuestSelected = true;
          this.mEventType = EventQuestList.EventQuestTypes.Ranking;
          this.RefreshQuestTypeText(this.mEventType);
          FlowNode_GameObject.ActivateOutputLinks((Component) this, 100);
          break;
        case 9:
          if (string.IsNullOrEmpty((string) GlobalVars.SelectedChapter))
            break;
          EventQuestList.EventQuestTypes fromSelectedChapter = this.GetQuestTypeFromSelectedChapter((string) GlobalVars.SelectedChapter);
          this.RefreshSwitchButton(fromSelectedChapter);
          this.RefreshBeginnerObjects(fromSelectedChapter);
          break;
      }
    }

    private void RestoreHierarchyRoot()
    {
      GlobalVars.SelectedChapter.Set((string) null);
    }

    private void RestoreHierarchy()
    {
      ChapterParam area = MonoSingleton<GameManager>.Instance.FindArea((string) GlobalVars.SelectedChapter);
      if (area != null)
      {
        this.mTabIndex = (int) area.GetSubQuestType();
        if (area.IsGpsQuest())
        {
          if (area.parent != null)
          {
            if (area.parent.HasGpsQuest())
            {
              this.mEventType = EventQuestList.EventQuestTypes.Gps;
            }
            else
            {
              this.mEventType = EventQuestList.EventQuestTypes.Normal;
              GlobalVars.SelectedChapter.Set((string) null);
              return;
            }
          }
          else
            this.mEventType = !area.HasGpsQuest() ? EventQuestList.EventQuestTypes.Normal : EventQuestList.EventQuestTypes.Gps;
        }
        else
          this.mEventType = !area.IsKeyQuest() ? (!area.IsBeginnerQuest() ? EventQuestList.EventQuestTypes.Normal : EventQuestList.EventQuestTypes.Beginner) : EventQuestList.EventQuestTypes.Key;
        if (area.parent != null)
          GlobalVars.SelectedChapter.Set(area.parent.iname);
        else
          GlobalVars.SelectedChapter.Set((string) null);
      }
      else
      {
        if (string.IsNullOrEmpty((string) GlobalVars.SelectedSection) || !this.IsSectionHidden((string) GlobalVars.SelectedSection))
          return;
        GlobalVars.SelectedChapter.Set((string) null);
      }
    }

    private bool IsSectionHidden(string iname)
    {
      SectionParam[] sections = MonoSingleton<GameManager>.Instance.Sections;
      for (int index = 0; index < sections.Length; ++index)
      {
        if (sections[index].iname == (string) GlobalVars.SelectedSection)
          return sections[index].hidden;
      }
      return false;
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

    private bool IsChapterChildOf(ChapterParam child, ChapterParam parent)
    {
      for (; child != null; child = child.parent)
      {
        if (child == parent)
          return true;
      }
      return false;
    }

    private List<ChapterParam> GetAvailableChapters(EventQuestList.EventQuestTypes type, ChapterParam[] allChapters, QuestParam[] questsAvailable, string selectedSection, string selectedChapter, long currentTime, out ChapterParam currentChapter)
    {
      GameManager instance = MonoSingleton<GameManager>.Instance;
      List<ChapterParam> chapterParamList = new List<ChapterParam>((IEnumerable<ChapterParam>) allChapters);
      currentChapter = (ChapterParam) null;
      for (int index = chapterParamList.Count - 1; index >= 0; --index)
      {
        if (selectedSection != chapterParamList[index].section)
          chapterParamList.RemoveAt(index);
      }
      if (!string.IsNullOrEmpty(selectedChapter))
      {
        currentChapter = instance.FindArea(selectedChapter);
        for (int index = chapterParamList.Count - 1; index >= 0; --index)
        {
          if (chapterParamList[index].parent == null || chapterParamList[index].parent.iname != selectedChapter)
            chapterParamList.RemoveAt(index);
        }
      }
      else
      {
        for (int index = chapterParamList.Count - 1; index >= 0; --index)
        {
          if (chapterParamList[index].parent != null)
            chapterParamList.RemoveAt(index);
        }
      }
      switch (type)
      {
        case EventQuestList.EventQuestTypes.Normal:
          for (int index = 0; index < chapterParamList.Count; ++index)
          {
            if (chapterParamList[index].IsKeyQuest() || chapterParamList[index].IsTowerQuest() || (chapterParamList[index].IsBeginnerQuest() || chapterParamList[index].IsGpsQuest()))
              chapterParamList.RemoveAt(index--);
          }
          break;
        case EventQuestList.EventQuestTypes.Key:
          for (int index = 0; index < chapterParamList.Count; ++index)
          {
            if (!chapterParamList[index].IsKeyQuest())
              chapterParamList.RemoveAt(index--);
          }
          break;
        case EventQuestList.EventQuestTypes.Gps:
        case EventQuestList.EventQuestTypes.NormalAndGps:
          for (int index = 0; index < chapterParamList.Count; ++index)
          {
            if (chapterParamList[index].IsKeyQuest() || chapterParamList[index].IsTowerQuest() || chapterParamList[index].IsBeginnerQuest())
              chapterParamList.RemoveAt(index--);
            else if (chapterParamList[index].IsGpsQuest() && !chapterParamList[index].HasGpsQuest() && type == EventQuestList.EventQuestTypes.Gps)
              chapterParamList.RemoveAt(index--);
          }
          break;
        case EventQuestList.EventQuestTypes.Tower:
          chapterParamList.Clear();
          break;
        case EventQuestList.EventQuestTypes.Beginner:
          for (int index = 0; index < chapterParamList.Count; ++index)
          {
            if (!chapterParamList[index].IsBeginnerQuest())
              chapterParamList.RemoveAt(index--);
          }
          break;
      }
      for (int index = chapterParamList.Count - 1; index >= 0; --index)
      {
        if (!this.ChapterContainsPlayableQuest(chapterParamList[index], allChapters, questsAvailable, currentTime))
          chapterParamList.RemoveAt(index);
      }
      for (int index = 0; index < chapterParamList.Count; ++index)
      {
        ChapterParam chapterParam = chapterParamList[index];
        if (chapterParam == null || chapterParam.IsGpsQuest())
        {
          chapterParamList.RemoveAt(index);
          --index;
        }
      }
      return chapterParamList;
    }

    private void Refresh(EventQuestList.EventQuestTypes type)
    {
      GameUtility.DestroyGameObjects<ListItemEvents>(this.mItems);
      this.mItems.Clear();
      GameManager instance = MonoSingleton<GameManager>.Instance;
      ChapterParam[] chapters = instance.Chapters;
      QuestParam[] availableQuests = instance.Player.AvailableQuests;
      long serverTime = Network.GetServerTime();
      ChapterParam currentChapter = (ChapterParam) null;
      List<ChapterParam> availableChapters = this.GetAvailableChapters(type, chapters, availableQuests, (string) GlobalVars.SelectedSection, (string) GlobalVars.SelectedChapter, serverTime, out currentChapter);
      if (type == EventQuestList.EventQuestTypes.Gps && this.mForceChangeTab)
      {
        for (int index = 0; index < availableChapters.Count; ++index)
        {
          if (availableChapters[index].IsGpsQuest())
          {
            int subQuestType = (int) availableChapters[index].GetSubQuestType();
            if (subQuestType != this.mTabIndex)
            {
              this.RemoveTabs();
              this.OnToggleValueChanged(subQuestType);
              this.InitializeTab(this.GetHighestPrioritySubType());
              return;
            }
          }
        }
        this.mForceChangeTab = false;
      }
      if (type == EventQuestList.EventQuestTypes.Ranking)
        availableChapters.Clear();
      if (type == EventQuestList.EventQuestTypes.Normal || type == EventQuestList.EventQuestTypes.Gps)
      {
        SubQuestTypes mTabIndex = (SubQuestTypes) this.mTabIndex;
        for (int index = 0; index < availableChapters.Count; ++index)
        {
          if (availableChapters[index].GetSubQuestType() != mTabIndex)
            availableChapters.RemoveAt(index--);
        }
      }
      if (this.Descending)
        availableChapters.Reverse();
      if (type == EventQuestList.EventQuestTypes.Tower && string.IsNullOrEmpty(GlobalVars.SelectedChapter.Get()))
      {
        List<TowerParam> towerParamList = new List<TowerParam>();
        for (int index1 = 0; index1 < instance.Towers.Length; ++index1)
        {
          TowerParam tower = instance.Towers[index1];
          for (int index2 = 0; index2 < availableQuests.Length; ++index2)
          {
            if (availableQuests[index2].type == QuestTypes.Tower && !(availableQuests[index2].iname != tower.iname) && availableQuests[index2].IsDateUnlock(serverTime))
            {
              towerParamList.Add(tower);
              break;
            }
          }
        }
        for (int index = 0; index < towerParamList.Count; ++index)
        {
          TowerParam data = towerParamList[index];
          ListItemEvents listItemEvents1 = !UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ItemTemplate, (UnityEngine.Object) null) ? (ListItemEvents) null : (ListItemEvents) this.ItemTemplate.GetComponent<ListItemEvents>();
          if (!string.IsNullOrEmpty(data.prefabPath))
          {
            StringBuilder stringBuilder = GameUtility.GetStringBuilder();
            stringBuilder.Append("QuestChapters/");
            stringBuilder.Append(data.prefabPath);
            listItemEvents1 = AssetManager.Load<ListItemEvents>(stringBuilder.ToString());
          }
          if (!UnityEngine.Object.op_Equality((UnityEngine.Object) listItemEvents1, (UnityEngine.Object) null))
          {
            QuestParam quest = MonoSingleton<GameManager>.Instance.FindQuest(data.iname);
            ListItemEvents listItemEvents2 = (ListItemEvents) UnityEngine.Object.Instantiate<ListItemEvents>((M0) listItemEvents1);
            DataSource.Bind<TowerParam>(((Component) listItemEvents2).get_gameObject(), data);
            DataSource.Bind<QuestParam>(((Component) listItemEvents2).get_gameObject(), quest);
            ((Component) listItemEvents2).get_transform().SetParent(this.ItemContainer.get_transform(), false);
            ((Component) listItemEvents2).get_gameObject().SetActive(true);
            listItemEvents2.OnSelect = new ListItemEvents.ListItemEvent(this.OnTowerSelect);
            this.mItems.Add(listItemEvents2);
          }
        }
        for (int index = 0; index < availableQuests.Length; ++index)
        {
          if (availableQuests[index].IsMultiTower && availableQuests[index].IsDateUnlock(serverTime))
          {
            ListItemEvents listItemEvents1 = !UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ItemTemplate, (UnityEngine.Object) null) ? (ListItemEvents) null : (ListItemEvents) this.ItemTemplate.GetComponent<ListItemEvents>();
            ChapterParam chapter = availableQuests[index].Chapter;
            if (chapter != null)
            {
              if (!string.IsNullOrEmpty(chapter.prefabPath))
              {
                StringBuilder stringBuilder = GameUtility.GetStringBuilder();
                stringBuilder.Append("QuestChapters/");
                stringBuilder.Append(chapter.prefabPath);
                listItemEvents1 = AssetManager.Load<ListItemEvents>(stringBuilder.ToString());
              }
              if (!UnityEngine.Object.op_Equality((UnityEngine.Object) listItemEvents1, (UnityEngine.Object) null))
              {
                ListItemEvents listItemEvents2 = (ListItemEvents) UnityEngine.Object.Instantiate<ListItemEvents>((M0) listItemEvents1);
                DataSource.Bind<QuestParam>(((Component) listItemEvents2).get_gameObject(), availableQuests[index]);
                ((Component) listItemEvents2).get_transform().SetParent(this.ItemContainer.get_transform(), false);
                ((Component) listItemEvents2).get_gameObject().SetActive(true);
                listItemEvents2.OnSelect = new ListItemEvents.ListItemEvent(this.OnMultiTowerSelect);
                this.mItems.Add(listItemEvents2);
              }
            }
          }
        }
      }
      for (int index = 0; index < availableChapters.Count; ++index)
      {
        ChapterParam data = availableChapters[index];
        ListItemEvents listItemEvents1 = !UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ItemTemplate, (UnityEngine.Object) null) ? (ListItemEvents) null : (ListItemEvents) this.ItemTemplate.GetComponent<ListItemEvents>();
        if (!string.IsNullOrEmpty(data.prefabPath))
        {
          StringBuilder stringBuilder = GameUtility.GetStringBuilder();
          stringBuilder.Append("QuestChapters/");
          stringBuilder.Append(data.prefabPath);
          listItemEvents1 = AssetManager.Load<ListItemEvents>(stringBuilder.ToString());
        }
        if (!UnityEngine.Object.op_Equality((UnityEngine.Object) listItemEvents1, (UnityEngine.Object) null))
        {
          ListItemEvents listItemEvents2 = (ListItemEvents) UnityEngine.Object.Instantiate<ListItemEvents>((M0) listItemEvents1);
          DataSource.Bind<ChapterParam>(((Component) listItemEvents2).get_gameObject(), data);
          DataSource.Bind<KeyItem>(((Component) listItemEvents2).get_gameObject(), data == null || data.keys.Count <= 0 ? (KeyItem) null : data.keys[0]);
          KeyQuestBanner component = (KeyQuestBanner) ((Component) listItemEvents2).get_gameObject().GetComponent<KeyQuestBanner>();
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
            component.UpdateValue();
          ((Component) listItemEvents2).get_transform().SetParent(this.ItemContainer.get_transform(), false);
          ((Component) listItemEvents2).get_gameObject().SetActive(true);
          listItemEvents2.OnSelect = new ListItemEvents.ListItemEvent(this.OnNodeSelect);
          this.mItems.Add(listItemEvents2);
        }
      }
      this.StableSort<ListItemEvents>(this.mItems, (Comparison<ListItemEvents>) ((p1, p2) =>
      {
        ChapterParam chapterParam1 = !UnityEngine.Object.op_Inequality((UnityEngine.Object) p1, (UnityEngine.Object) null) ? (ChapterParam) null : p1.Chapter;
        ChapterParam chapterParam2 = !UnityEngine.Object.op_Inequality((UnityEngine.Object) p2, (UnityEngine.Object) null) ? (ChapterParam) null : p2.Chapter;
        return (chapterParam1 == null ? 1 : (!chapterParam1.IsGpsQuest() ? 1 : 0)).CompareTo(chapterParam2 == null ? 1 : (!chapterParam2.IsGpsQuest() ? 1 : 0));
      }));
      for (int index = 0; index < this.mItems.Count; ++index)
      {
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mItems[index], (UnityEngine.Object) null))
          ((Component) this.mItems[index]).get_gameObject().get_transform().SetSiblingIndex(index);
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.BackButton, (UnityEngine.Object) null))
      {
        if (currentChapter != null)
          this.BackButton.SetActive(true);
        else if (!string.IsNullOrEmpty((string) GlobalVars.SelectedSection))
          this.BackButton.SetActive(!this.IsSectionHidden((string) GlobalVars.SelectedSection));
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.Caution, (UnityEngine.Object) null))
      {
        this.Caution.SetActive(availableChapters.Count == 0 && type != EventQuestList.EventQuestTypes.Tower);
        if (type == EventQuestList.EventQuestTypes.Ranking)
          this.CautionText.set_text(LocalizedText.Get("sys.RANKING_QUEST_NO_CHALLENGEABLE"));
        else
          this.CautionText.set_text(LocalizedText.Get("sys.KEYQUEST_CAUTION_NOTQUEST"));
      }
      this.RefreshSwitchButton(type);
      this.RefreshBeginnerObjects(type);
      if (type == EventQuestList.EventQuestTypes.Normal || type == EventQuestList.EventQuestTypes.Gps)
      {
        this.EnableTab();
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.QuestTypeTextFrame, (UnityEngine.Object) null))
          this.QuestTypeTextFrame.SetActive(false);
        this.SetToggleIsOn();
      }
      else
      {
        this.DisableTab();
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.QuestTypeTextFrame, (UnityEngine.Object) null))
          this.QuestTypeTextFrame.SetActive(true);
        this.RefreshQuestTypeText(type);
      }
      this.ResetScroll();
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 50);
    }

    private void SetToggleIsOn()
    {
      for (int index = 0; index < this.mCurrentTabPages.Length; ++index)
      {
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mCurrentTabPages[index], (UnityEngine.Object) null))
          this.mCurrentTabPages[index].set_isOn(index == this.mTabIndex);
      }
    }

    private void EnableTab()
    {
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mCurrentTab, (UnityEngine.Object) null))
        return;
      this.mCurrentTab.SetActive(true);
    }

    private void DisableTab()
    {
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mCurrentTab, (UnityEngine.Object) null))
        return;
      this.mCurrentTab.SetActive(false);
    }

    private void InitializeTab(SubQuestTypes subtype)
    {
      if (subtype == SubQuestTypes.Special)
      {
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.TabTriple, (UnityEngine.Object) null))
          this.TabTriple.SetActive(true);
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.TabDouble, (UnityEngine.Object) null))
          this.TabDouble.SetActive(false);
        this.mCurrentTab = this.TabTriple;
        this.mCurrentTabPages = this.TripleTabPages;
        for (int index = 0; index < this.TripleTabPages.Length; ++index)
        {
          // ISSUE: object of a compiler-generated type is created
          // ISSUE: method pointer
          ((UnityEvent<bool>) this.TripleTabPages[index].onValueChanged).AddListener(new UnityAction<bool>((object) new EventQuestList.\u003CInitializeTab\u003Ec__AnonStorey330()
          {
            \u003C\u003Ef__this = this,
            index = index
          }, __methodptr(\u003C\u003Em__302)));
        }
      }
      else
      {
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.TabTriple, (UnityEngine.Object) null))
          this.TabTriple.SetActive(false);
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.TabDouble, (UnityEngine.Object) null))
          this.TabDouble.SetActive(true);
        this.mCurrentTab = this.TabDouble;
        this.mCurrentTabPages = this.DoubleTabPages;
        for (int index = 0; index < this.DoubleTabPages.Length; ++index)
        {
          // ISSUE: object of a compiler-generated type is created
          // ISSUE: method pointer
          ((UnityEvent<bool>) this.DoubleTabPages[index].onValueChanged).AddListener(new UnityAction<bool>((object) new EventQuestList.\u003CInitializeTab\u003Ec__AnonStorey331()
          {
            \u003C\u003Ef__this = this,
            index = index
          }, __methodptr(\u003C\u003Em__303)));
        }
      }
    }

    private void RemoveTabs()
    {
      if (this.TripleTabPages != null)
      {
        for (int index = 0; index < this.TripleTabPages.Length; ++index)
          ((UnityEventBase) this.TripleTabPages[index].onValueChanged).RemoveAllListeners();
      }
      if (this.DoubleTabPages == null)
        return;
      for (int index = 0; index < this.DoubleTabPages.Length; ++index)
        ((UnityEventBase) this.DoubleTabPages[index].onValueChanged).RemoveAllListeners();
    }

    private void OnToggleValueChanged(int index)
    {
      if (!Enum.IsDefined(typeof (SubQuestTypes), (object) (SubQuestTypes) index) || index == this.mTabIndex)
        return;
      this.mTabIndex = index;
      this.SetToggleIsOn();
      GlobalVars.SelectedChapter.Set((string) null);
      this.Refresh(this.mEventType);
    }

    private void RefreshSwitchButton(EventQuestList.EventQuestTypes type)
    {
      ChapterParam[] chapters = MonoSingleton<GameManager>.Instance.Chapters;
      bool _interactable1 = false;
      bool flag = false;
      bool _interactable2 = false;
      if (chapters != null)
      {
        long serverTime = Network.GetServerTime();
        for (int index = 0; index < chapters.Length; ++index)
        {
          if (chapters[index].IsKeyQuest())
          {
            _interactable1 = true;
            if (chapters[index].IsDateUnlock(serverTime))
              _interactable2 = true;
            if (chapters[index].IsKeyUnlock(serverTime))
              flag = true;
          }
        }
      }
      switch (type)
      {
        case EventQuestList.EventQuestTypes.Normal:
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.EventQuestButton, (UnityEngine.Object) null))
            this.RefreshQuestButtonState(this.EventQuestButton, false, ((Selectable) this.EventQuestButton).get_interactable());
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.KeyQuestButton, (UnityEngine.Object) null))
            this.RefreshQuestButtonState(this.KeyQuestButton, true, _interactable2);
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.TowerQuestButton, (UnityEngine.Object) null))
          {
            this.RefreshQuestButtonState(this.TowerQuestButton, true, this.IsOpendTower());
            break;
          }
          break;
        case EventQuestList.EventQuestTypes.Key:
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.EventQuestButton, (UnityEngine.Object) null))
            this.RefreshQuestButtonState(this.EventQuestButton, true, ((Selectable) this.EventQuestButton).get_interactable());
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.KeyQuestButton, (UnityEngine.Object) null))
            this.RefreshQuestButtonState(this.KeyQuestButton, false, _interactable1);
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.TowerQuestButton, (UnityEngine.Object) null))
          {
            this.RefreshQuestButtonState(this.TowerQuestButton, true, this.IsOpendTower());
            break;
          }
          break;
        case EventQuestList.EventQuestTypes.Tower:
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.EventQuestButton, (UnityEngine.Object) null))
            this.RefreshQuestButtonState(this.EventQuestButton, true, ((Selectable) this.EventQuestButton).get_interactable());
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.KeyQuestButton, (UnityEngine.Object) null))
            this.RefreshQuestButtonState(this.KeyQuestButton, true, _interactable2);
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.TowerQuestButton, (UnityEngine.Object) null))
          {
            this.RefreshQuestButtonState(this.TowerQuestButton, false, ((Selectable) this.TowerQuestButton).get_interactable());
            break;
          }
          break;
        case EventQuestList.EventQuestTypes.Ranking:
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.EventQuestButton, (UnityEngine.Object) null))
            this.RefreshQuestButtonState(this.EventQuestButton, true, ((Selectable) this.EventQuestButton).get_interactable());
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.KeyQuestButton, (UnityEngine.Object) null))
            this.RefreshQuestButtonState(this.KeyQuestButton, true, _interactable2);
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.TowerQuestButton, (UnityEngine.Object) null))
          {
            this.RefreshQuestButtonState(this.TowerQuestButton, false, ((Selectable) this.TowerQuestButton).get_interactable());
            break;
          }
          break;
      }
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.KeyQuestOpenEffect, (UnityEngine.Object) null))
        return;
      this.KeyQuestOpenEffect.SetActive(flag);
    }

    private void RefreshBeginnerObjects(EventQuestList.EventQuestTypes type)
    {
      if (type != EventQuestList.EventQuestTypes.Beginner || this.DisabledInBeginnerQuest == null || this.DisabledInBeginnerQuest.Length <= 0)
        return;
      foreach (GameObject gameObject in this.DisabledInBeginnerQuest)
        gameObject.SetActive(false);
    }

    private bool RefreshQuestButtonState(Button _btn, bool _active, bool _interactable)
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) _btn, (UnityEngine.Object) null))
        return false;
      ((Component) _btn).get_gameObject().SetActive(_active);
      LevelLock component = (LevelLock) ((Component) _btn).get_gameObject().GetComponent<LevelLock>();
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
      {
        component.UpdateLockState();
        if (((Selectable) _btn).get_interactable())
          ((Selectable) _btn).set_interactable(_interactable);
      }
      else
        ((Selectable) _btn).set_interactable(_interactable);
      return ((Selectable) _btn).get_interactable();
    }

    private bool IsOpenRankingQuest()
    {
      return false;
    }

    private void RefreshQuestTypeText(EventQuestList.EventQuestTypes type)
    {
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.QuestTypeText, (UnityEngine.Object) null))
        return;
      switch (type)
      {
        case EventQuestList.EventQuestTypes.Normal:
          this.QuestTypeText.set_text(LocalizedText.Get("sys.QUESTTYPE_EVENT"));
          break;
        case EventQuestList.EventQuestTypes.Key:
          this.QuestTypeText.set_text(LocalizedText.Get("sys.QUESTTYPE_KEY"));
          break;
        case EventQuestList.EventQuestTypes.Gps:
          this.QuestTypeText.set_text(LocalizedText.Get("sys.QUESTTYPE_AREA"));
          break;
        case EventQuestList.EventQuestTypes.Tower:
          this.QuestTypeText.set_text(LocalizedText.Get("sys.QUESTTYPE_TOWER"));
          break;
        case EventQuestList.EventQuestTypes.Ranking:
          this.QuestTypeText.set_text(LocalizedText.Get("sys.QUESTTYPE_RANKING"));
          break;
        case EventQuestList.EventQuestTypes.Beginner:
          this.QuestTypeText.set_text(LocalizedText.Get("sys.QUESTTYPE_BEGINNER"));
          break;
      }
    }

    private void OnNodeSelect(GameObject go)
    {
      ChapterParam dataOfClass = DataSource.FindDataOfClass<ChapterParam>(go, (ChapterParam) null);
      foreach (ChapterParam chapter in MonoSingleton<GameManager>.Instance.Chapters)
      {
        if (chapter.parent == dataOfClass)
        {
          if (dataOfClass.IsGpsQuest())
          {
            GlobalVars.SelectedChapter.Set(dataOfClass.iname);
            GlobalEvent.Invoke("GPS_MODE", (object) this);
            return;
          }
          GlobalVars.SelectedChapter.Set(dataOfClass.iname);
          this.Refresh(this.mEventType);
          return;
        }
      }
      this.OnItemSelect(go);
    }

    private void OnItemSelect(GameObject go)
    {
      ChapterParam dataOfClass = DataSource.FindDataOfClass<ChapterParam>(go, (ChapterParam) null);
      if (dataOfClass == null)
        return;
      if (dataOfClass.IsKeyQuest())
      {
        long serverTime = Network.GetServerTime();
        if (!dataOfClass.IsKeyUnlock(serverTime))
        {
          if (!dataOfClass.IsDateUnlock(serverTime))
          {
            UIUtility.NegativeSystemMessage((string) null, LocalizedText.Get("sys.QUEST_OUT_OF_DATE"), (UIUtility.DialogResultEvent) null, (GameObject) null, false, -1);
            return;
          }
          GlobalVars.SelectedChapter.Set(dataOfClass.iname);
          GlobalVars.KeyQuestTimeOver = false;
          FlowNode_GameObject.ActivateOutputLinks((Component) this, 101);
          return;
        }
      }
      QuestParam[] availableQuests = MonoSingleton<GameManager>.Instance.Player.AvailableQuests;
      long serverTime1 = Network.GetServerTime();
      int num1 = 0;
      int num2 = 0;
      for (int index = 0; index < availableQuests.Length; ++index)
      {
        if (availableQuests[index].ChapterID == dataOfClass.iname && !availableQuests[index].IsMulti)
        {
          ++num1;
          if (availableQuests[index].IsJigen && !availableQuests[index].IsDateUnlock(serverTime1))
            ++num2;
        }
      }
      if (num1 > 0 && num1 == num2)
      {
        UIUtility.NegativeSystemMessage((string) null, LocalizedText.Get("sys.QUEST_OUT_OF_DATE"), (UIUtility.DialogResultEvent) null, (GameObject) null, false, -1);
      }
      else
      {
        GlobalVars.SelectedChapter.Set(dataOfClass.iname);
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 100);
      }
    }

    private void OnTowerSelect(GameObject go)
    {
      TowerParam dataOfClass = DataSource.FindDataOfClass<TowerParam>(go, (TowerParam) null);
      if (dataOfClass == null)
        return;
      QuestParam[] availableQuests = MonoSingleton<GameManager>.Instance.Player.AvailableQuests;
      long serverTime = Network.GetServerTime();
      int num1 = 0;
      int num2 = 0;
      for (int index = 0; index < availableQuests.Length; ++index)
      {
        if (availableQuests[index].type == QuestTypes.Tower && !(availableQuests[index].ChapterID != dataOfClass.iname) && !availableQuests[index].IsMulti)
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
        GlobalVars.SelectedTowerID = dataOfClass.iname;
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 200);
      }
    }

    private void OnMultiTowerSelect(GameObject go)
    {
      QuestParam dataOfClass = DataSource.FindDataOfClass<QuestParam>(go, (QuestParam) null);
      if (dataOfClass == null)
        return;
      long serverTime = Network.GetServerTime();
      if (dataOfClass.IsJigen && !dataOfClass.IsDateUnlock(serverTime))
      {
        UIUtility.NegativeSystemMessage((string) null, LocalizedText.Get("sys.QUEST_OUT_OF_DATE"), (UIUtility.DialogResultEvent) null, (GameObject) null, false, -1);
      }
      else
      {
        GlobalVars.SelectedMultiTowerID = dataOfClass.iname;
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 201);
      }
    }

    public void StableSort<T>(List<T> list, Comparison<T> comparison)
    {
      List<KeyValuePair<int, T>> keyValuePairList = new List<KeyValuePair<int, T>>(list.Count);
      for (int key = 0; key < list.Count; ++key)
        keyValuePairList.Add(new KeyValuePair<int, T>(key, list[key]));
      keyValuePairList.Sort((Comparison<KeyValuePair<int, T>>) ((x, y) =>
      {
        int num = comparison(x.Value, y.Value);
        if (num == 0)
          num = x.Key.CompareTo(y.Key);
        return num;
      }));
      for (int index = 0; index < list.Count; ++index)
        list[index] = keyValuePairList[index].Value;
    }

    public bool IsOpendTower()
    {
      GameManager instance = MonoSingleton<GameManager>.Instance;
      QuestParam[] availableQuests = instance.Player.AvailableQuests;
      long serverTime = Network.GetServerTime();
      for (int index1 = 0; index1 < instance.Towers.Length; ++index1)
      {
        TowerParam tower = instance.Towers[index1];
        for (int index2 = 0; index2 < availableQuests.Length; ++index2)
        {
          if (availableQuests[index2].type == QuestTypes.Tower && !(availableQuests[index2].iname != tower.iname) && availableQuests[index2].IsDateUnlock(serverTime))
            return true;
        }
      }
      for (int index = 0; index < availableQuests.Length; ++index)
      {
        if (availableQuests[index].IsMultiTower && availableQuests[index].IsDateUnlock(serverTime))
          return true;
      }
      return false;
    }

    public enum EventQuestTypes
    {
      Normal,
      Key,
      Gps,
      Tower,
      Ranking,
      Beginner,
      NormalAndGps,
    }
  }
}
