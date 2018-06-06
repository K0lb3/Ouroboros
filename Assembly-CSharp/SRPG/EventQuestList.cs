// Decompiled with JetBrains decompiler
// Type: SRPG.EventQuestList
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  [FlowNode.Pin(0, "再読み込み", FlowNode.PinTypes.Input, 40)]
  [FlowNode.Pin(1, "リスト要素を選択", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(2, "リスト階層を戻る", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(3, "通常チャプター切り替え", FlowNode.PinTypes.Input, 2)]
  [FlowNode.Pin(4, "鍵チャプター切り替え", FlowNode.PinTypes.Input, 3)]
  [FlowNode.Pin(50, "再読み込み完了", FlowNode.PinTypes.Output, 50)]
  [FlowNode.Pin(100, "クエストが選択された", FlowNode.PinTypes.Output, 100)]
  [FlowNode.Pin(101, "クエストのアンロック", FlowNode.PinTypes.Output, 101)]
  [FlowNode.Pin(200, "塔が選択された", FlowNode.PinTypes.Output, 200)]
  public class EventQuestList : MonoBehaviour, IFlowInterface
  {
    public GameObject ItemTemplate;
    public GameObject ItemContainer;
    public bool Descending;
    public bool RefreshOnStart;
    public RectTransform SwitchParent;
    public Button EventQuestButton;
    public Button KeyQuestButton;
    public GameObject KeyQuestOpenEffect;
    public UnityEngine.UI.Text QuestTypeText;
    public GameObject BackButton;
    public GameObject Caution;
    public Vector2 DefaultScrollPosition;
    private List<ListItemEvents> mItems;
    private EventQuestList.EventQuestTypes mEventType;

    public EventQuestList()
    {
      base.\u002Ector();
    }

    private void Start()
    {
      if (!string.IsNullOrEmpty((string) GlobalVars.SelectedChapter))
      {
        ChapterParam area = MonoSingleton<GameManager>.Instance.FindArea((string) GlobalVars.SelectedChapter);
        if (area != null)
          this.mEventType = !area.IsKeyQuest() ? EventQuestList.EventQuestTypes.Normal : EventQuestList.EventQuestTypes.Key;
      }
      else if (GlobalVars.ReqEventPageListType == GlobalVars.EventQuestListType.KeyQuest)
      {
        GlobalVars.ReqEventPageListType = GlobalVars.EventQuestListType.EventQuest;
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
      if (Object.op_Inequality((Object) this.ItemTemplate, (Object) null))
        this.ItemTemplate.SetActive(false);
      if (Object.op_Inequality((Object) this.Caution, (Object) null))
        this.Caution.SetActive(false);
      if (this.RefreshOnStart)
        this.Refresh(this.mEventType);
      this.RefreshQuestTypeText(this.mEventType);
    }

    private void ResetScroll()
    {
      if (!Object.op_Inequality((Object) this.ItemContainer, (Object) null))
        return;
      ScrollRect[] componentsInParent = (ScrollRect[]) this.ItemContainer.GetComponentsInParent<ScrollRect>(true);
      if (componentsInParent.Length <= 0)
        return;
      componentsInParent[0].set_normalizedPosition(this.DefaultScrollPosition);
    }

    public void Activated(int pinID)
    {
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
          this.Refresh(this.mEventType);
          break;
        case 4:
          this.RestoreHierarchyRoot();
          this.mEventType = EventQuestList.EventQuestTypes.Key;
          this.Refresh(this.mEventType);
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
        this.mEventType = !area.IsKeyQuest() ? EventQuestList.EventQuestTypes.Normal : EventQuestList.EventQuestTypes.Key;
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

    private void Refresh(EventQuestList.EventQuestTypes type)
    {
      GameUtility.DestroyGameObjects<ListItemEvents>(this.mItems);
      GameManager instance = MonoSingleton<GameManager>.Instance;
      ChapterParam[] chapters = instance.Chapters;
      List<ChapterParam> chapterParamList = new List<ChapterParam>((IEnumerable<ChapterParam>) chapters);
      QuestParam[] availableQuests = instance.Player.AvailableQuests;
      long serverTime = Network.GetServerTime();
      ChapterParam chapterParam = (ChapterParam) null;
      for (int index = chapterParamList.Count - 1; index >= 0; --index)
      {
        if ((string) GlobalVars.SelectedSection != chapterParamList[index].section)
          chapterParamList.RemoveAt(index);
      }
      if (!string.IsNullOrEmpty((string) GlobalVars.SelectedChapter))
      {
        chapterParam = instance.FindArea((string) GlobalVars.SelectedChapter);
        for (int index = chapterParamList.Count - 1; index >= 0; --index)
        {
          if (chapterParamList[index].parent == null || chapterParamList[index].parent.iname != (string) GlobalVars.SelectedChapter)
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
            if (chapterParamList[index].IsKeyQuest())
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
      }
      for (int index = chapterParamList.Count - 1; index >= 0; --index)
      {
        if (!this.ChapterContainsPlayableQuest(chapterParamList[index], chapters, availableQuests, serverTime))
          chapterParamList.RemoveAt(index);
      }
      if (this.Descending)
        chapterParamList.Reverse();
      if (type == EventQuestList.EventQuestTypes.Normal && string.IsNullOrEmpty(GlobalVars.SelectedChapter.Get()))
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
          ListItemEvents listItemEvents1 = !Object.op_Inequality((Object) this.ItemTemplate, (Object) null) ? (ListItemEvents) null : (ListItemEvents) this.ItemTemplate.GetComponent<ListItemEvents>();
          if (!string.IsNullOrEmpty(data.prefabPath))
          {
            StringBuilder stringBuilder = GameUtility.GetStringBuilder();
            stringBuilder.Append("QuestChapters/");
            stringBuilder.Append(data.prefabPath);
            listItemEvents1 = AssetManager.Load<ListItemEvents>(stringBuilder.ToString());
          }
          if (!Object.op_Equality((Object) listItemEvents1, (Object) null))
          {
            QuestParam quest = MonoSingleton<GameManager>.Instance.FindQuest(data.iname);
            ListItemEvents listItemEvents2 = (ListItemEvents) Object.Instantiate<ListItemEvents>((M0) listItemEvents1);
            DataSource.Bind<TowerParam>(((Component) listItemEvents2).get_gameObject(), data);
            DataSource.Bind<QuestParam>(((Component) listItemEvents2).get_gameObject(), quest);
            ((Component) listItemEvents2).get_transform().SetParent(this.ItemContainer.get_transform(), false);
            ((Component) listItemEvents2).get_gameObject().SetActive(true);
            listItemEvents2.OnSelect = new ListItemEvents.ListItemEvent(this.OnTowerSelect);
            this.mItems.Add(listItemEvents2);
          }
        }
      }
      for (int index = 0; index < chapterParamList.Count; ++index)
      {
        ChapterParam data = chapterParamList[index];
        ListItemEvents listItemEvents1 = !Object.op_Inequality((Object) this.ItemTemplate, (Object) null) ? (ListItemEvents) null : (ListItemEvents) this.ItemTemplate.GetComponent<ListItemEvents>();
        if (!string.IsNullOrEmpty(data.prefabPath))
        {
          StringBuilder stringBuilder = GameUtility.GetStringBuilder();
          stringBuilder.Append("QuestChapters/");
          stringBuilder.Append(data.prefabPath);
          listItemEvents1 = AssetManager.Load<ListItemEvents>(stringBuilder.ToString());
        }
        if (!Object.op_Equality((Object) listItemEvents1, (Object) null))
        {
          ListItemEvents listItemEvents2 = (ListItemEvents) Object.Instantiate<ListItemEvents>((M0) listItemEvents1);
          DataSource.Bind<ChapterParam>(((Component) listItemEvents2).get_gameObject(), data);
          DataSource.Bind<KeyItem>(((Component) listItemEvents2).get_gameObject(), data == null || data.keys.Count <= 0 ? (KeyItem) null : data.keys[0]);
          KeyQuestBanner component = (KeyQuestBanner) ((Component) listItemEvents2).get_gameObject().GetComponent<KeyQuestBanner>();
          if (Object.op_Inequality((Object) component, (Object) null))
            component.UpdateValue();
          ((Component) listItemEvents2).get_transform().SetParent(this.ItemContainer.get_transform(), false);
          ((Component) listItemEvents2).get_gameObject().SetActive(true);
          listItemEvents2.OnSelect = new ListItemEvents.ListItemEvent(this.OnNodeSelect);
          this.mItems.Add(listItemEvents2);
        }
      }
      if (Object.op_Inequality((Object) this.BackButton, (Object) null))
      {
        if (chapterParam != null)
          this.BackButton.SetActive(true);
        else if (!string.IsNullOrEmpty((string) GlobalVars.SelectedSection))
          this.BackButton.SetActive(!this.IsSectionHidden((string) GlobalVars.SelectedSection));
      }
      if (Object.op_Inequality((Object) this.Caution, (Object) null))
        this.Caution.SetActive(chapterParamList.Count == 0);
      this.RefreshSwitchButton(type);
      this.RefreshQuestTypeText(type);
      this.ResetScroll();
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 50);
    }

    private void RefreshSwitchButton(EventQuestList.EventQuestTypes type)
    {
      ChapterParam[] chapters = MonoSingleton<GameManager>.Instance.Chapters;
      bool flag1 = false;
      bool flag2 = false;
      bool flag3 = false;
      if (chapters != null)
      {
        long serverTime = Network.GetServerTime();
        for (int index = 0; index < chapters.Length; ++index)
        {
          if (chapters[index].IsKeyQuest())
          {
            flag1 = true;
            if (chapters[index].IsDateUnlock(serverTime))
              flag3 = true;
            if (chapters[index].IsKeyUnlock(serverTime))
              flag2 = true;
          }
        }
      }
      switch (type)
      {
        case EventQuestList.EventQuestTypes.Normal:
          if (Object.op_Inequality((Object) this.EventQuestButton, (Object) null))
            ((Component) this.EventQuestButton).get_gameObject().SetActive(false);
          if (Object.op_Inequality((Object) this.KeyQuestButton, (Object) null))
          {
            ((Component) this.KeyQuestButton).get_gameObject().SetActive(true);
            ((Selectable) this.KeyQuestButton).set_interactable(flag3);
            break;
          }
          break;
        case EventQuestList.EventQuestTypes.Key:
          if (Object.op_Inequality((Object) this.EventQuestButton, (Object) null))
            ((Component) this.EventQuestButton).get_gameObject().SetActive(true);
          if (Object.op_Inequality((Object) this.KeyQuestButton, (Object) null))
          {
            ((Component) this.KeyQuestButton).get_gameObject().SetActive(false);
            ((Selectable) this.KeyQuestButton).set_interactable(flag1);
            break;
          }
          break;
      }
      if (!Object.op_Inequality((Object) this.KeyQuestOpenEffect, (Object) null))
        return;
      this.KeyQuestOpenEffect.SetActive(flag2);
    }

    private void RefreshQuestTypeText(EventQuestList.EventQuestTypes type)
    {
      if (!Object.op_Inequality((Object) this.QuestTypeText, (Object) null))
        return;
      switch (type)
      {
        case EventQuestList.EventQuestTypes.Normal:
          this.QuestTypeText.set_text(LocalizedText.Get("sys.QUESTTYPE_EVENT"));
          break;
        case EventQuestList.EventQuestTypes.Key:
          this.QuestTypeText.set_text(LocalizedText.Get("sys.QUESTTYPE_KEY"));
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

    public enum EventQuestTypes
    {
      Normal,
      Key,
    }
  }
}
