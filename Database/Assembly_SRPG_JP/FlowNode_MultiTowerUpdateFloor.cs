// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_MultiTowerUpdateFloor
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  [FlowNode.Pin(102, "UpdatePass", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(2, "Failure", FlowNode.PinTypes.Output, 0)]
  [FlowNode.Pin(1, "Success", FlowNode.PinTypes.Output, 0)]
  [FlowNode.NodeType("Multi/UpdateFloor", 32741)]
  [FlowNode.Pin(101, "Update", FlowNode.PinTypes.Input, 0)]
  public class FlowNode_MultiTowerUpdateFloor : FlowNode
  {
    public override void OnActivate(int pinID)
    {
      switch (pinID)
      {
        case 101:
          MyPhoton instance1 = PunMonoSingleton<MyPhoton>.Instance;
          if (!instance1.IsOldestPlayer())
          {
            DebugUtility.Log("I'm not room owner");
            this.ActivateOutputLinks(2);
            break;
          }
          MyPhoton.MyRoom currentRoom1 = instance1.GetCurrentRoom();
          if (currentRoom1 == null)
          {
            DebugUtility.Log("CurrentRoom is null");
            this.ActivateOutputLinks(2);
            break;
          }
          JSON_MyPhotonRoomParam myPhotonRoomParam = JSON_MyPhotonRoomParam.Parse(currentRoom1.json);
          if (myPhotonRoomParam == null)
          {
            DebugUtility.Log("no roomParam");
            this.ActivateOutputLinks(2);
            break;
          }
          myPhotonRoomParam.challegedMTFloor = GlobalVars.SelectedMultiTowerFloor;
          myPhotonRoomParam.iname = GlobalVars.SelectedMultiTowerID + "_" + myPhotonRoomParam.challegedMTFloor.ToString();
          instance1.SetRoomParam(myPhotonRoomParam.Serialize());
          instance1.UpdateRoomParam("floor", (object) GlobalVars.SelectedMultiTowerFloor);
          GlobalVars.SelectedQuestID = myPhotonRoomParam.iname;
          this.ActivateOutputLinks(1);
          break;
        case 102:
          MyPhoton instance2 = PunMonoSingleton<MyPhoton>.Instance;
          if (!instance2.IsOldestPlayer())
          {
            DebugUtility.Log("I'm not room owner");
            this.ActivateOutputLinks(2);
            break;
          }
          MyPhoton.MyRoom currentRoom2 = instance2.GetCurrentRoom();
          if (currentRoom2 == null)
          {
            DebugUtility.Log("CurrentRoom is null");
            this.ActivateOutputLinks(2);
            break;
          }
          if (JSON_MyPhotonRoomParam.Parse(currentRoom2.json) == null)
          {
            DebugUtility.Log("no roomParam");
            this.ActivateOutputLinks(2);
            break;
          }
          instance2.UpdateRoomParam("Lock", (object) (GlobalVars.EditMultiPlayRoomPassCode != "0"));
          this.ActivateOutputLinks(1);
          break;
      }
    }
  }
}
