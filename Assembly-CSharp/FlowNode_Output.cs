// Decompiled with JetBrains decompiler
// Type: FlowNode_Output
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System;
using UnityEngine;

[FlowNode.NodeType("Event/Output", 32741)]
[FlowNode.Pin(1, "", FlowNode.PinTypes.Input, 0)]
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
