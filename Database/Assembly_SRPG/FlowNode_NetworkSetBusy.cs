// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_NetworkSetBusy
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  [FlowNode.Pin(0, "Set", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(100, "Output", FlowNode.PinTypes.Output, 100)]
  [FlowNode.NodeType("Network/SetBusy", 32741)]
  [FlowNode.Pin(1, "Reset", FlowNode.PinTypes.Input, 1)]
  public class FlowNode_NetworkSetBusy : FlowNode
  {
    public override void OnActivate(int pinID)
    {
      switch (pinID)
      {
        case 0:
          Network.IsForceBusy = true;
          DebugUtility.LogError("Set Busy");
          break;
        case 1:
          Network.IsForceBusy = false;
          DebugUtility.LogError("Reset Busy");
          break;
      }
      this.ActivateOutputLinks(100);
    }
  }
}
