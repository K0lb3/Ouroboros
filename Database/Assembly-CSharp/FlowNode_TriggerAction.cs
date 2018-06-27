// Decompiled with JetBrains decompiler
// Type: FlowNode_TriggerAction
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.Events;

[FlowNode.Pin(1, "Out", FlowNode.PinTypes.Output, 1)]
[FlowNode.ShowInInspector]
[FlowNode.NodeType("Event/TriggerAction", 32741)]
[FlowNode.Pin(0, "In", FlowNode.PinTypes.Input, 0)]
public class FlowNode_TriggerAction : FlowNode
{
  [SerializeField]
  public UnityEvent Action;

  public override void OnActivate(int pinID)
  {
    if (pinID != 0)
      return;
    this.Action.Invoke();
    this.ActivateOutputLinks(1);
  }
}
