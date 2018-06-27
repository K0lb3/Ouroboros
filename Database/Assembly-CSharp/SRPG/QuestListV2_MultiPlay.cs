// Decompiled with JetBrains decompiler
// Type: SRPG.QuestListV2_MultiPlay
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  [FlowNode.Pin(50, "鍵なし", FlowNode.PinTypes.Input, 50)]
  [FlowNode.Pin(51, "鍵あり", FlowNode.PinTypes.Input, 51)]
  [FlowNode.Pin(101, "選択された", FlowNode.PinTypes.Output, 101)]
  [AddComponentMenu("Multi/クエスト一覧")]
  [FlowNode.Pin(0, "表示", FlowNode.PinTypes.Input, 0)]
  public class QuestListV2_MultiPlay : MonoBehaviour, IFlowInterface
  {
    [Description("リストアイテムとして使用するゲームオブジェクト")]
    public GameObject ItemTemplate;
    [Description("詳細画面として使用するゲームオブジェクト")]
    public GameObject DetailTemplate;
    private GameObject mDetailInfo;
    public ScrollRect ScrollRect;

    public QuestListV2_MultiPlay()
    {
      base.\u002Ector();
    }

    public void Activated(int pinID)
    {
      switch (pinID)
      {
        case 0:
          this.Refresh();
          break;
        case 50:
          GlobalVars.EditMultiPlayRoomPassCode = (string) null;
          GameParameter.UpdateValuesOfType(GameParameter.ParameterTypes.QUEST_MULTI_LOCK);
          break;
        case 51:
          GlobalVars.EditMultiPlayRoomPassCode = "1";
          GameParameter.UpdateValuesOfType(GameParameter.ParameterTypes.QUEST_MULTI_LOCK);
          break;
      }
    }

    private void Awake()
    {
      GlobalVars.EditMultiPlayRoomPassCode = "0";
      string s = FlowNode_Variable.Get("MultiPlayPasscode");
      if (!string.IsNullOrEmpty(s))
      {
        int result = 0;
        if (int.TryParse(s, out result) && result != 0)
          this.Activated(51);
      }
      if (Object.op_Inequality((Object) this.ItemTemplate, (Object) null) && this.ItemTemplate.get_activeInHierarchy())
        this.ItemTemplate.SetActive(false);
      if (!Object.op_Inequality((Object) this.DetailTemplate, (Object) null) || !this.DetailTemplate.get_activeInHierarchy())
        return;
      this.DetailTemplate.SetActive(false);
    }

    private void Start()
    {
      this.RefreshItems();
    }

    public void Refresh()
    {
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
      for (int index = transform.get_childCount() - 1; index >= 0; --index)
      {
        Transform child = transform.GetChild(index);
        if (!Object.op_Equality((Object) child, (Object) null) && ((Component) child).get_gameObject().get_activeSelf())
          Object.DestroyImmediate((Object) ((Component) child).get_gameObject());
      }
      if (Object.op_Equality((Object) this.ItemTemplate, (Object) null))
        return;
      for (int index = 0; index < MonoSingleton<GameManager>.Instance.Quests.Length; ++index)
      {
        QuestParam quest = MonoSingleton<GameManager>.Instance.Quests[index];
        if (quest != null && quest.type == QuestTypes.Multi && (quest.IsMultiEvent == GlobalVars.SelectedMultiPlayQuestIsEvent && !string.IsNullOrEmpty(quest.ChapterID)) && (quest.ChapterID.Equals(GlobalVars.SelectedMultiPlayArea) && quest.IsDateUnlock(-1L)))
        {
          GameObject gameObject = (GameObject) Object.Instantiate<GameObject>((M0) this.ItemTemplate);
          ((Object) gameObject).set_hideFlags((HideFlags) 52);
          DataSource.Bind<QuestParam>(gameObject, quest);
          ListItemEvents component = (ListItemEvents) gameObject.GetComponent<ListItemEvents>();
          if (Object.op_Inequality((Object) component, (Object) null))
          {
            component.OnSelect = new ListItemEvents.ListItemEvent(this.OnSelectItem);
            component.OnOpenDetail = new ListItemEvents.ListItemEvent(this.OnOpenItemDetail);
            component.OnCloseDetail = new ListItemEvents.ListItemEvent(this.OnCloseItemDetail);
          }
          gameObject.get_transform().SetParent(transform, false);
          gameObject.get_gameObject().SetActive(true);
        }
      }
    }

    private void OnSelectItem(GameObject go)
    {
      QuestParam dataOfClass = DataSource.FindDataOfClass<QuestParam>(go, (QuestParam) null);
      if (dataOfClass == null || !Object.op_Inequality((Object) QuestDropParam.Instance, (Object) null))
        return;
      bool flag = QuestDropParam.Instance.IsChangedQuestDrops(dataOfClass);
      GlobalVars.SetDropTableGeneratedTime();
      if (flag && !QuestDropParam.Instance.IsWarningPopupDisable)
      {
        UIUtility.NegativeSystemMessage((string) null, LocalizedText.Get("sys.PARTYEDITOR_DROP_TABLE"), (UIUtility.DialogResultEvent) (obj =>
        {
          ListItemEvents component = (ListItemEvents) go.GetComponent<ListItemEvents>();
          if (!Object.op_Inequality((Object) component, (Object) null))
            return;
          component.OpenDetail();
        }), (GameObject) null, false, -1);
      }
      else
      {
        GlobalVars.SelectedQuestID = dataOfClass.iname;
        DebugUtility.Log("Select Quest:" + GlobalVars.SelectedQuestID);
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 101);
      }
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
      CanvasGroup componentInParent = (CanvasGroup) ((Component) this).get_gameObject().GetComponentInParent<CanvasGroup>();
      if (Object.op_Inequality((Object) componentInParent, (Object) null) && !componentInParent.get_interactable() || (!Object.op_Equality((Object) this.mDetailInfo, (Object) null) || dataOfClass == null))
        return;
      this.mDetailInfo = (GameObject) Object.Instantiate<GameObject>((M0) this.DetailTemplate);
      DataSource.Bind<QuestParam>(this.mDetailInfo, dataOfClass);
      this.mDetailInfo.SetActive(true);
    }
  }
}
