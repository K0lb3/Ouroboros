// Decompiled with JetBrains decompiler
// Type: FlowNode_Output
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System;
using UnityEngine;

[FlowNode.Pin(1, "", FlowNode.PinTypes.Input, 0)]
[FlowNode.NodeType("Event/Output", 32741)]
public class FlowNode_Output : FlowNode
{
  public string PinName;
  [NonSerialized]
  public FlowNode_ExternalLink TargetNode;
  [NonSerialized]
  public int TargetPinID;

  public override string GetCaption()
  {
    return base.GetCaption() + ":" + this.PinName;
  }

  public override void OnActivate(int pinID)
  {
    if (!Object.op_Inequality((Object) this.TargetNode, (Object) null))
      return;
    this.TargetNode.ActivateOutputLinks(this.TargetPinID);
  }
}
