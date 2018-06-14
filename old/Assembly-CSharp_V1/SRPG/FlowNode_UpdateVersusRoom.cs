// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_UpdateVersusRoom
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  [FlowNode.NodeType("Multi/UpdateRoomParam", 32741)]
  [FlowNode.Pin(200, "Finish", FlowNode.PinTypes.Output, 200)]
  [FlowNode.Pin(101, "Reflesh", FlowNode.PinTypes.Input, 101)]
  [FlowNode.Pin(100, "Update", FlowNode.PinTypes.Input, 100)]
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
      }
      this.ActivateOutputLinks(200);
    }
  }
}
