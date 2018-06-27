// Decompiled with JetBrains decompiler
// Type: FlowNode_ActionCall
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using SRPG;
using UnityEngine;

[FlowNode.Pin(10, "Input", FlowNode.PinTypes.Input, 0)]
[FlowNode.Pin(1, "Output", FlowNode.PinTypes.Output, 1)]
[FlowNode.NodeType("UI/ActionCall", 32741)]
public class FlowNode_ActionCall : FlowNode
{
  [FlowNode.ShowInInfo]
  [FlowNode.DropTarget(typeof (GameObject), true)]
  public GameObject Target;
  public ActionCall.EventType EventType;
  public SerializeValueList Value;

  public override void OnActivate(int pinID)
  {
    ActionCall component = (ActionCall) (!Object.op_Inequality((Object) this.Target, (Object) null) ? ((Component) this).get_gameObject() : this.Target).GetComponent<ActionCall>();
    if (Object.op_Inequality((Object) component, (Object) null))
      component.Invoke(this.EventType, this.Value);
    this.ActivateOutputLinks(1);
  }
}
