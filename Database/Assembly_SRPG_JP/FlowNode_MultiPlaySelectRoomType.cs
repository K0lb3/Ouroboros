// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_MultiPlaySelectRoomType
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  [FlowNode.Pin(2, "SelectTower", FlowNode.PinTypes.Input, 2)]
  [FlowNode.Pin(100, "Raid", FlowNode.PinTypes.Output, 100)]
  [FlowNode.Pin(101, "Versus", FlowNode.PinTypes.Output, 101)]
  [FlowNode.Pin(0, "SelectRaid", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(1, "SelectVersus", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(3, "SelectRankMatch", FlowNode.PinTypes.Input, 3)]
  [FlowNode.Pin(102, "Tower", FlowNode.PinTypes.Output, 102)]
  [FlowNode.Pin(103, "RankMatch", FlowNode.PinTypes.Output, 103)]
  [FlowNode.NodeType("Multi/MultiPlaySelectRoomType", 32741)]
  [FlowNode.Pin(10, "Test", FlowNode.PinTypes.Input, 10)]
  public class FlowNode_MultiPlaySelectRoomType : FlowNode
  {
    private const int PIN_IN_SELECT_RAID = 0;
    private const int PIN_IN_SELECT_VERSUS = 1;
    private const int PIN_IN_SELECT_TOWER = 2;
    private const int PIN_IN_SELECT_RANKMATCH = 3;
    private const int PIN_IN_TEST = 10;
    private const int PIN_OUT_RAID = 100;
    private const int PIN_OUT_VERSUS = 101;
    private const int PIN_OUT_TOWER = 102;
    private const int PIN_OUT_RANKMATCH = 103;

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
        case 3:
          DebugUtility.Log("rank match selected");
          GlobalVars.SelectedMultiPlayRoomType = JSON_MyPhotonRoomParam.EType.RANKMATCH;
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
        case JSON_MyPhotonRoomParam.EType.RANKMATCH:
          this.ActivateOutputLinks(103);
          break;
        default:
          this.ActivateOutputLinks(100);
          break;
      }
    }
  }
}
