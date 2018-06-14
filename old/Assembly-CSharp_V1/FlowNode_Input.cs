// Decompiled with JetBrains decompiler
// Type: FlowNode_Input
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

[FlowNode.NodeType("Event/Input", 58751)]
[FlowNode.Pin(1, "Output", FlowNode.PinTypes.Output, 0)]
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
