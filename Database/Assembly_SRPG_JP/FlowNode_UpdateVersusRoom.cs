// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_UpdateVersusRoom
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(300, "AudienceOn", FlowNode.PinTypes.Input, 300)]
  [FlowNode.NodeType("Multi/UpdateRoomParam", 32741)]
  [FlowNode.Pin(101, "Reflesh", FlowNode.PinTypes.Input, 101)]
  [FlowNode.Pin(100, "Update", FlowNode.PinTypes.Input, 100)]
  [FlowNode.Pin(301, "AudienceOff", FlowNode.PinTypes.Input, 301)]
  [FlowNode.Pin(200, "Finish", FlowNode.PinTypes.Output, 200)]
  public class FlowNode_UpdateVersusRoom : FlowNode
  {
    public override void OnActivate(int pinID)
    {
      switch (pinID)
      {
        case 100:
          MyPhoton instance1 = PunMonoSingleton<MyPhoton>.Instance;
          if (Object.op_Inequality((Object) instance1, (Object) null) && instance1.IsOldestPlayer())
          {
            MyPhoton.MyRoom currentRoom = instance1.GetCurrentRoom();
            if (currentRoom != null)
            {
              JSON_MyPhotonRoomParam myPhotonRoomParam = JSON_MyPhotonRoomParam.Parse(currentRoom.json);
              myPhotonRoomParam.iname = GlobalVars.SelectedQuestID;
              instance1.SetRoomParam(myPhotonRoomParam.Serialize());
            }
          }
          this.ActivateOutputLinks(200);
          break;
        case 101:
          MyPhoton instance2 = PunMonoSingleton<MyPhoton>.Instance;
          if (Object.op_Inequality((Object) instance2, (Object) null))
          {
            MyPhoton.MyRoom currentRoom = instance2.GetCurrentRoom();
            if (currentRoom != null)
            {
              GlobalVars.SelectedQuestID = JSON_MyPhotonRoomParam.Parse(currentRoom.json).iname;
              break;
            }
            break;
          }
          break;
        case 300:
          MyPhoton instance3 = PunMonoSingleton<MyPhoton>.Instance;
          if (Object.op_Inequality((Object) instance3, (Object) null))
          {
            MyPhoton.MyRoom currentRoom = instance3.GetCurrentRoom();
            if (currentRoom != null)
            {
              JSON_MyPhotonRoomParam myPhotonRoomParam = JSON_MyPhotonRoomParam.Parse(currentRoom.json);
              myPhotonRoomParam.audience = 1;
              instance3.SetRoomParam(myPhotonRoomParam.Serialize());
              break;
            }
            break;
          }
          break;
        case 301:
          MyPhoton instance4 = PunMonoSingleton<MyPhoton>.Instance;
          if (Object.op_Inequality((Object) instance4, (Object) null))
          {
            MyPhoton.MyRoom currentRoom = instance4.GetCurrentRoom();
            if (currentRoom != null)
            {
              JSON_MyPhotonRoomParam myPhotonRoomParam = JSON_MyPhotonRoomParam.Parse(currentRoom.json);
              myPhotonRoomParam.audience = 0;
              instance4.SetRoomParam(myPhotonRoomParam.Serialize());
              break;
            }
            break;
          }
          break;
      }
      this.ActivateOutputLinks(200);
    }
  }
}
