// Decompiled with JetBrains decompiler
// Type: FlowNodeEvent`1
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

public abstract class FlowNodeEvent<T> : FlowNode where T : FlowNode
{
  private static List<FlowNode> mNodes = new List<FlowNode>();

  protected override void Awake()
  {
    ((Behaviour) this).set_enabled(false);
    FlowNodeEvent<T>.mNodes.Add((FlowNode) this);
  }

  protected override void OnDestroy()
  {
    FlowNodeEvent<T>.mNodes.Remove((FlowNode) this);
  }

  public static void Invoke()
  {
    for (int index = 0; index < FlowNodeEvent<T>.mNodes.Count; ++index)
      FlowNodeEvent<T>.mNodes[index].Activate(-1);
  }

  public override void OnActivate(int pinID)
  {
    if (pinID != -1)
      return;
    this.ActivateOutputLinks(1);
  }
}
