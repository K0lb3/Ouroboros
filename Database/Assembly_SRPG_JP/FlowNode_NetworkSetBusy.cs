// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_NetworkSetBusy
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  [FlowNode.Pin(1, "Reset", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(0, "Set", FlowNode.PinTypes.Input, 0)]
  [FlowNode.NodeType("Network/SetBusy", 32741)]
  [FlowNode.Pin(100, "Output", FlowNode.PinTypes.Output, 100)]
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
