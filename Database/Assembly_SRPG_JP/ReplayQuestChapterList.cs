// Decompiled with JetBrains decompiler
// Type: SRPG.ReplayQuestChapterList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  [FlowNode.Pin(12, "アイテムが選択された", FlowNode.PinTypes.Output, 102)]
  [FlowNode.Pin(11, "閲覧可能なストーリーがない", FlowNode.PinTypes.Output, 101)]
  [FlowNode.Pin(10, "閲覧可能なストーリーがある", FlowNode.PinTypes.Output, 100)]
  [FlowNode.Pin(0, "再読み込み", FlowNode.PinTypes.Input, 0)]
  public class ReplayQuestChapterList : MonoBehaviour, IFlowInterface
  {
    private const int PIN_ID_REFRESH = 0;
    private const int PIN_ID_EXIST = 10;
    private const int PIN_ID_NOT_EXIST = 11;
    private const int PIN_ID_SELECT = 12;
    public ListItemEvents ItemTemplate;
    public GameObject ItemContainer;
    public bool Descending;
    public GameObject BackToSection;
    public GameObject BackToCategories;
    public ScrollRect ScrollRect;
    private QuestParam[] questParams;
    private string sectionName;
    private List<ListItemEvents> mItems;

    public ReplayQuestChapterList()
    {
      base.\u002Ector();
    }

    private void Start()
    {
      if (Object.op_Inequality((Object) this.ItemTemplate, (Object) null))
        ((Component) this.ItemTemplate).get_gameObject().SetActive(false);
      this.Refresh();
    }

    public void Activated(int pinID)
    {
      if (pinID != 0)
        return;
      this.Refresh();
    }

    private void Refresh()
    {
      if (this.sectionName == null)
        this.sectionName = (string) GlobalVars.ReplaySelectedSection;
      else if (this.sectionName.Equals((string) GlobalVars.ReplaySelectedSection))
        return;
      this.sectionName = (string) GlobalVars.ReplaySelectedSection;
      GameUtility.DestroyGameObjects<ListItemEvents>(this.mItems);
      this.mItems.Clear();
      if (Object.op_Equality((Object) this.ItemTemplate, (Object) null) || Object.op_Equality((Object) this.ItemContainer, (Object) null))
        return;
      GameManager instance = MonoSingleton<GameManager>.Instance;
      List<ReplayQuestChapterList.ReplayChapterParam> replayChapterParamList = new List<ReplayQuestChapterList.ReplayChapterParam>();
      QuestParam[] availableQuests = instance.Player.AvailableQuests;
      long serverTime = Network.GetServerTime();
      foreach (ChapterParam chapter in instance.Chapters)
      {
        bool flag1 = false;
        bool flag2 = false;
        foreach (QuestParam questParam in availableQuests)
        {
          if (!(questParam.ChapterID != chapter.iname) && !questParam.IsMulti && (questParam.type != QuestTypes.Beginner && questParam.IsReplayDateUnlock(serverTime)) && ((!string.IsNullOrEmpty(questParam.event_start) || !string.IsNullOrEmpty(questParam.event_clear)) && (questParam.state == QuestStates.Challenged || questParam.state == QuestStates.Cleared)))
          {
            if (questParam.replayLimit && questParam.end > 0L)
              flag2 = true;
            flag1 = true;
            break;
          }
        }
        if (flag1 && (string.IsNullOrEmpty((string) GlobalVars.ReplaySelectedSection) || chapter.section == (string) GlobalVars.ReplaySelectedSection))
          replayChapterParamList.Add(new ReplayQuestChapterList.ReplayChapterParam()
          {
            chapterParam = chapter,
            replayLimit = flag2
          });
      }
      if (this.Descending)
        replayChapterParamList.Reverse();
      using (List<ReplayQuestChapterList.ReplayChapterParam>.Enumerator enumerator = replayChapterParamList.GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          ReplayQuestChapterList.ReplayChapterParam current = enumerator.Current;
          ChapterParam chapterParam = current.chapterParam;
          ListItemEvents listItemEvents1 = (ListItemEvents) null;
          if (!string.IsNullOrEmpty(chapterParam.prefabPath))
          {
            StringBuilder stringBuilder = GameUtility.GetStringBuilder();
            stringBuilder.Append("QuestChapters/");
            stringBuilder.Append(chapterParam.prefabPath);
            listItemEvents1 = AssetManager.Load<ListItemEvents>(stringBuilder.ToString());
          }
          if (Object.op_Equality((Object) listItemEvents1, (Object) null))
            listItemEvents1 = this.ItemTemplate;
          ListItemEvents listItemEvents2 = (ListItemEvents) Object.Instantiate<ListItemEvents>((M0) listItemEvents1);
          DataSource.Bind<ChapterParam>(((Component) listItemEvents2).get_gameObject(), chapterParam);
          ((Component) listItemEvents2).get_transform().SetParent(this.ItemContainer.get_transform(), false);
          ((Component) listItemEvents2).get_gameObject().SetActive(true);
          listItemEvents2.OnSelect = new ListItemEvents.ListItemEvent(this.OnItemSelect);
          if (chapterParam.end > 0L)
            this.SetTimerActive(((Component) listItemEvents2).get_transform(), current.replayLimit);
          this.mItems.Add(listItemEvents2);
        }
      }
      if (Object.op_Inequality((Object) this.BackToCategories, (Object) null) && Object.op_Inequality((Object) this.BackToSection, (Object) null))
      {
        bool flag = false;
        foreach (SectionParam section in instance.Sections)
        {
          if (section.iname == (string) GlobalVars.ReplaySelectedSection)
          {
            flag = section.hidden;
            break;
          }
        }
        this.BackToCategories.SetActive(flag);
        this.BackToSection.SetActive(!flag);
      }
      if (Object.op_Inequality((Object) this.ScrollRect, (Object) null))
        this.ScrollRect.set_normalizedPosition(Vector2.get_one());
      if (this.mItems.Count > 0)
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 10);
      else
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 11);
    }

    private void SetTimerActive(Transform tr, bool value)
    {
      if (Object.op_Equality((Object) tr, (Object) null))
        return;
      QuestTimeLimit component = (QuestTimeLimit) ((Component) tr).GetComponent<QuestTimeLimit>();
      if (!Object.op_Inequality((Object) component, (Object) null))
        return;
      if (Object.op_Inequality((Object) component.Body, (Object) null))
      {
        component.Body.SetActive(value);
      }
      else
      {
        Transform child1 = tr.FindChild("bg");
        if (Object.op_Inequality((Object) child1, (Object) null))
        {
          Transform child2 = child1.FindChild("timer_base");
          if (Object.op_Inequality((Object) child2, (Object) null))
            ((Component) child2).get_gameObject().SetActive(value);
        }
      }
      ((Behaviour) component).set_enabled(value);
    }

    private void OnItemSelect(GameObject go)
    {
      ChapterParam dataOfClass = DataSource.FindDataOfClass<ChapterParam>(go, (ChapterParam) null);
      if (dataOfClass == null)
        return;
      GlobalVars.ReplaySelectedChapter.Set(dataOfClass.iname);
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 12);
    }

    private class ReplayChapterParam
    {
      internal ChapterParam chapterParam;
      internal bool replayLimit;
    }
  }
}
