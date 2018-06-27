// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_MultiPlaySelectRoomType
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  [FlowNode.Pin(102, "Tower", FlowNode.PinTypes.Output, 0)]
  [FlowNode.Pin(100, "Raid", FlowNode.PinTypes.Output, 0)]
  [FlowNode.Pin(10, "Test", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(2, "SelectTower", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(1, "SelectVersus", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(0, "SelectRaid", FlowNode.PinTypes.Input, 0)]
  [FlowNode.NodeType("Multi/MultiPlaySelectRoomType", 32741)]
  [FlowNode.Pin(101, "Versus", FlowNode.PinTypes.Output, 0)]
  public class FlowNode_MultiPlaySelectRoomType : FlowNode
  {
    public override void OnActivate(int pinID)
    {
      switch (pinID)
      {
        case 0:
          DebugUtility.Log("raid selected");
          GlobalVars.SelectedMultiPlayRoomType = JSON_MyPhotonRoomParam.EType.RAID;
          break;
        case 1:
          DebugUtility.Log("versus selected");
          GlobalVars.SelectedMultiPlayRoomType = JSON_MyPhotonRoomParam.EType.VERSUS;
          break;
        case 2:
          DebugUtility.Log("tower selected");
          GlobalVars.SelectedMultiPlayRoomType = JSON_MyPhotonRoomParam.EType.TOWER;
          break;
      }
      switch (GlobalVars.SelectedMultiPlayRoomType)
      {
        case JSON_MyPhotonRoomParam.EType.RAID:
          this.ActivateOutputLinks(100);
          break;
        case JSON_MyPhotonRoomParam.EType.VERSUS:
          this.ActivateOutputLinks(101);
          break;
        case JSON_MyPhotonRoomParam.EType.TOWER:
          this.ActivateOutputLinks(102);
          break;
      }
    }
  }
}
