// Decompiled with JetBrains decompiler
// Type: FlowNode_MultiPlayIsReady
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using SRPG;
using System.Collections.Generic;
using UnityEngine;

[FlowNode.Pin(301, "マルチタワーのスタミナは足りているか", FlowNode.PinTypes.Input, 301)]
[FlowNode.NodeType("Multi/MultiPlayIsReady", 32741)]
[FlowNode.Pin(100, "Are you ready for preparation?", FlowNode.PinTypes.Input, 100)]
[FlowNode.Pin(101, "Is everyone ready?", FlowNode.PinTypes.Input, 101)]
[FlowNode.Pin(102, "Are all the members except the room owner ready?", FlowNode.PinTypes.Input, 102)]
[FlowNode.Pin(200, "Is the room full?", FlowNode.PinTypes.Input, 200)]
[FlowNode.Pin(300, "Is stamina sufficient?", FlowNode.PinTypes.Input, 300)]
[FlowNode.Pin(400, "ユニット配置が被っていないか？", FlowNode.PinTypes.Input, 400)]
[FlowNode.Pin(401, "出撃制限に引っかかっていないか？", FlowNode.PinTypes.Input, 401)]
[FlowNode.Pin(402, "自身の出撃制限に引っかかっていないか？", FlowNode.PinTypes.Input, 402)]
[FlowNode.Pin(403, "挑戦できる階層か？", FlowNode.PinTypes.Input, 403)]
[FlowNode.Pin(1, "Yes", FlowNode.PinTypes.Output, 2)]
[FlowNode.Pin(2, "No", FlowNode.PinTypes.Output, 3)]
public class FlowNode_MultiPlayIsReady : FlowNode
{
  public override void OnActivate(int pinID)
  {
    switch (pinID)
    {
      case 100:
        MyPhoton.MyPlayer myPlayer1 = PunMonoSingleton<MyPhoton>.Instance.GetMyPlayer();
        JSON_MyPhotonPlayerParam photonPlayerParam1 = myPlayer1 != null ? JSON_MyPhotonPlayerParam.Parse(myPlayer1.json) : (JSON_MyPhotonPlayerParam) null;
        if (photonPlayerParam1 != null && photonPlayerParam1.state != 0 && photonPlayerParam1.state != 4)
        {
          this.ActivateOutputLinks(1);
          break;
        }
        this.ActivateOutputLinks(2);
        break;
      case 101:
        List<MyPhoton.MyPlayer> roomPlayerList1 = PunMonoSingleton<MyPhoton>.Instance.GetRoomPlayerList();
        if (roomPlayerList1 != null)
        {
          using (List<MyPhoton.MyPlayer>.Enumerator enumerator = roomPlayerList1.GetEnumerator())
          {
            while (enumerator.MoveNext())
            {
              MyPhoton.MyPlayer current = enumerator.Current;
              JSON_MyPhotonPlayerParam photonPlayerParam2 = current != null ? JSON_MyPhotonPlayerParam.Parse(current.json) : (JSON_MyPhotonPlayerParam) null;
              if (photonPlayerParam2 == null || photonPlayerParam2.state == 0 || (photonPlayerParam2.state == 4 || photonPlayerParam2.state == 5))
              {
                this.ActivateOutputLinks(2);
                return;
              }
            }
          }
        }
        this.ActivateOutputLinks(1);
        break;
      case 102:
        MyPhoton instance1 = PunMonoSingleton<MyPhoton>.Instance;
        List<MyPhoton.MyPlayer> roomPlayerList2 = instance1.GetRoomPlayerList();
        MyPhoton.MyPlayer myPlayer2 = instance1.GetMyPlayer();
        if (roomPlayerList2 != null && myPlayer2 != null)
        {
          using (List<MyPhoton.MyPlayer>.Enumerator enumerator = roomPlayerList2.GetEnumerator())
          {
            while (enumerator.MoveNext())
            {
              MyPhoton.MyPlayer current = enumerator.Current;
              if (current.playerID != myPlayer2.playerID)
              {
                JSON_MyPhotonPlayerParam photonPlayerParam2 = current != null ? JSON_MyPhotonPlayerParam.Parse(current.json) : (JSON_MyPhotonPlayerParam) null;
                if (photonPlayerParam2 == null || photonPlayerParam2.state == 0 || (photonPlayerParam2.state == 4 || photonPlayerParam2.state == 5) || current.start)
                {
                  this.ActivateOutputLinks(2);
                  return;
                }
              }
            }
          }
        }
        this.ActivateOutputLinks(1);
        break;
      case 200:
        MyPhoton.MyRoom currentRoom1 = PunMonoSingleton<MyPhoton>.Instance.GetCurrentRoom();
        int maxPlayers = currentRoom1.maxPlayers;
        if (currentRoom1 == null || currentRoom1.playerCount < maxPlayers)
        {
          this.ActivateOutputLinks(2);
          break;
        }
        this.ActivateOutputLinks(1);
        break;
      case 300:
        if (string.IsNullOrEmpty(GlobalVars.SelectedQuestID))
          this.ActivateOutputLinks(2);
        QuestParam quest = MonoSingleton<GameManager>.Instance.FindQuest(GlobalVars.SelectedQuestID);
        PlayerData player1 = MonoSingleton<GameManager>.Instance.Player;
        if (quest != null && player1 != null)
        {
          if (player1.Stamina >= quest.RequiredApWithPlayerLv(player1.Lv, true))
          {
            this.ActivateOutputLinks(1);
            break;
          }
          MonoSingleton<GameManager>.Instance.StartBuyStaminaSequence(true);
          this.ActivateOutputLinks(2);
          break;
        }
        this.ActivateOutputLinks(2);
        break;
      case 301:
        MyPhoton instance2 = PunMonoSingleton<MyPhoton>.Instance;
        if (Object.op_Inequality((Object) instance2, (Object) null))
        {
          List<MyPhoton.MyPlayer> roomPlayerList3 = instance2.GetRoomPlayerList();
          MyPhoton.MyRoom currentRoom2 = instance2.GetCurrentRoom();
          if (currentRoom2 == null)
          {
            this.ActivateOutputLinks(2);
            break;
          }
          JSON_MyPhotonRoomParam myPhotonRoomParam = JSON_MyPhotonRoomParam.Parse(currentRoom2.json);
          if (roomPlayerList3 == null || myPhotonRoomParam == null)
          {
            this.ActivateOutputLinks(2);
            break;
          }
          MultiTowerFloorParam mtFloorParam = MonoSingleton<GameManager>.Instance.GetMTFloorParam(GlobalVars.SelectedMultiTowerID, myPhotonRoomParam.challegedMTFloor);
          if (mtFloorParam == null)
          {
            this.ActivateOutputLinks(2);
            break;
          }
          QuestParam questParam = mtFloorParam.GetQuestParam();
          PlayerData player2 = MonoSingleton<GameManager>.Instance.Player;
          if (questParam != null && player2 != null)
          {
            if (player2.Stamina >= questParam.RequiredApWithPlayerLv(player2.Lv, true))
            {
              this.ActivateOutputLinks(1);
              break;
            }
            MonoSingleton<GameManager>.Instance.StartBuyStaminaSequence(true);
            this.ActivateOutputLinks(2);
            break;
          }
        }
        this.ActivateOutputLinks(2);
        break;
      case 400:
        MyPhoton instance3 = PunMonoSingleton<MyPhoton>.Instance;
        if (Object.op_Inequality((Object) instance3, (Object) null))
        {
          List<MyPhoton.MyPlayer> roomPlayerList3 = instance3.GetRoomPlayerList();
          MyPhoton.MyRoom currentRoom2 = instance3.GetCurrentRoom();
          if (currentRoom2 == null)
          {
            this.ActivateOutputLinks(2);
            break;
          }
          JSON_MyPhotonRoomParam myPhotonRoomParam = JSON_MyPhotonRoomParam.Parse(currentRoom2.json);
          if (roomPlayerList3 == null || myPhotonRoomParam == null)
          {
            this.ActivateOutputLinks(2);
            break;
          }
          MultiTowerFloorParam mtFloorParam = MonoSingleton<GameManager>.Instance.GetMTFloorParam(GlobalVars.SelectedMultiTowerID, myPhotonRoomParam.challegedMTFloor);
          if (mtFloorParam == null)
          {
            this.ActivateOutputLinks(2);
            break;
          }
          int totalUnitNum = JSON_MyPhotonRoomParam.GetTotalUnitNum(mtFloorParam.GetQuestParam());
          int[] numArray = new int[totalUnitNum];
          int num = 0;
          for (int index = 0; index < totalUnitNum; ++index)
            numArray[index] = -1;
          for (int index1 = 0; index1 < roomPlayerList3.Count; ++index1)
          {
            JSON_MyPhotonPlayerParam photonPlayerParam2 = JSON_MyPhotonPlayerParam.Parse(roomPlayerList3[index1].json);
            for (int index2 = 0; index2 < photonPlayerParam2.units.Length; ++index2)
            {
              if (photonPlayerParam2.units[index2] != null && photonPlayerParam2.units[index2].sub == 0 && photonPlayerParam2.units[index2].slotID >= 0)
                numArray[num++] = photonPlayerParam2.units[index2].place;
            }
          }
          for (int index1 = 0; index1 < totalUnitNum; ++index1)
          {
            if (numArray[index1] != -1)
            {
              for (int index2 = index1 + 1; index2 < totalUnitNum; ++index2)
              {
                if (numArray[index2] != -1 && numArray[index1] == numArray[index2])
                {
                  this.ActivateOutputLinks(2);
                  return;
                }
              }
            }
          }
          this.ActivateOutputLinks(1);
          break;
        }
        this.ActivateOutputLinks(2);
        break;
      case 401:
        MyPhoton instance4 = PunMonoSingleton<MyPhoton>.Instance;
        if (Object.op_Inequality((Object) instance4, (Object) null))
        {
          List<MyPhoton.MyPlayer> roomPlayerList3 = instance4.GetRoomPlayerList();
          MyPhoton.MyRoom currentRoom2 = instance4.GetCurrentRoom();
          if (currentRoom2 == null)
          {
            this.ActivateOutputLinks(2);
            break;
          }
          JSON_MyPhotonRoomParam myPhotonRoomParam = JSON_MyPhotonRoomParam.Parse(currentRoom2.json);
          if (roomPlayerList3 == null || myPhotonRoomParam == null)
          {
            this.ActivateOutputLinks(2);
            break;
          }
          MultiTowerFloorParam mtFloorParam = MonoSingleton<GameManager>.Instance.GetMTFloorParam(GlobalVars.SelectedMultiTowerID, myPhotonRoomParam.challegedMTFloor);
          if (mtFloorParam == null)
          {
            this.ActivateOutputLinks(2);
            break;
          }
          QuestParam questParam = mtFloorParam.GetQuestParam();
          for (int index1 = 0; index1 < roomPlayerList3.Count; ++index1)
          {
            JSON_MyPhotonPlayerParam photonPlayerParam2 = JSON_MyPhotonPlayerParam.Parse(roomPlayerList3[index1].json);
            for (int index2 = 0; index2 < photonPlayerParam2.units.Length; ++index2)
            {
              if (photonPlayerParam2.units[index2] != null)
              {
                UnitData unit = new UnitData();
                unit.Deserialize(photonPlayerParam2.units[index2].unitJson);
                if (!questParam.IsEntryQuestCondition(unit))
                {
                  this.ActivateOutputLinks(2);
                  return;
                }
              }
            }
          }
          this.ActivateOutputLinks(1);
          break;
        }
        this.ActivateOutputLinks(2);
        break;
      case 402:
        MyPhoton instance5 = PunMonoSingleton<MyPhoton>.Instance;
        if (Object.op_Inequality((Object) instance5, (Object) null))
        {
          MyPhoton.MyRoom currentRoom2 = instance5.GetCurrentRoom();
          if (currentRoom2 == null)
          {
            this.ActivateOutputLinks(2);
            break;
          }
          JSON_MyPhotonRoomParam myPhotonRoomParam = JSON_MyPhotonRoomParam.Parse(currentRoom2.json);
          if (myPhotonRoomParam == null)
          {
            this.ActivateOutputLinks(2);
            break;
          }
          MultiTowerFloorParam mtFloorParam = MonoSingleton<GameManager>.Instance.GetMTFloorParam(GlobalVars.SelectedMultiTowerID, myPhotonRoomParam.challegedMTFloor);
          if (mtFloorParam == null)
          {
            this.ActivateOutputLinks(2);
            break;
          }
          MyPhoton.MyPlayer myPlayer3 = instance5.GetMyPlayer();
          if (myPlayer3 == null)
          {
            this.ActivateOutputLinks(2);
            break;
          }
          JSON_MyPhotonPlayerParam photonPlayerParam2 = JSON_MyPhotonPlayerParam.Parse(myPlayer3.json);
          QuestParam questParam = mtFloorParam.GetQuestParam();
          if (photonPlayerParam2 == null)
          {
            this.ActivateOutputLinks(2);
            break;
          }
          for (int index = 0; index < photonPlayerParam2.units.Length; ++index)
          {
            if (photonPlayerParam2.units[index] != null)
            {
              UnitData unit = new UnitData();
              unit.Deserialize(photonPlayerParam2.units[index].unitJson);
              if (!questParam.IsEntryQuestCondition(unit))
              {
                this.ActivateOutputLinks(2);
                return;
              }
            }
          }
          this.ActivateOutputLinks(1);
          break;
        }
        this.ActivateOutputLinks(2);
        break;
      case 403:
        MyPhoton instance6 = PunMonoSingleton<MyPhoton>.Instance;
        if (Object.op_Inequality((Object) instance6, (Object) null))
        {
          List<MyPhoton.MyPlayer> roomPlayerList3 = instance6.GetRoomPlayerList();
          MyPhoton.MyRoom currentRoom2 = instance6.GetCurrentRoom();
          if (currentRoom2 == null)
          {
            this.ActivateOutputLinks(2);
            break;
          }
          JSON_MyPhotonRoomParam myPhotonRoomParam = JSON_MyPhotonRoomParam.Parse(currentRoom2.json);
          if (myPhotonRoomParam == null)
          {
            this.ActivateOutputLinks(2);
            break;
          }
          MultiTowerFloorParam mtFloorParam = MonoSingleton<GameManager>.Instance.GetMTFloorParam(GlobalVars.SelectedMultiTowerID, myPhotonRoomParam.challegedMTFloor);
          if (mtFloorParam == null)
          {
            this.ActivateOutputLinks(2);
            break;
          }
          if (mtFloorParam.GetQuestParam() == null)
          {
            this.ActivateOutputLinks(2);
            break;
          }
          for (int index = 0; index < roomPlayerList3.Count; ++index)
          {
            JSON_MyPhotonPlayerParam photonPlayerParam2 = JSON_MyPhotonPlayerParam.Parse(roomPlayerList3[index].json);
            if (photonPlayerParam2 != null && myPhotonRoomParam.challegedMTFloor > photonPlayerParam2.mtChallengeFloor)
            {
              this.ActivateOutputLinks(2);
              return;
            }
          }
          this.ActivateOutputLinks(1);
          break;
        }
        this.ActivateOutputLinks(2);
        break;
    }
  }
}
