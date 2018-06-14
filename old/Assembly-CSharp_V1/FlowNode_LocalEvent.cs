// Decompiled with JetBrains decompiler
// Type: FlowNode_LocalEvent
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;

[FlowNode.NodeType("Event/LocalEvent", 58751)]
[FlowNode.Pin(1, "Triggered", FlowNode.PinTypes.Output, 0)]
[AddComponentMenu("")]
public class FlowNode_LocalEvent : FlowNode
{
  [FlowNode.ShowInInfo]
  public string EventName;

  public override void OnActivate(int pinID)
  {
    this.ActivateOutputLinks(1);
  }
}
