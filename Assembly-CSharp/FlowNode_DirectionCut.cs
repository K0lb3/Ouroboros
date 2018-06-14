// Decompiled with JetBrains decompiler
// Type: FlowNode_DirectionCut
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

[FlowNode.Pin(1, "Off", FlowNode.PinTypes.Input, 1)]
[FlowNode.ShowInInspector]
[FlowNode.NodeType("Event/DirectionCut", 32741)]
[FlowNode.Pin(0, "On", FlowNode.PinTypes.Input, 0)]
[FlowNode.Pin(10, "Out", FlowNode.PinTypes.Output, 2)]
public class FlowNode_DirectionCut : FlowNode
{
  public override void OnActivate(int pinID)
  {
    switch (pinID)
    {
      case 0:
        GameUtility.Config_DirectionCut.Value = true;
        break;
      case 1:
        GameUtility.Config_DirectionCut.Value = false;
        break;
    }
    this.ActivateOutputLinks(10);
  }
}
