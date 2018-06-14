// Decompiled with JetBrains decompiler
// Type: FlowNode_Input
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

[FlowNode.Pin(1, "Output", FlowNode.PinTypes.Output, 0)]
[FlowNode.NodeType("Event/Input", 58751)]
public class FlowNode_Input : FlowNode
{
  public string PinName;

  public override string GetCaption()
  {
    return base.GetCaption() + ":" + this.PinName;
  }

  public override void OnActivate(int pinID)
  {
    this.ActivateOutputLinks(1);
  }
}
