// Decompiled with JetBrains decompiler
// Type: SRPG.RoomList
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  [AddComponentMenu("Multi/参加募集中の部屋一覧")]
  [FlowNode.Pin(2, "現状表示", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(101, "選択された", FlowNode.PinTypes.Output, 0)]
  [FlowNode.Pin(0, "ノーマル表示", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(1, "イベント表示", FlowNode.PinTypes.Input, 0)]
  public class RoomList : MonoBehaviour, IFlowInterface
  {
    [Description("リストアイテムとして使用するゲームオブジェクト")]
    public GameObject ItemTemplate;
    [Description("詳細画面として使用するゲームオブジェクト")]
    public GameObject DetailTemplate;
    private GameObject mDetailInfo;
    public ScrollRect ScrollRect;

    public RoomList()
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
        case 2:
          this.Refresh(GlobalVars.SelectedMultiPlayQuestIsEvent);
          break;
      }
    }

    private void Awake()
    {
      GlobalVars.SelectedMultiPlayQuestIsEvent = false;
      GlobalVars.SelectedMultiPlayArea = (string) null;
      ((Behaviour) this).set_enabled(true);
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
      for (int index = transform.get_childCount() - 1; index >= 0; --index)
      {
        Transform child = transform.GetChild(index);
        if (!Object.op_Equality((Object) child, (Object) null) && ((Component) child).get_gameObject().get_activeSelf())
          Object.DestroyImmediate((Object) ((Component) child).get_gameObject());
      }
      if (Object.op_Equality((Object) this.ItemTemplate, (Object) null))
        return;
      MultiPlayAPIRoom[] multiPlayApiRoomArray = FlowNode_MultiPlayAPI.RoomList != null ? FlowNode_MultiPlayAPI.RoomList.rooms : (MultiPlayAPIRoom[]) null;
      if (multiPlayApiRoomArray == null)
        return;
      for (int index = 0; index < multiPlayApiRoomArray.Length; ++index)
      {
        MultiPlayAPIRoom data1 = multiPlayApiRoomArray[index];
        if (data1 != null && data1.quest != null && !string.IsNullOrEmpty(data1.quest.iname))
        {
          QuestParam quest = MonoSingleton<GameManager>.Instance.FindQuest(data1.quest.iname);
          if (quest != null && quest.IsMultiEvent == GlobalVars.SelectedMultiPlayQuestIsEvent && (GlobalVars.SelectedMultiPlayArea == null || quest.ChapterID.Equals(GlobalVars.SelectedMultiPlayArea)))
          {
            GameObject gameObject = (GameObject) Object.Instantiate<GameObject>((M0) this.ItemTemplate);
            ((Object) gameObject).set_hideFlags((HideFlags) 52);
            Json_Unit[] jsonUnitArray = data1.owner != null ? data1.owner.units : (Json_Unit[]) null;
            if (jsonUnitArray != null && jsonUnitArray.Length > 0)
            {
              UnitData data2 = new UnitData();
              data2.Deserialize(jsonUnitArray[0]);
              DataSource.Bind<UnitData>(gameObject, data2);
            }
            DataSource.Bind<MultiPlayAPIRoom>(gameObject, data1);
            DataSource.Bind<QuestParam>(gameObject, quest);
            DebugUtility.Log("found roomid:" + (object) data1.roomid + " room:" + data1.comment + " iname:" + quest.iname + " playerNum:" + (object) quest.playerNum + " unitNum:" + (object) quest.unitNum);
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
      GameParameter.UpdateAll(((Component) transform).get_gameObject());
    }

    private void OnSelectItem(GameObject go)
    {
      MultiPlayAPIRoom dataOfClass = DataSource.FindDataOfClass<MultiPlayAPIRoom>(go, (MultiPlayAPIRoom) null);
      if (dataOfClass == null)
        return;
      GlobalVars.SelectedMultiPlayRoomID = dataOfClass.roomid;
      GlobalVars.SelectedMultiPlayRoomPassCodeHash = dataOfClass.pwd_hash;
      DebugUtility.Log("Select RoomID:" + (object) GlobalVars.SelectedMultiPlayRoomID + " PassCodeHash:" + GlobalVars.SelectedMultiPlayRoomPassCodeHash);
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 101);
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
      this.mDetailInfo.SetActive(true);
    }

    private void Update()
    {
    }
  }
}
