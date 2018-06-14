// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_VersusAward
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(200, "Out", FlowNode.PinTypes.Output, 0)]
  [FlowNode.NodeType("Multi/Award", 32741)]
  [FlowNode.Pin(100, "Set", FlowNode.PinTypes.Input, 0)]
  public class FlowNode_VersusAward : FlowNode
  {
    private readonly int ROOM_MAX_PLAYERCNT = 2;
    public GameObject BindObj;
    public bool MyPlayer;

    public override void OnActivate(int pinID)
    {
      if (pinID == 100)
      {
        GameManager instance = MonoSingleton<GameManager>.Instance;
        JSON_MyPhotonPlayerParam data = (JSON_MyPhotonPlayerParam) null;
        if (instance.AudienceMode)
        {
          MyPhoton.MyRoom audienceRoom = instance.AudienceRoom;
          if (audienceRoom != null)
          {
            JSON_MyPhotonRoomParam myPhotonRoomParam = JSON_MyPhotonRoomParam.Parse(audienceRoom.json);
            if (myPhotonRoomParam != null && myPhotonRoomParam.players != null && myPhotonRoomParam.players.Length >= this.ROOM_MAX_PLAYERCNT)
              data = myPhotonRoomParam.players[!this.MyPlayer ? 1 : 0];
          }
        }
        else
        {
          // ISSUE: object of a compiler-generated type is created
          // ISSUE: variable of a compiler-generated type
          FlowNode_VersusAward.\u003COnActivate\u003Ec__AnonStorey2D6 activateCAnonStorey2D6 = new FlowNode_VersusAward.\u003COnActivate\u003Ec__AnonStorey2D6();
          // ISSUE: reference to a compiler-generated field
          activateCAnonStorey2D6.pt = PunMonoSingleton<MyPhoton>.Instance;
          // ISSUE: reference to a compiler-generated field
          List<MyPhoton.MyPlayer> roomPlayerList = activateCAnonStorey2D6.pt.GetRoomPlayerList();
          if (roomPlayerList != null)
          {
            if (this.MyPlayer)
            {
              data = JSON_MyPhotonPlayerParam.Create(0, 0);
            }
            else
            {
              // ISSUE: reference to a compiler-generated method
              MyPhoton.MyPlayer myPlayer = roomPlayerList.Find(new Predicate<MyPhoton.MyPlayer>(activateCAnonStorey2D6.\u003C\u003Em__2CB));
              if (myPlayer != null)
                data = JSON_MyPhotonPlayerParam.Parse(myPlayer.json);
            }
          }
        }
        if (data != null)
          DataSource.Bind<JSON_MyPhotonPlayerParam>(this.BindObj, data);
      }
      this.ActivateOutputLinks(200);
    }
  }
}
