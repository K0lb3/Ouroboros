// Decompiled with JetBrains decompiler
// Type: SRPG.QuestListV2_MultiPlayCategory
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  [FlowNode.Pin(101, "選択された", FlowNode.PinTypes.Output, 101)]
  [FlowNode.Pin(402, "Disable filter animation", FlowNode.PinTypes.Input, 402)]
  [FlowNode.Pin(401, "Enable filter animation", FlowNode.PinTypes.Input, 401)]
  [FlowNode.Pin(400, "SG Filter View Mode", FlowNode.PinTypes.Input, 400)]
  [FlowNode.Pin(1, "イベントクエストを表示", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(0, "通常クエストを表示", FlowNode.PinTypes.Input, 0)]
  [AddComponentMenu("Multi/クエストカテゴリー一覧")]
  public class QuestListV2_MultiPlayCategory : MonoBehaviour, IFlowInterface
  {
    [Description("リストアイテムとして使用するゲームオブジェクト")]
    public ListItemEvents ItemTemplate;
    [Description("詳細画面として使用するゲームオブジェクト")]
    public GameObject DetailTemplate;
    private GameObject mDetailInfo;
    public ScrollRect ScrollRect;
    private bool isQuestFilter;
    private bool isScaledDown;
    private int selectedBanner;
    [SerializeField]
    private Animator filterAnimator;

    public QuestListV2_MultiPlayCategory()
    {
      base.\u002Ector();
    }

    public void Activated(int pinID)
    {
      switch (pinID)
      {
        case 0:
          this.Refresh(false);
          break;
        case 1:
          this.Refresh(true);
          break;
        case 400:
          this.Refresh(false);
          this.ScaleDownBanners();
          break;
        case 401:
          ((Behaviour) this.filterAnimator).set_enabled(true);
          break;
        case 402:
          ((Behaviour) this.filterAnimator).set_enabled(false);
          break;
      }
    }

    private void Awake()
    {
      this.isQuestFilter = false;
      this.isScaledDown = false;
      GlobalVars.SelectedMultiPlayQuestIsEvent = false;
      if (Object.op_Inequality((Object) this.ItemTemplate, (Object) null) && ((Component) this.ItemTemplate).get_gameObject().get_activeInHierarchy())
        ((Component) this.ItemTemplate).get_gameObject().SetActive(false);
      if (!Object.op_Inequality((Object) this.DetailTemplate, (Object) null) || !this.DetailTemplate.get_activeInHierarchy())
        return;
      this.DetailTemplate.SetActive(false);
    }

    private void Start()
    {
    }

    public void Refresh(bool isEvent)
    {
      GlobalVars.SelectedMultiPlayQuestIsEvent = isEvent;
      this.RefreshItems();
      if (!Object.op_Inequality((Object) this.ScrollRect, (Object) null))
        return;
      ListExtras component = (ListExtras) ((Component) this.ScrollRect).GetComponent<ListExtras>();
      if (Object.op_Inequality((Object) component, (Object) null))
        component.SetScrollPos(1f);
      else
        this.ScrollRect.set_normalizedPosition(Vector2.get_one());
    }

    private void RefreshItems()
    {
      Transform transform = ((Component) this).get_transform();
      DateTime serverTime = TimeManager.ServerTime;
      for (int index = transform.get_childCount() - 1; index >= 0; --index)
      {
        Transform child = transform.GetChild(index);
        if (!Object.op_Equality((Object) child, (Object) null) && ((Component) child).get_gameObject().get_activeSelf())
          Object.DestroyImmediate((Object) ((Component) child).get_gameObject());
      }
      if (Object.op_Equality((Object) this.ItemTemplate, (Object) null))
        return;
      List<ChapterParam> chapterParamList = new List<ChapterParam>();
      for (int index = 0; index < MonoSingleton<GameManager>.Instance.Quests.Length; ++index)
      {
        // ISSUE: object of a compiler-generated type is created
        // ISSUE: variable of a compiler-generated type
        QuestListV2_MultiPlayCategory.\u003CRefreshItems\u003Ec__AnonStorey267 itemsCAnonStorey267 = new QuestListV2_MultiPlayCategory.\u003CRefreshItems\u003Ec__AnonStorey267();
        QuestParam quest = MonoSingleton<GameManager>.Instance.Quests[index];
        if (quest != null && quest.type == QuestTypes.Multi && (quest.IsMultiEvent == GlobalVars.SelectedMultiPlayQuestIsEvent && quest.IsMultiVersus == (GlobalVars.SelectedMultiPlayRoomType == JSON_MyPhotonRoomParam.EType.VERSUS)) && quest.IsDateUnlock(-1L))
        {
          // ISSUE: reference to a compiler-generated field
          itemsCAnonStorey267.area = MonoSingleton<GameManager>.Instance.FindArea(quest.ChapterID);
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated method
          if (itemsCAnonStorey267.area != null && itemsCAnonStorey267.area.IsAvailable(serverTime) && chapterParamList.Find(new Predicate<ChapterParam>(itemsCAnonStorey267.\u003C\u003Em__2CA)) == null)
          {
            // ISSUE: reference to a compiler-generated field
            chapterParamList.Add(itemsCAnonStorey267.area);
          }
        }
      }
      for (int index = 0; index < chapterParamList.Count; ++index)
      {
        ListItemEvents listItemEvents1 = (ListItemEvents) null;
        if (!string.IsNullOrEmpty(chapterParamList[index].prefabPath))
        {
          StringBuilder stringBuilder = GameUtility.GetStringBuilder();
          stringBuilder.Append("QuestChapters/");
          stringBuilder.Append(chapterParamList[index].prefabPath);
          listItemEvents1 = AssetManager.Load<ListItemEvents>(stringBuilder.ToString());
        }
        if (Object.op_Equality((Object) listItemEvents1, (Object) null))
          listItemEvents1 = this.ItemTemplate;
        ListItemEvents listItemEvents2 = (ListItemEvents) Object.Instantiate<ListItemEvents>((M0) listItemEvents1);
        DataSource.Bind<ChapterParam>(((Component) listItemEvents2).get_gameObject(), chapterParamList[index]);
        listItemEvents2.OnSelect = new ListItemEvents.ListItemEvent(this.OnSelectItem);
        listItemEvents2.OnOpenDetail = new ListItemEvents.ListItemEvent(this.OnOpenItemDetail);
        listItemEvents2.OnCloseDetail = new ListItemEvents.ListItemEvent(this.OnCloseItemDetail);
        Transform child1 = ((Component) listItemEvents2).get_transform().FindChild("bg");
        if (Object.op_Inequality((Object) child1, (Object) null))
        {
          Transform child2 = child1.FindChild("timer_base");
          if (Object.op_Inequality((Object) child2, (Object) null) && chapterParamList[index].end <= 0L)
            ((Component) child2).get_gameObject().SetActive(false);
        }
        ((Component) listItemEvents2).get_transform().SetParent(transform, false);
        ((Component) listItemEvents2).get_gameObject().SetActive(true);
      }
      if (!this.isQuestFilter)
        return;
      this.isScaledDown = false;
      this.ScaleDownBanners();
    }

    private void OnSelectItem(GameObject go)
    {
      ChapterParam dataOfClass = DataSource.FindDataOfClass<ChapterParam>(go, (ChapterParam) null);
      if (dataOfClass != null)
      {
        GlobalVars.SelectedMultiPlayArea = dataOfClass.iname;
        DebugUtility.Log("Select Play Area:" + GlobalVars.SelectedMultiPlayArea);
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 101);
      }
      if (!this.isQuestFilter)
        return;
      this.UpdateSelectedChildren();
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
    }

    private void ScaleDownBanners()
    {
      if (this.isScaledDown)
        return;
      this.isScaledDown = true;
      this.isQuestFilter = true;
      Transform transform = ((Component) this).get_transform();
      for (int index = transform.get_childCount() - 1; index >= 0; --index)
      {
        Transform child = transform.GetChild(index);
        if (!Object.op_Equality((Object) child, (Object) null))
        {
          LayoutElement component = (LayoutElement) ((Component) child).GetComponent<LayoutElement>();
          if (Object.op_Inequality((Object) component, (Object) null))
          {
            LayoutElement layoutElement1 = component;
            layoutElement1.set_minWidth(layoutElement1.get_minWidth() * 0.7f);
            LayoutElement layoutElement2 = component;
            layoutElement2.set_minHeight(layoutElement2.get_minHeight() * 0.7f);
          }
        }
      }
    }

    private void UpdateSelectedChildren()
    {
      if (GlobalVars.SelectedMultiPlayArea == null)
        return;
      Transform transform = ((Component) this).get_transform();
      for (int index = transform.get_childCount() - 1; index >= 0; --index)
      {
        Transform child = transform.GetChild(index);
        if (!Object.op_Equality((Object) child, (Object) null))
        {
          ChapterParam dataOfClass = DataSource.FindDataOfClass<ChapterParam>(((Component) child).get_gameObject(), (ChapterParam) null);
          if (dataOfClass != null)
          {
            if (dataOfClass.iname != GlobalVars.SelectedMultiPlayArea)
            {
              M0 componentInChildren = ((Component) child).GetComponentInChildren<Image>();
              ColorBlock colors = ((Selectable) ((Component) child).GetComponentInChildren<Button>()).get_colors();
              // ISSUE: explicit reference operation
              Color disabledColor = ((ColorBlock) @colors).get_disabledColor();
              ((Graphic) componentInChildren).set_color(disabledColor);
            }
            else
            {
              M0 componentInChildren = ((Component) child).GetComponentInChildren<Image>();
              ColorBlock colors = ((Selectable) ((Component) child).GetComponentInChildren<Button>()).get_colors();
              // ISSUE: explicit reference operation
              Color normalColor = ((ColorBlock) @colors).get_normalColor();
              ((Graphic) componentInChildren).set_color(normalColor);
            }
          }
        }
      }
    }
  }
}
