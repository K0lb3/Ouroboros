// Decompiled with JetBrains decompiler
// Type: SRPG.RoomPlayerList
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  [FlowNode.Pin(0, "表示", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(1, "表示更新", FlowNode.PinTypes.Output, 1)]
  [FlowNode.Pin(101, "情報を見る", FlowNode.PinTypes.Output, 101)]
  [AddComponentMenu("Multi/部屋に参加中のプレイヤー一覧")]
  [FlowNode.Pin(200, "チーム編成ボタンが押された", FlowNode.PinTypes.Output, 200)]
  public class RoomPlayerList : MonoBehaviour, IFlowInterface
  {
    [Description("リストアイテムとして使用するゲームオブジェクト")]
    public GameObject ItemTemplate;
    [Description("大本ゲームオブジェクト")]
    public GameObject Root;
    [Description("編成ランキングボタン")]
    public GameObject RankButton;
    public ScrollRect ScrollRect;
    public List<GameObject> UIItemList;

    public RoomPlayerList()
    {
      base.\u002Ector();
    }

    public void Activated(int pinID)
    {
      if (pinID != 0)
        return;
      this.Refresh();
    }

    private void Awake()
    {
      ((Behaviour) this).set_enabled(true);
      if (Object.op_Inequality((Object) this.ItemTemplate, (Object) null) && this.ItemTemplate.get_activeInHierarchy())
        this.ItemTemplate.SetActive(false);
      JSON_MyPhotonRoomParam data = JSON_MyPhotonRoomParam.Parse(PunMonoSingleton<MyPhoton>.Instance.GetCurrentRoom().json);
      QuestParam quest = MonoSingleton<GameManager>.Instance.FindQuest(data.iname);
      DataSource.Bind<JSON_MyPhotonRoomParam>(this.Root, data);
      DataSource.Bind<QuestParam>(this.Root, quest);
      if (Object.op_Inequality((Object) this.RankButton, (Object) null))
        this.RankButton.get_gameObject().SetActive(quest.IsJigen);
      GameParameter.UpdateAll(this.Root);
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 1);
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
      if (Object.op_Equality((Object) this.ItemTemplate, (Object) null))
        return;
      MyPhoton instance = PunMonoSingleton<MyPhoton>.Instance;
      MyPhoton.MyRoom currentRoom = instance.GetCurrentRoom();
      JSON_MyPhotonRoomParam data1 = JSON_MyPhotonRoomParam.Parse(currentRoom.json);
      JSON_MyPhotonPlayerParam photonPlayerParam1 = data1 != null ? data1.GetOwner() : (JSON_MyPhotonPlayerParam) null;
      int num1 = photonPlayerParam1 != null ? photonPlayerParam1.playerIndex : 0;
      List<MyPhoton.MyPlayer> roomPlayerList = instance.GetRoomPlayerList();
      for (int count = this.UIItemList.Count; count < currentRoom.maxPlayers; ++count)
        this.UIItemList.Add((GameObject) Object.Instantiate<GameObject>((M0) this.ItemTemplate));
      for (int index1 = 0; index1 < currentRoom.maxPlayers; ++index1)
      {
        JSON_MyPhotonPlayerParam data2 = (JSON_MyPhotonPlayerParam) null;
        int num2 = index1 + 1;
        if (num1 > 0)
        {
          if (index1 == 0)
            num2 = num1;
          else if (index1 < num1)
            num2 = index1;
        }
        using (List<MyPhoton.MyPlayer>.Enumerator enumerator = roomPlayerList.GetEnumerator())
        {
          while (enumerator.MoveNext())
          {
            MyPhoton.MyPlayer current = enumerator.Current;
            if (current.json != null)
            {
              JSON_MyPhotonPlayerParam photonPlayerParam2 = JSON_MyPhotonPlayerParam.Parse(current.json);
              if (photonPlayerParam2 != null && photonPlayerParam2.playerIndex == num2)
              {
                data2 = photonPlayerParam2;
                break;
              }
            }
          }
        }
        if (data2 == null)
        {
          data2 = new JSON_MyPhotonPlayerParam();
          data2.playerIndex = num2;
        }
        GameObject uiItem = this.UIItemList[index1];
        ((Object) uiItem).set_hideFlags((HideFlags) 52);
        DataSource.Bind<JSON_MyPhotonPlayerParam>(uiItem, data2);
        DataSource.Bind<JSON_MyPhotonRoomParam>(uiItem, data1);
        GameObjectID[] componentsInChildren = (GameObjectID[]) uiItem.GetComponentsInChildren<GameObjectID>(true);
        if (data2 != null && data2.units != null && componentsInChildren != null)
        {
          for (int index2 = 0; index2 < componentsInChildren.Length; ++index2)
          {
            GameObjectID gameObjectId = componentsInChildren[index2];
            if (gameObjectId.ID != null)
              DataSource.Bind<UnitData>(((Component) gameObjectId).get_gameObject(), (UnitData) null);
          }
          for (int index2 = 0; index2 < data2.units.Length; ++index2)
          {
            int slotId = data2.units[index2].slotID;
            UnitData unit = data2.units[index2].unit;
            if (unit != null)
            {
              for (int index3 = 0; index3 < componentsInChildren.Length; ++index3)
              {
                GameObjectID gameObjectId = componentsInChildren[index3];
                if (gameObjectId.ID != null && gameObjectId.ID.Equals("unit" + (object) slotId))
                {
                  DataSource.Bind<UnitData>(((Component) gameObjectId).get_gameObject(), unit);
                  break;
                }
              }
            }
          }
        }
        ListItemEvents component = (ListItemEvents) uiItem.GetComponent<ListItemEvents>();
        if (Object.op_Inequality((Object) component, (Object) null))
        {
          component.OnSelect = new ListItemEvents.ListItemEvent(this.OnSelectItem);
          component.OnOpenDetail = new ListItemEvents.ListItemEvent(this.OnOpenItemDetail);
          component.OnCloseDetail = new ListItemEvents.ListItemEvent(this.OnCloseItemDetail);
        }
        uiItem.get_transform().SetParent(transform, false);
        uiItem.get_gameObject().SetActive(true);
      }
      DataSource.Bind<QuestParam>(this.Root, MonoSingleton<GameManager>.Instance.FindQuest(data1.iname));
      GameParameter.UpdateAll(this.Root);
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 1);
    }

    private void OnSelectItem(GameObject go)
    {
    }

    private void OnCloseItemDetail(GameObject go)
    {
    }

    private void OnOpenItemDetail(GameObject go)
    {
      JSON_MyPhotonPlayerParam dataOfClass = DataSource.FindDataOfClass<JSON_MyPhotonPlayerParam>(go, (JSON_MyPhotonPlayerParam) null);
      if (dataOfClass == null || dataOfClass.playerID <= 0)
        return;
      GlobalVars.SelectedMultiPlayerParam = dataOfClass;
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 101);
    }

    private void Update()
    {
    }

    public void OnEditTeam()
    {
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 200);
    }
  }
}
