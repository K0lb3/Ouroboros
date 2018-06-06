// Decompiled with JetBrains decompiler
// Type: FlowNode_ToggleInput
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using SRPG;

[FlowNode.Pin(10, "Enable", FlowNode.PinTypes.Input, 0)]
[FlowNode.Pin(1, "Output", FlowNode.PinTypes.Output, 0)]
[FlowNode.NodeType("Toggle/Input", 32741)]
[FlowNode.Pin(11, "Disable", FlowNode.PinTypes.Input, 0)]
public class FlowNode_ToggleInput : FlowNode
{
  public override void OnActivate(int pinID)
  {
    switch (pinID)
    {
      case 10:
        SRPG_TouchInputModule.LockInput();
        break;
      case 11:
        SRPG_TouchInputModule.UnlockInput(false);
        break;
    }
    this.ActivateOutputLinks(1);
  }
}
