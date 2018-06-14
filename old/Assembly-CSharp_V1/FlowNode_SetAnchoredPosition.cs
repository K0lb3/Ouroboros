// Decompiled with JetBrains decompiler
// Type: FlowNode_SetAnchoredPosition
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;

[FlowNode.Pin(1, "Out", FlowNode.PinTypes.Output, 1)]
[FlowNode.NodeType("UI/SetAnchoredPosition", 32741)]
[FlowNode.Pin(0, "In", FlowNode.PinTypes.Input, 0)]
public class FlowNode_SetAnchoredPosition : FlowNode
{
  [FlowNode.DropTarget(typeof (RectTransform), true)]
  [FlowNode.ShowInInfo]
  public RectTransform Target;
  public Vector2 TargetPosition;

  public override void OnActivate(int pinID)
  {
    if (pinID != 0)
      return;
    if (Object.op_Inequality((Object) this.Target, (Object) null))
      this.Target.set_anchoredPosition(this.TargetPosition);
    this.ActivateOutputLinks(1);
  }
}
