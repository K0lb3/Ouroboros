// Decompiled with JetBrains decompiler
// Type: SRPG.RoomPlayerList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  [FlowNode.Pin(3, "階層更新完了", FlowNode.PinTypes.Output, 3)]
  [FlowNode.Pin(10, "データソース更新", FlowNode.PinTypes.Input, 4)]
  [FlowNode.Pin(200, "チーム編成ボタンが押された", FlowNode.PinTypes.Output, 200)]
  [AddComponentMenu("Multi/部屋に参加中のプレイヤー一覧")]
  [FlowNode.Pin(0, "表示", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(1, "表示更新", FlowNode.PinTypes.Output, 1)]
  [FlowNode.Pin(2, "階層更新", FlowNode.PinTypes.Input, 2)]
  [FlowNode.Pin(101, "情報を見る", FlowNode.PinTypes.Output, 101)]
  public class RoomPlayerList : MonoBehaviour, IFlowInterface
  {
    [Description("リストアイテムとして使用するゲームオブジェクト")]
    public GameObject ItemTemplate;
    [Description("大本ゲームオブジェクト")]
    public GameObject Root;
    [Description("編成ランキングボタン")]
    public GameObject RankButton;
    [Description("スキル名")]
    public GameObject SkillObj;
    [Description("スキル詳細用プレハブ")]
    public GameObject Prefab_LeaderSkillDetail;
    public ScrollRect ScrollRect;
    public List<GameObject> UIItemList;
    [Description("プレイヤーのパーティ情報表示用のゲームオブジェクト")]
    public GameObject PlayerInfo;
    private List<GameObject> PlayerInfoList;

    public RoomPlayerList()
    {
      base.\u002Ector();
    }

    public void Activated(int pinID)
    {
      if (pinID == 0)
        this.Refresh();
      if (pinID == 2)
      {
        this.RefreshFloorQuest();
      }
      else
      {
        if (pinID != 10)
          return;
        DataSource.Bind<JSON_MyPhotonRoomParam>(this.Root, JSON_MyPhotonRoomParam.Parse(PunMonoSingleton<MyPhoton>.Instance.GetCurrentRoom().json));
      }
    }

    private void Awake()
    {
      ((Behaviour) this).set_enabled(true);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ItemTemplate, (UnityEngine.Object) null) && this.ItemTemplate.get_activeInHierarchy())
        this.ItemTemplate.SetActive(false);
      JSON_MyPhotonRoomParam data = JSON_MyPhotonRoomParam.Parse(PunMonoSingleton<MyPhoton>.Instance.GetCurrentRoom().json);
      QuestParam quest = MonoSingleton<GameManager>.Instance.FindQuest(data.iname);
      DataSource.Bind<JSON_MyPhotonRoomParam>(this.Root, data);
      DataSource.Bind<QuestParam>(this.Root, quest);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.RankButton, (UnityEngine.Object) null))
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
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ScrollRect, (UnityEngine.Object) null))
        return;
      ListExtras component = (ListExtras) ((Component) this.ScrollRect).GetComponent<ListExtras>();
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
        component.SetScrollPos(1f);
      else
        this.ScrollRect.set_normalizedPosition(Vector2.get_one());
    }

    public void RefreshFloorQuest()
    {
      JSON_MyPhotonRoomParam data = JSON_MyPhotonRoomParam.Parse(PunMonoSingleton<MyPhoton>.Instance.GetCurrentRoom().json);
      QuestParam quest = MonoSingleton<GameManager>.Instance.FindQuest(data.iname);
      DataSource.Bind<JSON_MyPhotonRoomParam>(this.Root, data);
      DataSource.Bind<QuestParam>(this.Root, quest);
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 3);
    }

    private void RefreshItems()
    {
      Transform transform = ((Component) this).get_transform();
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.ItemTemplate, (UnityEngine.Object) null))
        return;
      MyPhoton instance = PunMonoSingleton<MyPhoton>.Instance;
      MyPhoton.MyRoom currentRoom = instance.GetCurrentRoom();
      JSON_MyPhotonRoomParam data1 = JSON_MyPhotonRoomParam.Parse(currentRoom.json);
      JSON_MyPhotonPlayerParam owner = data1?.GetOwner();
      int num1 = owner != null ? owner.playerIndex : 0;
      List<MyPhoton.MyPlayer> roomPlayerList1 = instance.GetRoomPlayerList();
      for (int count = this.UIItemList.Count; count < currentRoom.maxPlayers; ++count)
      {
        this.UIItemList.Add((GameObject) UnityEngine.Object.Instantiate<GameObject>((M0) this.ItemTemplate));
        if (GlobalVars.SelectedMultiPlayRoomType == JSON_MyPhotonRoomParam.EType.TOWER)
        {
          this.PlayerInfo.SetActive(false);
          this.PlayerInfoList.Add((GameObject) UnityEngine.Object.Instantiate<GameObject>((M0) this.PlayerInfo));
        }
      }
      int maxPlayers = currentRoom.maxPlayers;
      if (GlobalVars.SelectedMultiPlayRoomType == JSON_MyPhotonRoomParam.EType.VERSUS)
        --maxPlayers;
      for (int index1 = 0; index1 < maxPlayers; ++index1)
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
        using (List<MyPhoton.MyPlayer>.Enumerator enumerator = roomPlayerList1.GetEnumerator())
        {
          while (enumerator.MoveNext())
          {
            MyPhoton.MyPlayer current = enumerator.Current;
            if (current.json != null)
            {
              JSON_MyPhotonPlayerParam photonPlayerParam = JSON_MyPhotonPlayerParam.Parse(current.json);
              if (photonPlayerParam != null && photonPlayerParam.playerIndex == num2)
              {
                data2 = photonPlayerParam;
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
        ((UnityEngine.Object) uiItem).set_hideFlags((HideFlags) 52);
        DataSource.Bind<JSON_MyPhotonPlayerParam>(uiItem, data2);
        DataSource.Bind<JSON_MyPhotonRoomParam>(uiItem, data1);
        if (GlobalVars.SelectedMultiPlayRoomType == JSON_MyPhotonRoomParam.EType.TOWER)
        {
          DataSource.Bind<JSON_MyPhotonPlayerParam>(this.PlayerInfoList[index1], data2);
          DataSource.Bind<JSON_MyPhotonRoomParam>(this.PlayerInfoList[index1], data1);
          this.PlayerInfoList[index1].get_transform().SetParent(this.PlayerInfo.get_transform().get_parent(), false);
          this.PlayerInfoList[index1].get_gameObject().SetActive(true);
        }
        GameObjectID[] componentsInChildren = (GameObjectID[]) uiItem.GetComponentsInChildren<GameObjectID>(true);
        if (data2 != null && componentsInChildren != null)
        {
          for (int index2 = 0; index2 < componentsInChildren.Length; ++index2)
          {
            GameObjectID gameObjectId = componentsInChildren[index2];
            if (gameObjectId.ID != null)
              DataSource.Bind<UnitData>(((Component) gameObjectId).get_gameObject(), (UnitData) null);
          }
          if (data1.draft_type == 1 && !string.IsNullOrEmpty(data2.support_unit))
          {
            GameObjectID gameObjectId = Array.Find<GameObjectID>(componentsInChildren, (Predicate<GameObjectID>) (goID => goID.ID.Equals("unit0")));
            Json_Unit jsonObject = JSONParser.parseJSONObject<Json_Unit>(data2.support_unit);
            if (UnityEngine.Object.op_Inequality((UnityEngine.Object) gameObjectId, (UnityEngine.Object) null) && jsonObject != null)
            {
              UnitData data3 = new UnitData();
              data3.Deserialize(jsonObject);
              DataSource.Bind<UnitData>(((Component) gameObjectId).get_gameObject(), data3);
            }
          }
          else if (data2.units != null)
          {
            for (int index2 = 0; index2 < data2.units.Length; ++index2)
            {
              int slotId = data2.units[index2].slotID;
              bool flag1 = data2.units[index2].sub == 1;
              UnitData data3 = data2.units[index2].unit;
              if (data3 != null)
              {
                if (GlobalVars.SelectedMultiPlayRoomType == JSON_MyPhotonRoomParam.EType.TOWER)
                {
                  for (int index3 = 0; index3 < componentsInChildren.Length; ++index3)
                  {
                    GameObjectID gameObjectId = componentsInChildren[index3];
                    if (gameObjectId.ID != null && (gameObjectId.ID.Equals("unit" + (object) slotId) || flag1) && (!flag1 || index3 == componentsInChildren.Length - 1))
                    {
                      data3.TempFlags |= UnitData.TemporaryFlags.AllowJobChange;
                      DataSource.Bind<UnitData>(((Component) gameObjectId).get_gameObject(), data3);
                      UnitIcon component = (UnitIcon) ((Component) gameObjectId).get_gameObject().GetComponent<UnitIcon>();
                      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null) && data2.playerIndex > 0)
                      {
                        DataSource.Bind<PlayerPartyTypes>(((Component) gameObjectId).get_gameObject(), PlayerPartyTypes.MultiTower);
                        bool flag2 = data2.playerIndex == PunMonoSingleton<MyPhoton>.Instance.MyPlayerIndex;
                        component.AllowJobChange = flag2;
                        if (flag2)
                        {
                          UnitData unitDataByUnitId = MonoSingleton<GameManager>.Instance.Player.FindUnitDataByUnitID(data3.UnitParam.iname);
                          if (unitDataByUnitId != null)
                          {
                            unitDataByUnitId.TempFlags |= UnitData.TemporaryFlags.AllowJobChange;
                            DataSource.Bind<UnitData>(((Component) gameObjectId).get_gameObject(), unitDataByUnitId);
                            data3 = unitDataByUnitId;
                          }
                        }
                      }
                      if (index3 == 0 && this.PlayerInfoList.Count > index1)
                      {
                        DataSource.Bind<UnitData>(this.PlayerInfoList[index1], data3);
                        break;
                      }
                      break;
                    }
                  }
                }
                else
                {
                  for (int index3 = 0; index3 < componentsInChildren.Length; ++index3)
                  {
                    GameObjectID gameObjectId = componentsInChildren[index3];
                    if (gameObjectId.ID != null && gameObjectId.ID.Equals("unit" + (object) slotId))
                    {
                      DataSource.Bind<UnitData>(((Component) gameObjectId).get_gameObject(), data3);
                      break;
                    }
                  }
                }
              }
            }
          }
        }
        ListItemEvents component1 = (ListItemEvents) uiItem.GetComponent<ListItemEvents>();
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component1, (UnityEngine.Object) null))
        {
          component1.OnSelect = new ListItemEvents.ListItemEvent(this.OnSelectItem);
          component1.OnOpenDetail = new ListItemEvents.ListItemEvent(this.OnOpenItemDetail);
          component1.OnCloseDetail = new ListItemEvents.ListItemEvent(this.OnCloseItemDetail);
        }
        uiItem.get_transform().SetParent(transform, false);
        uiItem.get_gameObject().SetActive(true);
      }
      QuestParam quest = MonoSingleton<GameManager>.Instance.FindQuest(data1.iname);
      DataSource.Bind<QuestParam>(this.Root, quest);
      bool flag = false;
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.SkillObj, (UnityEngine.Object) null) && quest != null)
      {
        if (quest.IsMultiLeaderSkill)
        {
          List<MyPhoton.MyPlayer> roomPlayerList2 = instance.GetRoomPlayerList();
          if (roomPlayerList2 != null)
          {
            MyPhoton.MyPlayer myPlayer = roomPlayerList2.Find((Predicate<MyPhoton.MyPlayer>) (member => member.playerID == 1));
            if (myPlayer != null)
            {
              JSON_MyPhotonPlayerParam photonPlayerParam = JSON_MyPhotonPlayerParam.Parse(myPlayer.json);
              if (photonPlayerParam != null && photonPlayerParam.units != null && photonPlayerParam.units.Length > 0)
              {
                UnitData data2 = new UnitData();
                if (data2 != null)
                {
                  data2.Deserialize(photonPlayerParam.units[0].unitJson);
                  DataSource.Bind<UnitData>(this.SkillObj, data2);
                  flag = data2.LeaderSkill != null;
                }
              }
            }
          }
        }
        SRPG_Button component = (SRPG_Button) this.SkillObj.GetComponent<SRPG_Button>();
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
          ((Selectable) component).set_interactable(quest.IsMultiLeaderSkill && flag);
      }
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

    public void OnEditTeamMultiTower(int index)
    {
      int[] numArray = new int[3]{ 0, 1, 2 };
      if (index >= 0 && index < numArray.Length)
        GlobalVars.SelectedTowerMultiPartyIndex = numArray[index];
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 200);
    }

    public void OpenLeaderSkillDetail()
    {
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.SkillObj, (UnityEngine.Object) null) || !UnityEngine.Object.op_Inequality((UnityEngine.Object) this.Prefab_LeaderSkillDetail, (UnityEngine.Object) null))
        return;
      UnitData dataOfClass = DataSource.FindDataOfClass<UnitData>(this.SkillObj, (UnitData) null);
      if (dataOfClass == null)
        return;
      DataSource.Bind<UnitData>((GameObject) UnityEngine.Object.Instantiate<GameObject>((M0) this.Prefab_LeaderSkillDetail), dataOfClass);
    }
  }
}
