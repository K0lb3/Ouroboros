// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_CheckMultiType
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  [FlowNode.Pin(100, "Coop", FlowNode.PinTypes.Output, 100)]
  [FlowNode.Pin(200, "Versus", FlowNode.PinTypes.Output, 200)]
  [FlowNode.Pin(9999, "Invalid", FlowNode.PinTypes.Output, 9999)]
  [FlowNode.Pin(0, "Check", FlowNode.PinTypes.Input, 0)]
  [FlowNode.NodeType("Multi/CheckSchemeType", 32741)]
  [FlowNode.Pin(300, "Tower", FlowNode.PinTypes.Output, 300)]
  public class FlowNode_CheckMultiType : FlowNode
  {
    public override void OnActivate(int pinID)
    {
      if (pinID != 0)
        return;
      if (FlowNode_OnUrlSchemeLaunch.LINEParam_Pending.type == JSON_MyPhotonRoomParam.EType.RAID)
        this.ActivateOutputLinks(100);
      else if (FlowNode_OnUrlSchemeLaunch.LINEParam_Pending.type == JSON_MyPhotonRoomParam.EType.VERSUS)
      {
        this.ActivateOutputLinks(200);
      }
      else
      {
        if (FlowNode_OnUrlSchemeLaunch.LINEParam_Pending.type != JSON_MyPhotonRoomParam.EType.TOWER)
          return;
        this.ActivateOutputLinks(300);
      }
    }
  }
}
