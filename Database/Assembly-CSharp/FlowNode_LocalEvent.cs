// Decompiled with JetBrains decompiler
// Type: FlowNode_LocalEvent
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;

[AddComponentMenu("")]
[FlowNode.Pin(1, "Triggered", FlowNode.PinTypes.Output, 0)]
[FlowNode.NodeType("Event/LocalEvent", 58751)]
public class FlowNode_LocalEvent : FlowNode
{
  [FlowNode.ShowInInfo]
  public string EventName;

  public override void OnActivate(int pinID)
  {
    this.ActivateOutputLinks(1);
  }
}
