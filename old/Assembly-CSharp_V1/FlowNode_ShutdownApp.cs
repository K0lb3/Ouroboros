// Decompiled with JetBrains decompiler
// Type: FlowNode_ShutdownApp
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;

[FlowNode.NodeType("System/Shutdown Application")]
[FlowNode.Pin(1, "Shutdown", FlowNode.PinTypes.Input, 0)]
public class FlowNode_ShutdownApp : FlowNode
{
  public override void OnActivate(int pinID)
  {
    if (pinID != 1)
      return;
    Application.Quit();
  }
}
