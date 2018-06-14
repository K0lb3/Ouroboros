// Decompiled with JetBrains decompiler
// Type: FlowNode_MultiPlayIsReady
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using SRPG;
using System.Collections.Generic;

[FlowNode.NodeType("Multi/MultiPlayIsReady", 32741)]
[FlowNode.Pin(1, "Yes", FlowNode.PinTypes.Output, 2)]
[FlowNode.Pin(102, "Are all the members except the room owner ready?", FlowNode.PinTypes.Input, 102)]
[FlowNode.Pin(2, "No", FlowNode.PinTypes.Output, 3)]
[FlowNode.Pin(200, "Is the room full?", FlowNode.PinTypes.Input, 200)]
[FlowNode.Pin(300, "Is stamina sufficient?", FlowNode.PinTypes.Input, 300)]
[FlowNode.Pin(100, "Are you ready for preparation?", FlowNode.PinTypes.Input, 100)]
[FlowNode.Pin(101, "Is everyone ready?", FlowNode.PinTypes.Input, 101)]
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
              if (photonPlayerParam2 == null || photonPlayerParam2.state == 0 || photonPlayerParam2.state == 4)
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
        MyPhoton instance = PunMonoSingleton<MyPhoton>.Instance;
        List<MyPhoton.MyPlayer> roomPlayerList2 = instance.GetRoomPlayerList();
        MyPhoton.MyPlayer myPlayer2 = instance.GetMyPlayer();
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
                if (photonPlayerParam2 == null || photonPlayerParam2.state == 0 || photonPlayerParam2.state == 4)
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
        MyPhoton.MyRoom currentRoom = PunMonoSingleton<MyPhoton>.Instance.GetCurrentRoom();
        if (currentRoom == null || currentRoom.playerCount < currentRoom.maxPlayers)
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
        PlayerData player = MonoSingleton<GameManager>.Instance.Player;
        if (quest != null && player != null)
        {
          if (player.Stamina >= quest.RequiredApWithPlayerLv(player.Lv, true))
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
    }
  }
}
