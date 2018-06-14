// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_MultiPlaySelectRoomType
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  [FlowNode.Pin(1, "SelectVersus", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(10, "Test", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(100, "Raid", FlowNode.PinTypes.Output, 0)]
  [FlowNode.Pin(101, "Versus", FlowNode.PinTypes.Output, 0)]
  [FlowNode.Pin(0, "SelectRaid", FlowNode.PinTypes.Input, 0)]
  [FlowNode.NodeType("Multi/MultiPlaySelectRoomType", 32741)]
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
      }
      if (GlobalVars.SelectedMultiPlayRoomType == JSON_MyPhotonRoomParam.EType.RAID)
      {
        this.ActivateOutputLinks(100);
      }
      else
      {
        if (GlobalVars.SelectedMultiPlayRoomType != JSON_MyPhotonRoomParam.EType.VERSUS)
          return;
        this.ActivateOutputLinks(101);
      }
    }
  }
}
