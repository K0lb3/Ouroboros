// Decompiled with JetBrains decompiler
// Type: FlowNode_EventCall
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using SRPG;
using UnityEngine;

[FlowNode.NodeType("Event/Call", 32741)]
[FlowNode.Pin(1, "Output", FlowNode.PinTypes.Output, 1)]
[FlowNode.Pin(10, "Input", FlowNode.PinTypes.Input, 0)]
public class FlowNode_EventCall : FlowNode
{
  [FlowNode.ShowInInfo]
  public string Key = string.Empty;
  public string Value = string.Empty;
  [FlowNode.DropTarget(typeof (GameObject), true)]
  [FlowNode.ShowInInfo]
  public GameObject Target;

  public override void OnActivate(int pinID)
  {
    EventCall component = (EventCall) (!Object.op_Inequality((Object) this.Target, (Object) null) ? ((Component) this).get_gameObject() : this.Target).GetComponent<EventCall>();
    if (Object.op_Inequality((Object) component, (Object) null))
      component.Invoke(this.Key, this.Value);
    this.ActivateOutputLinks(1);
  }
}
