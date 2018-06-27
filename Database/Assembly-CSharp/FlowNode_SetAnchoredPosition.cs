// Decompiled with JetBrains decompiler
// Type: FlowNode_SetAnchoredPosition
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;

[FlowNode.Pin(1, "Out", FlowNode.PinTypes.Output, 1)]
[FlowNode.Pin(0, "In", FlowNode.PinTypes.Input, 0)]
[FlowNode.NodeType("UI/SetAnchoredPosition", 32741)]
public class FlowNode_SetAnchoredPosition : FlowNode
{
  [FlowNode.ShowInInfo]
  [FlowNode.DropTarget(typeof (RectTransform), true)]
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
