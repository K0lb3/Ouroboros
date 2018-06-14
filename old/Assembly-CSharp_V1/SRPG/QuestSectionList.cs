// Decompiled with JetBrains decompiler
// Type: SRPG.QuestSectionList
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Events;

namespace SRPG
{
  [FlowNode.Pin(1, "On right tap", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(100, "アイテムが選択された", FlowNode.PinTypes.Output, 100)]
  [FlowNode.Pin(3, "Disable drag", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(2, "Enable drag", FlowNode.PinTypes.Input, 40)]
  [FlowNode.Pin(0, "On left tap", FlowNode.PinTypes.Input, 40)]
  public class QuestSectionList : MonoBehaviour, IFlowInterface
  {
    public ListItemEvents ItemTemplate;
    public GameObject ItemContainer;
    public string WorldMapControllerID;
    public bool RefreshOnStart;
    private List<ListItemEvents> mItems;
    private int currentSectionID;
    [SerializeField]
    private GameObject leftButton;
    [SerializeField]
    private GameObject rightButton;
    [SerializeField]
    private ScrollAutoFit scrollRect;

    public QuestSectionList()
    {
      base.\u002Ector();
    }

    public void Activated(int pinID)
    {
      switch (pinID)
      {
        case 0:
          if (this.currentSectionID <= 0)
            break;
          --this.currentSectionID;
          this.OnBannerSelect(((Component) this.mItems[this.currentSectionID]).get_gameObject());
          break;
        case 1:
          if (this.currentSectionID >= this.mItems.Count - 1)
            break;
          ++this.currentSectionID;
          this.OnBannerSelect(((Component) this.mItems[this.currentSectionID]).get_gameObject());
          break;
        case 2:
          this.EnterChapter(true);
          break;
        case 3:
          this.EnterChapter(false);
          break;
      }
    }

    private void Start()
    {
      if (Object.op_Inequality((Object) this.ItemTemplate, (Object) null))
        ((Component) this.ItemTemplate).get_gameObject().SetActive(false);
      if (this.RefreshOnStart)
        this.Refresh();
      if (this.mItems.Count <= 1)
        return;
      this.scrollRect.set_horizontalNormalizedPosition((float) this.currentSectionID / (float) (this.mItems.Count - 1));
      this.EnterChapter(false);
      // ISSUE: method pointer
      this.scrollRect.OnScrollStop.AddListener(new UnityAction((object) this, __methodptr(OnScrollStop)));
    }

    private void Refresh()
    {
      GameUtility.DestroyGameObjects<ListItemEvents>(this.mItems);
      if (Object.op_Equality((Object) this.ItemTemplate, (Object) null) || Object.op_Equality((Object) this.ItemContainer, (Object) null))
        return;
      GameManager instance = MonoSingleton<GameManager>.Instance;
      PlayerData player = instance.Player;
      SectionParam[] sections = instance.Sections;
      List<string> stringList1 = new List<string>();
      List<string> stringList2 = new List<string>();
      long serverTime = Network.GetServerTime();
      QuestParam[] availableQuests = player.AvailableQuests;
      for (int index = 0; index < availableQuests.Length; ++index)
      {
        if (!string.IsNullOrEmpty(availableQuests[index].ChapterID) && !stringList1.Contains(availableQuests[index].ChapterID) && (!availableQuests[index].IsMulti && availableQuests[index].IsDateUnlock(serverTime)))
          stringList1.Add(availableQuests[index].ChapterID);
      }
      for (int index = 0; index < stringList1.Count; ++index)
      {
        ChapterParam area = instance.FindArea(stringList1[index]);
        if (area != null && !stringList2.Contains(area.section))
          stringList2.Add(area.section);
      }
      for (int index = 0; index < sections.Length; ++index)
      {
        SectionParam sectionParam = sections[index];
        if (sections[index].IsDateUnlock() && stringList2.Contains(sections[index].iname))
        {
          ListItemEvents listItemEvents1 = (ListItemEvents) null;
          if (!string.IsNullOrEmpty(sectionParam.prefabPath))
          {
            StringBuilder stringBuilder = GameUtility.GetStringBuilder();
            stringBuilder.Append("QuestSections/");
            stringBuilder.Append(sectionParam.prefabPath);
            stringBuilder.Append("_sg");
            listItemEvents1 = AssetManager.Load<ListItemEvents>(stringBuilder.ToString());
          }
          if (Object.op_Equality((Object) listItemEvents1, (Object) null))
            listItemEvents1 = this.ItemTemplate;
          ListItemEvents listItemEvents2 = (ListItemEvents) Object.Instantiate<ListItemEvents>((M0) listItemEvents1);
          DataSource.Bind<UIQuestSectionData>(((Component) listItemEvents2).get_gameObject(), new UIQuestSectionData(sections[index]));
          ((Component) listItemEvents2).get_transform().SetParent(this.ItemContainer.get_transform(), false);
          ((Component) listItemEvents2).get_gameObject().SetActive(true);
          SGWorldBannerItem component = (SGWorldBannerItem) ((Component) listItemEvents2).GetComponent<SGWorldBannerItem>();
          if (Object.op_Inequality((Object) component, (Object) null))
          {
            ChapterParam area = MonoSingleton<GameManager>.Instance.FindArea((string) GlobalVars.SelectedChapter);
            if (area != null && area.section == sections[index].iname)
            {
              component.SetChapterText(area.name);
              this.currentSectionID = index;
            }
          }
          this.mItems.Add(listItemEvents2);
        }
      }
    }

    private void OnItemSelect(GameObject go)
    {
      UIQuestSectionData dataOfClass = DataSource.FindDataOfClass<UIQuestSectionData>(go, (UIQuestSectionData) null);
      GlobalVars.SelectedSection.Set(dataOfClass.SectionID);
      WorldMapController instance1 = WorldMapController.FindInstance(this.WorldMapControllerID);
      if (Object.op_Inequality((Object) instance1, (Object) null))
      {
        GameManager instance2 = MonoSingleton<GameManager>.Instance;
        for (int index = 0; index < instance2.Chapters.Length; ++index)
        {
          if (instance2.Chapters[index].section == dataOfClass.SectionID)
          {
            instance1.GotoArea(instance2.Chapters[index].world);
            break;
          }
        }
      }
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 100);
    }

    private void OnBannerSelect(GameObject go)
    {
      UIQuestSectionData dataOfClass = DataSource.FindDataOfClass<UIQuestSectionData>(go, (UIQuestSectionData) null);
      GlobalVars.SelectedSection.Set(dataOfClass.SectionID);
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 100);
    }

    private void EnterChapter(bool enter)
    {
      this.leftButton.SetActive(enter);
      this.rightButton.SetActive(enter);
      this.scrollRect.set_horizontal(enter);
      if (this.currentSectionID >= this.mItems.Count)
        return;
      SGWorldBannerItem component = (SGWorldBannerItem) ((Component) this.mItems[this.currentSectionID]).GetComponent<SGWorldBannerItem>();
      if (!Object.op_Inequality((Object) component, (Object) null))
        return;
      if (!enter)
      {
        ChapterParam area = MonoSingleton<GameManager>.Instance.FindArea((string) GlobalVars.SelectedChapter);
        if (area == null)
          return;
        component.SetChapterText(area.name);
      }
      else
        component.SetChapterText(string.Empty);
    }

    private void OnScrollStop()
    {
      int num = Mathf.Abs(Mathf.RoundToInt((float) this.scrollRect.get_content().get_anchoredPosition().x / this.scrollRect.ItemScale));
      if (num == this.currentSectionID)
        return;
      this.currentSectionID = num;
      this.OnBannerSelect(((Component) this.mItems[this.currentSectionID]).get_gameObject());
    }
  }
}
