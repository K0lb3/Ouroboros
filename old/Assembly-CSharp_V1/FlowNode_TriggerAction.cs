// Decompiled with JetBrains decompiler
// Type: FlowNode_TriggerAction
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.Events;

[FlowNode.NodeType("Event/TriggerAction", 32741)]
[FlowNode.ShowInInspector]
[FlowNode.Pin(1, "Out", FlowNode.PinTypes.Output, 1)]
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
