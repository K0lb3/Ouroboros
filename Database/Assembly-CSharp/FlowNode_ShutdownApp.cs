// Decompiled with JetBrains decompiler
// Type: FlowNode_ShutdownApp
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
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
