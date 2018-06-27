// Decompiled with JetBrains decompiler
// Type: FlowNode_Startup
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;

[FlowNode.Pin(1, "Output", FlowNode.PinTypes.Output, 0)]
[FlowNode.NodeType("Event/Startup", 58751)]
public class FlowNode_Startup : FlowNodePersistent
{
  private void Start()
  {
    this.ActivateOutputLinks(1);
    ((Behaviour) this).set_enabled(false);
  }

  public override void OnActivate(int pinID)
  {
  }
}
