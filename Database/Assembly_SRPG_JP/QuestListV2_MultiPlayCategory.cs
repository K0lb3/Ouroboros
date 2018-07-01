// Decompiled with JetBrains decompiler
// Type: SRPG.QuestListV2_MultiPlayCategory
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  [FlowNode.Pin(1, "イベントクエストを表示", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(2, "GPSクエストを含めて表示", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(101, "選択された", FlowNode.PinTypes.Output, 101)]
  [AddComponentMenu("Multi/クエストカテゴリー一覧")]
  [FlowNode.Pin(0, "通常クエストを表示", FlowNode.PinTypes.Input, 0)]
  public class QuestListV2_MultiPlayCategory : MonoBehaviour, IFlowInterface
  {
    [Description("リストアイテムとして使用するゲームオブジェクト")]
    public ListItemEvents ItemTemplate;
    [Description("詳細画面として使用するゲームオブジェクト")]
    public GameObject DetailTemplate;
    private GameObject mDetailInfo;
    public ScrollRect ScrollRect;

    public QuestListV2_MultiPlayCategory()
    {
      base.\u002Ector();
    }

    public void Activated(int pinID)
    {
      switch (pinID)
      {
        case 0:
          this.Refresh(false, QuestListV2_MultiPlayCategory.DISPLAY_QUEST_TYPE.Normal);
          break;
        case 1:
          this.Refresh(true, QuestListV2_MultiPlayCategory.DISPLAY_QUEST_TYPE.Normal);
          break;
        case 2:
          this.Refresh(false, QuestListV2_MultiPlayCategory.DISPLAY_QUEST_TYPE.WithGps);
          break;
      }
    }

    private void Awake()
    {
      GlobalVars.SelectedMultiPlayQuestIsEvent = false;
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ItemTemplate, (UnityEngine.Object) null) && ((Component) this.ItemTemplate).get_gameObject().get_activeInHierarchy())
        ((Component) this.ItemTemplate).get_gameObject().SetActive(false);
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.DetailTemplate, (UnityEngine.Object) null) || !this.DetailTemplate.get_activeInHierarchy())
        return;
      this.DetailTemplate.SetActive(false);
    }

    private void Start()
    {
    }

    private void Refresh(bool isEvent, QuestListV2_MultiPlayCategory.DISPLAY_QUEST_TYPE type = QuestListV2_MultiPlayCategory.DISPLAY_QUEST_TYPE.Normal)
    {
      GlobalVars.SelectedMultiPlayQuestIsEvent = isEvent;
      this.RefreshItems(type);
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ScrollRect, (UnityEngine.Object) null))
        return;
      ListExtras component = (ListExtras) ((Component) this.ScrollRect).GetComponent<ListExtras>();
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
        component.SetScrollPos(1f);
      else
        this.ScrollRect.set_normalizedPosition(Vector2.get_one());
    }

    private void RefreshItems(QuestListV2_MultiPlayCategory.DISPLAY_QUEST_TYPE dispMode)
    {
      Transform transform = ((Component) this).get_transform();
      DateTime serverTime = TimeManager.ServerTime;
      for (int index = transform.get_childCount() - 1; index >= 0; --index)
      {
        Transform child = transform.GetChild(index);
        if (!UnityEngine.Object.op_Equality((UnityEngine.Object) child, (UnityEngine.Object) null) && ((Component) child).get_gameObject().get_activeSelf())
          UnityEngine.Object.DestroyImmediate((UnityEngine.Object) ((Component) child).get_gameObject());
      }
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.ItemTemplate, (UnityEngine.Object) null))
        return;
      List<ChapterParam> chapterParamList = new List<ChapterParam>();
      for (int index = 0; index < MonoSingleton<GameManager>.Instance.Quests.Length; ++index)
      {
        QuestParam quest = MonoSingleton<GameManager>.Instance.Quests[index];
        if (quest != null && (quest.type == QuestTypes.Multi || quest.IsMultiAreaQuest) && (quest.IsMultiEvent == GlobalVars.SelectedMultiPlayQuestIsEvent && quest.IsMultiVersus == (GlobalVars.SelectedMultiPlayRoomType == JSON_MyPhotonRoomParam.EType.VERSUS) && (!quest.IsMultiAreaQuest || dispMode == QuestListV2_MultiPlayCategory.DISPLAY_QUEST_TYPE.WithGps)) && ((!quest.IsMultiAreaQuest || quest.gps_enable) && quest.IsDateUnlock(-1L)))
        {
          ChapterParam area = MonoSingleton<GameManager>.Instance.FindArea(quest.ChapterID);
          if (area != null && area.IsAvailable(serverTime) && chapterParamList.Find((Predicate<ChapterParam>) (a => a.iname.Equals(area.iname))) == null)
            chapterParamList.Add(area);
        }
      }
      Dictionary<string, int> indexList = new Dictionary<string, int>();
      for (int index = 0; index < chapterParamList.Count; ++index)
        indexList.Add(chapterParamList[index].iname, index);
      chapterParamList.Sort((Comparison<ChapterParam>) ((x, y) =>
      {
        bool flag1 = x.IsMultiGpsQuest();
        bool flag2 = y.IsMultiGpsQuest();
        if (flag1 && !flag2)
          return -1;
        if (!flag1 && flag2)
          return 1;
        if (!indexList.ContainsKey(x.iname) || !indexList.ContainsKey(y.iname))
          return 0;
        return indexList[x.iname] - indexList[y.iname];
      }));
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
        if (UnityEngine.Object.op_Equality((UnityEngine.Object) listItemEvents1, (UnityEngine.Object) null))
          listItemEvents1 = this.ItemTemplate;
        ListItemEvents listItemEvents2 = (ListItemEvents) UnityEngine.Object.Instantiate<ListItemEvents>((M0) listItemEvents1);
        DataSource.Bind<ChapterParam>(((Component) listItemEvents2).get_gameObject(), chapterParamList[index]);
        listItemEvents2.OnSelect = new ListItemEvents.ListItemEvent(this.OnSelectItem);
        listItemEvents2.OnOpenDetail = new ListItemEvents.ListItemEvent(this.OnOpenItemDetail);
        listItemEvents2.OnCloseDetail = new ListItemEvents.ListItemEvent(this.OnCloseItemDetail);
        Transform child1 = ((Component) listItemEvents2).get_transform().FindChild("bg");
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) child1, (UnityEngine.Object) null))
        {
          Transform child2 = child1.FindChild("timer_base");
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) child2, (UnityEngine.Object) null) && chapterParamList[index].end <= 0L)
            ((Component) child2).get_gameObject().SetActive(false);
        }
        ((Component) listItemEvents2).get_transform().SetParent(transform, false);
        ((Component) listItemEvents2).get_gameObject().SetActive(true);
      }
    }

    private void OnSelectItem(GameObject go)
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
        if (availableQuests[index].ChapterID == dataOfClass.iname && availableQuests[index].IsMulti)
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
        GlobalVars.SelectedMultiPlayArea = dataOfClass.iname;
        DebugUtility.Log("Select Play Area:" + GlobalVars.SelectedMultiPlayArea);
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 101);
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
    }

    private enum DISPLAY_QUEST_TYPE
    {
      Normal,
      WithGps,
      Max,
    }
  }
}
