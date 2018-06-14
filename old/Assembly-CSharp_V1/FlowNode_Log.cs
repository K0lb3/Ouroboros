// Decompiled with JetBrains decompiler
// Type: FlowNode_Log
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;

[FlowNode.NodeType("Debug/Log", 32741)]
[FlowNode.Pin(10, "Input", FlowNode.PinTypes.Input, 0)]
[FlowNode.Pin(1, "Output", FlowNode.PinTypes.Output, 1)]
public class FlowNode_Log : FlowNode
{
  public string Value;

  public override void OnActivate(int pinID)
  {
    Debug.Log((object) this.Value);
    this.ActivateOutputLinks(1);
  }
}
